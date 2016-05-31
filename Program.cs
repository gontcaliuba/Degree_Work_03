using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DegreeWork_01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RemindRange rem = new RemindRange();
            //rem.addMessage(new DateTime(2016, 05, 26, 19, 59, 0), "Напоминание 5");
            Application.Run(new MainForm());
        }
    }
}
