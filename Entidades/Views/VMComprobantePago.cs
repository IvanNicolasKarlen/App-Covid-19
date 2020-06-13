using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Views
{
    public class VMComprobantePago
    {
        [Required]
        public string ArchivoTransferencia { get; set; }

        public int IdDonacionMonetaria { get; set; }
    }
}
