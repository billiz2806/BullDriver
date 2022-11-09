using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using BullDriver.Views.Menu;
using System.IO;

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
                CrearArchivo();
                await Navigation.PushAsync(new MenuPrincipal());
            }
            else
            {
                await DisplayAlert("Alert", "Código Incorrecto", "OK");
            }
        }
        public void CrearArchivo()
        {
            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "auth.txt");
            StreamWriter sm;
            string estado = "1";
            try
            {
                if (File.Exists(ruta)==false)
                {
                    sm = File.CreateText(ruta);
                    sm.WriteLine(estado);
                    sm.Flush();
                    sm.Close();
                }
                else
                {
                    File.Delete(ruta);
                    sm = File.CreateText(ruta);
                    sm.WriteLine(estado);
                    sm.Flush();
                    sm.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region COMANDOS
        public ICommand ValidadCodigoCommand => new Command(ValidadCodigo);
        #endregion
    }
}
