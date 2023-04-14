using Moxxii.mobile.ViewModels.Acount;

namespace Moxxii.mobile.Views.Acount;

public partial class LoginStartPage : ContentPage
{
    #region Vars
    List<StackLayout> layouts;
    private readonly LoginStartPageViewModel _viewModel;
    #endregion

    #region Constructor
    [Obsolete]
    public LoginStartPage()
    {
        InitializeComponent();
        InitComponentStackLayout();
        BindingContext = _viewModel = new LoginStartPageViewModel(layouts, ImgLogin);
    }
    #endregion

    #region Methods Init Interfaces

    /*Initial component changed in loginViewModel
    Componente incial que cambia in loginViewModel*/
    public void InitComponentStackLayout()
    {
        try
        {
            layouts = new List<StackLayout>();
            layouts.Add(stkPrecentation);
            layouts.Add(stkMain);
        }
        catch (Exception exM)
        {
            Console.WriteLine("Error InitComponentStackLayout " + exM.Message);
        }
    }
    #endregion
}