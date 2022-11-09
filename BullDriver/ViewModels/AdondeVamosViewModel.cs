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
        string _txtDestino;

        double ltOrigen = 0;
        double lgOrigen = 0;
        double ltDestino = 0;
        double lgDestino = 0;

        bool _selectOrigen;
        bool _selectDestino;

        bool _enableTxtOrigen;
        bool _enableTxtDestino;
        #endregion
        #region CONSTRUCTOR
        public AdondeVamosViewModel(INavigation navigation)
        {
            Navigation = navigation;
            EnableTxtOrigen = false;
            EnableTxtDestino = false;
            SelectDestino = false;
            SelectOrigen = false;
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
        public string TxtDestino
        {
            get { return _txtDestino; }
            set { SetValue(ref _txtDestino, value); }
        }

        public bool SelectOrigen
        {
            get { return _selectOrigen; }
            set { SetValue(ref _selectOrigen, value); }
        }
        public bool SelectDestino
        {
            get { return _selectDestino; }
            set { SetValue(ref _selectDestino, value); }
        }

        public bool EnableTxtOrigen
        {
            get { return _enableTxtOrigen; }
            set { SetValue(ref _enableTxtOrigen, value); }
        }
        public bool EnableTxtDestino
        {
            get { return _enableTxtDestino; }
            set { SetValue(ref _enableTxtDestino, value); }
        }
        #endregion
        #region PROCESOS
        private async Task BuscarDirecciones(string buscador)
        {
            var places = await _googleMapsApi.ApiPlaces(buscador);
            var placesResult = places.AutoCompletePlaces;
            if (placesResult != null && placesResult.Count > 0) 
            {
                ListaDirecciones = new List<GooglePlaceAutoCompletePrediction>(placesResult);
            }
        }
        private async Task SeleccionarDireccion(GooglePlaceAutoCompletePrediction parametro)
        {
            var coordenadas = await _googleMapsApi.ApiPlacesDetails(parametro.PlaceId);
            if (coordenadas != null)
            {
                if(_selectOrigen== true)
                {
                    ltOrigen = coordenadas.Latitude;
                    lgOrigen = coordenadas.Longitude;
                    TxtOrigen = coordenadas.Name;
                }
                if (_selectDestino == true)
                {
                    ltDestino = coordenadas.Latitude;
                    lgDestino = coordenadas.Longitude;
                    TxtDestino = coordenadas.Name;
                }
            }
        }

        private void seleccionarOrigen()
        {
            SelectOrigen = true;
            SelectDestino = false;
            EnableTxtOrigen = true;
            EnableTxtDestino = false;
        }
        private void seleccionarDestino()
        {
            
            SelectDestino = true;
            SelectOrigen = false ;
            EnableTxtOrigen = false;
            EnableTxtDestino = true;
        }
        #endregion
        #region COMANDOS
        public ICommand SelectDireccionCommand => new Command<GooglePlaceAutoCompletePrediction>(async(p)=> await SeleccionarDireccion(p));
        public ICommand BuscarDireccionesCommand => new Command<string>(async(b)=> await BuscarDirecciones(b));
        public ICommand SelectOrigenCommand => new Command(seleccionarOrigen);
        public ICommand SelectDestinoCommand => new Command(seleccionarDestino);

        #endregion
    }
}
