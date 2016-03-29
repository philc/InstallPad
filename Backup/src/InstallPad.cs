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
using System.Xml;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using CodeProject.Downloader;
using CodeProject.AboutDialog;
using System.IO;
using InstallPad.Properties;
using InstallPad.UI;
namespace InstallPad
{
    /// <summary>
    /// Main InstallPad GUI.
    /// </summary>
    public partial class InstallPad : Form
    {
        private ControlList controlList;

        // Initialize and use if we encounter errors while loading the applist file.
        private AppListErrorDialog errorDialog;

        // Show error in the center of the application if we can't find an applist.xml
        private AppListErrorBox appListErrorBox;

        OpenFileDialog openDialog = new OpenFileDialog();

        // When the user clicks "Install All," every time something finishes downloading or
        // installing, we should begin downloading/installing the next enabled app on the list.
        private bool installingAll = false;

        // This is the list of items we've tried to instal in the current run. If one fails,
        // we can check this collection to make sure we don't try to install again.
        private List<ApplicationListItem> triedToInstall = new List<ApplicationListItem>();
        // For now, only install one app at a time.
        private int currentlyInstalling = 0;

        PreferencesDialog preferencesDialog;

        private Panel controlListPanel = null;

        public InstallPad()
        {
            InitializeComponent();
            this.controlListPanel = splitContainer.Panel1;
        }

        /// <summary>
        /// Load the application config file and restore form position
        /// </summary>
        private void LoadConfigFile()
        {
            TextReader reader = null;
            Rectangle formBounds = Rectangle.Empty;
            try
            {
                string configFolder = Path.GetDirectoryName(InstallPadApp.ConfigFilePath);
                if (!Directory.Exists(configFolder))
                    Directory.CreateDirectory(configFolder);

                reader = new StreamReader(InstallPadApp.ConfigFilePath);
                RectangleConverter converter = new RectangleConverter();
                formBounds = (Rectangle)converter.ConvertFromString(reader.ReadLine());
            }
            catch (NotSupportedException)
            {
                // Error in the configure file. Ignore
            }
            catch (IOException)
            {
                // No config file found. Ignore
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            if (formBounds != Rectangle.Empty)
            {
                // If the bounds are outside of the screen's work area, move the form so it's not outside
                // of the work area. This can happen if the user changes their resolution 
                // and we then restore the applicationto its position -- it may be off screen and 
                // then they can't see it or move it.

                // Get the working area of the monitor that contains this rectangle (in case it's a
                // multi-display system
                Rectangle workingArea = Screen.GetWorkingArea(formBounds);
                if (formBounds.Left < workingArea.Left)
                    formBounds.Location = new Point(workingArea.Location.X, formBounds.Location.Y);
                if (formBounds.Top < workingArea.Top)
                    formBounds.Location = new Point(formBounds.Location.X, workingArea.Location.Y);
                if (formBounds.Right > workingArea.Right)
                    formBounds.Location = new Point(formBounds.X - (formBounds.Right - workingArea.Right),
                        formBounds.Location.Y);
                if (formBounds.Bottom > workingArea.Bottom)
                    formBounds.Location = new Point(formBounds.X,
                        formBounds.Y - (formBounds.Bottom - workingArea.Bottom));

                this.Bounds = formBounds;
            }

        }

        private void InstallPad_Load(object sender, EventArgs e)
        {
            
            preferencesDialog = new PreferencesDialog();
            this.logoBox.Click += new EventHandler(logoBox_Click);
            this.errorLink.Click += new EventHandler(errorLink_Click);
            this.KeyUp += new KeyEventHandler(InstallPad_KeyUp);
            this.FormClosing += new FormClosingEventHandler(InstallPad_FormClosing);
            this.controlList = new ControlList();
            this.controlList.ListItemClicked += new MouseEventHandler(controlList_ListItemClicked);
            this.controlList.ListItemDoubleClicked += new MouseEventHandler(controlList_ListItemDoubleClicked);
            
            controlList.Size = this.controlListPanel.Size;
            this.controlListPanel.Controls.Add(controlList);

            controlList.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            this.controlList.Resize += new EventHandler(controlList_Resize);

            BuildContextMenuEntries();

            // Restore form position etc. from the installpad config file
            // Depending upon where we bomb out, this may or may not be recoverable.
            // E.g. if the static initializer for Preferences fails because the user
            // has messed up system environment variables, then we should exit.
            try
            {
                LoadConfigFile();
            }
            catch (Exception ex)
            {
                string message = (ex is TypeInitializationException && 
                    ex.InnerException!=null) ?
                    ex.InnerException.InnerException.Message : ex.Message;
                MessageBox.Show(message);
                System.Windows.Forms.Application.Exit();
                return;
            }
            // Should be externalized
            string errorMessage = Resources.TempFileCreateError;

            // Try and create the temp folder that we'll be downloading to.
            // If we aren't successful, maybe log a warning            
            if (!Directory.Exists(InstallPadApp.Preferences.DownloadTo))
            {
                try
                {
                    Directory.CreateDirectory(InstallPadApp.Preferences.DownloadTo);
                }
                catch (System.IO.IOException)
                {
                    //Debug.WriteLine("Error creating install folder: " + ex);
                    MessageBox.Show(this, errorMessage + InstallPadApp.Preferences.DownloadTo,
                        Resources.InstallFolderCreateError, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    // Should log this TODO
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show(this, errorMessage + InstallPadApp.Preferences.DownloadTo,
                        Resources.Errorcreatinginstallfolder, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            // Error box used to indicate a problem loading the application list
            appListErrorBox = new AppListErrorBox();
            this.controlListPanel.Controls.Add(appListErrorBox);
            appListErrorBox.Visible = false;
            appListErrorBox.openLink.Click += new EventHandler(openLink_Click);

            BuildSearchBox();


            // Load the application list. If not successful (file not found),
            // all controls will be disabled.
            LoadApplicationList(InstallPadApp.AppListFile);
        }

    

        void BuildSearchBox()
        {
            // Add a close button to the search box; only visible when it's "open"
            ImageButton button = new ImageButton();
            button.Parent = this.searchPanel;
            button.Location=new Point(this.searchPanel.Width-21,8);
            button.Anchor = AnchorStyles.Right | AnchorStyles.Top;

            button.BackgroundImage = global::InstallPad.Properties.Resources.close;
            button.OverImage = global::InstallPad.Properties.Resources.closeOver;
            button.PressedImage = global::InstallPad.Properties.Resources.closePressed;
            button.Visible = false;

            this.searchPanel.Controls.Add(button);
            
        }

        void controlList_ListItemDoubleClicked(object sender, MouseEventArgs e)
        {
            Control doubleClickedControl = controlList.ControlAtAbsolutePosition(Cursor.Position);
            
            //remove highlight from whatever controler may have been hightlighted.
            controlList.Unhighlight(controlList.HighlightedEntry);
            //highlight control that the user double clicked, so it shows behind the editing dialog.
            controlList.Highlight(doubleClickedControl);

            if (doubleClickedControl != null && doubleClickedControl is ApplicationListItem)
            {
                ApplicationListItem item = (ApplicationListItem)controlList.HighlightedEntry;
                ApplicationDialog dialog = new ApplicationDialog(item.ApplicationItem);
                dialog.Title = Resources.EditApplication;
                dialog.ShowDialog(this);

                if (dialog.Saved)
                {
                    // Update the list item
                    item.ApplicationItem = item.ApplicationItem;
                    SaveApplist();
                }

                //remove highlight from the control once editing is complete.
                controlList.Unhighlight(doubleClickedControl);
            }                        
        }

        #region left clicking

        /// <summary>
        /// When they click on a list item, check it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void controlList_ListItemClicked(object sender, MouseEventArgs e)
        {
            // TODO: I'm (philc) turning this off for now. When you left click on an
            // app item, the toggling is very slow - it can miss clicks
            // which is super annoying. Also, if you click on the label of an app item,
            // the click on that label is not registered as a click on the app
            // item, so you're clicking away for nothing. Until these problems are
            // solved, disable it.

            return;

            // Only interpret left clicks. Right clicks are for opening context menus

            //if (e.Button != MouseButtons.Left)
            //    return;
            //ApplicationListItem item = (ApplicationListItem)sender;
            //item.Checked = !item.Checked;

            //// Highlight the item we clicked on
            //controlList.Unhighlight(controlList.HighlightedEntry);
            //controlList.Highlight((Control)sender);
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            controlList.Unhighlight(controlList.HighlightedEntry);
        }
        void logoBox_Click(object sender, EventArgs e)
        {
            controlList.Unhighlight(controlList.HighlightedEntry);
        }
        #endregion


        /// <summary>
        /// If the "O" key is hit, open the dialog to choose an applist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InstallPad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 'O' && e.Control)
            {
                // Do something
                e.Handled = true;
                ShowAppListOpenDialog();
            }
        }

        /// <summary>
        /// Show an open dialog that lets the user load a new application list.
        /// Loads the application list after a valid selection.
        /// </summary>
        private void ShowAppListOpenDialog()
        {
            // Don't open a new list if something is downloading or installing
            foreach (ApplicationListItem item in this.controlList.ListItems)
            {
                if (item.State == ApplicationListItem.InstallState.Downloading ||
                    item.State == ApplicationListItem.InstallState.Installing)
                {
                    MessageBox.Show(this,
                        Resources.AppLockedShort,
                        Resources.AppLocked, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                appListErrorBox.Hide();

                this.controlList.ClearListItems();

                LoadApplicationList(openDialog.FileName);
            }
        }

        /// <summary>
        /// This link can be clicked only when an application list has failed to load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void openLink_Click(object sender, EventArgs e)
        {
            ShowAppListOpenDialog();
        }
        private void LoadApplicationList(string filename)
        {
            ApplicationList appList;
            SetErrorPanelVisibility(false);
            try
            {
                appList = ApplicationList.FromFile(filename);
                InstallPadApp.AppList = appList;
            }
            catch (FileNotFoundException)
            {
                ShowErrorBox(Resources.AppFileNotFound, null);
                return;
            }
            catch (XmlException ex)
            {
                ShowErrorBox(Resources.AppFileParseError, ex.Message);
                return;
            }

            appList.FileName = filename;

            if (appList.ApplicationItems.Count <= 0)
                SetControlsEnabled(false);
            else
                SetControlsEnabled(true);

            // Show errors, if we had any in loading
            if (appList.XmlErrors.Count > 0)
            {
                errorDialog = new AppListErrorDialog();
                foreach (string error in appList.XmlErrors)
                    errorDialog.ErrorText += error + System.Environment.NewLine;

                // Show the "encountered errors" label
                //this.errorPanel.Show();
                SetErrorPanelVisibility(true);
            }
            List<Control> toAdd = new List<Control>();
            foreach (ApplicationItem item in appList.ApplicationItems)
            {
                ApplicationListItem listItem = CreateApplicationListItem(item);
                toAdd.Add(listItem);
            }
            
            // Add the controls all at once.
            this.controlList.AddAll(toAdd);
        }
        private void SetErrorPanelVisibility(bool visible)
        {
            // When we show this thing, we need to push the control list
            // up out of the way to make room for it.
            if (visible)
            {
                this.errorPanel.Show();
                //this.controlListPanel.Height = errorPanel.Top - this.logoBox.Bottom;
                this.controlList.Height = controlListPanel.Height-this.errorPanel.Height;
            }
            else
            {
                //this.controlList.Bottom = errorPanel.Bottom;
                //this.controlList.Height = controlList.Height - (this.controlList.Bottom - errorPanel.Bottom);
                //this.controlListPanel.Height = this.errorPanel.Bottom - this.logoBox.Bottom; //controlList.Height - (this.controlList.Bottom - errorPanel.Bottom);
                this.controlList.Height = controlListPanel.Height;// -this.errorPanel.Height;
                this.errorPanel.Hide();
            }
        }
        /// <summary>
        /// Creates a list item and listens to its events
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private ApplicationListItem CreateApplicationListItem(ApplicationItem item)
        {
            ApplicationListItem listItem = new ApplicationListItem(item);
            listItem.FinishedDownloading += new EventHandler(HandleFinishedDownloading);
            listItem.FinishedInstalling += new EventHandler(HandleFinishedInstalling);
            listItem.FinishedUnInstalling += new EventHandler(HandleFinishedUnInstalling);
            return listItem;
        }
        /// <summary>
        /// Show an error box in the center of the application list, detailing
        /// that we can't find an applist.xml
        /// </summary>
        void ShowErrorBox(string errorCaption, string details)
        {
            appListErrorBox.errorLabel.Text = errorCaption;

            if (details == null)
                appListErrorBox.DetailsVisible = false;
            else
                appListErrorBox.DetailsText = details;

            UpdateErrorBoxLocation();
            this.appListErrorBox.BringToFront();
            appListErrorBox.Visible = true;

            SetControlsEnabled(false);
            return;
        }

        void InstallPad_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Serialize form position to file, so we can restore it later.
            RectangleConverter convert = new RectangleConverter();
            string formPosition = convert.ConvertToString(this.Bounds);

            TextWriter writer = null;
            try
            {
                string configFolder = Path.GetDirectoryName(InstallPadApp.ConfigFilePath);
                if (!Directory.Exists(configFolder))
                    Directory.CreateDirectory(configFolder);
                writer = new StreamWriter(InstallPadApp.ConfigFilePath);
                writer.Write(formPosition);
            }
            // We can get here if InstallPad.form_load had a problem,
            // so just eat exceptions rather than throwing more of them.
            catch (Exception ex) { }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Needed when the interface has no items to use.
        /// </summary>
        private void SetControlsEnabled(bool enabled)
        {
            this.buttonInstall.Enabled = enabled;

            if (enabled)
                this.controlList.ContextMenu = this.menu;
            else
                this.controlList.ContextMenu = null;
        }

        void controlList_Resize(object sender, EventArgs e)
        {
            if (appListErrorBox != null && appListErrorBox.Visible)
                UpdateErrorBoxLocation();
        }
        /// <summary>
        /// Moves the error box to the center of the application. Should be used
        /// whenever the error box is visible and the application gets resized
        /// </summary>
        void UpdateErrorBoxLocation()
        {
            int x = (this.controlList.Width - this.appListErrorBox.Width) / 2;
            int y = (this.controlList.Height - this.appListErrorBox.Height) / 2;

            this.appListErrorBox.Location = new Point(x, y);
        }

        void HandleFinishedInstalling(object sender, EventArgs e)
        {
            currentlyInstalling--;
            if (this.installingAll)
                InstallNext();
        }

        void HandleFinishedUnInstalling(object sender, EventArgs e)
        {
        }

        void HandleFinishedDownloading(object sender, EventArgs e)
        {
            if (this.installingAll)
            {
                DownloadNextOnList();
                InstallNext();
            }
        }

        private void InstallNext()
        {
            // You want to calculate what's downloading and what's installing,
            // instead of keeping counters, because users can click on the
            // individual install links at any time and trigger other things installing/downloading
            int downloading = 0;
            int installing = 0;

            // Kepe track of the first item we found that needs to be installed
            ApplicationListItem toInstall = null;

            foreach (ApplicationListItem item in this.controlList.ListItems)
            {
                if (!item.Checked)
                    continue;

                // Avoid trying items again that we've tried to install before
                if (item.State == ApplicationListItem.InstallState.Downloaded && !triedToInstall.Contains(item))
                {
                    if (toInstall == null)
                        toInstall = item;
                }
                else if (item.State == ApplicationListItem.InstallState.Installing)
                    installing++;
                else if (item.State == ApplicationListItem.InstallState.Downloading)
                    downloading++;
            }
            // If there's no installing or downloading happening, then we're done.
            if (installing == 0 && downloading == 0 && toInstall==null)
                this.installingAll = false;
            
            // Only install when no other installers are running - run 1 at a time.
            if (toInstall != null && installing == 0)
            {
                triedToInstall.Add(toInstall);
                toInstall.InstallApplication();
            }
        }


        // TODO this isn't being used right now.. we're not monitoring the installation. AppListItems
        // start installing their exe as soon as it downloads.
        /*private void InstallNextItem()
        {
            if (currentlyInstalling > 0)
                return;

            int currentlyDownloading = 0;

            // Go through all enabled items; if there's something that's downloaded and not installed,
            // install it.
            foreach (ApplicationListItem item in this.controlList.ListItems)
            {
                if (!item.Checked)
                    continue;

                if (item.DownloadComplete)
                {
                    if (!item.Installed && !item.Installing)
                    {
                        currentlyInstalling++;
                        item.InstallApplication();
                        break;
                    }
                }
                else
                    currentlyDownloading++;
            }
            if (currentlyInstalling == 0 && currentlyDownloading == 0)
                // We're done downloading and installing everything.
                this.installingAll = false;
        }*/

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            this.installingAll = true;
            this.triedToInstall.Clear();
            DownloadNextOnList();
        }

        private void DownloadNextOnList()
        {
            int currentlyDownloading = 0;
            foreach (ApplicationListItem item in this.controlList.ListItems)
            {
                if (item.State==ApplicationListItem.InstallState.Downloading)
                {
                    currentlyDownloading++;                    
                }
                else if (item.Checked && item.State == ApplicationListItem.InstallState.None)
                {                    
                    item.Download(false);
                    currentlyDownloading++; 
                }
                // Once we reach the limit on simul downloads, just exit.
                if (currentlyDownloading >= InstallPadApp.AppList.InstallationOptions.SimultaneousDownloads)
                    return;
            }
        }

        private void errorLink_Click(object sender, EventArgs e)
        {
            this.errorDialog.ShowDialog();
        }

        private void openAppList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowAppListOpenDialog();
        }

        private void preferencesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            preferencesDialog.Defaults = InstallPadApp.Preferences.Defaults;
            preferencesDialog.ShowDialog();
        }

        private void aboutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutDialog about = new AboutDialog();
            about.ProjectName = Resources.InstallPad;
            about.ProjectUrl = Resources.InstallPadUrl;
            about.WrittenBy = new string[] { Resources.InstallPadAuthor, Resources.InstallPadContrib1 };
            about.Copyright = Resources.InstallPadCopyRight;
            about.Version = Resources.InstallPadVersion;
            about.Image = global::InstallPad.Properties.Resources.about_logo;
            about.License = global::InstallPad.Properties.Resources.license;
            about.Description = Resources.InstallPadSlogan;
            about.ShowDialog();
        }
    }


}
