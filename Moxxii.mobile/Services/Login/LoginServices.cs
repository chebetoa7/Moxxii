using Moxxii.mobile.Models.Body;
using Moxxii.mobile.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Moxxii.mobile.Services.Login
{
    public class LoginServices : ILoginRepository
    {
        public async Task<Responsemoxxii> Login(loginModel item, string tabla)
        {
            JsonSerializerOptions _serializerOptions;
            
            try 
            {
                Uri uri = new Uri(string.Format(MoxxiiApi.BaseUrl + tabla, string.Empty));
                var client = new HttpClient();
                _serializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string json = System.Text.Json.JsonSerializer.Serialize<loginModel>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tConsumer sussefull.");


            }
            catch(Exception ex) 
            {
                Console.WriteLine(value: ex.Message);
            }
            return null;
        }
    }
}
