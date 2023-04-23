using Moxxii.Shared.Entities;

namespace Moxxii.Api.Models
{
    public partial class UsuarioValido
    {
        public Usuario usuario {get; set;}
        public string token {get; set;} 
    }
}
