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
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace InstallPad
{
    /// <summary>
    /// Zip utilities. Access through Zip.Instance
    /// </summary>
    public class Zip
    {
        
        private static string ASSEMBLY_RESOURCE_STRING = "InstallPad.lib.ICSharpCode.SharpZipLib.dll";

        // Reflected types
        private Assembly zipAssembly = null;

        Type fastZipType = null;
        Type zipFileType = null;
        Type zipEntryType = null;

        object fastZipInstance;

        private static Zip instance = null;

        /// <summary>
        /// Singleton
        /// </summary>
        public static Zip Instance
        {
            get
            {
                if (instance == null)
                    instance = new Zip();
                return instance;
            }
        }

        private Zip()
        {
            // Load types
            this.zipAssembly = LoadAssemblyFromResource(ASSEMBLY_RESOURCE_STRING);

            fastZipType = zipAssembly.GetType("ICSharpCode.SharpZipLib.Zip.FastZip");
            fastZipInstance = Activator.CreateInstance(fastZipType);

            zipFileType = zipAssembly.GetType("ICSharpCode.SharpZipLib.Zip.ZipFile");
            zipEntryType = zipAssembly.GetType("ICSharpCode.SharpZipLib.Zip.ZipEntry");
        }

        /// <summary>
        /// This method determines whether all the files in the specified zip file are rooted
        /// within a single directory.
        /// </summary>
        /// <remarks>
        /// Signatures of what we're calling:
        ///     ZipFile zf = new ZipFile(zipFile);
        ///     foreach(ZipEntry ze in zf)
        ///     {
        ///        string name = ze.Name;
        ///     }
        /// </remarks>
        /// <param name="zipFile">Zip file to examine.</param>
        /// <returns></returns>
        public bool HasRootFolder(string zipFile)
        {
            // Construct new ZipFile.
            IEnumerable zipFileInstance = (IEnumerable)Activator.CreateInstance(zipFileType, new object[1] { zipFile });

            try
            {
                // Iterate ZipEntries.
                string temp = string.Empty;
                foreach (object e in zipFileInstance)
                {
                    // Retrieve zip entry name.
                    object oName = zipEntryType.GetProperty("Name").GetValue(e, null);
                    string sName = (string)oName;

                    // Scan for root folder.
                    int offset = sName.IndexOf(@"/");

                    // If there are no folders, return as such.
                    if (offset == -1)
                    {
                        return false;
                    }

                    // Save first folder for comparison to every other zip entry name.
                    if (temp == string.Empty)
                    {
                        temp = sName.Substring(0, offset);
                    }
                    else
                    {
                        // If any zip entry name does not begin with same root folder, return false.
                        if (sName.Substring(0, offset) != temp)
                            return false;
                    }
                }
            }
            finally
            {
                zipFileType.InvokeMember("Close", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, zipFileInstance, null);
            }

            return true;
        }

        /// <summary>
        /// This method determines whether a given file exists in a specified zip file.
        /// </summary>
        /// <remarks>
        /// Signatures of what we're calling:
        ///     ZipFile zf = new ZipFile(zipFile);
        ///     foreach(ZipEntry ze in zf)
        ///     {
        ///        string name = ze.Name;
        ///     }
        /// </remarks>
        /// <param name="zipFile"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public bool Contains(string zipFile, string searchCriteria)
        {
            // Construct new ZipFile.
            IEnumerable zipFileInstance = (IEnumerable)Activator.CreateInstance(zipFileType, new object[1] { zipFile });

            try
            {
                // Iterate ZipEntries.
                string temp = string.Empty;
                foreach (object e in zipFileInstance)
                {
                    // Retrieve zip entry name.
                    object oName = zipEntryType.GetProperty("Name").GetValue(e, null);
                    string sName = (string)oName;

                    if (sName.Contains(searchCriteria))
                    {
                        return true;
                    }
                }
            }
            finally
            {
                zipFileType.InvokeMember("Close", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, zipFileInstance, null);
            }

            return false;
        }

        public void ExtractZip(string zipFile, string extractTo)
        {
            // There is a bug in the underlying zip implementation. It can't unzip a zip that has a 
            // 0 length file (like an empty text file) in it
            // http://community.sharpdevelop.net/forums/thread/11539.aspx

            // Signature of what we're calling
            /*
             * public void ExtractZip(
             * string zipFileName,
             * string targetDirectory,
             * string fileFilter)
             */

            // URI formats are not supported, e.g. you can't pass in file:///
            // if we get a file:/// url, just strip it out.
            zipFile = zipFile.Replace("file:///", "").Replace("file://","");

            try
            {
                fastZipType.InvokeMember("ExtractZip", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                    null, fastZipInstance, new object[]{
                zipFile,extractTo,""});
            }
            catch (Exception e)
            {
                throw (e.InnerException == null) ? e : e.InnerException;
                // TODO do a swtich statement on the inner exception, to handle e.g. file not found
                // and throw that exception
            }
        }

        /// <summary>
        /// Loads an assembly from a resource string.
        /// </summary>
        /// <param name="assemblyResourceString"></param>
        /// <returns></returns>
        private static Assembly LoadAssemblyFromResource(string assemblyResourceString)
        {
            Assembly a;
            using (Stream stream = Assembly.GetExecutingAssembly().
                GetManifestResourceStream(assemblyResourceString))
            {
                int length = (int)stream.Length;
                byte[] buffer = new byte[length];

                // move the contents of the stream to the buffer
                stream.Read(buffer, 0, length);

                // load the resource bytes into an assembly
                a = Assembly.Load(buffer);
            }
            return a;
        }

    }
}
