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
                //Asignar idUsuario que se encuentra logueado
               // usuario.identificacion = oUsuario.identificacion;
                if (usuario.estado == null)
                {
                    usuario.estado = 0;
                }

                if (ModelState.IsValid)
                {
                    usuario oUsuario1 = _ServiceUsuario.Save(usuario);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
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
           
            return View();
        }
    }
}