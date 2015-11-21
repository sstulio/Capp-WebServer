using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CappWebServer.Models
{
    public class ProvaViewModel
    {
        public IEnumerable<CappWebServer.Prova> listaProvas { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Quantidade de questões")]
        public byte QtdQuestoes { get; set; }

    }
}
