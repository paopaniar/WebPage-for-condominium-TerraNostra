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
    public class ResidenciaController : Controller
    {
        // GET: Residencia
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<residencia> lista = null;
            try
            {
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                lista = _ServiceResidencia.GetResidencia();
                ViewBag.title = "Residencias";
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

        //GET Residencia detalle
        public ActionResult Details(int? id)
        {
            ServiceResidencia _ServiceResidencia = new ServiceResidencia();
            residencia residencia = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Details");
                }

                residencia = _ServiceResidencia.GetResidenciaByID(Convert.ToInt32(id));
                if (residencia == null)
                {
                    TempData["Message"] = "No existe la residencia solicitada";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(residencia);
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

        private SelectList listUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<usuario> lista = _ServiceUsuario.GetUsuario();
            return new SelectList(lista, "identificacion", "nombre", idUsuario);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.id = listUsuarios();
            return View();
        }

        [HttpPost]
        public ActionResult Save(residencia residencia)
        {

            //Servicio Libro
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            try
            {

                if (ModelState.IsValid)
                {
                    residencia oResidenciaI = _ServiceResidencia.Save(residencia);
                }
                else
                {
                    
                    Utils.Util.ValidateErrors(this);
                  
                    ViewBag.id = listUsuarios(residencia.usuario1.identificacion);
                    
                    if (residencia.id > 0)
                    {
                        return (ActionResult)View("Edit", residencia);
                    }
                    else
                    {
                        return View("Create", residencia);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Residencia";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}