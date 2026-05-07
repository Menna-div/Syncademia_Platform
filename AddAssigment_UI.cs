using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class AddAssigment : UserControl
    {
        private int _mentorId;

        public AddAssigment(int mentorId, Form here)
        {
            InitializeComponent();
            _mentorId = mentorId;
            LoadPictureBoxImage();
            _ = LoadMentorSubjects(here);

            // CENTER FIX
            this.Resize += (s, e) => CenterMyControls();
            CenterMyControls();
        }

        // ── Load the mentor's subjects into the dropdown ───────────────────
        private async Task LoadMentorSubjects(Form here)
        {
            LoadingOverlay loading = new LoadingOverlay("Loading...");
            loading.ShowWithAnimation(here);
            here.Enabled = false;
            try
            {
                var mentorData = await dataBase.ReadSheet("Mentors!A:H");
                if (mentorData == null) return;

                foreach (var row in mentorData.Skip(1))
                {
                    if (row.Count < 1) continue;
                    if (!int.TryParse(row[0].ToString(), out int id)) continue;
                    if (id != _mentorId) continue;

                    string subjectsRaw = row.Count > 5 ? row[5].ToString() : "";
                    var subjects = subjectsRaw
                        .Split(',')
                        .Select(s => s.Trim())
                        .Where(s => s != "")
                        .ToList();

                    cmbSubject.Items.Clear();
                    foreach (var subject in subjects)
                        cmbSubject.Items.Add(subject);

                    if (cmbSubject.Items.Count > 0)
                        cmbSubject.SelectedIndex = 0;
                    break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading subjects: " + ex.Message);
            }
            finally
            {
                loading.Close();
                here.Enabled = true;
            }
        }

        // ── Upload button click ────────────────────────────────────────────
        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedItem == null)
            {
                MessageBox.Show("Please select a subject.", "Missing Subject",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("Please enter a task name.", "Missing Task Name",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTaskLink.Text) || !txtTaskLink.Text.StartsWith("http"))
            {
                MessageBox.Show("Please enter a valid Google Drive link.", "Invalid Link",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string chosenSubject = cmbSubject.SelectedItem.ToString();
            string title = txtTaskName.Text.Trim();
            string driveLink = txtTaskLink.Text.Trim();

            var overlay = new LoadingOverlay("Uploading assignment...");
            overlay.ShowWithAnimation(this.FindForm());

            try
            {
                (int grade, int semester) = Subjects.GetGradeAndSemesterForSubject(chosenSubject);

                int newId = await dataBase.GetLatestId("Assignments") + 1;
                int newRow = newId + 1;

                await dataBase.WriteSheet($"Assignments!A{newRow}", new List<IList<object>>
                {
                    new List<object> { newId, _mentorId, chosenSubject, title, driveLink, grade, semester }
                });

                MessageBox.Show($"✅ Assignment '{title}' uploaded successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTaskName.Clear();
                txtTaskLink.Clear();
                cmbSubject.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading assignment: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                overlay.Close();
            }
        }

        private void LoadPictureBoxImage()
        {
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "add_assignment.png");
                if (File.Exists(imagePath))
                {
                    guna2PictureBox1.Image = Image.FromFile(imagePath);
                    guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("AddAssigment image error: " + ex.Message);
            }
        }

        private void CenterMyControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Left = (this.Width - ctrl.Width) / 2;
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { }
    }
}