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
        private string description = null;
        private string version = null;

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public Bitmap Image
        {
            set {
                this.pictureBox.Image = value;
                this.pictureBox.Height = value.Height;
                this.pictureBox.Width = value.Width;
            }
        }


        public string Description
        {
            set
            {
                description = value;
            }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

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
            if (NonEmpty(projectUrl))
                projectLink.Text = projectUrl;                
            else
                projectLink.Visible = false;

            if (NonEmpty(projectName))
                this.nameAndVersion.Text = String.Format("{0} {1}", projectName, version);
            else
                nameAndVersion.Visible = false;

            if (NonEmpty(copyright))
                copyrightLabel.Text = "© " + copyright;
            else
                copyrightLabel.Visible = false;

            if (NonEmpty(licenseDialog.License))
            {
                licenseDialog.Text = String.Format("{0} {1}",projectName,licenseDialog.Text);                
            }else
                this.licenseButton.Visible = false;

            if (NonEmpty(description))
                this.descriptionLabel.Text = description;
            else
                descriptionLabel.Visible = false;

            SizeDialog();
        }
        private static bool NonEmpty(String o)
        {
            return o != null && o.Length > 0;
        }
        // Sizes the dialog nicely according to options set
        private void SizeDialog()
        {
            int dialogHeight = pictureBox.Top;
            int padding = 6;
            if (pictureBox.Visible)
            {
                dialogHeight = pictureBox.Bottom + padding;
                this.Width = pictureBox.Width + 4*padding;                
                //this.Width = pictureBox.Width;
                CenterHorizontally(pictureBox);
                // Very strange, the picture box is always to the right by 2-3 pixels...
                pictureBox.Left -= 3;
                
            }

            if (nameAndVersion.Visible)
            {
                nameAndVersion.Top = dialogHeight;
                dialogHeight = nameAndVersion.Bottom + padding;
                CenterHorizontally(nameAndVersion);
            }

            if (descriptionLabel.Visible)
            {
                this.descriptionLabel.Top = dialogHeight;
                dialogHeight = this.descriptionLabel.Bottom + padding;
                CenterHorizontally(descriptionLabel);
            }
            if (this.copyrightLabel.Visible){
                this.copyrightLabel.Top = dialogHeight;
                dialogHeight = this.copyrightLabel.Bottom + padding;
                CenterHorizontally(copyrightLabel);
            }

            if (this.projectLink.Visible)
            {
                this.projectLink.Top = dialogHeight;
                dialogHeight = this.projectLink.Bottom + padding;
                CenterHorizontally(projectLink);
            }
            this.Height = dialogHeight + 70;   // pixels to pad for the buttons and whitespace

        }
        
        private void CenterHorizontally(Control c)
        {
            c.Location = new Point((c.Parent.Width - c.Width) / 2, c.Location.Y);
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