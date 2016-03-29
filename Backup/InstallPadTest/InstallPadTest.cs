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
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace InstallPadTest
{
    class InstallPadTest
    {
        public static readonly string ONLINETESTPATH = "http://www.philisoft.com/projects/installpad/test/";

        public static string GetDownloadPath(string file)
        {
            return Path.Combine(InstallPadTest.ONLINETESTPATH, file);
        }

        /// <summary>
        /// Verify existance and size of file, in bytes.
        /// </summary>
        public static bool VerifyExistenceAndSize(string filename, long size)
        {
            if (!File.Exists(filename))
                return false;

            FileInfo info = new FileInfo(filename);
            if (info.Length != size)
            {
                Debug.WriteLine(String.Format("File size is {0}, expecting {1}", info.Length, size));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Extract embedded resource to file.
        /// </summary>
        /// <param name="Path">Location for output file.  If it does not exist we will create it.</param>
        /// <param name="Namespace">Embedded resources are organized by namespace.  Extract only resources in this namespace.</param>
        /// <param name="Type">Filter for resource type.<example>.config, .xml</example></param>
        public static void ExtractToFile(string Path, string Namespace, string Type)
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
    }
}
