using BullDriver.ViewModels;
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
    public partial class Empezar : ContentPage
    {
        public Empezar()
        {
            InitializeComponent();
            BindingContext = new EmpezarViewModel(Navigation);
        }
    }
}