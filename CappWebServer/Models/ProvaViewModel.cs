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
    }

    public class ResultadosViewModel
    {
        public Prova Prova { get; set; }
        public List<Resultado> ListaResultado { get; set; }  
    }

    public class Resultado
    {
        public string Aluno { get; set; }
        public int Nota { get; set; }  
    }
}
