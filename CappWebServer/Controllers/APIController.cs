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
        public ActionResult GetProva(string codigo)
        {
            Prova p = service.GetProva(codigo);

            ProvaWrapper wrapper = new ProvaWrapper();
            wrapper.Nome = p.Nome;
            wrapper.QtdQuestoes = p.QtdQuestoes;

            return Json(wrapper, JsonRequestBehavior.AllowGet);
        }
    }
}