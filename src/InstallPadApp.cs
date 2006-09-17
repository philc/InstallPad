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
