using ApplicationCore.Services;
using Infraestructure.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TerraNostra.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
         public ActionResult Login(usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            usuario oUsuario = null;
            try
            {
                ModelState.Remove("nombre");
                ModelState.Remove("apellido");
                ModelState.Remove("apellido2");
                ModelState.Remove("telefono");
                ModelState.Remove("rolId");
                //Verificar las credenciales
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.Email, usuario.password);
                    if (oUsuario != null)
                    {
                       Session["User"] = oUsuario;
                        Log.Info($"Inicio sesion: {usuario.Email}");
                        TempData["mensaje"] = TerraNostra.Utils.SweetAlertHelper.Mensaje("Login",
                            "Usuario autenticado", TerraNostra.Utils.SweetAlertMessageType.success
                            );
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"Intento de inicio: {usuario.Email}");
                        ViewBag.NotificationMessage = TerraNostra.Utils.SweetAlertHelper.Mensaje("Login",
                            "Usuario no válido", TerraNostra.Utils.SweetAlertMessageType.error
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View("Index");
        }
        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "No autorizado";
            if (Session["User"] != null)
            {
                usuario usuario = Session["User"] as usuario;
                Log.Warn($"No autorizado {usuario.Email}");
            }
            return View();
        }
        public ActionResult Logout()
        {
            try
            {
                //Eliminar variable de sesion
                Session["User"] = null;
                Session.Remove("User");

                return RedirectToAction("Index", "Login");
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
