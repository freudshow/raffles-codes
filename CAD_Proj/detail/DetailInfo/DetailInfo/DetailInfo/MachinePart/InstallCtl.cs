using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DetailInfo
{
    public partial class InstallCtl : UserControl
    {
        public InstallCtl()
        {
            InitializeComponent();
        }

        private void 安装完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string frmtext = this.Parent.Text.ToString();
            DateTime datetime = System.DateTime.Now;
            ArrayList selectedlist = new ArrayList();
            foreach (DataGridViewRow dr in this.dataGridView1.Rows)
            {
                if (dr.Selected == true)
                {
                    selectedlist.Add(dr.Index);
                }
            }
            for (int i = 0; i < selectedlist.Count; i++)
            {
                int index = int.Parse(selectedlist[i].ToString());
                string projectidStr = this.dataGridView1.Rows[index].Cells["项目号"].Value.ToString();
                string drawingStr = this.dataGridView1.Rows[index].Cells["图纸号"].Value.ToString();
                string spoolStr = this.dataGridView1.Rows[index].Cells["小票号"].Value.ToString();
                WorkShopStatusFlow.UpdateStatus("SP_UPDATESTATUS", projectidStr, drawingStr, spoolStr, datetime, frmtext);
                this.dataGridView1.Rows[index].Cells["安装日期"].Value = datetime;
            }
            selectedlist.Clear();
            MessageBox.Show("--------完成--------");
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }
            else
            {
                int rowcount = this.dataGridView1.SelectedRows.Count;
                if (rowcount > 0)
                {
                    this.contextMenuStrip1.Items[0].Visible = this.contextMenuStrip1.Items[1].Visible = this.contextMenuStrip1.Items[2].Visible = true;
                }
                else
                {
                    this.contextMenuStrip1.Items[0].Visible = this.contextMenuStrip1.Items[1].Visible = this.contextMenuStrip1.Items[2].Visible = false;
                }
            }
        }

        private void 导出ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.ExportToTxt(this.dataGridView1, this.progressBar1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            double weight = 0;
            foreach (DataGridViewRow dr in this.dataGridView1.SelectedRows)
            {
                weight += Convert.ToDouble(dr.Cells["重量"].Value.ToString());
            }

            ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel2"].Text = string.Format(" 当前所选总重量：{0}kg", weight);
        }


    }
}
