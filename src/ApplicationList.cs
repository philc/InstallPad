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

namespace InstallPad
{
    class ApplicationList
    {
        private ApplicationList() { }
        private List<ApplicationItem> applicationItems = new List<ApplicationItem>();
        private List<string> errors = new List<string>();
        private string fileName;

        /// <summary>
        /// Where this was loaded from, if anywhere.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public List<string> Errors
        {
            get { return errors; }
            set { errors = value; }
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
            System.IO.TextReader reader=null;
            reader = new System.IO.StreamReader(path);
            String fileContents = EscapeStandaloneAmpersands(reader.ReadToEnd());
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
                throw new ArgumentException("Can't save an application list without a filename.");

            XmlTextWriter writer = new XmlTextWriter(this.fileName,null);
            writer.Formatting = Formatting.Indented;
            this.WriteXml(writer);
            writer.Close();
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
                    if (reader.Name.Equals("Application") && !reader.IsEmptyElement)
                        list.applicationItems.Add(ApplicationItem.FromXml(reader, list.errors));
                    else if (reader.Name.Equals("InstallationOptions") && !reader.IsEmptyElement)
                        list.installationOptions = InstallationOptions.FromXml(reader,list.errors);
                    else
                        list.errors.Add(String.Format("Unrecognized element: \"{0}\"", reader.Name));
                }                
            }
            return list;
        }
        private void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ApplicationList");
            this.InstallationOptions.WriteXml(writer);
            foreach (ApplicationItem item in this.ApplicationItems)
                item.WriteXml(writer);
            writer.WriteEndElement();
        }
        #endregion
    }

    public class InstallationOptions
    {
        private bool installInOrder = false;

        public bool InstallInOrder
        {
            get { return installInOrder; }
            set { installInOrder = value; }
        }

        private bool silentInstall = false;

        public bool SilentInstall
        {
            get { return silentInstall; }
            set { silentInstall = value; }
        }

        private int simultaneousDownloads = 2;

        public int SimultaneousDownloads
        {
            get { return simultaneousDownloads; }
            set { simultaneousDownloads = value; }
        }

        private ProxyOptions proxyOptions = null;

        public ProxyOptions ProxyOptions
        {
            get { return proxyOptions; }
            set { proxyOptions = value; }
        }


        #region XML methods
        public static InstallationOptions FromXml(XmlReader reader, List<String> errors)
        {
            InstallationOptions options = new InstallationOptions();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "InstallInOrder")
                            options.InstallInOrder = true;
                        else if (reader.Name == "Proxy")
                        {
                            options.ProxyOptions = ProxyOptions.FromXml(reader, errors);
                        }
                        else if (reader.Name == "SilentInstall")
                        {
                            options.SilentInstall = true;
                        }
                        else if (reader.Name == "SimultaneousDownloads")
                        {
                            options.SimultaneousDownloads = int.Parse(reader.ReadString());
                            reader.ReadEndElement();
                        }
                        else
                            errors.Add(
                                String.Format("Unrecognized installation option: \"{0}\"", reader.Name));
                        break;

                    case XmlNodeType.EndElement:
                        // Only stop reading when we've hit the end of the InstallationOptions element
                        if (reader.Name == "InstallationOptions")
                            return options;
                        break;
                }

            }
            return options;
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("InstallationOptions");
            if (this.InstallInOrder)
                writer.WriteElementString("InstallInOrder", "");
            if (this.SilentInstall)
                writer.WriteElementString("SilentInstall", "");
            if (this.proxyOptions != null)
                this.proxyOptions.WriteXml(writer);
            
            writer.WriteElementString("SimultaneousDownloads", this.SimultaneousDownloads.ToString());
            writer.WriteEndElement();
        }
        #endregion
    }

    public class ProxyOptions
    {
        private string address = null;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string username = null;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password = null;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public System.Net.WebProxy ProxyFromOptions()
        {
            System.Net.WebProxy proxy = new System.Net.WebProxy();
            Uri addressUri = null;

            // Try and create this URi from the address given in the file. It can fail if it's
            // malformed, or it can fail if they specify an IP without http://.
            if (!Uri.TryCreate(address, UriKind.Absolute, out addressUri))
            {
                // Try and create it again, appending http:// this time (rooting it)
                if (!Uri.TryCreate("http://" + address, UriKind.Absolute, out addressUri))
                {
                    // Could not understand the URL. Exception case.
                    return null;
                }
            }

            proxy.Address = addressUri;
            if (this.username == null || this.password == null)
                proxy.UseDefaultCredentials = true;
            else
                proxy.Credentials = new System.Net.NetworkCredential(this.username, this.password);
            return proxy;
        }

        #region XML methods
        public static ProxyOptions FromXml(XmlReader reader, List<String> errors)
        {
            ProxyOptions options = new ProxyOptions();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "Address")
                        {
                            options.Address = reader.ReadString();
                            reader.ReadEndElement();
                        }
                        else if (reader.Name == "Username")
                        {
                            options.Username = reader.ReadString();
                            reader.ReadEndElement();
                        }
                        else if (reader.Name == "Password")
                        {
                            options.Password = reader.ReadString();
                            reader.ReadEndElement();
                        }
                        else
                            errors.Add(
                                String.Format("Unrecognized proxy option: \"{0}\"", reader.Name));
                        break;

                    case XmlNodeType.EndElement:
                        // Only stop reading when we've hit the end of the InstallationOptions element
                        if (reader.Name == "Proxy")
                            return options;
                        break;
                }

            }
            return options;
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Proxy");
            if (this.Address !=null)
                writer.WriteElementString("Address", this.Address);
            if (this.Username!=null)
                writer.WriteElementString("Username", this.Username);
            if (this.Password != null)
                writer.WriteElementString("Password", this.Password);
            writer.WriteEndElement();
        }
        #endregion
    }
}
