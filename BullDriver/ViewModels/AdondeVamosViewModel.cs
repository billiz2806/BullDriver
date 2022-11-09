using BullDriver.Models;
using BullDriver.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BullDriver.ViewModels
{
    public class AdondeVamosViewModel : BaseViewModel
    {
        #region VARIABLES
        List<GooglePlaceAutoCompletePrediction> _listaDirecciones;
        private readonly IGoogleMapsApiService _googleMapsApi = new GoogleMapsApiService();

        string _txtOrigen;
        #endregion
        #region CONSTRUCTOR
        public AdondeVamosViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
        public List<GooglePlaceAutoCompletePrediction> ListaDirecciones
        {
            get { return _listaDirecciones; }
            set { SetValue(ref _listaDirecciones, value); }
        }
        public string TxtOrigen
        {
            get { return _txtOrigen; }
            set { SetValue(ref _txtOrigen, value); }
        }
        #endregion
        #region PROCESOS
        private async Task BuscarDirecciones()
        {
            var places = await _googleMapsApi.ApiPlaces(TxtOrigen);
            var placesResult = places.AutoCompletePlaces;
            if (placesResult != null && placesResult.Count > 0)
            {
                ListaDirecciones = new List<GooglePlaceAutoCompletePrediction>(placesResult);
            }
        }

        #endregion
        #region COMANDOS
        public ICommand BuscarDireccionesCommand => new Command(async()=> await BuscarDirecciones());
        #endregion
    }
}
