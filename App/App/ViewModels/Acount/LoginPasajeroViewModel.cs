using App.Models.Body;
using App.Services;
using App.Views.Dashboard;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
                if (usuarioValido == null)
                {
                    await DisplayAlertMessage("Mensaje", "Usuario invalido", "OK");
                }
                else
                {
                    var myValue = Preferences.Get("TokenFirebase", "");
                    if (myValue != null) 
                    {
                        var updToken = await helperL.UpdateToken(myValue, usuarioValido.result.usuario.id, usuarioValido.result.token);
                    }
                    await NavigateAsync(new DashboardPasajeroPage());
                    //var toent_ = Preferences.Get("TokenFirebase");
                    
                }

            }
            catch (Exception ex)
            {
                await DisplayAlertMessage("Mensaje", "Error: " + ex.Message, "OK");
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
