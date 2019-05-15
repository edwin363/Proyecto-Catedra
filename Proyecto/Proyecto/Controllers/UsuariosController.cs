using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyecto.Controllers
{
    public class UsuariosController : Controller
    {
        usuariosModel model = new usuariosModel();
        empleadosModel modelemp = new empleadosModel();
        bolsaTrabajoEntities1 ctx = new bolsaTrabajoEntities1();

        // GET: Usuarios
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        // GET: Empleados
        [Route("verempleados")]
        public ActionResult VerEmpleados()
        {
            //empleados que pertenecen a bk
            var empleadosbk = ctx.usuarios.Join(ctx.empleados, usu => usu.id_usuario,
                emp => emp.id_usuario, (usu, emp) => new { usu, emp }).FirstOrDefault(x => x.emp.id_institucion == 1);

            return View(model.List());
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

        // GET: empleados/create
        public ActionResult Create()
        {
            return View();
        }

        // POST: empleados/Create
        [HttpPost]
        public ActionResult Create(usuarios empleado, empleados asignando)
        {

            empleado.id_tipo_usuario = 3;//tipo empleado
            empleado.password = (SecurityUtils.EncriptarSHA2(empleado.password));//emcriptando contraseña
            empleado.estado = 1;
            empleado.codigo = "default";

            //asignando empleado a la institucion actual
            asignando.id_usuario = empleado.id_usuario;
            asignando.id_institucion = 1;//insitucion actual

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Insert(empleado) > 0)
                    {
                        modelemp.Insert(asignando);
                        TempData["successMessage"] = "Candidato Ingresado con exito";
                        return RedirectToAction("verempleados");
                    }
                    TempData["successMessage"] = "Error al ingresar el candidato";
                }
                return View(empleado);

            }
            catch
            {
                return View();
            }
        }

        // GET: Cempleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuarios tipo = model.GetById(id);
            if (tipo == null)
            {
                TempData["errorMessage"] = "No existe este empleado";
                return RedirectToAction("Index");
            }
            if (model.Remove(id) > 0)
            {
                TempData["succesMessage"] = " Empleado eliminado";
            }
            else
            {
                //las relaciones no dejan
                TempData["errorMessage"] = "No se puede eliminar este cliente";
            }
            return RedirectToAction("Index");
        }

        // POST: empleado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

    
}