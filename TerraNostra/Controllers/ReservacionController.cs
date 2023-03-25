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
    public class ReservacionController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<reservacion> lista = null;
            try
            {
                IServiceReservacion _ServiceReservacion = new ServiceReservacion();
                lista = _ServiceReservacion.GetReservacion();
                ViewBag.title = "Reservación";

                IServiceAreaComun _ServiceArea = new ServiceAreaComun();
                ViewBag.listaAreas = _ServiceArea.GetAreaComun();
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                ViewBag.listaUsuarios = _ServiceUsuario.GetUsuario();

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

        private SelectList listaAreas(ICollection<areaComun> areas = null)
        {
            IServiceAreaComun _ServiceArea = new ServiceAreaComun();
            IEnumerable<areaComun> lista = _ServiceArea.GetAreaComun();
            //Seleccionar categorias
            int[] listaAreasSelect = null;
            if (areas != null)
            {
                listaAreasSelect = areas.Select(c => c.id).ToArray();
            }

            return new SelectList(lista, "id", "detalle", listaAreasSelect);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.areas = listaAreas();
            return View();
        }



        [HttpPost]
        public ActionResult Save(reservacion reservacion)
        {

            IServiceReservacion _ServiceReservacion = new ServiceReservacion();

            
            try
            {
              
                if (ModelState.IsValid)
                {
                    reservacion oReservacionI = _ServiceReservacion.Save(reservacion);
                }
                else
                {
                    Utils.Util.ValidateErrors(this);

                    if (reservacion.id > 0)
                    {
                        return (ActionResult)View("Edit", reservacion);
                    }
                    else
                    {
                        return View("Create", reservacion);
                     }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Reservacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }



        public ActionResult Details(int id)
        {
            ServiceReservacion _ServiceReservacion = new ServiceReservacion();
            reservacion reservacion = null;


            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                reservacion = _ServiceReservacion.GetReservacionById(Convert.ToInt32(id));


                if (reservacion == null)
                {
                    TempData["Message"] = "No existe la reservación solicitada";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(reservacion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Reservacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


        }


        public ActionResult Edit(int id)
        {
            IServiceReservacion _ServiceReservacion = new ServiceReservacion();
            reservacion reservacion = _ServiceReservacion.GetReservacionById(id);
            if (reservacion.estado == 1)
            {
                reservacion.estado = 0;
            }
            else
            {
                reservacion.estado = 1;
            }

            try
            {
                // Si va null
                if (ModelState.IsValid)
                {
                    reservacion oReservacion = _ServiceReservacion.SaveEstado(reservacion);

                }

                else
                {
                    TempData["Message"] = "No existe el incidente solicitado";
                    TempData["Redirect"] = "Reservacion";
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
                TempData["Redirect"] = "Reservacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}