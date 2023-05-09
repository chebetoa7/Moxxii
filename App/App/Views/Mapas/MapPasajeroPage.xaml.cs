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
    public partial class MapPasajeroPage : ContentPage
    {
        #region Vars
        private readonly MapPasajeViewModel _viewModel;
        #endregion
        [Obsolete]
        public MapPasajeroPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MapPasajeViewModel(mapa_);
        }
    }
}