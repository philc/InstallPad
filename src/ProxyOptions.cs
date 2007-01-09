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
    public class ProxyOptions : Persistable
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
        public static ProxyOptions FromXml(XmlReader reader)
        {
            ProxyOptions options = new ProxyOptions();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == Resources.Address)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                options.Address = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else if (reader.Name == Resources.Username)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                options.Username = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else if (reader.Name == Resources.Password)
                        {
                            if (reader.IsEmptyElement == false)
                            {
                                options.Password = reader.ReadString();
                                reader.ReadEndElement();
                            }
                        }
                        else
                            options.XmlErrors.Add(
                                String.Format("{0}: \"{1}\"", Resources.BadProxyOption, reader.Name));
                        break;

                    case XmlNodeType.EndElement:
                        // Only stop reading when we've hit the end of the InstallationOptions element
                        if (reader.Name == Resources.Proxy)
                            return options;
                        break;
                }

            }
            return options;
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(Resources.Proxy);
            if (this.Address != null)
                writer.WriteElementString(Resources.Address, this.Address);
            if (this.Username != null)
                writer.WriteElementString(Resources.Username, this.Username);
            if (this.Password != null)
                writer.WriteElementString(Resources.Password, this.Password);
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
