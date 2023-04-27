using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Moxxii.mobile.ViewModels.Mapa;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Moxxii.mobile.Views.map.mapUser;

public partial class MapUserPage : ContentPage
{
    //private CancellationTokenSource _cancelTokenSource;
    //private bool _isCheckingLocation;
    //public Map map;//= new Map();
    //private ActivityIndicator espera;
    //private GeolocationRequest request;

    //[Obsolete]

    #region Vars
    private readonly MapViewModel _viewModel;
    #endregion
    public MapUserPage()
	{
        InitializeComponent();
        BindingContext = _viewModel = new MapViewModel(mapa_);
        //Localitation();

        

    }

    /*
    private async Task ShowMessage ()
    {
       await DisplayAlert("Mensaje","Requiere permisos de localización","Ok");
       
    }

    [Obsolete]
    private Task ShowLoading()
    {
        Device.BeginInvokeOnMainThread(async () => await GetCurrentLocation());
        
        return Task.CompletedTask;
    }

    protected async System.Threading.Tasks.Task WaitAndExecute(int milisec, Action actionToExecute)
    {
        await System.Threading.Tasks.Task.Delay(milisec);
        actionToExecute();
    }

    

    [Obsolete]
    public async Task GetLocationAsync()
    {
        PermissionStatus status_ = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status_ != PermissionStatus.Granted)
        {
            // Notify user permission was denied
            await ShowMessage();
            status_ = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            await GetLocationAsync();
            return;
        }
        else
        {
           await WaitAndExecute(10800, () =>
           {
               _ = ShowLoading();
           });
            
        }
        stkLoading.IsVisible = true;
        stkMapa.IsVisible = false;
        return;
    }
    
    public async Task GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;
            await WaitAndExecute(10800, () =>
            {
                request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(18));
            });
            
            _cancelTokenSource = new CancellationTokenSource();
            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
            
            if (location != null)
            {
                _ = stkMapa.Remove(espera);
                Circle circle = new Circle
                {
                    Center = new Location(location.Latitude, location.Longitude),
                    Radius = new Distance(350),
                    StrokeColor = Color.FromArgb("#88FF0000"),
                    StrokeWidth = 8,
                    FillColor = Color.FromArgb("#88FFC0CB")
                };
                Pin pin = new Pin
                {
                    Label = "JA DLCruz",
                    Address = "La ventosa Oaxaca",
                    Type = PinType.Place,
                    Location = new Location(location.Latitude, location.Longitude)
                };
                await WaitAndExecute(10800, () =>
                {

                    MapSpan mapSpan = new MapSpan(pin.Location, 0.01, 0.01);
                    map = new Map(mapSpan);

                    map.MapElements.Add(circle);
                    map.Pins.Add(pin);

                    stkMapa.Add(map);
                });
            }
        }
        catch (Exception ex)
        {
            // Unable to get location
        }
        finally
        {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }*/
}