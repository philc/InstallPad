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

using NUnit.Framework;

namespace InstallPadTest
{
    public class BaseTestFixture
    {
        protected readonly string DATA_DIRECTORY = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data");
        protected readonly string OUTPUT_DIRECTORY = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "output");

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InstallPadTest.ExtractToFile(DATA_DIRECTORY, "data", ".txt");
            InstallPadTest.ExtractToFile(DATA_DIRECTORY, "data", ".zip");

            Directory.CreateDirectory(OUTPUT_DIRECTORY);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Directory.Delete(DATA_DIRECTORY, true);
            Directory.Delete(OUTPUT_DIRECTORY, true);
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            // clean output directory
            foreach (string f in Directory.GetFiles(OUTPUT_DIRECTORY))
            {
                File.Delete(f);
            }
        }
    }
}
