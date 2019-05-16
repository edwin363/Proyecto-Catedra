using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto.Models;

namespace Proyecto.Models
{
    public class ofertasModel : AbstractModel<ofertas>
    {
        public List<ofertas> ofertasToUser(int id)
        {
            return ctx.ofertas.Where(u => u.id_empleado == id).ToList();
        }
    }
}