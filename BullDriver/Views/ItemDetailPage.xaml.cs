using BullDriver.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BullDriver.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}