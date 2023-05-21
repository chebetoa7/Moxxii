using App.Services;
using App.ViewModels.Acount;
using App.ViewModels.Controls;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Controls.PopPup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarRutePopup : PopupPage
    {

        #region Vars
        private StartDayViewModel _viewModel;
        #endregion
        public StarRutePopup()
        {
            InitializeComponent();
            BindingContext = _viewModel = new StartDayViewModel();
        }

        
    }
}