namespace syncademia
{
    partial class VerifySignUp
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.txtCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnVerify = new Guna.UI2.WinForms.Guna2Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblInfo
            this.lblInfo.Text = "Please enter the verification code sent to your email";
            this.lblInfo.Location = new System.Drawing.Point(30, 30);
            this.lblInfo.Size = new System.Drawing.Size(280, 40);
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtCode
            this.txtCode.Location = new System.Drawing.Point(50, 90);
            this.txtCode.PlaceholderText = "6-Digit Code";
            this.txtCode.Size = new System.Drawing.Size(240, 40);
            this.txtCode.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;

            // btnVerify
            this.btnVerify.Location = new System.Drawing.Point(80, 160);
            this.btnVerify.Size = new System.Drawing.Size(180, 45);
            this.btnVerify.Text = "Verify and Register";
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);

            // Form settings
            this.ClientSize = new System.Drawing.Size(340, 250);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.btnVerify);
            this.Name = "VerifySignUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Email Verification";
            this.ResumeLayout(false);
        }
        private Guna.UI2.WinForms.Guna2TextBox txtCode;
        private Guna.UI2.WinForms.Guna2Button btnVerify;
        private System.Windows.Forms.Label lblInfo;
    }
}