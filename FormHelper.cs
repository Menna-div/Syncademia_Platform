using System.Drawing;
using System.Windows.Forms;

namespace syncademia
{
    public static class FormHelper
    {
        // ── Call this in every Form's constructor and Resize event ─────────
        // It keeps all controls centered when the window is maximized
        public static void CenterControls(Form form)
        {
            int centerX = form.ClientSize.Width  / 2;
            int centerY = form.ClientSize.Height / 2;

            foreach (Control ctrl in form.Controls)
            {
                // keep the control's original distance from center
                ctrl.Left = centerX - ctrl.Width  / 2 + (ctrl.Left  - form.ClientSize.Width  / 2);
                ctrl.Top  = centerY - ctrl.Height / 2 + (ctrl.Top   - form.ClientSize.Height / 2);
            }
        }

        // ── Call this for the mentorpage specifically ──────────────────────
        // The sidebar stays docked left, only panelMain content is centered
        public static void ApplyResponsiveLayout(Form form)
        {
            form.Resize += (s, e) =>
            {
                foreach (Control ctrl in form.Controls)
                {
                    // skip docked controls — they handle themselves
                    if (ctrl.Dock != DockStyle.None) continue;
                    ctrl.Left = (form.ClientSize.Width - ctrl.Width) / 2;
                }
            };
        }
    }
}