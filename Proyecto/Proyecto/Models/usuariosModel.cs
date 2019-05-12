using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class usuariosModel : AbstractModel<usuarios>
    {
        public usuarios CheckLogin(string user, string password)
        {
            string pass = SecurityUtils.EncriptarSHA2(password);
            return ctx.usuarios.Where(u => u.nombre == user && u.password == pass).FirstOrDefault();
        }
<<<<<<< HEAD

       
=======
>>>>>>> dfe55257b573f520ceeb405d9a648213271ac91e
    }
}
   
