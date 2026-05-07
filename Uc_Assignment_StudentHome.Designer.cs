namespace syncademia
{
    partial class Uc_Assignment_StudentHome
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgvSubject = new DataGridView();
            ColSubject = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();          // Still a text column – we style it in code
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
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 16.8F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvSubject.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSubject.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSubject.Columns.AddRange(new DataGridViewColumn[] { ColSubject, Column1, Column2 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvSubject.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSubject.Dock = DockStyle.Fill;
            dgvSubject.EnableHeadersVisualStyles = false;
            dgvSubject.Location = new Point(0, 0);
            dgvSubject.Name = "dgvSubject";
            dgvSubject.RowHeadersVisible = false;
            dgvSubject.RowHeadersWidth = 51;
            dgvSubject.RowTemplate.Height = 40;
            dgvSubject.Size = new Size(434, 281);
            dgvSubject.TabIndex = 1;
            dgvSubject.CellClick += dgvSubject_CellClick;    // ← Click event (opens link)
            // 
            // ColSubject
            // 
            ColSubject.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColSubject.HeaderText = "Subject ";
            ColSubject.MinimumWidth = 150;
            ColSubject.Name = "ColSubject";
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Task ";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            // 
            // Column2   (Will show the link – header renamed to "Link")
            // 
            Column2.HeaderText = "Link";
            Column2.MinimumWidth = 120;
            Column2.Name = "Column2";
            Column2.Resizable = DataGridViewTriState.True;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Uc_Assignment_StudentHome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvSubject);
            Name = "Uc_Assignment_StudentHome";
            Size = new Size(434, 281);
            ((System.ComponentModel.ISupportInitialize)dgvSubject).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvSubject;
        private DataGridViewTextBoxColumn ColSubject;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;   // Not a link column – we handle clicks and style manually
    }
}