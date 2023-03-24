﻿using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace TerraNostra.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthenticationFilter]
        public ActionResult Index()
        { if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.NotificationMessage = TempData["mensaje"];
            }
            Log.Info("Inicio");
            IServiceInformacion _ServiceInformacion = new ServiceInformacion();
            var model = _ServiceInformacion.GetInformacion();           
            return View("~/Views/Home/Index.cshtml", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Agrupar()
        {
            ServiceInformacion _ServiceInformacion = new ServiceInformacion();
            informacion informacion = null;


            try
            {
                ViewBag.Noticias = _ServiceInformacion.GetInformacionByTipo( 1);
                ViewBag.Actas = _ServiceInformacion.GetInformacionByTipo( 2);

                if (informacion == null)
                {
                    TempData["Message"] = "No existe la residencia solicitada";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(informacion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Home";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


        }
    }
}