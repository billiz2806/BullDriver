﻿using BullDriver.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BullDriver.Views.Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DigitarCodigo : ContentPage
    {
        public DigitarCodigo(String codigo, String telefono)
        {
            InitializeComponent();
            BindingContext = new DigitarCodigoViewModel(Navigation, codigo, telefono);
        }
    }
}