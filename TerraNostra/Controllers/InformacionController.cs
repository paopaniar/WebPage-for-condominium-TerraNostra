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
    public class InformacionController : Controller
    {
        // GET: Informacion
        public ActionResult Index()
        {
            IEnumerable<informacion> lista = null;
            try
            {
                IServiceInformacion _ServiceInformacion = new ServiceInformacion();
                lista = _ServiceInformacion.GetInformacion();
                ViewBag.title = "Informacion";

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
        public ActionResult Edit(int? id)
        {
            ServiceInformacion _ServiceInformacion = new ServiceInformacion();
            informacion informacion = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    //ATENCION! HACER EL INDEX byPao
                    return RedirectToAction("Index");
                }

                informacion = _ServiceInformacion.GetPlanInformacionById(Convert.ToInt32(id));
                if (informacion == null)
                {
                    TempData["Message"] = "No existe la información solicitada";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados
                ViewBag.idUsuario = listUsuarios(informacion.id);

                return View(informacion);
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

        [HttpPost]
        public ActionResult Save(informacion informacion)
        {

            //Servicio Libro
            IServiceInformacion _ServiceInformacion = new ServiceInformacion();
            try
            {

                if (ModelState.IsValid)
                {
                    informacion oInformacionI = _ServiceInformacion.Save(informacion);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    //  ViewBag.idUsuario = listUsuarios(incidente.id);
                    ViewBag.id = listUsuarios(informacion.usuario);
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (informacion.id > 0)
                    {
                        return (ActionResult)View("Edit", informacion);
                    }
                    else
                    {
                        return View("Create", informacion);
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
            //Que recursos necesito para crear un Libro
            //Autores
            ViewBag.idUsuario = listUsuarios();
           
            return View();
        }


        public ActionResult Details(int id)
        {
            ServiceInformacion _ServiceInformacion = new ServiceInformacion();
            informacion informacion= null;


            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                informacion = _ServiceInformacion.GetPlanInformacionById(Convert.ToInt32(id));
              
                ViewBag.Noticias = _ServiceInformacion.GetInformacionByTipo(informacion.id, 1);
                ViewBag.Actas = _ServiceInformacion.GetInformacionByTipo(informacion.id, 2);

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
                TempData["Redirect"] = "Informacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


        }


    }
}