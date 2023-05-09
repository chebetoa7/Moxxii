using Moxxii.mobile.ViewModels.Acount;

namespace Moxxii.mobile.Views.Acount;

public partial class LoginConductorSelectPage : ContentPage
{
    #region Vars
    private readonly LoginConductorSelectViewModel _viewModel;
    List<StackLayout> layouts;
    #endregion
    #region Constructor
    public LoginConductorSelectPage()
	{
		InitializeComponent();
        InitComponentStackLayout();
        BindingContext = _viewModel = new LoginConductorSelectViewModel(layouts);
    }
    #endregion

    #region Methods Init Interfaces

    public void InitComponentStackLayout()
    {
        try
        {
            layouts = new List<StackLayout>();
            layouts.Add(stkLoading);
        }
        catch (Exception exM)
        {
            Console.WriteLine("Error InitComponentStackLayout " + exM.Message);
        }
    }
    #endregion
}