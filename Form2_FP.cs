using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Form2_FP : Form
    {
        private string _email;
        private Login _login;

        public Form2_FP(string email, Login login)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            _email = email;
            _login = login;
            LoadLogo();

            // Start with password hidden (Guna2TextBox way)
            guna2TextBox2.PasswordChar = '●';   // or '*' – any character works
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
                System.Diagnostics.Debug.WriteLine("Logo Error (Form2_FP): " + ex.Message);
            }
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            string newPassword = guna2TextBox2.Text.Trim();

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await _login.ResetPassword(_email, newPassword);
                MessageBox.Show("Password changed successfully! You can now log in.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting password: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Toggle password visibility using Guna2TextBox PasswordChar
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
                guna2TextBox2.PasswordChar = '\0';   // show real text
            else
                guna2TextBox2.PasswordChar = '●';    // hide again
        }
    }
}