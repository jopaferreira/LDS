using System;
using System.Windows.Forms;
using System.Drawing;

namespace APIFirst.Views
{
    public class SplashScreen : Form
    {
        private System.Windows.Forms.Timer timer;
        private int timeLeft;
        private Label titleLabel;
        private Label countdownLabel;
        private Panel borderPanel;

        public SplashScreen()
        {
            this.Text = "APIFirst";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(420, 220);
            this.BackColor = Color.White;

            borderPanel = new Panel();
            borderPanel.BorderStyle = BorderStyle.FixedSingle;
            borderPanel.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 20);
            borderPanel.Location = new Point(10, 10);
            borderPanel.BackColor = Color.White;
            this.Controls.Add(borderPanel);

            titleLabel = new Label();
            titleLabel.Text = "APIFirst";
            titleLabel.Font = new Font("Verdana", 24, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point((borderPanel.Width - titleLabel.Width) / 2, (borderPanel.Height - titleLabel.Height) / 3);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            countdownLabel = new Label();
            countdownLabel.Font = new Font("Verdana", 14, FontStyle.Regular);
            countdownLabel.AutoSize = true;
            countdownLabel.Location = new Point((borderPanel.Width - countdownLabel.Width) / 2, (borderPanel.Height - countdownLabel.Height) * 2 / 3);
            countdownLabel.TextAlign = ContentAlignment.MiddleCenter;

            borderPanel.Controls.Add(titleLabel);
            borderPanel.Controls.Add(countdownLabel);

            timeLeft = 5; // 5 seconds for the countdown
            countdownLabel.Text = $"Aguarde {timeLeft} segundos";

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            countdownLabel.Text = $"Aguarde {timeLeft} segundos";
            if (timeLeft <= 0)
            {
                timer.Stop();
                this.Close();
            }
        }
    }
}
