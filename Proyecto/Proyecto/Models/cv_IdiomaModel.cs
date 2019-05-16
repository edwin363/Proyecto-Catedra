using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto.Models;

namespace Proyecto.Models
{
    public class cv_IdiomaModel : AbstractModel<cv_idiomas>
    {
        public List<cv_idiomas> idiomasCandidato(int id)
        {
            return ctx.cv_idiomas.Where(u => u.id_curriculum == id).ToList();
        }

        public cv_idiomas idIdioma(int idioma, int cv)
        {
            return ctx.cv_idiomas.Where(u => u.id_idioma == idioma && u.id_curriculum == cv).FirstOrDefault();
        }

        public cv_idiomas id_cv_idioma(int id)
        {
            return ctx.cv_idiomas.Where(u => u.id_cv_idiomas == id).FirstOrDefault();
        }

    }
}