namespace syncademia
{
    partial class SignUp
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
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox4 = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkShowPassword = new Guna.UI2.WinForms.Guna2CheckBox();
            this.guna2TextBox5 = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelStudent = new Guna.UI2.WinForms.Guna2Panel();
            this.txtYear = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbMajor = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panelMentor = new Guna.UI2.WinForms.Guna2Panel();
            this.txtRank = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSubjects = new Guna.UI2.WinForms.Guna2TextBox();
            
            // --- UPGRADED BUTTON TYPE ---
            this.btnSignUp = new Guna.UI2.WinForms.Guna2GradientButton();
            
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.panelStudent.SuspendLayout();
            this.panelMentor.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Location = new System.Drawing.Point(100, 20);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(150, 150);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.Location = new System.Drawing.Point(50, 190);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "Username";
            this.guna2TextBox1.Size = new System.Drawing.Size(250, 30);
            this.guna2TextBox1.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            // 
            // guna2TextBox2
            // 
            this.guna2TextBox2.Location = new System.Drawing.Point(50, 230);
            this.guna2TextBox2.Name = "guna2TextBox2";
            this.guna2TextBox2.PlaceholderText = "Full Name";
            this.guna2TextBox2.Size = new System.Drawing.Size(250, 30);
            this.guna2TextBox2.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            // 
            // guna2TextBox3
            // 
            this.guna2TextBox3.Location = new System.Drawing.Point(50, 270);
            this.guna2TextBox3.Name = "guna2TextBox3";
            this.guna2TextBox3.PlaceholderText = "Email Address";
            this.guna2TextBox3.Size = new System.Drawing.Size(250, 30);
            this.guna2TextBox3.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            // 
            // guna2TextBox4
            // 
            this.guna2TextBox4.Location = new System.Drawing.Point(50, 310);
            this.guna2TextBox4.Name = "guna2TextBox4";
            this.guna2TextBox4.PlaceholderText = "Password";
            this.guna2TextBox4.Size = new System.Drawing.Size(250, 30);
            this.guna2TextBox4.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.guna2TextBox4.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.Location = new System.Drawing.Point(195, 345);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(110, 19);
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // guna2TextBox5
            // 
            this.guna2TextBox5.Location = new System.Drawing.Point(50, 370);
            this.guna2TextBox5.Name = "guna2TextBox5";
            this.guna2TextBox5.PlaceholderText = "Phone Number";
            this.guna2TextBox5.Size = new System.Drawing.Size(250, 30);
            this.guna2TextBox5.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            // 
            // panelStudent
            // 
            this.panelStudent.Controls.Add(this.txtYear);
            this.panelStudent.Controls.Add(this.cmbMajor);
            this.panelStudent.Location = new System.Drawing.Point(50, 420);
            this.panelStudent.Name = "panelStudent";
            this.panelStudent.Size = new System.Drawing.Size(250, 80);
            this.panelStudent.Visible = true;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(0, 0);
            this.txtYear.Name = "txtYear";
            this.txtYear.PlaceholderText = "Academic Year (1-4)";
            this.txtYear.Size = new System.Drawing.Size(250, 30);
            this.txtYear.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            // 
            // cmbMajor
            // 
            this.cmbMajor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMajor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMajor.ForeColor = System.Drawing.Color.Black;
            this.cmbMajor.ItemHeight = 30;
            this.cmbMajor.Items.AddRange(new object[] { "CS", "IS", "IT", "General" });
            this.cmbMajor.Location = new System.Drawing.Point(0, 40);
            this.cmbMajor.Name = "cmbMajor";
            this.cmbMajor.Size = new System.Drawing.Size(250, 36);
            this.cmbMajor.TabIndex = 1;
            this.cmbMajor.SelectedIndex = 3;
            // 
            // panelMentor
            // 
            this.panelMentor.Controls.Add(this.txtRank);
            this.panelMentor.Controls.Add(this.txtSubjects);
            this.panelMentor.Location = new System.Drawing.Point(50, 420);
            this.panelMentor.Name = "panelMentor";
            this.panelMentor.Size = new System.Drawing.Size(250, 80);
            this.panelMentor.Visible = false;
            // 
            // txtRank
            // 
            this.txtRank.Location = new System.Drawing.Point(0, 0);
            this.txtRank.Name = "txtRank";
            this.txtRank.PlaceholderText = "Rank (e.g. Doctor, Demonstrator)";
            this.txtRank.Size = new System.Drawing.Size(250, 30);
            this.txtRank.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            // 
            // txtSubjects
            // 
            this.txtSubjects.Location = new System.Drawing.Point(0, 40);
            this.txtSubjects.Name = "txtSubjects";
            this.txtSubjects.PlaceholderText = "Subjects (comma separated)";
            this.txtSubjects.Size = new System.Drawing.Size(250, 30);
            this.txtSubjects.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            // 
            // btnSignUp
            // 
            // --- GRADIENT LOGIC APPLIED HERE ---
            this.btnSignUp.FillColor = System.Drawing.Color.FromArgb(3, 62, 132);
            this.btnSignUp.FillColor2 = System.Drawing.Color.FromArgb(0, 150, 200);
            this.btnSignUp.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnSignUp.ForeColor = System.Drawing.Color.White;
            this.btnSignUp.Location = new System.Drawing.Point(50, 520);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(250, 45);
            this.btnSignUp.Text = "Sign Up";
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // SignUp
            // 
            this.ClientSize = new System.Drawing.Size(350, 600);
            this.Controls.Add(this.panelMentor);
            this.Controls.Add(this.panelStudent);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.guna2TextBox5);
            this.Controls.Add(this.guna2TextBox4);
            this.Controls.Add(this.guna2TextBox3);
            this.Controls.Add(this.guna2TextBox2);
            this.Controls.Add(this.guna2TextBox1);
            this.Controls.Add(this.guna2PictureBox1);
            this.Name = "SignUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Sign Up";
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.panelStudent.ResumeLayout(false);
            this.panelMentor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2ComboBox cmbMajor;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1, guna2TextBox2, guna2TextBox3, guna2TextBox4, guna2TextBox5;
        
        // --- UPGRADED DECLARATION ---
        private Guna.UI2.WinForms.Guna2GradientButton btnSignUp;
        
        private Guna.UI2.WinForms.Guna2Panel panelStudent, panelMentor;
        private Guna.UI2.WinForms.Guna2TextBox txtYear, txtRank, txtSubjects;
        private Guna.UI2.WinForms.Guna2CheckBox chkShowPassword;
    }
}