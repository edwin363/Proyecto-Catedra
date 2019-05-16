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

        public usuarios recuperarContra(string emailUsuario)
        {
            return ctx.usuarios.Where(u => u.email == emailUsuario).FirstOrDefault();
        }

        public usuarios validarCodigo(string codigo)
        {
            return ctx.usuarios.Where(u => u.codigo == codigo).FirstOrDefault();
        }

        
    }
}
   
