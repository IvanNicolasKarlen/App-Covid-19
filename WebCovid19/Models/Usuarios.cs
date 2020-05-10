namespace WebCovid19
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage ="Fecha de nacimiento obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = " Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de Email erroneo")]
        [StringLength(50, ErrorMessage = "Email demaciado largo")]
        public string Email { get; set; }

        [Display(Name = "Clave")]
        [Required(ErrorMessage = " Contraseña es obligatoria")]
        //Puede comenzar con A-Z, o a-z, o numeros de 0 a 9    |  finaliza con a-z - A-Z -ó- 0-9   {desde, hasta}
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[a-zA-Z0-9]{1,}$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
        [MinLength(8, ErrorMessage = "Su contraseña debe tener 8 digitos como minimo")]
        public string Password { get; set; }

        [Display(Name = " Repita su clave")]
        [Required(ErrorMessage = " Repita su contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string RepeatPassword { get; set; }

        [StringLength(100)]
        public string Foto { get; set; }

        public int TipoUsuario { get; set; }

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
