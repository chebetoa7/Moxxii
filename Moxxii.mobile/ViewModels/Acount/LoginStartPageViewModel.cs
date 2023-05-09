using CommunityToolkit.Mvvm.ComponentModel;
using Moxxii.mobile.Views.Acount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Moxxii.mobile.ViewModels.Acount
{
    public partial class LoginStartPageViewModel : BaseViewModel
    {
        #region Vars
        [ObservableProperty]
        public bool _isBusy;

        [ObservableProperty]
        public bool _isVisible;

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
        public LoginStartPageViewModel(List<StackLayout> stkConfig, Image imgConfig)
        {
            //config Precentation app Initial
            //Configuración de la interfaz de precentación inicial
            stkk1 = stkConfig[0].FindByName<StackLayout>("stkPrecentation");
            stkk1.BackgroundColor = Color.FromRgba("#FF746B");
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
                await NavigateAsync(new LoginSessionSelectPage());
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
                await NavigateAsync(new LoginConductorSelectPage());
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
                await WaitAndExecute(2800, () =>
                {
                    stkk1.BackgroundColor = Color.FromRgba("#FFFFFF");
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
                await WaitAndExecute(1800, () =>
                {
                    stkk1.IsVisible = false;
                    stkk2.IsVisible = true;
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
