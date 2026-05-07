namespace syncademia
{
    partial class Uc_Subject_StudentHome
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            dgvSubject = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSubject).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(dgvSubject);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(20, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(460, 760);
            panel1.TabIndex = 1;
            // 
            // dgvSubject
            // 
            dataGridViewCellStyle4.BackColor = Color.WhiteSmoke;
            dgvSubject.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvSubject.BackgroundColor = Color.White;
            dgvSubject.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.RoyalBlue;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 16.8F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvSubject.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvSubject.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSubject.Columns.AddRange(new DataGridViewColumn[] { Column1 });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvSubject.DefaultCellStyle = dataGridViewCellStyle6;
            dgvSubject.Dock = DockStyle.Fill;
            dgvSubject.EnableHeadersVisualStyles = false;
            dgvSubject.Location = new Point(0, 0);
            dgvSubject.Name = "dgvSubject";
            dgvSubject.RowHeadersVisible = false;
            dgvSubject.RowHeadersWidth = 51;
            dgvSubject.RowTemplate.Height = 40;
            dgvSubject.Size = new Size(460, 760);
            dgvSubject.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Course Title";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            // 
            // Uc_Subject
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "Uc_Subject";
            Padding = new Padding(20);
            Size = new Size(500, 800);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSubject).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pn1header;
        private Label label1;
        private Panel panel1;
        private DataGridView dgvSubject;
        private DataGridViewTextBoxColumn Column1;
    }
}
