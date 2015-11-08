using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Web;
using System.IO;
namespace CIMCPPSPL
{
    public partial class SpoolQuery : Form
    {
        public SpoolQuery()
        {
            InitializeComponent();
            
        }
        private string   projectStr, spoolnameStr;
        private void button1_Click(object sender, EventArgs e)
        {

           
            projectStr = comboBox1.Text.Trim().ToUpper();
            spoolnameStr = textBox1.Text.Trim().ToUpper();
            string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态 from spool_tab t where upper(t.projectid) like '%"+projectStr+"%' and upper(t.spoolname) like '%"+spoolnameStr+"%' and t.flowstatus =7 and t.flag='Y' and t.locked=0";
             
            //DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
            try
            {
                WSR.PPSPLLogin ppspl = new CIMCPPSPL.WSR.PPSPLLogin();

                DataSet Ds = ppspl.DatagridDs(sqlString, "spool_tab");

                dataGrid1.DataSource = Ds.Tables[0].DefaultView;
                menuItem1.Enabled = false;
                menuItem3.Enabled = false;
                menuItem4.Enabled = false;
                menuItem5.Enabled = false;
                menuItem6.Enabled = false;
                if (Ds.Tables[0].Rows.Count != 0)
                {
                    menuItem2.Enabled = true;
                }
                else
                {
                    menuItem2.Enabled = false;
                    
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString()+":请检查手机是否已经正确连接至电脑！！");
            }
            
        }

        private void SpoolQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DataGridTableStyle ts = new DataGridTableStyle();
            //ts.MappingName = "spool_tab";

            //DataGridColumnStyle ID = new DataGridTextBoxColumn();
            //ID.MappingName = "PROJECTID";
            //ID.HeaderText = "项目ID";
            //ID.Width = 15;
            //ts.GridColumnStyles.Add(ID);
            //DataGridTextBoxColumn dts = new DataGridTextBoxColumn();
            //DataGridColumnStyle housename = new   DataGridTextBoxColumn();
            //housename.MappingName = "SPOOLNAME";
            //housename.HeaderText = "小票名";
            //housename.Width = 80;
            //housename.NullText = "";
            //ts.GridColumnStyles.Add(housename);

            //DataGridColumnStyle content = new DataGridTextBoxColumn();
            //content.MappingName = "ServiceContent";
            //content.HeaderText = "内容";
            //content.NullText = "";
            //content.Width = this.Width - ID.Width - housename.Width;
            //ts.GridColumnStyles.Add(content);
            //this.dataGrid1.RowHeadersVisible = true;
            //this.dataGrid1.HeaderBackColor = Color.Gray;
            //this.dataGrid1.HeaderForeColor = Color.White;
            //this.dataGrid1.GridLineColor = Color.DarkGray;
            //this.dataGrid1.BackColor = Color.White;
            //this.dataGrid1.TableStyles.Clear();
            //this.dataGrid1.TableStyles.Add(ts);
            string fpath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;           
            
            MessageBox.Show(fpath.Substring(0,fpath.Length - 13));
            
            //dataGrid1.Select(0);
            
            //MessageBox.Show(sdsd);
            
        }

        private void SpoolQuery_Load(object sender, EventArgs e)
        {
            
            menuItem1.Enabled = false;
            menuItem2.Enabled = false;
            menuItem3.Enabled = false;
            menuItem4.Enabled = false;
            menuItem5.Enabled = false;
            menuItem6.Enabled = false;
            WSR.PPSPLLogin sppspl = new CIMCPPSPL.WSR.PPSPLLogin();
            string sqlString = "select * from projectname";
            DataSet sDs = sppspl.DatagridDs(sqlString, "projectname");
            DataRow rowdim = sDs.Tables[0].NewRow();
            rowdim[0] = "";
            sDs.Tables[0].Rows.InsertAt(rowdim, 0);
            comboBox1.DataSource = sDs.Tables[0].DefaultView;
            comboBox1.ValueMember = "PROJNAME";
            comboBox1.DisplayMember = "PROJNAME";
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (this.dataGrid1.CurrentRowIndex >= 0)
            {
                int rowcount = dataGrid1.VisibleRowCount;
                if (rowcount == 0) return;
                string CheckFlag = "NO";
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    { CheckFlag = "YES"; }
                }
                if (CheckFlag == "NO") { MessageBox.Show("请选择要上传的数据行!","操作提示"); return; }
                try
                {
                    for (int i = 0; i < rowcount; i++)
                    {
                        
                        if (dataGrid1.IsSelected(i))
                        {
                            string projectid = dataGrid1[i, 0].ToString();
                            string spoolname = dataGrid1[i, 1].ToString();
                            string flowstatus = dataGrid1[i, 24].ToString();
                            string flowstatusremark = dataGrid1[i, 26].ToString();
                            string lockresult = flowstatus == "不合格" ? "0" : "2";
                            string flowtag = flowstatus == "不合格" ? "10" : "11";
                            string remarkstr = flowstatus == "不合格" ? flowstatusremark : "手机上传报验结果：合格";
                            string orastr = "update spool_tab t set t.locked = " + lockresult + ", t.flowstatusremark='" + remarkstr + "',t.flowstatus=" + flowtag +
                                " where t.spoolname = '" + spoolname + "' and t.projectid = '" + projectid + "' and t.flowstatus = 7 " +
                                "and t.locked= 1";
                            string pdastr = "update spool_tab  set locked =2  , flowstatusremark='" + remarkstr +
                                "' where spoolname = '" + spoolname + "' and projectid = '" + projectid + "'" +
                                "and locked= 1";
                            string orastrlog = "insert into spflowlog_tab(spoolname,username,flowstatus,remark,projectid) values('" + spoolname + "','" + XmlOper.getusername() + "'," + flowtag + ",'" + remarkstr + "','" + projectid + "') ";
                            string[] updatestr = new string[2];
                            updatestr[0] = orastr;
                            updatestr[1] = orastrlog;
                            WSR.PPSPLLogin ppspl = new CIMCPPSPL.WSR.PPSPLLogin();
                            //检查数据是否已经成功处理
                            if (User.RUNSQLCommon(pdastr) < 0)
                            {
                                MessageBox.Show("你所选数据上传失败。请重新选择上传", "提示消息");
                                return;
                            }
                            string eflag = ppspl.UpdateDs(updatestr);
                            if (eflag != "aaa")
                            {
                                MessageBox.Show("你所选数据上传失败。请重新选择上传", "提示消息");
                                return;
                            }

                        }
                    }
                    MessageBox.Show("上传报验结果成功。", "提示消息");
                    projectStr = comboBox1.Text.Trim();
                    spoolnameStr = textBox1.Text.Trim().ToUpper();
                    string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态,flowstatusremark as 流程备注 from spool_tab t where t.projectid like '%" + projectStr + "%' and upper(t.spoolname) like '%" + spoolnameStr + "%' and t.flowstatus <>7 and t.locked=1";
                    DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
                    dataGrid1.DataSource = Ds.Tables[0].DefaultView;
                }
                catch (Exception err)
                {
                    MessageBox.Show("请检查手机是否已经正确连接至电脑！！");
                }
            }
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (this.dataGrid1.CurrentRowIndex >= 0)
            {
                
                WSR.PPSPLLogin ppspl = new CIMCPPSPL.WSR.PPSPLLogin();
                int rowcount = dataGrid1.VisibleRowCount;
                if (rowcount == 0) return;
                string CheckFlag="NO";
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    { CheckFlag = "YES"; }
                }
                if (CheckFlag == "NO") { MessageBox.Show("请选择要下载的数据行!", "操作提示"); return; }
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    {
                        string projectid = dataGrid1[i, 0].ToString();
                        string spoolname = dataGrid1[i, 1].ToString();
                        string orastr = "update spool_tab t set t.locked = 1 " +
                            "where t.spoolname = '" + spoolname + "' and t.projectid = '" + projectid + "' and t.flowstatus = 7 " +
                            "and t.locked= 0";
                        string orastrlog = "insert into spflowlog_tab(spoolname,username,flowstatus,remark,projectid) values('" + spoolname + "','" + XmlOper.getusername() + "',7,'此数据已下载到手机端处理','" + projectid + "') ";

                        string[] updatestr = new string[2];
                        updatestr[0] = orastr;
                        updatestr[1] = orastrlog;
                        
                        //更新锁定数据之前检查数据是否已经被处理
                        try
                        {
                            if (ppspl.GetSpoolStatus(spoolname) != "7")
                            {
                                MessageBox.Show("你所选数据已经被另一用户处理。请重新选择", "提示消息");
                                return;
                            }
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.ToString()+":请检查手机是否已经正确连接至电脑！！");
                            return;
                        }
                        //锁定数据
                        string eflag = ppspl.UpdateDs(updatestr);
                        //将数据下载写入至手机端的SQLITE数据库
                        string p_proName = dataGrid1[i, 0].ToString();
                        string p_SpoolName = dataGrid1[i, 1].ToString();
                        string p_SysID = dataGrid1[i, 2].ToString();
                        string p_SysName = dataGrid1[i, 3].ToString();
                        string p_PipeClass = dataGrid1[i, 4].ToString();
                        string p_Surface = dataGrid1[i, 5].ToString();
                        string p_Pressure = dataGrid1[i, 6].ToString();
                        string p_TestPlace = dataGrid1[i, 7].ToString();

                        string p_PipeCheck = dataGrid1[i, 8].ToString();
                        string p_SpoolWeight = dataGrid1[i, 9].ToString();
                        string p_PaintColor = dataGrid1[i, 10].ToString();
                        string p_Canbin = dataGrid1[i, 11].ToString();
                        string p_Rev = dataGrid1[i, 12].ToString();
                        string p_ModifyType = dataGrid1[i, 13].ToString();
                        string p_ElbowType = dataGrid1[i, 14].ToString();
                        string p_WeldType = dataGrid1[i, 15].ToString();

                        string p_DrawingNo = dataGrid1[i, 16].ToString();
                        string p_BlockNo = dataGrid1[i, 17].ToString();
                        string p_ModifyDrawingNO = dataGrid1[i, 18].ToString();
                        string p_Remark = dataGrid1[i, 19].ToString();
                        string p_LogName = dataGrid1[i, 21].ToString();
                        string p_LogTime = dataGrid1[i, 22].ToString();
                        string p_LineName = dataGrid1[i, 23].ToString();

                        string sqlstr = "insert into SPOOL_TAB(PROJECTID,SPOOLNAME,SYSTEMID,SYSTEMNAME,PIPEGRADE,SURFACETREATMENT,WORKINGPRESSURE,PRESSURETESTFIELD,PIPECHECKFIELD,SPOOLWEIGHT," +
            "PAINTCOLOR,CABINTYPE,REVISION,SPOOLMODIFYTYPE,ELBOWTYPE,WELDTYPE,DRAWINGNO,BLOCKNO,MODIFYDRAWINGNO,REMARK,LOGNAME,SYSTEMTIME,LINENAME,FLOWSTATUS,LOCKED)" +
            " values('" + p_proName + "','" + p_SpoolName + "','" + p_SysID + "','" + p_SysName + "','" + p_PipeClass + "','" + p_Surface + "','" + p_Pressure + "','" + p_TestPlace + "','" + p_PipeCheck + "','" + p_SpoolWeight + "','" +
            p_PaintColor + "','" + p_Canbin + "','" + p_Rev + "','" + p_ModifyType + "','" + p_ElbowType + "','" + p_WeldType + "','" + p_DrawingNo + "','" + p_BlockNo + "','" + p_ModifyDrawingNO + "','" + p_Remark + "','" + p_LogName + "','" + p_LogTime + "','" + p_LineName + "',7,1)";
                        int flag = User.RUNSQLCommon(sqlstr);
                        if (flag > 0)
                        {
                            
                        }
                        else
                        {
                            MessageBox.Show("下载数据失败！", "提示消息");
                            return;
                        }
                    }
                }
                MessageBox.Show("下载数据成功","提示消息");
                projectStr = comboBox1.Text.Trim();
                spoolnameStr = textBox1.Text.Trim().ToUpper();
                string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态 from spool_tab t where  t.projectid like '%" + projectStr + "%' and upper(t.spoolname) like '%" + spoolnameStr + "%' and t.flowstatus =7 and t.flag='Y' and t.locked=0";

                //DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
                //WSR.PPSPLLogin ppsplnew = new CIMCPPSPL.WSR.PPSPLLogin();
                try
                {
                    DataSet Ds = ppspl.DatagridDs(sqlString, "spool_tab");

                    dataGrid1.DataSource = Ds.Tables[0].DefaultView;
                }
                catch (Exception err)
                {
                    MessageBox.Show("请检查手机是否已经正确连接至电脑！！");
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            projectStr = comboBox1.Text.Trim();
            spoolnameStr = textBox1.Text.Trim().ToUpper();
            string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态 from spool_tab t where t.projectid like '%"+projectStr+"%' and upper(t.spoolname) like '%"+spoolnameStr+"%' and t.flowstatus =7 and t.locked=1";

            DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
            //WSR.PPSPLLogin ppspl = new CIMCPPSPL.WSR.PPSPLLogin();
            //DataSet Ds = ppspl.DatagridDs(sqlString, "spool_tab");

            dataGrid1.DataSource = Ds.Tables[0].DefaultView;
            menuItem1.Enabled = false;
            menuItem2.Enabled = false;
            menuItem6.Enabled = false;
            if (Ds.Tables[0].Rows.Count != 0)
            {
                menuItem3.Enabled = true;
                menuItem4.Enabled = true;
                menuItem5.Enabled = true;
            }
            else
            {
                menuItem3.Enabled = false;
                menuItem4.Enabled = false;
                menuItem5.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (this.dataGrid1.CurrentRowIndex >= 0)
            {
                int rowcount = dataGrid1.VisibleRowCount;
                if (rowcount == 0) return;
                string CheckFlag = "NO";
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    { CheckFlag = "YES"; }
                }
                if (CheckFlag == "NO") { MessageBox.Show("请选择要处理的数据行!", "操作提示"); return; }
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    {
                        string projectid = dataGrid1[i, 0].ToString();
                        string spoolname = dataGrid1[i, 1].ToString();
                        string PDAstr = "update spool_tab  set flowstatus=11  " +
                            "where spoolname = '" + spoolname + "' and projectid = '" + projectid + "' and flowstatus = 7 " +
                            "and locked= 1";
                        int flag = User.RUNSQLCommon(PDAstr);
                        if (flag > 0)
                        {
                            
                        }
                        else 
                        {
                            MessageBox.Show("处理数据成功失败！", "提示消息");
                            return;
                        }
                    }
                }
                MessageBox.Show("处理数据成功:已报验合格", "提示消息");
                projectStr = comboBox1.Text.Trim();
                spoolnameStr = textBox1.Text.Trim().ToUpper();
                string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态 from spool_tab t where t.projectid like '%" + projectStr + "%' and upper(t.spoolname) like '%" + spoolnameStr + "%' and t.flowstatus =7 and t.locked=1";

                DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
                dataGrid1.DataSource = Ds.Tables[0].DefaultView;
            }
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            if (this.dataGrid1.CurrentRowIndex >= 0)
            {
                int rowcount = dataGrid1.VisibleRowCount;
                if (rowcount == 0) return;
                string CheckFlag = "NO";
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    { CheckFlag = "YES"; }
                }
                if (CheckFlag == "NO") { MessageBox.Show("请选择要处理的数据行!", "操作提示"); return; }
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    {
                        string projectid = dataGrid1[i, 0].ToString();
                        string spoolname = dataGrid1[i, 1].ToString();
                        string PDAstr = "update spool_tab  set flowstatus=10  " +
                            "where spoolname = '" + spoolname + "' and projectid = '" + projectid + "' and flowstatus = 7 " +
                            "and locked= 1";
                        
                        int flag = User.RUNSQLCommon(PDAstr);
                        if (flag > 0)
                        {
                            Form1 f1 = new Form1(spoolname,projectid);
                            if (f1.ShowDialog() != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("处理数据成功失败！", "提示消息");
                            return;
                        }
                    }
                }
                
                MessageBox.Show("处理数据成功:已报验不合格", "提示消息");
                string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态 from spool_tab t where t.flowstatus =7 and t.locked=1";

                DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
                dataGrid1.DataSource = Ds.Tables[0].DefaultView;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态,flowstatusremark as 流程备注 from spool_tab t where t.projectid like '%" + projectStr + "%' and upper(t.spoolname) like '%" + spoolnameStr + "%' and t.flowstatus in (10,11) and t.locked=1";

            DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
            dataGrid1.DataSource = Ds.Tables[0].DefaultView;
            menuItem2.Enabled = false;
            menuItem3.Enabled = false;
            menuItem4.Enabled = false;
            menuItem5.Enabled = false;
            if (Ds.Tables[0].Rows.Count != 0)
            {
                menuItem1.Enabled = true;
                menuItem6.Enabled = true;
            }
            else
            {
                menuItem1.Enabled = false;
                menuItem6.Enabled = false;
            }
            
        }

 
        private void contextMenu1_Popup(object sender, EventArgs e)
        {

        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            if (this.dataGrid1.CurrentRowIndex >= 0)
            {

                WSR.PPSPLLogin ppspl = new CIMCPPSPL.WSR.PPSPLLogin();
                int rowcount = dataGrid1.VisibleRowCount;
                if (rowcount == 0) return;
                string CheckFlag = "NO";
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    { CheckFlag = "YES"; }
                }
                if (CheckFlag == "NO") { MessageBox.Show("请选择要取消的数据行!", "操作提示"); return; }
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    {
                        string projectid = dataGrid1[i, 0].ToString();
                        string spoolname = dataGrid1[i, 1].ToString();
                        string orastr = "update spool_tab t set t.locked = 0 " +
                            "where t.spoolname = '" + spoolname + "' and t.projectid = '" + projectid + "' and t.flowstatus = 7 " +
                            "and t.locked= 1 and t.flag='Y'";
                        string orastrlog = "insert into spflowlog_tab(spoolname,username,flowstatus,remark,projectid) values('" + spoolname + "','" + XmlOper.getusername() + "',7,'此数据已取消在手机端处理','" + projectid + "') ";

                        string[] updatestr = new string[2];
                        updatestr[0] = orastr;
                        updatestr[1] = orastrlog;

                        //更新锁定数据之前检查数据是否已经被处理
                        try
                        {
                            string aaa= ppspl.UpdateDs(updatestr);
                            
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("请检查手机是否已经正确连接至电脑！！");
                            return;
                        }
                        
                        //将数据下载从手机端的SQLITE数据库删除
                        string p_proName = dataGrid1[i, 0].ToString();
                        string p_SpoolName = dataGrid1[i, 1].ToString();


                        string sqlstr = "delete from SPOOL_TAB where projectid='" + p_proName + "' and spoolname ='" + p_SpoolName+"'";
                        int flag = User.RUNSQLCommon(sqlstr);
                        if (flag > 0)
                        {

                        }
                        else
                        {
                            MessageBox.Show("取消下载数据失败！", "提示消息");
                            return;
                        }
                    }
                }
                MessageBox.Show("取消下载数据成功", "提示消息");
                projectStr = comboBox1.Text.Trim();
                spoolnameStr = textBox1.Text.Trim();
                string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态 from spool_tab t where t.projectid like '%" + projectStr + "%' and t.spoolname like '%" + spoolnameStr + "%' and t.flowstatus =7 and t.locked=1";

                DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
                dataGrid1.DataSource = Ds.Tables[0].DefaultView;
            }
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            if (this.dataGrid1.CurrentRowIndex >= 0)
            {
                int rowcount = dataGrid1.VisibleRowCount;
                if (rowcount == 0) return;
                string CheckFlag = "NO";
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    { CheckFlag = "YES"; }
                }
                if (CheckFlag == "NO") { MessageBox.Show("请选择要处理的数据行!", "操作提示"); return; }
                for (int i = 0; i < rowcount; i++)
                {
                    if (dataGrid1.IsSelected(i))
                    {
                        string projectid = dataGrid1[i, 0].ToString();
                        string spoolname = dataGrid1[i, 1].ToString();
                        string PDAstr = "update spool_tab  set flowstatus=7  " +
                            "where spoolname = '" + spoolname + "' and projectid = '" + projectid + "' and flag='Y' " +
                            "and locked= 1";
                        int flag = User.RUNSQLCommon(PDAstr);
                        if (flag > 0)
                        {

                        }
                        else
                        {
                            MessageBox.Show("重新处理数据失败！", "提示消息");
                            return;
                        }
                    }
                }
                MessageBox.Show("重新处理数据成功:已退为待处理", "提示消息");
                projectStr = comboBox1.Text.Trim();
                spoolnameStr = textBox1.Text.Trim().ToUpper();
                string sqlString = "SELECT PROJECTID AS 项目号, SPOOLNAME AS 小票号, SYSTEMID AS 系统号, SYSTEMNAME AS 系统名, PIPEGRADE AS 管路等级, SURFACETREATMENT AS 表面处理, WORKINGPRESSURE AS 工作压力, PRESSURETESTFIELD AS 压力测试场所, PIPECHECKFIELD AS 校管场所 , SPOOLWEIGHT AS 小票重量, PAINTCOLOR AS 油漆颜色, CABINTYPE AS 舱室种类, REVISION AS 小票版本, SPOOLMODIFYTYPE AS 小票修改种类,ELBOWTYPE AS 弯头形式, WELDTYPE AS 点焊件, DRAWINGNO AS 图号, BLOCKNO AS 分段号, MODIFYDRAWINGNO AS 修改通知单号, REMARK AS 备注, FLAG AS 版本标识, LOGNAME AS 登录名, SYSTEMTIME AS 系统时间, LINENAME AS 线号, (SELECT NAME FROM spflowstatus_tab s WHERE s.ID=FLOWSTATUS)  AS 流程状态标识, (SELECT NAME FROM QCSTATTUS_TAB s WHERE s.ID=LOCKED) as 锁定状态 from spool_tab t where t.projectid like '%" + projectStr + "%' and upper(t.spoolname) like '%" + spoolnameStr + "%' and t.flowstatus <>7 and t.locked=1";

                DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
                dataGrid1.DataSource = Ds.Tables[0].DefaultView;
            }
        }
    }
}