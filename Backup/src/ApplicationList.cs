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
using System.Diagnostics;
using System.ComponentModel;

using InstallPad.Properties;

namespace InstallPad
{
    class ApplicationList : Persistable
    {
        private ApplicationList() { }
        private List<ApplicationItem> applicationItems = new List<ApplicationItem>();
        private string fileName;

        /// <summary>
        /// Where this was loaded from, if anywhere.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public List<ApplicationItem> ApplicationItems
        {
            get { return applicationItems; }
            set { applicationItems = value; }
        }

        InstallationOptions installationOptions = new InstallationOptions();

        public InstallationOptions InstallationOptions
        {
            get { return installationOptions; }
            set { installationOptions = value; }
        }

        public static ApplicationList FromFile(string path)
        {
            // Annoying. Read in the whole file, replace ampersands and other invalid characters
            // in urls, or our xml reader will crash.
            System.IO.TextReader reader = null;

            string fileContents;

            reader = new System.IO.StreamReader(path);
            try
            {
                fileContents = EscapeStandaloneAmpersands(reader.ReadToEnd());
            }
            finally
            {
                reader.Close();
            }

            return FromXml(XmlReader.Create(new System.IO.StringReader(fileContents)));
        }
        private static string EscapeStandaloneAmpersands(String s)
        {
            // XML uses ampersands to escape special characters, e.g. &lt;
            // Users can also paste ampersands from a URL directly into the applist XML
            // document. Even though that's not valid XML, we still want to read it in,
            // so we have to preprocess those standalone ampersands and escape them.

            int i = -1;
            do
            {
                i++;
                i = s.IndexOf("&", i);
                if (i < 0)
                    break;

                // IF the next character is a #, it's an encoding
                if ((s.Length > i + 1) && s[i + 1] == '#')
                    continue;

                if (s.Length > (i + 5))
                {
                    string temp = s.Substring(i, 6);
                    if (!IsEscapedChar(temp))
                        s = s.Substring(0, i) + "&amp;" + s.Substring(i + 1);
                }
            } while (i < s.Length);

            return s;
        }
        private static bool IsEscapedChar(string s)
        {
            // These are the chars we care about preserving
            if (s.Contains("&quot;") || s.Contains("&amp;") || s.Contains("&lt;") || s.Contains("&gt;"))
                return true;
            return false;
        }

        public void SaveToFile()
        {
            if (this.FileName == null)
                throw new ArgumentException(Resources.AppListMissingFilename);

            //get original file contents
            System.IO.StreamReader originalReader = null;
            System.IO.StreamWriter originalWriter = null;
            string originalContents;

            originalReader = new System.IO.StreamReader(this.FileName);
            try
            {
                //store contents of the file in case it needs to be written back out
                //in the case of an error writing the XML to file.
                originalContents = originalReader.ReadToEnd();
            }
            finally
            {
                originalReader.Close();
            }

            try
            {
                XmlTextWriter writer = new XmlTextWriter(this.fileName, null);
                try
                {
                    writer.Formatting = Formatting.Indented;
                    this.WriteXml(writer);
                }
                finally
                {
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                // write the original contents of the file back out so no data is lost.
                originalWriter = new System.IO.StreamWriter(this.FileName);
                try
                {
                    originalWriter.Write(originalContents);
                }
                finally
                {
                    originalWriter.Close();
                }

                // throw the exception so that the user can see the error and report it.
                throw (e);
            }
        }

        #region XML methods
        private static ApplicationList FromXml(XmlReader reader)
        {
            ApplicationList list = new ApplicationList();

            // Read through the root node
            reader.ReadStartElement();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name.Equals(Resources.Application) && !reader.IsEmptyElement)
                    {
                        ApplicationItem ai = ApplicationItem.FromXml(reader);
                        list.XmlErrors.AddRange(ai.XmlErrors);
                        list.applicationItems.Add(ai);
                    }
                    else if (reader.Name.Equals(Resources.InstallationOptions) && !reader.IsEmptyElement)
                    {
                        list.installationOptions = InstallationOptions.FromXml(reader);
                        list.XmlErrors.AddRange(list.installationOptions.XmlErrors);
                    }
                    else
                    {
                        list.XmlErrors.Add(String.Format("{0}: \"{1}\"", Resources.AppListXmlUnknown, reader.Name));
                    }
                }
            }
            return list;
        }
        private void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(Resources.ApplicationList);
            this.InstallationOptions.WriteXml(writer);
            foreach (ApplicationItem item in this.ApplicationItems)
                item.WriteXml(writer);
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
