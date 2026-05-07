using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class MentorBasicInfoForm : Form
    {
        private Login _login = new Login();

        public MentorBasicInfoForm()
        {
            InitializeComponent();
            LoadLogo();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
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

        public string FullName => guna2TextBox2.Text.Trim();
        public string Username => guna2TextBox4.Text.Trim();
        public string Email => guna2TextBox6.Text.Trim();
        public string Phone => guna2TextBox8.Text.Trim();
        public string Password => guna2TextBox10.Text.Trim();
        public string Rank => guna2ComboBox1.SelectedItem?.ToString();

        private async void btnNext_Click(object sender, EventArgs e)
        {
            _login.canSignUp = true;
            var overlay = new LoadingOverlay("Please Wait...");

            overlay.ShowWithAnimation(this);
            try
            {

            // 1. Basic field validation
            if (string.IsNullOrEmpty(FullName)) {overlay.Close(); MessageBox.Show("Please enter your full name."); return; }
            if (string.IsNullOrEmpty(Username)) {overlay.Close(); MessageBox.Show("Please enter a username."); return; }
            if (string.IsNullOrEmpty(Email)) {overlay.Close(); MessageBox.Show("Please enter your email address."); return; }
            if (string.IsNullOrEmpty(Phone)) {overlay.Close(); MessageBox.Show("Please enter your phone number."); return; }
            if (string.IsNullOrEmpty(Password)) {overlay.Close(); MessageBox.Show("Please enter a password."); return; }
            if (Rank == null) {overlay.Close(); MessageBox.Show("Please select your academic rank."); return; }

            // 2. Name must be exactly 4 words
            if (FullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length != 4)
            {
                overlay.Close();
                MessageBox.Show("Full name must consist of exactly four words.\nExample: Ahmed Mohamed Ali Hassan");
                return;
            }

            // 3. Check username uniqueness (mentor)
            await _login.check_username(false, Username);
            if (!_login.canSignUp)
            {
                overlay.Close();
                MessageBox.Show("Username is already taken or invalid. Please choose another.");
                _login.canSignUp = true;
                return;
            }

            // 4. Check email uniqueness
            await _login.check_email(Email);
            if (!_login.canSignUp)
            {
                overlay.Close();
                MessageBox.Show("Email is already registered or invalid. Please use a different email.");
                _login.canSignUp = true;
                return;
            }

            // 5. Check password strength
            _login.check_password(Password);
            if (!_login.canSignUp)
            {
                overlay.Close();
                MessageBox.Show("Password must be at least 6 characters long.");
                _login.canSignUp = true;
                return;
            }

            // 6. Send verification code WITH LOADING OVERLAY

            
                await _login.SendVerificationCode("Syncademia Mentor Registration", Email);
                overlay.Close();
                MessageBox.Show("A verification code has been sent to your email address.\nPlease check your inbox.", "Code Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                overlay.Close();
                MessageBox.Show($"Failed to send verification code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 7. Open verification form
            VerifySignUp verifyForm = new VerifySignUp(_login.generated_code);
            if (verifyForm.ShowDialog() == DialogResult.OK)
            {
                // Email verified – proceed to subject selection form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Email verification failed. Please try again.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle the password character hiding/showing
            guna2TextBox10.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }
    }
}