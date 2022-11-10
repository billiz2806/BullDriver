using BullDriver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using BullDriver.Views.Registro;
using BullDriver.Views.Reutilizables;
using BullDriver.Datos;
using Rg.Plugins.Popup.Services;

namespace BullDriver.ViewModels
{
    public class CompletarRegistroViewModel : BaseViewModel
    {
        #region VARIABLES
        string _txtNumero;
        List<Paises> _listaPais;
        Paises _selectPaisDefault;
        Paises _selectPais;
        public GoogleUser _googleUser { get; set; }
        #endregion
        #region CONSTRUCTOR
        public CompletarRegistroViewModel(INavigation navigation, GoogleUser googleUser)
        {
            Navigation = navigation;
            _googleUser = googleUser;
            ObtenerDataPorPais();
        }
        #endregion
        #region OBJETOS
        public string TxtNumero
        {
            get { return _txtNumero; }
            set { SetValue(ref _txtNumero, value); }
        }
        public List<Paises> ListaPais
        {
            get { return _listaPais; }
            set { SetValue(ref _listaPais, value); }
        }
        public Paises SelectPaisDefault
        {
            get { return _selectPaisDefault; }
            set { SetValue(ref _selectPaisDefault, value); }
        }
        public Paises SelectPais
        {
            get { return _selectPais; }
            set { SetValue(ref _selectPais, value); }
        }
        #endregion
        #region PROCESOS
        public async void EnviarSms()
        {
            try
            {
                // Encuentre el SID de su cuenta y el token de autenticación en twilio.com/console
                // y establecer las variables de entorno. 

                //string accountSid = Environment.GetEnvironmentVariable("ACb11d293d8bc355fd81a0bff9bae5bbbd");
                //string authToken = Environment.GetEnvironmentVariable("45b7a5d9e0f73b8c5a63b8d6255a69a0");

                //TwilioClient.Init(accountSid, authToken);

                //var message = MessageResource.Create(
                //    body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
                //    from: new Twilio.Types.PhoneNumber("+15017122661"),
                //    to: new Twilio.Types.PhoneNumber("+51926922691")
                //);

                //Console.WriteLine(message.Sid);
                #region GENERAR CODIGO ALEATORIO
                Random random = new Random();
                String randomSMS = random.Next(0, 9999).ToString("D4");
                #endregion

                var accountSid = "ACd501423b59eccbb6605e9dcb5b8c0ed1";
                var authToken = "e9ba02ee211c58fc84b8b00642d8b667";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber(SelectPaisDefault.CodigoPais + TxtNumero));
                messageOptions.MessagingServiceSid = "MGccc6cc9cc938b86ba0c356be085ff501";
                messageOptions.Body = "Usa " + randomSMS + " para validar tu cuenta en BullDriver";

                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);

                await Navigation.PushAsync(new DigitarCodigo(randomSMS));
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        public void MostrarPaises()
        {
            var funcion = new DataPaises();
            ListaPais = funcion.MostrarPaises();
        }
        public void ObtenerDataPorPais()
        {
            var funcion = new DataPaises();
            SelectPaisDefault = funcion.MostrarPaisesPorNombre("Peru");
            SelectPais = funcion.MostrarPaisesPorNombre("Peru");
        }
        private void MostrarListaPaises()
        {
            var popup = new ListaPaises();
            popup.BindingContext = this;
            MostrarPaises();
            PopupNavigation.Instance.PushAsync(popup);
        }
        private void SeleccionarPais(Paises entidad)
        {
            SelectPais = entidad;
        }
        private void ConfirmarPais()
        {
            SelectPaisDefault = SelectPais;
            PopupNavigation.Instance.PopAsync();
        }
        private void Cancelar()
        {
            PopupNavigation.Instance.PopAsync();
        }
        private void BuscarPais(string buscador)
        {
            buscador = PrimerLetraMayuscula(buscador);
            var funcion = new DataPaises();
            var lista = funcion.ListaBusquedaPaisesPorNombre(buscador);

            if (string.IsNullOrWhiteSpace(buscador))
            {
                ListaPais = new List<Paises>();
                MostrarPaises();
            }
            else
            {
                if(lista.Count > 0)
                {
                    ListaPais = new List<Paises>();
                    ListaPais = lista;
                }
            }
        }
        #endregion
        #region COMANDOS
        public ICommand BuscarPaisCommand => new Command<string>(BuscarPais);
        public ICommand CancelarCommand => new Command(Cancelar);
        public ICommand ConfirmarPaisCommand => new Command(ConfirmarPais);
        public ICommand SeleccionarPaisCommand => new Command<Paises>(SeleccionarPais);
        public ICommand MostrarListaPaisesCommand => new Command(MostrarListaPaises);
        public ICommand EnviarSMScommand => new Command(EnviarSms);
        #endregion
    }
}
