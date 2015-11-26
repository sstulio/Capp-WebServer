using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CappWebServer.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "O que é CApp?";

            return View();
        }

        [Authorize] 
        public ActionResult Contact()
        {
            ViewBag.Message = "Informacoes para contato";

            return View();
        }
        
    }
}