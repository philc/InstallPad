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
using System.Xml;
using System.Collections.Generic;

using InstallPad.Properties;

namespace InstallPad
{
    public class InstallationOptions : Persistable
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

        private string installationRoot = string.Empty;

        public string InstallationRoot
        {
            get { return installationRoot; }
            set { installationRoot = value; }
        }

        private string alternateDownloadLocation = string.Empty;

        public string AlternateDownloadLocation
        {
            get { return alternateDownloadLocation; }
            set { alternateDownloadLocation = value; }
        }

        #region XML methods
        public static InstallationOptions FromXml(XmlReader reader)
        {
            InstallationOptions options = new InstallationOptions();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == Resources.InstallInOrder)
                            options.InstallInOrder = true;
                        else if (reader.Name == Resources.Proxy)
                        {
                            options.ProxyOptions = ProxyOptions.FromXml(reader);
                            options.XmlErrors.AddRange(options.ProxyOptions.XmlErrors);
                        }
                        else if (reader.Name == Resources.SilentInstall)
                        {
                            options.SilentInstall = true;
                        }
                        else if (reader.Name == Resources.SimultaneousDownloads)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                options.SimultaneousDownloads = int.Parse(reader.ReadString());
                                reader.ReadEndElement();
                            }
                        }
                        else if (reader.Name == Resources.InstallationRoot)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                options.InstallationRoot = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else if (reader.Name == Resources.AlternateDownloadLocation)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                options.AlternateDownloadLocation = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else
                            options.XmlErrors.Add(
                                String.Format("{0}: \"{1}\"", Resources.AppListXmlUnknown, reader.Name));
                        break;

                    case XmlNodeType.EndElement:
                        // Only stop reading when we've hit the end of the InstallationOptions element
                        if (reader.Name == Resources.InstallationOptions)
                            return options;
                        break;
                }

            }
            return options;
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(Resources.InstallationOptions);
            if (this.InstallInOrder)
                writer.WriteElementString(Resources.InstallInOrder, "");
            if (this.SilentInstall)
                writer.WriteElementString(Resources.SilentInstall, "");
            if (this.proxyOptions != null)
                this.proxyOptions.WriteXml(writer);
            if (this.installationRoot != string.Empty)
                writer.WriteElementString(Resources.InstallationRoot, this.InstallationRoot);
            if (this.alternateDownloadLocation != string.Empty)
                writer.WriteElementString(Resources.AlternateDownloadLocation, this.AlternateDownloadLocation);

            writer.WriteElementString(Resources.SimultaneousDownloads, this.SimultaneousDownloads.ToString());

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
