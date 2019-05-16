using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto.Models;

namespace Proyecto.Models
{
    public class cv_FormacionModel : AbstractModel<cv_form_academica>
    {
        public List<cv_form_academica> FormacionCandidato(int id)
        {
            return ctx.cv_form_academica.Where(u => u.id_curriculum == id).ToList();
        }

        public cv_form_academica ObjetoFormacion(int id, int idForm)
        {
            return ctx.cv_form_academica.Where(u => u.id_curriculum == id && u.id_form_academica == idForm).FirstOrDefault();
        }
    }
}