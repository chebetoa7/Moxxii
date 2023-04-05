using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.Shared.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage ="El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null!;

        [DisplayName("Apellido paterno")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string LastName { get; set; } = null!;
        public int OtherName { get; set; }
        
    }
}
