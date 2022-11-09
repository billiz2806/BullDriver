using BullDriver.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BullDriver.Views.Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdondeVamos : ContentPage
    {
        public AdondeVamos()
        {
            InitializeComponent();
            BindingContext = new AdondeVamosViewModel(Navigation);
        }
    }
}