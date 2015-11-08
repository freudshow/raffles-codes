using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework;

namespace DetailInfo.MaterialManage
{
    public partial class MaterialRequireQuery : Form
    {
        string Projectid=string.Empty;
        public MaterialRequireQuery()
        {
            //LoginUser = cuser;
            //Projectid = paraProjectid;
            //SubPro = ParaSubPro;
            //sub2pro = parasub2project;
            //activity = Paraactivity;
            if (Projectid == string.Empty)
            {
                //Projectid = MTypeTreeView.Str_Project;
                //SubPro = MTypeTreeView.ParentSubProjectId;
                //sub2pro = MTypeTreeView.SubProjectId;
                //activity = MTypeTreeView.ActivityId;
            }
            
            
            InitializeComponent();
        }

        
        private void MISC_PROCUREMENT_Query_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dataSet1.MM_PART_REQUIRE_VIEW”中。您可以根据需要移动或移除它。
            //this.mM_PART_REQUIRE_VIEWTableAdapter.Fill(this.dataSet1.MM_PART_REQUIRE_VIEW);
            ProjectCmbItem.ProjectCmbBind(cmb_project);
            ProjectCmbItem.ReasonCmbBind(cmb_reason);
            ProjectCmbItem.SiteCmbBind(cmb_site2);
            ProjectCmbItem.ProjectCmbBind(cmb_project);
            ProjectCmbItem.BindDiscipline(cmb_discipline2);
            BindPartNobyAct();
            
        }
        private void BindPartNobyAct()
        {
            
        }
        
        private void btn_queryERP_Click(object sender, EventArgs e)
        {
            listviewTitleBindERP();
            // string proId=cmb_project.SelectedValue.ToString();
            if (Projectid == string.Empty)
            {
                MessageBox.Show("请选择项目");
                return;
            }
            string partno=cmb_partno.Text.ToString();
            string Partname = cmb_partname.Text.ToString();
            string ReqDateFrom = dateTimePicker3.Checked == true ? dateTimePicker3.Value.ToString("yyyy-MM-dd") : string.Empty;
            string ReqDateTo = dateTimePicker4.Checked == true ? dateTimePicker4.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateFrom = dateTimePicker1.Checked == true ? dateTimePicker1.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateTo = dateTimePicker2.Checked == true ? dateTimePicker2.Value.ToString("yyyy-MM-dd") : string.Empty;
            string ReasonCode=cmb_reason.SelectedValue.ToString();
            string DesignCode=txt_designcode.Text.Trim().ToString();
            string Creator = txt_creator.Text.Trim().ToString();
            StringBuilder sb = new StringBuilder();
            if (Projectid != string.Empty) sb.Append(" AND misc.PROJECT_ID = '" + Projectid + "'");
            if (partno != string.Empty) sb.Append(" AND misc.part_no like '%" + partno + "%'");
            if (Partname != string.Empty) sb.Append(" AND IFSAPP.PURCHASE_PART_API.Get_Description(SITE, misc.PART_NO) like'%" + Partname + "%'");
            if (Creator != string.Empty) sb.Append(" AND CREATER like'%" + Creator + "%'");
            if (ReasonCode != string.Empty) sb.Append(" AND reason_code ='" + ReasonCode + "'");
            if (DesignCode != string.Empty) sb.Append(" AND design_code like'%" + DesignCode + "%'");
            if (ReqDateFrom != string.Empty) sb.Append(" and request_date>=" + Util.GetOracelDateSql(ReqDateFrom));
            if (ReqDateTo != string.Empty) sb.Append(" and request_date -interval '1' day   <=" + Util.GetOracelDateSql(ReqDateTo));
            string sqlSelect = "select PROJECT_ID,site,IFSAPP.ACTIVITY_API.Get_Description(misc.ACTIVITY_SEQ),misc.PART_NO,IFSAPP.PURCHASE_PART_API.Get_Description(SITE, misc.PART_NO),IFSAPP.INVENTORY_PART_API.Get_Unit_Meas(SITE, misc.PART_NO),REQUEST_QTY,REQUEST_DATE,P_REQUISITION_NO,IFSAPP.Purchase_Req_Util_API.Get_Line_State(P_REQUISITION_NO,P_REQ_LINE_NO,P_REQ_RELEASE_NO),INFORMATION,REASON_CODE,IFSAPP.YRS_REQUISITION_REASON_API.Get_Description(REASON_CODE),DESIGN_CODE,IFSAPP.Inventory_Part_API.Part_Exist(SITE, misc.PART_NO) from IFSAPP.PROJECT_MISC_PROCUREMENT misc left join IFSAPP.activity a on a.activity_seq = misc.activity_seq and misc.project_id = a.project_id left join IFSAPP.sub_project s on s.sub_project_id = a.sub_project_id and s.project_id = a.project_id  WHERE misc.p_requisition_no is not null ";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql;
            listviewBind(sqlSelect);
            
            
        }
        public void listviewTitleBindERP()
        {
            
        }
        public void listviewTitleBindInter()
        {
            
        }
        public void listviewBind(string sql)
        {
            this.dgv1.AutoGenerateColumns = false;
            this.dgv1.Rows.Clear();
            DataSet ds = MEOsub.QueryPartMiscProcListEPR(sql);
            DataView dv = ds.Tables[0].DefaultView;
            dgv1.DataSource = dv;
        }
        public void listviewBindInter(string sql)
        {
            this.dgv1.AutoGenerateColumns = false;
            //this.dgv1.Rows.Clear();
            DataSet ds = MEOsub.QueryPartMiscProcListEPR(sql);
            DataView dv = ds.Tables[0].DefaultView;
            dgv1.DataSource = dv;
            
        }
        private void btn_query_Click(object sender, EventArgs e)
        {
            listviewTitleBindInter();
            ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择项目", "提示");
                return;
            }
            Projectid = item.Value; 
            if (Projectid == string.Empty)
            {
                MessageBox.Show("请选择项目");
                return;
            }
            
            string meono = txt_meono.Text.Trim().ToLower();
            string partno = cmb_partno.Text.Trim().ToString().ToLower();
            string PartName = cmb_partname.Text.Trim().ToString().ToLower();
            string ReqDateFrom = dateTimePicker3.Checked==true?dateTimePicker3.Value.ToString("yyyy-MM-dd"):string.Empty;
            string ReqDateTo = dateTimePicker4.Checked == true ? dateTimePicker4.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateFrom = dateTimePicker1.Checked == true ? dateTimePicker1.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateTo = dateTimePicker2.Checked == true ? dateTimePicker2.Value.ToString("yyyy-MM-dd") : string.Empty;
            //ReasonCode Ritem = (ReasonCode)cmb_reason.SelectedValue;
            string ReasonCode = cmb_reason.SelectedValue.ToString();
            string DesignCode = txt_designcode.Text.Trim().ToString().ToLower();
            string Creator = txt_creator.Text.Trim().ToString().ToLower();
            string meostate = cmb_state.Text == null ? "" : cmb_state.Text;
            string manualno = tb_manual.Text.Trim().ToLower();
            StringBuilder sb = new StringBuilder();
            if (meostate != string.Empty && meostate != null)
            {
                sb.Append(meostate == "已审核" ? " and state='4'" : "and state='1'");
            }
            if (meono != string.Empty) sb.Append(" and MEO_ERP like '%"+meono+"%'");
            if (Projectid != string.Empty) sb.Append(" AND PROJECT_ID = '" + Projectid + "'");
            if (partno != string.Empty) sb.Append(" AND   lower(part_no) like '%" + partno + "%'");
            if (PartName != string.Empty) sb.Append(" AND  lower(part_name) like'%" + PartName + "%'");
            if (Creator != string.Empty) sb.Append(" AND  lower(CREATER) like'%" + Creator + "%'");
            if (manualno != string.Empty) sb.Append(" AND  lower(require_no) like'%" + manualno + "%'");
            if (ReasonCode != string.Empty) sb.Append(" AND REASON_CODE ='" + ReasonCode + "'");
            if (DesignCode != string.Empty) sb.Append(" AND  lower(DESIGN_CODE) like'%" + DesignCode + "%'");
            if (ReqDateFrom != string.Empty) sb.Append(" and REQUIRE_DATE>=" + Util.GetOracelDateSql(ReqDateFrom));
            if (ReqDateTo != string.Empty) sb.Append(" and REQUIRE_DATE -interval '1' day   <=" + Util.GetOracelDateSql(ReqDateTo));
            if (CreateDateFrom != string.Empty) sb.Append(" and CREATE_DATE>=" + Util.GetOracelDateSql(CreateDateFrom));
            if (CreateDateTo != string.Empty) sb.Append(" and CREATE_DATE -interval '1' day   <=" + Util.GetOracelDateSql(CreateDateTo));
           
            string sqlSelect = "select *  from plm.mmm_part_require_view  where 1=1 ";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql;
            listviewBindInter(sqlSelect);
            //XmlOper.setXML("Type", mtype);
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            //string meomanual = txt_meono.Text;
            string datestr = DateTime.Today.Date.ToString("yyyy-MM-dd");
            ExportExcel(datestr+"-MTOLIST", this.dgv1);
        }
        #region 导出excel
        private void ExportExcel(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得 sheet1

            //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    //fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }

            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
            MessageBox.Show(fileName + "的简明资料保存成功", "提示", MessageBoxButtons.OK);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv1.RowCount != 0)
            {
                string requireid = dgv1.CurrentRow.Cells["require_id"].Value.ToString();

                //FrmNew_Misc_Proc frmrequire = new FrmNew_Misc_Proc(requireid,LoginUser, Projectid, "", sub2pro, activity);
                //if (frmrequire.ShowDialog() == DialogResult.OK)
                //{
                //    //MessageBox.Show("11");
                //    //刷新数据
                //    btn_query_Click(sender, e);
                //}
                               
                
            }
        }

        

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        
        

        private void dgv1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv1.RowCount != 0)
            {
                if (e.Button == MouseButtons.Right)
                {

                    //dgv1.ClearSelection();//清空以前的选中项
                    MEO.Enabled = true;
                    MSS.Enabled = true;
                    Approve.Enabled = true;
                    Reject.Enabled = true;
                    if (dgv1.SelectedRows.Count == 1)
                    {
                        
                        //dgv1.ClearSelection();//清空以前的选中项
                        if (e.RowIndex == -1) return;
                        this.dgv1.Rows[e.RowIndex].Selected = true;
                        this.dgv1.CurrentCell = dgv1.Rows[e.RowIndex].Cells["project_id"];
                        string auditflag = dgv1.Rows[e.RowIndex].Cells["approveflag"].Value.ToString();
                        //MessageBox.Show(dgv1.Rows[e.RowIndex].Cells["part_no"].Value.ToString());
                        string issueqty = dgv1.Rows[e.RowIndex].Cells["dmssqty"].Value.ToString();
                        if (string.IsNullOrEmpty(dgv1.Rows[e.RowIndex].Cells["MEO_ERP"].Value.ToString()))
                        {
                            Approve.Enabled = false;
                            Reject.Enabled = false;
                            MSS.Enabled = false;
                        }
                        if (auditflag == "Y")
                        {
                            Approve.Enabled = false;
                        }
                        if (auditflag == "N")
                        {
                            Reject.Enabled = false;
                            MSS.Enabled = false;
                        }
                        if (issueqty != "")
                        {
                            Reject.Enabled = false;
                        }
                    }
                    else
                    {
                        //dgv1.ClearSelection();//清空以前的选中项
                        int rowcou = dgv1.RowCount;
                        MEO.Enabled = false;
                        MSS.Enabled = false;
                        StorageMSS.Enabled = false;
                        for (int i = 0; i < rowcou; i++)
                        {
                            if (dgv1.Rows[i].Selected == true)
                            {
                                string auditflag = dgv1.Rows[i].Cells["approveflag"].Value.ToString();
                                string issueqty = dgv1.Rows[i].Cells["dmssqty"].Value.ToString();
                                //MessageBox.Show(dgv1.Rows[i].Cells["part_no"].Value.ToString());
                                if (auditflag == "Y")
                                {
                                    Approve.Enabled = false;
                                }
                                if (auditflag == "N")
                                {
                                    Reject.Enabled = false;
                                    MSS.Enabled = false;
                                }
                                if ( issueqty != "")
                                {
                                    Reject.Enabled = false;
                                }
                            }

                        }
                    }
                    
                }
                


            }
            
        }

        private void MEO_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount > 0)
            {
                foreach (Form form in this.ParentForm.MdiChildren)
                {
                    if (form.Text == "材料维护页面")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                string requireid = dgv1.CurrentRow.Cells["require_id"].Value.ToString();
                string statename = dgv1.CurrentRow.Cells["state"].Value.ToString();
                string stateid = "4";
                if (statename == "待审")
                    stateid = "1";
                MaterialRequireUpdate detailform = new MaterialRequireUpdate(requireid, stateid);
                detailform.Text = "材料维护页面";
                detailform.MdiParent = this.MdiParent;
                detailform.Show();

            }
            else
            {
                return;
            }
        }

        private void MSS_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount != 0)
            {
                //string auditflag = dgv1.CurrentRow.Cells["approveflag"].Value.ToString();
                //if (auditflag == "N")
                //{
                //    MessageBox.Show("此MEO尚未审核，不能下发定额！","提示");
                //    return;
                //}
                if (MessageBox.Show("确定要做已申请的定额吗?", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    string requireid = dgv1.CurrentRow.Cells["require_id"].Value.ToString();
                    string partid = dgv1.CurrentRow.Cells["id"].Value.ToString();
                    
                }
            }
        }

        private void 审核此单据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount != 0)
            {
                
                
                if (MessageBox.Show("确定要审核选中的申请吗?", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    for (int i = 0; i < dgv1.RowCount; i++)
                    {
                        if (dgv1.Rows[i].Selected == true)
                        {
                            string requireid = dgv1.Rows[i].Cells["meolineno"].Value.ToString();
                            string partid = dgv1.Rows[i].Cells["id"].Value.ToString();
                            if (MEOmain.ApproveMEO(int.Parse(requireid), "Y") != 1)
                            {
                                MessageBox.Show("审核MEO单据失败!!", "错误提示");
                                return;
                            }
                        }
                    }
                    MessageBox.Show("审核MEO单据成功!!","操作提示");
                    //刷新数据
                    btn_query_Click(sender, e);
                    //this.Hide();
                }
            } 
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount != 0)
            {

                if (MessageBox.Show("确定要退回此申请吗?", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    
                    for (int i = 0; i < dgv1.RowCount; i++)
                    {
                        if (dgv1.Rows[i].Selected == true)
                        {
                            string requireid = dgv1.Rows[i].Cells["meolineno"].Value.ToString();
                            string partid = dgv1.Rows[i].Cells["id"].Value.ToString();
                            if (MEOmain.ApproveMEO(int.Parse(requireid), "N") != 1)
                            {
                                MessageBox.Show("退回MEO单据成功!!", "操作提示");
                                return;
                            }
                        }
                    }
                    MessageBox.Show("退回MEO单据失败!!", "错误提示");
                    //刷新数据
                    btn_query_Click(sender, e);
                    //this.Hide();
                }
            } 

        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StorageMSS_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount != 0)
            {
                //string auditflag = dgv1.CurrentRow.Cells["approveflag"].Value.ToString();
                //if (auditflag == "N")
                //{
                //    MessageBox.Show("此MEO尚未审核，不能下发定额！", "提示");
                //    return;
                //}
                if (MessageBox.Show("确定要做已申请的定额吗?", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    string requireid = dgv1.CurrentRow.Cells["require_id"].Value.ToString();
                    string partid = dgv1.CurrentRow.Cells["id"].Value.ToString();
                    //frmRationUpdate frm = new frmRationUpdate(LoginUser, Projectid, requireid, partid, activity);
                    //frm.MdiParent = this.ParentForm;
                    //frm.Show();
                    //frm.TopLevel = false;
                    //this.Owner.splitContainer1.Panel2.Controls.Add(frm);
                    //frm.Dock = DockStyle.Fill;

                    //if (frm.ShowDialog() == DialogResult.OK)
                    //{
                    //    //MessageBox.Show("11");
                    //    //刷新数据
                    //    btn_query_Click(sender, e);
                    //}

                    //this.Hide();
                }
            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_state_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_meono_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            if (dgv1.RowCount > 0)
            {
                foreach (Form form in this.ParentForm.MdiChildren)
                {
                    if (form.Text == "材料维护页面")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                string requireid = dgv1.CurrentRow.Cells["require_id"].Value.ToString();
                string statename = dgv1.CurrentRow.Cells["state"].Value.ToString();
                string stateid = "4";
                if (statename == "待审")
                    stateid = "1";
                MaterialRequireUpdate detailform = new MaterialRequireUpdate(requireid, stateid);
                detailform.Text = "材料维护页面";
                detailform.MdiParent = this.MdiParent;
                detailform.Show();

            }
            else
            {
                return;
            }
        }

        private void dgv1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.MdiParent = this.MdiParent;
            f1.Show();
        }

        


    }
}