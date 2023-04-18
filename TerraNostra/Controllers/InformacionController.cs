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
    public class InformacionController : Controller
    {
        // GET: Informacion
        [CustomAuthorize((int)Roles.Administrador)]
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
                ViewBag.Noticias = _ServiceInformacion.GetInformacionByTipo(1);
                ViewBag.Actas = _ServiceInformacion.GetInformacionByTipo(2);
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
            ServiceInformacion _ServiceLibro = new ServiceInformacion();
            informacion informacion = new informacion();
            usuario oUsuario = (usuario)Session["User"];
            informacion.usuario = oUsuario.identificacion;
            ViewBag.tipo = listTipos();
            ViewBag.estados = estado();
            try
            {

                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                informacion = _ServiceLibro.GetInformacionById(Convert.ToInt32(id));
                if (informacion == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados
                ViewBag.tipos = listaTipos();
                return View(informacion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
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

                usuario oUsuario = (usuario)Session["User"];
                //Asignar idUsuario que se encuentra logueado
                informacion.usuario = oUsuario.identificacion;
                if (informacion.estado == null)
                {
                    informacion.estado = 0;
                }
               

                if (ModelState.IsValid)
                {
                    informacion oInformacionI = _ServiceInformacion.Save(informacion);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                   
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
            ViewBag.estado = estado();
            ViewBag.tipos = listTipos();
            return View();
        }

        private SelectList listaTipos(ICollection<informacion> informaciones = null)
        {
            IServiceInformacion _ServiceInformacion = new ServiceInformacion();
            IEnumerable<informacion> lista = _ServiceInformacion.GetInformacion();
            //Seleccionar categorias
            int[] listaInformacionesSelect = null;
            if (informaciones != null)
            {
                listaInformacionesSelect = informaciones.Select(c => c.tipo).ToArray();
            }

            return new SelectList(lista, "tipo", "descTipo", listaInformacionesSelect);
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

                informacion = _ServiceInformacion.GetInformacionById(Convert.ToInt32(id));
              
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
                TempData["Redirect"] = "Informacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


        }
        private SelectList listTipos(int estado = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Noticias", Value = "1" });
            lista.Add(new SelectListItem { Text = "Actas", Value = "2" });
            lista.Add(new SelectListItem { Text = "Artículos", Value = "3" });
            return new SelectList(lista, "Value", "Text", estado);
        }
        private SelectList estado(int estado = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Visible", Value = "0" });
            lista.Add(new SelectListItem { Text = "Oculta", Value = "1" });

            return new SelectList(lista, "Value", "Text", estado);
        }

    }
}