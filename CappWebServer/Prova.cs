namespace CappWebServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prova")]
    public partial class Prova
    {
        public int ProvaID { get; set; }

        [StringLength(50)]
        public string CodigoProva { get; set; }

        public int ProfessorID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public int QtdQuestoes { get; set; }

        [StringLength(50)]
        public string DataCriada { get; set; }
    }
}
