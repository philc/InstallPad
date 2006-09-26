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
using System.IO;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace InstallPad
{
    /// <summary>
    /// Application wide resources
    /// </summary>
    class InstallPadApp
    {
        #region Fields and Ctor
        private static string appListFile = "applist.xml";
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
        /// The location to store downloaded files.  Defaults to %TEMP%\InstallPad.
        /// </summary>
        public static string InstallFolder
        {
            get
            {
                if (Preferences.DownloadCache != string.Empty)
                    return Preferences.DownloadCache;
                return System.IO.Path.Combine(Environment.GetEnvironmentVariable("TEMP"), @"InstallPad\");
                //return System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"\InstallPad\");                
            }
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
                return System.IO.Path.Combine(ApplicationDataPath, @"config.xml");
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

        public class Preferences
        {
            #region Fields and Ctor
            private static Configuration appConfigSettings = null;

            static Preferences()
            {
                // extract our InstallPad.exe.config file to ApplicationData path if its not already there.
                if (File.Exists(AppConfigFilePath) == false)
                {
                    ExtractToFile(ApplicationDataPath, "InstallPad", ".config");
                }

                // load InstallPad.exe.config
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = AppConfigFilePath;
                appConfigSettings = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            }
            #endregion

            #region Accessors
            /// <summary>
            /// The loaded application config settings.
            /// </summary>
            public static Configuration AppConfigSettings
            {
                get { return appConfigSettings; }
            }

            /// <summary>
            /// Preferred location to search for alternate downloads.
            /// </summary>
            public static string AlternateDownloadLocation
            {
                get
                {
                    if (HasAppSetting("AlternateDownloadLocation"))
                    {
                        return GetAppSetting("AlternateDownloadLocation");
                    }
                    return string.Empty;
                }
            }

            /// <summary>
            /// Preferred location to install packages.
            /// </summary>
            public static string InstallationRoot
            {
                get
                {
                    if (HasAppSetting("InstallationRoot"))
                    {
                        return GetAppSetting("InstallationRoot");
                    }
                    return string.Empty;
                }
            }

            /// <summary>
            /// Preferred location to store downloaded files.
            /// </summary>
            public static string DownloadCache
            {
                get
                {
                    if (HasAppSetting("DownloadCache"))
                    {
                        return GetAppSetting("DownloadCache");
                    }
                    return string.Empty;
                }
            }

            /// <summary>
            /// Preferred application list file to load.
            /// </summary>
            public static string AppListFile
            {
                get
                {
                    if (HasAppSetting("AppListFile"))
                    {
                        return GetAppSetting("AppListFile");
                    }
                    return string.Empty;
                }
            }
            #endregion

            #region Helper Methods
            /// <summary>
            /// Check if AppSetting key exists.
            /// </summary>
            /// <param name="key">The key name to check.</param>
            /// <returns>True if key exists and is accessible, false otherwise.</returns>
            private static bool HasAppSetting(string key)
            {
                if (appConfigSettings == null)
                    return false;
                if (appConfigSettings.AppSettings == null)
                    return false;
                if (appConfigSettings.AppSettings.Settings == null)
                    return false;
                if (appConfigSettings.AppSettings.Settings[key] == null)
                    return false;
                if (appConfigSettings.AppSettings.Settings[key].Value == null)
                    return false;
                if (appConfigSettings.AppSettings.Settings[key].Value.Length == 0)
                    return false;
                return true;
            }

            /// <summary>
            /// Retrieve and app setting.
            /// </summary>
            /// <param name="key">The app setting key name.</param>
            /// <returns>app setting value.</returns>
            private static string GetAppSetting(string key)
            {
                return appConfigSettings.AppSettings.Settings[key].Value;
            }

            /// <summary>
            /// Extract embedded resource to file.
            /// </summary>
            /// <param name="Path">Location for output file.  If it does not exist we will create it.</param>
            /// <param name="Namespace">Embedded resources are organized by namespace.  Extract only resources in this namespace.</param>
            /// <param name="Type">Filter for resource type.<example>.config, .xml</example></param>
            private static void ExtractToFile(string Path, string Namespace, string Type)
            {
                // Make sure Path is valid
                if (Directory.Exists(Path) == false)
                    Directory.CreateDirectory(Path);

                // Get handle to executing assembly.
                Assembly theAssembly = Assembly.GetExecutingAssembly();

                // Retrieve list of embedded resource names.
                string[] resNames = theAssembly.GetManifestResourceNames();

                // Iterate through the resources.
                foreach (string s in resNames)
                {
                    // If they passed a resource type, filter on it.
                    if (Type.Length == 0 || s.EndsWith(Type))
                    {
                        int nsLoc = s.IndexOf(Namespace) + Namespace.Length + 1;

                        // Ensure namespace is present within resource name.
                        if (nsLoc < 0) throw new FileNotFoundException("Invalid namespace.");

                        string filename = System.IO.Path.Combine(Path, s.Substring(nsLoc));

                        // Extract the resource.
                        using (Stream FromStream = theAssembly.GetManifestResourceStream(s))
                        {
                            // Create a new file.
                            using (Stream ToStream = File.Create(filename))
                            {
                                // Copy the resource to the file.
                                BinaryReader br = new BinaryReader(FromStream);
                                BinaryWriter bw = new BinaryWriter(ToStream);

                                // TODO: Can this return before reading all the bytes
                                bw.Write(br.ReadBytes((int)FromStream.Length));

                                bw.Flush();
                                bw.Close();
                                br.Close();
                            }
                        }
                    }
                }
            }
            #endregion
        }
    }
}
