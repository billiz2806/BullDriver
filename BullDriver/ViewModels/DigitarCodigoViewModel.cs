using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using BullDriver.Views.Menu;
using System.IO;
using BullDriver.Views.Navegacion;

namespace BullDriver.ViewModels
{
    public class DigitarCodigoViewModel :BaseViewModel
    {
        #region VARIABLES
        string _txtCodigo;
        string codigoRecibido;
        string _telefono;
        #endregion
        #region CONSTRUCTOR
        public DigitarCodigoViewModel(INavigation navigation, string codigo, string telefono)
        {
            Navigation = navigation;
            codigoRecibido = codigo;
            Telefono = telefono;
        }
        #endregion
        #region OBJETOS
        public string Telefono
        {
            get { return _telefono; }
            set { SetValue(ref _telefono, value); }
        }
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
                await Navigation.PushAsync(new AdondeVamos());
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
        private async void Volver()
        {
            await Navigation.PopAsync();
        }
        #endregion
        #region COMANDOS
        public ICommand VolverCommand => new Command(Volver);

        public ICommand ValidadCodigoCommand => new Command(ValidadCodigo);
        #endregion
    }
}
