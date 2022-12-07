using BullDriver.Conexiones;
using BullDriver.Models;
using BullDriver.Views.Navegacion;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullDriver.Datos
{
    public class DataOfertasConductores
    {
        public async Task< ObservableCollection<OfertaConductor>> ListaOfertas(Pedido parametros)
        {
            var data = new ObservableCollection<OfertaConductor>();
            var collection = Constantes.firebase
                .Child("OfertasConductores")
                .AsObservable<OfertaConductor>()
                .Subscribe(async (item) =>
                {
                    var funcion = new DataPedidos();
                    parametros.IdPedido = await funcion.ObtenerIdPedido(parametros);
                    //parametros.IdPedido = "-NIdf34Z9Ff9lUPtyS1E";
                    if (item.Object.IdPedido == parametros.IdPedido)
                    {
                        if (item.Key != item.Object.IdOferta)
                        {
                            item.Object.TimeSpan = TimeSpan.FromSeconds(20);
                            item.Object.IdOferta = item.Key;
                            item.Object.IdPedido = item.Object.IdPedido;
                            item.Object.Tarifa = item.Object.Tarifa;
                            item.Object.IdConductor = item.Object.IdConductor;
                            data.Add(item.Object);
                        }
                        else
                        {
                            data.Remove(item.Object);
                        }
                    }
                });
            return data;
        }

        public async Task EliminarOferta(Pedido parametros)
        {
            var data = (await Constantes.firebase
                .Child("OfertasConductores")
                .OnceAsync<OfertaConductor>())
                .Where(a => a.Object.IdPedido == parametros.IdPedido).FirstOrDefault();

            await Constantes.firebase
                .Child("OfertasConductores")
                .Child(data.Key)
                .DeleteAsync();
        }
        public async Task<List<OfertaConductor>> ListOfertasAEliminar(Pedido parametros)
        {
            return (await Constantes.firebase
                .Child("OfertasConductores")
                .OnceAsync<OfertaConductor>())
                .Where(a => a.Object.IdPedido == parametros.IdPedido)
                .Select(item => new OfertaConductor
                {
                    IdOferta = item.Key,
                    IdPedido = item.Object.IdPedido,
                }).ToList();
        }
        public async Task EliminarListaOfertas(Pedido parametrosIdUser)
        {
            try
            {
                var lista = await ListOfertasAEliminar(parametrosIdUser);
                if(lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        await EliminarOferta(parametrosIdUser);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
