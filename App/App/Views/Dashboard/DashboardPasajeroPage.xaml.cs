using App.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPasajeroPage : ContentPage
    {
        #region Vars
        private readonly DashboardPasajeroViewModel _viewModel;
        #endregion
        #region Constructor
        public DashboardPasajeroPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new DashboardPasajeroViewModel();
        }
        #endregion
    }
}