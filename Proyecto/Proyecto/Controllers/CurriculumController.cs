using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using WebApplication3.Models;

namespace Proyecto.Controllers
{
    public class CurriculumController : Controller
    {
        curriculumModel model = new curriculumModel();
        idiomasModel idioma = new idiomasModel();
        cv_IdiomaModel cvIdioma = new cv_IdiomaModel();
        form_academicaModel model1 = new form_academicaModel();
        cv_FormacionModel model2 = new cv_FormacionModel();

        // GET: Curriculum
        public ActionResult Index()
        {
            return View();
        }

        // POST: Curriculum
        [HttpPost]
        public ActionResult Index(string date, curriculum curriculum, string sexo, string foto)
        {
            curriculum.id_usuario = 2;
            curriculum.fechaNacimiento = Convert.ToDateTime(date);
            curriculum.sexo = sexo;
            curriculum.fotografia = foto;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Insert(curriculum) > 0)
                    {
                        TempData["successMessage"] = "Primer Paso Ingresado";
                        return RedirectToAction("Idioma");
                    }
                }
            }
            catch
            {

            }
            return View();
        }

        // PARTE DE IDIOMAS

        public ActionResult Idioma()
        {
            int id = 2; //ID DE LA SESSION 
            curriculum candidato = model.idCurriculum(id);
            if (candidato == null)
            {
                return View("Index");
            }
            else
            {
                ViewBag.idIdioma = idioma.List();
                ViewBag.Lista = cvIdioma.idiomasCandidato(candidato.id_curriculum);
                ViewBag.Idiomas = idioma.ListaIdiomas();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Idioma(int? id)
        {
            int idC = 2; //ID DE LA SESSION 
            curriculum candidato = model.idCurriculum(idC);
            ViewBag.idIdioma = idioma.List();
            ViewBag.Lista = cvIdioma.idiomasCandidato(candidato.id_curriculum);
            ViewBag.Idiomas = idioma.ListaIdiomas();
            if (id == null)
            {
                return View("Idioma");
            }
            int an = Convert.ToInt32(id);
            cv_idiomas bell = cvIdioma.id_cv_idioma(an);
            try
            {
                if (bell == null)
                {
                    return View("Idioma");
                }
                else
                {
                    if (cvIdioma.Remove(an) > 0)
                    {
                        return RedirectToAction("Idioma");
                    }
                    ViewBag.errorB = "ERROR: AL ELIMINAR EL IDIOMA";
                    return View("Idioma");
                }
            }
            catch
            {
                ViewBag.errorB = "ERROR: AL ELIMINAR EL IDIOMA";
                return View("Idioma");
            }
        }

        [HttpPost]
        public ActionResult Idioma(string idioma1)
        {
            int id = 2; //ID DE LA SESSION 
            int lang = Convert.ToInt32(idioma1);
            curriculum candidato = model.idCurriculum(id);

            ViewBag.idIdioma = idioma.List();
            ViewBag.Lista = cvIdioma.idiomasCandidato(candidato.id_curriculum);
            ViewBag.Idiomas = idioma.ListaIdiomas();

            if(candidato == null)
            {
                return View("Index");
            }
            else
            {
                cv_idiomas validacion = cvIdioma.idIdioma(lang, candidato.id_curriculum);
                if (validacion == null)
                {
                    try
                    {
                        cv_idiomas bell = new cv_idiomas();
                        bell.id_curriculum = candidato.id_curriculum;
                        bell.id_idioma = lang;
                        if (cvIdioma.Insert(bell) > 0)
                        {

                            ViewBag.errorB = "Idioma Ingresado";
                            return RedirectToAction("Idioma");
                        }
                        else
                        {
                            ViewBag.errorB = "Ya ha ingresado ha este idioma";
                            return View("Idioma");
                        } 
                    }
                    catch
                    {
                        ViewBag.errorB = "ERROR";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.idIdioma = idioma.List();
                    ViewBag.Lista = cvIdioma.idiomasCandidato(candidato.id_curriculum);
                    ViewBag.Idiomas = idioma.ListaIdiomas();
                    ViewBag.errorB = "Ya ha ingresado este idioma";
                    return View("Idioma");
                }
            }
        }

        // PARTE DE FORM_ACADEMICA


        public ActionResult FormacionAcademica()
        {
            int idC = 2; //ID DE LA SESSION 
            curriculum an = model.idCurriculum(idC);
            ViewBag.FormA = model2.FormacionCandidato(an.id_curriculum);
            ViewBag.Form = model1.List();

            return View();
        }

        [HttpGet]
        public ActionResult FormacionAcademica(int? id)
        {
            int idC = 2; //ID DE LA SESSION 
            curriculum an = model.idCurriculum(idC);
            ViewBag.FormA = model2.FormacionCandidato(an.id_curriculum);
            ViewBag.Form = model1.List();
            if (id == null)
            {
                return View("FormacionAcademica");
            }
            int idForm = Convert.ToInt32(id);
            cv_form_academica obj = model2.ObjetoFormacion(an.id_curriculum, idForm);

            try
            {
                if (obj == null)
                {
                    return View("FormacionAcademica");
                }
                else
                {
                    if (model2.Remove(obj.id_cv_form_academica) > 0)
                    {
                        return RedirectToAction("FormacionAcademica");
                    }
                    ViewBag.errorB = "ERROR: AL ELIMINAR LA FORMACION";
                    return View("FormacionAcademica");
                }
            }
            catch
            {
                ViewBag.errorB = "ERROR: AL ELIMINAR EL IDIOMA";
                return View("Idioma");
            }

        }

        [HttpPost]
        public ActionResult FormacionAcademica(string tipo, string inicio, string fin, form_academica formacion)
        {
            int idC = 2; //ID DE LA SESSION 
            curriculum an = model.idCurriculum(idC);
            ViewBag.FormA = model2.FormacionCandidato(an.id_curriculum);
            ViewBag.Form = model1.List();

            if (inicio.Length == 0 || fin.Length == 0)
            {
                ViewBag.tipo = "Ingrese las Fechas";
                return View();
            }
            formacion.tipo_educacion = tipo;
            formacion.fech_inicio = Convert.ToDateTime(inicio);
            formacion.fech_fin = Convert.ToDateTime(fin);

            try
            {
                if (ModelState.IsValid)
                {
                    if (model1.Insert(formacion) > 0)
                    {
                        cv_form_academica bell = new cv_form_academica();
                        bell.id_curriculum = an.id_curriculum;
                        bell.id_form_academica = formacion.id_form_academica;
                        if (model2.Insert(bell) > 0)
                        {
                            ViewBag.tipo = "Formacion Academica Ingresado";
                            return RedirectToAction("FormacionAcademica");
                        }
                        ViewBag.tipo = "ERROR3";
                        return View();
                    }
                    ViewBag.tipo = "ERROR2";
                    return View();
                }
                else
                {
                    ViewBag.tipo = "ERROR";
                    return View();
                }
            }
            catch
            {
                return View();
            }
            
        }
    }
}