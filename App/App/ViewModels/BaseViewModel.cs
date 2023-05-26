using App.Controls.PopPup;
using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        #region Var

        #endregion

        #region Command
        public ICommand InitializeDataCommand { get; set; }
        public ICommand ShareUserDataCommand { get; set; }
        protected virtual void InitCommands() { }
        #endregion

        #region Constructor 

        #endregion

        #region Init Methods
        protected virtual void InitMethos() { }

        public async Task<bool> NavigateAsync(Page NextPage)
        {
            await Shell.Current.Navigation.PushAsync(NextPage);
            return true;
        }
        #endregion

        #region Loading Methods
        public async Task LoadingTrue()
        {
            //ActivityIndicator activityIndicator = new ActivityIndicator();
            //await MauiPopup.PopupAction.DisplayPopup(new LoadingPopup());
            await PopupNavigation.Instance.PushAsync(new LoadingPopup());
            //string result = await MauiPopup.PopupAction.DisplayPopup(new LoadingPopup());
            //await .Instance.PushAsync(activityIndicator);
        }
        public async Task LoadingFalse()
        {
            //ActivityIndicator activityIndicator = new ActivityIndicator();
            //await MauiPopup.PopupAction.ClosePopup(new LoadingPopup());

            await PopupNavigation.Instance.PopAsync(true);
            //string result = await MauiPopup.PopupAction.DisplayPopup(new LoadingPopup());
            //await .Instance.PushAsync(activityIndicator);
        }

        public async Task DisplayAlertMessage(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        #endregion

        #region ConductorPopup
        public async Task IniciaRutaPopup()
        {
            //await MauiPopup.PopupAction.DisplayPopup(new StarRutePopup());
            
                await PopupNavigation.Instance.PushAsync(new StarRutePopup());
            
            
        }
        public async Task TerminaRutaPopup()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion

        #region PasajeroPopup
        public async Task ShareMotoPopup()
        {
            //await MauiPopup.PopupAction.DisplayPopup(new StarRutePopup());
            await PopupNavigation.Instance.PushAsync(new ShareMotoTaxxiiPopup());
        }
        public async Task ShareMotoStopPopup()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion

        #region SolicitudViaje
        public async Task SolicitudStartPopup()
        {
            //await MauiPopup.PopupAction.DisplayPopup(new StarRutePopup());
            await PopupNavigation.Instance.PushAsync(new SolicitudViajePopup());
        }
        public async Task SolicitudStopPopup()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion
    }
}
