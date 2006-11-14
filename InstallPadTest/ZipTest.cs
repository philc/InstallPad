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
using System.Diagnostics;

using NUnit.Framework;

namespace InstallPadTest
{
    [TestFixture]
    public class ZipTestFixture : BaseTestFixture
    {
        #region Test UnZip
        [Test]
        public void TestUnZip()
        {
            string target = "test4.zip";

            string fileUrl = Path.Combine(DATA_DIRECTORY, target);

            try
            {
                InstallPad.Zip.Instance.ExtractZip(fileUrl, OUTPUT_DIRECTORY);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (string f in Directory.GetFiles(OUTPUT_DIRECTORY))
            {
                FileInfo fi = new FileInfo(f);
                FileInfo fr = new FileInfo(Path.Combine(DATA_DIRECTORY, fi.Name));

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(f, fr.Length));
            }
        }
        #endregion

        #region Test Zip Contains
        [Test]
        public void TestZipContains()
        {
            string fileUrl0 = Path.Combine(DATA_DIRECTORY, "test4.zip");
            string fileUrl1 = Path.Combine(DATA_DIRECTORY, "test5.zip");

            Assert.IsTrue(InstallPad.Zip.Instance.Contains(fileUrl0, "test2.txt") == true);
            Assert.IsTrue(InstallPad.Zip.Instance.Contains(fileUrl0, "test3.txt") == true);
            Assert.IsTrue(InstallPad.Zip.Instance.Contains(fileUrl0, "test4.txt") == false);
            Assert.IsTrue(InstallPad.Zip.Instance.Contains(fileUrl1, "test5") == true);
            Assert.IsTrue(InstallPad.Zip.Instance.Contains(fileUrl1, "test2.txt") == true);
            Assert.IsTrue(InstallPad.Zip.Instance.Contains(fileUrl1, "test3.txt") == true);
            Assert.IsTrue(InstallPad.Zip.Instance.Contains(fileUrl1, "test4.txt") == false);
        }
        #endregion

        #region Test Zip Has Root Folder
        [Test]
        public void TestZipHasRootFolder()
        {
            string fileUrl0 = Path.Combine(DATA_DIRECTORY, "test4.zip");
            string fileUrl1 = Path.Combine(DATA_DIRECTORY, "test5.zip");

            Assert.IsTrue(InstallPad.Zip.Instance.HasRootFolder(fileUrl0) == false);
            Assert.IsTrue(InstallPad.Zip.Instance.HasRootFolder(fileUrl1) == true);
        }
        #endregion
    }
}
