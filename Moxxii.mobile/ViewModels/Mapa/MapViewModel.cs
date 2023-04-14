using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Moxxii.mobile.Controls.PopPup;
using Moxxii.mobile.Models.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Moxxii.mobile.ViewModels.Mapa
{
    public partial class MapViewModel : BaseViewModel
    {
        public ObservableCollection<Place> Places { get; } = new();

        private CancellationTokenSource cts;
        private IGeolocation geolocation;
        private IGeocoding geocoding;
        private Map NewMap;

        #region Command
        public ICommand ShearLocationCommand { get; set; }
        #endregion

        [Obsolete]
        public MapViewModel(Map _map)
        {
            //ShearLocationCommand = new Command(async () => await GetCurrentLocationAsync());
            NewMap = _map;

            _ = InitLocation();//Inicia la localización actual
            // InitCommands();
        }
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
                Console.WriteLine("\nError de StartActionAgainPrecentation: ");
            }
        }
        protected async System.Threading.Tasks.Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await System.Threading.Tasks.Task.Delay(milisec);
            actionToExecute();
        }

        protected override void InitCommands()
        {
            InitializeDataCommand = new Command(async () => await ExecuteInitializeDataCommandMapa());

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
                    MapSpan mapSpan = new MapSpan(_location, 0.01, 0.01);
                    var ma_pa = NewMap.FindByName<Map>("mapa_");
                    ma_pa.MoveToRegion(mapSpan);
                } catch (Exception ex)
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
                MapSpan mapSpan = new MapSpan(_location, 0.01, 0.01);
                var ma_pa = NewMap.FindByName<Map>("mapa_");
                ma_pa.MoveToRegion(mapSpan);
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            Device.BeginInvokeOnMainThread(async () => await LoadingFalse());
        }

        [RelayCommand]
        private void DisposeCancellationToken()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
        }
    }
}
