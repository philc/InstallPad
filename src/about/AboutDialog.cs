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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeProject.AboutDialog
{
    public partial class AboutDialog : Form
    {
        static int padding = 6;

        CreditsDialog creditsDialog = null;
        LicenseDialog licenseDialog = null;
        FeedbackDialog feedbackDialog = null;
        
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

        // Expose properties from the credits dialog
        public string[] WrittenBy
        {
            get{ return creditsDialog.WrittenBy; }
            set{ creditsDialog.WrittenBy = value; }
        }
        public string[] TranslatedBy
        {
            get { return creditsDialog.TranslatedBy; }
            set { creditsDialog.TranslatedBy = value; }
        }
        public string[] ArtworkBy
        {
            get { return creditsDialog.ArtworkBy; }
            set { creditsDialog.ArtworkBy = value; }
        }

        public AboutDialog()
        {
            InitializeComponent();
            creditsDialog = new CreditsDialog();
            licenseDialog = new LicenseDialog();
            feedbackDialog = new FeedbackDialog();
        }

        private void creditsButton_Click(object sender, EventArgs e)
        {
            creditsDialog.ShowDialog();
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
                copyrightLabel.Text = "Copyright © " + copyright;
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

            if (WrittenBy==null && ArtworkBy == null && TranslatedBy == null)
                this.creditsButton.Visible = false;

            feedbackDialog.To = "support@installpad.com";

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
            // Find the widest visible control That should be the dialog's width
            foreach (Control c in this.Controls)
            {
                if (c.Visible && (c.Width + 4 * padding) > this.Width)
                    this.Width = c.Width + 4 * padding;
            }

            if (pictureBox.Visible)
            {
                dialogHeight = pictureBox.Bottom + padding;
                CenterHorizontally(pictureBox);
                // Very strange, the picture box is always to the right by 2-3 pixels...
                pictureBox.Left -= 3;                
            }

            Control[] controls = new Control[] { nameAndVersion, descriptionLabel, copyrightLabel, projectLink };
            dialogHeight = LayoutControls(controls, dialogHeight);
            
            this.Height = dialogHeight + 70;   // pixels to pad for the dialog's buttons and whitespace

        }

        private static int LayoutControls(Control[] controls, int dialogHeight)
        {            
            foreach (Control c in controls)
            {
                if (!c.Visible)
                    continue;
                c.Top=dialogHeight;
                dialogHeight=c.Bottom + padding + c.Margin.Bottom;
                CenterHorizontally(c);
            }
            return dialogHeight;
        }

        private static void CenterHorizontally(Control c)
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

        private void feedbackButton_Click(object sender, EventArgs e)
        {
            this.feedbackDialog.ShowDialog();
        }
    }
}