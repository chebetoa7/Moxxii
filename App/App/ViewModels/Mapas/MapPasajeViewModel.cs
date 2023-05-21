using App.Models.Body;
using App.Models.Maps;
using App.Response;
using App.Services;
using CommunityToolkit.Mvvm.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Position = Xamarin.Forms.GoogleMaps.Position;

namespace App.ViewModels.Mapas
{
    public partial class MapPasajeViewModel : BaseViewModel
    {
        #region Vars
        public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
         
        private CancellationTokenSource cts;
        //private IGeolocation geolocation;
        //private IGeocoding geocoding;
        private Xamarin.Forms.GoogleMaps.Map NewMap;
        ManagerServices managerServices;
        #endregion

        #region Command
        public ICommand ShearLocationCommand { get; set; }
        #endregion

        [Obsolete]
        public MapPasajeViewModel(Xamarin.Forms.GoogleMaps.Map _map)
        {
            NewMap = _map;
            managerServices = new Services.ManagerServices();
            _ = InitLocation();
        }

        [Obsolete]
        private async Task InitLocation()
        {
            
            try
            {
                await WaitAndExecute(100, () =>
                {
                    _ = GetCurrentLocationAsync();
                    
                });
            }
            finally
            {
                Console.WriteLine("\nError de StartActionAgain: ");
            }
        }

        

        protected async System.Threading.Tasks.Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await System.Threading.Tasks.Task.Delay(milisec);
            actionToExecute();
        }

        protected override void InitCommands()
        {
            //InitializeDataCommand = new Command(async () => await ExecuteInitializeDataCommandMapa());
        }

        [Obsolete]
        private Task ExecuteInitializeDataCommandMapa()
        {
            Device.BeginInvokeOnMainThread(async () => await LoadingTrue());
            return Task.Run(async () =>
            {
                try
                {
                    cts = new CancellationTokenSource();
                    var request = new GeolocationRequest(
                        GeolocationAccuracy.Medium,
                        TimeSpan.FromSeconds(10));

                    var request_location = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();

                    var locator = CrossGeolocator.Current;
                    var position = await Geolocation.GetLocationAsync(); 

                    //var _location = await Geolocation.GetLocationAsync(request, cts.Token);
                    var placemarks = await Geocoding.GetPlacemarksAsync(position);
                    var _address = placemarks?.FirstOrDefault()?.AdminArea;

                    Xamarin.Forms.GoogleMaps.Position loc1 = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                    /*Pin marker1 = new Pin()
                    {
                        Address = _address,
                        IsVisible = true,
                        Label = "Microsoft Hyderabad",
                        Position = loc1,
                        Type = PinType.Place

                    };*/

                    Places.Clear();
                    Places.Add(new Place()
                    {
                        location = position,
                        address = _address,
                        description = "Current Location"
                    });
                    MapSpan mapSpan = new MapSpan(loc1, 0.01, 0.01);
                    var ma_pa = NewMap.FindByName<Xamarin.Forms.GoogleMaps.Map>("mapa_");
                    ma_pa.MoveToRegion(mapSpan);
                    //ma_pa.MoveToRegion(MapSpan.FromCenterAndRadius(Places.First().location, Distance.FromMeters(1000)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Device.BeginInvokeOnMainThread(async () => await LoadingFalse());
            });

        }

        [Obsolete]
        private async Task GetCurrentLocationAsync()
        {
            
            try
            {
                Device.BeginInvokeOnMainThread(async () => await LoadingTrue());

                cts = new CancellationTokenSource();

                var request = new GeolocationRequest(
                    GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));

                var locator = CrossGeolocator.Current;
                var position = await Geolocation.GetLocationAsync();

                var _location = await Geolocation.GetLocationAsync(request, cts.Token);
                var placemarks = await Geocoding.GetPlacemarksAsync(_location);
                var _address = placemarks?.FirstOrDefault()?.AdminArea;
                /*
                Polyline poliline = new Polyline
                {
                    StrokeWidth = 8,
                    StrokeColor = Color.FromHex("#1BA1E2"),
                };
                poliline.Positions.Clear();
                poliline.Positions.Add(new Position(16.554734, -94.947472));
                poliline.Positions.Add(new Position(16.554229, -94.947440));
                poliline.Positions.Add(new Position(16.554875, -94.944350));
                */
                Xamarin.Forms.GoogleMaps.Position loc1 = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                MapSpan mapSpan = new MapSpan(loc1, 0.01, 0.01);
                Pin Usuario = new Pin()
                {
                    Label = "Ubicación actual",
                    Type = PinType.Place,
                    Address = _address,
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("usuariopasajero.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "usuariopasajero.png", WidthRequest = 23, HeightRequest = 23 }),
                    Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude)
                };
                var ma_pa = NewMap.FindByName<Xamarin.Forms.GoogleMaps.Map>("mapa_");
                ma_pa.MoveToRegion(mapSpan);
                ma_pa.Pins.Add(Usuario);

                var idPasajero_ = Preferences.Get("config_user_id", 0);
                var city_ = Preferences.Get("config_user_city", "");
                var distric_ = Preferences.Get("config_user_distric", "");
                //Enviar solicitud 
                var solicitud = new NuevoViaje
                {
                    idPasajero = idPasajero_,
                    idConductor = 0,//por asignar
                    latInitial = Usuario.Position.Latitude,
                    longInitial = Usuario.Position.Longitude,
                    city = city_,
                    dictric = distric_,
                    status = true,
                    confirmationStatus = "Asignado",
                    TypeUser = "tdm",
                    Disponibility = "libre"
                };
                
                var userfree = await GetSolicitudMotoAsync(solicitud);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Unable to get location
            }
            Device.BeginInvokeOnMainThread(async () => await LoadingFalse());
            Device.BeginInvokeOnMainThread(async () => await ShareMotoPopup());
        }
        Helper.Login.loginHelper helperL;
        
        private async Task<bool> GetSolicitudMotoAsync(NuevoViaje sol)
        {
            var user = new Usuario();
            var usuarioValido = new ResponseToken();
            //helperL = new Helper.Login.loginHelper();
            try 
            {
                var access_token_Config = Preferences.Get("config_user_tokenApi", "");
                var cityBD = DB.ConfigRepository.Instancia.GetConfigUser().FirstOrDefault();

                var usuarioEncontrado = await managerServices.SolicitudViaje(sol, access_token_Config);

                var name_ = Preferences.Get("config_user_name", "");
                var lastName_ = Preferences.Get("config_user_lastName", "");
                var pushFirebase = SendPushFirebaseAsync("Nuevo viaje", "Tienes una solicitud de viaje de " + name_ + " " + lastName_, usuarioEncontrado.tokenfirebase, Services.MoxxiiApi.AutoritationFCM, access_token_Config);
                //usuarioValido = await helperL.ConductorLibre(cityBD.city.ToString(), "libre" , access_token_Config);
                return true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message); 
            }
            return false;
        }
        private async Task<bool> SendPushFirebaseAsync(string mensaje, string titulo, string tokenDivice, string tikenFirebase, string tokenAccess)
        {
            try 
            {
                //var access_token_Config = Preferences.Get("config_user_tokenApi", "");
                var cityBD = DB.ConfigRepository.Instancia.GetConfigUser().FirstOrDefault();
                var lodata = new FCMBody
                {
                    titulo = titulo,
                    mensaje = mensaje,
                    tokenDivice = tokenDivice,
                    tokenFirebase = tikenFirebase

                };
                var viaje = await managerServices.SendPushFirebase(lodata, tokenAccess);
                //usuarioValido = await helperL.ConductorLibre(cityBD.city.ToString(), "libre" , access_token_Config);
                return viaje;
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message); 
            }
            return false;
        }


    }
}
