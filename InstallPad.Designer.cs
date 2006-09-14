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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusPanel1 = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.errorLink = new System.Windows.Forms.LinkLabel();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.statusPanel2 = new System.Windows.Forms.Panel();
            this.statusPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.statusPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInstall
            // 
            this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstall.Location = new System.Drawing.Point(334, 340);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(75, 23);
            this.buttonInstall.TabIndex = 0;
            this.buttonInstall.Text = "&Install All";
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
            this.controlListPanel.Size = new System.Drawing.Size(416, 252);
            this.controlListPanel.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(415, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusPanel1
            // 
            this.statusPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusPanel1.Controls.Add(this.errorLabel);
            this.statusPanel1.Location = new System.Drawing.Point(0, 340);
            this.statusPanel1.Name = "statusPanel1";
            this.statusPanel1.Size = new System.Drawing.Size(415, 21);
            this.statusPanel1.TabIndex = 5;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(12, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(265, 13);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Text = "There were some problems loading the application file. ";
            this.errorLabel.Visible = false;
            // 
            // errorLink
            // 
            this.errorLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorLink.AutoSize = true;
            this.errorLink.Location = new System.Drawing.Point(12, 1);
            this.errorLink.Name = "errorLink";
            this.errorLink.Size = new System.Drawing.Size(59, 13);
            this.errorLink.TabIndex = 6;
            this.errorLink.TabStop = true;
            this.errorLink.Text = "View errors";
            this.errorLink.Visible = false;
            this.errorLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.errorLink_LinkClicked);
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
            this.logoBox.Size = new System.Drawing.Size(417, 75);
            this.logoBox.TabIndex = 3;
            this.logoBox.TabStop = false;
            // 
            // statusPanel2
            // 
            this.statusPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusPanel2.Controls.Add(this.errorLink);
            this.statusPanel2.Location = new System.Drawing.Point(0, 353);
            this.statusPanel2.Name = "statusPanel2";
            this.statusPanel2.Size = new System.Drawing.Size(401, 22);
            this.statusPanel2.TabIndex = 7;
            // 
            // InstallPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(415, 375);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.statusPanel2);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.controlListPanel);
            this.Controls.Add(this.statusPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(364, 275);
            this.Name = "InstallPad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InstallPad";
            this.Load += new System.EventHandler(this.InstallPad_Load);
            this.statusPanel1.ResumeLayout(false);
            this.statusPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.statusPanel2.ResumeLayout(false);
            this.statusPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Panel controlListPanel;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel statusPanel1;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.LinkLabel errorLink;
        private System.Windows.Forms.Panel statusPanel2;
    }
}

