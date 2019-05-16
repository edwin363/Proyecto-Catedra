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
        usuariosModel ni = new usuariosModel();
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
            usuarios us = (usuarios)Session["usuario"];
            curriculum.id_usuario = us.id_usuario;
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
            usuarios us = (usuarios)Session["usuario"];
            
            curriculum candidato = model.idCurriculum(us.id_usuario);
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
            usuarios us = (usuarios)Session["usuario"];
            curriculum candidato = model.idCurriculum(us.id_usuario);
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
            usuarios us = (usuarios)Session["usuario"];
            int lang = Convert.ToInt32(idioma1);
            curriculum candidato = model.idCurriculum(us.id_usuario);

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
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model2.FormacionCandidato(an.id_curriculum);
            ViewBag.Form = model1.List();

            return View();
        }

        [HttpGet]
        public ActionResult FormacionAcademica(int? id)
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
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
                    int x = obj.id_form_academica;
                    if (model2.Remove(obj.id_cv_form_academica) > 0)
                    {
                        if (model1.Remove(x)>0)
                        {
                            return RedirectToAction("FormacionAcademica");
                        }
                        
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
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model2.FormacionCandidato(an.id_curriculum);
            ViewBag.Form = model1.List();

            if (inicio.Length == 0 || fin.Length == 0)
            {
                ViewBag.errorB = "Ingrese las Fechas";
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
                            ViewBag.errorB = "Formacion Academica Ingresado";
                            return RedirectToAction("FormacionAcademica");
                        }
                        ViewBag.errorB = "ERROR3";
                        return View();
                    }
                    ViewBag.errorB = "ERROR2";
                    return View();
                }
                else
                {
                    ViewBag.errorB = "ERROR";
                    return View();
                }
            }
            catch
            {
                return View();
            }
            
        }


        /* EXPERIENCIA LABORAL */
        exp_profesionalModel model5 = new exp_profesionalModel();
        cv_ExpModel model6 = new cv_ExpModel();

        public ActionResult ExperienciaLaboral()
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model6.ListaExp(an.id_curriculum);
            ViewBag.Form = model5.List();
            return View();
        }

        [HttpGet]
        public ActionResult ExperienciaLaboral(int? id)
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model6.ListaExp(an.id_curriculum);
            ViewBag.Form = model5.List();
            if (id == null)
            {
                return View("ExperienciaLaboral");
            }

            int idExp = Convert.ToInt32(id);
            cv_exp_laboral obj = model6.ObjetoExp(an.id_curriculum, idExp);
            
            try
            {
                if (obj == null)
                {
                    return View("ExperienciaLaboral");
                }
                else
                {
                    int x = obj.id_exp_profesional;
                    if (model6.Remove(obj.id_cv_exp_laboral) > 0)
                    {
                        if (model5.Remove(x)>0)
                        {
                            return RedirectToAction("ExperienciaLaboral");
                        }
                        
                    }
                    ViewBag.errorB = "ERROR: AL ELIMINAR LA EXPERIENCIA";
                    return View("ExperienciaLaboral");
                }
            }
            catch
            {
                ViewBag.errorB = "ERROR: AL ELIMINAR la experiencia";
                return View("ExperienciaLaboral");
            }
        }

        [HttpPost]
        public ActionResult ExperienciaLaboral(string inicio, string fin, exp_profesional exp)
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model6.ListaExp(an.id_curriculum);
            ViewBag.Form = model5.List();
            if (inicio.Length == 0 || fin.Length == 0)
            {
                ViewBag.errorB = "Ingrese las Fechas";
                return View();
            }
            exp.fech_inicio = Convert.ToDateTime(inicio);
            exp.fech_fin = Convert.ToDateTime(fin);
            try
            {
                if (ModelState.IsValid)
                {
                    if (model5.Insert(exp) > 0)
                    {
                        cv_exp_laboral bell = new cv_exp_laboral();
                        bell.id_curriculum = an.id_curriculum;
                        bell.id_exp_profesional = exp.id_exp_profesional;
                        if (model6.Insert(bell) > 0)
                        {
                            ViewBag.errorB = "Experiencia Profesional Ingresado";
                            return RedirectToAction("ExperienciaLaboral");
                        }
                        ViewBag.errorB = "ERROR3";
                        return View();
                    }
                    ViewBag.errorB = "ERROR2";
                    return View();
                }
                else
                {
                    ViewBag.errorB = "ERROR";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        /* REFERENCIA PERSONAL */

        refer_personalesModel model7 = new refer_personalesModel();
        cv_referenciaModel model8 = new cv_referenciaModel();

        public ActionResult Referencia()
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model8.ListaR(an.id_curriculum);
            ViewBag.Form = model7.List();
            return View();
        }

        [HttpGet]
        public ActionResult Referencia(int? id)
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model8.ListaR(an.id_curriculum);
            ViewBag.Form = model7.List();
            if (id == null)
            {
                return View("Referencia");
            }
            

            int idR = Convert.ToInt32(id);
            cv_ref_profesionales obj = model8.ObjetoR(an.id_curriculum, idR);

            try
            {
                if (obj == null)
                {
                    return View("Referencia");
                }
                else
                {
                    if (model8.Remove(obj.id_cv_ref_profesionales) > 0)
                    {
                        if (model7.Remove(idR)>0)
                        {
                            return RedirectToAction("Referencia");
                        }
                        
                    }
                    ViewBag.errorB = "ERROR: AL ELIMINAR LA REFERENCIA";
                    return View("Referencia");
                }
            }
            catch
            {
                ViewBag.errorB = "ERROR: AL ELIMINAR la experiencia";
                return View("ExperienciaLaboral");
            }
        }

        [HttpPost]
        public ActionResult Referencia(refer_personales andrea)
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.FormA = model8.ListaR(an.id_curriculum);
            ViewBag.Form = model7.List();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model7.Insert(andrea) > 0)
                    {
                        cv_ref_profesionales bell = new cv_ref_profesionales();
                        bell.id_curriculum = an.id_curriculum;
                        bell.id_referencia = andrea.id_referencia;
                        if (model8.Insert(bell) > 0)
                        {
                            ViewBag.errorB = "Referencia Personal Ingresado";
                            return RedirectToAction("Referencia");
                        }
                        ViewBag.errorB = "ERROR3";
                        return View();
                    }
                    ViewBag.errorB = "ERROR2";
                    return View();
                }
                else
                {
                    ViewBag.errorB = "ERROR";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MostrarCV()
        {
            usuarios us = (usuarios)Session["usuario"];
            curriculum an = model.idCurriculum(us.id_usuario);
            ViewBag.Ref = model8.ListaR(an.id_curriculum);
            ViewBag.R = model7.List();

            ViewBag.Exp = model6.ListaExp(an.id_curriculum);
            ViewBag.E = model5.List();

            ViewBag.FormA = model2.FormacionCandidato(an.id_curriculum);
            ViewBag.Form = model1.List();

            ViewBag.idIdioma = idioma.List();
            ViewBag.Lista = cvIdioma.idiomasCandidato(an.id_curriculum);
            ViewBag.Idiomas = idioma.ListaIdiomas();

            curriculum otro = model.idCurriculum(us.id_usuario);
            ViewBag.fecha = otro.fechaNacimiento;
            ViewBag.direccion = otro.direccion;
            ViewBag.numero = otro.numeroTelefono;
            ViewBag.dui = otro.dui;
            ViewBag.sexo = otro.sexo;

            usuarios another = ni.GetById(us.id_usuario);
            ViewBag.nombre = another.nombre;
            ViewBag.apellido1 = another.apellido1;
            ViewBag.apellido2 = another.apellido2;
            ViewBag.correo = another.email;

            return View();
        }
    }
}