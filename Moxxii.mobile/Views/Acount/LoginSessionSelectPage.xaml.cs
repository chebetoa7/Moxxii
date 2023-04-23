using Moxxii.mobile.ViewModels.Acount;
using System.Collections.Generic;

namespace Moxxii.mobile.Views.Acount;

public partial class LoginSessionSelectPage : ContentPage
{
    #region Vars
    private readonly LoginSessionSelectViewModel _viewModel;
    List<StackLayout> layouts;
    #endregion

    #region Constructor
    public LoginSessionSelectPage()
    {
        InitializeComponent();
        InitComponentStackLayout();
        BindingContext = _viewModel = new LoginSessionSelectViewModel(layouts);

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