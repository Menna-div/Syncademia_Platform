namespace syncademia
{
    partial class AddGrades
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
            this.txtStudentID = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbSubjectName = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbGradeType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtGradeValue = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSubmitGrade = new Guna.UI2.WinForms.Guna2Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTitle.Location = new System.Drawing.Point(50, 20);
            this.labelTitle.Text = "Manage Student Grades";

            // txtStudentID
            this.txtStudentID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtStudentID.Location = new System.Drawing.Point(50, 70);
            this.txtStudentID.PlaceholderText = "Student ID...";
            this.txtStudentID.Size = new System.Drawing.Size(250, 36);
            this.txtStudentID.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.txtStudentID.TextChanged += new System.EventHandler(this.txtStudentID_TextChanged);

            // cmbSubjectName
            this.cmbSubjectName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSubjectName.Location = new System.Drawing.Point(50, 120);
            this.cmbSubjectName.Size = new System.Drawing.Size(250, 36);
            this.cmbSubjectName.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;

            // cmbGradeType
            this.cmbGradeType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbGradeType.Items.AddRange(new object[] { "MID", "Practical", "Oral", "Final", "Bonus" });
            this.cmbGradeType.Location = new System.Drawing.Point(50, 170);
            this.cmbGradeType.Size = new System.Drawing.Size(250, 36);
            this.cmbGradeType.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.cmbGradeType.StartIndex = 0;

            // txtGradeValue
            this.txtGradeValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtGradeValue.Location = new System.Drawing.Point(50, 220);
            this.txtGradeValue.PlaceholderText = "New Value...";
            this.txtGradeValue.Size = new System.Drawing.Size(250, 36);
            this.txtGradeValue.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;

            // btnSubmitGrade
            this.btnSubmitGrade.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmitGrade.Location = new System.Drawing.Point(50, 280);
            this.btnSubmitGrade.Size = new System.Drawing.Size(180, 45);
            this.btnSubmitGrade.Text = "Save Changes";
            this.btnSubmitGrade.Click += new System.EventHandler(this.btnSubmitGrade_Click);

            // Control Settings
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.txtStudentID);
            this.Controls.Add(this.cmbSubjectName);
            this.Controls.Add(this.cmbGradeType);
            this.Controls.Add(this.txtGradeValue);
            this.Controls.Add(this.btnSubmitGrade);
            this.BackColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(600, 400);
            this.ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2TextBox txtStudentID;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubjectName;
        private Guna.UI2.WinForms.Guna2ComboBox cmbGradeType;
        private Guna.UI2.WinForms.Guna2TextBox txtGradeValue;
        private Guna.UI2.WinForms.Guna2Button btnSubmitGrade;
        private System.Windows.Forms.Label labelTitle;
    }
}