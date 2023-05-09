using Moxxii.mobile.ViewModels.Mapa;

namespace Moxxii.mobile.Views.Mapa.mapconductor;

public partial class MapConductorPage : ContentPage
{
    #region Vars
    private readonly MapConductorViewModel _viewModel;
    #endregion
    #region Constructor
    public MapConductorPage()
	{
		InitializeComponent();
        BindingContext = _viewModel = new MapConductorViewModel(mapa_);
    }
    #endregion

}