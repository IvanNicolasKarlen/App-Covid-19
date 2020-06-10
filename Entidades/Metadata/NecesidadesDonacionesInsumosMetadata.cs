using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Metadata
{
    public class NecesidadesDonacionesInsumosMetadata
    {
        public int IdNecesidadDonacionInsumo { get; set; }
        [Required]
        public int IdNecesidad { get; set; }
        [Required(ErrorMessage = "El nombre del insumo es obligatorio")]
        public string Nombre { get; set; }
        [Required (ErrorMessage = "Debe añadir una cantidad")]
        public int Cantidad { get; set; }

        public virtual ICollection<DonacionesInsumos> DonacionesInsumos { get; set; }
        public virtual Necesidades Necesidades { get; set; }
    }
}
