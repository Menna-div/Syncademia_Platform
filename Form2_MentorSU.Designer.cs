namespace syncademia
{
    partial class MentorSubjectsForm
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
            this.lvSubjects = new System.Windows.Forms.ListView();
            
            // --- UPGRADED BUTTON TYPE ---
            this.btnSignUp = new Guna.UI2.WinForms.Guna2GradientButton();
            
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2PictureBox1 (Logo)
            // 
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(375, 20);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(150, 100);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 0;
            this.guna2PictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.Location = new System.Drawing.Point(300, 130);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Select Subjects You Teach";
            this.lblTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvSubjects
            // 
            this.lvSubjects.CheckBoxes = true;
            this.lvSubjects.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lvSubjects.FullRowSelect = true;
            this.lvSubjects.GridLines = false;
            this.lvSubjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSubjects.Location = new System.Drawing.Point(25, 180);
            this.lvSubjects.MultiSelect = false;
            this.lvSubjects.Name = "lvSubjects";
            this.lvSubjects.Size = new System.Drawing.Size(850, 400);
            this.lvSubjects.TabIndex = 2;
            this.lvSubjects.UseCompatibleStateImageBehavior = false;
            this.lvSubjects.View = System.Windows.Forms.View.Details;
            this.lvSubjects.Columns.Add("Subject", 800);
            // 
            // btnSignUp
            // 
            // --- GRADIENT LOGIC APPLIED HERE ---
            this.btnSignUp.FillColor = System.Drawing.Color.FromArgb(3, 62, 132);
            this.btnSignUp.FillColor2 = System.Drawing.Color.FromArgb(0, 150, 200);
            this.btnSignUp.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnSignUp.ForeColor = System.Drawing.Color.White;
            this.btnSignUp.Location = new System.Drawing.Point(350, 600);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(200, 45);
            this.btnSignUp.TabIndex = 3;
            this.btnSignUp.Text = "Sign Up";
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // MentorSubjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.lvSubjects);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.guna2PictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MentorSubjectsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Syncademia   Mentor Signup (Step 2)";
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private System.Windows.Forms.ListView lvSubjects;
        
        // --- UPGRADED DECLARATION ---
        private Guna.UI2.WinForms.Guna2GradientButton btnSignUp;
    }
}