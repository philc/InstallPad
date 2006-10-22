using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeProject.AboutDialog
{
    public partial class AboutDialog : Form
    {
        CreditsDialog credits = null;
        LicenseDialog licenseDialog = null;
        private string projectUrl = null;
        private string copyright = null;
        private string projectName = null;//for the title of the dialog

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        private string version;

        public string License
        {
            set
            {
                licenseDialog.License = value;
            }

        }
        public string Copyright
        {
            get { return copyright; }
            set { copyright = value; }
        }

        public string ProjectUrl
        {
            get { return projectUrl; }
            set { projectUrl = value; }
        }

        public AboutDialog()
        {
            InitializeComponent();
            credits = new CreditsDialog();
            licenseDialog = new LicenseDialog();
        }
        public string[] WrittenBy
        {
            set
            {
                credits.WrittenBy = value;
            }
        }

        private void creditsButton_Click(object sender, EventArgs e)
        {
            credits.ShowDialog();
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            
            this.Text = String.Format("{0} {1}", this.Text, projectName);
            // Disable controls if pieces of information are missing
            if (projectUrl == null || projectUrl.Length == 0)
                projectLink.Visible = false;
            if (copyright != null && copyright.Length > 0)
                copyrightLabel.Text = "© " + copyright;
            else
                copyrightLabel.Visible = false;
            if (licenseDialog.License != null && licenseDialog.License.Length>0)
            {
                licenseDialog.Text = String.Format("{0} {1}",projectName,licenseDialog.Text);                
            }else
                this.licenseButton.Visible = false;
        }

        private void installpadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Will launch the default browser
            System.Diagnostics.Process.Start(projectUrl);
        }

        private void licenseButton_Click(object sender, EventArgs e)
        {
            this.licenseDialog.ShowDialog();
        }
    }
}