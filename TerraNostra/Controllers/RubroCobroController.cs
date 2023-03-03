﻿using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

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
    }
}