using App.Models.Body;
using App.Services;
using App.Views.Dashboard;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewModels.Acount
{
    public partial class LoginPasajeroViewModel : BaseViewModel
    {
        #region Vars
        private StackLayout stkL;
        Helper.Login.loginHelper helperL;
        #endregion

        #region Properties
        private string username;
        public string UserName
        {
            get => username;
            set
            {
                SetProperty(ref username, value);
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
            }
        }


        #endregion

        #region Command
        public ICommand OkLoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginPasajeroViewModel(List<StackLayout> stkConfig)
        {
            helperL = new Helper.Login.loginHelper();
            stkL = stkConfig[0].FindByName<StackLayout>("stkLoading");
            OkLoginCommand = new Command(async () => await TapOkSessionLoginCommands());
        }

        #endregion

        #region Methods 
        [Obsolete]
        public async Task<bool> Login(string _usuario, string _password)
        {
            try
            {

                var lodata = new loginModel
                {
                    usuario = _usuario,
                    password = _password
                };

                var apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseUrl);
                var tokent1 = apiManager.GetTokenUser(lodata);
                var result = tokent1.Result;

                if (result.success == true)
                {

                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
        #endregion

        #region Tap Command
        [Obsolete]
        private async Task TapOkSessionLoginCommands()
        {
            try
            {
                //await LoadingTrue();
                //Device.BeginInvokeOnMainThread(async () => await LoadingTrue());
                stkL.IsVisible = true;
                var usuarioValido = await helperL.Login(UserName.ToString(), Password.ToString());
                if (!usuarioValido)
                {
                    await DisplayAlertMessage("Mensaje", "Usuario invalido", "OK");
                }
                else
                {
                    await NavigateAsync(new DashboardPasajeroPage());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message + ", TapOkSessionLoginCommands");
            }
            stkL.IsVisible = false;
            //Device.BeginInvokeOnMainThread(async () => await LoadingFalse());
            // await LoadingFalse();

        }

        protected async System.Threading.Tasks.Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await System.Threading.Tasks.Task.Delay(milisec);
            actionToExecute();
        }

        #endregion
    }
}
