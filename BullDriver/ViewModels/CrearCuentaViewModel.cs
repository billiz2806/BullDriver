using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BullDriver.Models;
using BullDriver.Views.Registro;

namespace BullDriver.ViewModels
{
    public class CrearCuentaViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        public readonly IGoogleManager _googleManager;
        GoogleUser googleUser = new GoogleUser();
        #endregion

        #region CONSTRUCTOR
        public CrearCuentaViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _googleManager = DependencyService.Get<IGoogleManager>();
        }
        #endregion

        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion

        #region PROCESOS
        
        public void LoginConGmail()
        {
            _googleManager.Login(LoginCompletado);
        }
        public async void LoginCompletado(GoogleUser user, string message)
        {
            if (user != null)
            {
                googleUser = user;
                string[] cadena = googleUser.Nombre.Split(' ');

                googleUser.Nombre = cadena[0];
                googleUser.Apellido = cadena[1];

                await Navigation.PushAsync(new CompletarRegistro(googleUser));
            }
            else
            {
                await DisplayAlert("Mensaje", message, "OK");
            }
        }
        #endregion

        #region COMANDOS
        public ICommand LoginGmailCommand => new Command(LoginConGmail);
        #endregion
    }
}
