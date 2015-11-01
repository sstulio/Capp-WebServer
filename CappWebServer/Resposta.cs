namespace CappWebServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resposta")]
    public partial class Resposta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RespostaID { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoProva { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoAluno { get; set; }

        public byte Questao { get; set; }

        [Required]
        [StringLength(50)]
        public string Alternativa { get; set; }
    }
}
