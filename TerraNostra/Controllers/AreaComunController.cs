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
    public class AreaComunController : Controller
    {
        // GET: AreaComun
        public ActionResult Index()
        {
            IEnumerable<areaComun> lista = null;
            try
            {
                IServiceAreaComun _ServiceAreaComun = new ServiceAreaComun();
                lista = _ServiceAreaComun.GetAreaComun();
                ViewBag.title = "Área Común";

                IServiceReservacion _ServiceReservacion = new ServiceReservacion();
                ViewBag.listaReservas = _ServiceReservacion.GetReservacion();

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
            ServiceAreaComun _ServiceArea = new ServiceAreaComun();
            areaComun areaComun = null;


            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                areaComun = _ServiceArea.GetAreaComunById(Convert.ToInt32(id));


                if (areaComun == null)
                {
                    TempData["Message"] = "No existe la reservación solicitada";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(areaComun);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "AreaComun";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


        }
    }
}