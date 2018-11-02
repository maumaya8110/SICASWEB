using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Abastos_WF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CASCO.EN.General.EventLog.Init_EventLog();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
