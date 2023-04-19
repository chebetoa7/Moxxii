using Moxxii.mobile.Models.Body;
using Moxxii.mobile.Models.Response;
using Moxxii.mobile.Services;
using Moxxii.mobile.Services.Login;
//using Moxxii.mobile.Services.Auth;
using Refit;

namespace Moxxii.mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        readonly ILoginRepository _loginRepository = new LoginServices();

        ApiServices _Services;

        public MainPage()
        {
            InitializeComponent();
            button_getf2p.Clicked += Button_getf2p_Clicked; 
        }
        private async void Button_getf2p_Clicked(object sender, EventArgs e)
        {
            try
            {
                _Services = new ApiServices();
                var _EnPointAPI = await _Services.GetFiltre<Responsemoxxii>(
                            MoxxiiApi.BaseUrl,
                            "/api",
                            "/Usuario/Get",
                            ""
                        );

                StacklayoutListF2P.ItemsSource = _EnPointAPI.First().result;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oups " + ex.Message);
            }
        }
        /*
        private async void Button_getf2p_Clicked(object sender, EventArgs e)
        {
            try
            {
                var musu = new loginModel { usuario = "nor@gmail.com", password = "Nor.8080" };
                var usu = await _loginRepository.Login(musu, "/Usuario/Login");

                StacklayoutListF2P.ItemsSource = usu.result;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oups " + ex.Message);
            }
        }

        */

        /*
        private async void Button_getf2p_Clicked(object sender, EventArgs e)
        {
            try
            {
                /*
                var apiClient2 = RestService.For<IMoxxiiApi>(
                    MoxxiiApi.BaseUrl,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                        Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiI4OWNmNzAwOS02ZmFkLTQ3ZjUtOGFhYS0zYTU2NTU0MDZiMWIiLCJpYXQiOiIxOC8wNC8yMDIzIDAyOjU2OjA2IHAuIG0uIiwiaWQiOiIxIiwidXNlcl9uYW1lIjoiTm9yYmVydG8iLCJleHAiOjE2ODE5MTYxNjYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzIzLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzIzLyJ9.wlEjUqQDWig7eQynP5huurPiGrMCbnN-xVaCw1oYyYY")
                    });*/

        /*var apiClient = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseUrl);
                var usuerapi = await apiClient.GetUsuarioAll();
                StacklayoutListF2P.ItemsSource = usuerapi;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oups " + ex.Message);
            }
        }*/

        /*
         private async void Button_getf2p_Clicked(object sender, EventArgs e)
        {
            try
            {
                var apiClient = RestService.For<IMoxxiiApi>(
                    MoxxiiApi.BaseUrl,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                        Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiI4OWNmNzAwOS02ZmFkLTQ3ZjUtOGFhYS0zYTU2NTU0MDZiMWIiLCJpYXQiOiIxOC8wNC8yMDIzIDAyOjU2OjA2IHAuIG0uIiwiaWQiOiIxIiwidXNlcl9uYW1lIjoiTm9yYmVydG8iLCJleHAiOjE2ODE5MTYxNjYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzIzLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzIzLyJ9.wlEjUqQDWig7eQynP5huurPiGrMCbnN-xVaCw1oYyYY")
                    });
                var usuerapi = apiClient.GetUsuario(1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiI4OWNmNzAwOS02ZmFkLTQ3ZjUtOGFhYS0zYTU2NTU0MDZiMWIiLCJpYXQiOiIxOC8wNC8yMDIzIDAyOjU2OjA2IHAuIG0uIiwiaWQiOiIxIiwidXNlcl9uYW1lIjoiTm9yYmVydG8iLCJleHAiOjE2ODE5MTYxNjYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzIzLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzIzLyJ9.wlEjUqQDWig7eQynP5huurPiGrMCbnN-xVaCw1oYyYY").Result;
                StacklayoutListF2P.ItemsSource = usuerapi;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oups " + ex.Message);
            }
        }
         */
    }
}