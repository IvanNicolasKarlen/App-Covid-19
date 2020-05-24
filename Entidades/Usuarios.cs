namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            Denuncias = new HashSet<Denuncias>();
            DonacionesInsumos = new HashSet<DonacionesInsumos>();
            DonacionesMonetarias = new HashSet<DonacionesMonetarias>();
            Necesidades = new HashSet<Necesidades>();
            NecesidadesValoraciones = new HashSet<NecesidadesValoraciones>();
        }

        [Key]
        public int IdUsuario { get; set; }

       
        public string Nombre { get; set; }

      
        public string Apellido { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        
        public string Email { get; set; }


        public string Password { get; set; }

   

        [StringLength(100)]
        public string Foto { get; set; }

        public int TipoUsuario { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCracion { get; set; }

        public bool Activo { get; set; }

        
        [StringLength(50)]
        public string Token { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Denuncias> Denuncias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonacionesInsumos> DonacionesInsumos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonacionesMonetarias> DonacionesMonetarias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Necesidades> Necesidades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesValoraciones> NecesidadesValoraciones { get; set; }
    }
}
