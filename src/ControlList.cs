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
using System.Diagnostics;
//using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace InstallPad
{
    #region ScrollPanel
    /// <summary>
    /// An autoscrollable panel used by ControlList to host the
    /// TableLayoutPanel. Essentially adds a scrollbar to TableLayout
    /// </summary>
    class ScrollPanel : ScrollableControl
    {
        Label label;
        public ScrollPanel()
        {   this.AutoScroll = true ;

            // Label acts as a place holder, it will reside next to the tablepanel
            label = new Label();
            label.Width = 1;
            label.Height = 10;
            this.Controls.Add(label);
        }

        public int MinimumHeight
        {
            set
            {
                this.label.Height = value;
            }
            get
            {
                return this.label.Height;
            }
        }
    }
    #endregion

    /// <summary>
    /// A control list shows controls in a scrollable list with alternating colors.
    /// </summary>
    public partial class ControlList : UserControl
    {
        #region Coloring
        static Color alternatingColor;            
        static Color highlightColor;

        static ControlList()
        {
            alternatingColor = Color.FromArgb(241, 241, 241);

            // The highlight is a little bit darker than the alternating
            highlightColor = Color.FromArgb(
                alternatingColor.R - 12, alternatingColor.G - 12, alternatingColor.B);
        }
        #endregion

        public event MouseEventHandler  ListItemClicked;
        public event MouseEventHandler ListItemDoubleClicked;

        public ControlList()
        {
            InitializeComponent();
            //this.DoubleBuffered = true;
        }

        private ScrollPanel scrollPanel;
        private TableLayoutPanel tableLayout;

        // Entry highlighted when a context menu is shown
        private Control highlightedEntry = null;

        private void ControlList_Load(object sender, EventArgs e)
        {
            
            // Build a scrollable control and add a table layout to it.
            this.tableLayout = new TableLayoutPanel();

            this.scrollPanel=new ScrollPanel();
            scrollPanel.Size = this.Size;

            scrollPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            this.Controls.Add(scrollPanel);
            this.scrollPanel.Controls.Add(this.tableLayout);

            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));

            this.tableLayout.Size = scrollPanel.Size;

            this.tableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // Don't anchor to the bottom; if you do, the table control will be too big and won't scroll right.
            //| System.Windows.Forms.AnchorStyles.Bottom));

            this.tableLayout.TabIndex = 2;

            Console.WriteLine(this.Width);
            Console.WriteLine(this.scrollPanel.Width);
            Console.WriteLine(this.tableLayout.Width);

            foreach (RowStyle style in this.tableLayout.RowStyles)
                style.SizeType = SizeType.Absolute;

            this.ContextMenu = new ContextMenu();

            UpdateTableHeight();
        }

        private void OnListItemClicked(object sender, MouseEventArgs e)
        {
            if (ListItemClicked != null)
                ListItemClicked(sender, e);
        }

        void OnlistItemDoubleClicked(object sender, MouseEventArgs e)
        {
            if (ListItemDoubleClicked != null)
                ListItemDoubleClicked(sender, e);
        }

        /// <summary>
        /// Find a control at the given screen position, in absolute coordinates.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Control ControlAtAbsolutePosition(Point p)
        {
            foreach (Control c in this.ListItems)
            {
                // Using absolute screen coordinates, because if we use coords relative to a control,
                // scrolling the control can mess that up.
                Rectangle bounds = new Rectangle(PointToClient(c.Bounds.Location), c.Bounds.Size);
                Point loc = Interop.UpperLeftCornerOfWindow(c.Handle);
                bounds = new Rectangle(loc, c.Bounds.Size);
                if (bounds.Contains(p))
                    return c;
            }
            return null;
        }

        public ControlCollection ListItems
        {
            get
            {
                return this.tableLayout.Controls;
            }
        }

        public void ClearListItems()
        {
            this.tableLayout.Controls.Clear();
            this.tableLayout.RowCount = 1;
            UpdateTableHeight();
        }

        /// <summary>
        /// Update the heights of the tablet layout and scroll panel controls
        /// </summary>
        private void UpdateTableHeight()
        {
            int controlsCount = tableLayout.Controls.Count;
            int controlHeight = (controlsCount > 0) ? tableLayout.Controls[0].Height : 0;
            this.tableLayout.MinimumSize = new Size(this.tableLayout.MinimumSize.Width, controlsCount*controlHeight);
            this.tableLayout.Height = controlHeight*controlsCount;
            this.scrollPanel.MinimumHeight = this.tableLayout.Height;
        }

        public void RemoveControl(Control control)
        {
            if (this.tableLayout.Controls.Contains(control)){
                int rowOfOldControl = this.tableLayout.GetRow(control);
                this.tableLayout.Controls.Remove(control);

                // Update minimum heights
                Size oldMinSize = this.tableLayout.MinimumSize;
                foreach (RowStyle style in this.tableLayout.RowStyles)
                {
                    style.SizeType = SizeType.Absolute;
                    style.Height = control.Height;
                }
                // Removing an entry leaves a hole in the cell positions of the table entries.
                // Shift everyone up into that hole.
                ShiftRowsUp(rowOfOldControl + 1);

                this.tableLayout.RowCount--;

                UpdateTableHeight();

                AlternateColorsOfEntries();
            }
        }
        /// <summary>
        /// All rows at 'row' will be shifted down one.
        /// </summary>
        /// <param name="row"></param>
        private void ShiftRowsDown(int row)
        {
            for (int i = this.tableLayout.RowCount - 1; i >= row; i-- )
            {
                Control c = this.tableLayout.GetControlFromPosition(0, i);
                if (c!=null)
                    this.tableLayout.SetRow(c, i + 1);
            }
        }
        /// <summary>
        /// All rows at 'row' will be shifted down one.
        /// </summary>
        /// <param name="row"></param>
        private void ShiftRowsUp(int row)
        {
            for (int i=row;i<this.tableLayout.RowCount-1;i++)
            {
                Control c = this.tableLayout.GetControlFromPosition(0, i);
                this.tableLayout.SetRow(c, i - 1);
            }
        }
        /// <summary>
        /// Add many controls at once. Improves drawing performance slightly.
        /// </summary>
        /// <param name="controls"></param>
        public void AddAll(List<Control> controls)
        {
            foreach (Control control in controls)
            {                
                FormatAndAddControl(control);
            }
            foreach (RowStyle style in this.tableLayout.RowStyles)
            {
                style.SizeType = SizeType.Absolute;
                style.Height = this.tableLayout.Controls[0].Height;
            }
            UpdateTableHeight();
            AlternateColorsOfEntries();
        }

        /// <summary>
        /// When a control is added, we need to add it to the TableView, adjust
        /// its width, listen to its visibility, and update our appearance.
        /// </summary>
        /// <param name="control"></param>
        public void AddControl(Control control)
        {
            // -1 is the end of the list
            AddControl(control, -1);
        }
        public void AddControl(Control control, int position)
        {
            FormatAndAddControl(control, position);
            
            // Set all row styles to be the same height. Neglecting to this will lead
            // to some oddly shaped rows.
            foreach (RowStyle style in this.tableLayout.RowStyles)
            {
                style.SizeType = SizeType.Absolute;
                style.Height = control.Height;
            }

            // Toggle the colors from white to light gray
            AlternateColorsOfEntries();

            UpdateTableHeight();
        }
        public int IndexOfListItem(Control control)
        {
            if (control == null)
                return -1;

            TableLayoutPanelCellPosition pos = this.tableLayout.GetCellPosition(control);
            return pos.Row;
        }

        /// <summary>
        /// Utility method to get a control read and stick it at the end of the table layout
        /// </summary>
        /// <param name="control"></param>
        private void FormatAndAddControl(Control control)
        {
            // -1 is the end of the list
            FormatAndAddControl(control, -1);
        }
        private void FormatAndAddControl(Control control, int position)
        {
            control.Width = this.Width;
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // If one of the controls we're showing gets hidden, we have to update our appearance.
            control.VisibleChanged += new EventHandler(control_VisibleChanged);

            this.tableLayout.RowCount++;

            // If they did not provide a position, insert at the end.
            // Insert new control before the last row of the table; the last row is of size zero
            // and always empty, because it fills the rest of the space when resized.            
            if (position == -1)
                position = this.tableLayout.RowCount - 2;
            else
            {
                ShiftRowsDown(position);
            }

            this.tableLayout.Controls.Add(control, 0, position);

            control.MouseClick += new MouseEventHandler(OnListItemClicked);
            control.MouseDoubleClick += new MouseEventHandler(OnlistItemDoubleClicked);
        }        

        void control_VisibleChanged(object sender, EventArgs e)
        {
            // When one of the panels gets hidden, have to update the the colors of the other
            // panels so that they alternate
            AlternateColorsOfEntries();

            // Shouldn't we update the minimum height of the table here, making it smaller
            // if a control is hidden?
            UpdateTableHeight();
        }

        #region Highlighting, coloring
        /// <summary>
        /// Alternates colors of entries between gray and transparent.
        /// </summary>
        protected void AlternateColorsOfEntries()
        {
            foreach (Control control in this.tableLayout.Controls)
            {
                // Only count those that are visible
                if (!control.Visible)
                    continue;
                
                Color targetColor;
                // Rows that are divisble by two are colored
                if (this.tableLayout.GetRow(control) % 2 == 0)
                    targetColor = Color.Transparent;
                else
                    targetColor=alternatingColor;

                if (control.BackColor != targetColor)
                    control.BackColor = targetColor;                
            }
        }

        public void Highlight(Control c)
        {
            if (c != null)
            {
                c.BackColor = highlightColor;
                highlightedEntry = c;
            }
        }
        public void Unhighlight(Control c)
        {
            // TODO: unhighlight one at a time
            AlternateColorsOfEntries();
            highlightedEntry = null;
        }
        #endregion

        #region Properties
        public Control HighlightedEntry
        {
            get { return highlightedEntry; }
            set { highlightedEntry = value; }
        }

        #endregion
    }
}
