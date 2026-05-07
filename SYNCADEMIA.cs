using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace syncademia
{
    public partial class SYNCADEMIA : Form
    {
        private Login _login = new Login();
        public static SYNCADEMIA Instance { get; private set; }

        public SYNCADEMIA()
        {
            InitializeComponent();
            LoadLogoFromFile();
            Instance = this;
        }

        private void LoadLogoFromFile()
        {
            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "main_logo.png");
                if (File.Exists(logoPath))
                {
                    pictureBox1.Image = Image.FromFile(logoPath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Logo Error: " + ex.Message);
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblWrong.Text = "";
            lblWrong.ForeColor = Color.Black;

            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                SetErrorMessage("Please enter both email and password.");
                return;
            }

            LoadingOverlay overlay = new LoadingOverlay("Logging in...");
            overlay.ShowWithAnimation(this);

            try
            {
                await _login.check_login_email(email);

                if (!_login.canSignUp)
                {
                    overlay.Close();
                    SetErrorMessage("Email not found.");
                    _login.canSignUp = true;
                    return;
                }

                await _login.check_login_password(password, email);

                if (!_login.loggedIn)
                {
                    overlay.Close();
                    SetErrorMessage("Invalid password.");
                    _login.canSignUp = true;
                    return;
                }

                overlay.Close();
                

                if (_login.is_mentor)
                {
                    var mentorPage = new mentorpage(_login.id);
                    mentorPage.FormClosed += (s, args) => { if (!this.Visible) Application.Exit(); };
                    mentorPage.Show();
                    this.Hide();
                }
                else
                {
                    var studentHome = new StudentHome(_login.id);
                    studentHome.FormClosed += (s, args) => { if (!this.Visible) Application.Exit(); };
                    studentHome.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                overlay.Close();
                lblWrong.Text = ""; // Clear the red text label
                
                // Show our smart popup
                NetworkErrorDialog.Show(ex, this);
                
                _login.canSignUp = true;
            }
        }

        private async void btnForgetPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                SetErrorMessage("Enter your email first, then click Forget Password.");
                return;
            }

            LoadingOverlay overlay = new LoadingOverlay("Checking email...");
            overlay.ShowWithAnimation(this);

            try
            {
                await _login.check_login_email(email);
                if (!_login.canSignUp)
                {
                    overlay.Close();
                    SetErrorMessage("Email not found.");
                    _login.canSignUp = true;
                    return;
                }

                overlay.Close();

                string msg = "Password Reset code";
                await _login.SendVerificationCode(msg, email);

                MessageBox.Show("Verification code sent to your email.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form1_FP codeForm = new Form1_FP(email, _login.generated_code);
                codeForm.ShowDialog();

                lblWrong.Text = "";
                lblWrong.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                overlay.Close();
                SetErrorMessage("Error: " + ex.Message);
            }
        }

        // --- UPDATED METHOD: Using the new custom dialog ---
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // Show our custom selection dialog
            using (RoleSelectionDialog roleDialog = new RoleSelectionDialog())
            {
                DialogResult result = roleDialog.ShowDialog(this);

                // If they closed the window without picking, just return
                if (result == DialogResult.Cancel) return;

                bool isStudent = (result == DialogResult.Yes);

                if (isStudent)
                {
                    SignUp signUpForm = new SignUp();
                    var signUpResult = signUpForm.ShowDialog();

                    if (signUpResult == DialogResult.OK && signUpForm.LoggedInUser != null)
                    {
                        this.Hide();
                        var studentHome = new StudentHome(signUpForm.LoggedInUser.id);
                        studentHome.FormClosed += (s, args) => { if (!this.Visible) Application.Exit(); };
                        studentHome.Show();
                    }
                }
                else
                {
                    MentorBasicInfoForm basicForm = new MentorBasicInfoForm();
                    if (basicForm.ShowDialog() == DialogResult.OK)
                    {
                        MentorSubjectsForm subjectsForm = new MentorSubjectsForm(
                            basicForm.Username, basicForm.Email, basicForm.Password,
                            basicForm.FullName, basicForm.Phone, basicForm.Rank);

                        subjectsForm.ShowDialog(basicForm);

                        if (MentorSignUpResult.Success && MentorSignUpResult.LoggedInUser != null)
                        {
                            this.Hide();
                            var mentorPage = new mentorpage(MentorSignUpResult.LoggedInUser.id);
                            mentorPage.FormClosed += (s, args) => { if (!this.Visible) Application.Exit(); };
                            mentorPage.Show();
                        }
                    }
                }
            }
        }

        private void SetErrorMessage(string message)
        {
            lblWrong.Text = message;
            lblWrong.ForeColor = Color.Red;
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkPassword.Checked;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { }
        private void SYNCADEMIA_Load(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }

   
    public class RoleSelectionDialog : Form
    {
        public RoleSelectionDialog()
        {
            // Form setup
            this.Text = "Select Account Type";
            this.Size = new Size(350, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.ShowIcon = false;

            // Question Label
            Label lblQuestion = new Label();
            lblQuestion.Text = "Are you signing up as a Mentor or Student?";
            lblQuestion.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
            lblQuestion.Dock = DockStyle.Top;
            lblQuestion.Height = 70;
            lblQuestion.ForeColor = Color.Black;

            // --- CHANGED: Use Guna2GradientButton to match the Login button ---
            Guna.UI2.WinForms.Guna2GradientButton btnStudent = new Guna.UI2.WinForms.Guna2GradientButton();
            btnStudent.Text = "Student";
            btnStudent.Size = new Size(110, 40);
            btnStudent.Location = new Point(45, 80);
            btnStudent.FillColor = Color.FromArgb(3, 62, 132);
            btnStudent.FillColor2 = Color.FromArgb(0, 150, 200);
            btnStudent.ForeColor = Color.White;
            btnStudent.Font = new Font("Segoe UI Black", 10f, FontStyle.Bold);
            btnStudent.Cursor = Cursors.Hand;
            btnStudent.DialogResult = DialogResult.Yes; // Maps to "Student"
            btnStudent.Click += (s, e) => { this.DialogResult = DialogResult.Yes; this.Close(); };

            Guna.UI2.WinForms.Guna2GradientButton btnMentor = new Guna.UI2.WinForms.Guna2GradientButton();
            btnMentor.Text = "Mentor";
            btnMentor.Size = new Size(110, 40);
            btnMentor.Location = new Point(175, 80);
            btnMentor.FillColor = Color.FromArgb(3, 62, 132);
            btnMentor.FillColor2 = Color.FromArgb(0, 150, 200);
            btnMentor.ForeColor = Color.White;
            btnMentor.Font = new Font("Segoe UI Black", 10f, FontStyle.Bold);
            btnMentor.Cursor = Cursors.Hand;
            btnMentor.DialogResult = DialogResult.No; // Maps to "Mentor"
            btnMentor.Click += (s, e) => { this.DialogResult = DialogResult.No; this.Close(); };

            // Prevent focus highlighting on load
            this.Load += (s, e) => { this.ActiveControl = lblQuestion; };

            this.Controls.Add(lblQuestion);
            this.Controls.Add(btnStudent);
            this.Controls.Add(btnMentor);
        }
    }
}