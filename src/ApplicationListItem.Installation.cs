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
    public partial class ApplicationListItem
    {
        #region Properties
        public bool Downloading
        {
            get { return downloading; }
            set { downloading = value; }
        }

        public bool DownloadComplete
        {
            get { return downloadComplete; }
            set { downloadComplete = value; }
        }

        public bool Installing
        {
            get { return this.labelStatus.Text.Contains("Installing"); }
        }
        private bool installed = false;

        public bool Installed
        {
            get { return installed; }
        }
        #endregion

        #region Event we publish
        public event EventHandler FinishedDownloading;
        private void OnFinishedDownloading()
        {


            if (this.FinishedDownloading != null)
                FinishedDownloading(this, new EventArgs());
        }
        public event EventHandler FinishedInstalling;
        private void OnFinishedInstalling()
        {
            this.Invoke(new EventHandler(delegate
            {
                if (this.installProcess != null && this.installProcess.ExitCode == 0)
                {
                    this.installed = true;
                    this.labelStatus.Text = "Install finished";
                    RunPostInstallationScript();
                }
                else
                    this.labelStatus.Text = "Downloaded";

                MoveControlToTheLeftOf(labelStatus, this.Right);
                SetInstalLinkText("Install");
            }));

            if (this.FinishedInstalling != null)
                FinishedInstalling(this, new EventArgs());
        }
        #endregion

        private void RunPostInstallationScript()
        {
            if (this.application.Options.PostInstallScript == null)
                return;
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = application.Options.PostInstallScript;
                Process p = Process.Start(info);
            }
            catch (Exception e)
            {
                this.installErrorBox.DetailsText =
                    String.Format("Error running the post install script {0} : {1}",
                        application.Options.PostInstallScript, e.Message);
                this.installErrorBox.Show();
            }
        }
        void downloader_DownloadComplete(object sender, EventArgs e)
        {
            // Update our appearance, let others know we're finished downloading.
            Debug.WriteLine("download complete.");
            if (!this.downloading)
                return;

            this.downloading = false;
            this.downloadComplete = true;

            this.Invoke(new EventHandler(delegate
            {
                this.progressBar.Visible = false;
                this.labelStatus.Visible = true;
                this.labelStatus.Text = "Downloaded";
                MoveControlToTheLeftOf(labelStatus, this.Right);
            }));

            this.OnFinishedDownloading();
        }

        private void FinishedDownloadingAfterLinkClicked(object sender, EventArgs e)
        {
            // If we finished downloading after they explictly clicked on the "Install"
            // link, they we should launch the installer
            this.FinishedDownloading -= LinkClickedDownloadHandler;
            InstallApplication();
        }
        public void Download()
        {
            // If we're already downloading this file, ignore this click
            if (this.Downloading)
                return;
            this.downloading = true;

            this.Invoke(new EventHandler(delegate
            {
                // Hide any previous errors we might have had
                this.downloadErrorBox.Visible = false;
                this.installErrorBox.Visible = false;

                // Update the progress bar with our progress
                this.progressBar.Visible = true;
            }));

            // Change "install" to "cancel"
            SetInstalLinkText("Cancel");

            this.FinishedDownloading += LinkClickedDownloadHandler;

            ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncDownload), null);
        }

        /// <summary>
        /// Begins an async download
        /// </summary>
        /// <param name="data"></param>
        private void AsyncDownload(object data)
        {
            Exception ex = null;
            try
            {
                String downloadUrl = this.application.FindLatestUrl();
                downloader.Download(downloadUrl, InstallPadApp.InstallFolder);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                ex = e;
            }
            catch (Exception e)
            {
                ex = e;
            }
            if (ex != null)
            {
                this.Invoke(new EventHandler(delegate
                {
                    // Show an error
                    this.downloadErrorBox.Visible = true;
                    this.downloadErrorBox.DetailsText = ex.Message;
                    this.progressBar.Visible = false;
                    this.labelStatus.Visible = false;
                }));
                this.Downloading = false;
                SetInstalLinkText("Install");
            }
        }

        /// <summary>
        /// Installs the application referred to by this app list item. 
        /// </summary>
        public void InstallApplication()
        {

            this.Invoke(new EventHandler(delegate
            {
                this.labelStatus.Text = "Installing...";
                this.downloadErrorBox.Visible = false;
                this.installErrorBox.Visible = false;
                this.labelProgress.Hide();
            }));

            // TODO: Should check to make sure the app is downloaded first.
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncInstall), null);
        }

        private static string ArgumentsForSilentInstall(ApplicationItem application)
        {
            // Coding in special rules here for apps. Each of these special rules should be moved
            // to another class, or a seperate file. Make sure there is a space before the command line arguments.

            // TODO: externalize this information. Put it in a .ini or .config file accessible by users?
            // Each rule should be on its own line; first, a regex to match the application name, and next to it,
            // the installer arguments that pertain to that app

            if (application.FileName.ToLower().Contains("firefox"))
                return "-ms";
            else if (application.FileName.ToLower().Contains("itunessetup"))
            {
                // Args are: /s /v"SILENT_INSTALL=1 ALLUSERS=1 /qb"
                return "/s /v\"SILENT_INSTALL=1 ALLUSERS=1 /qb\"";
            }
            else if (application.FileName.ToLower().Contains("spybot"))
            {
                return "/VERYSILENT";
            }
            else if (application.FileName.ToLower().Contains("adberdr"))
            {
                // Adobe Acrobat Reader. Argh adobe...
                return "/S /w /v\"/qb-! /norestart EULA_ACCEPT=YES\"";
            }
            else
                // This will work for most installers - /S for nullsoft installers, and -s for InstallShield
                return "/S -s";
        
        }

        /// <summary>
        /// Begin an async install
        /// </summary>
        private void AsyncInstall(object data)
        {
            this.installed = false;

            ProcessStartInfo psi = new ProcessStartInfo(downloader.DownloadingTo);
            psi.Arguments = application.Options.InstallerArguments;
            if (application.Options.SilentInstall || InstallPadApp.AppList.InstallationOptions.SilentInstall)
            {
                psi.Arguments = String.Format("{0} {1}",psi.Arguments,ArgumentsForSilentInstall(application));
            }

            installProcess = new Process();

            installProcess.StartInfo = psi;
            installProcess.EnableRaisingEvents = true;
            installProcess.Exited += new EventHandler(process_Exited);

            Exception ex = null;
            try
            {
                installProcess.Start();
            }
            catch (Exception e)
            {
                ex = e;
            }
            if (ex != null)
            {
                this.installProcess = null;
                this.Invoke(new EventHandler(delegate
                {
                    // Show an error
                    installErrorBox.DetailsText = String.Format("Couldn't install {0} : {1}",
                        downloader.DownloadingTo, ex.Message);
                    installErrorBox.Visible = true;
                }));

                SetInstalLinkText("Install");

                // We're done running the installer..
                OnFinishedInstalling();
            }
        }
        void downloader_ProgressChanged(object sender, CodeProject.Downloader.DownloadEventArgs e)
        {
            // Sometimes we may get events fired even after we're done downloading.
            // Don't react to them.
            if (!this.Downloading)
                return;
            this.Invoke(new EventHandler(delegate
            {
                if (e.PercentDone <= 100)
                {
                    this.progressBar.Value = e.PercentDone;
                    UpdateProgressLabel(e);
                }
            }));

        }


        void process_Exited(object sender, EventArgs e)
        {
            OnFinishedInstalling();
        }

    }
}
