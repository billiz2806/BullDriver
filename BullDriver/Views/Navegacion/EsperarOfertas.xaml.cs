using BullDriver.Models;
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
    public partial class EsperarOfertas : ContentPage
    {
        public EsperarOfertas(Pedido parametros)
        {
            InitializeComponent();
        }
    }
}