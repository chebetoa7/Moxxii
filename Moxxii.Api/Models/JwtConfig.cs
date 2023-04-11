using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moxxii.Api.Data;
using Moxxii.Shared.Entities;
using System.Security.Claims;

namespace Moxxii.Api.Models
{
    public class JwtConfig
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Subject { get; set; }
        private static DataContext _context;
        //private static string? _id;

        public async static Task<bool> GetIdUserValidDB(string _id, DataContext context)
        {
            
            var usuario = await _context.Usuarios.Where(w => w.Id.ToString() == _id).FirstOrDefaultAsync();
            if (usuario != null) 
                return true;
            else
                return false;
            
        }
        public static dynamic ValidarTokent(ClaimsIdentity identity,DataContext context)
        {
            try
            {
                _context = context;
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Invalid Tokent",
                        result = ""
                    };
                    
                }

                 var id = identity.Claims.FirstOrDefault(w => w.Type == "id")?.Value;

                if (id == null)
                {
                    return new
                    {
                        success = false,
                        message = "Invalid Id",
                        result = ""
                    };
                }
                
                var usuario = _context.Usuarios.Where(w => w.Id.ToString() == id).FirstOrDefault();

                return new
                {
                    success = true,
                    message = "Usuario Eliminado",
                    result = usuario
                };

                /*
                var dVal = GetIdUserValidDB(id, context);
                if (dVal.ToString() == "true")
                {
                    var usuarioMessage = "Usuario eliminado con Id = " + user_.Id;
                    //var usuario = _context.Usuarios.Where(w => w.Id.ToString() == id).FirstOrDefault();
                    return new
                    {
                        success = true,
                        message = usuarioMessage,
                        result = user_
                    };
                }
                else
                {
                    var usuarioMessage = "Usuario no valido = " + user_.Id;
                    //var usuario = _context.Usuarios.Where(w => w.Id.ToString() == id).FirstOrDefault();
                    return new
                    {
                        success = true,
                        message = usuarioMessage,
                        result = user_
                    };
                }*/


            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                    result = ""
                };
            }

        }
    }
}
