using System;
using System.Windows.Forms;
using System.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using APIFirst.Controllers;
using APIFirst.Models;

namespace APIFirst.Views
{
    public class OptionForm : Form
    {
        private Button generatePdfButton;
        private Button addPdfsButton;
        private Button exitButton;

        public OptionForm()
        {
            this.Text = "Escolha uma Opção:";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 500);

            generatePdfButton = new Button();
            generatePdfButton.Text = "1 - Gerar PDF";
            generatePdfButton.Font = new Font("Verdana", 14, FontStyle.Regular);
            generatePdfButton.Size = new Size(300, 50);
            generatePdfButton.Location = new Point((this.Width - generatePdfButton.Width) / 2, (this.Height - generatePdfButton.Height) / 4);
            generatePdfButton.Click += new EventHandler(GeneratePdfButton_Click);

            addPdfsButton = new Button();
            addPdfsButton.Text = "2 - Adicionar PDF's";
            addPdfsButton.Font = new Font("Verdana", 14, FontStyle.Regular);
            addPdfsButton.Size = new Size(300, 50);
            addPdfsButton.Location = new Point((this.Width - addPdfsButton.Width) / 2, (this.Height - addPdfsButton.Height) / 2);
            addPdfsButton.Click += new EventHandler(AddPdfsButton_Click);

            exitButton = new Button();
            exitButton.Text = "3 - Sair do Programa";
            exitButton.Font = new Font("Verdana", 14, FontStyle.Regular);
            exitButton.Size = new Size(300, 50);
            exitButton.Location = new Point((this.Width - exitButton.Width) / 2, 3 * (this.Height - exitButton.Height) / 4);
            exitButton.Click += new EventHandler(ExitButton_Click);

            this.Controls.Add(generatePdfButton);
            this.Controls.Add(addPdfsButton);
            this.Controls.Add(exitButton);
        }

        private void GeneratePdfButton_Click(object sender, EventArgs e)
        {
            string outputPdfPath = GetOutputPdfPath("Gerar PDF");
            if (!string.IsNullOrEmpty(outputPdfPath))
            {
                PdfDocumentModel model = GetPdfContent();
                if (model != null)
                {
                    PdfController pdfController = new PdfController();
                    pdfController.CreatePdf(model, outputPdfPath);
                    MessageBox.Show("PDF gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void AddPdfsButton_Click(object sender, EventArgs e)
        {
            int pdfCount = GetPdfCount();
            if (pdfCount > 0)
            {
                var pdfPaths = SelectPdfs(pdfCount);
                if (pdfPaths != null && pdfPaths.Length == pdfCount)
                {
                    string outputPdfPath = GetOutputPdfPath("Adicionar PDFs");
                    if (!string.IsNullOrEmpty(outputPdfPath))
                    {
                        ConcatenatePdfs(pdfPaths, outputPdfPath);
                        MessageBox.Show("PDFs concatenados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private int GetPdfCount()
        {
            int pdfCount = 0;
            using (var inputBox = new InputBox("Quantos PDFs quer unir?", "Quantidade de PDFs"))
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(inputBox.InputText, out pdfCount);
                }
            }
            return pdfCount;
        }

        private string[] SelectPdfs(int count)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.FileNames.Length == count)
                    {
                        return openFileDialog.FileNames;
                    }
                    else
                    {
                        MessageBox.Show($"Por favor, selecione exatamente {count} ficheiros PDF.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return null;
        }

        private string GetOutputPdfPath(string title)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = title;
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
            }
            return null;
        }

        private PdfDocumentModel GetPdfContent()
        {
            using (var inputBox = new InputBox("Introduza o conteúdo do PDF", "Conteúdo do PDF"))
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    return new PdfDocumentModel
                    {
                        Title = "PDF Gerado",
                        Content = inputBox.InputText
                    };
                }
            }
            return null;
        }

        private void ConcatenatePdfs(string[] pdfPaths, string outputPdfPath)
        {
            using (PdfDocument outputDocument = new PdfDocument())
            {
                foreach (string pdfPath in pdfPaths)
                {
                    using (PdfDocument inputDocument = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import))
                    {
                        for (int idx = 0; idx < inputDocument.PageCount; idx++)
                        {
                            outputDocument.AddPage(inputDocument.Pages[idx]);
                        }
                    }
                }
                outputDocument.Save(outputPdfPath);
            }
        }
    }

    public class InputBox : Form
    {
        private Label label;
        private TextBox textBox;
        private Button okButton;
        private Button cancelButton;

        public string InputText => textBox.Text;

        public InputBox(string prompt, string title)
        {
            this.Text = title;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 150);

            label = new Label();
            label.Text = prompt;
            label.Location = new Point(10, 10);
            label.Size = new Size(360, 20);

            textBox = new TextBox();
            textBox.Location = new Point(10, 40);
            textBox.Size = new Size(360, 20);

            okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new Point(210, 70);
            okButton.Click += new EventHandler(OkButton_Click);

            cancelButton = new Button();
            cancelButton.Text = "Cancelar";
            cancelButton.Location = new Point(290, 70);
            cancelButton.Click += new EventHandler(CancelButton_Click);

            this.Controls.Add(label);
            this.Controls.Add(textBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
