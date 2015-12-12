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

    }
}