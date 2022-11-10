using BullDriver.Models;
using BullDriver.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BullDriver.Views.Reutilizables
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPaises : PopupPage
    {
        public ListaPaises()
        {
            InitializeComponent();
        }
    }
}