using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class cv_referenciaModel : AbstractModel<cv_ref_profesionales>
    {
        public List<cv_ref_profesionales> ListaR(int id)
        {
            return ctx.cv_ref_profesionales.Where(u => u.id_curriculum == id).ToList();
        }

        public cv_ref_profesionales ObjetoR(int id, int referencia)
        {
            return ctx.cv_ref_profesionales.Where(u => u.id_curriculum == id && u.id_referencia == referencia).FirstOrDefault();
        }
    }
}