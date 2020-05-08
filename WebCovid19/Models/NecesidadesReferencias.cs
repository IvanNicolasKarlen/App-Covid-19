namespace WebCovid19
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NecesidadesReferencias
    {
        [Key]
        public int IdReferencia { get; set; }

        public int IdNecesidad { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefono { get; set; }

        public virtual Necesidades Necesidades { get; set; }
    }
}
