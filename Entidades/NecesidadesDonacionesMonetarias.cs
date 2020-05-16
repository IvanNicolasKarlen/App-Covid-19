namespace WebCovid19
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NecesidadesDonacionesMonetarias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NecesidadesDonacionesMonetarias()
        {
            DonacionesMonetarias = new HashSet<DonacionesMonetarias>();
        }

        [Key]
        public int IdNecesidadDonacionMonetaria { get; set; }

        public int IdNecesidad { get; set; }

        public decimal Dinero { get; set; }

        [Required]
        [StringLength(80)]
        public string CBU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonacionesMonetarias> DonacionesMonetarias { get; set; }

        public virtual Necesidades Necesidades { get; set; }
    }
}
