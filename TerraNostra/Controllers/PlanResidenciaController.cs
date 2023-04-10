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
using TerraNostra.Utils;

namespace TerraNostra.Controllers
{
    public class PlanResidenciaController : Controller
    {
        // GET: PlanResidencia
        [CustomAuthorize((int)Roles.Administrador)]
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

        public ActionResult Details(int id)
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
                IServicePlanResidencia _ServiceResidencia = new ServicePlanResidencia();
                ViewBag.EstadosPagados = _ServicePlanResidencia.GetEstadosByEstado(p_residencia.residenciaId,1);
                ViewBag.EstadosPendientes = _ServicePlanResidencia.GetEstadosByEstado(p_residencia.residenciaId,0);

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
        [CustomAuthorize((int)Roles.Administrador)]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.idResidencia = listaResidencias();
            ViewBag.idPlanes = listaPlanes();

            return View();
        }
        private SelectList listaResidencias(ICollection<residencia> residencias = null)
        {
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            IEnumerable<residencia> lista = _ServiceResidencia.GetResidencia();
            //Seleccionar categorias
            int[] listaResidenciasSelect = null;
            if (residencias != null)
            {
                listaResidenciasSelect = residencias.Select(c => c.id).ToArray();
            }

            return new SelectList(lista, "id", "numeroCasa", listaResidenciasSelect);
        }

            private SelectList listaPlanes(ICollection<plan_cobro> planes = null)
            {
                IServicePlanCobro _ServicePlanes = new ServicePlanCobro();
                IEnumerable<plan_cobro> lista = _ServicePlanes.GetPlanCobro();
                //Seleccionar categorias
                int[] listaPlanesSelect = null;
                if (planes != null)
                {
                listaPlanesSelect = planes.Select(c => c.id).ToArray();
                }

                return new SelectList(lista, "id", "detail", listaPlanesSelect);
            }

        [HttpPost]
        public ActionResult Save(plan_residencia plan_residencia, string[] selectedResidencia, string[] selectedPlanes)
        {
            IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            try
            {
                if (ModelState.IsValid)
                {
                    var existingPlanResidencia = _ServicePlanResidencia.GetPlanResidenciaByMonthAndYear(plan_residencia.residenciaId,plan_residencia.fecha.Month, plan_residencia.fecha.Year);

                    if (existingPlanResidencia.Count > 0)
                    {
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Error", "Ya existe un plan de cobro para el mes y año ingresados.", SweetAlertMessageType.error);
                        ViewBag.idResidencia = listaResidencias();
                        ViewBag.idPlanes = listaPlanes();

                        if (plan_residencia.id > 0)
                        {
                            //aquui no sé si poner el plan cobro porque no sé como hacerlo
                            return (ActionResult)View("Edit", plan_residencia.residencia);
                        }
                        else
                        {
                            return View("Create", plan_residencia);
                        }
                    }
                    else
                    {
                        plan_residencia oPlanResidenciaI = _ServicePlanResidencia.Save(plan_residencia, selectedResidencia, selectedPlanes);
                    }
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);

                    ViewBag.idResidencia = listaResidencias();
                    ViewBag.idPlanes = listaPlanes();

                    if (plan_residencia.id > 0)
                    {
                        //aquui no sé si poner el plan cobro porque no sé como hacerlo
                        return (ActionResult)View("Edit", plan_residencia.residencia);
                    }
                    else
                    {
                        return View("Create", plan_residencia);
                    }
                }

                return RedirectToAction("IndexEstadosxMes");
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
        }


        public ActionResult Edit(int id)
        {
            IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            plan_residencia plan_residencia = _ServicePlanResidencia.GetPlanResidenciaBy(id);
            if (plan_residencia.estado == 1)
            {
                plan_residencia.estado = 0;
            }
            else
            {
                plan_residencia.estado = 1;
            }

            try
            {
                // Si va null
                if (ModelState.IsValid)
                {
                    plan_residencia oPlanResidencia = _ServicePlanResidencia.Guardar(plan_residencia);
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Se realizó el pago exitosamente!.", SweetAlertMessageType.success);

                }

                else
                {
                    TempData["Message"] = "No existe el incidente solicitado";
                    TempData["Redirect"] = "PlanResidencia";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                return View();
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
        }

        public ActionResult _PartialViewListaEstados()
        {
            return PartialView("_PartialViewListaEstados");
        }
        public ActionResult _PartialViewListaEstadosxUsuario()
        {
            return PartialView("_PartialViewListaEstadosxUsuario");
        }

        public ActionResult estadosxMes()
        {
            return PartialView("_PartialViewListaEstados");
        }
        public ActionResult estadosxMesxUsuario()
        {
            return PartialView("_PartialViewListaEstadosUser");
        }
        public ActionResult obtenerFiltro(int? mes)
        {
            IEnumerable<plan_residencia> lista = null;
            IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            if (mes!=null)
            {
                lista = _ServicePlanResidencia.GetEstadosMes(mes);  
            }
            else
            {
                lista = _ServicePlanResidencia.GetPlanResidencia();
            }
           
            return PartialView("_PartialViewListaEstados", lista);
        }

        public ActionResult obtenerFiltroEstadosxUsuario(int user, int? mes)
        {
            IEnumerable<plan_residencia> lista = null;
            IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            lista = _ServicePlanResidencia.GetEstadosCuentaxUsuarioxMes(user,mes);
            return PartialView("_PartialViewListaEstadosUser", lista);
        }
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexEstadosxMes()
        {   
            IEnumerable<plan_residencia> lista = null;
            try
            {
                IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
                lista = _ServicePlanResidencia.GetPlanResidencia();
                ViewBag.title = "Estados Por Mes";
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                ViewBag.listaResidencia = _ServiceResidencia.GetResidencia();
                IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
                ViewBag.listaPlanCobro = _ServicePlanCobro.GetPlanCobro();
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                ViewBag.listaUsuario = _ServiceUsuario.GetUsuario();
                ViewBag.listaMes = listMeses();
                ViewBag.lista = lista;
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult IndexEstadosxMesxUsuario()
        {
            IEnumerable<plan_residencia> lista = null;
            try
            {
                IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
                lista = _ServicePlanResidencia.GetPlanResidencia();
                ViewBag.title = "Estados Por Mes";
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                ViewBag.listaResidencia = _ServiceResidencia.GetResidencia();
                IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
                ViewBag.listaPlanCobro = _ServicePlanCobro.GetPlanCobro();
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                ViewBag.listaUsuario = _ServiceUsuario.GetUsuario();
                ViewBag.listaMes = listMeses();
                ViewBag.lista = lista;
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listMeses(int mes = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Enero", Value = "1" });
            lista.Add(new SelectListItem { Text = "Febrero", Value = "2" });
            lista.Add(new SelectListItem { Text = "Marzo", Value = "3" });
            lista.Add(new SelectListItem { Text = "Abril", Value = "4" });
            lista.Add(new SelectListItem { Text = "Mayo", Value = "5" });
            lista.Add(new SelectListItem { Text = "Junio", Value = "6" });
            lista.Add(new SelectListItem { Text = "Julio", Value = "7" });
            lista.Add(new SelectListItem { Text = "Agosto", Value = "8" });
            lista.Add(new SelectListItem { Text = "Septiembre", Value = "9" });
            lista.Add(new SelectListItem { Text = "Octubre", Value = "10" });
            lista.Add(new SelectListItem { Text = "Noviembre", Value = "11" });
            lista.Add(new SelectListItem { Text = "Diciembre", Value = "13" });

            return new SelectList(lista, "Value", "Text", mes);
        }

    }
}