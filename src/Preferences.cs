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
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace InstallPad
{
    partial class InstallPadApp
    {
        /// <summary>
        /// Application-wide preferences.
        /// </summary>
        public class Preferences
        {
            private static Configuration appConfigSettings = null;


            private static Dictionary<string, string> defaults=null;
            static Preferences()
            {
                // extract our InstallPad.exe.config file to ApplicationData path if its not already there.
                if (File.Exists(InstallPadApp.AppConfigFilePath) == false)
                {
                    // TODO: should throw a dialog if user can't write the file to disk (rare...)
                    ExtractToFile(InstallPadApp.ApplicationDataPath, "InstallPad", ".config");
                }

                // Build default list of preferences
                defaults = BuildDefaults();

                // load InstallPad.exe.config
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = InstallPadApp.AppConfigFilePath;
                appConfigSettings = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);


            }
            private static Dictionary<string,string> BuildDefaults()
            {
                // If these every become dynamic, we can just build this map every time we need it.
                Dictionary<string, string> defaults = new Dictionary<string, string>();
                try
                {
                    defaults.Add("DownloadTo", Path.GetFullPath(
                        Path.Combine(Environment.GetEnvironmentVariable("TEMP"), @"InstallPad\")));
                    defaults.Add("InstallationRoot", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
                }
                catch (Exception e) { 
                    throw new Exception("We were unable to find the full path of either your temp folder " + 
                        "or your program files folder. This might be due to some bad environment variables.",e);
                }
                return defaults;
            }

            #region Accessors for specific preferences
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
                    return GetAppSetting("AlternateDownloadLocation");
                }
                set { SetAppSetting("AlternateDownloadLocation", value); }
            }

            /// <summary>
            /// Preferred location to install packages.
            /// </summary>
            public static string InstallationRoot
            {
                get
                {
                    return GetAppSetting("InstallationRoot");
                }
                set { SetAppSetting("InstallationRoot", value); }
            }

            /// <summary>
            /// Preferred location to store downloaded files.
            /// </summary>
            public static string DownloadTo
            {
                get
                {
                    return GetAppSetting("DownloadTo");
                }
                set { SetAppSetting("DownloadTo", value); }
            }

            /// <summary>
            /// Preferred application list file to load.
            /// </summary>
            public static string AppListFile
            {
                get
                {
                    return GetAppSetting("AppListFile");
                }
                set { SetAppSetting("AppListFile", value); }
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
                if (HasAppSetting(key))
                    return appConfigSettings.AppSettings.Settings[key].Value;

                // If no preference is set, return the default for that pref
                return defaults.ContainsKey(key) ? defaults[key] : String.Empty;
            }
            /// <summary>
            /// Sets an app setting in the preferences file
            /// </summary>
            private static void SetAppSetting(String key, String value)
            {
                if (appConfigSettings.AppSettings.Settings[key] != null)
                    appConfigSettings.AppSettings.Settings[key].Value = value;
                else
                    appConfigSettings.AppSettings.Settings.Add(key, value);                
                // Persist immediatley
                AppConfigSettings.Save();
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

                                // TODO: Can this return before reading all the bytes?
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


