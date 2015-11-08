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
    public partial class WShopManHour : Form
    {
        public WShopManHour()
        {
            InitializeComponent();
        }

        DrawingControl Dcontrol;
        SPControl SPcontrol;
        string sqlStr = string.Empty;
        string projectStr = string.Empty;
        string drawingStr = string.Empty;
        private void WShopManHour_Load(object sender, EventArgs e)
        {
            if (this.Text == "压力试验工时定额")
            {
                SPcontrol = new SPControl();
                SPcontrol.Dock = DockStyle.Fill;
                this.splitContainer1.Panel1.Controls.Add(SPcontrol);
            }
            else
            {
                Dcontrol = new DrawingControl();
                Dcontrol.Dock = DockStyle.Fill;
                this.splitContainer1.Panel1.Controls.Add(Dcontrol);
            }
            this.toolStripStatusLabel1.Text = string.Format(" ");
            this.toolStripStatusLabel2.Text = string.Format(" ");
        }

        /// <summary>
        /// 激活窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WShopManHour_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = true;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WShopManHour_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }

        private void WorkingTimeDgv_SelectionChanged(object sender, EventArgs e)
        {
            int count = this.WorkingTimeDgv.SelectedRows.Count;
            this.toolStripStatusLabel2.Text = string.Format("当前选中{0}行", count);
        }
    }
}
