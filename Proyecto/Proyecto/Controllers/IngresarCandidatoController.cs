using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.utils;

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
            //variables del codigo
            string c1="";
            string c2="";

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
            candidato.codigo = c1+c2 + sPassword.Substring(sPassword.Length - 8, 8);


            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Insert(candidato) > 0)
                    {
                        TempData["successMessage"] = "Candidato Ingresado con exito";
                        return RedirectToAction("Index");
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
    }
}