using System;
using System.Windows.Forms;
using APIFirst.Views;
using PdfSharp.Fonts;

namespace APIFirst
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            GlobalFontSettings.FontResolver = CustomFontResolver.Instance;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Mostrar ecrã de arranque
            using (var splash = new SplashScreen())
            {
                Application.Run(splash);
            }

            // Mostrar ecrã de opções
            using (var optionForm = new OptionForm())
            {
                if (optionForm.ShowDialog() == DialogResult.OK)
                {
                    // Iniciar o formulário principal
                    Application.Run(new MainForm());
                }
            }
        }
    }
}
