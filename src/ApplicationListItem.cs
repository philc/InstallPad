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
    /// A list item, one for each app. Each app can be in the state of:
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

        // Value of the "enabled" check box
        private bool enabledCheck = true;

        public bool EnabledCheck
        {
            get { return enabledCheck; }
            set
            {
                enabledCheck = value;
                OnEnabledCheckChanged();
            }
        }
        public bool Installing
        {
            get { return this.labelStatus.Text.Contains("Installing"); }
        }


        /// <summary>
        /// True if the application list item's checkbox is checked.
        /// </summary>
        public bool Checked
        {
            get { return this.checkboxEnabled.Checked; }
        }

        private bool installed = false;

        public bool Installed
        {
            get { return installed; }
        }
        #endregion

        #region Events
        public event EventHandler EnabledCheckChanged;
        private void OnEnabledCheckChanged()
        {
            this.enabledCheck = true;
            if (this.EnabledCheckChanged != null)
                EnabledCheckChanged(this, new EventArgs());
        }

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

        private void FinishedDownloadingAfterLinkClicked(object sender, EventArgs e){
            // If we finished downloading after they explictly clicked on the "Install"
            // link, they we should launch the installer
            this.FinishedDownloading -= LinkClickedDownloadHandler;
            InstallApplication();
        }

        private void installLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            if (this.installLink.Text=="Install"){
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
                else if (installProcess!=null)
                {
                    try
                    {
                        // The installer is running. Close the installer.
                        //if (!installProcess.HasExited)
                            installProcess.Kill();
                    }
                    catch (InvalidOperationException)
                    {
                        // If the process has already exited by this time, we might get an exception
                        // trying to kill it
                    }
                }
                SetInstalLinkText("Install");
            }
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

            // TODO: should this link clicked handler be used? It will start the install after the download finishes..
            this.FinishedDownloading += LinkClickedDownloadHandler;
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncDownload), null);           
            
        }
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

        private void SetInstalLinkText(string s)
        {
            this.Invoke(new EventHandler(delegate
            {
                this.installLink.Text = s;
                MoveControlToTheLeftOf(installLink, this.Right);
            }));
        }       


        public void InstallApplication()
        {
            
            this.Invoke(new EventHandler(delegate
            {
                this.labelStatus.Text = "Installing...";
                this.downloadErrorBox.Visible = false;
                this.installErrorBox.Visible = false;
                this.labelProgress.Hide();
            }));

            // Should check to make sure the app is downloaded.
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncInstall), null);
        }

        private void AsyncInstall(object data)
        {
            Debug.WriteLine("Async install.");
            this.installed = false;
            
            ProcessStartInfo psi = new ProcessStartInfo(downloader.DownloadingTo);
            psi.Arguments = application.Options.InstallerArguments;
            if (application.Options.SilentInstall || InstallPadApp.AppList.InstallationOptions.SilentInstall)
            {
                // Coding in special rules here for apps. Each of these special rules should be moved
                // to another class, or a seperate file. Make sure there is a space before the command line arguments.
                // TODO: externalize this information.
                if (application.FileName.ToLower().Contains("firefox"))
                    psi.Arguments=psi.Arguments + " -ms";
                else if (application.FileName.ToLower().Contains("itunessetup")){
                    // Args are: /s /v"SILENT_INSTALL=1 ALLUSERS=1 /qb"
                    psi.Arguments = psi.Arguments + " /s /v\"SILENT_INSTALL=1 ALLUSERS=1 /qb\"";
                }else if (application.FileName.ToLower().Contains("spybot")){
                    psi.Arguments = psi.Arguments + " /VERYSILENT";

                }else if (application.FileName.ToLower().Contains("adberdr")){
                    // Adobe Acrobat Reader. Argh adobe...
                    psi.Arguments = psi.Arguments + 
                        " /S /w /v\"/qb-! /norestart EULA_ACCEPT=YES\"";
                }
                else
                    // This will work for most installers - /S for nullsoft installers, and -s for InstallShield
                    psi.Arguments = psi.Arguments + " /S -s";
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
                    //this.labelStatus.Visible = false;
                }));
                SetInstalLinkText("Install");
                // We're done running the installer..
                OnFinishedInstalling();
            }
            //p.WaitForExit();
        }

        void downloader_ProgressChanged(object sender, DownloadEventArgs e)
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

            //Debug.WriteLine(e.PercentDone);
        }
        /// <summary>
        /// Sets the text of a label that's right aligned to the control; the control
        /// is then repositioned so it's a constant width from the right side of the
        /// control, even after the text changes.
        /// </summary>
        private void MoveControlToTheLeftOf(Control control,int point){
            control.Left = point - control.Width - labelDistanceFromRight;
        }

        private void UpdateProgressLabel(DownloadEventArgs e)
        {           
            // If the total file size is 0, then the downloader doesn't know its progress. Might as well not show it
            if (e.TotalFileSize == 0 && e.CurrentFileSize == 0)
                this.labelProgress.Visible = false;
            else{
                this.labelProgress.Visible = true;
                this.labelProgress.Text = DownloadProgressString.ProgressString(e.CurrentFileSize, e.TotalFileSize);
                MoveControlToTheLeftOf(labelProgress, this.progressBar.Left);
                //this.progressBar);
            }
        }


        void process_Exited(object sender, EventArgs e)
        {            
            OnFinishedInstalling();

        }

        private void checkboxEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

    }

    /// <summary>
    /// This class has utility methods to process and format a progress string
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
                if (currentk <=0)
                    return "0K";
                else if (currentk<1000)
                    return currentk + "K";
                else                
                    return InMegabytes(current).ToString("N1") + "MB";
            }
            
            if (totalk<1000)
                return String.Format("{0} of {1} KB",currentk,totalk);
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
            // TODO: need to reduce the number of decimal digits here. Can be 1.6123123 etc.
            long k = InKilobytes(bytes);
            //return (k < 1000) ? 0 : ((double)k) / 1000;
            return ((double)k) / 1000;
            
        }
        private static long InKilobytes(long bytes)
        {
            return (bytes < 1000) ? 0 : bytes / 1000;
        }
    }
}
