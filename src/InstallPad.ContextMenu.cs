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
using System.Text;
using System.Windows.Forms;

namespace InstallPad
{
    public partial class InstallPad
    {
        private MenuItem addNewItem;
        private MenuItem removeItem;
        private MenuItem editItem;

        // Used to save which control was highlighted. When a menu collapses,
        // the control is no longer highlighted. We use this to determine
        // the insertion point for the "add item" menu entry
        Control highlightedControl;

        ContextMenu menu;

        private void BuildContextMenuEntries()
        {
            menu = new ContextMenu();
            addNewItem = new MenuItem("&Add new application");
            addNewItem.Click += new EventHandler(addNewItem_Click);

            editItem = new MenuItem("&Edit application");
            editItem.Click += new EventHandler(editItem_Click);

            removeItem = new MenuItem("&Remove application");
            removeItem.Click += new EventHandler(removeItem_Click);

            menu.Popup += new EventHandler(menu_Popup);
            menu.Collapse += new EventHandler(menu_Collapse);

            menu.MenuItems.Add(addNewItem);
            menu.MenuItems.Add(editItem);
            menu.MenuItems.Add(removeItem);
        }

        void menu_Collapse(object sender, EventArgs e)
        {
            // Unhighlight the entry
            controlList.Unhighlight(controlList.HighlightedEntry);
        }

        void menu_Popup(object sender, EventArgs e)
        {
            // Figure out which control is selected and visually highlight it
            Control c = controlList.ControlAtAbsolutePosition(Cursor.Position);
            controlList.Unhighlight(controlList.HighlightedEntry);
            if (c != null && c is ApplicationListItem)
            {
                // Enable edit and remove buttons
                this.editItem.Enabled = true;
                this.removeItem.Enabled = true;

                controlList.Highlight(c);
                highlightedControl = c;
            }
            else
            {
                // Disable menu entries pertaining to a specific item
                this.editItem.Enabled = false;
                this.removeItem.Enabled = false;
                highlightedControl = null;
            }
        }
        void addNewItem_Click(object sender, EventArgs e)
        {
            ApplicationDialog dialog = new ApplicationDialog();
            dialog.Title = "Add New Application";
            dialog.ShowDialog(this);

            if (dialog.Saved)
            {
                ApplicationListItem listItem = CreateApplicationListItem(dialog.ApplicationItem);
                int i = controlList.IndexOfListItem(highlightedControl);
                controlList.AddControl(listItem,i);

                if (i>=0)
                    InstallPadApp.AppList.ApplicationItems.Insert(i,dialog.ApplicationItem);
                else
                    InstallPadApp.AppList.ApplicationItems.Add(dialog.ApplicationItem);

                SaveApplist();
            }
            
        }
        void removeItem_Click(object sender, EventArgs e)
        {
            ApplicationListItem item = (ApplicationListItem)highlightedControl;
            string appTitle = item.ApplicationItem.Name;

            if (item.State==ApplicationListItem.InstallState.Downloading){
                MessageBox.Show(String.Format("{0} is currently being downloaded. Stop the download before removing the item.",appTitle));
                return;
            }
            else if (item.State == ApplicationListItem.InstallState.Installing)
            {
                MessageBox.Show(String.Format("{0} is currently being installed. Stop the download before removing the item.", appTitle));
                return;
            }


            DialogResult result = MessageBox.Show(this,
                String.Format("Are you sure you want to remove '{0}' from the list of installable applications?", appTitle),
                String.Format("Remove {0}?", appTitle),
                MessageBoxButtons.YesNo);
            if (result.Equals(DialogResult.Yes))
            {
                controlList.RemoveControl(item);
                InstallPadApp.AppList.ApplicationItems.Remove(item.ApplicationItem);
                SaveApplist();
            }            
        }

        void editItem_Click(object sender, EventArgs e)
        {
            ApplicationListItem item = (ApplicationListItem)highlightedControl;
            ApplicationDialog dialog = new ApplicationDialog(item.ApplicationItem);
            dialog.Title = "Edit Application";
            dialog.ShowDialog(this);

            if (dialog.Saved)
            {
                // Update the list item
                item.ApplicationItem = item.ApplicationItem;
                SaveApplist();
            }            
        }
        private void SaveApplist()
        {
            try
            {
                InstallPadApp.AppList.SaveToFile();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(this,
                    String.Format("Could not save the application list: {0}", e.Message), 
                    "Error saving application list",MessageBoxButtons.OK);
            }
        }
    }
}
