﻿using BullDriver.Conexiones;
using BullDriver.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace BullDriver.Datos
{
    public class DataPedidos
    {
        
        public async Task<bool> InsertPedidos(Pedido parametros)
        {
            await Constantes.firebase
                .Child("Pedidos")
                .PostAsync(new Pedido
                {
                    Destino_lugar = parametros.Destino_lugar,
                    Origen_lugar = parametros.Origen_lugar,
                    Estado = parametros.Estado,
                    IdChofer = parametros.IdChofer,
                    IdUser = parametros.IdUser,
                    Lt_lg_destino = parametros.Lt_lg_destino,
                    Lt_lg_origen = parametros.Lt_lg_origen,
                    Tiempo = parametros.Tiempo,
                    Tarifa = parametros.Tarifa,
                    Distancia = parametros.Distancia,
                    Notificacion = parametros.Notificacion,
                    CalificarCliente = parametros.CalificarCliente,
                    CalificarConductor = parametros.CalificarConductor,
                    ComentarioConductor = parametros.ComentarioConductor
                });
            return true;
        }
        public async Task<string> ObtenerIdPedido(Pedido parametros)
        {
            var idPedido = "sin data";
            int contador = 0;
            contador = await ValidarPedido(parametros);

            if (contador > 0)
            {
                var data = (await Constantes.firebase
                .Child("Pedidos")
                .OnceAsync<Pedido>())
                .Where(a => a.Object.IdUser == parametros.IdUser)
                .Where(b => b.Object.Estado != "FINALIZADO")
                .Where(c => c.Object.IdUser != "-")
                .FirstOrDefault();
                if (data != null)
                {
                    idPedido = data.Key;
                    return idPedido;
                }
            }
            return idPedido;

        }
        private async Task<int> ValidarPedido(Pedido parametros)
        {
            int contador = 0;
            var data = (await Constantes.firebase
                .Child("Pedidos")
                .OnceAsync<Pedido>())
                .Where(a => a.Object.IdUser == parametros.IdUser)
                .Where(b => b.Object.Estado != "FINALIZADO")
                .Where(c => c.Object.IdUser != "-");

            contador = data.Count();
            return contador;
        }
        public async Task ConfirmarPedido(Pedido parametros)
        {
            parametros.IdPedido = await ObtenerIdPedido(parametros);
            //obtiene el pedido segun id
            var data = (await Constantes.firebase
                .Child("Pedidos")
                .OnceAsync<Pedido>())
                .Where(a => a.Key == parametros.IdPedido)
                .FirstOrDefault();

            //actualizamos datos
            data.Object.Estado = "CONFIRMADO";
            data.Object.Tarifa = parametros.Tarifa;
            data.Object.IdChofer = parametros.IdChofer;

            //enviamos datos actualizados a la api DB
            await Constantes.firebase
                .Child("Pedidos")
                .Child(data.Key)
                .PutAsync(data.Object);

            var funcion = new DataOfertasConductores();
            await funcion.EliminarListaOfertas(parametros);

        }
        public async Task<List<Pedido>> ListarPedidosPendientes(Pedido parametros)
        {
            return (await Constantes.firebase
                .Child("Pedidos")
                .OnceAsync<Pedido>())
                .Where(a => a.Object.IdUser == parametros.IdUser)
                .Where(b => b.Object.Estado == "PENDIENTE")
                .Select(item =>new Pedido
                {
                    IdPedido = item.Key,
                    Notificacion = item.Object.Notificacion
                }).ToList();
        }
        public async Task<List<Pedido>> ListarPedidosConfirmados(Pedido parametros)
        {
            return (await Constantes.firebase
                .Child("Pedidos")
                .OnceAsync<Pedido>())
                .Where(a => a.Object.IdUser == parametros.IdUser)
                .Where(b => b.Object.Estado == "CONFIRMADO")
                .Select(item => new Pedido
                {
                    IdPedido = item.Key,
                    Notificacion = item.Object.Notificacion
                }).ToList();
        }
        public async Task<List<Pedido>> ListarPedidosFinalizados(Pedido parametros)
        {
            return (await Constantes.firebase
                .Child("Pedidos")
                .OnceAsync<Pedido>())
                .Where(a => a.Object.IdUser == parametros.IdUser)
                .Where(b => b.Object.Estado == "FINALIZADO")
                .Where(b => b.Object.ComentarioConductor == "-")
                .Select(item => new Pedido
                {
                    IdPedido = item.Key,
                    Notificacion = item.Object.Notificacion
                }).ToList();
        }
        public async Task CalificarConductor(Pedido parametros)
        {
            //obtiene el pedido segun id
            var data = (await Constantes.firebase
                .Child("Pedidos")
                .OnceAsync<Pedido>())
                .Where(a => a.Key == parametros.IdPedido)
                .FirstOrDefault();

            //actualizamos datos
            data.Object.ComentarioConductor = parametros.ComentarioConductor;
            data.Object.CalificarConductor = parametros.CalificarConductor;

            //enviamos datos actualizados a la api DB
            await Constantes.firebase
                .Child("Pedidos")
                .Child(data.Key)
                .PutAsync(data.Object);
        }
    }
}