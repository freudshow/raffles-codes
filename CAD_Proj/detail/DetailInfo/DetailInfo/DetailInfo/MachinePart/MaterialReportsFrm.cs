using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Collections;

namespace DetailInfo
{
    public partial class MaterialReportsFrm : Form
    {
        public MaterialReportsFrm()
        {
            InitializeComponent();
        }
        string projectStr = string.Empty;
        string blockStr = string.Empty;
        string sqlStr = string.Empty;
        private Point point;
        private void MaterialReportsFrm_Load(object sender, EventArgs e)
        {
            string sqlstr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82) and NAME IN (SELECT DISTINCT S.PROJECTID　FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y')   ORDER BY NAME";
            DataSet ds = new DataSet();
            User.DataBaseConnect(sqlstr, ds);
            TreeNode tn;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i][0].ToString();
                treeView1.Nodes["PROJECTS"].Nodes.Add(tn);
            }
            this.treeView1.Nodes[0].Expand();
            ds.Dispose();

        }


        //private DataSet GetDs(string blockstr, int flag)
        //{
            //DataSet ds = new DataSet();
            //List<DetailInfo.Categery.Acceemp> acceemplist = DetailInfo.Categery.Acceemp.Finding(blockstr,0);
            //ds.Tables.Add(new DataTable());
            //ds.Tables[0].Columns.Add(new DataColumn("附件名称"));
            //ds.Tables[0].Columns.Add(new DataColumn("规格"));
            //ds.Tables[0].Columns.Add(new DataColumn("数量"));
            //ds.Tables[0].Columns.Add(new DataColumn("重量"));
            //for (int i = 0; i < acceemplist.Count; i++)
            //{
            //    if (ds.Tables[0].Rows.Count == 0)
            //    {
            //        if (acceemplist[i].DoubleScrewBolt == "B")
            //            ds.Tables[0].Rows.Add("螺栓", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            //        else
            //            ds.Tables[0].Rows.Add("双头螺柱", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            //        ds.Tables[0].Rows.Add("螺母", acceemplist[i].NutStandard, (acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].NutWeight.ToString());
            //        ds.Tables[0].Rows.Add("垫片", acceemplist[i].GasketStandard, acceemplist[i].TotalNum.ToString(), string.Empty);
            //    }
            //    else
            //    {
            //        string flag1 = "N";
            //        string flag2 = "N";
            //        string flag3 = "N";
            //        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            //        {
            //            if (acceemplist[i].DoubleScrewBolt == "B" && ds.Tables[0].Rows[j][0].ToString() == "螺栓" && ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].BoltStandard)
            //            {
            //                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString();
            //                flag1 = "Y";
            //            }
            //            if (acceemplist[i].DoubleScrewBolt == "DB" && ds.Tables[0].Rows[j][0].ToString() == "双头螺柱" && ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].BoltStandard)
            //            {
            //                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString();
            //                flag1 = "Y";
            //            }
            //            if (ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].NutStandard)
            //            {
            //                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString();
            //                flag2 = "Y";
            //            }
            //            if (ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].GasketStandard)
            //            {
            //                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].TotalNum).ToString();
            //                flag3 = "Y";
            //            }
            //        }
            //        if (flag1 == "N")
            //        {
            //            if (acceemplist[i].DoubleScrewBolt == "B")
            //            {
            //                ds.Tables[0].Rows.Add("螺栓", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            //            }
            //            else
            //            {
            //                ds.Tables[0].Rows.Add("双头螺柱", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            //            }
            //        }
            //        if (flag2 == "N")
            //            ds.Tables[0].Rows.Add("螺母", acceemplist[i].NutStandard, (acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].NutWeight.ToString());
            //        if (flag3 == "N")
            //            ds.Tables[0].Rows.Add("垫片", acceemplist[i].GasketStandard, acceemplist[i].TotalNum.ToString(), string.Empty);
            //    }
            //}
            //return ds;
        //}

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ClearControlData();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ClearControlData();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ClearControlData();
        }
        /// <summary>
        /// 清空控件数据源
        /// </summary>
        private void ClearControlData()
        {
            this.dataGridView1.DataSource = null;
            this.checkedListBox1.Items.Clear();
            this.labelColor1.Text = "";
        }

        private void MaterialReportsFrm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }

        private void MaterialReportsFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }

        /// <summary>
        /// 获取附件材料数据集
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private DataSet GetAccessoryDS(string project)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand("SP_GETACCESSORY", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters["project_in"].Direction = System.Data.ParameterDirection.Input;
            //cmd.Parameters.Add("block_in", OracleType.VarChar).Value = block;
            //cmd.Parameters["block_in"].Direction = System.Data.ParameterDirection.Input;
            //cmd.Parameters.Add("flag_in", OracleType.Number).Value = flag;
            //cmd.Parameters["flag_in"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
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
                this.dataGridView1.DataSource = null;
                checkedListBox1.Items.Clear();
                projectStr = this.treeView1.SelectedNode.Text.ToString();
                this.labelColor1.Text = projectStr;
                if (this.radioButton1.Checked == true)
                {
                    WorkShopClass.GetMaterialReports("SP_GETMATERIALREPORTS", projectStr, blockStr, this.dataGridView1);
                    FillCheckListBox();
                }
                else if (this.radioButton2.Checked == true)
                {
                    DataSet MyDS = GetAccessoryDS(projectStr);
                    MyDS.Tables[0].Columns[0].ColumnName = "附件名称";
                    MyDS.Tables[0].Columns[1].ColumnName = "规格";
                    MyDS.Tables[0].Columns[2].ColumnName = "数量";
                    MyDS.Tables[0].Columns[3].ColumnName = "重量";
                    this.dataGridView1.DataSource = MyDS.Tables[0].DefaultView;
                    MyDS.Dispose();
                    FillCheckListBox();
                }
                else if (this.radioButton3.Checked == true)
                {
                    DataSet MyDS = Getxyz(projectStr);
                    this.dataGridView1.DataSource = MyDS.Tables[0];
                    MyDS.Dispose();
                    FillCheckListBox();
                }
                else
                {
                    MessageBox.Show("请选择要输出的表的类型！", "信息提示!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
        }

        private DataSet Getxyz(string project)
        {
            OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
            string sql = "";
            sql = "select t.projectid 项目号,t.drawingno 图纸号,t.blockno 分段,sum(t.spoolweight) 重量,round(sum(t.XPOS)/sum(t.SPOOLWEIGHT),2) X,round(sum(t.YPOS)/sum(t.SPOOLWEIGHT),2) Y,round(sum(t.ZPOS)/sum(t.SPOOLWEIGHT),2) Z from sp_spool_tab t where t.projectid='" + project + "' and t.flag='Y' group by t.projectid,t.drawingno,t.blockno";

           // OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            OracleDataAdapter sqlAd = new OracleDataAdapter(sql, sqlCon);
           // sqlAd.SelectCommand = sqlCmd;
            DataSet ds = new DataSet();
            sqlAd.Fill(ds, "sqls");
            return ds;
        }

        /// <summary>
        /// 根据鼠标位置得到当前活动节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X,e.Y);
        }

        /// <summary>
        ///获取指定项目所包含的分段
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private DataSet GetProjectBlockNo(string project)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand("SP_GetProjectBlockNo", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters["project_in"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 填充checklistbox
        /// </summary>
        private void FillCheckListBox()
        {
            DataSet BlockDS = GetProjectBlockNo(projectStr);
            for (int i = 0; i < BlockDS.Tables[0].Rows.Count; i++)
            {
                blockStr = BlockDS.Tables[0].Rows[i][0].ToString();
                this.checkedListBox1.Items.Add(blockStr);
            }
            BlockDS.Dispose();
        }
        /// <summary>
        /// 按分段查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            projectStr = this.treeView1.SelectedNode.Text.ToString();
            this.dataGridView1.DataSource = null;
            int selectionCount = this.checkedListBox1.CheckedItems.Count;
            if (selectionCount == 0)
            {
                return;
            }
            string whereSql = " where 1=1 and (";
            if (this.radioButton1.Checked == true)
            {

                foreach (object item in this.checkedListBox1.CheckedItems)
                {
                    blockStr = item.ToString();
                    whereSql += "BLOCKNO = '" + blockStr + "' OR ";
                }
                whereSql = whereSql.Remove(whereSql.Length - 3, 3);
                whereSql = whereSql + ")" + " and PROJECTID = '" + projectStr + "'";
                sqlStr = "select PROJECTID 项目号,DRAWINGNO 图纸号,BLOCKNO 分段号,MATERIAL 材质,NORM 规格,LENGTH 长度,AMOUNT 数量,SPOOLWEIGHT 重量 from FUN_PIPEMATERIAL_LSH " + whereSql + " order by MATERIAL,BLOCKNO";
                DataSet ds = new DataSet();
                User.DataBaseConnect(sqlStr, ds);
                this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ds.Dispose();
            }

            else if (this.radioButton2.Checked == true)
            {
                foreach (object item in this.checkedListBox1.CheckedItems)
                {
                    blockStr = item.ToString();
                    whereSql += "BLOCKNAME = '" + blockStr + "' OR ";
                }
                whereSql = whereSql.Remove(whereSql.Length - 3, 3);
                whereSql = whereSql + ")" + " and PROJECTID = '" + projectStr + "'";
                sqlStr = @"with summary as
     (SELECT NUTSTANDARD,
             BOLTSTANDARD,
             GASKETSTANDARD,
             DOUBLESCREWBOLT,
             
             SUM(NUTNUMBER * TOTALNUM) as N_NUM,
             SUM(BOLTNUMBER * TOTALNUM) as B_NUM,
             SUM(TOTALNUM) as G_COUNT,
             SUM(NUTWEIGHT * TOTALNUM) N_WEIGHT,
             SUM(BOLTWEIGHT * TOTALNUM) B_WEIGHT
      
        FROM SP_ACCEEMP
      
        "+whereSql+@"
            
         and flag = 'Y'
      
       GROUP BY BOLTSTANDARD,
                
                GASKETSTANDARD,
                
                NUTSTANDARD,
                
                DOUBLESCREWBOLT)
    
    SELECT '螺栓' ACCENAME,
           to_char(BOLTSTANDARD) STANDERD,
           sum(b_num) QTY,
           sum(b_weight) WEIGHT
      from summary
     where DOUBLESCREWBOLT = 'B'
     group by BOLTSTANDARD
    
    union
    
    select '双头螺柱' ACCENAME,
           to_char(BOLTSTANDARD) STANDERD,
           sum(b_num) QTY,
           sum(b_weight) WEIGHT
      from summary
     where DOUBLESCREWBOLT = 'DB'
     group by BOLTSTANDARD
    
    union
    
    SELECT '螺母' ACCENAME,
           to_char(NUTSTANDARD) STANDERD,
           sum(N_NUM) QTY,
           sum(n_weight) WEIGHT
      from summary
     group by NUTSTANDARD
    
    union
    
    SELECT '垫片' ACCENAME,
           to_char(GASKETSTANDARD) STANDERD,
           sum(G_COUNT) QTY,
           null WEIGHT
      from summary
     group by GASKETSTANDARD";
                DataSet ds = new DataSet();
                User.DataBaseConnect(sqlStr, ds);
                ds.Tables[0].Columns[0].ColumnName = "附件名称";
                ds.Tables[0].Columns[1].ColumnName = "规格";
                ds.Tables[0].Columns[2].ColumnName = "数量";
                ds.Tables[0].Columns[3].ColumnName = "重量";
                this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ds.Dispose();
            }
            else if (this.radioButton3.Checked == true)
            {
                foreach (object item in this.checkedListBox1.CheckedItems)
                {
                    blockStr = item.ToString();
                    whereSql += "BLOCKNO = '" + blockStr + "' OR ";
                }
                whereSql = whereSql.Remove(whereSql.Length - 3, 3);
                whereSql = whereSql + ")" + " and PROJECTID = '" + projectStr + "'";
                sqlStr = @"select t.projectid 项目号,t.drawingno 图纸号,t.blockno 分段,sum(t.spoolweight) 重量,round(sum(t.XPOS)/sum(t.SPOOLWEIGHT),2) X,round(sum(t.YPOS)/sum(t.SPOOLWEIGHT),2) Y,round(sum(t.ZPOS)/sum(t.SPOOLWEIGHT),2) Z from sp_spool_tab t " + whereSql + @" and t.flag='Y'  group by t.projectid,t.drawingno,t.blockno";
                DataSet ds = new DataSet();
                User.DataBaseConnect(sqlStr, ds);
                this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ds.Dispose();
            }

        }


    }
}
