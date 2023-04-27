using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moxxii.Api.Data;
using Moxxii.Api.Services;
using Moxxii.Shared.Entities;
using Newtonsoft.Json;
using Refit;
using System.Xml.Linq;

namespace Moxxii.Api.Controllers
{
    [ApiController]
    public class ViajeControllers : ControllerBase
    {
        private DataContext _context;
        private static DataContext _contextStatic;
        private Usuario? usuario;

        public IConfiguration _configuration { get; set; }

        public ViajeControllers(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/api/viaje/solicitudNuevoViaje")]
        public async Task<dynamic> NuevoViaje([FromBody] Object optData) 
        {
            
            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

                if (data == null)
                {
                    return null;
                }
                Usuario usuarioAsignado = AsignaUsuario(data);
            }
            catch (Exception ex) 
            {

            }
            throw new NotImplementedException();
        }

        private async Task<Usuario> AsignaUsuario(dynamic data)
        {
            //usuario = new Usuario();
            List<Usuario> usuarios = new List<Usuario>();
            try 
            {
                
                string Distric = data.Distic.ToString();
                string City = data.City.ToString();
                double LatOrigi = data.LatOri.ToString();
                double LonOrigi = data.LonOri.ToString();
                double LatDes = data.LatOri.ToString();
                double LonDes = data.LatDes.ToString();

                usuarios = await _context.Usuarios.
                    Where(w => w.Distric == Distric || w.City == City).
                    Where(y => y.TypeUser == "tdm").ToListAsync();

                foreach (var usu in usuarios) 
                {
                    
                }


            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return usuario;
        }

        #region Methods AsignarUsuario
        private Usuario Trabajadores(double lat, double lon, double  latDes, double longDes,dynamic data) 
        {
            try 
            {
                var apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseMapsUrl);
                var tokent1 = apiManager.GetDistan(lat,lon, latDes, longDes, "AIzaSyC2XudfRAoNIVBkWWwi9Y20obpb00T6jU0");
                var distance = tokent1.Result.routes.FirstOrDefault().legs.FirstOrDefault().distance.ToString();
            } catch (Exception ex) 

            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        #endregion
    }
}
