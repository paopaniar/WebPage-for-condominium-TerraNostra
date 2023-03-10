using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TerraNostra.Utils;

namespace TerraNostra.Controllers
{
    public class RubroCobroController : Controller
    {
        // GET: RubroCobro
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


        [HttpPost]
        public ActionResult Save(rubro_cobro rubro)
        {
  
            //Servicio Libro
            IServiceRubroCobro _ServiceRubroCobro = new ServiceRubroCobro();
            try
            {  
                if (ModelState.IsValid)
                {
                    rubro_cobro oRubro = _ServiceRubroCobro.Save(rubro);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    ViewBag.id = listRubros(rubro.id);
                
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (rubro.id > 0)
                    {
                        return (ActionResult)View("Edit", rubro);
                    }
                    else
                    {
                        return View("Create", rubro);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "RubroCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

       
    }
}