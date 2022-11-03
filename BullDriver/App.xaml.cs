﻿using BullDriver.Views.Registro;
using Xamarin.Forms;

namespace BullDriver
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new CompletarRegistro();
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
