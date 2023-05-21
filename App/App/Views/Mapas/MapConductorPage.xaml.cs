using App.ViewModels.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views.Mapas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapConductorPage : ContentPage
    {
        #region Vars
        private readonly MapConductorViewModel _viewModel;
        #endregion
        [Obsolete]
        public MapConductorPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MapConductorViewModel(mapa_);
        }
    }
}