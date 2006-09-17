//
// Author: Phil Crosby
//

// Copyright (C) 2006 Phil Crosby
// Permission is granted to use, copy, modify, and merge copies
// of this software for personal use. Permission is not granted
// to use or change this software for commercial use or commercial
// redistribution. Permission is not granted to use, modify or 
// distribute this software internally within a corporation.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace InstallPad
{
    public partial class AppListErrorBox : UserControl
    {
        public AppListErrorBox()
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
                //BuildControlLayout();
            }
        }

        private string detailsCaption;

        public string DetailsCaption
        {
            get { return detailsCaption; }
            set { detailsCaption = value;
            //BuildControlLayout();
        }
        }

        private string detailsText;

        public string DetailsText
        {
            get { return detailsText; }
            set
            {
                detailsText = value;
            }
        }

        public bool DetailsVisible
        {
            set
            {
                detailsLink.Visible = value;
            }
        }

        private bool detailsOnSameLine = false;

        public bool DetailsOnSameLine
        {
            get { return detailsOnSameLine; }
            set { detailsOnSameLine = value;
            //BuildControlLayout(); 
        }
        }


        /*private void BuildControlLayout()
        {
            this.errorLabel.Text = errorCaption;
            this.detailsLink.Text = detailsCaption;
            if (DetailsOnSameLine)
            {
                this.detailsLink.Left = this.errorLabel.Right;
                this.detailsLink.Top = this.errorLabel.Top;
            }

        }*/

        private void detailsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppListErrorDialog d = new AppListErrorDialog();
            d.errorsText.Text = DetailsText;
            d.ShowDialog();

        }

        

    }
}
