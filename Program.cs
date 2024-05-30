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

            // Mostrar ecr� de arranque
            using (var splash = new SplashScreen())
            {
                Application.Run(splash);
            }

            // Mostrar ecr� de op��es
            using (var optionForm = new OptionForm())
            {
                if (optionForm.ShowDialog() == DialogResult.OK)
                {
                    // Iniciar o formul�rio principal
                    Application.Run(new MainForm());
                }
            }
        }
    }
}
