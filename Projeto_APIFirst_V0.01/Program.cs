using System;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using PdfSharp.Drawing;

namespace APIFirst_V02
{
    public class Controller
    {
        private readonly View _view;
        private readonly Model _model;

        private PdfDocument _document;

        public Controller()
        {
            _view = new View(this);
            _model = new Model();
        }

        public void CreatePDF(string title)
        {
            _document = new PdfDocument();
            _document.Info.Title = title;
            _view.UpdateStatus($"PDF '{title}' created.");
        }

        public void AddPage()
        {
            if (_document != null)
            {
                PdfPage page = _document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12);
                gfx.DrawString("This is a new page.", font, XBrushes.Black, new XRect(0, 0, page.Width, 20), XStringFormats.TopLeft);
                _view.UpdateStatus("New page added to the PDF.");
            }
            else
            {
                _view.UpdateStatus("No PDF document created yet. Please create a PDF first.");
            }
        }

        public void AddNoteToPage(int pageIndex, string author, string content)
        {
            if (_document != null && pageIndex >= 0 && pageIndex < _document.PageCount)
            {
                PdfPage page = _document.Pages[pageIndex];
                XRect rect = new XRect(100, 100, 200, 50);
                PdfTextAnnotation textAnnotation = new PdfTextAnnotation(page, rect);
                textAnnotation.Contents = content;
                textAnnotation.Author = author;
                textAnnotation.Elements.Add("/Subtype", new PdfLiteral("/Text"));
                page.Annotations.Add(textAnnotation);
                _view.UpdateStatus($"Note added to page {pageIndex}.");
            }
            else
            {
                _view.UpdateStatus("Invalid page index or no PDF document created yet. Please check your input.");
            }
        }

        public void Run()
        {
            Application.Run(_view);
        }
    }

    public class View : Form
    {
        private readonly Controller _controller;
        private TextBox _titleTextBox;
        private Button _createButton;
        private Button _addPageButton;
        private Button _addNoteButton;
        private TextBox _pageIndexTextBox;
        private TextBox _authorTextBox;
        private TextBox _contentTextBox;
        private TextBox _statusTextBox;

        public View(Controller controller)
        {
            _controller = controller;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _titleTextBox = new TextBox();
            _titleTextBox.Location = new System.Drawing.Point(10, 10);
            _titleTextBox.Size = new System.Drawing.Size(200, 20);

            _createButton = new Button();
            _createButton.Text = "Create PDF";
            _createButton.Location = new System.Drawing.Point(220, 10);
            _createButton.Click += CreateButtonClick;

            _addPageButton = new Button();
            _addPageButton.Text = "Add Page";
            _addPageButton.Location = new System.Drawing.Point(10, 40);
            _addPageButton.Click += AddPageButtonClick;

            _addNoteButton = new Button();
            _addNoteButton.Text = "Add Note";
            _addNoteButton.Location = new System.Drawing.Point(10, 100);
            _addNoteButton.Click += AddNoteButtonClick;

            _pageIndexTextBox = new TextBox();
            _pageIndexTextBox.Location = new System.Drawing.Point(120, 70);
            _pageIndexTextBox.Size = new System.Drawing.Size(50, 20);

            _authorTextBox = new TextBox();
            _authorTextBox.Location = new System.Drawing.Point(10, 70);
            _authorTextBox.Size = new System.Drawing.Size(100, 20);

            _contentTextBox = new TextBox();
            _contentTextBox.Location = new System.Drawing.Point(10, 130);
            _contentTextBox.Size = new System.Drawing.Size(200, 20);

            _statusTextBox = new TextBox();
            _statusTextBox.Location = new System.Drawing.Point(10, 160);
            _statusTextBox.Size = new System.Drawing.Size(300, 50);
            _statusTextBox.Multiline = true;
            _statusTextBox.ReadOnly = true;

            Controls.Add(_titleTextBox);
            Controls.Add(_createButton);
            Controls.Add(_addPageButton);
            Controls.Add(_addNoteButton);
            Controls.Add(_pageIndexTextBox);
            Controls.Add(_authorTextBox);
            Controls.Add(_contentTextBox);
            Controls.Add(_statusTextBox);
        }

        private void CreateButtonClick(object sender, EventArgs e)
        {
            _controller.CreatePDF(_titleTextBox.Text);
        }

        private void AddPageButtonClick(object sender, EventArgs e)
        {
            _controller.AddPage();
        }

        private void AddNoteButtonClick(object sender, EventArgs e)
        {
            int pageIndex;
            if (int.TryParse(_pageIndexTextBox.Text, out pageIndex))
            {
                _controller.AddNoteToPage(pageIndex, _authorTextBox.Text, _contentTextBox.Text);
            }
            else
            {
                UpdateStatus("Invalid page index. Please enter a valid number.");
            }
        }

        public void UpdateStatus(string status)
        {
            _statusTextBox.AppendText(status + Environment.NewLine);
        }
    }

    public class Model
    {
        public Model() { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Run();
        }
    }
}
