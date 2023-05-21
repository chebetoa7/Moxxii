using App.Views.Acount;
using App.Views.Dashboard;
using App.Views.Mapas;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewModels.Acount
{
    public partial class LoginPrecentationViewModel : BaseViewModel
    {
        #region Vars
        //[ObservableProperty]
        //public bool _isBusy;

        //[ObservableProperty]
       // public bool _isVisible;

        private StackLayout stkk1;
        private StackLayout stkk2;
        private Image imgg;

        #endregion

        #region Command
        public ICommand OpenUserCommand { get; set; }
        public ICommand OpenConductorCommand { get; }
        #endregion

        #region Constructor 
        [Obsolete]
        public LoginPrecentationViewModel(List<StackLayout> stkConfig, Image imgConfig)
        {
            // config Precentation app Initial
            //Configuración de la interfaz de precentación inicial
            stkk1 = stkConfig[0].FindByName<StackLayout>("stkPrecentation");
            stkk1.BackgroundColor = Color.FromHex("#FF746B");
            stkk2 = stkConfig[1].FindByName<StackLayout>("stkMain");
            stkk2.IsVisible = false;
            imgg = imgConfig.FindByName<Image>("ImgLogin");
            imgg.Source = "logincontraste.png";

            //Action changes page precentation
            //Evento para cambiar las precentación inicial de la app
            _ = StarPrecentationOne();

            OpenUserCommand = new Command(async () => await TapUserInitialDataCommands());
            OpenConductorCommand = new Command(async () => await TapConductorDataCommands());
        }
        #endregion

        #region Tap Command
        private async Task TapUserInitialDataCommands()
        {
            try
            {
                await NavigateAsync(new LoginPasajeroPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }
        private async Task TapConductorDataCommands()
        {
            try
            {
                await NavigateAsync(new LoginConductorPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }
        #endregion

        #region Methods 

        private async Task StarPrecentationOne()
        {
            try
            {
                await WaitAndExecute(1800, () =>
                {
                    stkk1.BackgroundColor = Color.FromHex("#FFFFFF");
                    imgg.Source = "loginsplash.png";
                    _ = StarPrecentationTwo();
                });
            }
            finally
            {
                Console.WriteLine("\nError de StartActionAgainPrecentation: ");
            }
        }
        private async Task StarPrecentationTwo()
        {
            try
            {
                await WaitAndExecute(1200, () =>
                {
                    
                    var usuarioConfig = DB.ConfigRepository.Instancia.GetConfigUser().ToList();

                    if (usuarioConfig.Count() > 0)
                    {
                        var usuario = usuarioConfig.FirstOrDefault();
                        if (usuario.typeUser == "pasajero")
                        {
                            _ = NavigateAsync(new DashboardPasajeroPage());
                        }
                        else
                        {
                            _ = NavigateAsync(new MapConductorPage());
                        }
                    }
                    else 
                    {
                        stkk1.IsVisible = false;
                        stkk2.IsVisible = true;
                    }
                    
                });
            }
            finally
            {
                Console.WriteLine("\nError de StartActionAgainPrecentation: ");
            }
        }

        protected async System.Threading.Tasks.Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await System.Threading.Tasks.Task.Delay(milisec);
            actionToExecute();
        }

        #endregion
    }
}
