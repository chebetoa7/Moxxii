using Moxxii.mobile.Views.Dashboard.UserMainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Moxxii.mobile.ViewModels.Acount
{
    public partial class LoginSessionSelectViewModel : BaseViewModel
    {
        #region Var
        #endregion

        #region Command
        public ICommand OkSessionCommand { get; set; }
        public ICommand OkSamuelGayCommand { get; set; }
        #endregion

        #region Constructor
        public LoginSessionSelectViewModel()
        {
            OkSessionCommand = new Command(async () => await TapOkSessionDataCommands());
            OkSamuelGayCommand = new Command(async () => await TapOkSamuelEsGay());
        }
        #endregion

        #region Methods 
        #endregion

        #region Tap Command
        private async Task TapOkSessionDataCommands()
        {
            await NavigateAsync(new DashboardUserMainPage());
        }
        private async Task TapOkSamuelEsGay()
        {
            await App.Current.MainPage.DisplayAlert("Mensaje","Samuel es Gay rete Gay le gusta la polloga","OK");
        }
        #endregion

    }
}
