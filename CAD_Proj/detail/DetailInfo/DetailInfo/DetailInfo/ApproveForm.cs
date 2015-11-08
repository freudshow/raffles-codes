using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class ApproveForm : Form
    {
        public ApproveForm()
        {
            InitializeComponent();
            this.toolStripStatusLabel2.Text = null;
            requestbtn.Visible = false;
            disbtn.Visible = false; 

        }

        private void ApproveForm_Load(object sender, EventArgs e)
        {
            SetStatus();
            string sqlstr = string.Empty;
            string formtext = this.Text.ToString();
            switch (formtext)
            {
                case "请求审核":
                    sqlstr = "SELECT DISTINCT PROJECTID FROM SPOOL_TAB WHERE FLAG = 'Y'";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.PIDcomboBox, sqlstr);
                    this.PIDcomboBox.SelectedIndex = 0;
                    requestbtn.Visible = true;
                    this.requestbtn.Text = "请求审核";
                    break;

                case "审核窗口":
                    sqlstr = "SELECT DISTINCT PROJECTID FROM PIPEAPPROVE_TAB WHERE ASSESOR = '"+User.cur_user+"' AND STATE = 0 AND APPROVENEEDFLAG = 'Y'";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.PIDcomboBox, sqlstr);
                    this.PIDcomboBox.SelectedIndex = 0;
                    this.requestbtn.Visible = true;  this.requestbtn.Text = "同意";
                    this.disbtn.Visible = true; disbtn.Enabled = false;
                    break;

                case "已请求审核的小票":
                    this.groupBox1.Text = string.Format("{0}", "查询条件");
                    sqlstr = "SELECT DISTINCT T.PROJECTID FROM SPFLOWLOG_TAB T WHERE T.FLOWSTATUS = 1 AND T.USERNAME = '"+User.cur_user+"'";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.PIDcomboBox, sqlstr);
                    this.PIDcomboBox.SelectedIndex = 0;
                    
                    break;

                case "已通过审核的小票":
                    this.groupBox1.Text = string.Format("{0}", "查询条件");
                    sqlstr = "SELECT DISTINCT T.PROJECTID FROM SPOOL_TAB T WHERE T.FLOWSTATUS = 2 AND T.PROJECTID IN (SELECT DISTINCT S.PROJECTID FROM SPFLOWLOG_TAB S WHERE S.USERNAME = '"+User.cur_user+"')";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.PIDcomboBox, sqlstr);
                    this.requestbtn.Visible = true; this.requestbtn.Text = "发往生产";
                    //this.PIDcomboBox.SelectedIndex = 0;
                    break;

                case "审核通过小票":
                    this.groupBox1.Text = string.Format("{0}", "查询条件");
                    sqlstr = "SELECT DISTINCT T.PROJECTID FROM PIPEAPPROVE_TAB T WHERE T.ASSESOR = '"+User.cur_user+"' AND T.STATE = 1 AND T.APPROVENEEDFLAG = 'N'";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.PIDcomboBox, sqlstr);
                    this.PIDcomboBox.SelectedIndex = 0;
                    break;

                case "审核未通过小票":
                    this.groupBox1.Text = string.Format("{0}", "查询条件");
                    sqlstr = "SELECT DISTINCT T.PROJECTID FROM SPOOL_TAB T WHERE T.FLOWSTATUS = 5 AND T.FLAG = 'Y' AND T.PROJECTID IN (SELECT DISTINCT S.PROJECTID FROM SPFLOWLOG_TAB S WHERE S.FLOWSTATUS = 5 AND S.USERNAME = '"+User.cur_user+"')";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.PIDcomboBox, sqlstr);
                    this.PIDcomboBox.SelectedIndex = 0;
                    break;

                case "审核反馈小票":
                    this.groupBox1.Text = string.Format("{0}", "查询条件");
                    this.requestbtn.Visible = true; this.requestbtn.Text = "处理";
                    sqlstr = "SELECT DISTINCT T.PROJECTID FROM SPOOL_TAB T WHERE T.FLOWSTATUS = 5 AND T.FLAG = 'Y' AND  T.PROJECTID IN (SELECT S.PROJECTID from spflowlog_tab S where S.PROJECTID in (select DISTINCT A.Projectid from spflowlog_tab A where A.username = '" + User.cur_user + "' and A.flowstatus = 1) and S.flowstatus = 5)";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.PIDcomboBox, sqlstr);
                    this.PIDcomboBox.SelectedIndex = 0;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 设置状态栏
        /// </summary>
        private void SetStatus()
        {
            int count = 0;

            if (this.tabControl1.SelectedTab.Text == "小票信息")
            {
                count = this.Appdgv.Rows.Count;
            }
            else if (this.tabControl1.SelectedTab.Text == "材料信息")
            {
                count = this.Materialdgv.Rows.Count;
            }
            else if (this.tabControl1.SelectedTab.Text == "附件信息")
            {
                count = this.Partdgv.Rows.Count;
            }

            this.toolStripStatusLabel1.Text = string.Format(" 当前总记录数：{0}个", count);
        }

        /// <summary>
        /// 小票信息选择行变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Appdgv_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectionRowCount(this.Appdgv);
        }

        /// <summary>
        /// 选区项目号获取图纸号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PIDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Appdgv.DataSource = this.Materialdgv.DataSource = this.Partdgv.DataSource = null;
            this.DRAWINGNOcomboBox.Items.Clear(); this.DRAWINGNOcomboBox.Text = null;
            string formtext = this.Text.ToString();
            string sqlstr = string.Empty;
            switch (formtext)
            {
                case "请求审核":
                    sqlstr = "SELECT DISTINCT T.DRAWINGNO FROM SPOOL_TAB T WHERE T.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND  T.FLAG = 'Y' AND T.DRAWINGNO IS NOT NULL AND T.DRAWINGNO NOT IN ( SELECT S.DRAWINGNO FROM PIPEAPPROVE_TAB S WHERE S.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "') ";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);
                    break;

                case "审核窗口":
                    sqlstr = "SELECT DISTINCT T.DRAWINGNO FROM PIPEAPPROVE_TAB T WHERE T.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND T.ASSESOR = '" + User.cur_user + "' AND T.STATE = 0 AND T.APPROVENEEDFLAG = 'Y'";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);
                    break;

                case "已请求审核的小票":
                    sqlstr = "SELECT DISTINCT T.DRAWINGNO FROM SPOOL_TAB T WHERE T.FLAG = 'Y' AND  T.FLOWSTATUS = 1 AND T.FLOWSTATUS IN ( SELECT DISTINCT S.FLOWSTATUS FROM SPFLOWLOG_TAB S WHERE S.USERNAME = '" + User.cur_user + "' AND S.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' )";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);
                    break;

                case "已通过审核的小票":
                    sqlstr = "SELECT DISTINCT T.DRAWINGNO FROM SPOOL_TAB T WHERE T.FLAG = 'Y' AND T.FLOWSTATUS = 2 AND T.DRAWINGNO IN (SELECT DISTINCT S.DRAWINGNO FROM SPFLOWLOG_TAB S WHERE S.USERNAME = '" + User.cur_user + "' AND S.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "')";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);
                    break;

                case "审核通过小票":
                    sqlstr = "SELECT DISTINCT T.DRAWINGNO FROM PIPEAPPROVE_TAB T WHERE T.ASSESOR = '"+User.cur_user+"' AND T.STATE = 1 AND T.APPROVENEEDFLAG = 'N' AND T.PROJECTID = '"+this.PIDcomboBox.SelectedItem+"'";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);
                    break;

                case "审核未通过小票":
                    sqlstr = "SELECT DISTINCT T.DRAWINGNO FROM SPOOL_TAB T WHERE T.FLOWSTATUS = 5 AND T.FLAG = 'Y' AND T.DRAWINGNO IN (SELECT DISTINCT S.DRAWINGNO FROM SPFLOWLOG_TAB S WHERE S.FLOWSTATUS = 5 AND  S.USERNAME = '"+User.cur_user+"' AND S.PROJECTID = '"+this.PIDcomboBox.SelectedItem+"')";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);
                    break;

                case "审核反馈小票":
                    sqlstr = "SELECT DISTINCT T.DRAWINGNO FROM SPOOL_TAB T WHERE T.FLOWSTATUS = 5 AND T.FLAG = 'Y' AND T.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND  T.DRAWINGNO IN (SELECT S.DRAWINGNO from spflowlog_tab S where S.FLOWSTATUS in (select DISTINCT A.FLOWSTATUS from spflowlog_tab A where A.username = '"+User.cur_user+"' and A.flowstatus = 1) )";
                    DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);
                    break;
            }
        }

        /// <summary>
        /// 选取图纸号获取小票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DRAWINGNOcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            object drawnostr = this.DRAWINGNOcomboBox.SelectedItem;
            if (drawnostr !=null )
            {
                this.disbtn.Enabled = true;
            }
            if (this.tabControl1.SelectedTab.Text == "小票信息")
            {
                string sqlstr = "SELECT T.PROJECTID  项目号, T.SPOOLNAME  小票号, T.SYSTEMID  系统号, T.SYSTEMNAME  系统名, T.PIPEGRADE  管路等级, T.SURFACETREATMENT  表面处理, T.WORKINGPRESSURE  工作压力, T.PRESSURETESTFIELD  压力测试场所, T.PIPECHECKFIELD  校管场所 , T.SPOOLWEIGHT  \"小票重量(kg)\", T.PAINTCOLOR  油漆颜色, T.CABINTYPE  舱室种类, T.REVISION  小票版本, T.SPOOLMODIFYTYPE  小票修改种类,T.ELBOWTYPE  弯头形式, T.WELDTYPE  点焊件, T.DRAWINGNO  图号, T.BLOCKNO  分段号, T.MODIFYDRAWINGNO  修改通知单号, T.REMARK  备注,  T.LOGNAME  录入人, T.LOGDATE  录入日期, T.LINENAME  线号 FROM SPOOL_TAB T WHERE T.FLAG = 'Y' AND T.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND T.DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "' ";
                GetData(sqlstr, this.Appdgv);
            }

            else if (this.tabControl1.SelectedTab.Text == "材料信息")
            {
                string sqlstr = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from spoolmaterial_tab t where t.materialname like '%管%' and t.flag = 'Y' and t.projectid = '" + this.PIDcomboBox.SelectedItem + "' and t.spoolname in (select s.spoolname from spool_tab s where s.drawingno = '" + this.DRAWINGNOcomboBox.SelectedItem + "' and s.projectid = '" + this.PIDcomboBox.SelectedItem + "' and s.flag = 'Y') ";
                GetData(sqlstr, this.Materialdgv);
            }

            else if (this.tabControl1.SelectedTab.Text == "附件信息")
            {
                string sqlstr = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from spoolmaterial_tab t where t.materialname not like '%管%' and t.flag = 'Y' and t.projectid = '" + this.PIDcomboBox.SelectedItem + "' and t.spoolname in (select s.spoolname from spool_tab s where s.drawingno = '" + this.DRAWINGNOcomboBox.SelectedItem + "' and s.projectid = '" + this.PIDcomboBox.SelectedItem + "' and s.flag = 'Y') ";
                GetData(sqlstr, this.Partdgv);
            }

        }

        /// <summary>
        /// 单元格格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Appdgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            SpoolCellFormat.FormatCell(Appdgv);
        }

        /// <summary>
        /// 标签跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Text == "小票信息")
            {
                if (this.PIDcomboBox.Text.ToString() == string.Empty)
                {
                    return;
                }
                else
                {
                    if (this.DRAWINGNOcomboBox.Text.ToString() == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        string sqlstr = "SELECT T.PROJECTID  项目号, T.SPOOLNAME  小票号, T.SYSTEMID  系统号, T.SYSTEMNAME  系统名, T.PIPEGRADE  管路等级, T.SURFACETREATMENT  表面处理, T.WORKINGPRESSURE  工作压力, T.PRESSURETESTFIELD  压力测试场所, T.PIPECHECKFIELD  校管场所 , T.SPOOLWEIGHT  \"小票重量(kg)\", T.PAINTCOLOR  油漆颜色, T.CABINTYPE  舱室种类, T.REVISION  小票版本, T.SPOOLMODIFYTYPE  小票修改种类,T.ELBOWTYPE  弯头形式, T.WELDTYPE  点焊件, T.DRAWINGNO  图号, T.BLOCKNO  分段号, T.MODIFYDRAWINGNO  修改通知单号, T.REMARK  备注,  T.LOGNAME  录入人, T.LOGDATE  录入日期, T.LINENAME  线号 FROM SPOOL_TAB T WHERE T.FLAG = 'Y' AND T.PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND T.DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "' ";
                        GetInfo(sqlstr, this.Appdgv);
                    }
                }
            }

            else if (this.tabControl1.SelectedTab.Text == "材料信息")
            {
                string sqlstr = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from spoolmaterial_tab t where t.materialname like '%管%' and t.flag = 'Y' and t.projectid = '" + this.PIDcomboBox.SelectedItem + "' and t.spoolname in (select s.spoolname from spool_tab s where s.drawingno = '" + this.DRAWINGNOcomboBox.SelectedItem + "' and s.projectid = '" + this.PIDcomboBox.SelectedItem + "' and s.flag = 'Y') ";
                GetInfo(sqlstr, this.Materialdgv);
            }

            else if (this.tabControl1.SelectedTab.Text == "附件信息")
            {
                string sqlstr = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from spoolmaterial_tab t where t.materialname not like '%管%' and t.flag = 'Y' and t.projectid = '" + this.PIDcomboBox.SelectedItem + "' and t.spoolname in (select s.spoolname from spool_tab s where s.drawingno = '" + this.DRAWINGNOcomboBox.SelectedItem + "' and s.projectid = '" + this.PIDcomboBox.SelectedItem + "' and s.flag = 'Y') ";
                GetInfo(sqlstr, this.Partdgv);
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dgv"></param>
        private void GetInfo(string sql, DataGridView dgv)
        {
            DataSet ds = new DataSet();
            User.DataBaseConnect(sql, ds);
            dgv.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            SetStatus();
        }

        private void GetData(string sql, DataGridView dgv)
        {

            if (this.PIDcomboBox.SelectedItem.ToString() == string.Empty)
            {
                return;
            }
            else
            {
                if (this.DRAWINGNOcomboBox.SelectedItem.ToString() == string.Empty)
                {
                    return;
                }
                else
                {
                    GetInfo(sql, dgv);
                }
            }

        }

        /// <summary>
        /// 材料信息选择行变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Materialdgv_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectionRowCount(this.Materialdgv);
        }

        /// <summary>
        /// 附件信息选择行变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Partdgv_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectionRowCount(this.Partdgv);
        }

        /// <summary>
        /// 获取选择行数
        /// </summary>
        /// <param name="dgv"></param>
        private void GetSelectionRowCount(DataGridView dgv )
        {
            int count = dgv.SelectedRows.Count;
            this.toolStripStatusLabel2.Text = string.Format("当前选中{0}行", count);
        }

        /// <summary>
        /// 请求审核或审核（按图纸号）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void requestbtn_Click(object sender, EventArgs e)
        {
            object pid = this.PIDcomboBox.SelectedItem;
            object drawno = this.DRAWINGNOcomboBox.SelectedItem;
            string btntext = this.requestbtn.Text.ToString();
            switch (btntext)
            {
                case "请求审核":
                    if (pid == null || drawno == null)
                    {
                        return;
                    }
                    else
                    {
                        string sql = "select count(*) from projectapprove where PROJECTID = '" + pid.ToString() + "'";
                        object count = User.GetScalar1(sql, DataAccess.OIDSConnStr);
                        if (Convert.ToInt16(count) == 0)
                        {
                            MessageBox.Show("请与系统数据维护人员联系添加项目审核人！");
                            return;
                        }
                        else
                        {
                            AssessorForm asform = new AssessorForm();
                            asform.pid = this.PIDcomboBox.SelectedItem.ToString();
                            asform.ShowDialog();
                            if (asform.DialogResult == DialogResult.OK)
                            {
                                string[] person = asform.personstr.Split(new char[] { ',' });
                                int num = person.Length - 1;
                                if (num < 0)
                                {
                                    MessageBox.Show("请与系统维护人员联系添加项目审核人！");
                                    return;
                                }
                                string[] assesor = new string[num];

                                for (int n = 0; n < num; n++)
                                {
                                    assesor[n] = person[n];
                                }

                                char[] flag = new char[num];

                                flag[0] = 'Y';

                                for (int m = 1; m < num; m++)
                                {
                                    flag[m] = 'N';
                                }

                                for (int k = 1; k <= num; k++)
                                {
                                    DBConnection.InsertPipeApproveTab(pid.ToString(), drawno.ToString(), k, assesor[k - 1], 0, flag[k - 1]);
                                    if (k == num)
                                    {
                                        DBConnection.UpdateSpoolTabWithDrawingNo((int)FlowState.待审, pid.ToString(), drawno.ToString());
                                        DBConnection.InsertApproveIntoFlowLog(User.cur_user, (int)FlowState.待审, pid.ToString(), drawno.ToString());
                                        ClearControls();
                                        MessageBox.Show("等待审核通过");
                                        return;
                                    }
                                }

                            }
                        }
                    }
                    break;

                case "同意":

                    if (pid == null || drawno == null)
                    {
                        return;
                    }
                    else
                    {
                        string sql1 = "UPDATE PIPEAPPROVE_TAB SET STATE = 1,  APPROVENEEDFLAG = 'N' WHERE  INDEX_ID =(SELECT INDEX_ID FROM PIPEAPPROVE_TAB WHERE ASSESOR ='" + User.cur_user + "' AND APPROVENEEDFLAG = 'Y' AND PROJECTID = '" + this.PIDcomboBox.SelectedItem + "'  AND DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "') AND PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "'";
                        User.UpdateCon(sql1, DataAccess.OIDSConnStr);
                        string sql2 = "UPDATE PIPEAPPROVE_TAB SET   APPROVENEEDFLAG = 'Y' WHERE STATE = 0 AND INDEX_ID =((SELECT INDEX_ID FROM PIPEAPPROVE_TAB WHERE ASSESOR ='" + User.cur_user + "' AND APPROVENEEDFLAG = 'N' AND PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "')+1) AND PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "' ";
                        User.UpdateCon(sql2, DataAccess.OIDSConnStr);

                        DataSet ds = new DataSet();
                        string sqlcount = "SELECT COUNT(*) FROM PROJECTAPPROVE WHERE PROJECTID = '" + this.PIDcomboBox.SelectedItem + "'";
                        User.DataBaseConnect(sqlcount, ds);
                        int countvalue = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        ds.Dispose();

                        DataSet ds1 = new DataSet();
                        string sql3 = "SELECT DISTINCT INDEX_ID FROM PIPEAPPROVE_TAB WHERE ASSESOR = '" + User.cur_user + "' AND PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "' ";
                        User.DataBaseConnect(sql3, ds1);
                        if (Convert.ToInt32(ds1.Tables[0].Rows[0][0]) == countvalue)
                        {
                            DBConnection.UpdateSpoolTabWithDrawingNo((int)FlowState.审核通过, pid.ToString(), drawno.ToString());

                            DBConnection.InsertApproveIntoFlowLog(User.cur_user, (int)FlowState.审核通过, pid.ToString(), drawno.ToString());

                            ClearControls();

                        }

                        else
                        {
                            DBConnection.InsertApproveIntoFlowLog(User.cur_user, (int)FlowState.待审, pid.ToString(), drawno.ToString());
                            ClearControls();
                        }
                        ds1.Dispose();
                    }

                    break;

                case "发往生产":
                    if (pid == null || drawno == null)
                    {
                        return;
                    }
                    else
                    {
                        DBConnection.UpdateSpoolTabWithDrawingNo((int)FlowState.待分配, pid.ToString(), drawno.ToString());
                        DBConnection.InsertApproveIntoFlowLog(User.cur_user, (int)FlowState.待分配, pid.ToString(), drawno.ToString());
                        ClearControls();
                    }
                    break;

                case "处理":
                    if (pid == null || drawno == null)
                    {
                        return;
                    }
                    else
                    {
                        DBConnection.UpdateSpoolTabWithDrawingNo((int)FlowState.处理审核反馈, pid.ToString(), drawno.ToString());
                        DBConnection.InsertApproveIntoFlowLog(User.cur_user, (int)FlowState.处理审核反馈, pid.ToString(), drawno.ToString());
                        ClearControls();
                    }
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// 审核不通过反馈给设计员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disbtn_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("确定要反馈吗？", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            if (result == DialogResult.OK)
            {
                Add_Remark remarkform = new Add_Remark();
                remarkform.ShowDialog();
                if (remarkform.DialogResult == DialogResult.OK)
                {
                    string marktext = remarkform.GetRemark();
                    string sqlstr = "UPDATE PIPEAPPROVE_TAB SET APPROVENEEDFLAG = 'N', STATE = 1 WHERE PROJECTID = '" + this.PIDcomboBox.SelectedItem + "' AND DRAWINGNO = '" + this.DRAWINGNOcomboBox.SelectedItem + "'";
                    User.UpdateCon(sqlstr, DataAccess.OIDSConnStr);
                    DBConnection.UpdateSpoolTabWithDrawingNoRemark((int)FlowState.审核反馈, marktext, this.PIDcomboBox.SelectedItem.ToString(), this.DRAWINGNOcomboBox.SelectedItem.ToString());
                    DBConnection.InsertApproveFeedBackFlowLog(User.cur_user, (int)FlowState.审核反馈, marktext, this.PIDcomboBox.SelectedItem.ToString(), this.DRAWINGNOcomboBox.SelectedItem.ToString());
                    ClearControls();
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 激活当前窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApproveForm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
        }

        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearControls()
        {
            this.DRAWINGNOcomboBox.Items.Remove(this.DRAWINGNOcomboBox.SelectedItem);
            this.Appdgv.DataSource = this.Materialdgv.DataSource = this.Partdgv.DataSource = null;
            SetStatus();
        }




    }

   
}