using BullDriver.Datos;
using BullDriver.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BullDriver.ViewModels
{
    public class EsperarOfertaViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<OfertaConductor> _listaOfertas;
        bool _visibleOferta;
        #endregion


        #region CONSTRUCTOR
        public EsperarOfertaViewModel(INavigation navigation)
        {
            Navigation = navigation;
            VisibleOferta = false;
            ListarOfertas();
            ActivarTimer();
        }
        #endregion


        #region OBJETOS
        public ObservableCollection<OfertaConductor> ListaOfertas
        {
            get { return _listaOfertas; }
            set { SetValue(ref _listaOfertas, value); }
        }
        public bool VisibleOferta
        {
            get { return _visibleOferta; }
            set { SetValue(ref _visibleOferta, value); }
        }
        #endregion


        #region PROCESOS
        private void ActivarTimer()
        {
            var tiempo = TimeSpan.FromSeconds(1);
            Device.StartTimer(tiempo, () =>
            {
                if (ListaOfertas.Count > 0)
                {
                    VisibleOferta = true;
                    foreach (var item in ListaOfertas)
                    {
                        var timeSpan = item.TimeSpan - TimeSpan.FromSeconds(1);
                        item.TimeSpan = timeSpan;
                        String[] cadena = timeSpan.ToString().Split(':');   //00:00:20 separa los valores de la hora en un array
                        var time = cadena[2];                               //obtiene los segundos
                        item.Progress = Convert.ToDouble(time) * 0.05;        //conbierte el valor de los segundos a rango de 0 a 1 para la lectura del pogressbar

                        //if (Convert.ToDouble(time) == 0)
                        //{
                        //    //EliminarOferta(item);
                        //}
                    }
                }
                else
                {
                    VisibleOferta = false;
                }
                return true;
            });
        }
        public async void ListarOfertas()
        {
            var funcion = new DataOfertasConductores();
            var parametros = new Pedido();
            parametros.IdUser = "Modelo";
            ListaOfertas = await funcion.ListaOfertas(parametros);
        }


        #endregion


        #region COMANDOS
        //public ICommand ListarOfertasCommand => new Command(ListarOfertas);
        #endregion
    }
}
