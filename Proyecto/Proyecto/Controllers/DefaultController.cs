using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto;
using Proyecto.Models;

public class DefaultController : Controller
{
    ofertasModel model = new ofertasModel();
    empleadosModel model2 = new empleadosModel();
        // GET: Default
        public ActionResult Index()
        {
            usuarios user = new usuarios();
            user = (usuarios)Session["usuario"];
            Debug.WriteLine(user.id_usuario);
            int userid = user.id_usuario; 
            empleados emple = new empleados();
            emple = model2.getEmpleado(userid);
            //Debug.WriteLine(user.id_usuario);
            Debug.WriteLine(emple.id_empleado);
            int id = emple.id_empleado;
            Debug.WriteLine(id);
            List<ofertas> oferta = new List<ofertas>();
            oferta = model.ofertasToUser(id);
            if(oferta != null)
            {
                return View(oferta.ToList());
            }
            else
            {
                Debug.WriteLine("Error");
                return View();
            }
        }
    }