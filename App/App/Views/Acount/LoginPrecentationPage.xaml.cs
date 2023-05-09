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
    public partial class LoginPrecentationPage : ContentPage
    {
        #region Vars
        List<StackLayout> layouts;
        private readonly LoginPrecentationViewModel _viewModel;
        #endregion

        #region Constructor
        [Obsolete]
        public LoginPrecentationPage()
        {
            InitializeComponent();
            InitComponentStackLayout();
            BindingContext = _viewModel = new LoginPrecentationViewModel(layouts, ImgLogin);
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
}