using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OracleClient;

namespace DetailInfo
{
    public partial class ModulationFeedBack : Form
    {
        public ModulationFeedBack()
        {
            InitializeComponent();
            this.ModulationFeedBackContextMenuStrip.Enabled = false;
        }

        private void ModulationFeedBack_Load(object sender, EventArgs e)
        {
            //DataSet ds = new DataSet();
            //string sql = "select PROJECTID 项目号, SPOOLNAME 小票号, SYSTEMID  系统号, SYSTEMNAME  系统名, PIPEGRADE  管路等级, SURFACETREATMENT  表面处理, WORKINGPRESSURE  工作压力, PRESSURETESTFIELD  压力测试场所, PIPECHECKFIELD  校管场所 , SPOOLWEIGHT as  \"小票重量(kg)\", PAINTCOLOR  油漆颜色, CABINTYPE  舱室种类, REVISION  小票版本, SPOOLMODIFYTYPE  小票修改种类,ELBOWTYPE  弯头形式, WELDTYPE  点焊件, DRAWINGNO  图号, BLOCKNO  分段号, MODIFYDRAWINGNO  修改通知单号, REMARK  备注, FLAG  版本标识, LOGNAME  录入人, LOGDATE  录入日期, LINENAME  线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)   流程状态标识, FLOWSTATUSREMARK 流程状态备注 from SP_SPOOL_TAB where flag = 'Y' and  spoolname in (SELECT s.SPOOLNAME as 小票名 from spflowlog_tab S where s.spoolname in (select DISTINCT t.spoolname  from spflowlog_tab t where t.username = '" + User.cur_user + "' and t.flowstatus = " + (int)FlowState.待调试 + ") and s.flowstatus = " + (int)FlowState.调试失败 + ")";
            //User.DataBaseConnect(sql, ds);
            //dgv_modulationfeedback.DataSource = ds.Tables[0].DefaultView;
            //if (dgv_modulationfeedback.Rows.Count != 0)
            //{
            //    this.ModulationFeedBackContextMenuStrip.Enabled = true;
            //}
            //ds.Dispose();
        }

        private void 接受处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ArrayList selectlist = new ArrayList();
            //ArrayList select_array = new ArrayList();
            //if (dgv_modulationfeedback.RowCount == 0)
            //{
            //    MessageBox.Show("暂时没有要处理的数据！");
            //    return;
            //}
            //if (dgv_modulationfeedback.RowCount != 0)
            //{
            //    for (int i = 0; i < dgv_modulationfeedback.RowCount; i++)
            //    {
            //        if (dgv_modulationfeedback.Rows[i].Selected == true)
            //        {
            //            selectlist.Add(i);
            //        }
            //    }
            //    if (selectlist.Count != 0)
            //    {
            //        DialogResult result;
            //        result = MessageBox.Show("确定对选定数据进行处理？", "问题小票处理", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            foreach (DataGridViewRow dr in dgv_modulationfeedback.Rows)
            //            {
            //                if (dr.Selected == true)
            //                {
            //                    int id = Convert.ToInt32(dr.Index);
            //                    select_array.Add(id);
            //                }
            //            }
            //            if (select_array.Count != 0)
            //            {
            //                for (int j = 0; j < select_array.Count; j++)
            //                {
            //                    int index = Convert.ToInt32(select_array[j]);
            //                    string spname = dgv_modulationfeedback.Rows[index].Cells["小票号"].Value.ToString();
            //                    string prid = dgv_modulationfeedback.Rows[index].Cells["项目号"].Value.ToString();

            //                    //string sql3 = "update SP_SPOOL_TAB set FLOWSTATUS =(select ID from SPFLOWSTATUS_TAB where NAME = '待调试') where SPOOLNAME = '" + spname + "' and PROJECTID = '"+prid+"'  and FLAG = 'Y'";
            //                    DBConnection.UpDateState((int)FlowState.待调试, spname, prid, "Y");

            //                    //string sql4 = "insert into SPFLOWLOG_TAB (SPOOLNAME,USERNAME,FLOWSTATUS,PROJECTID) VALUES( '" + spname + "', '" + User.cur_user + "',  (select ID from SPFLOWSTATUS_TAB where NAME = '待调试') ,'" + prid + "')";
            //                    DBConnection.InsertFlowLog(spname, User.cur_user, (int)FlowState.待调试, prid);
            //                }
            //                ModulationFeedBack_Load(sender, e);
            //            }
            //        }
            //        else
            //        {
            //            return;
            //        }
            //    }
            //}
        }

        private void 导出Excel表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.ExportToTxt(dgv_modulationfeedback, progressBar1);
        }

        private void dgv_modulationfeedback_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            SpoolCellFormat.FormatCell(dgv_modulationfeedback);
        }

    }
}