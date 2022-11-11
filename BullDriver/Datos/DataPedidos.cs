using BullDriver.Conexiones;
using BullDriver.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
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
                });
            return true;
        }
    }
}
