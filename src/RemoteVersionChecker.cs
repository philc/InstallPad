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
using System.Net;
using System.Diagnostics;
using System.Text;

namespace InstallPad
{
    class RemoteVersionChecker
    {
        private List<int> versions = new List<int>();

        private List<int> foundVersion = null;        
        private List<int> startingVersion = null;


        public List<int> LatestVersion(string url, List<int> startingVersion){
            if (startingVersion == null)
                return null;
            this.startingVersion = startingVersion;

            List<int> currentVersion = new List<int>(new int[startingVersion.Count]);
            return CheckVersion(url, currentVersion, 0);
        }
        public static string FillInUrl(string url, List<int> version)
        {
            for (int i = 0; i < version.Count; i++)
                url = url.Replace("{" + i + "}", version[i].ToString());
            return url;
        }

        private static bool SameVersionSoFar(List<int> currentVersion, List<int> otherVersion, int ordinal)
        {
            // Explain why ordinal has to be >0
            if (ordinal == 0)
                return true;
            for (int i = 0; i < ordinal-1; i++)
                if (currentVersion[i] != otherVersion[i])
                    return false;

            return true;

        }

        private List<int> CheckVersion(string url, List<int> currentVersion, int ordinal)
        {
            //1.2.8.exe
            //string url = "http://superb-west.dl.sourceforge.net/sourceforge/synergy2/SynergyInstaller-";
            bool found = false;

            if (ordinal > currentVersion.Count-1)
                return currentVersion;
            // If our version looks like the one given to us from the file, start at that
            // version number, not one lower! E.g. if starting version is 3.2.2, start with 3, not 0.
            int ordinalValue = SameVersionSoFar(currentVersion, startingVersion,ordinal) ? startingVersion[ordinal] : 0;            
            //int ordinalValue = 0;

            // This should be whatever the original ordinal was +3 or 4...
            int maximumCheck = 9;

            while (!found)
            {
                string newUrl = url;
                currentVersion[ordinal] = ordinalValue;
                newUrl = FillInUrl(url, currentVersion);

                if (CheckExistence(newUrl))
                {
                    // TODO this isn't necessary. Just store a bool or something that we found a good version.
                    // Save the good version
                    int[] array = new int[versions.Count];
                    versions.CopyTo(array);
                    foundVersion = new List<int>(array);
                }
                else
                {
                    // If we failed to find one, and we've already found a good one,
                    // then either keep searching or finish.
                    if (foundVersion != null)
                    {
                        // If we're the last ordinal in the string, we're done.
                        // Otherwise, descend into the next ordinal
                        if (ordinal == versions.Count - 1)
                            found = true;
                        else
                        {
                            // The _last_ version worked, so restore "versions" to the last version
                            currentVersion[ordinal] = ordinalValue - 1;
                            return CheckVersion(url, currentVersion, ordinal + 1);
                        }
                    }
                    else
                    {
                        // If we've searched past what was given to us, e.g. when we're at 4.0.0
                        // when the original url was 3.1.5, then just default to 3 and move on.
                        if (SameVersionSoFar(currentVersion,startingVersion,ordinal) &&
                            currentVersion[ordinal]>startingVersion[ordinal]){
                            currentVersion[ordinal]=startingVersion[ordinal];
                            return CheckVersion(url, currentVersion, ordinal + 1);
                        }
                    }
                }
                ordinalValue++;
                if (ordinalValue > maximumCheck)
                    break;
            }
            return foundVersion;

        }

        /// <summary>
        /// Checks to make sure a URL doesn't return 404.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool CheckExistence(string url)
        {
            WebRequest request = HttpWebRequest.Create(new Uri(url));
            bool result = true;
            // 3 second timeout
            request.Timeout = 3000;
            WebResponse response=null;
            try
            {
                response= request.GetResponse();
                
                // If we got an HTML redirect, that means we didn't get a binary from the server.
                if (response.ContentType == "text/html")
                    result=false;

            }
            catch (WebException)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return result;
        }

    }
}
