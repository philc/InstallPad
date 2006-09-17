namespace InstallPad
{
    partial class AppListErrorBox
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
            this.openLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // detailsLink
            // 
            this.detailsLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detailsLink.AutoSize = true;
            this.detailsLink.BackColor = System.Drawing.SystemColors.ControlLight;
            this.detailsLink.Location = new System.Drawing.Point(3, 51);
            this.detailsLink.Name = "detailsLink";
            this.detailsLink.Size = new System.Drawing.Size(63, 13);
            this.detailsLink.TabIndex = 6;
            this.detailsLink.TabStop = true;
            this.detailsLink.Text = "View details";
            this.detailsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.detailsLink_LinkClicked);
            // 
            // errorLabel
            // 
            this.errorLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.errorLabel.Location = new System.Drawing.Point(3, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Padding = new System.Windows.Forms.Padding(4);
            this.errorLabel.Size = new System.Drawing.Size(210, 64);
            this.errorLabel.TabIndex = 5;
            this.errorLabel.Text = "Could not find an application file. Ensure that there is an applist.xml file in t" +
                "he same folder as InstallPad.exe";
            // 
            // openLink
            // 
            this.openLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openLink.AutoSize = true;
            this.openLink.BackColor = System.Drawing.SystemColors.ControlLight;
            this.openLink.Location = new System.Drawing.Point(95, 51);
            this.openLink.Name = "openLink";
            this.openLink.Size = new System.Drawing.Size(118, 13);
            this.openLink.TabIndex = 7;
            this.openLink.TabStop = true;
            this.openLink.Text = "Open an application file";
            // 
            // AppListErrorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.openLink);
            this.Controls.Add(this.detailsLink);
            this.Controls.Add(this.errorLabel);
            this.Name = "AppListErrorBox";
            this.Size = new System.Drawing.Size(216, 68);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel detailsLink;
        public System.Windows.Forms.Label errorLabel;
        public System.Windows.Forms.LinkLabel openLink;
    }
}
