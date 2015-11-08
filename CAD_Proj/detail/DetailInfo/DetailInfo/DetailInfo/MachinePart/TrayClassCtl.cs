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
    public partial class TrayClassCtl : UserControl
    {
        public TrayClassCtl()
        {
            InitializeComponent();
        }

        private void 添加托盘号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTrayNoFrm addtrayno = new AddTrayNoFrm();
            if (addtrayno.ShowDialog() == DialogResult.OK)
            {
                string trayStr = addtrayno.textBox1.Text.ToUpper().ToString();
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
                    WorkShopStatusFlow.AddTrayORClass("SP_ADDTRAYNO", projectidStr, drawingStr, spoolStr, trayStr);
                    this.dataGridView1.Rows[index].Cells["托盘号"].Value = trayStr;
                }
                selectedlist.Clear();
                MessageBox.Show("--------完成--------");
            }
        }

        private void 添加分类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClassFrm addclass = new AddClassFrm();
            if (addclass.ShowDialog() == DialogResult.OK)
            {
                string classStr = addclass.textBox1.Text.ToUpper().ToString();
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
                    WorkShopStatusFlow.AddTrayORClass("SP_ADDCLASSIFICATION", projectidStr, drawingStr, spoolStr, classStr);
                    this.dataGridView1.Rows[index].Cells["分类"].Value = classStr;
                }
                selectedlist.Clear();
                MessageBox.Show("--------完成--------");
            }
        }

        private void 导出ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.ExportToTxt(this.dataGridView1, this.progressBar1);
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
                    this.contextMenuStrip1.Items[0].Visible = this.contextMenuStrip1.Items[1].Visible = this.contextMenuStrip1.Items[2].Visible = this.contextMenuStrip1.Items[3].Visible = this.contextMenuStrip1.Items[4].Visible = true;
                }
                else
                {
                    this.contextMenuStrip1.Items[0].Visible = this.contextMenuStrip1.Items[1].Visible = this.contextMenuStrip1.Items[2].Visible = this.contextMenuStrip1.Items[3].Visible = this.contextMenuStrip1.Items[4].Visible = false;
                }
            }
        }
    }
}
