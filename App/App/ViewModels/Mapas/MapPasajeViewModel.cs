using App.Models.Maps;
using CommunityToolkit.Mvvm.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ObservableCollection<Place> Places { get; } = new ObservableCollection<Place>();
         
        private CancellationTokenSource cts;
        //private IGeolocation geolocation;
        //private IGeocoding geocoding;
        private Xamarin.Forms.GoogleMaps.Map NewMap;

        #region Command
        public ICommand ShearLocationCommand { get; set; }
        #endregion

        [Obsolete]
        public MapPasajeViewModel(Xamarin.Forms.GoogleMaps.Map _map)
        {
            NewMap = _map;
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

                Polyline poliline = new Polyline
                {
                    StrokeWidth = 8,
                    StrokeColor = Color.FromHex("#1BA1E2"),
                };
                poliline.Positions.Clear();
                poliline.Positions.Add(new Position(16.554734, -94.947472));
                poliline.Positions.Add(new Position(16.554229, -94.947440));
                poliline.Positions.Add(new Position(16.554875, -94.944350));
                Xamarin.Forms.GoogleMaps.Position loc1 = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                MapSpan mapSpan = new MapSpan(loc1, 0.01, 0.01);
                var pins = new List<Pin>
                {
                    new Pin { Type = PinType.Place, Label = "This is my home",
                        Address = _address, 
                        Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude) }
                };
                var ma_pa = NewMap.FindByName<Xamarin.Forms.GoogleMaps.Map>("mapa_");
                ma_pa.MoveToRegion(mapSpan);
                ma_pa.Pins.Add(pins[0]);
                ma_pa.Polylines.Add(poliline);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Unable to get location
            }
           // Device.BeginInvokeOnMainThread(async () => await LoadingFalse());
        }

        
    }
}
