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

namespace CodeProject.AboutDialog
{
    public partial class FeedbackDialog : Form
    {
        public FeedbackDialog()
        {
            InitializeComponent();
        }

        public string To
        {
            get { return txtTo.Text; }
            set { txtTo.Text = value; }
        }

        private void FeedbackDialog_Load(object sender, EventArgs e)
        {
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text.Length == 0)
            {
                MessageBox.Show("Please enter a return address.");
            }
            if (txtSubject.Text.Length == 0)
            {
                MessageBox.Show("Please enter a subject.");
            }
            if (txtMessage.Text.Length == 0)
            {
                MessageBox.Show("Please enter a message.");
            }

            // Send email...
        }
    }
}