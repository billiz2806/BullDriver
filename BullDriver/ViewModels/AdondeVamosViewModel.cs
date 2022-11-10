using BullDriver.Models;
using BullDriver.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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
        bool _visibleListaDirecciones;
        bool _fijarMapa;
        Pin _punto = new Pin();
        Xamarin.Forms.GoogleMaps.Map _mapa;
        #endregion
        #region CONSTRUCTOR
        public AdondeVamosViewModel(INavigation navigation, Xamarin.Forms.GoogleMaps.Map mapa)
        {
            Navigation = navigation;
            this._mapa = mapa;
            _mapa.PropertyChanged += _mapa_PropertyChanged;
            EnableTxtOrigen = false;
            EnableTxtDestino = false;
            SelectDestino = false;
            SelectOrigen = false;
            VisibleListaDirecciones = false;
            FijarMapa = false;
            PinActual();
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
        public bool VisibleListaDirecciones
        {
            get { return _visibleListaDirecciones; }
            set { SetValue(ref _visibleListaDirecciones, value); }
        }
        public bool FijarMapa
        { 
            get { return _fijarMapa; }
            set { SetValue(ref _fijarMapa, value); }
        }
        #endregion
        #region PROCESOS
        private async void _mapa_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (FijarMapa == false)
            {
                return;
            }

            var m = (Xamarin.Forms.GoogleMaps.Map)sender;

            if (m.VisibleRegion != null)
            {
                if (SelectOrigen == true)
                {
                    ltOrigen = m.VisibleRegion.Center.Latitude;
                    lgOrigen = m.VisibleRegion.Center.Longitude;
                    TxtOrigen = await ObtenerDireccion(ltOrigen, lgOrigen);
                }
                if (SelectDestino == true)
                {
                    ltDestino = m.VisibleRegion.Center.Latitude;
                    lgDestino = m.VisibleRegion.Center.Longitude;
                    TxtDestino = await ObtenerDireccion(ltDestino, lgDestino);
                }
            }
        }
        private async Task<string>ObtenerDireccion(double lt, double lg)
        {
            try
            {
                Geocoder geoCoder = new Geocoder();
                Position position = new Position(lt, lg);
                IEnumerable<string> listaDirecciones = await geoCoder.GetAddressesForPositionAsync(position);
                string direccion = listaDirecciones.FirstOrDefault();
                return direccion;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
        private async Task BuscarDirecciones(string buscador)
        {
            VisibleListaDirecciones = true;
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
                VisibleListaDirecciones = false;
            }
        }
        private void seleccionarOrigen()
        {
            SelectOrigen = true;
            SelectDestino = false;
            EnableTxtOrigen = true;
            EnableTxtDestino = false;
            VisibleListaDirecciones = true;
            FijarMapa = false;
        }
        private void seleccionarDestino()
        {
            
            SelectDestino = true;
            SelectOrigen = false ;
            EnableTxtOrigen = false;
            EnableTxtDestino = true;
            VisibleListaDirecciones = true;
            FijarMapa = false;
        }
        private async Task PinActual()
        {
            _punto = new Pin
            {
                Label = "Tu ubicación actual",
                Type = PinType.Place,
                Icon = (Device.RuntimePlatform == Device.Android) ?
                BitmapDescriptorFactory.FromBundle("pin.png") :
                BitmapDescriptorFactory.FromView(new Image()
                {
                    Source = "pin.png",
                    WidthRequest = 64,
                    HeightRequest = 64,
                }),
                Position = new Position(0, 0)
            };
            _mapa.Pins.Add(_punto);
            await GeolocalizacionActual();
        }
        private async Task GeolocalizacionActual()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.High,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location != null)
                {
                    ltOrigen = location.Latitude;
                    lgOrigen = location.Longitude;
                    TxtOrigen = "Tu Ubicación";
                    var position = new Position(ltOrigen, lgOrigen);
                    _punto.Position = new Position(ltOrigen, lgOrigen);
                    _mapa.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(500)));
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void FijarEnMapa()
        {
            FijarMapa = true;
            VisibleListaDirecciones = false;
            EnableTxtOrigen = false;
            EnableTxtDestino = false;

        }

        #endregion
        #region COMANDOS
        public ICommand SelectDireccionCommand => new Command<GooglePlaceAutoCompletePrediction>(async(p)=> await SeleccionarDireccion(p));
        public ICommand BuscarDireccionesCommand => new Command<string>(async(b)=> await BuscarDirecciones(b));
        public ICommand SelectOrigenCommand => new Command(seleccionarOrigen);
        public ICommand SelectDestinoCommand => new Command(seleccionarDestino);
        public ICommand FijarEnMapaCommand => new Command(FijarEnMapa);

        #endregion
    }
}
