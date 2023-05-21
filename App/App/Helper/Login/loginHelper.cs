using App.DB;
using App.Models.Body;
using App.Response;
using App.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace App.Helper.Login
{
    public partial class loginHelper
    {
        
        public async Task<ResponseToken> ConductorLibre(string city, string disponibility, string tokenAccess)
        {
            
            ResponseToken res = null;
            try
            {

                var lodata = new UserFreeBody
                {
                    city = city,
                    aviable = disponibility
                };
                var apiManager = RestService.For<IMoxxiiApi>(
                    MoxxiiApi.BaseUrl,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                        Task.FromResult(tokenAccess)
                    });
                var user = await apiManager.GetShareFree(lodata);
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
                    DB.ConfigRepository.Instancia.deleteAll();
                    var userConfig = new ConfigUser 
                    {
                        id = response.usuario.id,
                        disponibility = response.usuario.disponibility,
                        name = response.usuario.name,
                        lastName = response.usuario.lastName,
                        otherLastName  = response.usuario.otherLastName,
                        birthdate = response.usuario.birthdate,
                        dateAge = response.usuario.dateAge,
                        sex = response.usuario.sex,
                        address = response.usuario.address,
                        country = response.usuario.country,
                        city = response.usuario.city,
                        distric = response.usuario.distric,
                        ubicationRealLat = response.usuario.ubicationRealLat,
                        ubicationRealLon = response.usuario.ubicationRealLon,
                        typeUser = response.usuario.typeUser,
                        status  = response.usuario.status,
                        metadata = response.usuario.metadata,   
                        phoneNumber = response.usuario.phoneNumber,
                        email = response.usuario.email,
                        password = response.usuario.password,
                        tokenfirebase = response.usuario.tokenfirebase,
                        dataPushUpdate = response.usuario.dataPushUpdate,
                        loginNow = response.usuario.loginNow,
                        
                    };
                    DB.ConfigRepository.Instancia.AddConfigUser(userConfig);
                    Preferences.Set("config_user_id", response.usuario.id);
                    Preferences.Set("config_user_tokenApi", response.token);
                    Preferences.Set("config_user_disponibility", response.usuario.disponibility);
                    Preferences.Set("config_user_name", response.usuario.name);
                    Preferences.Set("config_user_lastName", response.usuario.lastName);
                    Preferences.Set("config_user_contry", response.usuario.country);
                    Preferences.Set("config_user_address", response.usuario.address);
                    Preferences.Set("config_user_city", response.usuario.city);
                    Preferences.Set("config_user_distric", response.usuario.distric);
                    Preferences.Set("config_user_typeUser", response.usuario.typeUser);
                    Preferences.Set("config_user_phoneNumber", response.usuario.phoneNumber);
                    Preferences.Set("config_user_email", response.usuario.email);
                    Preferences.Set("config_user_loginNow", response.usuario.loginNow);
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
