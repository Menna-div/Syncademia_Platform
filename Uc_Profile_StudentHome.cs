using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Uc_Profile : UserControl
    {
        private int _studentId;
        private Login loginSys = new Login();

        public Uc_Profile(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;

            _ = LoadStudentProfile();

            // CENTER FIX
            this.Resize += (s, e) => CenterMyControls();
            CenterMyControls();
        }

        private async Task LoadStudentProfile()
        {
            try
            {
                List<string> data = await loginSys.GetStudent(_studentId);

                if (data != null && data.Count > 1)
                {
                    textBox1.Text = data[4]; // Name
                    textBox2.Text = data[1]; // Username
                    textBox3.Text = data[2]; // Email

                    string picPath = data.Count > 7 ? data[7] : "";

                    if (!string.IsNullOrEmpty(picPath) && File.Exists(picPath))
                    {
                        pictureBox1.Image = Image.FromFile(picPath);
                    }
                    else
                    {
                        LoadDefaultImage();
                    }

                    if (data.Count > 8)
                    {
                        string totalPercentage = data[8];
                        textBox4.Text = totalPercentage + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadDefaultImage()
        {
            string path = Path.Combine(Application.StartupPath, "Resources", "default_user.png");
            if (File.Exists(path))
            {
                pictureBox1.Image = Image.FromFile(path);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void Uc_Profile_Load(object sender, EventArgs e)
        {
        }

        private void CenterMyControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Left = (this.Width - ctrl.Width) / 2;
        }
    }
}