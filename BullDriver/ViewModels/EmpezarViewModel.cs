using BullDriver.Views.Registro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BullDriver.ViewModels
{
    public class EmpezarViewModel : BaseViewModel
    {

        #region CONSTRUCTOR
        public EmpezarViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region PROCESOS
        private async void IrCrearCuenta()
        {
            await Navigation.PushAsync(new CrearCuenta());
        }
        #endregion

        #region COMANDOS
        public ICommand IrCrearCuentaComand => new Command(IrCrearCuenta);
        #endregion
    }
}
