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
    partial class ApplicationDialog
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
            this.components = new System.ComponentModel.Container();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.appNameBox = new System.Windows.Forms.TextBox();
            this.appNameLabel = new System.Windows.Forms.Label();
            this.downloadUrlLabel = new System.Windows.Forms.Label();
            this.downloadUrlBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkedByDefault = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.installerArgumentsBox = new System.Windows.Forms.TextBox();
            this.silentInstallCheck = new System.Windows.Forms.CheckBox();
            this.latestVersionCheck = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.appCommentLabel = new System.Windows.Forms.Label();
            this.appCommentBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(151, 306);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(232, 306);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // appNameBox
            // 
            this.appNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.appNameBox.Location = new System.Drawing.Point(16, 31);
            this.appNameBox.Name = "appNameBox";
            this.appNameBox.Size = new System.Drawing.Size(291, 20);
            this.appNameBox.TabIndex = 1;
            // 
            // appNameLabel
            // 
            this.appNameLabel.AutoSize = true;
            this.appNameLabel.Location = new System.Drawing.Point(13, 15);
            this.appNameLabel.Name = "appNameLabel";
            this.appNameLabel.Size = new System.Drawing.Size(91, 13);
            this.appNameLabel.TabIndex = 0;
            this.appNameLabel.Text = "Application &name:";
            // 
            // downloadUrlLabel
            // 
            this.downloadUrlLabel.AutoSize = true;
            this.downloadUrlLabel.Location = new System.Drawing.Point(13, 61);
            this.downloadUrlLabel.Name = "downloadUrlLabel";
            this.downloadUrlLabel.Size = new System.Drawing.Size(83, 13);
            this.downloadUrlLabel.TabIndex = 2;
            this.downloadUrlLabel.Text = "Download &URL:";
            // 
            // downloadUrlBox
            // 
            this.downloadUrlBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadUrlBox.Location = new System.Drawing.Point(16, 77);
            this.downloadUrlBox.Name = "downloadUrlBox";
            this.downloadUrlBox.Size = new System.Drawing.Size(291, 20);
            this.downloadUrlBox.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkedByDefault);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.installerArgumentsBox);
            this.groupBox1.Controls.Add(this.silentInstallCheck);
            this.groupBox1.Controls.Add(this.latestVersionCheck);
            this.groupBox1.Location = new System.Drawing.Point(16, 160);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 137);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // checkedByDefault
            // 
            this.checkedByDefault.AutoSize = true;
            this.checkedByDefault.Checked = true;
            this.checkedByDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkedByDefault.Location = new System.Drawing.Point(8, 65);
            this.checkedByDefault.Name = "checkedByDefault";
            this.checkedByDefault.Size = new System.Drawing.Size(118, 17);
            this.checkedByDefault.TabIndex = 8;
            this.checkedByDefault.Text = "C&hecked by default";
            this.checkedByDefault.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Installer &arguments:";
            // 
            // installerArgumentsBox
            // 
            this.installerArgumentsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.installerArgumentsBox.Location = new System.Drawing.Point(6, 107);
            this.installerArgumentsBox.Name = "installerArgumentsBox";
            this.installerArgumentsBox.Size = new System.Drawing.Size(277, 20);
            this.installerArgumentsBox.TabIndex = 10;
            // 
            // silentInstallCheck
            // 
            this.silentInstallCheck.AutoSize = true;
            this.silentInstallCheck.Checked = true;
            this.silentInstallCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.silentInstallCheck.Location = new System.Drawing.Point(8, 19);
            this.silentInstallCheck.Name = "silentInstallCheck";
            this.silentInstallCheck.Size = new System.Drawing.Size(81, 17);
            this.silentInstallCheck.TabIndex = 6;
            this.silentInstallCheck.Text = "Silen&t install";
            this.silentInstallCheck.UseVisualStyleBackColor = true;
            // 
            // latestVersionCheck
            // 
            this.latestVersionCheck.AutoSize = true;
            this.latestVersionCheck.Location = new System.Drawing.Point(8, 42);
            this.latestVersionCheck.Name = "latestVersionCheck";
            this.latestVersionCheck.Size = new System.Drawing.Size(139, 17);
            this.latestVersionCheck.TabIndex = 7;
            this.latestVersionCheck.Text = "Download latest &version";
            this.latestVersionCheck.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // appCommentLabel
            // 
            this.appCommentLabel.AutoSize = true;
            this.appCommentLabel.Location = new System.Drawing.Point(13, 108);
            this.appCommentLabel.Name = "appCommentLabel";
            this.appCommentLabel.Size = new System.Drawing.Size(54, 13);
            this.appCommentLabel.TabIndex = 4;
            this.appCommentLabel.Text = "Comme&nt:";
            // 
            // appCommentBox
            // 
            this.appCommentBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.appCommentBox.Location = new System.Drawing.Point(16, 124);
            this.appCommentBox.Name = "appCommentBox";
            this.appCommentBox.Size = new System.Drawing.Size(291, 20);
            this.appCommentBox.TabIndex = 5;
            // 
            // ApplicationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 341);
            this.Controls.Add(this.appCommentBox);
            this.Controls.Add(this.appCommentLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.downloadUrlBox);
            this.Controls.Add(this.downloadUrlLabel);
            this.Controls.Add(this.appNameLabel);
            this.Controls.Add(this.appNameBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(199, 379);
            this.Name = "ApplicationDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox appNameBox;
        private System.Windows.Forms.Label appNameLabel;
        private System.Windows.Forms.Label downloadUrlLabel;
        private System.Windows.Forms.TextBox downloadUrlBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox latestVersionCheck;
        private System.Windows.Forms.CheckBox silentInstallCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox installerArgumentsBox;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox appCommentBox;
        private System.Windows.Forms.Label appCommentLabel;
        private System.Windows.Forms.CheckBox checkedByDefault;
    }
}