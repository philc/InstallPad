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
using NUnit.Framework;
using CodeProject.Downloader;
using System.Diagnostics;
namespace InstallPadTest

{
    [TestFixture]
    public class DownloaderTest
    {
        Dictionary<string, long> fileSizes = new Dictionary<string, long>();
        List<string> badUrls = new List<string>();

        // Used for loading test files from a windows share
        private static readonly string sambaPath = "//kagero/incoming/test/";

        private static readonly string firefoxFtpUrl = 
            "ftp://ftp.mozilla.org/pub/mozilla.org/firefox/releases/1.5/win32/en-US/Firefox%20Setup%201.5.exe";

        private string NonEmptyFile;
        private string malformedUrl = "!@#$%badUrl";

        public DownloaderTest()
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
        [SetUp]
        public void SetUp(){                        
        }

        [Test]
        public void DownloadHttp()
        {
            FileDownloader downloader = new FileDownloader();
            foreach (String s in fileSizes.Keys)
            {
                downloader.Download(InstallPadTest.GetDownloadPath(s));

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(s, fileSizes[s]));
            }           
        }
        [Test]
        public void DownloadFtp()
        {
            FileInfo info;
            
            // We expect no exceptions
            downloadFtpTestDownloader = new FileDownloader();
            downloadFtpTestDownloader.ProgressChanged += new DownloadProgressHandler(DownloadFtp_ProgressChanged);
            downloadFtpTestDownloader.Download(firefoxFtpUrl);

            // Ensure the file size is 1KB, meaning we were notified of progress information
            // and Cancel worked.
            info = new FileInfo(Path.GetFileName(new Uri(firefoxFtpUrl).ToString()));
            Assert.AreEqual(info.Length, 1024);

            // Trying (and failing) to resume an ftp source should result
            // in the file getting deleted and starting over at 0 KB.
            downloadFtpTestDownloader.Download(firefoxFtpUrl);
            info = new FileInfo(Path.GetFileName(new Uri(firefoxFtpUrl).ToString()));
            Assert.AreEqual(info.Length, 1024);            
        }

        [Test]
        public void DownloadFile()
        {
            FileDownloader downloader = new FileDownloader();
            string dataDirectory = "../../data/";
            foreach (String s in fileSizes.Keys)
            {
                string fileUrl = "file:///" + Path.GetFullPath(dataDirectory + s);
                downloader.Download(fileUrl);

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(s, fileSizes[s]));
            }
            // Download them from a windows share
            foreach (String s in fileSizes.Keys)
            {
                string fileUrl = "file:///" + sambaPath + s;
                downloader.Download(fileUrl);
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(s, fileSizes[s]));
            }
        }

        [Test]
        public void DownloadFileWithAlternate()
        {
            FileDownloader downloader = new FileDownloader();
            string dataDirectory = "../../data/";
            foreach (String s in fileSizes.Keys)
            {
                string fileUrl = "file:///" + Path.GetFullPath(dataDirectory + s);

                List<string> urlList = new List<string>();

                urlList.Add("file://somebadpath"+s);
                urlList.Add(fileUrl);
                urlList.Add("file://somebadpath"+s);

                downloader.Download(urlList);

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(s, fileSizes[s]));
            }
            // Download them from a windows share
            foreach (String s in fileSizes.Keys)
            {
                string fileUrl = "file:///" + sambaPath + s;

                List<string> urlList = new List<string>();

                urlList.Add("file://somebadpath"+s);
                urlList.Add(fileUrl);
                urlList.Add("file://somebadpath"+s);

                downloader.Download(urlList);

                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(s, fileSizes[s]));
            }
        }

        FileDownloader downloadFtpTestDownloader;

        void DownloadFtp_ProgressChanged(object sender, DownloadEventArgs e)
        {
            downloadFtpTestDownloader.Cancel();
        }

        [Test]
        public void Resume()
        {
            // Create a file, fill with some data, then try and resume.
            File.Delete(NonEmptyFile);
            StreamWriter w = File.CreateText(NonEmptyFile);
            w.Write("222222222");
            w.Close();
            FileDownloader downloader = new FileDownloader();
            downloader.Download(InstallPadTest.GetDownloadPath(NonEmptyFile));
            Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(NonEmptyFile, fileSizes[NonEmptyFile]));
            
            // Verify that the first 9 bytes of the file are 2's
            TextReader reader = new StreamReader(NonEmptyFile);
            int charactersRead=0;
            while (charactersRead<9){
                char c = (char)reader.Read() ;
                Assert.IsTrue(c == '2');
                charactersRead++;
            }
            reader.Close();

        }

        [Test]
        public void BadUrls()
        {
            FileDownloader downloader = new FileDownloader();

            foreach (String badUrl in badUrls)
            {
                System.Net.WebException webException = null;
                try
                {
                    downloader.Download(badUrl);
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
        [Test]
        public void MalformedUrl()
        {
            // We expect some kind of descriptive argument exception when we give a malformed url
            FileDownloader downloader = new FileDownloader();
            ArgumentException ex=null;

            try
            {
                downloader.Download(malformedUrl);
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

        [Test]
        public void Cancel()
        {
            cancelTestDownloader = new FileDownloader();
            cancelTestDownloader.ProgressChanged += new DownloadProgressHandler(CancelProgressChanged);
            cancelTestDownloader.Download(InstallPadTest.GetDownloadPath("test3.txt"));
            // Ensure file exists but is not full, since our block size is 1k and we've only downloaded
            // one block by this point.
            Assert.IsTrue(File.Exists("test3.txt"));
            FileInfo info = new FileInfo("test3.txt");
            
            // If block size is 1K, which it is by default, then info.Length should be 1024
            Assert.IsTrue(info.Length != 0);
            Assert.IsTrue(info.Length < fileSizes["test3.txt"]);
        }

        FileDownloader cancelTestDownloader;

        void CancelProgressChanged(object sender, DownloadEventArgs e)
        {
            cancelTestDownloader.Cancel();
        }

        [TearDown]
        public void TearDown()
        {
            // Delete all files that we may have downloaded
            foreach (String s in fileSizes.Keys)
                File.Delete(s);
        }

    }
}
