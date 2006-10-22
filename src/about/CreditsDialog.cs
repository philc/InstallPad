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

        public CreditsDialog()
        {
            InitializeComponent();
        }

        private void CreditsDialog_Load(object sender, EventArgs e)
        {
            if (writtenBy!=null){
                foreach (String author in this.writtenBy)
                    writtenByTextBox.AppendText(author + "\n");
            }
            
        }
    }
}