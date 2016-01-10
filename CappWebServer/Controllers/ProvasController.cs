using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CappWebServer;
using CappWebServer.Models;
using System.Collections.Generic;

namespace CappWebServer.Controllers
{
    public class ProvasController : Controller
    {
        private CAppDataModel db = new CAppDataModel();

        // GET: Provas
        [Authorize]
        public ActionResult Index()
        {
            int professorId = int.Parse(User.Identity.Name);
            var prova = db.Prova.Where(p => p.ProfessorID == professorId);
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

        // GET: Provas/EditarGabarito/5
        [Authorize]
        public ActionResult EditarGabarito(int? id)
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

            ViewBag.Nome = prova.Nome;
            ViewBag.QtdQuestao = prova.QtdQuestoes;

            EditarGabaritoViewModel model = new EditarGabaritoViewModel();

            model.ListaResposta = new System.Collections.Generic.List<Resposta>();

            using (CAppDataModel dc = new CAppDataModel())
            {
                var respostas = dc.Resposta.Where(re => re.ProvaID.Equals(prova.ProvaID) && re.isGabarito.Equals(1));
                model.ListaResposta.AddRange(respostas);

                for (int i = respostas.Count(); i < prova.QtdQuestoes; i++)
                {
                    Resposta r = new Resposta();
                    r.Alternativa = "A";
                    r.CodigoAluno = prova.ProfessorID.ToString();
                    r.ProvaID = prova.ProvaID;
                    r.isGabarito = 1;
                    r.Questao = i + 1;

                    model.ListaResposta.Add(r);
                }

            }

            return View(model);
        }

        // POST: Provas/EditarGabarito/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarGabarito(EditarGabaritoViewModel model)
        {
            if (ModelState.IsValid) { 
                using (CAppDataModel dc = new CAppDataModel())
                {
                    foreach (Resposta r in model.ListaResposta)
                    {
                        var resposta = dc.Resposta.Where(re => re.ProvaID.Equals(r.ProvaID) && re.Questao.Equals(r.Questao)).FirstOrDefault();
                        if (resposta != null)
                        {
                            resposta.Alternativa = r.Alternativa;
                            resposta.isGabarito = 1;
                            UpdateModel(resposta);
                        }
                        else
                        {
                            dc.Resposta.Add(r);
                        }
                    }
                    dc.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Provas/Resultados/5
        [Authorize]
        public ActionResult Resultados(int? id)
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

            ViewBag.ProvaID = prova.ProvaID;

            ResultadosViewModel model = new ResultadosViewModel();

            model.Prova = prova;
            model.ListaResultado = new System.Collections.Generic.List<Resultado>();

            using (CAppDataModel dc = new CAppDataModel())
            {
                List<string> alunos = dc.Resposta.Where(re => re.ProvaID.Equals(prova.ProvaID) && re.isGabarito.Equals(0)).Select(m => m.CodigoAluno).Distinct().ToList();
                List<Resposta> gabarito = dc.Resposta.Where(re => re.ProvaID.Equals(prova.ProvaID) && re.isGabarito.Equals(1)).ToList();

                for (int i = 0; i < alunos.Count; i++)
                {
                    string aluno = alunos.ElementAt(i);
                    int acertos = 0;
                    int totalDeQuestoes = gabarito.Count;

                    List<Resposta> respostaAluno = dc.Resposta.Where(re => re.ProvaID.Equals(prova.ProvaID) && re.CodigoAluno.Equals(aluno)).ToList();
                    for (int j = 0; j < totalDeQuestoes; j++)
                    {
                        if (gabarito.ElementAt(j).Alternativa == respostaAluno.ElementAt(j).Alternativa)
                        {
                            acertos++;
                        }
                    }

                    Resultado resultado = new Resultado();
                    resultado.Aluno = aluno;
                    resultado.Nota = (10 * acertos) / totalDeQuestoes;
                    model.ListaResultado.Add(resultado);
                }
            }
                
            

            return View(model);
        }

        // GET: Provas/RespostaAluno/5
        [Authorize]
        public ActionResult RespostaAluno(int? provaID, string alunoID)
        {
            if (provaID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prova prova = db.Prova.Find(provaID);
            if (prova == null)
            {
                return HttpNotFound();
            }

            RespostaAlunoViewModel model = new RespostaAlunoViewModel();

            model.Prova = prova;
            ViewBag.AlunoID = alunoID;

            using (CAppDataModel dc = new CAppDataModel())
            {
                List<Resposta> resposta = dc.Resposta.Where(re => re.ProvaID.Equals(prova.ProvaID) && re.isGabarito.Equals(0) && re.CodigoAluno.Equals(alunoID)).ToList();
                List<Resposta> gabarito = dc.Resposta.Where(re => re.ProvaID.Equals(prova.ProvaID) && re.isGabarito.Equals(1)).ToList();

                model.RespostaAluno = resposta;
                model.RespostaGabarito = gabarito;
            }



            return View(model);
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
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string chars = "ABCDE0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
