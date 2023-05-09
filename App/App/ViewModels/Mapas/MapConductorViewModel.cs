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
        public MapConductorViewModel(Xamarin.Forms.GoogleMaps.Map _map)
        {
            NewMap = _map;
            _ = InitLocation();
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

                    var locator = CrossGeolocator.Current;
                    var position = await Geolocation.GetLocationAsync();

                    var _location = await Geolocation.GetLocationAsync(request, cts.Token);
                    var placemarks = await Geocoding.GetPlacemarksAsync(_location);
                    var _address = placemarks?.FirstOrDefault()?.AdminArea;

                    Places.Clear();
                    Places.Add(new Place()
                    {
                        location = _location,
                        address = _address,
                        description = "Current Location"
                    });

                    Xamarin.Forms.GoogleMaps.Position loc1 = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);

                    MapSpan mapSpan = new MapSpan(loc1, 0.01, 0.01);
                    var ma_pa = NewMap.FindByName<Xamarin.Forms.GoogleMaps.Map>("mapa_");
                    ma_pa.MoveToRegion(mapSpan);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                IniciaRutaPopup();
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

                Places.Clear();
                Places.Add(new Place()
                {
                    location = _location,
                    address = _address,
                    description = "Ubicación actual"
                });

                Xamarin.Forms.GoogleMaps.Position loc1 = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                MapSpan mapSpan = new MapSpan(loc1, 0.01, 0.01);
                var ma_pa = NewMap.FindByName<Xamarin.Forms.GoogleMaps.Map>("mapa_");
                ma_pa.MoveToRegion(mapSpan);


            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            Device.BeginInvokeOnMainThread(async () => await LoadingFalse());
        }


        /*[RelayCommand]
        private void DisposeCancellationToken()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
        }*/
        #endregion

    }
}
