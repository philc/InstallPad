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
using System.Diagnostics;

namespace InstallPadTest
{
    [TestFixture]
    public class ZipTest
    {
        Dictionary<string, long> fileSizes = new Dictionary<string, long>();

        public ZipTest()
        {
           // fileSizes.Add("test1.txt", 0);
            fileSizes.Add("test2.txt", 98);
            fileSizes.Add("test3.txt", 1747);
        }

        [SetUp]
        public void SetUp()
        {                        
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void UnZip1()
        {
            string target = "test4.zip";
            string dataDirectory = "../../data/";
            string outputDirectory = "../../data/out/";

            string fileUrl = Path.GetFullPath(dataDirectory + target);
            string destUrl = Path.GetFullPath(outputDirectory);

            try
            {
                InstallPad.Zip.Instance.ExtractZip(fileUrl, destUrl);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (String s in fileSizes.Keys)
            {
                fileUrl = "file:///" + Path.GetFullPath(outputDirectory + s);

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(s, fileSizes[s]));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void UnZip2()
        {
            string target = "test5.zip";
            string dataDirectory = "../../data/";
            string outputDirectory = "../../data/out2/";

            string fileUrl = Path.GetFullPath(dataDirectory + target);
            string destUrl = Path.GetFullPath(outputDirectory);

            try
            {
                InstallPad.Zip.Instance.ExtractZip(fileUrl, destUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (String s in fileSizes.Keys)
            {
                fileUrl = "file:///" + Path.GetFullPath(outputDirectory + @"test\" + s);

                // Veryify file exists and is the correct size
                Assert.IsTrue(InstallPadTest.VerifyExistenceAndSize(s, fileSizes[s]));
            }
        }

        [TearDown]
        public void TearDown()
        {
        }

    }
}
