using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class EmpleadosModel
    {
        public empleados getEmpleado(int id)
        {
            return ctx.empleados.Where(u => u.id_usuario == id).FirstOrDefault();
        }
    }
}