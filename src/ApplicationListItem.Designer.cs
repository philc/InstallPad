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
    partial class ApplicationListItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.installLink = new System.Windows.Forms.LinkLabel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkboxEnabled = new System.Windows.Forms.CheckBox();
            this.labelProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(29, 13);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(118, 15);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "application name";
            // 
            // installLink
            // 
            this.installLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.installLink.AutoSize = true;
            this.installLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installLink.LinkColor = System.Drawing.Color.MediumBlue;
            this.installLink.Location = new System.Drawing.Point(351, 28);
            this.installLink.Name = "installLink";
            this.installLink.Size = new System.Drawing.Size(42, 16);
            this.installLink.TabIndex = 2;
            this.installLink.TabStop = true;
            this.installLink.Text = "Install";
            this.installLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.installLink_LinkClicked);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(293, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 19);
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelStatus.Location = new System.Drawing.Point(312, 4);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(85, 16);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Downloaded";
            this.labelStatus.Visible = false;
            // 
            // checkboxEnabled
            // 
            this.checkboxEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.checkboxEnabled.AutoSize = true;
            this.checkboxEnabled.Checked = true;
            this.checkboxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxEnabled.Location = new System.Drawing.Point(8, 15);
            this.checkboxEnabled.Name = "checkboxEnabled";
            this.checkboxEnabled.Size = new System.Drawing.Size(15, 14);
            this.checkboxEnabled.TabIndex = 1;
            this.checkboxEnabled.UseVisualStyleBackColor = true;
            this.checkboxEnabled.CheckedChanged += new System.EventHandler(this.checkboxEnabled_CheckedChanged);
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(252, 9);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(47, 13);
            this.labelProgress.TabIndex = 7;
            this.labelProgress.Text = "progress";
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelProgress.Visible = false;
            // 
            // ApplicationListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.checkboxEnabled);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.installLink);
            this.Controls.Add(this.labelName);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ApplicationListItem";
            this.Size = new System.Drawing.Size(397, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.LinkLabel installLink;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkboxEnabled;
        private System.Windows.Forms.Label labelProgress;
    }
}
