using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InstallPad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            ProcessArguments(args);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InstallPad());
        }
        private static void ProcessArguments(string[] args)
        {
            if (args.Length <= 0)
                return;
            // Process command line arguments
            if (args[0] == "/f" && args.Length > 1)
            {
                InstallPadApp.AppListFile = args[1];
            }
        }
    }
}