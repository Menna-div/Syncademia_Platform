using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class StudentHome : Form
    {
        private int _studentId;

        public StudentHome(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;

            // Full‑screen (borderless + maximized)
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(900, 500);   // optional – prevents too small

            label2.Text = "Welcome!";
            LoadSidebarIcons();
            _ = LoadWelcomeMessageAsync();
            btSignout.Text = "exit";
            label2.Click += personalAccount_Click;
        }

        // --- Step 1: Add this entire method to load icons ---
        private void LoadSidebarIcons()
        {
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            // Define filenames for each icon in the Resources folder[cite: 7]
            string subjectsIcon = "icons8-book-50.png";
            string assignmentsIcon = "icons8-report-card-50.png";
            string gradesIcon = "icons8-chart-50.png";
            string scanQRIcon = "qr-code.png";
            string signoutIcon = "icons8-logout-rounded-50.png";
            
            // --- NEW: Add the main logo for the profile picture ---
            string profileIcon = "main_logo.png"; 

            // Map each icon filename to its corresponding PictureBox on the form[cite: 7]
            Dictionary<string, PictureBox> iconMap = new Dictionary<string, PictureBox>
            {
                { subjectsIcon, pictureBox1 },
                { assignmentsIcon, pictureBox2 },
                { gradesIcon, pictureBox3 },
                { scanQRIcon, pictureBox5_QR },
                { signoutIcon, pictureBox4 },
                
                // --- NEW: Map the logo to the personalAccount PictureBox ---
                { profileIcon, personalAccount } 
            };

            foreach (var icon in iconMap)
            {
                string fullPath = Path.Combine(basePath, icon.Key);
                try
                {
                    if (File.Exists(fullPath))
                    {
                        icon.Value.Image = Image.FromFile(fullPath);
                        icon.Value.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Warning: Icon file not found at {fullPath}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to load {icon.Key}: {ex.Message}");
                }
            }
        }
        // --- End of LoadSidebarIcons method ---

        // =========================================================================
        // DO NOT CHANGE the button logic below! This must be preserved verbatim.
        // =========================================================================

        private void btScanQR_Click(object sender, EventArgs e)
        {
            HighlightButton(btScanQR);
            var uc = new Uc_ScanQR_StudentHome(_studentId);
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(uc);
        }

        private async Task LoadWelcomeMessageAsync()
        {
            try
            {
                var school = new Schoollogic();
                List<string> data = await school.Getstudentsinfo(_studentId);
                if (data != null && data.Count > 2)
                {
                    string username = data[2];
                    if (label2.InvokeRequired)
                        label2.Invoke(new Action(() => label2.Text = $"Welcome, {username} "));
                    else
                        label2.Text = $"Welcome, {username} ";
                }
            }
            catch { }
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private async void personalAccount_Click(object sender, EventArgs e)
        {
            try
            {
                Login login = new Login();
                List<string> data = await login.GetStudent(_studentId);
                if (data != null)
                {
                    Form1 infoForm = new Form1();
                    infoForm.PopulateData(data);
                    infoForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not retrieve student information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message);
            }
        }

        private async void btSubject_Click(object sender, EventArgs e)
        {
            HighlightButton(btSubject);
            var overlay = new LoadingOverlay("Loading subjects...");
            overlay.ShowWithAnimation(this);
            try
            {
                var uc = new Uc_Subject_StudentHome();
                uc.Dock = DockStyle.Fill;
                panel3.Controls.Clear();
                panel3.Controls.Add(uc);
                await uc.LoadSubjectsForStudent(_studentId);
                overlay.Close();
            }
            catch (Exception ex)
            {
                overlay.Close();
                MessageBox.Show("Error loading subjects: " + ex.Message);
            }
        }

        private async void btAssignment_Click(object sender, EventArgs e)
        {
            HighlightButton(btAssignment);
            var overlay = new LoadingOverlay("Loading assignments...");
            overlay.ShowWithAnimation(this);
            try
            {
                var uc = new Uc_Assignment_StudentHome();
                uc.Dock = DockStyle.Fill;
                panel3.Controls.Clear();
                panel3.Controls.Add(uc);
                await uc.LoadAssignmentsForStudent(_studentId);
                overlay.Close();
            }
            catch (Exception ex)
            {
                overlay.Close();
                MessageBox.Show("Error loading assignments: " + ex.Message);
            }
        }

        private async void btGrade_Click(object sender, EventArgs e)
        {
            HighlightButton(btGrade);
            var overlay = new LoadingOverlay("Loading grades...");
            overlay.ShowWithAnimation(this);
            try
            {
                var uc = new Uc_Grading_StudentHome();
                uc.Dock = DockStyle.Fill;
                panel3.Controls.Clear();
                panel3.Controls.Add(uc);
                await uc.LoadGradesForStudent(_studentId);
                overlay.Close();
            }
            catch (Exception ex)
            {
                overlay.Close();
                MessageBox.Show("Error loading grades: " + ex.Message);
            }
        }

        private void btSignout_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
              
                Application.Exit();
            }
        }

        private void HighlightButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            // نرجع كل الزراير للون الأصلي
            btSubject.FillColor = Color.RoyalBlue;
            btAssignment.FillColor = Color.RoyalBlue;
            btGrade.FillColor = Color.RoyalBlue;
            btScanQR.FillColor = Color.RoyalBlue;
            btSignout.FillColor = Color.RoyalBlue;

            // نغمق لون الزرار اللي اليوزر داس عليه
            activeButton.FillColor = Color.FromArgb(40, 60, 120);
        }
    }
}