using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CappWebServer;
using CappWebServer.Models;

namespace CappWebServer.Controllers
{
    public class ProvasController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Provas
        [Authorize]
        public ActionResult Index()
        {
            var prova = db.Prova.Include(p => p.Professor);
            ProvaViewModel model = new ProvaViewModel();
            model.listaProvas = prova.ToList();
            return View(model);
        }

        // POST: Provas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ProvaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Prova prova = new Prova();

                prova.Nome = model.Nome;
                prova.QtdQuestoes = model.QtdQuestoes;
                prova.ProfessorID = int.Parse(User.Identity.Name);
                prova.DataCriada = DateTime.Now.ToString();
                prova.CodigoProva = "TEMP";

                db.Prova.Add(prova);
                db.SaveChanges();

                String codigoGerado = RandomString(3) + prova.ProvaID.ToString() + RandomString(3);
                prova.CodigoProva = codigoGerado;

                UpdateModel(prova);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Provas/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prova prova = db.Prova.Find(id);
            if (prova == null)
            {
                return HttpNotFound();
            }
            return View(prova);
        }

        // GET: Provas/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProfessorID = new SelectList(db.Professor, "ProfessorID", "Nome");
            return View();
        }

        // GET: Provas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prova prova = db.Prova.Find(id);
            if (prova == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorID = new SelectList(db.Professor, "ProfessorID", "Nome", prova.ProfessorID);
            return View(prova);
        }

        // POST: Provas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProvaID,CodigoProva,ProfessorID,Nome,QtdQuestoes,DataCriada")] Prova prova)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prova).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorID = new SelectList(db.Professor, "ProfessorID", "Nome", prova.ProfessorID);
            return View(prova);
        }

        // GET: Provas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prova prova = db.Prova.Find(id);
            if (prova == null)
            {
                return HttpNotFound();
            }
            return View(prova);
        }

        // POST: Provas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Prova prova = db.Prova.Find(id);
            db.Prova.Remove(prova);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
