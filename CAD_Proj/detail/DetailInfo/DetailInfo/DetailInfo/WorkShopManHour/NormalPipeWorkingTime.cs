using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class NormalPipeWorkingTime : Form
    {
        public NormalPipeWorkingTime()
        {
            InitializeComponent();
        }

        private void NormalPipeWorkingTime_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = string.Format(" ");
            this.toolStripStatusLabel2.Text = string.Format(" ");
        }

        private void NormalPipeWorkingTime_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = true;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }

        private void NormalPipeWorkingTime_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int count = this.dataGridView1.SelectedRows.Count;
            this.toolStripStatusLabel2.Text = string.Format("当前选中{0}行", count);
        }
    }
}
