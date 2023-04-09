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
                ViewBag.listaReservaciones = _ServiceReservacion.GetReservacion();             
                ViewBag.lista = lista;
                ViewBag.listaValueEstado = listEstados();
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

        public ActionResult _PartialViewListaReservas()
        {
            return PartialView("_PartialViewListaReservas");
        }


        public PartialViewResult reservasxEstado(int? estado)
        {
            IEnumerable<reservacion> lista = null;
            IServiceReservacion _ServiceReservacion = new ServiceReservacion();
            if (estado != null)
            {
                if (estado == 0)
                {
                    lista = _ServiceReservacion.GetReservacion();
                }
                else
                {
                    lista = _ServiceReservacion.GetReservacionesxEstado((int)estado);
                }
            }
            return PartialView("_PartialViewListaReservasEstado", lista);
        }

        public ActionResult IndexEstado()
        {
            IEnumerable<reservacion> lista = null;
            try
            {
                IServiceReservacion _ServiceReservacion = new ServiceReservacion();
                lista = _ServiceReservacion.GetReservacion();
                ViewBag.title = "Reservación";
                IServiceAreaComun _ServiceArea = new ServiceAreaComun();
                ViewBag.listaAreas = _ServiceArea.GetAreaComun();


                ViewBag.listaReservaciones = _ServiceReservacion.GetReservacion();
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                ViewBag.listaUsuarios = listUsuarios();
                ViewBag.lista = lista;
                ViewBag.listaValueEstado = listEstados();
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

        private SelectList listUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<usuario> lista = _ServiceUsuario.GetUsuario();
            return new SelectList(lista, "identificacion", "nombre", idUsuario);
        }

        private SelectList listEstados(int estado=0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Aprobadas", Value = "1" });
            lista.Add(new SelectListItem { Text = "Denegadas", Value = "0" });
            return new SelectList(lista, "Value", "Text", estado);
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
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("ÉXITO!", "Se modificó correctamente", SweetAlertMessageType.success);
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

        public ActionResult reservacionesxUsuarioxEstado()
        {
            return PartialView("_PartialViewListaReservas");
        }

        public ActionResult obtenerFiltro(int user, int? estado)
        {

            IEnumerable<reservacion> lista = null;
            IServiceReservacion _ServiceReservacion = new ServiceReservacion();
            lista = _ServiceReservacion.GetReservacionesxUsuarioxEstado(user, estado);
            return PartialView("_PartialViewListaReservas", lista);
        }

        public ActionResult obtenerFiltro1(int? estado)
        {
            IEnumerable<reservacion> lista = null;
            IServiceReservacion _ServiceReservacion = new ServiceReservacion();
            lista = _ServiceReservacion.GetReservacionesxEstado(estado);
            return PartialView("_PartialViewListaReservasEstado", lista);
        }



    }
}