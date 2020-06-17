using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Views
{
    public class VMNecesidadesDonacionesInsumos
    {
        [Range(0, 3500, ErrorMessage = "Puede donar hasta 3500 insumos")]
        public int Cantidad { get; set; }

        public int IdNecesidadDonacionInsumo { get; set; }
    }
}