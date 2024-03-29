﻿using ApplicationCore.Services;
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
                ViewBag.lista = lista;
                ViewBag.listTipos = listTipos();
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


        public ActionResult Edit(int id)
        {
            usuario oUsuario = (usuario)Session["User"];
            IServiceAreaComun _ServiceAreaComun = new ServiceAreaComun();
            areaComun areaComun = _ServiceAreaComun.GetAreaComunById(id);
            ServiceReservacion reserva = new ServiceReservacion();
            reservacion reservacion = new reservacion();
            reservacion.areaComunId = areaComun.id;
            reservacion.usuario = oUsuario.identificacion;
            reservacion.detalle = "Prueba";
            reservacion.estado = 2;
            

            if (areaComun.estado == 1)
            {
                areaComun.estado = 0;
            }
            else
            {
                areaComun.estado = 0;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    areaComun oAreaComun = _ServiceAreaComun.Save(areaComun);
                    reservacion oReservacion = reserva.Save(reservacion);

                }

                else
                {
                    TempData["Message"] = "No existe el incidente solicitado";
                    TempData["Redirect"] = "AreaComun";
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
                TempData["Redirect"] = "AreaComun";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        public PartialViewResult _PartialViewByTipo(string tipo)
        {
            IEnumerable<areaComun> lista = null;
            IServiceAreaComun _ServiceReservacion = new ServiceAreaComun();
            if (tipo != null)
            {
                if (tipo == null)
                {
                    lista = _ServiceReservacion.GetAreaComun();
                }
                else
                {
                    lista = _ServiceReservacion.GetAreasByTipo(tipo);
                }
            }
            return PartialView("_PartialViewByTipo", lista);
        }

        public ActionResult obtenerFiltro(string tipo)
        {
            IEnumerable<areaComun> lista = null;
            IServiceAreaComun _ServiceReservacion = new ServiceAreaComun();
            lista = _ServiceReservacion.GetAreasByTipo(tipo);
            return PartialView("_PartialViewByTipo", lista);
        }

        private SelectList listTipos(int tipo = 0)
        {
            IServiceAreaComun _ServiceReservacion = new ServiceAreaComun();
            IEnumerable<areaComun> lista = _ServiceReservacion.GetAreaComun();
            return new SelectList(lista, "detalle", "detalle", tipo);
        }


        [HttpPost]
        public ActionResult Save(areaComun area)
        {
            IServiceAreaComun _ServiceRubro = new ServiceAreaComun();
            
            try
            {

                if (ModelState.IsValid)
                {
                    area.estado = 1;
                    area.disponibilidad = 1;
                    area.horaDisponible = "Todo el día.";
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Realizado correctamente!", SweetAlertMessageType.success);
                    areaComun oRubroCobro = _ServiceRubro.Save(area);
                }
                else
                {
                    Utils.Util.ValidateErrors(this);

                    if (area.id > 0)
                    {
                        return (ActionResult)View("Edit", area);
                    }
                    else
                    {
                        return View("Create", area);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            return View();
        }

    }
}