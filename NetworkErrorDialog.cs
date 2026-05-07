using System;
using System.Drawing;
using System.Windows.Forms;

namespace syncademia
{
    public class NetworkErrorDialog : Form
    {
        public NetworkErrorDialog()
        {
            // Form setup
            this.Text = "Connection Error";
            this.Size = new Size(380, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.ShowIcon = false;

            // Optional: A Wi-Fi/Web emoji icon at the top
            Label lblIcon = new Label();
            lblIcon.Text = "🌐"; 
            lblIcon.Font = new Font("Segoe UI", 28f);
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            lblIcon.Dock = DockStyle.Top;
            lblIcon.Height = 50;

            // Message Label
            Label lblMessage = new Label();
            lblMessage.Text = "Internet connection lost.\nPlease check your network and try again.";
            lblMessage.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblMessage.ForeColor = Color.FromArgb(220, 53, 69); // Warning Red color
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.Dock = DockStyle.Top;
            lblMessage.Height = 50;

            // OK Button (Matching your theme)
            Guna.UI2.WinForms.Guna2GradientButton btnOk = new Guna.UI2.WinForms.Guna2GradientButton();
            btnOk.Text = "OK";
            btnOk.Size = new Size(120, 40);
            btnOk.Location = new Point(125, 105); // Centered
            btnOk.FillColor = Color.FromArgb(3, 62, 132);
            btnOk.FillColor2 = Color.FromArgb(0, 150, 200);
            btnOk.ForeColor = Color.White;
            btnOk.Font = new Font("Segoe UI Black", 10f, FontStyle.Bold);
            btnOk.Cursor = Cursors.Hand;
            btnOk.BorderRadius = 10;
            btnOk.Click += (s, e) => { this.Close(); };

            // Prevent focus highlight ring around the button
            this.Load += (s, e) => { this.ActiveControl = lblMessage; };

            this.Controls.Add(btnOk);
            this.Controls.Add(lblMessage);
            this.Controls.Add(lblIcon);
        }

        // --- SMART HELPER METHOD ---
        // Just call NetworkErrorDialog.Show(ex, this); in any catch block!
        public static void Show(Exception ex, Form parentForm = null)
        {
            // If the error came from our database network check...
            if (ex.Message.Contains("Internet connection lost"))
            {
                using (NetworkErrorDialog dialog = new NetworkErrorDialog())
                {
                    if (parentForm != null)
                        dialog.ShowDialog(parentForm);
                    else
                        dialog.ShowDialog();
                }
            }
            else
            {
                // If it's a normal application error, just show standard MessageBox
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}