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
using System.Text;

namespace InstallPad
{
    class InstallPadApp
    {
        public static string AppListFile = "applist.xml";
        static InstallPadApp()
        {
        }

        private static ApplicationList appList;

        public static ApplicationList AppList
        {
            get { return InstallPadApp.appList; }
            set { InstallPadApp.appList = value; }
        }
        public static string InstallFolder
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetEnvironmentVariable("TEMP"), @"InstallPad\");
                //return System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"\InstallPad\");
                
            }
        }
        public static string ConfigFilePath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"InstallPad\config.xml");
            }
        }
    }
}
