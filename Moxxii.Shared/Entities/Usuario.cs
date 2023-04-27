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
        #region Identificador
        public int Id { get; set; }

        [DisplayName("Disponibilidad de usuario")]
        public string Disponibility { get; set; }

        public ICollection<Viaje>? Viajes { get; set; } = null;
        #endregion

        #region DataPersonal
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage ="El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null!;

        [DisplayName("Apellido paterno")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string LastName { get; set; } = null!;

        [DisplayName("Apellido materno")]
        public string? OtherLastName { get; set; } 

        [DisplayName("Fecha de cumpleaños")]
        public string? Birthdate { get; set; } 
        

        [DisplayName("Edad")]
        public int? DateAge { get; set; }

        [DisplayName("Sexo")]
        public string? Sex { get; set; }

        [DisplayName("Dirección")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(120, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Address { get; set; } = null!;

        [DisplayName("Pais")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Country { get; set; } = null!;

        [DisplayName("Ciudad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string City { get; set; } = null!;

        [DisplayName("Colonia o distrito")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Distric { get; set; } = null!;
        #endregion

        #region ConfigData
        [DisplayName("Ubicación en tiempo real lat")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public double UbicationRealLat {get; set; } = 0f;

        [DisplayName("Ubicación en tiempo real lon")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public double UbicationRealLon {get; set; } = 0f;

        [DisplayName("Tipo de usuario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string TypeUser {get; set; } = null!;

        [DisplayName("Estado de la tabla")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Status {get; set; }

        [DisplayName("Informacion adicional")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Metadata { get; set; } = null!;
        #endregion

        #region AccessData

        [DisplayName("Telefono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string PhoneNumber { get; set; } = null!;

        [DisplayName("Correo Electrónico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Email { get; set; } = null!;

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Password { get; set; } = null!;
        #endregion
        
    }
}
