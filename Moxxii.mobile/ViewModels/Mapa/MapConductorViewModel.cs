using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Maps;
using Moxxii.mobile.Controls.PopPup;
using Moxxii.mobile.Models.Map;
using System.Collections.ObjectModel;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Moxxii.mobile.ViewModels.Mapa
{
    public partial class MapConductorViewModel : BaseViewModel
    {
        #region Vars
        public ObservableCollection<Place> Places { get; } = new();
        public Command StartDayCommand { get; private set; }

        private CancellationTokenSource cts;
        private Map NewMap;
        #endregion

        #region Constructor
        public MapConductorViewModel(Map _map)
        {
            NewMap = _map;
            _ = InitLocation();
        }
        #endregion

        #region Commands
        protected override void InitCommands()
        {
            InitializeDataCommand = new Command(async () => await ExecuteInitializeDataCommandMapa());
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
        #endregion

    }
}
