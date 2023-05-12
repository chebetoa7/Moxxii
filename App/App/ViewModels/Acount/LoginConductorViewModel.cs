using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using App.Views.Mapas;
using App.Views.Dashboard;
using Xamarin.Essentials;

namespace App.ViewModels.Acount
{
    public partial class LoginConductorViewModel : BaseViewModel
    {
        #region Vars
        private StackLayout stkL;
        Helper.Login.loginHelper helperL;
        #endregion

        #region Vars
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
        public LoginConductorViewModel(List<StackLayout> stkConfig)
        {
            helperL = new Helper.Login.loginHelper();
            stkL = stkConfig[0].FindByName<StackLayout>("stkLoading");
            OkLoginCommand = new Command(async () => await TapOkSessionLoginCommands());

        }
        #endregion
        #region Tap Command
        [Obsolete]
        private async Task TapOkSessionLoginCommands()
        {
            try
            {
                stkL.IsVisible = true;
                /*var usuarioValido = await helperL.Login(UserName.ToString(), Password.ToString());
                if (usuarioValido == null)
                {
                    await DisplayAlertMessage("Mensaje", "Usuario invalido", "OK");
                }
                else
                {
                    await NavigateAsync(new MapConductorPage());
                }*/
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
                Console.WriteLine("Error: " + ex.Message + ", TapOkSessionLoginCommands");
            }
            stkL.IsVisible = false;

        }
        #endregion

        #region Methods 
        #endregion
    }
}
