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
    public partial class ModifyStatisticsFrm : Form
    {
        public ModifyStatisticsFrm()
        {
            InitializeComponent();
        }
        string sqlstr = string.Empty;
        string projectstr = string.Empty;
        string drawingstr = string.Empty;
        Point point;
        private void ModifyStatisticsFrm_Load(object sender, EventArgs e)
        {
            sqlstr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT PROJECTID FROM SP_SPOOL_TAB WHERE FLAG = 'Y')   ORDER BY NAME";
            DataSet ds = new DataSet();
            User.DataBaseConnect(sqlstr, ds);
            TreeNode tn;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i][0].ToString();
                treeView1.Nodes["PROJECTS"].Nodes.Add(tn);
                sqlstr = "select distinct drawingno from sp_spool_tab where projectid = '" + tn.Text + "' and flag = 'Y' and drawingno in (select drawing_no from  PROJECT_DRAWING_TAB where ISSUED_TIME is not null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + tn.Text.ToString() + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N')";
                FillTreeViewFunction.FillTreeView(tn, sqlstr);
            }
            this.treeView1.Nodes[0].Expand();
            ds.Dispose();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.GetNodeAt(point);
            if (point.X < node.Bounds.Left || point.X > node.Bounds.Right)
            {
                return;
            }
            else
            {
                projectstr = this.treeView1.SelectedNode.Parent.Text.ToString();
                drawingstr = this.treeView1.SelectedNode.Text.ToString();
                int count = 0;
                if (this.radioButton1.Checked == true)
                {
                    if (this.treeView1.SelectedNode.Level == 2)
                    {
                        WorkShopClass.GetModifyStatistics("SP_MODIFYSTATISTICS", projectstr, drawingstr, 0, this.dataGridView1);
                        count = this.dataGridView1.Rows.Count;
                        this.toolStripStatusLabel1.Text = "当前记录数共：" + count;
                    }
                }
                if (this.radioButton2.Checked == true)
                {
                    WorkShopClass.GetModifyStatistics("SP_MODIFYSTATISTICS", projectstr, drawingstr, 1, this.dataGridView1);
                    count = this.dataGridView1.Rows.Count;
                    this.toolStripStatusLabel1.Text = "当前记录数共：" + count;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.toolStripStatusLabel1.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.toolStripStatusLabel1.Text = "";
        }

        private void ModifyStatisticsFrm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }

        private void ModifyStatisticsFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }
    }
}
