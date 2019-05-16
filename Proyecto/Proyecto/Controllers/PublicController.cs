using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class PublicController : Controller
    {
        empleadosModel empleadomodel = new empleadosModel();
        ofertasModel model = new ofertasModel();
        institucionesModel modelInstituciones = new institucionesModel();

        // GET: Public
        public ActionResult Index()
        {
            return View(model.List());
        }

        // GET: Public/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Public/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Public/Create
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

        // GET: Public/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Public/Edit/5
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

        // GET: Public/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Public/Delete/5
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
