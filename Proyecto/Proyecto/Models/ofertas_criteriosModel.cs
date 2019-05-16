using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto
{
    public class ofertas_criteriosModel : AbstractModel<ofertas_criterios>
    {
        //public List<ofertas_criterios> ofertasPorcentajeSum(int id)
        //{
        //    List<ofertas_criterios> listItems = new List<ofertas_criterios>();
        //    var items = (from db in ctx.ofertas_criterios group db db.)
        //}
        public List<ofertas_criterios> ofertasCriterios(int id)
        {
            return ctx.ofertas_criterios.Where(u => u.id_oferta == id).ToList();
        }
    }
}