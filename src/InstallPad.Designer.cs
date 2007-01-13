//
// Author: Phil Crosby
//

// Copyright (C) 2006 Phil Crosby
// Permission is granted to use, copy, modify, and merge copies
// of this software for personal use. Permission is not granted
// to use or change this software for commercial use or commercial
// redistribution. Permission is not granted to use, modify or 
// distribute this software internally within a corporation.
using InstallPad.Properties;

namespace InstallPad
{
    partial class InstallPad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallPad));
            this.buttonInstall = new System.Windows.Forms.Button();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.openAppList = new System.Windows.Forms.LinkLabel();
            this.preferencesLink = new System.Windows.Forms.LinkLabel();
            this.aboutLink = new System.Windows.Forms.LinkLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.labelSearchOnline = new System.Windows.Forms.Label();
            this.errorPanel = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.errorLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.errorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInstall
            // 
            this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstall.Location = new System.Drawing.Point(331, 422);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(89, 22);
            this.buttonInstall.TabIndex = 0;
            this.buttonInstall.Text = "&Install Checked";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // logoBox
            // 
            this.logoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logoBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(112)))), ((int)(((byte)(152)))));
            this.logoBox.BackgroundImage = global::InstallPad.Properties.Resources.logoBorder;
            this.logoBox.Image = global::InstallPad.Properties.Resources.logo;
            this.logoBox.Location = new System.Drawing.Point(-1, 0);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(429, 75);
            this.logoBox.TabIndex = 3;
            this.logoBox.TabStop = false;
            // 
            // openAppList
            // 
            this.openAppList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openAppList.AutoSize = true;
            this.openAppList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openAppList.LinkColor = System.Drawing.Color.MediumBlue;
            this.openAppList.Location = new System.Drawing.Point(6, 425);
            this.openAppList.Name = "openAppList";
            this.openAppList.Size = new System.Drawing.Size(147, 16);
            this.openAppList.TabIndex = 11;
            this.openAppList.TabStop = true;
            this.openAppList.Text = "&Open an application list";
            this.openAppList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openAppList_LinkClicked);
            // 
            // preferencesLink
            // 
            this.preferencesLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.preferencesLink.AutoSize = true;
            this.preferencesLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesLink.LinkColor = System.Drawing.Color.MediumBlue;
            this.preferencesLink.Location = new System.Drawing.Point(159, 425);
            this.preferencesLink.Name = "preferencesLink";
            this.preferencesLink.Size = new System.Drawing.Size(81, 16);
            this.preferencesLink.TabIndex = 12;
            this.preferencesLink.TabStop = true;
            this.preferencesLink.Text = "&Preferences";
            this.preferencesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.preferencesLink_LinkClicked);
            // 
            // aboutLink
            // 
            this.aboutLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.aboutLink.AutoSize = true;
            this.aboutLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutLink.LinkColor = System.Drawing.Color.MediumBlue;
            this.aboutLink.Location = new System.Drawing.Point(246, 425);
            this.aboutLink.Name = "aboutLink";
            this.aboutLink.Size = new System.Drawing.Size(43, 16);
            this.aboutLink.TabIndex = 13;
            this.aboutLink.TabStop = true;
            this.aboutLink.Text = "About";
            this.aboutLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.aboutLink_LinkClicked);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(-1, 73);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel1.Controls.Add(this.errorPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.searchPanel);
            this.splitContainer.Size = new System.Drawing.Size(429, 349);
            this.splitContainer.SplitterDistance = 315;
            this.splitContainer.TabIndex = 17;
            // 
            // searchPanel
            // 
            this.searchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPanel.Controls.Add(this.button1);
            this.searchPanel.Controls.Add(this.SearchBox);
            this.searchPanel.Controls.Add(this.labelSearchOnline);
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(429, 30);
            this.searchPanel.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(332, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBox.Location = new System.Drawing.Point(124, 4);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(199, 20);
            this.SearchBox.TabIndex = 1;
            // 
            // labelSearchOnline
            // 
            this.labelSearchOnline.AutoSize = true;
            this.labelSearchOnline.Location = new System.Drawing.Point(6, 7);
            this.labelSearchOnline.Name = "labelSearchOnline";
            this.labelSearchOnline.Size = new System.Drawing.Size(112, 13);
            this.labelSearchOnline.TabIndex = 0;
            this.labelSearchOnline.Text = "&Find new applications:";
            // 
            // errorPanel
            // 
            this.errorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.errorPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.errorPanel.Controls.Add(this.errorLabel);
            this.errorPanel.Controls.Add(this.errorLink);
            this.errorPanel.Location = new System.Drawing.Point(0, 291);
            this.errorPanel.Name = "errorPanel";
            this.errorPanel.Size = new System.Drawing.Size(428, 25);
            this.errorPanel.TabIndex = 16;
            this.errorPanel.Visible = false;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.Location = new System.Drawing.Point(4, 6);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(262, 13);
            this.errorLabel.TabIndex = 12;
            this.errorLabel.Text = "There were some problems loading the application file.";
            // 
            // errorLink
            // 
            this.errorLink.AutoSize = true;
            this.errorLink.BackColor = System.Drawing.Color.Transparent;
            this.errorLink.LinkColor = System.Drawing.Color.MediumBlue;
            this.errorLink.Location = new System.Drawing.Point(264, 6);
            this.errorLink.Name = "errorLink";
            this.errorLink.Size = new System.Drawing.Size(59, 13);
            this.errorLink.TabIndex = 11;
            this.errorLink.TabStop = true;
            this.errorLink.Text = "View errors";
            // 
            // InstallPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(427, 457);
            this.Controls.Add(this.aboutLink);
            this.Controls.Add(this.preferencesLink);
            this.Controls.Add(this.openAppList);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(435, 275);
            this.Name = "InstallPad";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InstallPad";
            this.Load += new System.EventHandler(this.InstallPad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.errorPanel.ResumeLayout(false);
            this.errorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.LinkLabel openAppList;
        private System.Windows.Forms.LinkLabel preferencesLink;
        private System.Windows.Forms.LinkLabel aboutLink;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Label labelSearchOnline;
        private System.Windows.Forms.Panel errorPanel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.LinkLabel errorLink;
    }
}

