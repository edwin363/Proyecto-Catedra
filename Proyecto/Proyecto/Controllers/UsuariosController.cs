using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

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
        public ActionResult VerEmpleados(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //paginacion
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //empleados que pertenecen al id actual
            //   var empleadosbk = ctx.usuarios.Join(ctx.empleados, usu => usu.id_usuario,
            //     emp => emp.id_usuario, (usu, emp) => new { usu, emp }).First(x => x.emp.id_institucion == Convert.ToInt32(Session["idIn"]));

            var students = from s in ctx.usuarios
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.nombre.Contains(searchString)
                                       || s.apellido1.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.nombre);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.apellido1);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.apellido2);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.email);
                    break;
            }


            //no se como ponerlo en la vista
            ///return View(empleadosbk.ToList());
            //paginacion
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));

            //return View(model.List());
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
                //id del usuario
                var idActual = ctx.usuarios.Where(x => x.nombre.Equals(usuario))
                       .FirstOrDefault().id_usuario;
                //id de la institucion
                Session["idIn"] = ctx.empleados.Where(x => x.id_usuario == idActual)
                       .FirstOrDefault().id_institucion;
                //nombre
                Session["nameIn"] = ctx.empleados.Where(x => x.id_usuario == idActual)
                       .FirstOrDefault().instituciones.nombre;

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

        public ActionResult RecuperarContrasena()

        {
            return View();
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




            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Insert(empleado) > 0)
                    {
                        //asignando empleado a la institucion actual
                        //obtenemos el id anterios
                        asignando.id_usuario = ctx.usuarios.OrderByDescending(x => x.id_usuario).First().id_usuario;
                        //obteniendo el id del admin logueado
                        asignando.id_institucion = Convert.ToInt32(Session["idIn"]);
                        if (modelemp.Insert(asignando) > 0)
                        {

                            TempData["successMessage"] = "Empleado ingresado con exito";
                            return View("verempleados");
                        }
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
            //id de usuario en empleados
            var idemp = ctx.empleados.Where(x => x.id_usuario == id)
                       .FirstOrDefault().id_empleado;


            if (tipo == null)
            {
                TempData["errorMessage"] = "No existe este empleado";
                return RedirectToAction("verempleados");
            }
            if (modelemp.Remove(idemp) > 0 && model.Remove(id) > 0)
            {
                TempData["succesMessage"] = " Empleado eliminado";
                return RedirectToAction("verempleados");
            }
            else
            {
                //las relaciones no dejan
                TempData["errorMessage"] = "No se puede eliminar este empleado";
            }
            return RedirectToAction("verempleados");
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



        [HttpPost]
        public ActionResult RecuperarContrasena(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("email", "Debes ingresar el email");
            }
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            usuarios user = model.recuperarContra(email);
            if (user == null)
            {
                ModelState.AddModelError("email", "El email que ha ingresado no existe en nuestra base de datos");
                return View(ModelState);
            }
            else
            {
                ModelState.AddModelError("email", "Correo enviado");
                recuperarContra(email);
                return View("login",ModelState);
            }
        }

        [HttpPost]
        public ActionResult Recuperar(string correo)
        {
            ViewBag.correo = correo;
            return View();
        }

        [HttpPost]
        public ActionResult AsignarContra(string email, string pass1, string pass2)
        {
            if (String.IsNullOrWhiteSpace(pass1))
            {
                ModelState.AddModelError("password", "Debes ingresar una nueva contraseña");
            }
            if (String.IsNullOrWhiteSpace(pass2))
            {
                ModelState.AddModelError("password", "Debes ingresar la confirmacion de la contraseña");
            }
            if (pass1 != pass2)
            {
                ModelState.AddModelError("password", "La contraseña de confirmación no coinciden con la nueva contraseña ingresada");
            }
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            
            usuarios user = model.recuperarContra(email);
            if (user == null)
            {
                //Si llega a este punto es porq la validacion de RecuperarContrasena lo dejo pasar
                ModelState.AddModelError("email", "El email que ha ingresado no existe en nuestra base de datos");
                return View(ModelState);
            }
            else
            {
                try
                {
                    user.password = SecurityUtils.EncriptarSHA2(pass1);
                    if (model.Update(user, user.id_usuario) > 0)
                    {
                        ModelState.AddModelError("email", "Su contraseña ha sido restablecida");
                        return View("login", ModelState);
                    }
                    ModelState.AddModelError("email", "ERROR");
                    return View("login", ModelState);
                }
                catch
                {
                    ModelState.AddModelError("email", "ERROR 2");
                    return View("login", ModelState);
                }
                
            }
        }

        public void recuperarContra(string emailDestino)
        {
            string asunto = "Recuperar Contraseña";

            MailMessage msg = new MailMessage();
            msg.To.Add(emailDestino);

            msg.From = new MailAddress("bolsatrabajo76@gmail.com", "Bolsa de Trabajo SV", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = string.Format("<h2>Bolsa de Trabajo</h2>" +
                "<p>Si quieres recuperar la contraseña de click en el boton recuperar.</p>" +
                "<br>" +
                "<form action='http://localhost:51489/Usuarios/Recuperar' method='POST'>" +
                "<input type='hidden' value='{0}' id='correo' name='correo' />" +
                "<button type='submit'>Recuperar</button>" +
                "" +
                "</form>" +
                "<br>" +
                "<p>Si no has solicitado esta recuperacion, ignorar este mensaje.</p>", emailDestino);
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            SmtpClient cliente = new SmtpClient();
            cliente.Credentials = new NetworkCredential("bolsatrabajo76@gmail.com", "bolsa1234");
            cliente.Port = 25;
            cliente.Host = "smtp.gmail.com";
            cliente.EnableSsl = true;
            try
            {
                cliente.Send(msg);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }

    
}