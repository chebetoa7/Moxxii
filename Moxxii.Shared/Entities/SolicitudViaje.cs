using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.Shared.Entities
{
    public class SolicitudViaje
    {
        #region Identificador
        public int Id { get; set; }
        public int IdPasajero { get; set; }
        public int? IdConductor { get; set; }

        #endregion

        #region Ubicación
        [DisplayName("Latitud original")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Double? latInitial { get; set; } = 0f;

        [DisplayName("Longitud original")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Double? longInitial { get; set; } = 0f;

        [DisplayName("Latitud Destino")]
        public Double? latEnd { get; set; } = 0f;

        [DisplayName("Longitud Destino")]
        public Double? longEnd { get; set; } = 0f;
        #endregion

        #region Zone
        [DisplayName("Ciudad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string City { get; set; } = null!;

        [DisplayName("Colonia/Distrito")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Dictric { get; set; } = null!;
        #endregion

        #region Config
        [DisplayName("Estatus de solicitud Abierto o cerrado (true/false)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool status { get; set; }

        [DisplayName("Confirmado rechasado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string ConfirmationStatus { get; set; } = null!;

        
        #endregion
    }
}
