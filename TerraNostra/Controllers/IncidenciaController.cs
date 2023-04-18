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
    //HAY QUE HACER LOS INDEXES
    public class IncidenciaController : Controller
    {
        // GET: Incidencia
        public ActionResult Index()
        {
            IEnumerable<incidente> lista = null;
            try
            {
                IServiceIncidente _ServiceIncidente = new ServiceIncidente();
                lista = _ServiceIncidente.GetIncidente();
                ViewBag.title = "Lista Incidencias";
             
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
        private SelectList listUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<usuario> lista = _ServiceUsuario.GetUsuario();
            return new SelectList(lista, "identificacion", "nombre", idUsuario);
        }
        private SelectList listaTipos(ICollection<incidente> incidencias = null)
        {
            IServiceIncidente _ServiceIncidente = new ServiceIncidente();
            IEnumerable<incidente> lista = _ServiceIncidente.GetIncidente();
            //Seleccionar categorias
            int[] listaIncidencias = null;
            if (incidencias != null)
            {
                listaIncidencias = incidencias.Select(c => c.tipo).ToArray();
            }

            return new SelectList(lista, "tipo", "descTipo", listaIncidencias);
        }
        public ActionResult Edit(int id)
        {
            IServiceIncidente _ServiceIncidente = new ServiceIncidente();
            incidente incidente = _ServiceIncidente.GetIncidenteById(id);
            if (incidente.estado==1)
            {
                incidente.estado = 0;
            }
            else
            {
                incidente.estado = 1;
            }

            try
            {
                // Si va null
                if (ModelState.IsValid)
                {
                    incidente oIncidente = _ServiceIncidente.Save(incidente);
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Se creó la incidencia!.", SweetAlertMessageType.success);
                }

               else
                {
                    TempData["Message"] = "No existe el incidente solicitado";
                    TempData["Redirect"] = "Incidencia";
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
                TempData["Redirect"] = "Incidencia";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Libro/Edit/5
        [HttpPost]
        public ActionResult Save(incidente incidente)
        {
            IServiceIncidente _ServiceIncidente = new ServiceIncidente();
            try
            {
                usuario oUsuario = (usuario)Session["User"];
                incidente.usuario = oUsuario.identificacion;
                if (incidente.estado == null)
                {
                    incidente.estado = 1;
                }

                if (ModelState.IsValid)
                {
                   
                    incidente oIncidenteI = _ServiceIncidente.Save(incidente);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //  ViewBag.idUsuario = listUsuarios(incidente.id);
                    ViewBag.id = listUsuarios(incidente.usuario);
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (incidente.id > 0)
                    {
                        return (ActionResult)View("Edit", incidente);
                    }
                    else
                    {
                        return View("Create", incidente);
                    }
                }
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Se creó la incidencia!.", SweetAlertMessageType.success);
                return RedirectToAction("Index", incidente);
            }

            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Incidencia";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.tipos = listTiposVal();
            return View();
        }


        public ActionResult _PartialViewLista()
        {
            return PartialView("_PartialViewLista");
        }
        public ActionResult incidenciaxEstado()
        {
            return PartialView("_PartialViewLista");
        }
        public ActionResult obtenerFiltro(int? estado)
        {
            IEnumerable<incidente> lista = null;
            IServiceIncidente _ServicePlanResidencia = new ServiceIncidente();
            lista = _ServicePlanResidencia.GetIncidentexEstado(estado);
            return PartialView("_PartialViewLista", lista);
        }

        private SelectList listTiposVal(int tipo = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Apartados", Value = "1" });
            lista.Add(new SelectListItem { Text = "Mantenimiento", Value = "2" });
            lista.Add(new SelectListItem { Text = "Mensaje", Value = "3" });
            lista.Add(new SelectListItem { Text = "Requerimiento", Value = "4" });
          
          

            return new SelectList(lista, "Value", "Text", tipo);
        }
        private SelectList listEstados(int estado = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Pendientes", Value = "1" });
            lista.Add(new SelectListItem { Text = "Resueltas", Value = "0" });
            return new SelectList(lista, "Value", "Text", estado);
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexIncidenciasxEstado()
        {
            IEnumerable<incidente> lista = null;
            try
            {
                IServiceIncidente _ServiceIncidente = new ServiceIncidente();
                lista = _ServiceIncidente.GetIncidente();
                ViewBag.title = "Incidentes Por Estado";
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                ViewBag.listaUsuario = _ServiceUsuario.GetUsuario();
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
    }
}