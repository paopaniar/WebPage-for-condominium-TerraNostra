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
    }
}