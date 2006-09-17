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
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CodeProject.Downloader;
using InstallPad.Properties;

namespace InstallPad
{
    /// <summary>
    /// A list item to be displayed in a ControList, one for each application.
    /// Each app can be in one of the following states:
    /// downloading, downloaded, installing, installed, or none of those.
    /// </summary>
    public partial class ApplicationListItem : UserControl
    {
        private static int labelDistanceFromRight = 5;

        private ApplicationItem application;
        FileDownloader downloader;

        // Custom controls
        ErrorBox downloadErrorBox;
        ErrorBox installErrorBox;

        // When we run an installer, this is a handle to its process.
        Process installProcess;

        // Listens for the "FinishedDownloadingAfterLinkClicked" event.
        EventHandler LinkClickedDownloadHandler;

        private bool downloadComplete = false;
        private bool downloading = false;

        #region Properties



        /// <summary>
        /// True if the application list item's checkbox is checked.
        /// </summary>
        public bool Checked
        {
            get { return this.checkboxEnabled.Checked; }
        }

        #endregion



        
        public ApplicationListItem()
        {
            InitializeComponent();
        }

        public ApplicationItem ApplicationItem
        {
            get { return application; }
            set
            {
                application = value;
                this.labelName.Text = this.application.Name;
            }
        }
        /// <summary>
        /// Initialize this with the information from an ApplicationItem
        /// </summary>
        /// <param name="application"></param>
        public ApplicationListItem(ApplicationItem application)
            : this()
        {
            this.ApplicationItem = application;
            this.LinkClickedDownloadHandler = new EventHandler(this.FinishedDownloadingAfterLinkClicked);


            this.Controls.Add(downloadErrorBox);
            AddErrorBoxes();


            downloader = new FileDownloader();
            ProxyOptions options = InstallPadApp.AppList.InstallationOptions.ProxyOptions;
            if (options != null)
                downloader.Proxy = options.ProxyFromOptions();

            downloader.ProgressChanged += new DownloadProgressHandler(downloader_ProgressChanged);
            downloader.DownloadComplete += new EventHandler(downloader_DownloadComplete);
        }

        /// <summary>
        /// Add error boxes to the application list item, with the appropriate captions.
        /// </summary>
        private void AddErrorBoxes()
        {
            // Add a custom error control to our list item
            downloadErrorBox = new ErrorBox();
            installErrorBox = new ErrorBox();
            downloadErrorBox.ErrorCaption = Resources.ErrorDownloading;
            downloadErrorBox.DetailsCaption = Resources.ViewDetails;
            downloadErrorBox.DetailsOnSameLine = true;

            installErrorBox.ErrorCaption = Resources.ErrorInstalling;
            installErrorBox.DetailsCaption = Resources.ViewDetails;
            installErrorBox.DetailsOnSameLine = true;

            this.Controls.Add(downloadErrorBox);
            downloadErrorBox.Location = new Point(this.Left, this.Bottom - downloadErrorBox.Height - 2);
            downloadErrorBox.Hide();

            this.Controls.Add(installErrorBox);
            installErrorBox.Location = new Point(this.Left, this.Bottom - installErrorBox.Height - 2);
            installErrorBox.Hide();
        }



       

        private void installLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (this.installLink.Text == "Install")
            {
                Download();
            }
            else if (this.installLink.Text == "Cancel")
            {
                this.FinishedDownloading -= LinkClickedDownloadHandler;

                // They've pressed cancel, so change "cancel" to "install"
                if (this.downloading)
                {
                    this.downloader.Cancel();
                    this.downloading = false;
                    this.progressBar.Hide();
                    this.labelProgress.Hide();
                }
                else if (installProcess != null)
                {
                    try
                    {
                        // The installer is running. Close the installer.
                        installProcess.Kill();
                    }
                    catch (InvalidOperationException)
                    {
                        // If the process has already exited by this time, we might get an exception
                        // trying to kill it. Ignore
                    }
                }
                SetInstalLinkText("Install");
            }
        }


        

       

        private void SetInstalLinkText(string s)
        {
            this.Invoke(new EventHandler(delegate
            {
                this.installLink.Text = s;
                MoveControlToTheLeftOf(installLink, this.Right);
            }));
        }


       


        /// <summary>
        /// Sets the text of a label that's right aligned to the control; the control
        /// is then repositioned so it's a constant width from the right side of the
        /// control, even after the text changes.
        /// </summary>
        private void MoveControlToTheLeftOf(Control control, int point)
        {
            control.Left = point - control.Width - labelDistanceFromRight;
        }

        private void UpdateProgressLabel(DownloadEventArgs e)
        {
            // If the total file size is 0, then the downloader doesn't know its progress. Might as well not show it
            if (e.TotalFileSize == 0 && e.CurrentFileSize == 0)
                this.labelProgress.Visible = false;
            else
            {
                this.labelProgress.Visible = true;
                this.labelProgress.Text = DownloadProgressString.ProgressString(e.CurrentFileSize, e.TotalFileSize);
                MoveControlToTheLeftOf(labelProgress, this.progressBar.Left);
            }
        }

        private void checkboxEnabled_CheckedChanged(object sender, EventArgs e)
        {
        }

    }

    /// <summary>
    /// Utility methods to process and format a progress string
    /// given current and total bytes in a download. Used for UI display
    /// </summary>
    class DownloadProgressString
    {
        // Logic is, if total is <1MB, always display (1 of 5K)
        // If total is >1MB, always display (1 of 5MB)
        public static string ProgressString(long current, long total)//, long totalBytes)
        {
            long currentk = InKilobytes(current);
            long totalk = InKilobytes(total);

            // If we don't know the total, then just return the current
            if (totalk <= 0)
            {
                if (currentk <= 0)
                    return "0K";
                else if (currentk < 1000)
                    return currentk + "K";
                else
                    return InMegabytes(current).ToString("N1") + "MB";
            }

            if (totalk < 1000)
                return String.Format("{0} of {1} KB", currentk, totalk);
            else
                return String.Format("{0} of {1} MB", MegabyteString(InMegabytes(current)),
                    MegabyteString(InMegabytes(total)));
        }
        private static string MegabyteString(double mb)
        {
            // If MB are > 9, leave off the decimal place. So 9.5 MB, but 19MB.
            if (mb < 10)
                return mb.ToString("N1");
            else
                return mb.ToString("N0");
        }
        private static double InMegabytes(long bytes)
        {
            long k = InKilobytes(bytes);
            return ((double)k) / 1000;
        }
        private static long InKilobytes(long bytes)
        {
            return (bytes < 1000) ? 0 : bytes / 1000;
        }
    }
}
