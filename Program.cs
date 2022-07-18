using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DpsCamera {
    internal static class Program {
        [STAThread]
        static void Main() {
            string processName = Process.GetCurrentProcess().ProcessName.ToUpper();
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 1) {
                MessageBox.Show("프로그램이 이미 실행 중입니다.");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
