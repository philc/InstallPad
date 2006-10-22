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