using BullDriver.Datos;
using BullDriver.Models;
using BullDriver.Services;
using BullDriver.Views.Navegacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<OfertaConductor> _listaOfertas;
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
        bool _visibleTxtOrigen;
        bool _visibleTxtDestino;
        bool _visibleListaDirecciones;
        bool _fijarMapa;
        bool _visibleOfertar;
        string _txtTarifa;
        string _txtTarifaEmergente;
        Pin _punto = new Pin();
        Xamarin.Forms.GoogleMaps.Map _mapa;
        bool _visibleOferta;
        bool _visibleEsperarOferta;
        Pedido _modelPedido;
        bool _visibleNavegar;
        public GoogleMatrix ParametrosMatrix { get; set; }


        #endregion
        #region CONSTRUCTOR
        public AdondeVamosViewModel(INavigation navigation, Xamarin.Forms.GoogleMaps.Map mapa)
        {
            Navigation = navigation;
            this._mapa = mapa;
            _mapa.PropertyChanged += _mapa_PropertyChanged;
            VisibleTxtOrigen = true;
            VisibleTxtDestino = true;
            EnableTxtOrigen = false;
            EnableTxtDestino = false;
            SelectDestino = false;
            SelectOrigen = false;
            VisibleListaDirecciones = false;
            FijarMapa = false;
            VisibleOfertar = false;
            TxtTarifa = "Ofresca su tarifa";
            VisibleOferta = false;
            VisibleEsperarOferta = false;
            VisibleNavegar = false;


            Task.Run(PinActual);
            ListarOfertas();
            ActivarTimer();
        }
        #endregion
        #region OBJETOS
        public bool VisibleNavegar
        {
            get { return _visibleNavegar; }
            set { SetValue(ref _visibleNavegar, value); }
        }
        public bool VisibleEsperarOferta
        {
            get { return _visibleEsperarOferta; }
            set { SetValue(ref _visibleEsperarOferta, value); }
        }
        public bool VisibleOferta
        {
            get { return _visibleOferta; }
            set { SetValue(ref _visibleOferta, value); }
        }
        public ObservableCollection<OfertaConductor> ListaOfertas
        {
            get { return _listaOfertas; }
            set { SetValue(ref _listaOfertas, value); }
        }
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

        public bool VisibleTxtOrigen
        {
            get { return _visibleTxtOrigen; }
            set { SetValue(ref _visibleTxtOrigen, value); }
        }
        public bool VisibleTxtDestino
        {
            get { return _visibleTxtDestino; }
            set { SetValue(ref _visibleTxtDestino, value); }
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

        public bool VisibleOfertar
        {
            get { return _visibleOfertar; }
            set { SetValue(ref _visibleOfertar, value); }
        }

        public string TxtTarifa
        {
            get { return _txtTarifa; }
            set { SetValue(ref _txtTarifa, value); }
        }
        public string TxtTarifaEmergente
        {
            get { return _txtTarifaEmergente; }
            set { SetValue(ref _txtTarifaEmergente, value); }
        }
        #endregion


        #region PROCESOS
        private void AgregarTarifa()
        {
            TxtTarifa = TxtTarifaEmergente;
            OcultarOfertar();
        }
        private void MostrarOfertar()
        {
            VisibleOfertar = true;
        }
        private void OcultarOfertar()
        {
            VisibleOfertar = false;
        }
        private async void _mapa_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (FijarMapa == false)
            {
                return;
            }

            var m = (Xamarin.Forms.GoogleMaps.Map)sender;
            VisibleListaDirecciones = false;
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
        private async Task<string> ObtenerDireccion(double lt, double lg)
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
                if (_selectOrigen == true)
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
            VisibleTxtOrigen = false;
            VisibleTxtDestino = true;
            EnableTxtDestino = false;
            VisibleListaDirecciones = true;
            FijarMapa = false;
        }
        private void seleccionarDestino()
        {

            SelectDestino = true;
            SelectOrigen = false;
            EnableTxtOrigen = false;
            EnableTxtDestino = true;
            VisibleTxtOrigen = true;
            VisibleTxtDestino = false;
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
            VisibleTxtOrigen = true;
            VisibleTxtDestino = true;

        }
        private async void InsertarPedido()
        {
            if (!string.IsNullOrWhiteSpace(TxtOrigen))
            {
                if (!string.IsNullOrWhiteSpace(TxtDestino))
                {
                    if (!string.IsNullOrWhiteSpace(TxtTarifa) && TxtTarifa != "Ofresca su tarifa")
                    {
                        var funcion = new DataPedidos();
                        var parametros = new Pedido();
                        var coorOrigen = ltOrigen.ToString().Replace(",", ".") + "," + lgOrigen.ToString().Replace(",", ".");
                        var coorDestino = ltDestino.ToString().Replace(",", ".") + "," + lgDestino.ToString().Replace(",", ".");
                        ParametrosMatrix = await _googleMapsApi.CalcularDistanciaTiempo(coorOrigen, coorDestino);
                        parametros.Origen_lugar = TxtOrigen;
                        parametros.Destino_lugar = TxtDestino;
                        parametros.IdChofer = "Modelo";
                        parametros.IdUser = "Modelo";
                        parametros.Lt_lg_origen = coorOrigen;
                        parametros.Lt_lg_destino = coorDestino;
                        parametros.Estado = "PENDIENTE";
                        parametros.Tiempo = ParametrosMatrix.Rows[0].Elements[0].Duration.Text;
                        parametros.Tarifa = TxtTarifa;
                        parametros.Distancia = ParametrosMatrix.Rows[0].Elements[0].Distance.Value.ToString();

                        await funcion.InsertPedidos(parametros);
                        VisibleOfertar = true;
                    }
                    else
                    {
                        await DisplayAlert("Faltan Datos", "Ingrese una tarifa", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Faltan Datos", "Seleccione un destino", "OK");
                }
            }
            else
            {
              await  DisplayAlert("Faltan Datos", "Seleccione un origen", "OK");
            }





        }
        public async void ListarOfertas()
        {
            var funcion = new DataOfertasConductores();
            var parametros = new Pedido();
            parametros.IdUser = "Modelo";
            ListaOfertas = await funcion.ListaOfertas(parametros);
        }
        private void ActivarTimer()
        {
            var tiempo = TimeSpan.FromSeconds(1);
            Device.StartTimer(tiempo, () =>
            {
                if(ListaOfertas != null)
                {
                    if (ListaOfertas.Count > 0)
                    {
                        VisibleOferta = true;
                        VisibleEsperarOferta = true;
                        foreach (var item in ListaOfertas)
                        {
                            var timeSpan = item.TimeSpan - TimeSpan.FromSeconds(1);
                            item.TimeSpan = timeSpan;
                            String[] cadena = timeSpan.ToString().Split(':');   //00:00:20 separa los valores de la hora en un array
                            var time = cadena[2];                               //obtiene los segundos
                            item.Progress = Convert.ToDouble(time) * 0.05;        //conbierte el valor de los segundos a rango de 0 a 1 para la lectura del pogressbar

                        }
                    }
                    else
                    {
                        VisibleOferta = false;
                        VisibleEsperarOferta = false;
                    }
                } 
                return true;
            });
        }
        private async void ConfirmarPedido(OfertaConductor parametros)
        {
            var funcion = new DataPedidos();
            _modelPedido = new Pedido();
            _modelPedido.IdUser = "Modelo";
            _modelPedido.Tarifa = parametros.Tarifa;
            _modelPedido.IdChofer = parametros.IdConductor;
            await funcion.ConfirmarPedido(_modelPedido);
            VisibleNavegar = true;
            VisibleEsperarOferta = false;
        }

        #endregion
        #region COMANDOS
        public ICommand ConfirmarPedidoCommand => new Command<OfertaConductor>(ConfirmarPedido);
        public ICommand SelectDireccionCommand => new Command<GooglePlaceAutoCompletePrediction>(async (p) => await SeleccionarDireccion(p));
        public ICommand BuscarDireccionesCommand => new Command<string>(async (b) => await BuscarDirecciones(b));
        public ICommand SelectOrigenCommand => new Command(seleccionarOrigen);
        public ICommand SelectDestinoCommand => new Command(seleccionarDestino);
        public ICommand FijarEnMapaCommand => new Command(FijarEnMapa);
        public ICommand MostrarOfertarCommand => new Command(MostrarOfertar);
        public ICommand OcultarOfertarCommand => new Command(OcultarOfertar);
        public ICommand AgregarTarifaCommand => new Command(AgregarTarifa);
        public ICommand InsertarPedidoCommand => new Command(InsertarPedido);


        #endregion
    }
}
