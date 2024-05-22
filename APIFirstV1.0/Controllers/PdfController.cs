using APIFirst.Models;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System.Drawing;
using System.IO;
using System.Xml.Linq;


namespace APIFirst.Controllers
{
    public class PdfController
    {
        public void CreatePdf(PdfDocumentModel model, string outputPath)
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
        }
    }
}
