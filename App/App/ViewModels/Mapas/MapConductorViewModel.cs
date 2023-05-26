using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using App.Models.Maps;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using System.Linq;
using Position = Xamarin.Forms.GoogleMaps.Position;
using System.Net.NetworkInformation;
using Rg.Plugins.Popup.Services;
using App.Services;
using static Xamarin.Essentials.Permissions;

namespace App.ViewModels.Mapas
{
    public partial class MapConductorViewModel : BaseViewModel
    {
        #region Vars
        public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
        public Command StartDayCommand { get; private set; }

        private CancellationTokenSource cts;
        private Xamarin.Forms.GoogleMaps.Map NewMap;
        ManagerServices managerServices;
        #endregion

        #region Constructor
        [Obsolete]
        public MapConductorViewModel(Xamarin.Forms.GoogleMaps.Map _map)
        {
            NewMap = _map;
            managerServices = new Services.ManagerServices();
            _ = InitLocation();


            //VereficarExistenciaViajes(5000);
        }

        [Obsolete]
        private async Task VereficarExistenciaViajes()
        {
            try 
            {
                
                    await verificarMin();
                
            } catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        #endregion

        #region Commands
        protected override void InitCommands()
        {
            //InitializeDataCommand = new Command(async () => await ExecuteInitializeDataCommandMapa());
            StartDayCommand = new Command(async () => await btnStarDayCommands());

        }


        #endregion

        #region Methods General
        private Task btnStarDayCommands()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError de btnStarDayCommands: ");
            }

            return null;
        }

        #endregion

        #region MethodsLocation
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
                Console.WriteLine("\nError de InitLocation ");
            }
        }


        [Obsolete]
        //Verificar que no exista viaje en caso de ser conductor
        //Verificar que el conductor acepte el viaje en caso de ser pasaje
        //countMin son los minutos que le envias o lapso de tiempo para volver a realizar la petición
        private async Task verificarMin()
        {
            try
            {
                
                    await WaitAndExecute(120000, () =>
                    {
                        _ = VerificarSiAceptoRuta();
                        _ = VereficarExistenciaViajes();

                    });
                
                
            }
            finally
            {
                Console.WriteLine("\nError de InitLocation ");
            }
        }
        protected async System.Threading.Tasks.Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await System.Threading.Tasks.Task.Delay(milisec);
            actionToExecute();
        }

        [Obsolete]
        private async Task GetCurrentLocationAsync()//Localiza al usuario en el mapa
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

                /*Polyline poliline = new Polyline
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
                
                Pin Moto = new Pin()
                {
                    Label = "Moxxii",
                    Type = PinType.Place,
                    Address = _address,
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("motocircule.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "motocircule.png", WidthRequest = 23, HeightRequest = 23 }),
                    Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude)
                };
                var ma_pa = NewMap.FindByName<Xamarin.Forms.GoogleMaps.Map>("mapa_");
                ma_pa.MoveToRegion(mapSpan);
                ma_pa.Pins.Add(Moto);
                //ma_pa.Polylines.Add(poliline);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Device.BeginInvokeOnMainThread(async () => await LoadingFalse());

            await verificarInicioRutaYPushViaje();//Verificar si exite un nuevo viaje y mostrar modal
            await VereficarExistenciaViajes();


        }

        private async Task verificarInicioRutaYPushViaje()
        {
            try 
            {
                var confUp = DB.ConfigRepository.Instancia.GetConfigUser().FirstOrDefault();
                if (confUp.disponibility == "libre")//Si ya inicio dia
                {
                    if (App.ActionPush_)//Si existe push 
                    {
                        //await DisplayAlertMessage("solicitud", "Viaje nuevo", "OK");
                        if (vanSolicitudViaje == false)
                            Device.BeginInvokeOnMainThread(async () => await SolicitudStartPopup());
                    }
                    else 
                    {
                        /*En caso que no encuentre push al entrar espera 1.5 minutos
                         * para verificar si la push no se envio y ver en el sistema
                         * y comprovar que no tenga alguna solicitu de viaje
                        */
                        if (vanSolicitudViaje == false) //vanSolicitudViaje en false sig. no a existe viaje volver a verificar
                        {
                            await WaitAndExecute(1600, () =>
                            {
                                _ = VerificarSiAceptoRuta();

                            });
                        }
                        
                        
                    }
                }
                else//En caso de no iniciar dia mostrar modal para iniciar su día
                {
                    _ = IniciaRutaPopup();
                }
            } catch (Exception ex) 
            {
                Console.WriteLine(ex.Message); 
            }
        }
        bool vanAceptado = false;//Viaje aceptado o no cuando eres pasajero
        bool vanSolicitudViaje = false;//Viaje para ver si tienes solicitud de viaje conductor
        private async Task VerificarSiAceptoRuta()
        {
            try 
            {
                var idP = Preferences.Get("config_user_id", 0);

                if (vanSolicitudViaje == false)
                {
                    var verificarViaje_ = await managerServices.EscanTravel(idP);
                    if (!verificarViaje_)
                    {

                        vanSolicitudViaje = false;
                        //no existe solicitu a conductor - vanAceptado = false
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => await SolicitudStartPopup());
                        vanSolicitudViaje = true;
                        //Tienes un viaje para aceptar y notificar por push y por api
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

    }
}
