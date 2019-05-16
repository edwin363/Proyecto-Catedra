using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class cv_ExpModel : AbstractModel<cv_exp_laboral>
    {
        public List<cv_exp_laboral> ListaExp(int id)
        {
            return ctx.cv_exp_laboral.Where(u => u.id_curriculum == id).ToList();
        }

        public cv_exp_laboral ObjetoExp(int id, int idForm)
        {
            return ctx.cv_exp_laboral.Where(u => u.id_curriculum == id && u.id_exp_profesional == idForm).FirstOrDefault();
        }
    }
}