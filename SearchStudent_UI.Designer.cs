namespace syncademia
{
    partial class SearchStudent
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
            this.txtSearchID = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();

            this.txtSearchID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSearchID.Location = new System.Drawing.Point(50, 50);
            this.txtSearchID.PlaceholderText = "Enter Student ID to Search...";
            this.txtSearchID.Size = new System.Drawing.Size(250, 36);

            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Location = new System.Drawing.Point(50, 110);
            this.btnSearch.Text = "Show Info";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.Controls.Add(this.txtSearchID);
            this.Controls.Add(this.btnSearch);
            this.Size = new System.Drawing.Size(600, 400);
            this.ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2TextBox txtSearchID;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        #endregion
    }
}