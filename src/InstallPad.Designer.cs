//
// Author: Phil Crosby
//

// Copyright (C) 2006 Phil Crosby
// Permission is granted to use, copy, modify, and merge copies
// of this software for personal use. Permission is not granted
// to use or change this software for commercial use or commercial
// redistribution. Permission is not granted to use, modify or 
// distribute this software internally within a corporation.

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
            this.controlListPanel = new System.Windows.Forms.Panel();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.errorLink = new System.Windows.Forms.LinkLabel();
            this.openAppList = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInstall
            // 
            this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstall.Location = new System.Drawing.Point(331, 405);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(89, 23);
            this.buttonInstall.TabIndex = 0;
            this.buttonInstall.Text = "&Install Checked";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // controlListPanel
            // 
            this.controlListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.controlListPanel.BackColor = System.Drawing.SystemColors.Window;
            this.controlListPanel.Location = new System.Drawing.Point(0, 75);
            this.controlListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.controlListPanel.Name = "controlListPanel";
            this.controlListPanel.Size = new System.Drawing.Size(428, 317);
            this.controlListPanel.TabIndex = 1;
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
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.Location = new System.Drawing.Point(12, 418);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(262, 13);
            this.errorLabel.TabIndex = 10;
            this.errorLabel.Text = "There were some problems loading the application file.";
            this.errorLabel.Visible = false;
            // 
            // errorLink
            // 
            this.errorLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorLink.AutoSize = true;
            this.errorLink.BackColor = System.Drawing.Color.Transparent;
            this.errorLink.Location = new System.Drawing.Point(272, 418);
            this.errorLink.Name = "errorLink";
            this.errorLink.Size = new System.Drawing.Size(59, 13);
            this.errorLink.TabIndex = 9;
            this.errorLink.TabStop = true;
            this.errorLink.Text = "View errors";
            this.errorLink.Visible = false;
            // 
            // openAppList
            // 
            this.openAppList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openAppList.AutoSize = true;
            this.openAppList.Location = new System.Drawing.Point(12, 400);
            this.openAppList.Name = "openAppList";
            this.openAppList.Size = new System.Drawing.Size(126, 13);
            this.openAppList.TabIndex = 11;
            this.openAppList.TabStop = true;
            this.openAppList.Text = "Open an application list...";
            this.openAppList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openAppList_LinkClicked);
            // 
            // InstallPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(427, 440);
            this.Controls.Add(this.openAppList);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.errorLink);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.controlListPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(435, 275);
            this.Name = "InstallPad";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InstallPad";
            this.Load += new System.EventHandler(this.InstallPad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Panel controlListPanel;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.LinkLabel errorLink;
        private System.Windows.Forms.LinkLabel openAppList;
    }
}

