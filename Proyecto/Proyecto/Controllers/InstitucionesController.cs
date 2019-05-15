using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class InstitucionesController : Controller
    {
        institucionesModel model = new institucionesModel();
        
        // GET: Instituciones
        public ActionResult Index()
        {
            return View(model.List());
        }

        // GET: Instituciones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Instituciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instituciones/Create
        [HttpPost]
        public ActionResult Create(instituciones instituciones)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (model.Insert(instituciones) > 0)
                    {
                        TempData["successMessage"] = "Institución insertada con exito";
                        return RedirectToAction("Index");
                    }
                    TempData["errorMessage"] = "Upss, no se pudo insertar";
                }

                return View(instituciones);
            }
            catch
            {
                return View();
            }
        }

        // GET: Instituciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            instituciones institucion = model.GetById(id);
            if (institucion == null)
            {
                return HttpNotFound();
            }
            return View(institucion);
        }

        // POST: Instituciones/Edit/5
        [HttpPost]
        public ActionResult Edit(instituciones institucion)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (model.Update(institucion, institucion.id_institucion) > 0)
                    {
                        TempData["successMessage"] = "Institución modificada con exito";
                        return RedirectToAction("Index");
                    }
                    TempData["errorMessage"] = "Upss, no se pudo modificar";
                }
                return View(institucion);
            }
            catch
            {
                return View();
            }
        }

        // GET: Instituciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            instituciones institucion = model.GetById(id);
            if (institucion == null)
            {
                TempData["errorMessage"] = "No existe esta institución";
                return RedirectToAction("Index");
            }
            if (model.Remove(id) > 0)
            {
                TempData["successMessage"] = "Institución eliminada correctamente";
            }
            else
            {
                TempData["errorMessage"] = "No se puede eliminar esta institución";
            }
            return RedirectToAction("Index");
        }

        // POST: Instituciones/Delete/5
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
