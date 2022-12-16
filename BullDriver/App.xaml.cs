﻿using BullDriver.Views.Menu;
using BullDriver.Views.Navegacion;
using BullDriver.Views.Registro;
using BullDriver.Views.Reutilizables;
using BullDriver.Views.Configuraciones;
using Xamarin.Forms;

namespace BullDriver
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AdondeVamos());
            //MainPage = new NavigationPage(new PerfilUsuario());

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
