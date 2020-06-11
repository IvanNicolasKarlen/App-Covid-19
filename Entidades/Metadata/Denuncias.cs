using Entidades.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [MetadataType(typeof(DenunciasMetadata))]
    public partial class Denuncias
    {
       
    }

    public class DenunciasMetadata
    {
        [Required]
        public int IdMotivo { get; set; }

        [Required]
        public string Comentarios { get; set; }
        public TipoEstadoDenuncia Estado;
    }
}
