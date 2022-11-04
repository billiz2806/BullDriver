using BullDriver.Models;
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
    public partial class CompletarRegistro : ContentPage
    {
        public CompletarRegistro(GoogleUser userParameter)
        {
            InitializeComponent();
        }
    }
}