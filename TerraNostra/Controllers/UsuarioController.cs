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
	public class UsuarioController : Controller
	{
		[CustomAuthorize((int)Roles.Administrador)]

        public ActionResult Index()
        {
            IEnumerable<usuario> lista = null;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuario();
                ViewBag.title = "Usuarios";          
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


        [HttpPost]
        public ActionResult Save(usuario usuario)
        {

            //Servicio Libro
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {

                usuario oUsuario = (usuario)Session["User"];
                
                if (ModelState.IsValid)
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Se ha guardado el usuario.", SweetAlertMessageType.success);
                    usuario oUsuario1 = _ServiceUsuario.Save(usuario);
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Realizado correctamente!", SweetAlertMessageType.success);
                }
                else
                {
                    
                    Utils.Util.ValidateErrors(this);

                    if (usuario.identificacion > 0)
                    {
                        return (ActionResult)View("Edit", usuario);
                    }
                    else
                    {
                        return View("Create", usuario);
                    }
                }
               
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Se ha guardado el usuario.", SweetAlertMessageType.success);
           
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Informacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.estado = estdo();
            ViewBag.rol = rol();
            return View();
        }

        public ActionResult Edit(int id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            usuario usuario = _ServiceUsuario.GetUsuarioByID(id);
            if (usuario.estado == 1)
            {
                usuario.estado = 0;
            }
         
            try
            {
                // Si va null
                if (ModelState.IsValid)
                {
                    usuario oUsuario = _ServiceUsuario.Save(usuario);

                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Éxito", "Se ha eliminado el usuario.", SweetAlertMessageType.success);
                    return RedirectToAction("Index", "Usuario");

                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("ÉXITO!", "Se eliminó correctamente", SweetAlertMessageType.success);

                }

                else
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

            }


            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        private SelectList estdo(int estado = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Activo", Value = "1" });
            lista.Add(new SelectListItem { Text = "Inactivo", Value = "0" });
            return new SelectList(lista, "Value", "Text", estado);
        }
        private SelectList rol(int rol = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Administrador", Value = "1" });
            lista.Add(new SelectListItem { Text = "Residente", Value = "2" });
            return new SelectList(lista, "Value", "Text", rol);
        }

        public ActionResult cambiarContraseña(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            usuario usuario = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                usuario = _ServiceUsuario.GetUsuarioByID(Convert.ToInt32(id));
                if (usuario == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                
                return View(usuario);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


    }
}