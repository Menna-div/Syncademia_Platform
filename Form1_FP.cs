using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Form1_FP : Form
    {
        private string _email;
        private string _correctCode;
        private Login _login;

        public Form1_FP(string email, string correctCode)
        {
            InitializeComponent();
            _email = email;
            _correctCode = correctCode;
            _login = new Login();
            LoadLogo();   // same logo as login form
        }

        private void LoadLogo()
        {
            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "main_logo.png");
                if (File.Exists(logoPath))
                {
                    guna2CirclePictureBox1.Image = Image.FromFile(logoPath);
                    guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Logo Error (Form1_FP): " + ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string userCode = guna2TextBox2.Text.Trim();

            if (userCode == _correctCode)
            {
                // Code correct → open new password form
                Form2_FP f2 = new Form2_FP(_email, _login);
                f2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { }
    }
}