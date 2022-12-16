using BullDriver.Models;
using BullDriver.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BullDriver.Views.Configuraciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilUsuario : ContentPage
    {
        public PerfilUsuario(Usuario parametro)
        {
            InitializeComponent();
            BindingContext = new PerfilUsuarioViewModel(Navigation,parametro);
        }
    }
}