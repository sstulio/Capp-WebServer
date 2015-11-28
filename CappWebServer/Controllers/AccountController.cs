using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CappWebServer.Models;

namespace CappWebServer.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login(string returnUrl)
        {

            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null) { 
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);
            }

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {
            using (CAppDataModel dc = new CAppDataModel())
            {
                var user = dc.Professor.Where(a => a.Email.Equals(l.Email) && a.Senha.Equals(l.Senha)).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.ProfessorID.ToString(), l.Lembrar);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Provas");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inválida combinação de email e senha");
                }
            }

            ModelState.Remove("Senha");

            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (CAppDataModel dc = new CAppDataModel())
                {
                    
                    Professor p = new Professor();
                    p.Nome = model.Nome;
                    p.Email = model.Email;
                    p.Senha = model.Senha;

                    //checking duplicate registration here 
                    var user = dc.Professor.Where(a => a.Email.Equals(p.Email)).FirstOrDefault();
                    if (user == null) { 
                        dc.Professor.Add(p);
                        dc.SaveChanges();

                        FormsAuthentication.SetAuthCookie(p.ProfessorID.ToString(), false);
                        return RedirectToAction("Index", "Provas");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Já existe um usuário com este endereço de email");
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}