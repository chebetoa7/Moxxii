using App.ViewModels.Acount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views.Acount
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginConductorPage : ContentPage
    {
        #region Vars
        private readonly LoginConductorViewModel _viewModel;
        List<StackLayout> layouts;
        #endregion
        #region Constructor
        public LoginConductorPage()
        {
            InitializeComponent();
            InitComponentStackLayout();
            BindingContext = _viewModel = new LoginConductorViewModel(layouts);
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
}