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
    public partial class CreditsDialog : Form
    {
        private string[] writtenBy = null;

        public string[] WrittenBy
        {
            get { return writtenBy; }
            set { writtenBy = value; }
        }
        private string[] translatedBy = null;

        public string[] TranslatedBy
        {
            get { return translatedBy; }
            set { translatedBy = value; }
        }

        private string[] artworkBy = null;

        public string[] ArtworkBy
        {
            get { return artworkBy; }
            set { artworkBy = value; }
        }

        public CreditsDialog()
        {
            InitializeComponent();
        }

        private void CreditsDialog_Load(object sender, EventArgs e)
        {
            if (writtenBy != null)
            {
                writtenByTextBox.Text = "";
                foreach (String author in this.writtenBy)
                    writtenByTextBox.AppendText(author + "\n");
            }
            else
                tabControl.TabPages.Remove(writtenByTab);

            if (translatedBy != null)
            {
                translatedByTextBox.Text = "";
                foreach (String author in this.translatedBy)
                    translatedByTextBox.AppendText(author + "\n");
            }
            else
                tabControl.TabPages.Remove(translatedByTab);

            if (artworkBy != null)
            {
                artworkByTextBox.Text = "";
                foreach (String author in this.artworkBy)
                    artworkByTextBox.AppendText(author + "\n");
            }
            else
                tabControl.TabPages.Remove(artworkByTab);
            
        }
    }
}