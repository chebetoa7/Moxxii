using App.Models.Body;
using App.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;


namespace App.Services
{
    public partial class ManagerServices
    {
        Helper.Login.loginHelper helperL;

        public async Task<bool> StartDay(string disponibility, int id, string tokenAccess) 
        {
            bool startDayResponse = false;
            try 
            {
                var apiManager = RestService.For<IMoxxiiApi>(
                    MoxxiiApi.BaseUrl,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                        Task.FromResult(tokenAccess)
                    });

                
                var response = await apiManager.StartDay(disponibility,id);
                
                if (response.sucess == true)
                {
                    startDayResponse = response.sucess;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return startDayResponse;
        }
        public async Task<Usuario> SolicitudViaje(NuevoViaje solicitud, string tokenAccess) 
        {
            ResponseToken res = null;
            Usuario usuarioEncontrado = null;
            try
            {

                var lodata = new UserFreeBody
                {
                    city = solicitud.city,
                    aviable = solicitud.Disponibility
                };
                var apiManager = RestService.For<IMoxxiiApi>(
                    MoxxiiApi.BaseUrl,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                        Task.FromResult(tokenAccess)
                    });
                var user = await apiManager.GetShareFree(lodata);
                
                if (user != null)
                {
                    usuarioEncontrado = user.result.usuario;

                    solicitud.idConductor = usuarioEncontrado.id;
                    var responseSolViaje = await apiManager.SolicitudViaje(solicitud);
                    if (user != null && responseSolViaje != null)
                    {
                        return usuarioEncontrado;
                    }
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuarioEncontrado;
        }
        public async Task<bool> SendPushFirebase(FCMBody body, string tokenAccess) 
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
                var push = await apiManager.SendPushFirebase(body);
                
                if (push != null)
                {
                    return push.success;
                    
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
