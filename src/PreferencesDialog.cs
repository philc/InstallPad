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

using InstallPad.Properties;

namespace InstallPad
{
    public partial class PreferencesDialog : Form
    {
        // Could bind the controls to a preference value... meh.
        // http://msdn2.microsoft.com/en-us/library/system.windows.forms.binding.aspx

        private Dictionary<string, string> defaults = null;

        public Dictionary<string, string> Defaults
        {
            get { return defaults; }
            set { defaults = value; }
        }

        public PreferencesDialog()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(PreferencesDialog_FormClosing);
        }

        void PreferencesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save preferences when closed
            if (this.DialogResult == DialogResult.OK)
                SavePreferences();
        }
        private void SavePreferences()
        {
            InstallPadApp.Preferences.InstallationRoot = this.extractTo.Text;
            InstallPadApp.Preferences.DownloadTo = this.downloadTo.Text;
        }

        private void PreferencesDialog_Load(object sender, EventArgs e)
        {
            this.downloadTo.Text = InstallPadApp.Preferences.DownloadTo;
            this.extractTo.Text = InstallPadApp.Preferences.InstallationRoot;
        }

        private void downloadToButton_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.downloadTo.Text))
                downloadToDialog.SelectedPath=this.downloadTo.Text;

            DialogResult result = downloadToDialog.ShowDialog();
            if (result == DialogResult.OK)
                this.downloadTo.Text=downloadToDialog.SelectedPath;
        }

        private void extractToButton_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.extractTo.Text))
                extractToDialog.SelectedPath = this.extractTo.Text;

            DialogResult result = extractToDialog.ShowDialog();
            if (result == DialogResult.OK)
                this.extractTo.Text = extractToDialog.SelectedPath;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            extractTo.Text = Defaults[Resources.InstallationRoot];
            downloadTo.Text = Defaults[Resources.DownloadTo];
        }

        public override void Refresh()
        {
            base.Refresh();

            resetButton.Enabled = (extractTo.Text != defaults[Resources.InstallationRoot]) ||
                                  (downloadTo.Text != defaults[Resources.DownloadTo]);
        }

        private void extractTo_TextChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void downloadTo_TextChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}