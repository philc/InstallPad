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
using System.Xml;

using Microsoft.Win32;

using InstallPad.Properties;

namespace InstallPad
{
    /// <summary>
    /// An application item describes an application - its name, filename, and download options.
    /// It can also attempt to check and find the latest version of the application online.
    /// </summary>
    public class ApplicationItem : Persistable
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string downloadUrl;

        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(DownloadUrl);
            }
        }

        string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        /// <summary>
        /// If the user has requested that we find the latest version of the software,
        /// this will return the regular expression of the software's URL filled
        /// in with the latest software, if possible.
        /// </summary>
        /// <returns></returns>
        public string FindLatestUrl()
        {
            RemoteVersionChecker checker = new RemoteVersionChecker();
            List<int> versionString = null;

            // If we need to, build the download URL by going out there and finding the latest version
            if (options.DownloadLatestVersion && this.version == null)
            {
                // If original version is null, we couldn't aprse anything from the download URL
                if (this.originalVersion!=null)
                {   
                versionString = checker.LatestVersion(this.parsedDownloadUrl, this.originalVersion);
                this.downloadUrl = RemoteVersionChecker.FillInUrl(this.parsedDownloadUrl, versionString);

                // Update our version
                for (int i = 0; i < versionString.Count; i++)
                    // Avoid adding a period onto the last part of the string
                    this.version = this.version + i + ((i == versionString.Count - 1) ? "" : ".");
                }
            }
            
            return this.downloadUrl;
        }

        /// <summary>
        /// Creates an ordered list of download URLs to try - primary, alternate, etc.
        /// </summary>
        public List<string> CreateOrderedUrlList()
        {
            List<string> urlList = new List<string>();

            // first would be alternate download location from preferences (because its probably local cache)
            // TODO add this, and a UI for it.

            // next would be the specified url
            urlList.Add(FindLatestUrl());

            // next would be appitem alternate download location
            foreach (String s in options.AlternateFileUrls)
            {
                if (s.Length > 0)
                    urlList.Add(s);
            }
            return urlList;
        }

        public string DownloadUrl
        {
            get {                 
                return downloadUrl.Trim(); 
            }
            set {
                downloadUrl = value;
                if (options.DownloadLatestVersion)
                    ParseDownloadUrl();
            }
        }
        string version=null;

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        string detectedVersion = string.Empty;
        public string DetectedVersion
        {
            get { return detectedVersion; }
        }

        string uninstallString = string.Empty;
        public string UnInstallString
        {
            get { return uninstallString; }
        }

        /// <summary>
        /// Detect the version that's already installed.
        /// </summary>
        /// <returns>Whether the version was successfully retrieved from the registry</returns>
        public bool DetectVersion()
        {
            RegistryKey UninstallKey = Registry.LocalMachine;
            UninstallKey = UninstallKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);

            // If this app doesn't have a name, we can't find it in the registry..
            if (name == null || name == "")
                return false;

            try
            {
                // Iterate subkeys... each subkey is an installed package
                foreach (string subkeyname in UninstallKey.GetSubKeyNames())
                {
                    RegistryKey pkg = Registry.LocalMachine;
                    pkg = UninstallKey.OpenSubKey(subkeyname, true);

                    try
                    {
                        object pkgValue = pkg.GetValue(Resources.DisplayName);
                        if (pkgValue == null)
                            continue;
                        string displayName=pkgValue.ToString();

                        if (displayName.Contains(name) || name.Contains(displayName))
                        {
                            try
                            {
                                object versionValue = pkg.GetValue(Resources.DisplayVersion);
                                if (versionValue!=null)
                                    detectedVersion = versionValue.ToString();

                                object uninstallValue = pkg.GetValue("UninstallString").ToString();
                                if (uninstallValue != null)
                                    uninstallString = uninstallValue.ToString();

                                return true;
                            }
                            catch
                            {
                                detectedVersion = Resources.AppVersionNotFound;
                            }
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        pkg.Close();
                    }
                }
            }
            finally
            {
                UninstallKey.Close();
            }
            return false;
        }

        ApplicationItemOptions options = new ApplicationItemOptions();

        public ApplicationItemOptions Options
        {
            get { return options; }
            set { 
                options = value;
                if (options.DownloadLatestVersion)
                    ParseDownloadUrl();
            }
        }
        private List<int> originalVersion;
        private string parsedDownloadUrl = "";

        private bool IsNumber(char c)
        {
            int i = (int)c;
            return (i >= 48 && i <= 57) ;
        }
        private void ParseDownloadUrl()
        {
            // Keep track of the ints we find that may be versions, and also where we found them
            List<int> intsEncountered = new List<int>();
            List<int> indexOfInt = new List<int>();            
            
            for (int i = DownloadUrl.Length - 1; i > 0; i--)
            {
                if (!IsNumber(DownloadUrl[i]))
                    continue;
                
                // Found a number. Search to the left until we find the end of the int.
                string s = SearchLeftForInt(i);
                i -= s.Length;
                intsEncountered.Insert(0, int.Parse(s));
                indexOfInt.Insert(0, i+1);

                if (intsEncountered.Count >= 3)
                {
                    // We've found three version numbers. That's good enough. We're done parsing.
                    break;
                }
                if (i < 0)
                    break;

                // After finding the int, if our next character isn't a delimeter we should
                // exit if we have two version numbers; otherwise, keep searching through the string.
                if (!IsDelimeterCharacter(DownloadUrl[i]))
                {
                    if (intsEncountered.Count >= 2)
                        break;
                    else
                    {
                        intsEncountered.Clear();
                        indexOfInt.Clear();
                    }
                }
            }
            this.originalVersion = intsEncountered;
            this.parsedDownloadUrl = this.DownloadUrl;

            // Replace all the integers we found with {0}, {1}, ... so the version string looks like firefox-{0}.{1}.exe
            // Go backwards so that when we start replacing characters with {0} etc., the stored indices don't get all jacked up.
            for (int i = indexOfInt.Count - 1; i >= 0; i--)
            {
                this.parsedDownloadUrl = this.parsedDownloadUrl.Remove(indexOfInt[i], (intsEncountered[i] < 10 ? 1 : 2));
                this.parsedDownloadUrl = this.parsedDownloadUrl.Insert(indexOfInt[i], "{" + i + "}");
            }

        }
        private string SearchLeftForInt(int index)
        {
            string s = "";
            while (index>=0 && IsNumber(DownloadUrl[index]))
            {
                // if the number is 
                s=s.Insert(0, DownloadUrl[index].ToString());
                index--;
            }
            return s;
        }

   

        private bool IsDelimeterCharacter(char c)
        {
            return c == '.' || c == '_' || c == '-';
        }
        /// <summary>
        /// Searches for an integer in the provided string, to the left of index.
        /// Integers can be more than one character, and this method will parse such integers.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static int FindInteger(string str, int index)
        {
            String found="";
            while (index > 0)
            {
                // If it's a digit
                if (IsDigit(str[index]))
                    found = str[index] + found;
                else
                    break;
                index--;
            }
            if (found == "")
                return -1;
            else
                return int.Parse(found);
        }
        private static bool IsDigit(char c)
        {
            return c > 47 && c < 58;
        }

        #region Xml methods
        public static ApplicationItem FromXml(XmlReader reader)
        {
            ApplicationItem item = new ApplicationItem();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == Resources.Name)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                item.Name = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else if (reader.Name == Resources.FileUrl)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                item.DownloadUrl = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else if (reader.Name == Resources.Comment)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                item.Comment = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else if (reader.Name == Resources.Options)
                        {
                            item.Options = ApplicationItemOptions.FromXml(reader);
                            item.XmlErrors.AddRange(item.options.XmlErrors);
                        }
                        else
                        {
                            item.XmlErrors.Add(
                                String.Format("{0}: \"{1}\"", Resources.AppListUnknownElement, reader.Name));
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == Resources.Application)
                        {
                            item.DetectVersion();
                            return item;
                        }
                        break;
                }
            }
            item.DetectVersion();
            return item;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(Resources.Application);
            writer.WriteElementString(Resources.Name, this.Name);
			writer.WriteElementString(Resources.FileUrl, this.DownloadUrl);
            
            if (this.Comment != null && this.Comment.Length > 0)
            {
                writer.WriteElementString(Resources.Comment, this.Comment);
            }

            this.Options.WriteXml(writer);

            writer.WriteEndElement();
        }
        #endregion 

        #region Persistable
        public override bool Validate()
        {
            return true;
        }
        #endregion
    }
}
