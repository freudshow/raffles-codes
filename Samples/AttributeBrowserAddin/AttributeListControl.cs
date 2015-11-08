//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Aveva.Presentation.AttributeBrowserAddin
{
    public partial class AttributeListControl : UserControl
    {
        public AttributeListControl()
        {
            InitializeComponent();
        }

        public void AddAttribute(string name, string value)
        {
            ListViewItem newItem = new ListViewItem(name);
            newItem.SubItems.Add(value);
            this.attributeList.Items.Add(newItem);
        }

        public void Clear()
        {
            this.attributeList.Items.Clear();
        }

        private void attributeList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
