using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace syncademia
{
    public partial class SearchStudent : UserControl
    {
        public SearchStudent()
        {
            InitializeComponent();

            // CENTER FIX
            this.Resize += (s, e) => CenterMyControls();
            CenterMyControls();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchID.Text, out int studentID))
            {
                Form parentForm = this.FindForm();
                var overlay = new LoadingOverlay("Searching...");
                overlay.ShowWithAnimation(parentForm);
                try
                {
                    Login login = new Login();
                    List<string> data = await login.GetStudent(studentID);
                    overlay.Close();
                    if (data != null)
                    {
                        Form1 infoForm = new Form1();
                        infoForm.PopulateData(data);
                        infoForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Student ID not found!");
                    }
                }
                catch (Exception ex)
                {
                    overlay.Close();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void CenterMyControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Left = (this.Width - ctrl.Width) / 2;
        }
    }
}