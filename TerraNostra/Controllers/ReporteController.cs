using ApplicationCore.Services;
using Infraestructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TerraNostra.Enum;
using TerraNostra.Security;

namespace TerraNostra.Controllers
{
    public class ReporteController : Controller
    {
        decimal totalSum = 0;
        IEnumerable<plan_residencia> lista = null;
        public ActionResult Index()
        {

            return View("_PartialViewReporte");
        }

        public ActionResult ReporteDeudas()
        {
            IServicePlanResidencia _ServiceLibro = new ServicePlanResidencia();
          
            ViewBag.listaMes = listMeses();
            ViewBag.lista = lista;
            ViewBag.idResidencia = listaResidencias();
            try
            {
                ViewBag.EstadosPendientes = _ServiceLibro.GetReporteByEstado(0);
                lista = _ServiceLibro.GetReporteByEstado(0);
                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult CreatePdfLibroCatalogo()
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<plan_residencia> lista = null;
            try
            {
                // Extraer informacion
                IServicePlanResidencia _ServiceLibro = new ServicePlanResidencia();
                lista = _ServiceLibro.GetReporteByEstado(0);

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Reporte de Deudas")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);

               
                // Crear tabla con 5 columnas 
                Table table = new Table(5, true);


                // los Encabezados
                table.AddHeaderCell("Casa");
                table.AddHeaderCell("Detalle");
                table.AddHeaderCell("Mes");
                table.AddHeaderCell("Estado");
                table.AddHeaderCell("Total");

                foreach (var item in lista)
                {
                    table.AddCell(new Paragraph(item.residencia.numeroCasa.ToString()));
                    table.AddCell(new Paragraph(item.detalle));

                    switch (item.fecha.Month)
                    {
                        case 1:
                            table.AddCell(new Paragraph("Enero"));
                            break;
                        case 2:
                            table.AddCell(new Paragraph("Febrero"));
                            break;
                        case 3:
                            table.AddCell(new Paragraph("Marzo"));
                            break;
                        case 4:
                            table.AddCell(new Paragraph("Abril"));
                            break;
                        case 5:
                            table.AddCell(new Paragraph("Mayo"));
                            break;
                        case 6:
                            table.AddCell(new Paragraph("Junio"));
                            break;
                        case 7:
                            table.AddCell(new Paragraph("Julio"));
                            break;
                        case 8:
                            table.AddCell(new Paragraph("Agosto"));
                            break;
                        case 9:
                            table.AddCell(new Paragraph("Septiembre"));
                            break;
                        case 10:
                            table.AddCell(new Paragraph("Octubre"));
                            break;
                        case 11:
                            table.AddCell(new Paragraph("Noviembre"));
                            break;
                        case 12:
                            table.AddCell(new Paragraph("Diciembre"));
                            break;
                        default:
                            table.AddCell(new Paragraph("No válido"));
                            break;
                    }
                  
                    if (item.estado ==0)
                    {
                        table.AddCell(new Paragraph("Sin pagar"));
                    }

                    if (item.plan_cobro.rubro_cobro != null && item.plan_cobro.rubro_cobro.Any())
                    {
                        totalSum = item.plan_cobro.rubro_cobro.Sum(c => Convert.ToDecimal(c.monto));
                    }
                    table.AddCell(new Paragraph(totalSum.ToString()));

                }
                doc.Add(table);



                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte.pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        public ActionResult _PartialViewReporte()
        {
            return PartialView("_PartialViewReporte");
        }
        public ActionResult obtenerFiltro(int? mes, int? residente, int? estado)
        {
            IEnumerable<plan_residencia> lista = null;
            IServicePlanResidencia _ServicePlanResidencia = new ServicePlanResidencia();
            lista = _ServicePlanResidencia.GetReporteByResidenteByMes(mes, residente, 0);
            return PartialView("_PartialViewReporte", lista);
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

            return new SelectList(lista, "id", "numeroCasa", listaResidenciasSelect);
        }

        private SelectList listMeses(int mes = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "Enero", Value = "1" });
            lista.Add(new SelectListItem { Text = "Febrero", Value = "2" });
            lista.Add(new SelectListItem { Text = "Marzo", Value = "3" });
            lista.Add(new SelectListItem { Text = "Abril", Value = "4" });
            lista.Add(new SelectListItem { Text = "Mayo", Value = "5" });
            lista.Add(new SelectListItem { Text = "Junio", Value = "6" });
            lista.Add(new SelectListItem { Text = "Julio", Value = "7" });
            lista.Add(new SelectListItem { Text = "Agosto", Value = "8" });
            lista.Add(new SelectListItem { Text = "Septiembre", Value = "9" });
            lista.Add(new SelectListItem { Text = "Octubre", Value = "10" });
            lista.Add(new SelectListItem { Text = "Noviembre", Value = "11" });
            lista.Add(new SelectListItem { Text = "Diciembre", Value = "13" });

            return new SelectList(lista, "Value", "Text", mes);
        }

    }
}