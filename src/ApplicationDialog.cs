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

namespace InstallPad
{
    /// <summary>
    /// Used to add and edit application list items
    /// </summary>
    public partial class ApplicationDialog : Form
    {
        private ApplicationItem applicationItem = null;
        
        // Whether the underlying application item has been saved
        private bool saved;

        /// <summary>
        /// Initialize with an application item
        /// </summary>
        /// <param name="item"></param>
        public ApplicationDialog(ApplicationItem item)
        {
            InitializeComponent();
            this.applicationItem = item;
            this.ApplicationName = item.Name;
            this.DownloadUrl = item.DownloadUrl;
            this.DownloadLatestVersion = item.Options.DownloadLatestVersion;
            this.SilentInstall = item.Options.SilentInstall;
            this.InstallerArguments = item.Options.InstallerArguments;
            this.Comment = item.Comment;
            this.Checked = item.Options.Checked ;
            this.InstallationRoot = item.Options.InstallationRoot;

            // if no app specific installation root, default to applist.installationroot.
            if (this.InstallationRoot==null || this.InstallationRoot.Length == 0)
            {
                this.InstallationRoot = InstallPadApp.AppList.InstallationOptions.InstallationRoot;
                // but preferences overrides applist installation options!
                if (InstallPadApp.Preferences.InstallationRoot != string.Empty)
                    this.InstallationRoot = InstallPadApp.Preferences.InstallationRoot;
            }

            Init();
        }
        public ApplicationDialog()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            this.appNameBox.TextChanged += new EventHandler(appNameBox_TextChanged);
            this.downloadUrlBox.TextChanged += new EventHandler(downloadUrlBox_TextChanged);
            EnableSaveButtonIfValid();
        }

        void appNameBox_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        void downloadUrlBox_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            if (ValidateAppName() && ValidateDownloadUrl())
                saveButton.Enabled = true;
            else
                saveButton.Enabled = false;            
        }
        private bool ValidateDownloadUrl()
        {
            if (downloadUrlBox.Text.Length <= 0)
                return false;

            return true;
        }
        private bool ValidateAppName()
        {
            if (appNameBox.Text.Length <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// If the form is valid, this will enable the save button.
        /// </summary>
        private void EnableSaveButtonIfValid()
        {
            if (appNameBox.Text.Length > 0 && downloadUrlBox.Text.Length > 0)
                saveButton.Enabled = true;
            else
                saveButton.Enabled = false;
        }

        #region Properties
        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
        public string ApplicationName
        {
            get
            {
                return this.appNameBox.Text;
            }
            set
            {
                this.appNameBox.Text = value;
            }
        }
        public string DownloadUrl
        {
            get
            {
                return this.downloadUrlBox.Text;
            }
            set
            {
                this.downloadUrlBox.Text = value;
            }
        }
        public string Comment
        {
            get
            {
                return this.appCommentBox.Text;
            }
            set
            {
                this.appCommentBox.Text = value;
            }
        }
        public bool Checked
        {
            get
            {
                return this.checkedByDefault.Checked;
            }
            set { this.checkedByDefault.Checked = value; }
        
        }
        public bool DownloadLatestVersion
        {
            get
            {
                return this.latestVersionCheck.Checked;
            }
            set
            {
                this.latestVersionCheck.Checked = value;
            }
        }
        public bool SilentInstall
        {
            get
            {
                return this.silentInstallCheck.Checked;
            }
            set
            {
                this.silentInstallCheck.Checked = value;
            }
        }
        public string InstallerArguments
        {
            get
            {
                return this.installerArgumentsBox.Text;
            }
            set
            {
                this.installerArgumentsBox.Text = value;
            }
        }
        // TODO: not sure what to do with this UI feature. Should the installation root
        // even be settable per application in the UI? It's currently a global user preference
        public string InstallationRoot
        {
            get
            {
                //return this.installationRootBox.Text;
                return null;
            }
            set
            {
                //this.installationRootBox.Text = value;
                
            }
        }
        public ApplicationItem ApplicationItem
        {
            get
            {
                return this.applicationItem;
            }
        }
        #endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.saved = true;

            // If this wasn't used to edit an ApplicationItem, create a new one
            if (this.applicationItem == null)
                this.applicationItem = new ApplicationItem();

            ModifyApplicationItemFromDialog(this.applicationItem);
            
            this.Close();
        }
        public void ModifyApplicationItemFromDialog(ApplicationItem item)
        {
            item.Name = this.ApplicationName;
            item.DownloadUrl = this.DownloadUrl;
            item.Comment = this.Comment;
            item.Options.DownloadLatestVersion = this.DownloadLatestVersion;
            item.Options.SilentInstall = this.SilentInstall;
            item.Options.InstallerArguments = this.InstallerArguments;
            item.Options.Checked = this.Checked;

            // Setting InstallationRoot is disabled for now until the rules are decided.
            // Could allow setting Options, but probably wouldn't want to allow setting
            // AppList, and\or Preferences.  Might be confusing either way.

            // Maybe there should be no GUI for installation root except via preferences.
        }

        public bool Saved
        {
            get { return saved; }
        }
    }
}