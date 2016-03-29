namespace CodeProject.AboutDialog
{
    partial class CreditsDialog
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.writtenByTab = new System.Windows.Forms.TabPage();
            this.writtenByTextBox = new System.Windows.Forms.RichTextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.translatedByTab = new System.Windows.Forms.TabPage();
            this.artworkByTab = new System.Windows.Forms.TabPage();
            this.artworkByTextBox = new System.Windows.Forms.RichTextBox();
            this.translatedByTextBox = new System.Windows.Forms.RichTextBox();
            this.tabControl.SuspendLayout();
            this.writtenByTab.SuspendLayout();
            this.translatedByTab.SuspendLayout();
            this.artworkByTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.writtenByTab);
            this.tabControl.Controls.Add(this.translatedByTab);
            this.tabControl.Controls.Add(this.artworkByTab);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(240, 164);
            this.tabControl.TabIndex = 0;
            // 
            // writtenByTab
            // 
            this.writtenByTab.AutoScroll = true;
            this.writtenByTab.Controls.Add(this.writtenByTextBox);
            this.writtenByTab.Location = new System.Drawing.Point(4, 22);
            this.writtenByTab.Name = "writtenByTab";
            this.writtenByTab.Padding = new System.Windows.Forms.Padding(3);
            this.writtenByTab.Size = new System.Drawing.Size(232, 138);
            this.writtenByTab.TabIndex = 0;
            this.writtenByTab.Text = "Written by";
            this.writtenByTab.UseVisualStyleBackColor = true;
            // 
            // writtenByTextBox
            // 
            this.writtenByTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.writtenByTextBox.BackColor = System.Drawing.Color.White;
            this.writtenByTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.writtenByTextBox.Location = new System.Drawing.Point(6, 6);
            this.writtenByTextBox.Name = "writtenByTextBox";
            this.writtenByTextBox.ReadOnly = true;
            this.writtenByTextBox.Size = new System.Drawing.Size(220, 126);
            this.writtenByTextBox.TabIndex = 0;
            this.writtenByTextBox.Text = "";
            this.writtenByTextBox.WordWrap = false;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.Location = new System.Drawing.Point(177, 182);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // translatedByTab
            // 
            this.translatedByTab.Controls.Add(this.translatedByTextBox);
            this.translatedByTab.Location = new System.Drawing.Point(4, 22);
            this.translatedByTab.Name = "translatedByTab";
            this.translatedByTab.Size = new System.Drawing.Size(232, 138);
            this.translatedByTab.TabIndex = 1;
            this.translatedByTab.Text = "Translated By";
            this.translatedByTab.UseVisualStyleBackColor = true;
            // 
            // artworkByTab
            // 
            this.artworkByTab.Controls.Add(this.artworkByTextBox);
            this.artworkByTab.Location = new System.Drawing.Point(4, 22);
            this.artworkByTab.Name = "artworkByTab";
            this.artworkByTab.Size = new System.Drawing.Size(232, 138);
            this.artworkByTab.TabIndex = 2;
            this.artworkByTab.Text = "Artwork By";
            this.artworkByTab.UseVisualStyleBackColor = true;
            // 
            // artworkByTextBox
            // 
            this.artworkByTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.artworkByTextBox.BackColor = System.Drawing.Color.White;
            this.artworkByTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.artworkByTextBox.Location = new System.Drawing.Point(6, 6);
            this.artworkByTextBox.Name = "artworkByTextBox";
            this.artworkByTextBox.ReadOnly = true;
            this.artworkByTextBox.Size = new System.Drawing.Size(220, 126);
            this.artworkByTextBox.TabIndex = 1;
            this.artworkByTextBox.Text = "";
            this.artworkByTextBox.WordWrap = false;
            // 
            // translatedByTextBox
            // 
            this.translatedByTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.translatedByTextBox.BackColor = System.Drawing.Color.White;
            this.translatedByTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.translatedByTextBox.Location = new System.Drawing.Point(6, 6);
            this.translatedByTextBox.Name = "translatedByTextBox";
            this.translatedByTextBox.ReadOnly = true;
            this.translatedByTextBox.Size = new System.Drawing.Size(220, 126);
            this.translatedByTextBox.TabIndex = 2;
            this.translatedByTextBox.Text = "";
            this.translatedByTextBox.WordWrap = false;
            // 
            // CreditsDialog
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(264, 217);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Credits";
            this.Load += new System.EventHandler(this.CreditsDialog_Load);
            this.tabControl.ResumeLayout(false);
            this.writtenByTab.ResumeLayout(false);
            this.translatedByTab.ResumeLayout(false);
            this.artworkByTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage writtenByTab;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.RichTextBox writtenByTextBox;
        private System.Windows.Forms.TabPage translatedByTab;
        private System.Windows.Forms.TabPage artworkByTab;
        private System.Windows.Forms.RichTextBox artworkByTextBox;
        private System.Windows.Forms.RichTextBox translatedByTextBox;
    }
}