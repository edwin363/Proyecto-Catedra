<<<<<<< HEAD
﻿    using Proyecto.Models;
=======
﻿using Proyecto.Models;
>>>>>>> dfe55257b573f520ceeb405d9a648213271ac91e
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.utils
{
    public class AuthorizeUserLevel:AuthorizeAttribute
    {
        public string UserRole { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthenticated = base.AuthorizeCore(httpContext);
            if (!isAuthenticated)
            {
                return false;
            }
            string usuario = HttpContext.Current.User.Identity.Name;
            usuariosModel model = new usuariosModel();
<<<<<<< HEAD

            string rol = ((usuarios)HttpContext.Current.Session["user"]).tipos_usuarios.tipo_usuario;
            return (rol.Equals(UserRole));
         
=======
            
            string rol = ((usuarios)HttpContext.Current.Session["user"]).tipos_usuarios.tipo_usuario;
            return (rol.Equals(UserRole));
>>>>>>> dfe55257b573f520ceeb405d9a648213271ac91e
        }
    }
}