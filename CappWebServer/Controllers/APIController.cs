using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CappWebServer.Service;
using CappWebServer.Models;

namespace CappWebServer.Controllers
{
    public class APIController : Controller
    {
        CAppService service;

        public APIController()
        {
            service = new CAppService();
        }

        // GET: API
        public ActionResult PegarProva(string codigo)
        {
            Prova p = service.GetProva(codigo);

            if (p != null) { 
                ProvaWrapper wrapper = new ProvaWrapper();
                wrapper.ID = p.ProvaID;
                wrapper.Nome = p.Nome;
                wrapper.QtdQuestoes = p.QtdQuestoes;

                return Json(wrapper, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: API
        public ActionResult Responder(int provaID, string aluno, string respostas)
        {
            bool result = false;
            for (int i = 0; i < respostas.Length; i += 2)
            {
                Resposta resposta = new Resposta();
                resposta.ProvaID = provaID;
                resposta.CodigoAluno = aluno;
                resposta.isGabarito = 0;

                resposta.Questao = respostas[i] - 48; //48 = '0' na tabela ASCII
                resposta.Alternativa = respostas[i + 1].ToString();

                result = service.InserirResposta(resposta);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}