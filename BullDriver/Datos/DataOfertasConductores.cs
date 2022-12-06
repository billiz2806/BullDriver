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
        string idPedido;
        private async void obtenerPedido(Pedido parametros)
        {
            var funcion = new DataPedidos();
            idPedido = await funcion.ObtenerIdPedido(parametros);
        }
        public ObservableCollection<OfertaConductor> ListaOfertas(Pedido parametros)
        {
            obtenerPedido(parametros);
            var data = new ObservableCollection<OfertaConductor>();
            var collection = Constantes.firebase
                .Child("OfertasConductores")
                .AsObservable<OfertaConductor>()
                .Subscribe((item) =>
                {
                    if (item.Object.IdPedido == idPedido)
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

        public async Task EliminarOferta(OfertaConductor parametros)
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

        public async Task EliminarListaOfertas(Pedido parametrosIdUser)
        {
            try
            {
                var lista = ListaOfertas(parametrosIdUser);
                var parametro = new OfertaConductor();

                foreach (var item in lista)
                {
                    parametro.IdPedido = item.IdPedido;
                    await EliminarOferta(parametro);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
