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
    /// <summary>
    /// Box to show errors, used in ApplicationListItems
    /// </summary>
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

        /// <summary>
        /// Caption to put on the "show details" link
        /// </summary>
        public string DetailsCaption
        {
            get { return detailsCaption; }
            set { detailsCaption = value;
            BuildControlLayout();
        }
        }

        private string detailsText;

        /// <summary>
        /// Details of the error message
        /// </summary>
        public string DetailsText
        {
            get { return detailsText; }
            set { detailsText = value; }
        }
        private bool detailsOnSameLine = false;

        /// <summary>
        /// Determines whether the details should be displayed on the same 
        /// line of text as the error message
        /// </summary>
        public bool DetailsOnSameLine
        {
            get { return detailsOnSameLine; }
            set
            {
                detailsOnSameLine = value;
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
            d.ErrorText = DetailsText;
            d.ShowDialog();
        }
    }
}
