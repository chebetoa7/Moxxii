using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Moxxii.mobile.ViewModels.Mapa;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Moxxii.mobile.Views.map.mapUser;

public partial class MapUserPage : ContentPage
{
    #region Vars
    private readonly MapViewModel _viewModel;
    #endregion
    public MapUserPage()
	{
        InitializeComponent();
        BindingContext = _viewModel = new MapViewModel(mapa_);
       
    }

    
}