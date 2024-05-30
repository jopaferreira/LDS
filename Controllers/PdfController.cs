using APIFirst.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;

namespace APIFirst.Controllers
{
    public class PdfController
    {
        public delegate void PdfGeneratedEventHandler(string message);
        public event PdfGeneratedEventHandler PdfGenerated;

        public void CreatePdf(PdfDocumentModel model, string outputPath)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = model.Title;

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont titleFont = new XFont("Verdana", 20);
                XFont contentFont = new XFont("Verdana", 12);

                gfx.DrawString(model.Title, titleFont, XBrushes.Black, new XRect(0, 0, page.Width, 50), XStringFormats.Center);
                gfx.DrawString(model.Content, contentFont, XBrushes.Black, new XRect(20, 60, page.Width - 40, page.Height - 80), XStringFormats.TopLeft);

                document.Save(outputPath);

                PdfGenerated?.Invoke("PDF criado com sucesso!");
            }
            catch (Exception ex)
            {
                // Tratar exceções aqui
                PdfGenerated?.Invoke($"Erro ao criar PDF: {ex.Message}");
            }
        }
    }
}
