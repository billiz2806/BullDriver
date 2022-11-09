using BullDriver.Views.Registro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BullDriver.ViewModels
{
    public class MenuPrincipalViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        int _estado = 0;
        #endregion
        #region CONSTRUCTOR
        public MenuPrincipalViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ValidarAuth();
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

        public int Autenticacion()
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "auth.txt");
                _estado = Convert.ToInt32( File.ReadAllText(ruta));
                return _estado;
            }
            catch (Exception)
            {

                return _estado;
            }
        }

        public void ValidarAuth()
        {
            Autenticacion();
            if (_estado == 0)
            {
                Application.Current.MainPage = new NavigationPage(new Empezar());
            }
        }
        #endregion
        #region COMANDOS
        public ICommand ValidarAuthCommand => new Command(ValidarAuth);
        #endregion
    }
}
