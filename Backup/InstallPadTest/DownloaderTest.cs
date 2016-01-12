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
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;

using NUnit.Framework;

using CodeProject.Downloader;

namespace InstallPadTest
{
    [TestFixture]
    public class DownloaderTestFixture : BaseTestFixture
    {
        #region Fields and Ctor
        private List<string> badUrls = new List<string>();
        private Dictionary<string, long> fileSizes = new Dictionary<string, long>();

        // Used for loading test files from a windows share.
        private static readonly string sambaPath = ConfigurationManager.AppSettings["sambaPath"];

        private static readonly string firefoxFtpUrl = "ftp://ftp.mozilla.org/pub/mozilla.org/firefox/releases/1.5/win32/en-US/Firefox%20Setup%201.5.exe";

        private string NonEmptyFile = string.Empty;
        private string malformedUrl = "!@#$%badUrl";

        private FileDownloader cancelTestDownloader;
        private FileDownloader downloadFtpTestDownloader;

        public DownloaderTestFixture()
        {
            fileSizes.Add("test1.txt", 0);
            fileSizes.Add("test2.txt", 98);
            fileSizes.Add("test3.txt", 1747);

            NonEmptyFile = "test2.txt";

            badUrls.Add("http://nonexistanthost123456.com/file");
            badUrls.Add("http://www.google.com/i-dont-exist");
            badUrls.Add("ftp://ftp.mozilla.org/pub/i-dont-exist");
            badUrls.Add("file://c:/doesNotExist.txt");
        }
        #endregion

        #region Test Download HTTP
        [Test]
        public void DownloadHttp()
        {
            FileDownloader downloader = new FileDownloader();
            foreach (String s in fileSizes.Keys)
            {
                downloader.Download(InstallPadTest.GetDownloadPath(s), OUTPUT_DIRECTORY);

                string fi = Path.Combine(OUTPUT_DIRECTORY, s);

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(fi, fileSizes[s]));
            }
        }
        #endregion

        #region Test Download FTP
        [Test]
        public void DownloadFtp()
        {
            FileInfo info;
            
            // We expect no exceptions
            downloadFtpTestDownloader = new FileDownloader();
            downloadFtpTestDownloader.ProgressChanged += new DownloadProgressHandler(DownloadFtp_ProgressChanged);
            downloadFtpTestDownloader.Download(firefoxFtpUrl, OUTPUT_DIRECTORY);

            string fi = Path.Combine(OUTPUT_DIRECTORY, Path.GetFileName(new Uri(firefoxFtpUrl).ToString()));

            // Ensure the file size is 1KB, meaning we were notified of progress information
            // and Cancel worked.
            info = new FileInfo(fi);
            Assert.AreEqual(info.Length, 1024);

            // Trying (and failing) to resume an ftp source should result
            // in the file getting deleted and starting over at 0 KB.
            downloadFtpTestDownloader.Download(firefoxFtpUrl, OUTPUT_DIRECTORY);
            info = new FileInfo(fi);
            Assert.AreEqual(info.Length, 1024);
        }
        #endregion

        #region Test Download File
        [Test]
        public void DownloadFile()
        {
            FileDownloader downloader = new FileDownloader();
            foreach (String s in fileSizes.Keys)
            {
                string fileUrl = "file:///" + Path.Combine(DATA_DIRECTORY, s);

                downloader.Download(fileUrl, OUTPUT_DIRECTORY);

                string fi = Path.Combine(OUTPUT_DIRECTORY, s);

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(fi, fileSizes[s]));
            }
        }
        #endregion

        #region Test Download File From Windows Share
        [Test]
        public void DownloadFileFromWindowsShare()
        {
            FileDownloader downloader = new FileDownloader();

            // Download them from a windows share.  We could get a C# MapDrive() function to allow this portion of the
            // test not to rely on a hardcoded share path.
            if (Directory.Exists(sambaPath))
            {
                foreach (String s in fileSizes.Keys)
                {
                    string fileUrl = "file:///" + sambaPath + s;
                    downloader.Download(fileUrl, OUTPUT_DIRECTORY);
                    string fi = Path.Combine(OUTPUT_DIRECTORY, s);
                    Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(fi, fileSizes[s]));
                }
            }
            else
            {
                Assert.Ignore("Test ignored because share is not mapped or may not exist.");
            }
        }
        #endregion

        #region Test Download Alternate Location
        [Test]
        public void DownloadFileWithAlternate()
        {
            // These download URLs need to be changed
            FileDownloader downloader = new FileDownloader();
            foreach (String s in fileSizes.Keys)
            {
                string fileUrl = "file:///" + Path.Combine(DATA_DIRECTORY, s);

                List<string> urlList = new List<string>();

                urlList.Add("file://somebadpath"+s);
                urlList.Add(fileUrl);
                urlList.Add("file://somebadpath"+s);

                downloader.Download(urlList, OUTPUT_DIRECTORY);

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(Path.Combine(OUTPUT_DIRECTORY, s), fileSizes[s]));
            }
        }
        #endregion

        #region Test Download Alternate Location From Windows Share
        [Test]
        public void DownloadFileWithAlternateFromWindowsShare()
        {
            // These download URLs need to be changed
            FileDownloader downloader = new FileDownloader();

            // Download them from a windows share.  We could get a C# MapDrive() function to allow this portion of the
            // test not to rely on a hardcoded SAMBA path.
            if (Directory.Exists(sambaPath))
            {
                foreach (String s in fileSizes.Keys)
                {
                    string fileUrl = "file:///" + sambaPath + s;

                    List<string> urlList = new List<string>();

                    urlList.Add("file://somebadpath" + s);
                    urlList.Add(fileUrl);
                    urlList.Add("file://somebadpath" + s);

                    downloader.Download(urlList, OUTPUT_DIRECTORY);

                    string fi = Path.Combine(OUTPUT_DIRECTORY, s);

                    Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(fi, fileSizes[s]));
                }
            }
            else
            {
                Assert.Ignore("Test ignored because share is not mapped or may not exist.");
            }
        }
        #endregion

        #region Test Download Bad Urls
        [Test]
        public void BadUrls()
        {
            FileDownloader downloader = new FileDownloader();

            foreach (String badUrl in badUrls)
            {
                System.Net.WebException webException = null;
                try
                {
                    downloader.Download(badUrl, OUTPUT_DIRECTORY);
                }
                catch (System.Net.WebException e)
                {
                    webException = e;
                }

                finally
                {
                    Assert.IsNotNull(webException);
                }
            }
        }
        #endregion

        #region Test Download Malformed Urls
        [Test]
        public void MalformedUrl()
        {
            // We expect some kind of descriptive argument exception when we give a malformed url
            FileDownloader downloader = new FileDownloader();
            ArgumentException ex=null;

            try
            {
                downloader.Download(malformedUrl, OUTPUT_DIRECTORY);
            }
            catch (ArgumentException e)
            {
                ex = e;
            }
            finally
            {
                Assert.IsNotNull(ex);
            }

        }
        #endregion

        #region Test Download Cancel
        [Test]
        public void Cancel()
        {
            cancelTestDownloader = new FileDownloader();

            cancelTestDownloader.ProgressChanged += new DownloadProgressHandler(Cancel_ProgressChanged);
            cancelTestDownloader.Download(InstallPadTest.GetDownloadPath("test3.txt"), OUTPUT_DIRECTORY);

            string fi = Path.Combine(OUTPUT_DIRECTORY, "test3.txt");

            // Ensure file exists but is not full, since our block size is 1k and we've only downloaded
            // one block by this point.
            Assert.IsTrue(File.Exists(fi));

            FileInfo info = new FileInfo(fi);
            
            // If block size is 1K, which it is by default, then info.Length should be 1024
            Assert.IsTrue(info.Length != 0);
            Assert.IsTrue(info.Length < fileSizes["test3.txt"]);
        }
        #endregion

        #region Helper Methods
        private void Cancel_ProgressChanged(object sender, DownloadEventArgs e)
        {
            cancelTestDownloader.Cancel();
        }

        private void DownloadFtp_ProgressChanged(object sender, DownloadEventArgs e)
        {
            downloadFtpTestDownloader.Cancel();
        }
        #endregion
    }
}
