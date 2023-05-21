using App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.ViewModels.Controls
{
    public partial class ShareMotoTaxxiiViewModel : BaseViewModel
    {
        #region Vars
        private ManagerServices managerServices;

        #endregion

        #region Commands  
        public Command OkShareMotoDay { get; }
        #endregion

        #region Constructor
        public ShareMotoTaxxiiViewModel()
        {
            managerServices = new ManagerServices();
            OkShareMotoDay = new Command(async () => await TapShareMotoCommands());
            
        }
        #endregion

        private async Task TapShareMotoCommands()
        {
            
        }
        private async Task ShareMotoAsync()
        {
            try 
            {

            }catch(Exception) 
            {
                
            }
        }

    }
}
