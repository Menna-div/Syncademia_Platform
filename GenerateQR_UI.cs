using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace syncademia
{
    public partial class GenerateQR : UserControl
    {
        private int _mentorId;
        private Login _login = new Login();

        public GenerateQR(int mentorId, Form here)
        {
            InitializeComponent();
            _mentorId = mentorId;

            _ = LoadMentorSubjectsAsync(here);

            // CENTER FIX
            this.Resize += (s, e) => CenterMyControls();
            CenterMyControls();
        }

        private async Task LoadMentorSubjectsAsync(Form here)
        {
            LoadingOverlay loading = new LoadingOverlay("Loading...");
            loading.ShowWithAnimation(here);
            here.Enabled = false;
            try
            {
                List<string> data = await _login.getmentor(_mentorId, here);

                if (data != null && data.Count > 5)
                {
                    string subjectsString = data[5];

                    if (!string.IsNullOrEmpty(subjectsString) && subjectsString != "N/A")
                    {
                        string[] subjects = subjectsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        cmbSubject.Items.Clear();
                        foreach (string subj in subjects)
                        {
                            cmbSubject.Items.Add(subj.Trim());
                        }
                    }
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

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSubject.SelectedItem == null)
                {
                    MessageBox.Show("Please select a subject from the list first.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string subject = cmbSubject.SelectedItem.ToString();
                double bonus = 0;
                double.TryParse(txtBonus.Text, out bonus);

                Form parentForm = this.FindForm();
                var overlay = new LoadingOverlay("Generating QR code...");
                if (parentForm != null) overlay.ShowWithAnimation(parentForm);

                try
                {
                    string generatedCode = await AttendanceSystem.GenerateQRCode(_mentorId, subject, bonus);
                    overlay.Close();

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(generatedCode, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(10);

                    using (Form qrPopup = new Form())
                    {
                        qrPopup.Text = "Scan Attendance QR";
                        qrPopup.Size = new Size(400, 480);
                        qrPopup.StartPosition = FormStartPosition.CenterParent;
                        qrPopup.FormBorderStyle = FormBorderStyle.FixedDialog;
                        qrPopup.MaximizeBox = false;
                        qrPopup.MinimizeBox = false;
                        qrPopup.BackColor = Color.White;

                        PictureBox pbQR = new PictureBox
                        {
                            Image = qrCodeImage,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Dock = DockStyle.Top,
                            Height = 340
                        };

                        Label lblDetails = new Label
                        {
                            Text = $"Subject: {subject}",
                            Font = new Font("Segoe UI", 14, FontStyle.Bold),
                            ForeColor = Color.RoyalBlue,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Fill
                        };

                        qrPopup.Controls.Add(lblDetails);
                        qrPopup.Controls.Add(pbQR);

                        qrPopup.ShowDialog(parentForm);
                    }

                    cmbSubject.SelectedIndex = -1;
                    txtBonus.Clear();
                }
                catch (Exception ex)
                {
                    overlay.Close();
                    MessageBox.Show("Error generating QR: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CenterMyControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Left = (this.Width - ctrl.Width) / 2;
        }
    }
}