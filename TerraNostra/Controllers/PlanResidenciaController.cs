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
    public class PlanResidenciaController : Controller
    {
        // GET: PlanResidencia
        public ActionResult Index()
        {
            IEnumerable<plan_residencia> lista = null;
            try
            {
                IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
                lista = _ServicePlanResidencia.GetPlanResidencia();
                ViewBag.title = "Lista planes residencia";
                //Lista autores
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                ViewBag.listaResidencia = _ServiceResidencia.GetResidencia();
                IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
                ViewBag.listaPlanCobro = _ServicePlanCobro.GetPlanCobro();
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

        public ActionResult Details(int id)
        {
            ServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            plan_residencia p_residencia = null;

         
            try
            {
                // Si va null
                    if (id == null)
                {
                    return RedirectToAction("Index");
                }

                p_residencia = _ServicePlanResidencia.GetPlanResidenciaByID(Convert.ToInt32(id));
                IServicePlanResidencia _ServiceResidencia = new ServicePlanResidencia();
                ViewBag.EstadosPagados = _ServicePlanResidencia.GetEstadosByEstado(p_residencia.residenciaId,1);
                ViewBag.EstadosPendientes = _ServicePlanResidencia.GetEstadosByEstado(p_residencia.residenciaId,0);

                if (p_residencia == null)
                {
                    TempData["Message"] = "No existe la residencia solicitada";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(p_residencia);
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
        [HttpGet]
        public ActionResult Create()
        {
         
            return View();
        }
        private SelectList listaResidencias(ICollection<residencia> residencias = null)
        {
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            IEnumerable<residencia> lista = _ServiceResidencia.GetResidencia();
            //Seleccionar categorias
            int[] listaResidenciasSelect = null;
            if (residencias != null)
            {
                listaResidenciasSelect = residencias.Select(c => c.id).ToArray();
            }

            return new SelectList(lista, "id", "otherInfoDetails", listaResidenciasSelect);
        }
        [HttpPost]
        public ActionResult Save(plan_residencia plan_residencia, string[] selectedResidencia)
        {
            IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            try
            {

                if (ModelState.IsValid)
                {
                    plan_residencia oPlanResidenciaI = _ServicePlanResidencia.Save(plan_residencia, selectedResidencia);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //  ViewBag.idUsuario = listUsuarios(incidente.id);
                    ViewBag.idResidencica = listaResidencias();
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (plan_residencia.id > 0)
                    {
                        return (ActionResult)View("Edit", plan_residencia.residencia);
                    }
                    else
                    {
                        return View("Create", plan_residencia);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PlanCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

    }
}