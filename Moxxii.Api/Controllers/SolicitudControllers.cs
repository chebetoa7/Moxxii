using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Moxxii.Api.Body;
using Moxxii.Api.Data;
using Moxxii.Api.Models;
using Moxxii.Api.Services;
using Moxxii.Shared.Entities;
using Newtonsoft.Json;
using Refit;
using System.Xml.Linq;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Moxxii.Api.Controllers
{
    [ApiController]
    public class SolicitudControllers : ControllerBase
    {
        private DataContext _context;
        private SolicitudViaje? solinueva;
        private SolicitudViaje? soli;
        private IMoxxiiApi apiManager;
        private Task<RouteMaps> mapCord;
        private Models.Route? route;

        public SolicitudControllers(DataContext context, IConfiguration configuration)
        {
            _context = context;
        }
        int van = 0;
        private int idConductornew_;

        /*Cuando es una solicitud nueva el estatus de confirmación es "asignado"*/
        [HttpPost]
        [Route("/api/solicitud/solicitudNuevoViaje")]
        [Authorize]
        public async Task<dynamic> NuevoViaje(SolicitudViaje solicitud)//, int userDescartado)
        {
            van = 0;
            solinueva = new SolicitudViaje();
            solinueva = solicitud;
            try
            {
                //var usuario = await AsignarSolicitudChofer(solicitud.IdPasajero.ToString());
                solinueva.IdConductor = solicitud.IdConductor;
                _context.solicitudViajes.Add(solinueva);
                await _context.SaveChangesAsync();
                var usuario = await _context.Usuarios.Where(w => w.Id == solicitud.IdConductor).FirstOrDefaultAsync();
                //await EnviarPush("Nuevo viaje", "Nueva solicitud del usuario: " + usuario.Name + usuario.LastName, usuario.Tokenfirebase, MoxxiiApi.AutoritationFCM);
                return new
                {
                    success = true,
                    message = "exito",
                    result = solinueva
                };
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
                return new {
                    success= false,
                    message = "error: " + ex.Message + ", error: " + ex ,
                    result = solinueva
                };
            }
        }
        
        /*Cuando es una solicitud nueva el estatus de confirmación es "asignado"*/
        [HttpPost]
        [Route("/api/solicitud/NuevoViaje")]
        [Authorize]
        public async Task<dynamic> SLNuevoViaje([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

            SolicitudViaje solicitud = new SolicitudViaje();
            List<Usuario> usuarios_ = new List<Usuario>();
            try
            {
                /*var disponible = VerDisponibilidad(nuevoViaje.Disponibility);
                var distric = VerDistric(nuevoViaje.dictric);
                var city = VerCity(nuevoViaje.city);
                var typeUser = verTypeUser(nuevoViaje.TypeUser);*/

                int idPasajero_ = data.idPasajero;
                Double latInitial_ = data.latInitial;
                Double longInitial_ = data.longInitial;
                string city_ = data.city.ToString();
                string dictric_ = data.dictric.ToString();
                bool status_ = data.status;
                string confirmationStatus_ = data.confirmationStatus.ToString();
                string typeUser_ = data.typeUser.ToString();
                string disponibility_ = data.disponibility.ToString();

                usuarios_ = await _context.Usuarios.
                   Where(w => w.TypeUser == typeUser_ &&
                   w.Disponibility == disponibility_ &&
                   w.Distric == dictric_ &&
                   w.City == city_).ToListAsync();

                if (usuarios_.Count() == 0)
                {

                    usuarios_ = await _context.Usuarios.
                        Where(w => w.Disponibility == "libre" &&
                        w.Distric == dictric_ &&
                        w.City == city_).ToListAsync();

                }
                if (usuarios_.Count() > 0)
                {
                    solicitud = new SolicitudViaje
                    {
                        IdPasajero = idPasajero_,
                        IdConductor = usuarios_.FirstOrDefault().Id,
                        latInitial = latInitial_,
                        longInitial = longInitial_,
                        City = city_,
                        Dictric = dictric_,
                        status = status_,
                        ConfirmationStatus = confirmationStatus_
                    };

                    var usuariAEnviar = usuarios_.FirstOrDefault();
                    //usuariAsignadoConductor = usuariAEnviar.Id;
                    var usPasaje = await _context.Usuarios.Where(w => w.Id == idPasajero_).FirstOrDefaultAsync();
                    //await EnviarPushLocal(usuariAEnviar.Tokenfirebase, "Nuevo viaje", "Nueva solicitud del usuario: " + usPasaje.Name + usPasaje.LastName, MoxxiiApi.AutoritationFCM);

                    _context.solicitudViajes.Add(solicitud);
                    //van = 3;
                    await _context.SaveChangesAsync();

                    return new
                    {
                        success = true,
                        message = "exito"
                    };
                }
                else 
                {
                    return new
                    {
                        success = false,
                        message = "Sin usuario asignado"
                    };
                }


                
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
                return new {
                    success= false,
                    message = "error: " + ex.Message + ", error: " + ex 
                };
            }
        }

        private string VerDisponibilidad(string disponibility)
        {
            string enviarDisponibilidad = "";
            if (disponibility == "ID") { enviarDisponibilidad = "libre"; } 
            else 
            {
                enviarDisponibilidad = "";
            }
            return enviarDisponibilidad;
        }
        private string VerDistric(string dis)
        {
            string envDis = "";
            if (dis == "LAVA") { envDis = "la ventosa"; } 
            else 
            {
                envDis = "";
            }
            return envDis;
        }
        private string VerCity(string city)
        {
            string envCity = "";
            if (city == "JUCN") { envCity = "Juchitan"; } 
            else 
            {
                envCity = "";
            }
            return envCity;
        }
        string verTypeUser(string type)
        {
            string types = "";
            if (type == "tm") { types = "tdm"; } 
            else 
            {
                types = "";
            }
            return types;
        }

        [HttpPatch]
        [Route("/api/solicitud/ReenviarSolicitudViaje")]
        public async Task<dynamic> ActualizarNuevoViaje(SolicitudViaje solicitud)
        {
            solinueva = new SolicitudViaje();
            try
            {
                /*solinueva = await AsignarSolicitudChofer(solicitud);//, userDescartado);
                _context.solicitudViajes.Update(solinueva);
                await _context.SaveChangesAsync();*/
                return new
                {
                    success = true,
                    message = "exito",
                    result = solinueva
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new
                {
                    success = false,
                    message = "error: " + ex.Message,
                    result = solinueva
                };
            }
        }

        [HttpGet]
        [Route("/api/solicitud/rechasarSolicitud")]
        public async Task<dynamic> rechasar(int idConductor)
        { 
            try 
            {

            } catch (Exception ex) 
            {
            }
            return null;
        }
        [HttpGet]
        [Route("/api/solicitud/escaneoSolicitudViaje")]
        public async Task<dynamic> VerificarViaje(int id)
        {
            try 
            {
                var solicitudes = await _context.solicitudViajes. Where(
                    
                    w => w.IdConductor == id ||
                    w.IdPasajero == id
                    &&
                    w.ConfirmationStatus != "Terminado" ||
                    w.ConfirmationStatus != "Aceptado"
                    &&
                    w.status == true
                    ).ToListAsync();

                if(solicitudes.Count()>0)
                    return new
                    {
                        success = true,
                        message = "asignado"
                    };
                else
                    return new
                    {
                        success = false,
                        message = "noAsignado"
                    };
            } catch (Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message);
                return new
                {
                    success = false,
                    message = "Error " + ex.Message
                };
            }

        }
        [HttpPatch]
        [Route("/api/solicitud/tomarviaje")]
        public async Task<dynamic> TomarViajeCondctor(SolicitudViaje solicitud) 
        {
            List<SolicitudViaje> solicitudes = new List<SolicitudViaje>();
            try 
            {
                solicitudes = await _context.solicitudViajes.
                    Where(
                    w => w.Dictric == solicitud.Dictric && 
                    w.City == solicitud.City && (
                    w.ConfirmationStatus != "Terminado" ||
                    w.ConfirmationStatus != "Aceptado") &&
                    w.IdConductor == solicitud.IdConductor
                    ).ToListAsync();
                soli = solicitudes.FirstOrDefault();
                if (soli != null)
                {
                    soli.IdConductor = solicitud.IdConductor;
                    soli.ConfirmationStatus = solicitud.ConfirmationStatus;
                    soli.latEnd = solicitud.latEnd;
                    soli.longEnd = solicitud.longEnd;
                    soli.status = false;

                    apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseMapsUrl);
                    mapCord = apiManager.GetRoute(soli.latInitial, soli.longInitial, soli.latEnd, soli.longEnd, "AIzaSyC2XudfRAoNIVBkWWwi9Y20obpb00T6jU0");
                    route = mapCord.Result.routes.FirstOrDefault();// routes.FirstOrDefault().legs.FirstOrDefault().distance.ToString();



                    _context.solicitudViajes.Update(soli);
                    await _context.SaveChangesAsync();

                    return new
                    {
                        success = true,
                        message = "exito",
                        result = soli,
                        routes = route,
                        map = mapCord
                    };
                }
                else 
                {
                    return new
                    {
                        success = false,
                        message = "error",
                        result = soli,
                        routes = route,
                        map = mapCord
                    };
                }
                

            } catch (Exception ex) 
            {
                Console.WriteLine($"{ex.Message}");
                return new
                {
                    success = false,
                    message = "error: " + ex.Message,
                    result = soli,
                    routes = route,
                    map = mapCord
                };
            }
        }

        private async Task<SolicitudViaje> AsignarSolicitudChofer(string IdPasajero_)//, int IdUsuarioDescartado)
        {
            int usuariAsignadoConductor = 0;
            int IdPasajero = Int32.Parse(IdPasajero_);
            List <Usuario> usuarios = new List<Usuario>();
            try 
            {
                
                usuarios = await _context.Usuarios.
                    Where(w => w.TypeUser == "tdm" && w.Disponibility == "libre").ToListAsync();
                
                if (usuarios.Count() == 0)
                {
                    
                    usuarios = await _context.Usuarios.
                        Where(w => w.Disponibility == "libre").ToListAsync();
                   
                }

                
                
                if (usuarios.Count() > 0) //Usuario encontrado enviar
                {
                    
                    var usuariAEnviar = usuarios.FirstOrDefault();
                    usuariAsignadoConductor = usuariAEnviar.Id;
                    var usPasaje = await _context.Usuarios.Where(w => w.Id == IdPasajero).FirstOrDefaultAsync();
                   // await EnviarPushLocal( usuariAEnviar.Tokenfirebase, "Nuevo viaje","Nueva solicitud del usuario: " + usPasaje.Name + usPasaje.LastName, MoxxiiApi.AutoritationFCM);
                  
                    return null;
                }

            } catch (Exception ex) 
            {
                
                Console.WriteLine(ex.ToString());
                 
            }
            return null;
        }

        [HttpGet]
        [Route("/api/Solicitud/Get")]
        public async Task<dynamic> GetSolicitudes()
        {
            try
            {

                var us = await _context.solicitudViajes.ToListAsync();
                return Ok(us);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/Solicitud/Remove")]
        public async Task<dynamic> RemoveSolicitudes(int idsolicitud)
        {
            try
            {
                var result = await _context.solicitudViajes.FirstOrDefaultAsync(e => e.Id == idsolicitud);
                _context.solicitudViajes.Remove(result);
                await _context.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/NotificationFirebase/Push")]
        [Authorize]
        public async Task<dynamic> EnviarPushLocal([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

            string titulo = data.titulo.ToString();
            string mensaje = data.mensaje.ToString();
            string k1 = data.key_1.ToString();
            string k2 = data.key_2.ToString();

            string tokenDivice = data.tokenDivice.ToString();
            string tokenFCM = data.tokenFirebase.ToString();
            string collapse_key = data.collapse_key.ToString();

            try
            {
                var apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BasePushUrl);


                var data_ = new Moxxii.Api.Body.Data
                {
                    
                    key_1 = k1,
                    key_2 = k2
                };
                var notification = new Moxxii.Api.Body.Notification
                {
                    body = mensaje,
                    title = titulo
                };
                var _body = new fcmBodyData
                {
                    to = tokenDivice,
                    notification = notification,
                    data = data_,
                };


                var send = await apiManager.SendPush(_body, "key=" + tokenFCM);
                var result = send.results;
                return new
                {
                    success = false,
                    message = "Mensaje enviado"
                };
            } catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new
                {
                    success = false,
                    message = "Error" + ex.Message
                };
            }
            //var distance = tokent1.Result.routes.FirstOrDefault().legs.FirstOrDefault().distance.ToString();
        }
    }
}
