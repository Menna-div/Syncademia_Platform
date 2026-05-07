using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Uc_ScanQR_StudentHome : UserControl
    {
        private int _studentId;

        public Uc_ScanQR_StudentHome(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;

            // CENTER FIX
            this.Resize += (s, e) => CenterMyControls();
            CenterMyControls();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            string code = txtQRCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please enter the QR code you received.", "No Code",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (success, message) = await AttendanceSystem.UseQRCode(_studentId, code, this.FindForm());
                MessageBox.Show(message, success ? "Success" : "Error",
                                MessageBoxButtons.OK,
                                success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (success)
                    txtQRCode.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CenterMyControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Left = (this.Width - ctrl.Width) / 2;
        }
    }
}