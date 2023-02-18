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
    public class ResidenciaController : Controller
    {
        // GET: Residencia
        public ActionResult Index()
        {
            IEnumerable<residencia> lista = null;
            try
            {
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                lista = _ServiceResidencia.GetResidencia();
                ViewBag.title = "Residencias";
                //Lista usuarios
                //IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                //ViewBag.listaUsuarios = _ServiceUsuario.GetUsuario();
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


    }
}