using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Moxxii.Api.Body;
using Moxxii.Api.Data;
using Moxxii.Api.Models;
using Moxxii.Api.Services;
using Moxxii.Shared.Entities;
using Refit;

namespace Moxxii.Api.Controllers
{
    [ApiController]
    public class SolicitudControllers : ControllerBase
    {
        private DataContext _context;
        private SolicitudViaje solinueva;
        private SolicitudViaje? soli;
        private IMoxxiiApi apiManager;
        private Task<RouteMaps> mapCord;
        private Models.Route? route;

        public SolicitudControllers(DataContext context, IConfiguration configuration)
        {
            _context = context;
        }
        

        /*Cuando es una solicitud nueva el estatus de confirmación es "asignado"*/
        [HttpPost]
        [Route("/api/solicitud/solicitudNuevoViaje")]
        public async Task<dynamic> NuevoViaje(SolicitudViaje solicitud)//, int userDescartado)
        {
            solinueva = new SolicitudViaje();
            try
            {
                solinueva = await AsignarSolicitudChofer(solicitud);//, userDescartado);
                _context.solicitudViajes.Add(solinueva);
                    
                await _context.SaveChangesAsync();
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
                return new
                {
                    success = false,
                    message = "error: " + ex.Message,
                    result = solinueva
                };
            }
        }


        [HttpPatch]
        [Route("/api/solicitud/ReenviarSolicitudViaje")]
        public async Task<dynamic> ActualizarNuevoViaje(SolicitudViaje solicitud)
        {
            solinueva = new SolicitudViaje();
            try
            {
                solinueva = await AsignarSolicitudChofer(solicitud);//, userDescartado);
                _context.solicitudViajes.Update(solinueva);
                await _context.SaveChangesAsync();
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
        public async Task<dynamic> VerificarViaje(int idConductor)
        {
            try 
            {
                var solicitudes = await _context.solicitudViajes. Where(
                    w => w.IdConductor == idConductor && 
                    ( 
                        w.ConfirmationStatus != "Terminado" ||
                        w.ConfirmationStatus != "Aceptado"
                    ) &&
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

                    _context.solicitudViajes.Update(soli);
                    await _context.SaveChangesAsync();

                    apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseMapsUrl);
                    mapCord = apiManager.GetRoute(soli.latInitial, soli.longInitial, soli.latEnd, soli.longEnd, "AIzaSyC2XudfRAoNIVBkWWwi9Y20obpb00T6jU0");
                    route = mapCord.Result.routes.FirstOrDefault();// routes.FirstOrDefault().legs.FirstOrDefault().distance.ToString();

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



        private async Task<SolicitudViaje> AsignarSolicitudChofer(SolicitudViaje solicitud)//, int IdUsuarioDescartado)
        {
            List<Usuario> usuarios = new List<Usuario>();
            try 
            {
                usuarios = await _context.Usuarios.
                    Where(w => w.Distric == solicitud.Dictric && w.City == solicitud.City && w.TypeUser == "tdm" && w.Disponibility == "libre").ToListAsync();
                if(usuarios.Count() == 0)
                    usuarios = await _context.Usuarios.
                        Where(w => w.Distric == solicitud.Dictric && w.City == solicitud.City && w.Disponibility == "libre").ToListAsync();

                /*if (IdUsuarioDescartado != 0) 
                {
                    var descar_usuarios = usuarios.Where(w => w.Id == IdUsuarioDescartado).ToList();
                    if (descar_usuarios.Count() > 0)
                    {
                        Usuario? eliminar = descar_usuarios.FirstOrDefault();
                        _ = usuarios.Remove(eliminar);
                    }

                }*/

                if (usuarios.Count() > 0) //Usuario encontrado enviar
                {
                    var usuariAEnviar = usuarios.FirstOrDefault();
                    solicitud.IdConductor = usuariAEnviar.Id;
                    
                    return solicitud;
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

        private void EnviarPushLocal(SolicitudViaje solicitud)
        {
            var apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BasePushUrl);
            fcmBody fcmBody_ = new fcmBody();

            var titulo = fcmBody_.data.title = "";
            var message = fcmBody_.data.message = "";

            var data_ = new Moxxii.Api.Body.Data 
            {
                title = titulo,
                message = message,
            };
            var _body = new fcmBody
            {
                to = "",
                data = data_
            };

            //var tokent1 = apiManager.NewPushNotification(_body);
            //var distance = tokent1.Result.routes.FirstOrDefault().legs.FirstOrDefault().distance.ToString();
        }
    }
}
