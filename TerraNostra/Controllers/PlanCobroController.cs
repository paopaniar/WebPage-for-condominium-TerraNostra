using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace TerraNostra.Controllers
{
    public class PlanCobroController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<plan_cobro> lista = null;
            try
            {
                IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
                lista = _ServicePlanCobro.GetPlanCobro();
                ViewBag.title = "Lista Plan Cobro";
                IServiceRubroCobro _ServiceRubroCobro = new ServiceRubroCobro();
                ViewBag.listaRubroCobro = _ServiceRubroCobro.GetRubroCobro();

                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Details(int? id)
        {
            ServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            plan_cobro planCobro = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Details");
                }

                planCobro = _ServicePlanCobro.GetPlanCobroById(Convert.ToInt32(id));
                if (planCobro == null)
                {
                    TempData["Message"] = "No existe el plan de cobro solicitado";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(planCobro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PlanCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        private SelectList lisRubros(int idRubro = 0)
        {
            IServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
            IEnumerable<rubro_cobro> lista = _ServiceRubro.GetRubroCobro();
            return new SelectList(lista, "id", "detalle", idRubro);
        }


        [HttpPost]
        public ActionResult Save(plan_cobro plan_cobro, string[] selectedRubros)
        {

            //Servicio Libro
            IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            try
            {

                if (ModelState.IsValid)
                {
                    plan_cobro oPlanCobroI = _ServicePlanCobro.Save(plan_cobro, selectedRubros);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //  ViewBag.idUsuario = listUsuarios(incidente.id);
                   // ViewBag.idRubros = lisRubros(plan_cobro.rubroCobroId);
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (plan_cobro.id > 0)
                    {
                        return (ActionResult)View("Edit", plan_cobro);
                    }
                    else
                    {
                        return View("Create", plan_cobro);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PlanCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Que recursos necesito para crear una incidencia
            //usuarios
            ViewBag.idRubros = lisRubros();

            return View();
        }


        public ActionResult Edit(int? id)
        {
            ServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            plan_cobro plan_Cobro = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    //ATENCION! HACER EL INDEX byPao
                    return RedirectToAction("Index");
                }

                plan_Cobro = _ServicePlanCobro.GetPlanCobroById(Convert.ToInt32(id));
                if (plan_Cobro == null)
                {
                    TempData["Message"] = "No existe el plan de cobro solicitado";
                    TempData["Redirect"] = "PlanCobro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados
                ViewBag.idRubros = lisRubros(plan_Cobro.id);

                return View(plan_Cobro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PlanCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}