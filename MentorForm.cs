using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace syncademia
{
    public partial class MentorForm : Form
    {
        public MentorForm()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            SetupLayoutAndLogo();
        }

        private void SetupLayoutAndLogo()
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Anchor = AnchorStyles.None;
            }

            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "main_logo.png");
                
                if (File.Exists(logoPath))
                {
                    using (FileStream fs = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                    {
                        guna2PictureBox1.Image = Image.FromStream(fs);
                    }
                    guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    guna2PictureBox1.Location = new Point(195, 10); 
                }
            }
            catch (Exception ex) { Console.WriteLine("Logo Error: " + ex.Message); }
        }

        private void BackButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        public void PopulateData(List<string> mentorData)
        {
            // ترتيب البيانات المتوقع من Schoollogic.Getmentorinfo هو:
            // 0: Name, 1: Username, 2: Email, 3: Phone, 4: Rank, 5: Subjects[cite: 1]
            if (mentorData != null && mentorData.Count >= 6)
            {
                guna2TextBox1.Text = mentorData[0]; // Name
                guna2TextBox2.Text = mentorData[1]; // Username
                guna2TextBox3.Text = mentorData[2]; // Email
                guna2TextBox4.Text = mentorData[3]; // Phone
                guna2TextBox5.Text = mentorData[4]; // Rank
                guna2TextBox6.Text = mentorData[5]; // Subjects
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox5_TextChanged(object sender, EventArgs e) { }
        private void guna2PictureBox1_Click(object sender, EventArgs e) { }
    }
}