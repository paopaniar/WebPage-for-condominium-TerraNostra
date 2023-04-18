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
    public class RubroCobroController : Controller
    {
        // GET: RubroCobro
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<rubro_cobro> lista = null;
            try
            {
                IServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
                lista = _ServiceRubro.GetRubroCobro();
                ViewBag.title = "Rubros de Cobro";
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

        private SelectList listRubros(int id = 0)
        {
            IServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
            IEnumerable<rubro_cobro> lista = _ServiceRubro.GetRubroCobro();
            return new SelectList(lista, "id", "detalle", id);
        }

       
        public ActionResult Create()
        {
            ViewBag.idRubros = listRubros();
            ViewBag.estado = listEstados();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.estado = listEstados();

            ServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
            rubro_cobro rubroCobro = null;
            try
            {
                if (id == null)
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Se guardó correctamente!", SweetAlertMessageType.success);
                    return RedirectToAction("Index");
                }

                rubroCobro = _ServiceRubro.GetRubroCobroById(Convert.ToInt32(id));
                if (rubroCobro == null)
                {
                    TempData["Message"] = "No existe la información solicitada";
                    TempData["Redirect"] = "RubroCobro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados
                

                return View(rubroCobro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Informacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpPost]
        public ActionResult Save(rubro_cobro rubro)
        {   
            IServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Realizado correctamente!", SweetAlertMessageType.success);
                    rubro_cobro oRubroCobro = _ServiceRubro.Save(rubro);
                }
                else
                {
                    Utils.Util.ValidateErrors(this);                 
                
                    if (rubro.id > 0)
                    {
                        return (ActionResult)View("Edit", rubro);
                    }
                    else
                    {
                        return View("Create", rubro);
                    }
                }
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Realizado correctamente!", SweetAlertMessageType.success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "RubroCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listEstados(int estado = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Activo", Value = "1" });
            lista.Add(new SelectListItem { Text = "Inactivo", Value = "0" });
            return new SelectList(lista, "Value", "Text", estado);
        }

    }
}