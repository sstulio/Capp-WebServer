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
        public int RespostaID { get; set; }

        public int ProvaID { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoAluno { get; set; }

        public int Questao { get; set; }

        [Required]
        [StringLength(50)]
        public string Alternativa { get; set; }

        public byte isGabarito { get; set; }
    }
}
