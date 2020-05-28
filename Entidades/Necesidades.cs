namespace Entidades
{
    using Entidades;
    using Entidades;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Necesidades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Necesidades()
        {
            Denuncias = new HashSet<Denuncias>();
            NecesidadesDonacionesInsumos = new HashSet<NecesidadesDonacionesInsumos>();
            NecesidadesDonacionesMonetarias = new HashSet<NecesidadesDonacionesMonetarias>();
            NecesidadesReferencias = new HashSet<NecesidadesReferencias>();
            NecesidadesValoraciones = new HashSet<NecesidadesValoraciones>();
        }

        [Key]
        public int IdNecesidad { get; set; }


        public string Nombre { get; set; }


        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaFin { get; set; }

 
        public string TelefonoContacto { get; set; }

        public int TipoDonacion { get; set; }

        public string Foto { get; set; }

        public int IdUsuarioCreador { get; set; }

        public int Estado { get; set; }

        public decimal? Valoracion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Denuncias> Denuncias { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesDonacionesInsumos> NecesidadesDonacionesInsumos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesDonacionesMonetarias> NecesidadesDonacionesMonetarias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesReferencias> NecesidadesReferencias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesValoraciones> NecesidadesValoraciones { get; set; }
    }
}
