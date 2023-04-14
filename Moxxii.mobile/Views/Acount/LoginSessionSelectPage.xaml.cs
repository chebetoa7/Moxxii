using Moxxii.mobile.ViewModels.Acount;

namespace Moxxii.mobile.Views.Acount;

public partial class LoginSessionSelectPage : ContentPage
{
    #region Vars
    private readonly LoginSessionSelectViewModel _viewModel;
    #endregion

    #region Constructor
    public LoginSessionSelectPage()
    {
        InitializeComponent();

        BindingContext = _viewModel = new LoginSessionSelectViewModel();

    }
    #endregion
}