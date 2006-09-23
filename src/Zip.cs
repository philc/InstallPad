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
using System.Reflection;
namespace InstallPad
{
    /// <summary>
    /// Zip utilities. Access through Zip.Instance
    /// </summary>
    class Zip
    {
        // 
        private static string ASSEMBLY_RESOURCE_STRING = "InstallPad.lib.ICSharpCode.SharpZipLib.dll";

        // Reflected types
        private Assembly zipAssembly = null;
        Type fastZipType = null;
        object fastZipInstance;

        private static Zip instance=null;

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
        }

        public void ExtractZip(string zipFile, string extractTo)
        {
            // Signature of what we're calling
            /*
             * public void ExtractZip(
             * string zipFileName,
             * string targetDirectory,
             * string fileFilter)
             */

            try
            {
                fastZipType.InvokeMember("ExtractZip", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                    null, fastZipInstance, new object[]{
                zipFile,extractTo,""});
            }
            catch (Exception e)
            {
                // do a swtich statement on the inner exception, to handle e.g. file not found
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
            Type fastZip;
            using (Stream stream = Assembly.GetExecutingAssembly().
                GetManifestResourceStream(assemblyResourceString))
            {
                // get the length
                int length = (int)stream.Length;

                // set up the buffer
                byte[] buffer = new byte[length];

                // move the contents of the stream to the buffer
                stream.Read(buffer, 0, length);

                // load the resource bytes into an assembly
                a = Assembly.Load(buffer);

                // get a specific type out of the assembly to use
                fastZip = a.GetType("ICSharpCode.SharpZipLib.Zip.FastZip");
            }
            return a;
        }

    }
}
