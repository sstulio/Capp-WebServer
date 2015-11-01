namespace CappWebServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Aluno")]
    public partial class Aluno
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlunoID { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoAluno { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
    }
}
