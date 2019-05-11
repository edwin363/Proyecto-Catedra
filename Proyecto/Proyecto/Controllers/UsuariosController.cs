using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyecto.Controllers
{
    public class UsuariosController : Controller
    {
        usuariosModel model = new usuariosModel();
        // GET: Usuarios
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(string usuario, string password, string returnURL)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                ModelState.AddModelError("usuario", "Debes ingresar un usuario");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("password", "Debes ingresar una contraseña");
            }
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            usuarios user = model.CheckLogin(usuario, password);
            if (user == null)
            {
                ModelState.AddModelError("usuario", "Usuario y/o contrasela incorrectos");
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(usuario, false);
                Session["usuario"] = user;
                if (returnURL == null)
                {
                    return Redirect("Home/Index");
                }

                return Redirect(returnURL);
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        [Route("logout", Name = "logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("login");
        }
    }
}