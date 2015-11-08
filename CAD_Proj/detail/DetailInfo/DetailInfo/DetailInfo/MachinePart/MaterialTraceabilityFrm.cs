using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace DetailInfo
{
    public partial class MaterialTraceabilityFrm : Form
    {
        public MaterialTraceabilityFrm()
        {
            InitializeComponent();
        }
        private Point point;
        string sqlstr = string.Empty;
        private void MaterialTraceabilityFrm_Load(object sender, EventArgs e)
        {

            sqlstr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT PROJECTID FROM SP_SPOOL_TAB WHERE FLAG = 'Y')   ORDER BY NAME";
            DetailInfo.Application_Code.FillComboBox.FillComb(this.comboBox1, sqlstr);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            string projectstr = this.comboBox1.Text.ToString();
            TreeNode tn = new TreeNode();
            sqlstr = "select distinct BLOCKNO from sp_spool_tab where flag = 'Y' and drawingno in (select drawing_no from  PROJECT_DRAWING_TAB where ISSUED_TIME is not null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + projectstr + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N')";
            FillTreeViewFunction.FillTree(this.treeView1, sqlstr);
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                sqlstr = "select distinct SYSTEMID from sp_spool_tab where projectid = '" + projectstr + "' and blockno = '" + node.Text.ToString() + "' and flag = 'Y'";
                FillTreeViewFunction.FillTreeView(node, sqlstr);
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked == true)
            {
                TreeNode node = this.treeView1.GetNodeAt(point);
                if (point.X < node.Bounds.Left || point.X > node.Bounds.Right)
                {
                    return;
                }
                else
                {
                    if (this.treeView1.SelectedNode.Level == 1)
                    {
                        string projectstr = this.comboBox1.Text.ToString();
                        string blockstr = this.treeView1.SelectedNode.Parent.Text.ToString();
                        string systemidstr = this.treeView1.SelectedNode.Text.ToString();
                        WorkShopClass.TraceabilityIII("SP_TraceabilityIII", projectstr, blockstr, systemidstr, this.dataGridView1);

                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if (this.radioButton2.Checked == true)
            {
                MessageBox.Show("-----开发中！----");
                return;
            }
            else
            {
                MessageBox.Show("请选择管路类别！","信息提示！",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
        }

        private void MaterialTraceabilityFrm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }

        private void MaterialTraceabilityFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }
    }
}
