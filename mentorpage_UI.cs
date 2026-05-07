using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class mentorpage : Form
    {
        private int _mentorId;
        private Login loginSys = new Login();
        

        public mentorpage(int mentorId)
        {
            InitializeComponent();
            _mentorId = mentorId;

            // Maximized startup
            // Full‑screen (borderless + maximized)
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(900, 500);   // optional – prevents too small
            this.StartPosition = FormStartPosition.CenterScreen;

            LoadSidebarIcons();
            pictureBox1.Click += pictureBox1_Click;
            _ = LoadMentorProfile();
        }
        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // 1. Fetch mentor info from Google Sheets using your existing method
                List<string> data = await loginSys.getmentor(_mentorId,this);

                if (data != null)
                {
                    // 2. Create an instance of the Mentor Account form
                    MentorForm infoForm = new MentorForm();

                    // 3. Populate the textboxes with the fetched data
                    infoForm.PopulateData(data);

                    // 4. Show the form as a pop-up dialog
                    infoForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not retrieve mentor information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        // 2. Add this method to load the images from your Resources folder
        private void LoadSidebarIcons()
        {
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            // Match these image names to the ones in your Resources folder
            string[] fileNames = {
                "icons8-book-50.png",             // pictureBox2 (Add Assignment)
                "icons8-report-card-50.png",      // pictureBox3 (Add Grades)
                "qr-code.png",                    // pictureBox4 (Generate QR)
                "icons8-user-50.png",             // pictureBox5 (Search Students)
                "icons8-logout-rounded-50.png"    // pictureBox6 (Logout)
            };

            PictureBox[] boxes = {
                pictureBox2,
                pictureBox3,
                pictureBox4,
                pictureBox5,
                pictureBox6
            };  

            for (int i = 0; i < fileNames.Length; i++)
            {
                string fullPath = Path.Combine(basePath, fileNames[i]);
                try
                {
                    if (File.Exists(fullPath))
                    {
                        boxes[i].Image = Image.FromFile(fullPath);
                        boxes[i].SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to load {fileNames[i]}: {ex.Message}");
                }
            }
        }

        private void mentorpage_Load(object sender, EventArgs e)
        {
            // Any initialization logic can go here
        }

        private async Task LoadMentorProfile()
        {
            try
            {
                List<string> data = await loginSys.getmentor(_mentorId,this);
                if (data != null && data.Count > 1)
                {
                    button2.Text = $"Welcome, {data[1]}";
                    LoadDefaultMentorImage(); 
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading mentor profile: " + ex.Message);
                LoadDefaultMentorImage();
            }
        }

        private void LoadDefaultMentorImage()
        {
            string defaultPath = Path.Combine(Application.StartupPath, "Resources", "main_logo.png");
            if (File.Exists(defaultPath))
            {
                pictureBox1.Image = Image.FromFile(defaultPath);
            }
        }

        private void button3_click(object sender, EventArgs e)
        {
            // Go back to the login screen
            
                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to exit?",
                    "Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                
                    Application.Exit();
                }
        }
        

        // ---------- Button Click Events ----------

        // ---------- Helper Method to Load Screens ----------
        
        // This method clears the main panel and loads the chosen screen into it
        private void addUserControl(UserControl userControl)
        {
            LoadingOverlay loading = new LoadingOverlay("Please Wait...");
            loading.ShowWithAnimation(this);
            try
            {
                userControl.Dock = DockStyle.Fill;
                panelMain.Controls.Clear();
                panelMain.Controls.Add(userControl);
                userControl.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            finally
            {
                loading.Close();
            }
        }

        // ---------- Button Click Events ----------

        private void button5_Click(object sender, EventArgs e)
        {
            // Load Add Assignment Screen
            var uc = new AddAssigment(_mentorId, this); 
            addUserControl(uc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Load Add Grades Screen[cite: 8]
            var uc = new AddGrades(_mentorId);
            addUserControl(uc);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Load Generate QR Code Screen[cite: 8]
            var uc = new GenerateQR(_mentorId, this);
            addUserControl(uc);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Load Search Students Screen (This one doesn't need a mentor ID)[cite: 8]
            var uc = new SearchStudent();
            addUserControl(uc);
        }
    }
}