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
    public class PlanResidenciaController : Controller
    {
        // GET: PlanResidencia
        public ActionResult Index()
        {
            IEnumerable<plan_residencia> lista = null;
            try
            {
                IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
                lista = _ServicePlanResidencia.GetPlanResidencia();
                ViewBag.title = "Lista planes residencia";
                //Lista autores
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                ViewBag.listaResidencia = _ServiceResidencia.GetResidencia();
                IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
                ViewBag.listaPlanCobro = _ServicePlanCobro.GetPlanCobro();
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                ViewBag.listaUsuario = _ServiceUsuario.GetUsuario();
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
            ServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            plan_residencia p_residencia = null;
            try
            {
                // Si va null
                    if (id == null)
                {
                    return RedirectToAction("Index");
                }

                p_residencia = _ServicePlanResidencia.GetPlanResidenciaByID(Convert.ToInt32(id));

                if (p_residencia == null)
                {
                    TempData["Message"] = "No existe la residencia solicitada";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(p_residencia);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Residencias";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        
    }


    /* public ActionResult Details(int? id)
       {
            ServicePlanResidencia _ServicePlan = new ServicePlanResidencia();
            plan_residencia plan = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                plan = _ServicePlan.GetPlanResidenciaByID(Convert.ToInt32(id));
                if (plan == null)
                {
                    TempData["Message"] = "No existe el plan solicitado";
                    TempData["Redirect"] = "PlanResidencia";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(plan);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PlanResidencia";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }*/
    }
}