using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enum;

namespace Entidades.Metadata
{
   public class NecesidadesMetadata
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(30)]
        public string TelefonoContacto { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [Required]
        [StringLength(100)]
        public string Foto { get; set; }

        [Required]
        public int IdUsuarioCreador { get; set; }

        [Required]
        public TipoDonacion TipoDonacion { get; set; }
    }
}
