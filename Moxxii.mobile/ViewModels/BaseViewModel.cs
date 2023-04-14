using CommunityToolkit.Mvvm.ComponentModel;
using Moxxii.mobile.Controls.PopPup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Moxxii.mobile.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        #region Var

        #endregion

        #region Command
        public ICommand InitializeDataCommand { get; set; }
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
            await MauiPopup.PopupAction.DisplayPopup(new LoadingPopup());
            //string result = await MauiPopup.PopupAction.DisplayPopup(new LoadingPopup());
            //await .Instance.PushAsync(activityIndicator);
        }
        public async Task LoadingFalse()
        {
            //ActivityIndicator activityIndicator = new ActivityIndicator();
            await MauiPopup.PopupAction.ClosePopup(new LoadingPopup());
            //string result = await MauiPopup.PopupAction.DisplayPopup(new LoadingPopup());
            //await .Instance.PushAsync(activityIndicator);
        }
        #endregion
    }
}
