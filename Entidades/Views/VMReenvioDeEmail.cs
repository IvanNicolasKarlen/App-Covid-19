using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Views
{
    public class VMReenvioDeEmail
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = " Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de Email erroneo")]
        [StringLength(50, ErrorMessage = "Email demasiado largo")]
        public string Email { get; set; }
    }
}
