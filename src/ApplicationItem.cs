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

namespace InstallPad
{
    public class ApplicationItem
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
        /// <summary>
        /// IF the user has requested that we find the latest version of the software,
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
                if (this.originalVersion!=null){
                    
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
        public string DownloadUrl
        {
            get { 
                
                return downloadUrl; 
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
            
            for (int i = downloadUrl.Length - 1; i > 0; i--)
            {
                if (!IsNumber(downloadUrl[i]))
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
                if (!IsDelimeterCharacter(downloadUrl[i]))
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
            this.parsedDownloadUrl = this.downloadUrl;

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
            while (index>=0 && IsNumber(downloadUrl[index]))
            {
                // if the number is 
                s=s.Insert(0, downloadUrl[index].ToString());
                index--;
            }
            return s;
        }

        private void ParseDownloadUrl2()
        {
            
            List<int> intsEncountered = new List<int>();
            List<int> indexOfInt = new List<int>();

            // Search from right to left find integers separated by periods.
            // We interpret this pattern as being the version.
            for (int i = downloadUrl.Length - 1; i > 0; i--)
            {
                // If we have a delimeter, search for an int after it
                if (IsDelimeterCharacter(downloadUrl[i]))
                {
                    int n = FindInteger(downloadUrl, i - 1);
                    if (n > -1)
                    {
                        //store in reverse order that we found them
                        intsEncountered.Insert(0, n);

                        // Digit can be two chars big
                        int sizeOfInt = n < 10 ? 1 : 2;
                        i = i - sizeOfInt;
                        indexOfInt.Insert(0, i); 
                        
                    }
                }
                else
                {
                    // If we found a revision that's two lengths long, and now we're no
                    // longer near a delimeter, go ahead and say we're done.
                    if (intsEncountered.Count < 2)
                    {
                        // Otherwise, clear what we thought was a version number and keep searching
                        intsEncountered.Clear();
                        indexOfInt.Clear();
                    }
                    else
                        // Found a version string with two or more elements. That's good enough. We're done.
                        break;
                }
                if (intsEncountered.Count >= 4)
                    // We're done. 4 is enough version parts.
                    break;
            }

            this.parsedDownloadUrl = this.downloadUrl;

            // Replace all the integers we found with {0} etc.
            // Go backwards so that when we start replacing characters with {0} etc., the stored indices don't get all jacked up.
            for (int i = indexOfInt.Count - 1; i >= 0; i--)
            {
                this.parsedDownloadUrl = this.parsedDownloadUrl.Remove(indexOfInt[i], (intsEncountered[i] < 10 ? 1 : 2));
                this.parsedDownloadUrl = this.parsedDownloadUrl.Insert(indexOfInt[i], "{" + i + "}");
            }

            // This is the original version we've picked up from our parsed url.
            this.originalVersion = intsEncountered;
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
        public static ApplicationItem FromXml(XmlReader reader, List<string> errors)
        {
            ApplicationItem item = new ApplicationItem();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "Name")
                        {
                            item.Name = reader.ReadString();
                            reader.ReadEndElement();
                        }
                        else if (reader.Name == "FileUrl")
                        {
                            item.DownloadUrl = reader.ReadString();
                            reader.ReadEndElement();
                        }
                        else if (reader.Name == "Options")
                        {
                            item.Options = ApplicationItemOptions.FromXml(reader, errors);
                        }
                        else
                        {
                            errors.Add(
                                String.Format("Unrecognized element in an application: \"{0}\"", reader.Name));
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name=="Application")
                            return item;
                        break;
                }
            }
            return item;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Application");
            writer.WriteElementString("Name", this.Name);
            writer.WriteElementString("FileUrl", this.DownloadUrl);
            this.Options.WriteXml(writer);

            writer.WriteEndElement();
        }
        #endregion 
    }

    public class ApplicationItemOptions
    {
        #region XML methods
        public static  ApplicationItemOptions FromXml(XmlReader reader, List<string> errors)
        {
            ApplicationItemOptions options = new ApplicationItemOptions();
            
            // If this is an empty option element then don't read further
            if (reader.IsEmptyElement)
                return options;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "DownloadLatestVersion")
                        {
                            options.DownloadLatestVersion = true;
                        }
                        else if (reader.Name == "SilentInstall")
                        {
                            options.SilentInstall = true;
                        }
                        else if (reader.Name == "PostInstallScript")
                        {
                            options.PostInstallScript = reader.ReadString();
                            reader.ReadEndElement();
                        }
                        else if (reader.Name == "InstallerArguments")
                        {
                            options.InstallerArguments = reader.ReadString();
                            reader.ReadEndElement();
                        }
                        else
                            errors.Add(String.Format("Unrecognized application option: \"{0}\"", reader.Name));

                        break;
                    case XmlNodeType.EndElement:
                        // Only stop reading when we've hit the end of the Options element
                        if (reader.Name == "Options")
                            return options;
                        break;
                }
            }
            return options;
        }
        public void WriteXml(XmlWriter writer)
        {
            // Only write if there is an option set. This could be more elegant.
            if ((InstallerArguments != null && InstallerArguments.Length > 0) ||
                (PostInstallScript != null && PostInstallScript.Length > 0) ||
                this.SilentInstall || this.DownloadLatestVersion)
            {

                writer.WriteStartElement("Options");
                if (this.DownloadLatestVersion)
                    writer.WriteElementString("DownloadLatestVersion", "");
                if (this.SilentInstall)
                    writer.WriteElementString("SilentInstall", "");

                if (InstallerArguments != null && InstallerArguments.Length > 0)
                    writer.WriteElementString("InstallerArguments", this.InstallerArguments);
                if (PostInstallScript != null && PostInstallScript.Length > 0)
                    writer.WriteElementString("PostInstallScript", this.PostInstallScript);

                writer.WriteEndElement();
            }
        }
        #endregion

        private bool downloadLatestVersion=false;

        public bool DownloadLatestVersion
        {
            get { return downloadLatestVersion; }
            set { downloadLatestVersion = value; }
        }

        private bool silentInstall = false;
        public bool SilentInstall
        {
            get { return silentInstall; }
            set { silentInstall = value; }
        }
        private string installerArguments=null;

        public string InstallerArguments
        {
            get { return installerArguments; }
            set { installerArguments = value; }
        }
        private string postInstallScript=null;

        public string PostInstallScript
        {
            get { return postInstallScript; }
            set { postInstallScript = value; }
        }


    }
}
