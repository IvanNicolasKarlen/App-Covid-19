using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMReferencias
    {
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre del primer contacto")]
        public string Nombre1 { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Telefono del primer contacto")]
        public string Telefono1 { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre del segundo contacto")]
        public string Nombre2 { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Telefono del segundo contacto")]
        public string Telefono2 { get; set; }

        public int IdNecesidad { get; set; }

        public Necesidades Necesidades { get; set; }

    }
}
