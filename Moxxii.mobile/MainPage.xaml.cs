using Moxxii.mobile.Services;
using Refit;

namespace Moxxii.mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            button_getf2p.Clicked += Button_getf2p_Clicked; 
        }

        private async void Button_getf2p_Clicked(object sender, EventArgs e)
        {
            try
            {
                var apiClient = RestService.For<IFreeToPlayApi>(BaseFreeToPlayApi.BaseUrl);
                var listF2P = await apiClient.GetF2PAsync();
                StacklayoutListF2P.ItemsSource = listF2P;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oups " + ex.Message);
            }
        }
    }
}