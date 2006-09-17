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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InstallPad
{
    /// <summary>
    /// A small dialog used to show many errors at once, in a list view.
    /// Used to report multiple problems parsing applist xml files
    /// </summary>
    public partial class AppListErrorDialog : Form
    {
        public AppListErrorDialog()
        {
            InitializeComponent();
        }
        public string ErrorText
        {
            set
            {
                this.errorsText.Text = value;
            }
            get
            {
                return this.errorsText.Text;
            }
        }
    }
}