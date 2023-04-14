using Moxxii.mobile.ViewModels.Dashboard.UserMainPage;

namespace Moxxii.mobile.Views.Dashboard.UserMainPage;

public partial class DashboardUserMainPage : ContentPage
{
    #region Vars
    private readonly DashboardUserMainPageViewModel _viewModel;
    #endregion
    #region Constructor
    public DashboardUserMainPage()
    {
        InitializeComponent();

        BindingContext = _viewModel = new DashboardUserMainPageViewModel();
    }
    #endregion
}