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
    tipos_usuariosModel model6 = new tipos_usuariosModel();
    usuariosModel model5 = new usuariosModel();
    ofertasModel model = new ofertasModel();

    EmpleadosModel model2 = new EmpleadosModel();
    curriculumModel model3 = new curriculumModel();
    candidatos_ofertasModel model4 = new candidatos_ofertasModel();
    
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

    public ActionResult ofertToList()
    {
        return View(model.List());
    }

    [HttpGet]
    public ActionResult candiToOfert(int id)
    {
        candidatos_ofertas candidatos = new candidatos_ofertas();
        usuarios user = new usuarios();
        user = (usuarios)Session["usuario"];
        Debug.WriteLine(user.id_usuario);
        int userid = user.id_usuario;
        curriculum cv = new curriculum();
        cv = model3.getCurriculum(userid);
        Debug.WriteLine(cv.id_curriculum);
        int idcv = cv.id_curriculum;
        candidatos.id_oferta = id;
        candidatos.id_curriculum = idcv;
        candidatos.descripcion = "Algo random";
        model4.Insert(candidatos);
        return View("ofertToList", model.List());
    }

    [HttpGet]
     public ActionResult candiToAp(int id)
    {
        List<candidatos_ofertas> canToOfer = new List<candidatos_ofertas>();
        canToOfer = model4.getCandiOfer(id);
        return View(canToOfer.ToList());
    }

    public ActionResult Details(int id)
    {
        curriculum cv = model3.GetById(id);
        if(cv == null)
        {
            return HttpNotFound();
        }
        return View(cv);
    }
 }