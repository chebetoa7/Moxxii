using Moxxii.mobile.Views.map.mapUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Moxxii.mobile.ViewModels.Dashboard.UserMainPage
{
    public partial class DashboardUserMainPageViewModel : BaseViewModel
    {
        #region Var
        #endregion

        #region Properties
        
        #endregion

        #region Command
        public ICommand MapCommand { get; set; }
        #endregion

        #region Constructor 
        public DashboardUserMainPageViewModel()
        {
            MapCommand = new Command(async () => await TapMapDataCommands());
        }
        #endregion

        #region Tap Command
        private async Task TapMapDataCommands()
        {
            
            await NavigateAsync(new MapUserPage());
        }
        #endregion

        #region Methods 
        #endregion
    }
}
