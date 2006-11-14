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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeProject.AboutDialog
{
    public partial class LicenseDialog : Form
    {
        private string license = null;

        public string License
        {
            get { return license; }
            set { license = value; }
        }

        public LicenseDialog()
        {
            InitializeComponent();
        }

        private void LicenseDialog_Load(object sender, EventArgs e)
        {
            licenseText.Text = license;
        }
    }
}