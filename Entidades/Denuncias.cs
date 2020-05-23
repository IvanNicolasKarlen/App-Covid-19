namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Denuncias
    {
        [Key]
        public int IdDenuncia { get; set; }

        public int IdNecesidad { get; set; }

        public int IdMotivo { get; set; }

        [Required]
        [StringLength(300)]
        public string Comentarios { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int Estado { get; set; }

        public virtual MotivoDenuncia MotivoDenuncia { get; set; }

        public virtual Necesidades Necesidades { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
