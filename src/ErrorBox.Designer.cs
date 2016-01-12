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
    partial class ErrorBox
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
            this.detailsLink = new System.Windows.Forms.LinkLabel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // detailsLink
            // 
            this.detailsLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsLink.AutoSize = true;
            this.detailsLink.BackColor = System.Drawing.Color.Transparent;
            this.detailsLink.LinkColor = System.Drawing.Color.MediumBlue;
            this.detailsLink.Location = new System.Drawing.Point(126, 18);
            this.detailsLink.Name = "detailsLink";
            this.detailsLink.Size = new System.Drawing.Size(85, 13);
            this.detailsLink.TabIndex = 6;
            this.detailsLink.TabStop = true;
            this.detailsLink.Text = "More information";
            this.detailsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreInfoLink_LinkClicked);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.errorLabel.Location = new System.Drawing.Point(3, 3);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(54, 13);
            this.errorLabel.TabIndex = 5;
            this.errorLabel.Text = "errorLabel";
            // 
            // ErrorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.detailsLink);
            this.Controls.Add(this.errorLabel);
            this.Name = "ErrorBox";
            this.Size = new System.Drawing.Size(211, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel detailsLink;
        private System.Windows.Forms.Label errorLabel;
    }
}
