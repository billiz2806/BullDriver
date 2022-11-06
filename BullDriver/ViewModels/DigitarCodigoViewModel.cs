using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using BullDriver.Views.Menu;

namespace BullDriver.ViewModels
{
    public class DigitarCodigoViewModel :BaseViewModel
    {
        #region VARIABLES
        string _txtCodigo;
        string codigoRecibido;
        #endregion
        #region CONSTRUCTOR
        public DigitarCodigoViewModel(INavigation navigation, string codigo)
        {
            Navigation = navigation;
            codigoRecibido = codigo;
        }
        #endregion
        #region OBJETOS
        public string TxtCodigo
        {
            get { return _txtCodigo; }
            set { SetValue(ref _txtCodigo, value); }
        }
        #endregion
        #region PROCESOS
        public async void ValidadCodigo()
        {
            if (TxtCodigo == codigoRecibido)
            {
                await Navigation.PushAsync(new MenuPrincipal());
            }
            else
            {
                await DisplayAlert("Alert", "Código Incorrecto", "OK");
            }
        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand ValidadCodigoCommand => new Command(ValidadCodigo);
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
