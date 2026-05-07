using System;
using System.Windows.Forms;

namespace syncademia
{
    static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // يبدأ البرنامج بفتح واجهة تسجيل الدخول الرئيسية
            Application.Run(new SYNCADEMIA()); 
        }
    }
}