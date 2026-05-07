using System;
using System.Drawing;
using System.Windows.Forms;

namespace syncademia
{
    public partial class LoadingOverlay : Form
    {
        private System.Windows.Forms.Timer animationTimer;
        private int dotState = 0;
        private Label lblMessage;
        private Guna.UI2.WinForms.Guna2Panel roundedPanel;
        private Form _parentForm;

        public LoadingOverlay(string message = "Please wait...")
        {
            InitializeComponent();
            lblMessage.Text = message;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.ShowInTaskbar = false;
            this.TopMost = true; // Keep on top
        }

        private void InitializeComponent()
        {
            this.roundedPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.roundedPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // roundedPanel
            // 
            this.roundedPanel.BackColor = System.Drawing.Color.White;
            this.roundedPanel.BorderColor = System.Drawing.Color.RoyalBlue;
            this.roundedPanel.BorderRadius = 20;
            this.roundedPanel.BorderThickness = 2;
            this.roundedPanel.Dock = DockStyle.Fill;
            this.roundedPanel.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel.Name = "roundedPanel";
            this.roundedPanel.Size = new System.Drawing.Size(200, 120);
            this.roundedPanel.TabIndex = 0;

            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblMessage.Location = new System.Drawing.Point(40, 45);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(120, 25);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Loading...";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.Anchor = AnchorStyles.None;

            // 
            // LoadingOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(200, 120);
            this.Controls.Add(this.roundedPanel);
            this.Name = "LoadingOverlay";
            this.Text = "LoadingOverlay";
            this.roundedPanel.Controls.Add(this.lblMessage);
            this.roundedPanel.ResumeLayout(false);
            this.roundedPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void ShowWithAnimation(Form parentForm)
        {
            _parentForm = parentForm;
            // Disable the parent form to prevent interaction
            _parentForm.Enabled = false;
            
            this.Owner = _parentForm;
            this.Show(parentForm);
            this.CenterToParent();
            
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 300;
            animationTimer.Tick += (s, e) => AnimateDots();
            animationTimer.Start();
        }

        private void AnimateDots()
        {
            dotState = (dotState + 1) % 4;
            string dots = "";
            for (int i = 0; i < dotState; i++) dots += ".";
            lblMessage.Text = $"Loading{dots}";
            if (dotState == 0) lblMessage.Text = "Loading";
        }

        public new void Close()
        {
            animationTimer?.Stop();
            animationTimer?.Dispose();
            // Re-enable the parent form before closing
            if (_parentForm != null && !_parentForm.IsDisposed)
                _parentForm.Enabled = true;
            base.Close();
        }
    }
}