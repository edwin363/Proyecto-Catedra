using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class OfertasController : Controller
    {
        EmpleadosModel empleadomodel = new EmpleadosModel();
        ofertasModel model = new ofertasModel();

        // GET: Ofertas
        public ActionResult Index()
        {
            return View(model.List());
        }

        // GET: Ofertas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas oferta = model.GetById(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            return View(oferta);
        }

        // GET: Ofertas/Create
        public ActionResult Create()
        {
            ViewBag.listaempleados = new SelectList(empleadomodel.List(), "id_empleado", "id_usuario");
            return View();
        }

        // POST: Ofertas/Create
        [HttpPost]
        public ActionResult Create(ofertas oferta)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (model.Insert(oferta) > 0)
                    {
                        TempData["successMessage"] = "Oferta insertada con exito";
                        return RedirectToAction("Index");
                    }
                    TempData["errorMessage"] = "Upss, no se pudo insertar";
                }
                return View(oferta);
            }
            catch
            {
                return View();
            }
        }

        // GET: Ofertas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas oferta = model.GetById(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            return View(oferta);
        }

        // POST: Ofertas/Edit/5
        [HttpPost]
        public ActionResult Edit(ofertas oferta)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (model.Update(oferta, oferta.id_oferta) > 0)
                    {
                        TempData["successMessage"] = "Oferta modificada con exito";
                        return RedirectToAction("Index");
                    }
                    TempData["errorMessage"] = "Upss, no se pudo modificar";
                }

                return View(oferta);
            }
            catch
            {
                return View();
            }
        }

        // GET: Ofertas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas oferta = model.GetById(id);
            if (oferta == null)
            {
                TempData["errorMessage"] = "No existe esta oferta";
                return RedirectToAction("Index");
            }
            if (model.Remove(id) > 0)
            {
                TempData["successMessage"] = "Oferta eliminada correctamente";
            }
            else
            {
                TempData["errorMessage"] = "No se puede eliminar este oferta";
            }
            return RedirectToAction("Index");
        }

        // POST: Ofertas/Delete/5
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
