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

        public ActionResult RecuperarContrasena()

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
                    user.password = pass1;
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