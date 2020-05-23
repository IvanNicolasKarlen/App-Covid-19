using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades.Views
{
    public class VMDonacionMonetaria
    {
        [Required(ErrorMessage = "Debe ingresar un monto a donar valido")]
        public decimal Dinero { get; set; }
        public string Foto { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }
        public string NombreSignificativoImagen
        {
            get
            {
                //en caso de ambos null, devuelve "ApellidoNombre"
                return string.Format("{0}{1}", this.apellido ?? "Apellido", this.nombre ?? "Nombre");
                
            }
        }

    }
}