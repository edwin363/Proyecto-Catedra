using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult enviarCorreo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult enviarCorreo(string emailDestino, string codigo)
        {
            validarUsuario(emailDestino,codigo);
            return RedirectToAction("Index");
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
            cliente.Credentials = new NetworkCredential("bolsatrabajo76@gmail.com","bolsa1234");
            cliente.Port = 25;
            cliente.Host = "smtp.gmail.com";
            cliente.EnableSsl = true;
            try
            {
                cliente.Send(msg);
            }
            catch(SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}