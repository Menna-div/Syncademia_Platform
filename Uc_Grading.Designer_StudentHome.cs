namespace syncademia
{
    partial class Uc_Grading_StudentHome
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            dgvSubject = new DataGridView();
            ColSubject = new DataGridViewTextBoxColumn();
            colmidterm = new DataGridViewTextBoxColumn();
            coloral = new DataGridViewTextBoxColumn();
            colpractical = new DataGridViewTextBoxColumn();
            colfinal = new DataGridViewTextBoxColumn();
            colbonus = new DataGridViewTextBoxColumn();    // ← new
            coltotal = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvSubject).BeginInit();
            SuspendLayout();
            // 
            // dgvSubject
            // 
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            dgvSubject.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSubject.BackgroundColor = Color.White;
            dgvSubject.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.RoyalBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvSubject.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSubject.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSubject.Columns.AddRange(new DataGridViewColumn[] { ColSubject, colmidterm, coloral, colpractical, colfinal, colbonus, coltotal });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvSubject.DefaultCellStyle = dataGridViewCellStyle8;
            dgvSubject.Dock = DockStyle.Fill;
            dgvSubject.EnableHeadersVisualStyles = false;
            dgvSubject.Location = new Point(0, 0);
            dgvSubject.Name = "dgvSubject";
            dgvSubject.RowHeadersVisible = false;
            dgvSubject.RowHeadersWidth = 51;
            dgvSubject.RowTemplate.Height = 40;
            dgvSubject.Size = new Size(427, 252);
            dgvSubject.TabIndex = 2;
            // 
            // ColSubject
            // 
            ColSubject.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColSubject.DefaultCellStyle = dataGridViewCellStyle3;
            ColSubject.HeaderText = "Subject ";
            ColSubject.MinimumWidth = 150;
            ColSubject.Name = "ColSubject";
            // 
            // colmidterm
            // 
            colmidterm.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colmidterm.DefaultCellStyle = dataGridViewCellStyle4;
            colmidterm.HeaderText = "Midterm";
            colmidterm.MinimumWidth = 6;
            colmidterm.Name = "colmidterm";
            colmidterm.Width = 78;
            // 
            // coloral
            // 
            coloral.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            coloral.DefaultCellStyle = dataGridViewCellStyle5;
            coloral.HeaderText = "Oral";
            coloral.MinimumWidth = 6;
            coloral.Name = "coloral";
            coloral.Width = 75;
            // 
            // colpractical
            // 
            colpractical.HeaderText = "Practical";
            colpractical.MinimumWidth = 6;
            colpractical.Name = "colpractical";
            colpractical.Width = 78;
            // 
            // colfinal
            // 
            colfinal.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colfinal.DefaultCellStyle = dataGridViewCellStyle6;
            colfinal.HeaderText = "Final";
            colfinal.MinimumWidth = 6;
            colfinal.Name = "colfinal";
            colfinal.Width = 75;
            // 
            // colbonus
            // 
            colbonus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colbonus.HeaderText = "Bonus";
            colbonus.MinimumWidth = 6;
            colbonus.Name = "colbonus";
            colbonus.Width = 75;
            // 
            // coltotal
            // 
            coltotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle7.ForeColor = Color.RoyalBlue;
            coltotal.DefaultCellStyle = dataGridViewCellStyle7;
            coltotal.HeaderText = "Total";
            coltotal.MinimumWidth = 6;
            coltotal.Name = "coltotal";
            coltotal.Width = 75;
            // 
            // Uc_Grading_StudentHome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvSubject);
            Name = "Uc_Grading_StudentHome";
            Size = new Size(427, 252);
            ((System.ComponentModel.ISupportInitialize)dgvSubject).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvSubject;
        private DataGridViewTextBoxColumn ColSubject;
        private DataGridViewTextBoxColumn colmidterm;
        private DataGridViewTextBoxColumn coloral;
        private DataGridViewTextBoxColumn colpractical;
        private DataGridViewTextBoxColumn colfinal;
        private DataGridViewTextBoxColumn colbonus;   // ← new field
        private DataGridViewTextBoxColumn coltotal;
    }
}