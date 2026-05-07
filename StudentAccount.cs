using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnBack.Click += BackButton_Click; 
            
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
                        pictureBox1.Image = Image.FromStream(fs);
                    }
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Location = new Point(296, 10); 
                }
            }
            catch (Exception ex) { Console.WriteLine("Logo Error: " + ex.Message); }
        }

        private void BackButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        public void PopulateData(List<string> studentData)
        {
            // No loading overlay – data is already loaded
            if (studentData == null)
            {
                MessageBox.Show("studentData is null");
                return;
            }
            if (studentData.Count < 8)
            {
                MessageBox.Show($"studentData has only {studentData.Count} items");
                return;
            }

            textBox2.Text = studentData[0]; // ID
            textBox1.Text = studentData[1]; // Name
            textBox3.Text = studentData[2]; // Username
            textBox4.Text = studentData[3]; // Email
            textBox6.Text = studentData[4]; // Major
            textBox5.Text = studentData[5]; // Year
            textBox7.Text = studentData[6]; // Phone
            textBox8.Text = studentData[7] + "%"; // Percentage
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = 
            textBox4.Enabled = textBox5.Enabled = textBox6.Enabled =
            textBox7.Enabled = textBox8.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}