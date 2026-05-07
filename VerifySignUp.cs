using System.Windows.Forms;

namespace syncademia
{
    public partial class VerifySignUp : Form
    {
        private string _correctCode;

        public VerifySignUp(string correctCode)
        {
            InitializeComponent();
            _correctCode = correctCode;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() == _correctCode)
            {
                MessageBox.Show("Email verified successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid code. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}