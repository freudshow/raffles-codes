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
    public partial class SpoolReportFrm : Form
    {
        public SpoolReportFrm()
        {
            InitializeComponent();
        }
        
        private void SpoolReportFrm_Load(object sender, EventArgs e)
        {
            string sqlstr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT PROJECTID FROM SP_SPOOL_TAB WHERE FLAG = 'Y')   ORDER BY NAME";
            DataSet ds = new DataSet();
            User.DataBaseConnect(sqlstr, ds);
            TreeNode tn;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i][0].ToString();
                treeView1.Nodes["PROJECTS"].Nodes.Add(tn);
                tn.ImageIndex = 3;
            }
            this.treeView1.Nodes[0].Expand();
            ds.Dispose();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked == true)
            {
                this.toolStripStatusLabel6.Visible = false;
                this.toolStripStatusLabel12.Visible = false;
                this.dataGridView1.DataSource = null;
                string projectStr = this.treeView1.SelectedNode.Text.ToString();
                if (projectStr == "项目")
                {
                    return;
                }
                WorkShopStatusFlow.GetWorkShopReport("SP_WORKSHOPREPORT", projectStr, 0, this.dataGridView1);
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked == true)
            {
                this.toolStripStatusLabel6.Visible = true;
                this.toolStripStatusLabel12.Visible = true;
                this.dataGridView1.DataSource = null;
                string projectStr = this.treeView1.SelectedNode.Text.ToString();
                if (projectStr == "项目")
                {
                    return;
                }
                WorkShopStatusFlow.GetWorkShopReport("SP_WORKSHOPREPORT", projectStr, 1, this.dataGridView1);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton3.Checked == true)
            {
                this.toolStripStatusLabel6.Visible = true;
                this.toolStripStatusLabel12.Visible = true;
                this.dataGridView1.DataSource = null;
                string projectStr = this.treeView1.SelectedNode.Text.ToString();
                if (projectStr == "项目")
                {
                    return;
                }
                WorkShopStatusFlow.GetWorkShopReport("SP_WORKSHOPREPORT", projectStr, 2, this.dataGridView1);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.radioButton1.Checked = this.radioButton2.Checked = this.radioButton3.Checked = false;
            string projectStr = this.treeView1.SelectedNode.Text.ToString();
            if (projectStr != "项目")
            {
                this.label1.Text = this.treeView1.SelectedNode.Text.ToString();
            }
            
        }

        private void SpoolReportFrm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }

        private void SpoolReportFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[1].Enabled = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count != 0)
            {
                double yuzhicount = 0;
                double hanjiecount = 0;
                double baoyancount = 0;
                double jieshoucount = 0;
                double peisongcount = 0;
                double anzhuangcount = 0;

                double realyuzhicount = 0;
                double realhanjiecount = 0;
                double realbaoyancount = 0;
                double realjieshoucount = 0;
                double realpeisongcount = 0;
                double realanzhuangcount = 0;
                
                if (this.radioButton1.Checked == true)
                {
                    
                    foreach (DataGridViewRow dr in this.dataGridView1.SelectedRows)
                    {
                        yuzhicount += Convert.ToDouble(dr.Cells["有效预制数量"].Value.ToString());
                        hanjiecount += Convert.ToDouble(dr.Cells["有效焊接数量"].Value.ToString());
                        baoyancount += Convert.ToDouble(dr.Cells["有效报验数量"].Value.ToString());
                        jieshoucount += Convert.ToDouble(dr.Cells["有效接收数量"].Value.ToString());
                        peisongcount += Convert.ToDouble(dr.Cells["有效配送数量"].Value.ToString());

                        realyuzhicount += Convert.ToDouble(dr.Cells["实际预制数量"].Value.ToString());
                        realhanjiecount += Convert.ToDouble(dr.Cells["实际焊接数量"].Value.ToString());
                        realbaoyancount += Convert.ToDouble(dr.Cells["实际报验数量"].Value.ToString());
                        realjieshoucount += Convert.ToDouble(dr.Cells["实际接收数量"].Value.ToString());
                        realpeisongcount += Convert.ToDouble(dr.Cells["实际配送数量"].Value.ToString());
                    }
                    this.toolStripStatusLabel1.Text = "有效预制：" + yuzhicount;
                    this.toolStripStatusLabel2.Text = "有效焊接：" + hanjiecount;
                    this.toolStripStatusLabel3.Text = "有效报验：" + baoyancount;
                    this.toolStripStatusLabel4.Text = "有效接收：" + jieshoucount;
                    this.toolStripStatusLabel5.Text = "有效配送：" + peisongcount;

                    this.toolStripStatusLabel7.Text = "实际预制：" + realyuzhicount;
                    this.toolStripStatusLabel8.Text = "实际焊接：" + realhanjiecount;
                    this.toolStripStatusLabel9.Text = "实际报验：" + realbaoyancount;
                    this.toolStripStatusLabel10.Text = "实际接收：" + realjieshoucount;
                    this.toolStripStatusLabel11.Text = "实际配送：" + realpeisongcount;
                }
                if (this.radioButton2.Checked == true)
                {
                    foreach (DataGridViewRow dr in this.dataGridView1.SelectedRows)
                    {
                        yuzhicount += Convert.ToDouble(dr.Cells["有效预制数量"].Value.ToString());
                        hanjiecount += Convert.ToDouble(dr.Cells["有效焊接数量"].Value.ToString());
                        baoyancount += Convert.ToDouble(dr.Cells["有效报验数量"].Value.ToString());
                        jieshoucount += Convert.ToDouble(dr.Cells["有效接收数量"].Value.ToString());
                        peisongcount += Convert.ToDouble(dr.Cells["有效配送数量"].Value.ToString());
                        anzhuangcount += Convert.ToDouble(dr.Cells["有效安装数量"].Value.ToString());

                        realyuzhicount += Convert.ToDouble(dr.Cells["实际预制数量"].Value.ToString());
                        realhanjiecount += Convert.ToDouble(dr.Cells["实际焊接数量"].Value.ToString());
                        realbaoyancount += Convert.ToDouble(dr.Cells["实际报验数量"].Value.ToString());
                        realjieshoucount += Convert.ToDouble(dr.Cells["实际接收数量"].Value.ToString());
                        realpeisongcount += Convert.ToDouble(dr.Cells["实际配送数量"].Value.ToString());
                        realanzhuangcount += Convert.ToDouble(dr.Cells["实际安装数量"].Value.ToString());
                    }
                    this.toolStripStatusLabel1.Text = "有效预制：" + yuzhicount;
                    this.toolStripStatusLabel2.Text = "有效焊接：" + hanjiecount;
                    this.toolStripStatusLabel3.Text = "有效报验：" + baoyancount;
                    this.toolStripStatusLabel4.Text = "有效接收：" + jieshoucount;
                    this.toolStripStatusLabel5.Text = "有效配送：" + peisongcount;
                    this.toolStripStatusLabel6.Text = "有效安装：" + anzhuangcount;

                    this.toolStripStatusLabel7.Text = "实际预制：" + realyuzhicount;
                    this.toolStripStatusLabel8.Text = "实际焊接：" + realhanjiecount;
                    this.toolStripStatusLabel9.Text = "实际报验：" + realbaoyancount;
                    this.toolStripStatusLabel10.Text = "实际接收：" + realjieshoucount;
                    this.toolStripStatusLabel11.Text = "实际配送：" + realpeisongcount;
                    this.toolStripStatusLabel12.Text = "实际安装：" + realanzhuangcount;
                }
                if (this.radioButton3.Checked == true)
                {
                    foreach (DataGridViewRow dr in this.dataGridView1.SelectedRows)
                    {
                        yuzhicount += Convert.ToDouble(dr.Cells["有效预制数量"].Value.ToString());
                        hanjiecount += Convert.ToDouble(dr.Cells["有效焊接数量"].Value.ToString());
                        baoyancount += Convert.ToDouble(dr.Cells["有效报验数量"].Value.ToString());
                        jieshoucount += Convert.ToDouble(dr.Cells["有效接收数量"].Value.ToString());
                        peisongcount += Convert.ToDouble(dr.Cells["有效配送数量"].Value.ToString());
                        anzhuangcount += Convert.ToDouble(dr.Cells["有效安装数量"].Value.ToString());

                        realyuzhicount += Convert.ToDouble(dr.Cells["实际预制数量"].Value.ToString());
                        realhanjiecount += Convert.ToDouble(dr.Cells["实际焊接数量"].Value.ToString());
                        realbaoyancount += Convert.ToDouble(dr.Cells["实际报验数量"].Value.ToString());
                        realjieshoucount += Convert.ToDouble(dr.Cells["实际接收数量"].Value.ToString());
                        realpeisongcount += Convert.ToDouble(dr.Cells["实际配送数量"].Value.ToString());
                        realanzhuangcount += Convert.ToDouble(dr.Cells["实际安装数量"].Value.ToString());
                    }
                    this.toolStripStatusLabel1.Text = "有效预制：" + yuzhicount;
                    this.toolStripStatusLabel2.Text = "有效焊接：" + hanjiecount;
                    this.toolStripStatusLabel3.Text = "有效报验：" + baoyancount;
                    this.toolStripStatusLabel4.Text = "有效接收：" + jieshoucount;
                    this.toolStripStatusLabel5.Text = "有效配送：" + peisongcount;
                    this.toolStripStatusLabel6.Text = "有效安装：" + anzhuangcount;

                    this.toolStripStatusLabel7.Text = "实际预制：" + realyuzhicount;
                    this.toolStripStatusLabel8.Text = "实际焊接：" + realhanjiecount;
                    this.toolStripStatusLabel9.Text = "实际报验：" + realbaoyancount;
                    this.toolStripStatusLabel10.Text = "实际接收：" + realjieshoucount;
                    this.toolStripStatusLabel11.Text = "实际配送：" + realpeisongcount;
                    this.toolStripStatusLabel12.Text = "实际安装：" + realanzhuangcount;
                }
            }
            else
            {
                return;
            }
        }



    }
}
