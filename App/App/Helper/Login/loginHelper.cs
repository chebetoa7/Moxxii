using App.Models.Body;
using App.Response;
using App.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Helper.Login
{
    public partial class loginHelper
    {
        public async Task<ResponseToken> Login(string _usuario, string _password)
        {
            ResponseToken res = null;
            try
            {

                var lodata = new loginModel
                {
                    usuario = _usuario,
                    password = _password
                };

                var apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseUrl);
                var user = await apiManager.GetTokenUser(lodata);
                var response = user.result;

                if (user.success == true)
                {
                    res = user;
                    return res;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return res;
        }
        public async Task<bool> UpdateToken(string token, int idUser, string tokenAccess)
        {
            try
            {

                var apiManager = RestService.For<IMoxxiiApi>(
                    MoxxiiApi.BaseUrl,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                        Task.FromResult(tokenAccess)
                    });
                //var apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseUrl);
                var tokentupdateResponse = await apiManager.UpdateToken(token, idUser);
                //var response = tokentupdateResponse.success;

                if (tokentupdateResponse.success == true)
                {

                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
