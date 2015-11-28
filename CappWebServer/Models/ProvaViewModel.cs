using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        public int QtdQuestoes { get; set; }

    }

    public class EditarGabaritoViewModel
    {
        public List<CappWebServer.Resposta> ListaResposta { get; set; }

       // public IEnumerable<SelectListItem> Alternativas { get; set; }       

    }

    /* public enum Alternativas
     {
         A, B, C, D, E
     }*/
}
