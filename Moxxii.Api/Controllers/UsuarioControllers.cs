using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Moxxii.Api.Data;
using Moxxii.Shared.Entities;
using Moxxii.Shared.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Moxxii.Api.Controllers
{
    [ApiController]
    [Route("/api/usuario")]
    public class UsuarioControllers : ControllerBase
    {
        private DataContext _context;
        public IConfiguration _configuration { get; set; }

        public UsuarioControllers(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/Get")]
        public async Task<IActionResult> GetUsuario()
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
        [HttpPost]
        [Route("/Add")]
        public async  Task<ActionResult> PostUsuario(Usuario usuario)
        {
            try 
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/Login")]
        public async Task<dynamic> IniciaSession([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string user = data.usuario.ToString();
            string password = data.password.ToString();

            var usuario = await _context.Usuarios.
                Where(w => w.Email == user || w.PhoneNumber == user).
                Where(y => y.Password == password).FirstOrDefaultAsync();
            if(usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<JwtConfig>();

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
                success = false,
                message = "Exito",
                result = new JwtSecurityTokenHandler().WriteToken(tokent)
            };  
        }
    }
}
