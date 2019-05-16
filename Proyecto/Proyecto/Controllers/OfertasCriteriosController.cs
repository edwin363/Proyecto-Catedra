using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class OfertasCriteriosController : Controller
    {
        ofertas_criteriosModel model = new ofertas_criteriosModel();
        ofertasModel ofertamodel = new ofertasModel();
        CriteriosModel criteriomodel = new CriteriosModel();
        empleadosModel empleadomodel = new empleadosModel();

        // GET: OfertasCriterios
        public ActionResult Index()
        {
            return View(model.List());
        }

        // GET: OfertasCriterios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OfertasCriterios/Create
        public ActionResult Create()
        {
            //Obteniendo ofertas del empleado logueado
            usuarios user = new usuarios();
            user = (usuarios)Session["usuario"];
            int userid = user.id_usuario;
            empleados emple = new empleados();
            emple = empleadomodel.getEmpleado(userid);
            int id = emple.id_empleado;
            List<ofertas> oferta = new List<ofertas>();
            oferta = ofertamodel.ofertasToUser(id);
            
            
            
            if (oferta != null)
            {
                ViewBag.listaCriterios = new SelectList(criteriomodel.List(), "id_criterio", "nombre_criterio");
                ViewBag.listaOfertas = new SelectList(oferta.ToList(), "id_oferta", "nombre_puesto");
                return View();
            }
            else
            {
                //Debug.WriteLine("Error");
                return View();
            }


           
            
        }

        // POST: OfertasCriterios/Create
        [HttpPost]
        public ActionResult Create(ofertas_criterios oferta_criterio)
        {
            try
            {
                
                //Obteniendo ofertas del empleado logueado
                usuarios user = new usuarios();
                user = (usuarios)Session["usuario"];
                int userid = user.id_usuario;
                empleados emple = new empleados();
                emple = empleadomodel.getEmpleado(userid);
                int id = emple.id_empleado;
                List<ofertas> oferta = new List<ofertas>();
                oferta = ofertamodel.ofertasToUser(id);

                ViewBag.listaCriterios = new SelectList(criteriomodel.List(), "id_criterio", "nombre_criterio");
                ViewBag.listaOfertas = new SelectList(oferta.ToList(), "id_oferta", "nombre_puesto");

                oferta_criterio.porcentaje = oferta_criterio.porcentaje / 100;

                decimal total = 0;
                List<ofertas_criterios> bell = model.ofertasCriterios(oferta_criterio.id_oferta);
                foreach (var item in bell)
                {
                    total += item.porcentaje;
                }
                total = total + oferta_criterio.porcentaje ;
                if (total > 1)
                {
                    ViewBag.mes = "porcentaje incorrecto";
                    return View("Index", model.List());
                }

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (model.Insert(oferta_criterio) > 0)
                    {
                        TempData["successMessage"] = "Ponderación insertada con exito";
                        return RedirectToAction("Index");
                    }
                    TempData["errorMessage"] = "Upss, no se pudo insertar";
                }
                return View(oferta_criterio);
            }
            catch
            {
                return View();
            }
        }

        // GET: OfertasCriterios/Edit/5
        public ActionResult Edit(int? id)
        {
            //Obteniendo ofertas del empleado logueado
            usuarios user = new usuarios();
            user = (usuarios)Session["usuario"];
            int userid = user.id_usuario;
            empleados emple = new empleados();
            emple = empleadomodel.getEmpleado(userid);
            int idem = emple.id_empleado;
            List<ofertas> oferta = new List<ofertas>();
            oferta = ofertamodel.ofertasToUser(idem);

            //if (oferta != null)
            //{
            //    ViewBag.listaCriterios = new SelectList(criteriomodel.List(), "id_criterio", "nombre_criterio");
            //    ViewBag.listaOfertas = new SelectList(oferta.ToList(), "id_oferta", "nombre_puesto");
            //    return View();
            //}

            //Parte de modificar
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas_criterios oferta_criterio = model.GetById(id);
            if (oferta_criterio == null)
            {
                return HttpNotFound();
            }
            Session["porcenOld"] = oferta_criterio.porcentaje;
            Session["idEditar"] = oferta_criterio.id_oferta_crit;
            Debug.WriteLine(Session["idEditar"]);
            ViewBag.listaCriterios = new SelectList(criteriomodel.List(), "id_criterio", "nombre_criterio");
            ViewBag.listaOfertas = new SelectList(oferta.ToList(), "id_oferta", "nombre_puesto");
            return View(oferta_criterio);
        }

        // POST: OfertasCriterios/Edit/5
        [HttpPost]
        public ActionResult Edit(ofertas_criterios oferta_criterio)
        {
            try
            {
                //Obteniendo ofertas del empleado logueado
                usuarios user = new usuarios();
                user = (usuarios)Session["usuario"];
                int userid = user.id_usuario;
                empleados emple = new empleados();
                emple = empleadomodel.getEmpleado(userid);
                int idem = emple.id_empleado;
                List<ofertas> oferta = new List<ofertas>();
                oferta = ofertamodel.ofertasToUser(idem);

                ViewBag.listaCriterios = new SelectList(criteriomodel.List(), "id_criterio", "nombre_criterio");
                ViewBag.listaOfertas = new SelectList(oferta.ToList(), "id_oferta", "nombre_puesto");

                oferta_criterio.porcentaje = oferta_criterio.porcentaje / 100;
                decimal porcentajeAntiguo = Convert.ToDecimal(Session["porcenOld"]);

               // decimal porcentajeAntiguo = oferta_criterio.porcentaje;

                decimal total = 0;
                List<ofertas_criterios> bell = model.ofertasCriterios(oferta_criterio.id_oferta);
                foreach (var item in bell)
                {
                    total += item.porcentaje;
                }
                total = total - porcentajeAntiguo;
                total = total + oferta_criterio.porcentaje;
                if (total > 1)
                {
                    ViewBag.mes = "porcentaje incorrecto";
                    return View("Index", model.List());
                }

                oferta_criterio.id_oferta_crit = Convert.ToInt32(Session["idEditar"]);

                //Debug.WriteLine(idCriterio);

                if (ModelState.IsValid)
                {
                    if (model.Update(oferta_criterio, oferta_criterio.id_oferta_crit) > 0)
                    {
                        TempData["successMessage"] = "Criterio ponderado modificado con exito";
                        return RedirectToAction("Index");
                    }
                    TempData["errorMessage"] = "Upss, no se pudo modificar";
                }

                return View(oferta_criterio);
            }
            catch
            {
                return View();
            }
        }

        // GET: OfertasCriterios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas_criterios oferta_criterio = model.GetById(id);
            if (oferta_criterio == null)
            {
                TempData["errorMessage"] = "No existe este criterio de evaluación";
                return RedirectToAction("Index");
            }
            if (model.Remove(id) > 0)
            {
                TempData["successMessage"] = "Criterio de evaluación eliminado correctamente";
            }
            else
            {
                TempData["errorMessage"] = "No se puede eliminar este criterio de evaluación";
            }
            return RedirectToAction("Index");
        }

        // POST: OfertasCriterios/Delete/5
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
