using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moxxii.Api.Data;
using Moxxii.Shared.Entities;

namespace Moxxii.Api.Controllers
{
    [ApiController]
    [Route("/api/usuario")]
    public class UsuarioControllers : ControllerBase
    {
        private DataContext _context;

        public UsuarioControllers(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
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
    }
}
