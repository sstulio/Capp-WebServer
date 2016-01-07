using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CappWebServer.Service
{
    public class CAppService
    {

        public Prova GetProva(string codigo)
        {
            using (CAppDataModel dc = new CAppDataModel())
            {
                Prova prova = dc.Prova.Where(p => p.CodigoProva.Equals(codigo)).FirstOrDefault();
                return prova;
            }
        }

        public bool InserirResposta(Resposta resposta)
        {
            using (CAppDataModel dc = new CAppDataModel())
            {
                var r = dc.Resposta.Where(a => a.ProvaID.Equals(resposta.ProvaID) && a.Questao.Equals(resposta.Questao) && a.CodigoAluno.Equals(resposta.CodigoAluno)).FirstOrDefault();
                if (r == null)
                {
                    dc.Resposta.Add(resposta);
                    dc.SaveChanges();
                    return true;
                }
            }

            return false;
        }

    }
}