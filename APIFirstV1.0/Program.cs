using System;
using System.Windows.Forms;
using PDFGeneratorApp.Views;
using PdfSharp.Fonts;

namespace PDFGeneratorApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            GlobalFontSettings.FontResolver = CustomFontResolver.Instance;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
