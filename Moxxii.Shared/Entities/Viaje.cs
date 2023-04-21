using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.Shared.Entities
{
    public class Viaje
    {
        #region Identificador
        public int Id { get; set; }

        public ICollection<Usuario> Usuario { get; set; }

        public ICollection<Rutas_Paradas> rutas_paradas { get; set; }

        #endregion

        #region PrecioViaje
        /*Este precio sera fijo por el momento*/
        [DisplayName("Precio total de servicio generado por paradas ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public double TripPriceTotalMoxxii { get; set; } = 0f!;
        #endregion

        #region ComentariosYFecha
        [DisplayName("Comentario de pasajero")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(550, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string CommentPass { get; set; } = null!;

        [DisplayName("Comentario de chofer")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(550, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string CommentDrive { get; set; } = null!;

        [DisplayName("Estatus de viaje")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool StatusTrip { get; set; }

        [DisplayName("Hora inicial")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayName("Hora inicial local")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [DisplayName("Hora final")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DisplayName("Hora final local")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateLocal => EndDate.ToLocalTime();

        [DisplayName("Informacion adicional")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Metadata { get; set; } = null!;
        #endregion

        #region Ubicación

        [DisplayName("Latitud inicial")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Double latInitial { get; set; } = 0f!;

        [DisplayName("Longitud inicial")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Double LongInitial { get; set; } = 0f!;

        [DisplayName("Latitud Final")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Double latEnd { get; set; } = 0f!;

        [DisplayName("Longitud Final")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Double LongEnd { get; set; } = 0f!;
        #endregion
    }
}
