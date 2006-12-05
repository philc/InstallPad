//
// Author: Phil Crosby
// Author: Zac Ruiz
//

// Copyright (C) 2006 Phil Crosby
// Permission is granted to use, copy, modify, and merge copies
// of this software for personal use. Permission is not granted
// to use or change this software for commercial use or commercial
// redistribution. Permission is not granted to use, modify or 
// distribute this software internally within a corporation.

using System;
using System.IO;
using System.Text;
using System.Reflection;

namespace InstallPad
{
    /// <summary>
    /// Application wide resources
    /// </summary>
    partial class InstallPadApp
    {
        #region Fields and Ctor
        private static string appListFile = 
            Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "applist.xml");
        private static ApplicationList appList = null;
        private static Preferences preferences = new Preferences();

        static InstallPadApp()
        {
            if (Preferences.AppListFile != string.Empty)
                appListFile = Preferences.AppListFile;
        }
        #endregion

        #region Accessors
        /// <summary>
        /// The loaded application list.
        /// </summary>
        public static ApplicationList AppList
        {
            get { return InstallPadApp.appList; }
            set { InstallPadApp.appList = value; }
        }

        /// <summary>
        /// Location to store application data.
        /// </summary>
        public static string ApplicationDataPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"InstallPad");
            }
        }

        /// <summary>
        /// Full path of file to cache WinForm config data. 
        /// </summary>
        public static string ConfigFilePath
        {
            get
            {
                return System.IO.Path.Combine(ApplicationDataPath, @"ui.xml");
            }
        }

        /// <summary>
        /// Full path of application configuration file.
        /// </summary>
        public static string AppConfigFilePath
        {
            get
            {
                return System.IO.Path.Combine(ApplicationDataPath, @"InstallPad.exe.config");
            }
        }

        /// <summary>
        /// File containing application list.
        /// </summary>
        public static string AppListFile
        {
            get
            {
                return appListFile;
            }
            set
            {
                appListFile = value;
            }
        }
        #endregion
    }
}