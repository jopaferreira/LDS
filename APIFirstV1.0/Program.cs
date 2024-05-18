using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GeracaoRelatoriosMVC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Relatorio     // MODEL
    {
        public string Titulo { get; set; }
        public List<string> Dados { get; set; }

        public Relatorio(string titulo, List<string> dados)
        {
            Titulo = titulo;
            Dados = dados;
        }
    }

    public class PdfView
    {
        public void GerarRelatorioPDF(Relatorio relatorio, string caminhoArquivo)
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12);

                gfx.DrawString(relatorio.Titulo, font, XBrushes.Black,
                    new XRect(0, 0, page.Width, 20),
                    XStringFormats.TopCenter);

                for (int i = 0; i < relatorio.Dados.Count; i++)
                {
                    gfx.DrawString(relatorio.Dados[i], font, XBrushes.Black,
                        new XRect(0, 20 + i * 20, page.Width, 20),
                        XStringFormats.TopLeft);
                }

                document.Save(caminhoArquivo);
            }
        }
    }

    public class RelatorioController   // CONTROLLER
    {
        private PdfView pdfView;

        public RelatorioController()
        {
            pdfView = new PdfView();
        }

        public void GerarRelatorio(string titulo, List<string> dados, string caminhoArquivo)
        {
            Relatorio relatorio = new Relatorio(titulo, dados);
            pdfView.GerarRelatorioPDF(relatorio, caminhoArquivo);
        }
    }

    public partial class Form1 : Form    // VIEW
    {
        private RelatorioController relatorioController;
        private TextBox txtTitulo;
        private TextBox txtDados;
        private Button btnGerarRelatorio;

        public Form1()
        {
            InitializeComponent();
            relatorioController = new RelatorioController();
        }

        private void InitializeComponent()
        {
            this.txtTitulo = new TextBox();
            this.txtDados = new TextBox();
            this.btnGerarRelatorio = new Button();
            this.SuspendLayout();

            this.txtTitulo.Location = new System.Drawing.Point(12, 12);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(776, 20);
            this.txtTitulo.TabIndex = 0;
            this.txtTitulo.Text = "Título do Relatório";

            this.txtDados.Location = new System.Drawing.Point(12, 38);
            this.txtDados.Multiline = true;
            this.txtDados.Name = "txtDados";
            this.txtDados.Size = new System.Drawing.Size(776, 370);
            this.txtDados.TabIndex = 1;
            this.txtDados.Text = "Linha 1\r\nLinha 2\r\nLinha 3";

            this.btnGerarRelatorio.Location = new System.Drawing.Point(12, 414);
            this.btnGerarRelatorio.Name = "btnGerarRelatorio";
            this.btnGerarRelatorio.Size = new System.Drawing.Size(776, 23);
            this.btnGerarRelatorio.TabIndex = 2;
            this.btnGerarRelatorio.Text = "Gerar Relatório";
            this.btnGerarRelatorio.UseVisualStyleBackColor = true;
            this.btnGerarRelatorio.Click += new System.EventHandler(this.btnGerarRelatorio_Click);

            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGerarRelatorio);
            this.Controls.Add(this.txtDados);
            this.Controls.Add(this.txtTitulo);
            this.Name = "Form1";
            this.Text = "Gerador de Relatórios";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text;
            List<string> dados = new List<string>(txtDados.Lines);
            string caminhoArquivo = "relatorio.pdf";

            relatorioController.GerarRelatorio(titulo, dados, caminhoArquivo);
            MessageBox.Show("Relatório gerado com sucesso em: " + caminhoArquivo);
        }
    }
}
