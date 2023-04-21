using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.Shared.Entities
{
    public class Promociones
    {
        #region Identificador
        public int Id { get; set; }

        public Rutas_Paradas? ruta_parada { get; set; }

        #endregion

        #region ConfigPromotion
        [DisplayName("Cantidad de descuento de promoción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public double PromotionPrice { get; set; } = 0f!;

        [DisplayName("Nombre de promoción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(120, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null!;

        [DisplayName("Descripción de promoción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(120, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Descrition { get; set; } = null!;
        #endregion
    }
}
