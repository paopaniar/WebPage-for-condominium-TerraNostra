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
    }
}