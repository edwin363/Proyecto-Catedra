
﻿    using Proyecto.Models;

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


            string rol = ((usuarios)HttpContext.Current.Session["user"]).tipos_usuarios.tipo_usuario;
            return (rol.Equals(UserRole));
         

        }
    }
}