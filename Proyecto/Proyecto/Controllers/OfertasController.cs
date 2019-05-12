using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class OfertasController : Controller
    {
        EmpleadosModel empleadomodel = new EmpleadosModel();
        OfertasModel model = new OfertasModel();

        // GET: Ofertas
        public ActionResult Index()
        {
            return View(model.List());
        }

        // GET: Ofertas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ofertas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ofertas/Create
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

        // GET: Ofertas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ofertas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ofertas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
