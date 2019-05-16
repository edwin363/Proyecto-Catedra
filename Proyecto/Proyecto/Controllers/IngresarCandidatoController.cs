using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.utils;
using System.Net.Mail;
using System.Net;

namespace Proyecto.Controllers
{
    public class IngresarCandidatoController : Controller
    {


        usuariosModel model = new usuariosModel();

        // GET: IngresarCandidato
        public ActionResult Index()
        {
            return View();
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(usuarios candidato)
        {

            string c1, c2;
            candidato.id_tipo_usuario = 4;//tipo candidato
            candidato.password = (SecurityUtils.EncriptarSHA2(candidato.password));//emcriptando contraseña
            candidato.estado = 0;//estado no veirficado
            //generando codigo
            c1 = candidato.apellido1.Substring(0, 1);
            c2 = candidato.apellido2.Substring(0, 1);

            System.Random randomGenerate = new System.Random();
            System.String sPassword = "";
            sPassword = System.Convert.ToString(randomGenerate.Next(00000001, 99999999));
            sPassword.Substring(sPassword.Length - 8, 8);

            //agregando codigo
            candidato.codigo = c1 + c2 + sPassword.Substring(sPassword.Length - 8, 8);

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Insert(candidato) > 0)
                    {
                        validarUsuario(candidato.email, candidato.codigo);
                        TempData["successMessage"] = "Verifique su correo para validar su registro";
                        return RedirectToAction("Validar");
                    }
                    TempData["successMessage"] = "Error al ingresar el candidato";
                }
                return View(candidato);
            }
            catch
            {
                return View();
            }
        }

        //Ruta para ingresar el codigo de validacion de cuenta
        public ActionResult Validar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validar(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                ModelState.AddModelError("email", "Debes ingresar el codigo");
            }
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            usuarios user = model.validarCodigo(codigo);
            if (user == null)
            {
                ModelState.AddModelError("email", "Código de validación incorrecto");
                return View(ModelState);
            }
            else
            {
                try
                {
                    user.estado = 1;
                    if (model.Update(user, user.id_usuario) > 0)
                    {
                        ModelState.AddModelError("email", "Su cuenta ha sido validada");
                        return View("Validar", ModelState);
                    }
                    ModelState.AddModelError("email", "ERROR");
                    return View("Validar", ModelState);
                }
                catch
                {
                    ModelState.AddModelError("email", "ERROR 2");
                    return View("Validar", ModelState);
                }
            }
        }

        public void validarUsuario(string emailDestino, string codigo)
        {
            string asunto = "Validar registro de usuario";

            MailMessage msg = new MailMessage();
            msg.To.Add(emailDestino);

            msg.From = new MailAddress("bolsatrabajo76@gmail.com", "Bolsa de Trabajo SV", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = string.Format("<h2>Bolsa de Trabajo</h2>" +
                "<p>Te damos la bienvenida a Bolsa de Trabajo SV, ahora podras disfrutar de los diferentes beneficios que te ofrece.</p>" +
                "Ingrese este codigo para validar su cuenta en Bolsa de Trabajo: <b>{0}</b>" +
                "<br>" +
                "<p>Si no has solicitado este codigo, ignorar este mensaje.</p>", codigo);
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