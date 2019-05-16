using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto
{
    public class curriculumModel : AbstractModel<curriculum>
    {
        public curriculum idCurriculum(int id)
        {
            return ctx.curriculum.Where(u => u.id_usuario == id).FirstOrDefault();
        }
    }
}