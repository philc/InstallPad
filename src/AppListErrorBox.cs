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
    /// This is a little box that sits in the middle of the main InstallPad
    /// window that can show errors (like missing applist) prominently
    /// </summary>
    public partial class AppListErrorBox : UserControl
    {
        public AppListErrorBox()
        {
            InitializeComponent();
        }

        private string detailsText;

        /// <summary>
        /// More detailed error information avaialable via a "Details..." link
        /// </summary>
        public string DetailsText
        {
            get { return detailsText; }
            set
            {
                detailsText = value;
            }
        }

        /// <summary>
        /// Show a "Details..." link that pops up a dialog containing DetailsText
        /// </summary>
        public bool DetailsVisible
        {
            set
            {
                detailsLink.Visible = value;
            }
        }

        private void detailsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppListErrorDialog d = new AppListErrorDialog();
            d.ErrorText = DetailsText;
            d.ShowDialog();
        }

        

    }
}
