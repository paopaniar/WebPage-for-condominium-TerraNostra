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
           
            //Servicio Libro
            IServiceIncidente _ServiceIncidente= new ServiceIncidente();
            try
            {
                
                if (ModelState.IsValid)
                {
                    incidente oIncidenteI= _ServiceIncidente.Save(incidente);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                  //  ViewBag.idUsuario = listUsuarios(incidente.id);
                    ViewBag.id = listUsuarios(incidente.usuario);
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (incidente.id> 0)
                    {
                        return (ActionResult)View("Edit", incidente);
                    }
                    else
                    {
                        return View("Create", incidente);
                    }
                }

                return RedirectToAction("Index");
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
            ViewBag.idUsuario = listUsuarios();
            return View();
        }
    }
}