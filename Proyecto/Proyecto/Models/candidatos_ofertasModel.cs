using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto
{
    public class candidatos_ofertasModel : AbstractModel<candidatos_ofertas>
    {
        public List<candidatos_ofertas> getCandiOfer(int id)
        {
            return ctx.candidatos_ofertas.Where(u => u.id_oferta == id).ToList();
        }
    }
}