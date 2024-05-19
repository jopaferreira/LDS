using PDFGeneratorApp.Controllers;
using PDFGeneratorApp.Models;
using System;
using System.Windows.Forms;

namespace PDFGeneratorApp.Views
{
    public partial class MainForm : Form
    {
        private PdfController pdfController;

        public MainForm()
        {
            InitializeComponent();
            pdfController = new PdfController();
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
            string outputPath = projectDirectory + "/" + "DocumentoGerado.pdf";
            pdfController.CreatePdf(model, outputPath);

            MessageBox.Show("PDF gerado com sucesso!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
