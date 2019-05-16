using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto.Models;

namespace WebApplication3.Models
{
    public class idiomasModel: AbstractModel<idiomas>
    {
        public List<idiomas> ListaIdiomas()
        {
            return ctx.idiomas.ToList();
        }

        
    }
}