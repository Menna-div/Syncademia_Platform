namespace syncademia
{
    partial class GenerateQR
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.cmbSubject = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtBonus = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnGenerate = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();

            // cmbSubject
            this.cmbSubject.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSubject.BackColor = System.Drawing.Color.Transparent;
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubject.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSubject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbSubject.ItemHeight = 30;
            this.cmbSubject.Location = new System.Drawing.Point(50, 50);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(250, 36);
            this.cmbSubject.TabIndex = 0;

            // txtBonus
            this.txtBonus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBonus.Location = new System.Drawing.Point(50, 110);
            this.txtBonus.Name = "txtBonus";
            this.txtBonus.PlaceholderText = "Bonus Grade (e.g. 2)...";
            this.txtBonus.Size = new System.Drawing.Size(250, 36);
            this.txtBonus.TabIndex = 1;

            // btnGenerate
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGenerate.Location = new System.Drawing.Point(50, 180);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(180, 45);
            this.btnGenerate.Text = "Generate QR Code";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            this.btnGenerate.TabIndex = 2;

            // GenerateQR
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.txtBonus);
            this.Controls.Add(this.btnGenerate);
            this.Size = new System.Drawing.Size(600, 400);
            this.ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2ComboBox cmbSubject;
        private Guna.UI2.WinForms.Guna2TextBox txtBonus;
        private Guna.UI2.WinForms.Guna2Button btnGenerate;

        #endregion
    }
}