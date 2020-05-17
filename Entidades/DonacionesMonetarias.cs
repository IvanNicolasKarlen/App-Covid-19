namespace WebCovid19
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DonacionesMonetarias
    {
        [Key]
        public int IdDonacionMonetaria { get; set; }

        public int IdNecesidadDonacionMonetaria { get; set; }

        public int IdUsuario { get; set; }
        
        [Required]
        public decimal Dinero { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el comprobante de pago")]
        [StringLength(200)]
        public string ArchivoTransferencia { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual NecesidadesDonacionesMonetarias NecesidadesDonacionesMonetarias { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
