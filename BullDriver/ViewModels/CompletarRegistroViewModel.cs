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

namespace BullDriver.ViewModels
{
    public class CompletarRegistroViewModel : BaseViewModel
    {
        #region VARIABLES
        string _txtNumero;
        public GoogleUser _googleUser { get; set; }
        #endregion
        #region CONSTRUCTOR
        public CompletarRegistroViewModel(INavigation navigation, GoogleUser googleUser)
        {
            Navigation = navigation;
            _googleUser = googleUser;
        }
        #endregion
        #region OBJETOS
        public string TxtNumero
        {
            get { return _txtNumero; }
            set { SetValue(ref _txtNumero, value); }
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
                    new PhoneNumber(TxtNumero));
                messageOptions.MessagingServiceSid = "MGccc6cc9cc938b86ba0c356be085ff501";
                messageOptions.Body = "Usa" + randomSMS + " para validar tu cuenta en BullDriver";

                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);

                await Navigation.PushAsync(new DigitarCodigo(randomSMS));
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        #endregion
        #region COMANDOS
        public ICommand EnviarSMScommand => new Command(EnviarSms);
        #endregion
    }
}
