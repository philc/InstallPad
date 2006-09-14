using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace InstallPad
{
    public partial class ErrorBox : UserControl
    {
        public ErrorBox()
        {
            InitializeComponent();
        }

        private string errorCaption;

        public string ErrorCaption
        {
            get { return errorCaption; }
            set
            {
                errorCaption = value;
                BuildControlLayout();
            }
        }

        private string detailsCaption;

        public string DetailsCaption
        {
            get { return detailsCaption; }
            set { detailsCaption = value;
            BuildControlLayout();
        }
        }

        private string detailsText;

        public string DetailsText
        {
            get { return detailsText; }
            set { detailsText = value; }
        }

        private bool detailsOnSameLine = false;

        public bool DetailsOnSameLine
        {
            get { return detailsOnSameLine; }
            set { detailsOnSameLine = value;
            BuildControlLayout(); 
        }
        }


        private void BuildControlLayout()
        {
            this.errorLabel.Text = errorCaption;
            this.detailsLink.Text = detailsCaption;
            if (DetailsOnSameLine)
            {
                this.detailsLink.Left = this.errorLabel.Right;
                this.detailsLink.Top = this.errorLabel.Top;
            }
            this.Width = detailsLink.Right;
            this.Height = (errorLabel.Bottom > detailsLink.Bottom) ? errorLabel.Bottom : detailsLink.Bottom;

        }

        private void moreInfoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppListErrorDialog d = new AppListErrorDialog();
            d.errorsText.Text = DetailsText;
            d.ShowDialog();
            /*MessageBox.Show("InstallPad downloads and installs applications defined in " +
            "an applist.xml file, which must be in the same folder as InstallPad.exe. " +
            "If your application list file is named differently, you can specify its name " +
            "via the /f command line switch.",
            "Error loading applist.xml", MessageBoxButtons.OK);*/

        }
    }
}
