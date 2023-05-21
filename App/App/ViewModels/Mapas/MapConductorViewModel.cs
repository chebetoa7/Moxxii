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

namespace App.ViewModels.Mapas
{
    public partial class MapConductorViewModel : BaseViewModel
    {
        #region Vars
        public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
        public Command StartDayCommand { get; private set; }

        private CancellationTokenSource cts;
        private Xamarin.Forms.GoogleMaps.Map NewMap;
        #endregion

        #region Constructor
        [Obsolete]
        public MapConductorViewModel(Xamarin.Forms.GoogleMaps.Map _map)
        {
            NewMap = _map;
            _ = InitLocation();
            _ = IniciaRutaPopup();
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
        protected async System.Threading.Tasks.Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await System.Threading.Tasks.Task.Delay(milisec);
            actionToExecute();
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
        }
        #endregion

    }
}
