namespace WebCovid19
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DonacionesInsumos
    {
        [Key]
        public int IdDonacionInsumo { get; set; }

        public int IdNecesidadDonacionInsumo { get; set; }

        public int IdUsuario { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public virtual NecesidadesDonacionesInsumos NecesidadesDonacionesInsumos { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
