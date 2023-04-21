using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Moxxii.Api.Data;
using Moxxii.Api.Models;
using Moxxii.Shared.Models;
using Moxxii.Shared.Entities;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Moxxii.Api.Controllers
{
    [ApiController]
    public class UsuarioControllers : ControllerBase
    {
        
        private DataContext _context;
        private static DataContext _contextStatic;
        private Task<dynamic> rtokent;

        public IConfiguration _configuration { get; set; }

        public UsuarioControllers(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _contextStatic = context;
            _configuration = configuration;
        }

        

        [HttpPost]
        [Route("/api/Usuario/token")]
        public async Task<dynamic> IniciaSessionToken([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string user = data.usuario.ToString();
            string password = data.password.ToString();

            var usuario = await _context.Usuarios.
                Where(w => w.Email == user || w.PhoneNumber == user).
                Where(y => y.Password == password).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Shared.Models.JwtConfig>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.Id.ToString()),
                new Claim("user_name", usuario.Name.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokent = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: singIn
                );
            return new
            {
                success = true,
                message = "Exito",
                result = new JwtSecurityTokenHandler().WriteToken(tokent)
            };
        }


        [HttpGet]
        [Route("/api/Usuario/Get")]
        public async Task<dynamic> GetUsuario()
        {
            try 
            {
                
                var us = await _context.Usuarios.ToListAsync();
                return Ok(us);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/Usuario/GetById")]
        [Authorize]
        public async Task<dynamic> GetByIdUsuario(int id)
        {
            try 
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rtokent = Models.JwtConfig.ValidarTokent(identity, _context);
                if (!rtokent.success)
                    return rtokent;
                
                var usu = await _context.Usuarios.
                    Where(x => x.Id == id).ToListAsync();
                return new
                {
                    success = true,
                    message = "exito",
                    result = usu.FirstOrDefault()
                };
            } catch (Exception ex)
            {
                return new
                {
                    success = true,
                    message = "Error",
                    result = BadRequest(ex.Message)
                };
            }
        }

        [HttpPost]
        [Route("/api/Usuario/Add")]
        public async Task<dynamic> PostUsuario(Usuario usuario)
        {
            try 
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return new
                {
                    success = true,
                    message = "exito",
                    result = usuario
                };
            } catch (Exception ex)
            {
                return new
                {
                    success = true,
                    message = "Error",
                    result = BadRequest(ex.Message)
                };
            }
        }

        [HttpPost]
        [Route("/api/Usuario/Remove")]
        [Authorize]
        public async Task<dynamic> DeleteUsuario(Usuario usuario) 
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                
                var rtokent = Models.JwtConfig.ValidarTokent(identity, _context);
                //var rtokent = ValidarTokent(identity);
                if (!rtokent.success) 
                    return rtokent;

                Usuario us = rtokent.result;
                if(us.Email!= "administradorMoxxii_LV@gmail.com")
                {
                    return new
                    {
                        success = false,
                        message = "No cuentas con permisos para eliminar dato",
                        result = ""
                    };
                }
                else
                {
                   // _context.Remove(usuario);
                    var result = await _context.Usuarios.FirstOrDefaultAsync(e => e.Id == usuario.Id);
                    _context.Usuarios.Remove(result);
                    await _context.SaveChangesAsync();
                    return new
                    {
                        success = false,
                        message = "Eliminado",
                        result = Ok()
                    };
                }
                
            }
            catch(Exception ex) 
            {
                return new
                {
                    sucess = false,
                    message = "Error: " + ex.Message,
                    result = ""
                };
            }
            
        }

        /*
        public static dynamic ValidarTokent(ClaimsIdentity identity)
        { 
            try
            {
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
                Usuario? usuario =  _contextStatic.Usuarios.Where(w => w.Id.ToString() == id).FirstOrDefault();
                if(usuario != null)
                {
                    return new
                    {
                        success = true,
                        message = "Usuario Eliminado",
                        result = usuario
                    };
                }
                else
                {
                    return new
                    {
                        success = true,
                        message = "Usuario sin permisos para esta acción",
                        result = usuario
                    };
                }
                

            }
            catch(Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Catch: " +ex.Message,
                    result = ""
                };
            }

        }


        */
    }
}
