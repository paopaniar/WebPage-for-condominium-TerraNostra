using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TerraNostra.Enum;
using TerraNostra.Security;

namespace TerraNostra.Controllers
{
    public class PlanCobroController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<plan_cobro> lista = null;
            try
            {
                IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
                lista = _ServicePlanCobro.GetPlanCobro();
                IServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
                ViewBag.listaRubros = _ServiceRubro.GetRubroCobro();
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

        [HttpPost]
        public ActionResult Save(plan_cobro plan_cobro, string[] selectedRubros)
        {
            IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            try
            {
              

                if (plan_cobro.estado == null)
                {
                    plan_cobro.estado = 1;
                }


                if (ModelState.IsValid)
                {
                    plan_cobro oPlanCobroI = _ServicePlanCobro.Save(plan_cobro, selectedRubros);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //  ViewBag.idUsuario = listUsuarios(incidente.id);
                    ViewBag.idRubro = listaRubros(plan_cobro.rubro_cobro);
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
            ViewBag.idRubros = listaRubros();
            return View();
        }
        private MultiSelectList listaRubros(ICollection<rubro_cobro> rubros = null)
        {
            IServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
            IEnumerable<rubro_cobro> lista = _ServiceRubro.GetRubroCobro();
            //Seleccionar categorias
            int[] listaRubrosSelect = null;
            if (rubros != null)
            {
                listaRubrosSelect = rubros.Select(c => c.id).ToArray();
            }

            return new MultiSelectList(lista, "id", "detalle", listaRubrosSelect);
        }

        public ActionResult Edit(int? id)
        {
            ServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            plan_cobro planCobro = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                planCobro = _ServicePlanCobro.GetPlanCobroById(Convert.ToInt32(id));
                if (planCobro == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IdRubro = listaRubros(planCobro.rubro_cobro);
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