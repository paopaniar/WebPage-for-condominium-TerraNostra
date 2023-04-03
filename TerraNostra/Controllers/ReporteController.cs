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
        // GET: Reporte
        public ActionResult Index()
        {
            return View("ReporteDeudas");
        }

        public ActionResult ReporteDeudas()
        {
            IServicePlanResidencia _ServiceLibro = new ServicePlanResidencia();
            IEnumerable<plan_residencia> lista = null;
            ViewBag.EstadosPendientes = _ServiceLibro.GetReporteByEstado(0);
            try
            {
                lista = _ServiceLibro.GetPlanResidencia();
                return View(lista);
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
                lista = _ServiceLibro.GetPlanResidencia();

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
                table.AddHeaderCell("Estado");
                table.AddHeaderCell("Total");

                foreach (var item in lista)
                {
                    table.AddCell(new Paragraph(item.residencia.numeroCasa.ToString()));
                    table.AddCell(new Paragraph(item.detalle));
                    table.AddCell(new Paragraph(item.estado.ToString()));
                    table.AddCell(new Paragraph(item.plan_cobro.total.ToString()));

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

    }
}