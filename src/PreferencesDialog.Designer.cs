namespace InstallPad
{
    partial class PreferencesDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.downloadTo = new System.Windows.Forms.TextBox();
            this.downloadToDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.downloadToButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.extractToButton = new System.Windows.Forms.Button();
            this.extractTo = new System.Windows.Forms.TextBox();
            this.extractToDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(199, 127);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "&Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(280, 127);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Download files to:";
            // 
            // downloadTo
            // 
            this.downloadTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadTo.Location = new System.Drawing.Point(15, 35);
            this.downloadTo.Name = "downloadTo";
            this.downloadTo.Size = new System.Drawing.Size(259, 20);
            this.downloadTo.TabIndex = 3;
            // 
            // downloadToButton
            // 
            this.downloadToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadToButton.Location = new System.Drawing.Point(280, 33);
            this.downloadToButton.Name = "downloadToButton";
            this.downloadToButton.Size = new System.Drawing.Size(75, 23);
            this.downloadToButton.TabIndex = 4;
            this.downloadToButton.Text = "Browse";
            this.downloadToButton.UseVisualStyleBackColor = true;
            this.downloadToButton.Click += new System.EventHandler(this.downloadToButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "&Extract any zip files to:";
            // 
            // extractToButton
            // 
            this.extractToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extractToButton.Location = new System.Drawing.Point(280, 83);
            this.extractToButton.Name = "extractToButton";
            this.extractToButton.Size = new System.Drawing.Size(75, 23);
            this.extractToButton.TabIndex = 7;
            this.extractToButton.Text = "Browse";
            this.extractToButton.UseVisualStyleBackColor = true;
            this.extractToButton.Click += new System.EventHandler(this.extractToButton_Click);
            // 
            // extractTo
            // 
            this.extractTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.extractTo.Location = new System.Drawing.Point(15, 85);
            this.extractTo.Name = "extractTo";
            this.extractTo.Size = new System.Drawing.Size(259, 20);
            this.extractTo.TabIndex = 6;
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(367, 162);
            this.Controls.Add(this.extractToButton);
            this.Controls.Add(this.extractTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.downloadToButton);
            this.Controls.Add(this.downloadTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(188, 190);
            this.Name = "PreferencesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.PreferencesDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox downloadTo;
        private System.Windows.Forms.FolderBrowserDialog downloadToDialog;
        private System.Windows.Forms.Button downloadToButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button extractToButton;
        private System.Windows.Forms.TextBox extractTo;
        private System.Windows.Forms.FolderBrowserDialog extractToDialog;
    }
}