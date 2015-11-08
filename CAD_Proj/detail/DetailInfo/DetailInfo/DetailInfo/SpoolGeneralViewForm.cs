using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Data.OracleClient;

namespace DetailInfo
{
    public partial class SpoolGeneralViewForm : Form
    {
        public ContextMenuStrip displayMenueStrip = null;
        public System.DateTime currentTime = System.DateTime.Now;
        public SpoolGeneralViewForm()
        {
            InitializeComponent();
            displayMenueStrip = this.DisplaycontextMenuStrip;
            //DisplaycontextMenuStrip.Enabled = false;
            for (int i = 0; i < DisplaycontextMenuStrip.Items.Count; i++)
            {
                DisplaycontextMenuStrip.Items[i].Visible = false;
            }
        }
        
        /// <summary>
        /// 关闭窗口时search按钮禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpoolGeneralViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }

        #region 设计部门操作
        /// <summary>
        /// 所有小票相关的材料，连接件，加工，焊接以及相对坐标信息展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int count = this.OverViewdgv.SelectedRows.Count;
            if (count > 0)
            {
                for (int i = 0; i < OverViewdgv.Rows.Count; i++)
                {
                    if (OverViewdgv.Rows[i].Selected == true)
                    {
                        string spoolname = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                        sb.Append(spoolname + ",");
                    }
                }

                DetailsForm detailform = new DetailsForm();
                detailform.Text = "小票详细信息";
                detailform.MdiParent = this.MdiParent;
                detailform.spoolstr = sb.ToString();
                detailform.Show();

            }
            else
            {
                return;
            }
            
            
        }

        /// <summary>
        /// 设计人员在转到管加工之前提请三级审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequireApproveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList req_arry = new ArrayList(); ArrayList st_arry = new ArrayList(); ArrayList pidlist = new ArrayList();
            //if (OverViewdgv.RowCount==0)
            //{
            //    return;
            //}
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.RowCount; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string sname = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                st_arry.Add(status);
            //                foreach (string item in st_arry)
            //                {
            //                    if (item != "初始")
            //                    {
            //                        MessageBox.Show("不能重复审核");
            //                        return;
            //                    }
            //                }
            //            }
            //        }

            //        AssessorForm aform = new AssessorForm();
            //        for (int k = 0; k < OverViewdgv.Rows.Count; k++)
            //        {
            //            if (OverViewdgv.Rows[k].Selected == true)
            //            {
            //                string pidstr = OverViewdgv.Rows[k].Cells["项目号"].Value.ToString();
            //                pidlist.Add(pidstr);
            //            }
            //        }
            //        if (pidlist.Count == 1)
            //        {
            //            aform.pid = pidlist[0].ToString();
            //            string sql1 = "select count(PROJECTID) from projectapprove where PROJECTID = '" + aform.pid + "'";
            //            object count1 = User.GetScalar1(sql1, DataAccess.OIDSConnStr);
            //            if (Convert.ToInt16(count1) == 0)
            //            {
            //                MessageBox.Show("请与系统数据维护人员联系添加项目审核人！");
            //                return;
            //            }
            //        }
            //        else if (pidlist.Count > 1)
            //        {
            //            for (int h = 0; h < pidlist.Count - 1; h++)
            //            {
            //                if (pidlist[h].ToString() == pidlist[h + 1].ToString())
            //                {
            //                    aform.pid = pidlist[0].ToString();
            //                    string sql2 = "select count(PROJECTID) from projectapprove where PROJECTID = '" + aform.pid + "'";
            //                    object count2 = User.GetScalar1(sql2, DataAccess.OIDSConnStr);
            //                    if (Convert.ToInt16(count2) == 0)
            //                    {
            //                        MessageBox.Show("请与系统数据维护人员联系添加项目审核人！");
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    MessageBox.Show("请确定所选项属同一项目");
            //                    return;
            //                }
            //            }
            //        }

            //        aform.ShowDialog();
            //        try
            //        {
            //            if (aform.DialogResult == DialogResult.OK)
            //            {
            //                string[] person = aform.personstr.Split(new char[] { ',' });
            //                int num = person.Length - 1;
            //                if (num < 0)
            //                {
            //                    MessageBox.Show("请与系统维护人员联系添加项目审核人！");
            //                    return;
            //                }
            //                string[] assesor = new string[num];
            //                for (int n = 0; n < num; n++)
            //                {
            //                    assesor[n] = person[n];
            //                }

            //                char[] flag = new char[num];
            //                flag[0] = 'Y';
            //                for (int m = 1; m < num; m++)
            //                {
            //                    flag[m] = 'N';
            //                }

            //                foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //                {
            //                    if (dr.Selected == true)
            //                    {
            //                        int id = Convert.ToInt32(dr.Index);
            //                        req_arry.Add(id);
            //                    }
            //                }
            //                if (req_arry.Count != 0)
            //                {
            //                    for (int j = 0; j < req_arry.Count; j++)
            //                    {
            //                        int index = Convert.ToInt32(req_arry[j]);
            //                        OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "待审";
            //                        string prid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                        string namestr = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString().Trim();

            //                        DBConnection.UpDateState((int)FlowState.待审, namestr, prid, "Y");

            //                        for (int k = 1; k <= num; k++)
            //                        {
            //                            //string app_sql = "INSERT INTO SPOOL_APPROVE_TAB (SPOOLNAME, INDEX_ID, ASSESOR, STATE, APPROVENEEDFLAG) VALUES ('" + namestr + "', " + k + ", '" + assesor[k - 1] + "', 0, '" + flag[k - 1] + "')";
            //                            //User.UpdateCon(app_sql, DataAccess.OIDSConnStr);
            //                            DBConnection.InsertSpoolApproveTab(namestr, k, assesor[k - 1], 0, flag[k - 1]);
            //                        }

            //                        DBConnection.InsertFlowLog(namestr, User.cur_user, (int)FlowState.待审, prid);

            //                        if (j == req_arry.Count - 1)
            //                        {
            //                            MessageBox.Show("等待审核通过！");
            //                            return;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
        }

        /// <summary>
        /// 审核通过后由设计人员转发到管加工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToMachineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList tran_array = new ArrayList();
            //ArrayList status_arrray = new ArrayList();
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.Rows.Count; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                status_arrray.Add(status);
            //            }
            //        }
            //        foreach (string item in status_arrray)
            //        {
            //            if (item != "审核通过")
            //            {
            //                MessageBox.Show("请确认所有项通过审核");
            //                return;
            //            }

            //        }
            //        DialogResult result;
            //        result = MessageBox.Show("确定转到管加工？", "转管加工", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //            {
            //                if (dr.Selected == true)
            //                {
            //                    int id = Convert.ToInt32(dr.Index);
            //                    tran_array.Add(id);
            //                }
            //            }
            //            if (tran_array.Count != 0)
            //            {
            //                for (int j = 0; j < tran_array.Count; j++)
            //                {
            //                    int index = Convert.ToInt32(tran_array[j]);
            //                    string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                    string pidname = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                    OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "待分配";

            //                    DBConnection.UpDateState((int)FlowState.待分配, name, pidname, "Y");

            //                    DBConnection.InsertFlowLog(name, User.cur_user, (int)FlowState.待分配, pidname);

            //                    if (j == tran_array.Count - 1)
            //                    {
            //                        MessageBox.Show("数据记录成功！");
            //                    }
            //                }

            //            }
            //        }
            //        if (result == DialogResult.Cancel)
            //        {
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
        }

        /// <summary>
        /// 导出管路材料信息的水晶报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ExportPipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.SPLPIPEReprot pipreport = new DetailInfo.Report.SPLPIPEReprot();
            pipreport.MdiParent = MDIForm.pMainWin;
            pipreport.Show();
        }

        /// <summary>
        /// 导出管路附件报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportMaterialtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.MaterAtt partreport = new DetailInfo.Report.MaterAtt();
            partreport.MdiParent = MDIForm.pMainWin;
            partreport.Show();
        }

        #endregion

        
        #region 管加工部门操作
        /// <summary>
        /// 由管加工负责人分配任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void AllocateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList status_array = new ArrayList();
            //ArrayList allocation_list = new ArrayList();
            //string charger = string.Empty;
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int k = 0; k < OverViewdgv.RowCount; k++)
            //        {
            //            if (OverViewdgv.Rows[k].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[k].Cells["流程状态标识"].Value.ToString();
            //                status_array.Add(status);
            //            }
            //            foreach (string item in status_array)
            //            {
            //                if (item != "待分配")
            //                {
            //                    MessageBox.Show("请确认所有项已处于待分配状态！");
            //                    return;
            //                }
            //            }

            //        }
            //        //WorkersInfo wform = new WorkersInfo();
            //        //wform.ShowDialog();
            //        //Allocation af = new Allocation();
            //        //af.ShowDialog();
            //        AssignTask atform = new AssignTask();
            //        atform.ShowDialog();
            //        try
            //        {
            //            if (atform.DialogResult == DialogResult.OK)
            //            {
            //                //object obj = wform.GetChargerName();
            //                string objstr = atform.Personnames.ToString();
            //                //string charger = af.GetChargerName();
            //                //if (obj == null)
            //                if (objstr == string.Empty)
            //                {
            //                    MessageBox.Show("责任人不能为空，请重新分配");
            //                    return;
            //                }
            //                else
            //                {
            //                    charger = objstr.ToString();
            //                }
            //                foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //                {
            //                    if (dr.Selected == true)
            //                    {
            //                        int id = Convert.ToInt32(dr.Index);
            //                        allocation_list.Add(id);
            //                    }
            //                }
            //                if (allocation_list.Count != 0)
            //                {
            //                    for (int m = 0; m < allocation_list.Count; m++)
            //                    {
            //                        int index = Convert.ToInt32(allocation_list[m]);
            //                        string pid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                        string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                        OverViewdgv.Rows[index].Cells["负责人"].Value = charger;
            //                        OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "加工中";

            //                        OverViewdgv.Rows[index].Cells["任务分配日期"].Value = currentTime.Date;

            //                        DBConnection.InsertTaskCarrier(charger, currentTime.Date, (int)FlowState.加工中, name, pid, "Y");
            //                        DBConnection.InsertFlowLog(name, User.cur_user, (int)FlowState.加工中, pid);

            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有供分配的数据");
            //    return;
            //}
        }

        /// <summary>
        /// 加工完成后转到质检部门待检
        /// </summary>
        public string QCremark = "";
        private void ToQCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList qclist = new ArrayList();
            ArrayList status_array = new ArrayList();
            if (OverViewdgv.RowCount != 0)
            {
                int rowcount = this.OverViewdgv.SelectedRows.Count;
                if (rowcount > 0)
                {
                    for (int j = 0; j < OverViewdgv.RowCount; j++)
                    {
                        if (OverViewdgv.Rows[j].Selected == true)
                        {
                            string status = OverViewdgv.Rows[j].Cells["流程状态标识"].Value.ToString();
                            status_array.Add(status);
                        }
                        foreach (string item in status_array)
                        {
                            if (item != "加工中")
                            {
                                MessageBox.Show("请确认所有项已加工完成！");
                                return;
                            }
                        }
                    }
                    AddQCRemark addQCR = new AddQCRemark();
                    addQCR.ShowDialog();
                    try
                    {
                        if (addQCR.DialogResult == DialogResult.OK)
                        {
                            QCremark = addQCR.GetQCRemark();
                            foreach (DataGridViewRow dr in OverViewdgv.Rows)
                            {
                                if (dr.Selected == true)
                                {
                                    int id = Convert.ToInt32(dr.Index);
                                    qclist.Add(id);
                                }
                            }
                            if (qclist.Count != 0)
                            {
                                for (int i = 0; i < qclist.Count; i++)
                                {
                                    int index = Convert.ToInt32(qclist[i]);
                                    string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
                                    string proid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
                                    OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "待验";

                                    DBConnection.UpDateState((int)FlowState.待验, name, proid, "Y");
                                    DBConnection.InsertFlowLog(name, User.cur_user, (int)FlowState.待验, proid);
                                }

                            }
                            if (qclist.Count == 0)
                            {
                                MessageBox.Show("请选择要转到质检的小票！");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("没有要转到生产的数据");
                return;
            }
        }

        /// <summary>
        /// 管加工把有问题的小票反馈给设计人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FeedBackToDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList feedback = new ArrayList(); ArrayList select_feedback = new ArrayList(); ArrayList status_array = new ArrayList();
            //if (OverViewdgv.RowCount == 0)
            //{
            //    return;
            //}
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.RowCount; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                feedback.Add(i);
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                status_array.Add(status);
            //            }
            //            foreach (string item in status_array)
            //            {
            //                if (item != "加工中")
            //                {
            //                    MessageBox.Show("所选项不能返回到设计！");
            //                    return;
            //                }
            //            }

            //        }
            //        if (feedback.Count != 0)
            //        {
            //            DialogResult result;
            //            result = MessageBox.Show("确定反馈问题给设计？", "问题反馈", MessageBoxButtons.OKCancel);
            //            if (result == DialogResult.OK)
            //            {
            //                Add_Remark FeedBackRemark = new Add_Remark();
            //                FeedBackRemark.ShowDialog();
            //                try
            //                {
            //                    if (FeedBackRemark.DialogResult == DialogResult.OK)
            //                    {
            //                        foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //                        {
            //                            if (dr.Selected == true)
            //                            {
            //                                int id = Convert.ToInt32(dr.Index);
            //                                select_feedback.Add(id);
            //                            }
            //                        }
            //                        if (select_feedback.Count != 0)
            //                        {
            //                            for (int j = 0; j < select_feedback.Count; j++)
            //                            {
            //                                int index = Convert.ToInt32(select_feedback[j]);

            //                                string remark = FeedBackRemark.GetRemark();
            //                                string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                                string prid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                                OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "反馈设计";

            //                                DBConnection.InsertFlowLogWithRemark(name, User.cur_user, (int)FlowState.反馈设计, remark, prid);

            //                                DBConnection.UpdateSpoolTabWithRemark((int)FlowState.反馈设计, remark, name, prid, "Y");
            //                            }
            //                        }
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }

            //            }
            //            if (result == DialogResult.Cancel)
            //            {
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
        }
        #endregion

        #region 质监部门操作
        /// <summary>
        /// 质监部门检验通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassQCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList status_array = new ArrayList();
            //ArrayList selectlist = new ArrayList();
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.RowCount; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                status_array.Add(status);
            //            }
            //            foreach (string item in status_array)
            //            {
            //                if (item != "待验")
            //                {
            //                    MessageBox.Show("请确认所有项为待验状态！");
            //                    return;
            //                }
            //            }
            //        }

            //        DialogResult result;
            //        result = MessageBox.Show("确定质检通过？", "质检通过", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //            {

            //                if (dr.Selected == true)
            //                {
            //                    if (dr.Cells["锁定状态"].Value.ToString() == "锁定数据")
            //                    {
            //                        MessageBox.Show("所选项被锁定暂不能操作", "警告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                        return;
            //                    }
            //                    int id = Convert.ToInt32(dr.Index);
            //                    selectlist.Add(id);
            //                }
            //            }
            //            if (selectlist.Count != 0)
            //            {
            //                for (int j = 0; j < selectlist.Count; j++)
            //                {
            //                    int index = Convert.ToInt32(selectlist[j]);

            //                    string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                    string prid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                    OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "检验通过待安装";

            //                    DBConnection.InsertFlowLog(name, User.cur_user, (int)FlowState.检验通过待安装, prid);
            //                    DBConnection.UpDateState((int)FlowState.检验通过待安装, name, prid, "Y");
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("暂时没有待检小票");
            //    return;
            //}

        }

        /// <summary>
        /// 质监部门反馈给管加工的问题小票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FeedBackToMachineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList status_array = new ArrayList();
            //ArrayList select_list = new ArrayList();
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.RowCount; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                status_array.Add(status);
            //            }
            //            foreach (string item in status_array)
            //            {
            //                if (item != "待验")
            //                {
            //                    MessageBox.Show("请确认所有项为待验状态！");
            //                    return;
            //                }
            //            }
            //        }

            //        DialogResult result;
            //        result = MessageBox.Show("确定反馈问题给加工？", "问题反馈", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            Add_Remark qcremark = new Add_Remark();
            //            qcremark.ShowDialog();
            //            try
            //            {
            //                if (qcremark.DialogResult == DialogResult.OK)
            //                {
            //                    foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //                    {
            //                        if (dr.Selected == true)
            //                        {
            //                            int id = Convert.ToInt32(dr.Index);
            //                            select_list.Add(id);
            //                        }
            //                    }
            //                    if (select_list.Count != 0)
            //                    {
            //                        for (int k = 0; k < select_list.Count; k++)
            //                        {
            //                            int index = Convert.ToInt32(select_list[k]);

            //                            string remark = qcremark.GetRemark();
            //                            string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                            string prid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                            OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "不合格";

            //                            DBConnection.InsertFlowLogWithRemark(name, User.cur_user, (int)FlowState.不合格, remark, prid);

            //                            DBConnection.UpdateSpoolTabWithRemark((int)FlowState.不合格, remark, name, prid, "Y");
            //                        }

            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("暂时没有待检小票！");
            //    return;
            //}
        }
        #endregion

        #region 生产部门操作
        /// <summary>
        /// 开始安装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList selectlist = new ArrayList();
            //ArrayList prolist = new ArrayList();
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.Rows.Count; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                selectlist.Add(status);
            //            }
            //            foreach (string item in selectlist)
            //            {
            //                if (item != "检验通过待安装")
            //                {
            //                    MessageBox.Show("请确认所选项为待安装状态！");
            //                    return;
            //                }
            //            }
            //        }

            //        DialogResult result;
            //        result = MessageBox.Show("确定实施安装？", "实施安装", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //        if (result == DialogResult.OK)
            //        {
            //            foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //            {
            //                if (dr.Selected == true)
            //                {
            //                    int id = Convert.ToInt32(dr.Index);
            //                    prolist.Add(id);
            //                }
            //            }
            //            if (prolist.Count != 0)
            //            {
            //                for (int j = 0; j < prolist.Count; j++)
            //                {
            //                    int index = Convert.ToInt32(prolist[j]);
            //                    string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                    string proid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                    OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "安装中";

            //                    DBConnection.UpDateState((int)FlowState.安装中, name, proid, "Y");
            //                    DBConnection.InsertFlowLog(name, User.cur_user, (int)FlowState.安装中, proid);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("暂时没有查询到需要的数据！");
            //    return;
            //}
        }

        /// <summary>
        /// 安装完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteInstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList selectlist = new ArrayList();
            //ArrayList prolist = new ArrayList();
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.RowCount; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                selectlist.Add(status);
            //            }
            //            foreach (string item in selectlist)
            //            {
            //                if (item != "安装中")
            //                {
            //                    MessageBox.Show("请确认所选项处于待安装状态！");
            //                    return;
            //                }
            //            }
            //        }

            //        DialogResult result;
            //        result = MessageBox.Show("确定安装完毕？", "安装完毕", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //            {
            //                if (dr.Selected == true)
            //                {
            //                    int id = Convert.ToInt32(dr.Index);
            //                    prolist.Add(id);
            //                }
            //            }
            //            if (prolist.Count != 0)
            //            {
            //                for (int j = 0; j < prolist.Count; j++)
            //                {
            //                    int index = Convert.ToInt32(prolist[j]);
            //                    string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                    string proid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                    OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "待调试";

            //                    DBConnection.UpDateState((int)FlowState.待调试, name, proid, "Y");
            //                    DBConnection.InsertFlowLog(name, User.cur_user, (int)FlowState.待调试, proid);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("暂时没有查询到需要的数据！");
            //    return;
            //}
        }
        #endregion 

        #region 调试部门操作
        private void CompleteModulatetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList selectlist = new ArrayList();
            //ArrayList prolist = new ArrayList();
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.RowCount; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                selectlist.Add(status);
            //            }
            //            foreach (string item in selectlist)
            //            {
            //                if (item != "待调试")
            //                {
            //                    MessageBox.Show("请确认所选项处于待调试状态！");
            //                    return;
            //                }
            //            }
            //        }

            //        DialogResult result;
            //        result = MessageBox.Show("确定调试完成？", "调试完成", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //            {
            //                if (dr.Selected == true)
            //                {
            //                    int id = Convert.ToInt32(dr.Index);
            //                    prolist.Add(id);
            //                }
            //            }
            //            if (prolist.Count != 0)
            //            {
            //                for (int j = 0; j < prolist.Count; j++)
            //                {
            //                    int index = Convert.ToInt32(prolist[j]);
            //                    string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                    string proid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                    OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "调试完成";

            //                    DBConnection.UpDateState((int)FlowState.调试完成, name, proid, "Y");
            //                    DBConnection.InsertFlowLog(name, User.cur_user, (int)FlowState.调试完成, proid);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("当前没有选中任何行！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("暂时没有查询到需要的数据！");
            //    return;
            //}
        }

        private void FeedBackToInstalltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList select_list = new ArrayList();
            //ArrayList prolist = new ArrayList();
            //if (OverViewdgv.RowCount != 0)
            //{
            //    int rowcount = this.OverViewdgv.SelectedRows.Count;
            //    if (rowcount > 0)
            //    {
            //        for (int i = 0; i < OverViewdgv.RowCount; i++)
            //        {
            //            if (OverViewdgv.Rows[i].Selected == true)
            //            {
            //                string status = OverViewdgv.Rows[i].Cells["流程状态标识"].Value.ToString();
            //                prolist.Add(status);
            //            }
            //            foreach (string item in prolist)
            //            {
            //                if (item != "待调试")
            //                {
            //                    MessageBox.Show("请确认所选项处于待调试状态！");
            //                    return;
            //                }
            //            }
            //        }
            //        DialogResult result;
            //        result = MessageBox.Show("确定反馈问题给安装？", "问题反馈", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            Add_Remark qcremark = new Add_Remark();
            //            qcremark.ShowDialog();
            //            try
            //            {
            //                if (qcremark.DialogResult == DialogResult.OK)
            //                {
            //                    foreach (DataGridViewRow dr in OverViewdgv.Rows)
            //                    {
            //                        if (dr.Selected == true)
            //                        {
            //                            int id = Convert.ToInt32(dr.Index);
            //                            select_list.Add(id);
            //                        }
            //                    }
            //                    if (select_list.Count != 0)
            //                    {
            //                        for (int i = 0; i < select_list.Count; i++)
            //                        {
            //                            int index = Convert.ToInt32(select_list[i]);
            //                            string remark = qcremark.GetRemark();
            //                            string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            //                            string prid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            //                            OverViewdgv.Rows[index].Cells["流程状态标识"].Value = "调试失败";
            //                            //string sql1 = "INSERT INTO SPFLOWLOG_TAB (SPOOLNAME,USERNAME,FLOWSTATUS, REMARK,PROJECTID) VALUES ( '" + name + "', '" + User.cur_user + "', (SELECT ID FROM SPFLOWSTATUS_TAB WHERE NAME = '调试失败'), '" + remark + "','" + prid + "')";
            //                            //User.UpdateCon(sql1, DataAccess.OIDSConnStr);
            //                            DBConnection.InsertFlowLogWithRemark(name, User.cur_user, (int)FlowState.调试失败, remark, prid);

            //                            //string sql2 = "UPDATE SPOOL_tAB SET FLOWSTATUS = (SELECT ID FROM SPFLOWSTATUS_TAB WHERE NAME = '调试失败'), FLOWSTATUSREMARK = '" + remark + "' WHERE FLAG = 'Y' AND SPOOLNAME = '" + name + "' AND PROJECTID = '"+prid+"'";
            //                            //User.UpdateCon(sql2, DataAccess.OIDSConnStr);
            //                            DBConnection.UpdateSpoolTabWithRemark((int)FlowState.调试失败, remark, name, prid, "Y");
            //                        }
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("当前没有选中任何行！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("暂时没有调试小票！");
            //    return;
            //}
            
        }

        #endregion


        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAttachmenttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < OverViewdgv.Rows.Count; i++)
                {
                    if (OverViewdgv.Rows[i].Selected == true)
                    {
                        string spoolname = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                        sb.Append(spoolname + ",");
                    }
                }
                QCAttachment attachform = new QCAttachment();
                attachform.namestr = sb.ToString();
                attachform.ShowDialog();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 双击操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverViewdgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Text == "加工小票概览" || this.Text == "质检小票概览" || this.Text =="安装小票概览")
            {
                string spoolname = OverViewdgv.CurrentRow.Cells["小票号"].Value.ToString();
                QCAttachmentView qcav = new QCAttachmentView();
                qcav.namestr = spoolname;
                qcav.ShowDialog();
            }

            else
            { return; }
        }

        /// <summary>
        /// 右键菜单选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpoolGeneralViewForm_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = string.Format(" ");
            this.toolStripStatusLabel2.Text = string.Format(" ");
        }

        /// <summary>
        /// 添加炉批号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddHeattoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                AddHeatNumber heatnumform = new AddHeatNumber();
                heatnumform.ShowDialog();
                object obj = heatnumform.heatnumber;
                if (obj == null)
                {
                    return;
                }
                string heatno = heatnumform.heatnumber.ToUpper().ToString();
                try
                {
                    if (heatnumform.DialogResult == DialogResult.OK)
                    {
                        for (int i = 0; i < this.OverViewdgv.Rows.Count; i++)
                        {
                            if (this.OverViewdgv.Rows[i].Selected == true)
                            {
                                string pid = OverViewdgv.Rows[i].Cells["项目号"].Value.ToString();
                                string name = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                                string norm = OverViewdgv.Rows[i].Cells["材料型号"].Value.ToString();
                                this.OverViewdgv.Rows[i].Cells["炉批号"].Value = heatnumform.heatnumber.ToUpper();
                                object num = OverViewdgv.Rows[i].Cells["序号"].Value;
                                DBConnection.AddHeatNo(heatno,pid,name,norm,Convert.ToInt16(num));

                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 添加证书号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCertitoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                AddCertificateNumber certificatenumform = new AddCertificateNumber();
                certificatenumform.ShowDialog();
                object obj = certificatenumform.certificatenumber;
                if (obj == null)
                {
                    return;
                }
                string certi = certificatenumform.certificatenumber.ToUpper().ToString();
                try
                {
                    if (certificatenumform.DialogResult == DialogResult.OK)
                    {
                        for (int i = 0; i < this.OverViewdgv.Rows.Count; i++)
                        {
                            if (this.OverViewdgv.Rows[i].Selected == true)
                            {
                                string pid = OverViewdgv.Rows[i].Cells["项目号"].Value.ToString();
                                string name = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                                string norm = OverViewdgv.Rows[i].Cells["材料型号"].Value.ToString();
                                this.OverViewdgv.Rows[i].Cells["证书号"].Value = certificatenumform.certificatenumber.ToUpper();
                                object num = OverViewdgv.Rows[i].Cells["序号"].Value;
                                DBConnection.AddCertificate(certi, pid, name, norm, Convert.ToInt16(num));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 添加工时定额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddHourNormtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                AddHourNorm hournormform = new AddHourNorm();
                hournormform.ShowDialog();
                try
                {
                    if (hournormform.DialogResult == DialogResult.OK)
                    {
                        for (int i = 0; i < this.OverViewdgv.Rows.Count; i++)
                        {
                            if (this.OverViewdgv.Rows[i].Selected == true)
                            {
                                string pid = OverViewdgv.Rows[i].Cells["项目号"].Value.ToString();
                                string name = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                                this.OverViewdgv.Rows[i].Cells["工时定额(小时)"].Value = Math.Round(Convert.ToDouble(hournormform.hour), 2);
                                string sql = "update HOURNORM_TAB SET HOURNORM = " + Math.Round(Convert.ToDouble(hournormform.hour), 2) + " WHERE PROJECTID = '" + pid + "' AND SPOOLNAME = '" + name + "' ";
                                User.UpdateCon(sql, DataAccess.OIDSConnStr);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 添加实耗工时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddActualHourtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                AddActualHour actualhourform = new AddActualHour();
                actualhourform.ShowDialog();
                try
                {
                    if (actualhourform.DialogResult == DialogResult.OK)
                    {
                        for (int i = 0; i < this.OverViewdgv.Rows.Count; i++)
                        {
                            if (this.OverViewdgv.Rows[i].Selected == true)
                            {
                                string pid = OverViewdgv.Rows[i].Cells["项目号"].Value.ToString();
                                string name = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                                this.OverViewdgv.Rows[i].Cells["实耗工时(小时)"].Value = Math.Round(Convert.ToDouble(actualhourform.time), 2);
                                string sql = "update HOURNORM_TAB SET ACTUALHOUR = " + Math.Round(Convert.ToDouble(actualhourform.time), 2) + " WHERE PROJECTID = '" + pid + "' AND SPOOLNAME = '" + name + "' ";
                                User.UpdateCon(sql, DataAccess.OIDSConnStr);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }

        }

        /// <summary>
        /// 批量添加材料定额号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMSSNOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                AddMSS_NO mssnoform = new AddMSS_NO();
                mssnoform.ShowDialog();
                string mss = mssnoform.mssno.ToUpper().ToString();
                try
                {
                    if (mssnoform.DialogResult == DialogResult.OK)
                    {
                        for (int i = 0; i < this.OverViewdgv.Rows.Count; i++)
                        {
                            if (this.OverViewdgv.Rows[i].Selected == true)
                            {
                                string pid = OverViewdgv.Rows[i].Cells["项目号"].Value.ToString();
                                string name = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                                string norm = OverViewdgv.Rows[i].Cells["材料型号"].Value.ToString();
                                this.OverViewdgv.Rows[i].Cells["材料定额号"].Value = mssnoform.mssno.ToUpper().ToString();
                                object num = OverViewdgv.Rows[i].Cells["序号"].Value;
                                DBConnection.AddMSSNO(mss,pid,name,norm, Convert.ToInt16(num));

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 导出Excel报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.ExportToTxt(OverViewdgv, progressBar1);
        }

        /// <summary>
        /// 直接输入炉批号或者证书号添加或修改数据库的原始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverViewdgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int index = 0;
            try
            {
                index = OverViewdgv.CurrentRow.Index;
            }
            catch(SystemException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }

            string pid = OverViewdgv.Rows[index].Cells["项目号"].Value.ToString();
            string name = OverViewdgv.Rows[index].Cells["小票号"].Value.ToString();
            if (this.Text == "材料信息概览")
            {
                object num = OverViewdgv.Rows[index].Cells["序号"].Value;
                string norm = OverViewdgv.Rows[index].Cells["材料型号"].Value.ToString();
                string erpstr = OverViewdgv.Rows[index].Cells["ERP零件号"].Value.ToString();
                //if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLDESIGNUSERS"))
                //{
                //    string mssno = this.OverViewdgv.Rows[index].Cells["材料定额号"].Value.ToString();
                //    if (mssno != string.Empty)
                //    {
                //        DBConnection.AddMSSNO(mssno, pid, name, norm, Convert.ToInt16(num));
                //    }
                //    else
                //    {
                //        return;
                //    }
                //}
                if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLMACHINEUSERS"))
                {

                    //string trayno = this.OverViewdgv.Rows[index].Cells["托盘号"].Value.ToString();
                    string heat = this.OverViewdgv.Rows[index].Cells["炉批号"].Value.ToString();
                    string certificate = this.OverViewdgv.Rows[index].Cells["证书号"].Value.ToString();
                    string wpsnostr = this.OverViewdgv.Rows[index].Cells["焊接工艺号"].Value.ToString();
                    //if (trayno != string.Empty)
                    //{
                    //    try
                    //    {
                    //        OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接   
                    //        conn.Open();
                    //        OracleCommand cmd = conn.CreateCommand();
                    //        cmd.CommandText = "update SP_SPOOLMATERIAL_TAB set TRAYNAME = :te where PROJECTID = :pd and  SPOOLNAME = :se AND MATERIALNAME = :me  AND AMOUNT = :at  AND FLAG = :fg";
                    //        cmd.Parameters.Add("te", OracleType.VarChar).Value = trayno;
                    //        cmd.Parameters.Add("pd", OracleType.VarChar).Value = pid;
                    //        cmd.Parameters.Add("se", OracleType.VarChar).Value = name;
                    //        cmd.Parameters.Add("me", OracleType.VarChar).Value = norm;
                    //        cmd.Parameters.Add("at", OracleType.Number).Value = num;
                    //        cmd.Parameters.Add("fg", OracleType.VarChar).Value = "Y";

                    //        cmd.ExecuteNonQuery();
                    //        conn.Close();
                    //    }
                    //    catch (OracleException ex)
                    //    {
                    //        MessageBox.Show(ex.Message.ToString());
                    //    }
                    //}
                    //else
                    //{
                    //    return;
                    //}
                    string colname = this.OverViewdgv.CurrentCell.OwningColumn.Name.ToString();

                    //if (heat != string.Empty)
                    if (colname == "炉批号")
                    {
                        DBConnection.AddHeatNo(heat,pid,name,norm,Convert.ToInt16(num));
                    }


                    //else if (certificate != string.Empty)
                    else if (colname == "证书号")
                    {
                        DBConnection.AddCertificate(certificate, pid, name, norm, Convert.ToInt16(num));
                    }

                    else if (colname == "焊接工艺号")
                    {
                        try
                        {
                            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接   
                            conn.Open();
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "update SP_SPOOLMATERIAL_TAB set WPSNO = :te where PROJECTID = :pd and  SPOOLNAME = :se AND MATERIALNAME = :me  AND AMOUNT = :at  AND FLAG = :fg";
                            cmd.Parameters.Add("te", OracleType.VarChar).Value = wpsnostr;
                            cmd.Parameters.Add("pd", OracleType.VarChar).Value = pid;
                            cmd.Parameters.Add("se", OracleType.VarChar).Value = name;
                            cmd.Parameters.Add("me", OracleType.VarChar).Value = norm;
                            cmd.Parameters.Add("at", OracleType.Number).Value = num;
                            cmd.Parameters.Add("fg", OracleType.VarChar).Value = "Y";

                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (OracleException ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }

                    else
                    {
                        return;
                    }


                }
            }

            else if (this.Text == "焊接信息概览")
            {
                object wobj = OverViewdgv.Rows[index].Cells["焊缝对象"].Value;
                object wcount = OverViewdgv.Rows[index].Cells["焊缝序号"].Value;
                if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLMACHINEUSERS"))
                {
                    object weldperson = OverViewdgv.Rows[index].Cells["焊接人"].Value;

                    if (weldperson != System.DBNull.Value)
                    {
                        try
                        {
                            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接   
                            conn.Open();
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "update SP_WELD_TAB set WELDBY = :wb where PROJECTID = :pd and  SPOOLNAME = :se and WELDNAME = :wd and WELDCOUNT = :wc and FLAG = :fg ";
                            cmd.Parameters.Add("wb", OracleType.VarChar).Value = weldperson;
                            cmd.Parameters.Add("pd", OracleType.VarChar).Value = pid;
                            cmd.Parameters.Add("se", OracleType.VarChar).Value = name;
                            cmd.Parameters.Add("wd", OracleType.VarChar).Value = wobj;
                            cmd.Parameters.Add("wc", OracleType.Number).Value = wcount;
                            cmd.Parameters.Add("fg", OracleType.VarChar).Value = "Y";

                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (OracleException ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }

                else
                {
                    return;
                }
            }

        }

        private void SpoolGeneralViewForm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
            if(this.Text =="管路材料信息" || this.Text == "管路附件信息")
                MDIForm.tool_strip.Items[0].Enabled = false;
            else
                MDIForm.tool_strip.Items[0].Enabled = true;

        }

        /// <summary>
        /// 错误检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverViewdgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(this.OverViewdgv.Columns[e.ColumnIndex].HeaderText + " is \" " + this.OverViewdgv.Columns[e.ColumnIndex].ValueType + "\". Data error. 请检查.");
            return;
        }

        private void AddWeldBytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddWeldBy weldby = new AddWeldBy();
            weldby.ShowDialog();
            try
            {
                if (weldby.DialogResult == DialogResult.OK)
                {
                    for (int i = 0; i < this.OverViewdgv.Rows.Count; i++)
                    {
                        if (this.OverViewdgv.Rows[i].Selected == true)
                        {
                            string pid = OverViewdgv.Rows[i].Cells["项目号"].Value.ToString();
                            string name = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                            string wname = OverViewdgv.Rows[i].Cells["焊缝对象"].Value.ToString();
                            string wcount = OverViewdgv.Rows[i].Cells["焊缝序号"].Value.ToString();
                            this.OverViewdgv.Rows[i].Cells["焊接人"].Value = weldby.nameby.ToUpper();
                            string sql = "update SP_WELD_TAB SET WELDBY = '" + weldby.nameby + "' WHERE PROJECTID = '" + pid + "' AND SPOOLNAME = '" + name + "' AND WELDNAME = '" + wname + "' AND WELDCOUNT = '"+wcount+"' AND FLAG = 'Y'";
                            User.UpdateCon(sql, DataAccess.OIDSConnStr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// 对齐格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverViewdgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn dgvc in  OverViewdgv.Columns)
            {
                if (dgvc.ValueType == typeof(DateTime))
                {
                    dgvc.DefaultCellStyle.Format = "yyyy-MM-dd";
                }
                if (dgvc.ValueType == typeof(decimal) )
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (dgvc.Name != "行号" && dgvc.Name != "序号" && dgvc.Name != "焊缝序号")
                    {
                        dgvc.DefaultCellStyle.Format = "N2";
                    }
                }
            }
        }

        /// <summary>
        /// 编辑完成显示大写格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverViewdgv_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.DesiredType == typeof(string))
            {
                e.Value = e.Value.ToString().ToUpper();
                e.ParsingApplied = true;
            }
        }

        private void OverViewdgv_SelectionChanged(object sender, EventArgs e)
        {
            int count = this.OverViewdgv.SelectedRows.Count;
            this.toolStripStatusLabel2.Text = string.Format("当前选中{0}行",count);
            if (this.OverViewdgv.SelectedRows.Count > 0)
            {
                GetContextMenu();
            }
            else
            {
                for (int i = 0; i < DisplaycontextMenuStrip.Items.Count; i++)
                {
                    DisplaycontextMenuStrip.Items[i].Visible = false;
                }
            }

        }

        /// <summary>
        /// 查询所选中的单个或多个小票的管路材料信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PipeMatialtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < OverViewdgv.Rows.Count; i++)
                {
                    if (OverViewdgv.Rows[i].Selected == true)
                    {
                        string spoolname = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                        sb.Append("'" + spoolname + "'" + ",");
                    }
                }
                string str = sb.Remove(sb.Length - 1, 1).ToString();
                DataSet ds = new DataSet();
                string sql = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from SP_SPOOLMATERIAL_TAB t where (T.MATERIALNAME LIKE '%主管%' OR T.MATERIALNAME like '%支管%') and t.flag = 'Y' and t.spoolname in (select spoolname from sp_spool_tab where  spoolname in (" + str + ") and flag = 'Y')";
                foreach (Form form in MDIForm.pMainWin.MdiChildren)
                {
                    if (form.Text == "管路材料信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = null;
                        User.DataBaseConnect(sql, ds);
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                        int i = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                        ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前材料数量共：{0}个", i);
                        ds.Dispose();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "管路材料信息";
                sgvf.MdiParent = MDIForm.pMainWin;
                sgvf.Show();
                User.DataBaseConnect(sql, ds);
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                int count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前材料数量共：{0}个", count);
                ds.Dispose();
            }
            else
            {
                return;
            }
 
        }

        /// <summary>
        /// 查询所选中的单个或多个小票的管路附件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PipeSparetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;

            if (rowcount > 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < OverViewdgv.Rows.Count; i++)
                {
                    if (OverViewdgv.Rows[i].Selected == true)
                    {
                        string spoolname = OverViewdgv.Rows[i].Cells["小票号"].Value.ToString();
                        sb.Append("'" + spoolname + "'" + ",");
                    }
                }
                string str = sb.Remove(sb.Length - 1, 1).ToString();
                DataSet ds = new DataSet();
                string sql = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from SP_SPOOLMATERIAL_TAB t  where t.materialname not like '%管%' and t.flag = 'Y' and t.spoolname in (select spoolname from sp_spool_tab where  spoolname in (" + str + ") and flag = 'Y')";
                foreach (Form form in MDIForm.pMainWin.MdiChildren)
                {
                    if (form.Text == "管路附件信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = null;
                        User.DataBaseConnect(sql, ds);
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                        int i = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                        ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前附件数量共：{0}个", i);
                        ds.Dispose();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "管路附件信息";
                sgvf.MdiParent = MDIForm.pMainWin;
                sgvf.Show();
                User.DataBaseConnect(sql, ds);
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                int count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前材料数量共：{0}个", count);
                ds.Dispose();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 拉动滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverViewdgv_Scroll(object sender, ScrollEventArgs e)
        {
            this.WeightToolTip.Active = false;
            return;
        }

        /// <summary>
        /// 右键菜单控制
        /// </summary>
        public void GetContextMenu()
        {
            #region 所有用户右键操作
            if (this.Text == "材料信息概览")
            {
                if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLMACHINEUSERS"))
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["AddHeattoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["AddCertitoolStripMenuItem"].Visible = true;
                }

                else if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLDESIGNUSERS"))
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator11"].Visible = DisplaycontextMenuStrip.Items["AddMSSNOToolStripMenuItem"].Visible = true;
                }
                else
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = true;
                }
            }
            else if (this.Text == "连接件信息概览" || this.Text == "加工信息概览" || this.Text == "管路材料信息" || this.Text =="管路附件信息")
            {
                DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible =true;
            }

            else if (this.Text == "焊接信息概览")
            {
                if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLMACHINEUSERS"))
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["AddWeldBytoolStripMenuItem"].Visible = true;
                }
                else
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible =true;
                }
            }

            else if (this.Text == "管路统计信息")
            {
                DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["ExportPipeToolStripMenuItem"].Visible = true;
            }
            else if (this.Text == "管路附件统计信息")
            {
                DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["ExportMaterialtoolStripMenuItem"].Visible = true;
            }

            #endregion

            #region 设计用户右键操作
            else if (this.Text == "设计小票概览")
            {
                if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLDESIGNUSERS"))
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["PipeSparetoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["PipeMatialtoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator12"].Visible = DisplaycontextMenuStrip.Items["删除记录ToolStripMenuItem"].Visible = true;
                    
                }
            }
            #endregion

            #region  管加工用户右键操作
            else if (this.Text == "加工小票概览")
            {
                if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLMACHINEVIEW"))
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible  = true;
                }
                else
                {
                    DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["AddAttachmenttoolStripMenuItem"].Visible  = true;
                }
            }

            #endregion

            #region 质检用户右键操作
            else if (this.Text == "质检小票概览")
            {
                DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible =  DisplaycontextMenuStrip.Items["AddAttachmenttoolStripMenuItem"].Visible = true;
            }
            else if (this.Text == "检验通过小票概览")
            {
                DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible =  true;
            }
            #endregion

            #region 安装用户右键操作
            else if (this.Text == "安装小票概览")
            {
                DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["InstallToolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["CompleteInstallToolStripMenuItem"].Visible  = true;
            }
            #endregion

            #region 调试用户右键操作
            else if (this.Text == "调试小票概览")
            {
                DisplaycontextMenuStrip.Items["DetailstoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["toolStripSeparator6"].Visible = DisplaycontextMenuStrip.Items["CompleteModulatetoolStripMenuItem"].Visible = DisplaycontextMenuStrip.Items["FeedBackToInstalltoolStripMenuItem"].Visible =  true;
            }
            #endregion
        }
        /// <summary>
        /// 删除所选记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.OverViewdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                DialogResult result;
                result = MessageBox.Show("确定要删除所选中的小票以及相关信息？","信息提示！",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow drow in this.OverViewdgv.SelectedRows)
                    {
                        int rowindex = drow.Index;
                        string project = this.OverViewdgv.Rows[rowindex].Cells["项目号"].Value.ToString();
                        string spool = this.OverViewdgv.Rows[rowindex].Cells["小票号"].Value.ToString();
                        string drawno = this.OverViewdgv.Rows[rowindex].Cells["图号"].Value.ToString();
                        string sqStr = "select count(*)from project_drawing_tab t where t.project_id = (select id from project_tab where name = '" + project + "') and t.drawing_no = '"+drawno+"' and t.responsible_user = '"+User.cur_user+"'";

                        object count = User.GetScalar(sqStr, DataAccess.OIDSConnStr);
                        if (count == null || Convert.ToInt16(count.ToString()) == 0 )
                        {
                            MessageBox.Show("您没有权限删除所选的数据行！");
                            return;
                        }
                        DelSpoolRecord.MarkDeletedSpoolRecord(project, spool, drawno, User.cur_user);
                    }

                    MessageBox.Show("-数据删除完毕！-请重新查询并验证！");

                }
            }
            else
            {
                return;
            }
        }
        
    }
}