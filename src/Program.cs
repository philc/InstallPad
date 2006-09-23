//
// Author: Phil Crosby
//

// Copyright (C) 2006 Phil Crosby
// Permission is granted to use, copy, modify, and merge copies
// of this software for personal use. Permission is not granted
// to use or change this software for commercial use or commercial
// redistribution. Permission is not granted to use, modify or 
// distribute this software internally within a corporation.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
namespace InstallPad
{
    /// <summary>
    /// InstallPad main application
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            
            //View("hey.zip");
            Zip.Instance.ExtractZip(
            @"C:\test\anthem.zip", @"c:\test\");
            ProcessArguments(args);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InstallPad());
        }

        private static void ProcessArguments(string[] args)
        {

            if (args.Length <= 0)
                return;

            // /f switch is a pointer to an applist.xml
            if (args[0] == "/f" && args.Length > 1)
            {
                InstallPadApp.AppListFile = args[1];
            }
        }
    }
}