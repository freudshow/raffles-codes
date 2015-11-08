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
    public partial class ECDrawingReportFrm : Form
    {
        public ECDrawingReportFrm()
        {
            InitializeComponent();
        }

        Point point;
        private void ECDrawingReportFrm_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sqlstr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT S.PROJECTID　FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y')   ORDER BY NAME";
            User.DataBaseConnect(sqlstr, ds);
            TreeNode tn;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i][0].ToString();
                treeView1.Nodes["PROJECTS"].Nodes.Add(tn);
                tn.ImageIndex = 3;
            }
            ds.Dispose();
            this.treeView1.Nodes[0].Expand();

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
                string projectstr = this.treeView1.SelectedNode.Text.ToString();
                if (projectstr == "项目")
                {
                    return;
                }
                string sqlstr = "select DRAWING,CC,WEIGHT,CH_NAME,CHANQIAN,CHANQIANWEIGHT,CHANHOU,CHANHOUWEIGHT,ASSEMBLYCOUNT,INSTALLCOUNT from ECDRAWINGREPORTS_VIEW where DRAWING in (select drawingno from sp_spool_tab where projectid = '"+projectstr+"' and flag = 'Y')";
                DataSet ds = new DataSet();
                User.DataBaseConnect(sqlstr, ds);
                this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ds.Dispose();
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void ECDrawingReportFrm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }

        private void ECDrawingReportFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[1].Enabled = false;
        }

    }
}
