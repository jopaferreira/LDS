using APIFirst.Controllers;
using APIFirst.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace APIFirst.Views
{
    public partial class MainForm : Form
    {
        private PdfController pdfController;

        public MainForm()
        {
            InitializeComponent();
            pdfController = new PdfController();
            pdfController.PdfGenerated += PdfController_PdfGenerated;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text;
            string content = contentTextBox.Text;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Por favor, informe o título e o conteúdo.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PdfDocumentModel model = new PdfDocumentModel
            {
                Title = title,
                Content = content
            };


            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string outputPath = Path.Combine(projectDirectory, "DocumentoGerado.pdf");
            pdfController.CreatePdf(model, outputPath);
        }

        private void PdfController_PdfGenerated(string message)
        {
            MessageBox.Show(message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
