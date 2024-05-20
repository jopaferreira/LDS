namespace PDFGeneratorApp.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            titleTextBox = new TextBox();
            contentTextBox = new TextBox();
            generateButton = new Button();
            SuspendLayout();
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(12, 12);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.PlaceholderText = "Título";
            titleTextBox.Size = new Size(407, 27);
            titleTextBox.TabIndex = 0;
            // 
            // contentTextBox
            // 
            contentTextBox.Location = new Point(12, 38);
            contentTextBox.Multiline = true;
            contentTextBox.Name = "contentTextBox";
            contentTextBox.PlaceholderText = "Conteúdo";
            contentTextBox.Size = new Size(407, 150);
            contentTextBox.TabIndex = 1;
            // 
            // generateButton
            // 
            generateButton.Location = new Point(12, 194);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(122, 40);
            generateButton.TabIndex = 2;
            generateButton.Text = "Gerar PDF";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += GenerateButton_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(431, 246);
            Controls.Add(generateButton);
            Controls.Add(contentTextBox);
            Controls.Add(titleTextBox);
            Name = "MainForm";
            Text = "PDF Generator";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox contentTextBox;
        private System.Windows.Forms.Button generateButton;
    }
}
