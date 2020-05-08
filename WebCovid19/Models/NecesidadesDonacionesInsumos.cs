namespace WebCovid19
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NecesidadesDonacionesInsumos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NecesidadesDonacionesInsumos()
        {
            DonacionesInsumos = new HashSet<DonacionesInsumos>();
        }

        [Key]
        public int IdNecesidadDonacionInsumo { get; set; }

        public int IdNecesidad { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public int Cantidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonacionesInsumos> DonacionesInsumos { get; set; }

        public virtual Necesidades Necesidades { get; set; }
    }
}
