using BullDriver.Views.Menu;
using BullDriver.Views.Registro;
using BullDriver.Views.Reutilizables;
using Xamarin.Forms;

namespace BullDriver
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new ListaPaises());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
