namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
