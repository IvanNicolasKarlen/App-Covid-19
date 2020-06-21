using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class DonacionesInsumosVM
    {
        public string NombreNecesidadInsumos { get; set; }
        public int TotalRecaudado { get; set; }
        public DonacionesInsumosVM()
        {
            this.TotalRecaudado = 0;
        }
        
    }
}
