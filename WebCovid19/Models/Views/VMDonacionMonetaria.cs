using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCovid19.Models.Views
{
    public class VMDonacionMonetaria
    {

        [Required(ErrorMessage = "Debe ingresar un monto a donar valido")]
        public decimal Dinero { get; set; }


    }
}