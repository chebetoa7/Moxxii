using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Services
{
    public class ApiServices
    {
        #region Variables
        public static string Mensaje = "";
        #endregion


        public async Task<List<T>> GetFiltre<T>
            (
                string urlBase, string servicePreFix,
                string controller,
                string key
            )
        {
            List<T> lista = new List<T>();
            try
            {
                var client = new HttpClient(new System.Net.Http.HttpClientHandler());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("api_key", key);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePreFix, controller);
                var respose = await client.GetAsync("https://moxxiiapi20230419123954.azurewebsites.net/WeatherForecast");//"192.168.114.30:44358/api/usuario");

                if (respose.IsSuccessStatusCode == false)
                {
                    return null;
                }
                else
                {
                    var result = await respose.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<T>(result);
                    lista.Add(list);
                    return lista;
                }
            }
            catch (Exception exM)
            {
                Mensaje = exM.Message;
                throw;
            }
        }

    }
}
