using App.Views.Mapas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewModels.Dashboard
{
    public partial class DashboardPasajeroViewModel : BaseViewModel
    {
        #region Var
        #endregion

        #region Properties

        #endregion

        #region Command
        public ICommand MapCommand { get; set; }
        #endregion

        #region Constructor 
        public DashboardPasajeroViewModel()
        {
            MapCommand = new Command(async () => await TapMapDataCommands());
        }
        #endregion

        #region Tap Command
        private async Task TapMapDataCommands()
        {

            await NavigateAsync(new MapPasajeroPage());
        }
        #endregion

        #region Methods 
        #endregion
    }
}
