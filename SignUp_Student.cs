using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Google.Apis.Sheets.v4.Data;

namespace syncademia
{
    public partial class SignUp : Form
    {
        private Login loginSys = new Login();
        public Login LoggedInUser { get; private set; }

        public SignUp()
        {
            InitializeComponent();
            LoadLogo();
            
            // Hide the mentor panel (if present in designer)
            if (panelMentor != null) panelMentor.Visible = false;

            // --- NEW: Listen to the Year textbox to change the major dynamically ---
            txtYear.TextChanged += txtYear_TextChanged;
        }

        // --- NEW: Automatically set and lock the Major to "General" for Year 1 & 2 ---
        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtYear.Text.Trim(), out int year))
            {
                if (year == 1 || year == 2)
                {
                    cmbMajor.SelectedItem = "General"; // Force selection to General
                    cmbMajor.Enabled = false;          // Lock the dropdown so they can't change it
                }
                else
                {
                    cmbMajor.Enabled = true;           // Unlock it for year 3 and 4
                }
            }
        }

        private void LoadLogo()
        {
            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "main_logo.png");
                if (File.Exists(logoPath))
                    guna2PictureBox1.Image = System.Drawing.Image.FromFile(logoPath);
            }
            catch { }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            guna2TextBox4.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private async void btnSignUp_Click(object sender, EventArgs e)
        {
            LoadingOverlay overlay = new LoadingOverlay("Please Wait...");
            string username = guna2TextBox1.Text.Trim();
            string name = guna2TextBox2.Text.Trim();
            string email = guna2TextBox3.Text.Trim();
            string password = guna2TextBox4.Text.Trim();
            string phone = guna2TextBox5.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            // Name must be exactly 4 words
            if (name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length != 4)
            {
                MessageBox.Show("Full name must be exactly four words (e.g., Ahmed Mohamed Ali Hassan).");
                return;
            }
            
            // Reset flags
            loginSys.canSignUp = true;
            
            overlay.ShowWithAnimation(this);

            try
            {
                // Validate username (student)
                await loginSys.check_username(true, username);
                if (!loginSys.canSignUp)
                {
                    overlay.Close();
                    MessageBox.Show("Username already taken or invalid.");
                    loginSys.canSignUp = true;
                    return;
                }

                // Validate email
                await loginSys.check_email(email);
                if (!loginSys.canSignUp)
                {
                    overlay.Close();
                    MessageBox.Show("Email already registered or invalid.");
                    loginSys.canSignUp = true;
                    return;
                }

                // Validate password
                loginSys.check_password(password);
                if (!loginSys.canSignUp)
                {
                    overlay.Close();
                    MessageBox.Show("Password must be at least 6 characters.");
                    loginSys.canSignUp = true;
                    return;
                }

                // Send verification code with loading overlay
                await loginSys.SendVerificationCode("Syncademia Student Registration", email);
                overlay.Close();
                MessageBox.Show("Verification code sent to your email.");
            }
            catch (Exception ex)
            {
                overlay.Close();
                NetworkErrorDialog.Show(ex, this);
            }

            // Verify code
            VerifySignUp verifyForm = new VerifySignUp(loginSys.generated_code);
            if (verifyForm.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Email verification failed.");
                return;
            }

            // Collect student specific data
            int.TryParse(txtYear.Text.Trim(), out int year);
            string major = cmbMajor.SelectedItem?.ToString();

            // --- NEW: Backend Safety Check. Absolutely enforce "General" for Year 1 and 2 ---
            if (year == 1 || year == 2)
            {
                major = "General";
            }

            // Perform sign up with loading overlay
            overlay = new LoadingOverlay("Creating your account...");
            overlay.ShowWithAnimation(this);

            try
            {
                var (success, message) = await loginSys.signup(username, email, password, name, phone, 0.0, year, major);
                overlay.Close();

                if (success)
                {
                    LoggedInUser = loginSys;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sign-up failed: " + message);
                }
            }
           catch (Exception ex)
            {
                overlay.Close();
                NetworkErrorDialog.Show(ex, this);
            }
        }
    }
}