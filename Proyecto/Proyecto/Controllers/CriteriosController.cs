using Proyecto.Models;
using Proyecto.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class CriteriosController : Controller
    {
        CriteriosModel model = new CriteriosModel();

        // GET: Criterios
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
        public ActionResult Index()
        {
            return View(model.List());
        }

        // GET: Criterios/Details/5
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Criterios/Create
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Criterios/Create
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Criterios/Edit/5
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criterios criterio = model.GetById(id);
            if (criterio == null)
            {
                return HttpNotFound();
            }
            return View(criterio);
        }

        // POST: Criterios/Edit/5
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
        [HttpPost]
        public ActionResult Edit(criterios criterio)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (model.Update(criterio, criterio.id_criterio) > 0)
                    {
                        TempData["successMessage"] = "Criterio modificado con exito";
                        return RedirectToAction("Index");
                    }
                    TempData["errorMessage"] = "Upss, no se pudo modificar";
                }             
                return View(criterio);
            }
            catch
            {
                return View();
            }
        }

        // GET: Criterios/Delete/5
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criterios criterio = model.GetById(id);
            if (criterio == null)
            {
                TempData["errorMessage"] = "No existe este criterio";
                return RedirectToAction("Index");
            }
            if (model.Remove(id) > 0)
            {
                TempData["successMessage"] = "Criterio eliminado correctamente";
            }
            else
            {
                TempData["errorMessage"] = "No se puede eliminar este criterio";
            }
            return RedirectToAction("Index");
        }

        // POST: Criterios/Delete/5
        [AuthorizeUserLevel(UserRole = "Personal de recursos humanos")]
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
