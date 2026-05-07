namespace syncademia
{
    partial class AddAssigment
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSubject = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaskName = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaskLink = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnUpload = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();

            // ── guna2PictureBox1 ───────────────────────────────────────────
            this.guna2PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(273, 17);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(149, 138);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox1.TabIndex = 0;
            this.guna2PictureBox1.TabStop = false;

            // ── label1 (Subject) ───────────────────────────────────────────
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(20, 175);
            this.label1.Name = "label1";
            this.label1.TabIndex = 1;
            this.label1.Text = "Subject :";

            // ── cmbSubject (dropdown) ──────────────────────────────────────
            this.cmbSubject.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSubject.BorderRadius = 8;
            this.cmbSubject.FocusedState.BorderColor = System.Drawing.Color.RoyalBlue;
            this.cmbSubject.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbSubject.ForeColor = System.Drawing.Color.Black;
            this.cmbSubject.ItemHeight = 30;
            this.cmbSubject.Location = new System.Drawing.Point(180, 170);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(273, 36);
            this.cmbSubject.TabIndex = 2;

            // ── label2 (Task Name) ─────────────────────────────────────────
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(20, 230);
            this.label2.Name = "label2";
            this.label2.TabIndex = 3;
            this.label2.Text = "Task Name :";

            // ── txtTaskName ────────────────────────────────────────────────
            this.txtTaskName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTaskName.BorderColor = System.Drawing.Color.Silver;
            this.txtTaskName.BorderRadius = 8;
            this.txtTaskName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTaskName.DefaultText = "";
            this.txtTaskName.FocusedState.BorderColor = System.Drawing.Color.RoyalBlue;
            this.txtTaskName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTaskName.Location = new System.Drawing.Point(180, 228);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.PlaceholderText = "enter task name...";
            this.txtTaskName.SelectedText = "";
            this.txtTaskName.Size = new System.Drawing.Size(273, 36);
            this.txtTaskName.TabIndex = 4;
            this.txtTaskName.TextChanged += new System.EventHandler(this.guna2TextBox2_TextChanged);

            // ── label3 (Task Link) ─────────────────────────────────────────
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.Location = new System.Drawing.Point(20, 285);
            this.label3.Name = "label3";
            this.label3.TabIndex = 5;
            this.label3.Text = "Task Link :";

            // ── txtTaskLink ────────────────────────────────────────────────
            this.txtTaskLink.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTaskLink.BorderColor = System.Drawing.Color.Silver;
            this.txtTaskLink.BorderRadius = 8;
            this.txtTaskLink.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTaskLink.DefaultText = "";
            this.txtTaskLink.FocusedState.BorderColor = System.Drawing.Color.RoyalBlue;
            this.txtTaskLink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTaskLink.Location = new System.Drawing.Point(180, 283);
            this.txtTaskLink.Name = "txtTaskLink";
            this.txtTaskLink.PlaceholderText = "paste Google Drive link...";
            this.txtTaskLink.SelectedText = "";
            this.txtTaskLink.Size = new System.Drawing.Size(273, 36);
            this.txtTaskLink.TabIndex = 6;

            // ── btnUpload ──────────────────────────────────────────────────
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpload.BorderRadius = 10;
            this.btnUpload.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpload.Location = new System.Drawing.Point(240, 345);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(150, 42);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Upload";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);

            // ── AddAssigment ───────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtTaskLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2PictureBox1);
            this.Name = "AddAssigment";
            this.Size = new System.Drawing.Size(709, 440);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubject;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtTaskName;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtTaskLink;
        private Guna.UI2.WinForms.Guna2Button btnUpload;
    }
}