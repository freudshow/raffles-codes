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
    public partial class WorkShopPlan : Form
    {
        BlockContructionControl blockcontrol;
        TRAY_MATERIAL_DELIVERYTIMEControl traymaterialcontrol;
        TRAY_PREPLANControl traypreplancontrol;
        TRAY_INSTALLATIONControl trayinstallcontrol;
        //TRAY_MATERIAL_DETAILControl traymaterialdetialcontrol;
        //TRAY_PRE_DETAILControl traypredetailcontrol;
        public WorkShopPlan()
        {
            InitializeComponent();
        }
        string TypeNme = string.Empty;
        string sqlStr = string.Empty;
        private void WorkShopPlan_Load(object sender, EventArgs e)
        {
            string[] TypeList = new string[] { "分段施工计划", "托盘材料纳期计划", "托盘预制计划", "托盘安装计划"};
            foreach (string item in TypeList)
            {
                this.PlanComb.Items.Add(item);
            }
            
            sqlStr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82)   ORDER BY NAME";
            ComFill(this.ProjectComb, sqlStr);
        }

        /// <summary>
        /// 选择计划类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlanComb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.splitContainer1.Panel1.Controls.Count != 0)
            {
                this.splitContainer1.Panel1.Controls.RemoveAt(0);
            }
            this.PlanDGV.DataSource = null;
            this.ProjectComb.SelectedIndex = -1;
            TypeNme = this.PlanComb.SelectedItem.ToString();
            switch (TypeNme)
            {
                case "分段施工计划":
                    blockcontrol = new BlockContructionControl();
                    blockcontrol.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel1.Controls.Add(blockcontrol);
                    break;
                case "托盘材料纳期计划":
                    traymaterialcontrol = new TRAY_MATERIAL_DELIVERYTIMEControl();
                    traymaterialcontrol.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel1.Controls.Add(traymaterialcontrol);
                    break;
                case "托盘预制计划":
                    traypreplancontrol = new TRAY_PREPLANControl();
                    traypreplancontrol.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel1.Controls.Add(traypreplancontrol);
                    break;
                case "托盘安装计划":
                    trayinstallcontrol = new TRAY_INSTALLATIONControl();
                    trayinstallcontrol.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel1.Controls.Add(trayinstallcontrol);
                    break;
                default:
                    break;
            }
            

        }

        /// <summary>
        /// 填充combobox
        /// </summary>
        /// <param name="tscomb"></param>
        /// <param name="sql"></param>
        public static void ComFill(ToolStripComboBox tscomb, string sql)
        {
            DataSet ds = new DataSet();
            User.DataBaseConnect(sql, ds);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "-所有项目-";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tscomb.ComboBox.Items.Add(ds.Tables[0].Rows[i][0]);
            } 
            ds.Dispose();
        }

        /// <summary>
        /// 选择相关项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectComb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet MyDataSet;
            object obj = this.PlanComb.ComboBox.SelectedItem;
            if (obj == null)
            {
                return;
            }
            TypeNme = this.PlanComb.ComboBox.SelectedItem.ToString();
            if (this.ProjectComb.ComboBox.SelectedIndex == -1)
            {
                return;
            }
            string projectStr = this.ProjectComb.ComboBox.SelectedItem.ToString();
            string wheresql = string.Empty;
            switch (TypeNme)
            {
                case "分段施工计划":
                    MyDataSet = new DataSet();
                    if (projectStr != "-所有项目-")
                    {
                        wheresql = " where PROJECTID = '" + projectStr + "' AND VERSION = 'Y' ORDER BY  ID ASC";
                    }
                    else
                    {
                        wheresql = " WHERE VERSION = 'Y' ORDER BY  ID ASC";
                    }
                    sqlStr = "SELECT PROJECTID 项目号,BLOCKNO 分段号, DRAWINGNO 图号, PLAN_BLOCK_PRE_START 计划分段预制开始, PLAN_BLOCK_PRE_FINISH 计划分段预制结束, ACTUAL_BLOCK_PRE_START 实际分段预制开始, ACTUAL_BLOCK_PRE_FINISH 实际分段预制结束, PLAN_BLOCK_ASS_START 计划分段合拢开始, PLAN_BLOCK_ASS_FINISH 计划分段合拢结束, ACTUAL_BLOCK_ASS_START 实际分段合拢开始, ACTUAL_BLOCK_ASS_FINISH 实际分段合拢结束, PREPAREDBY 录入人, PREPAREDATE 录入日期 , REMARK 备注 FROM BLOCKCONSTRUCTIONPLAN_TAB  ";
                    sqlStr = sqlStr + wheresql; 
                    User.DataBaseConnect(sqlStr, MyDataSet);
                    this.PlanDGV.DataSource = MyDataSet.Tables[0].DefaultView;
                    MyDataSet.Dispose();
                    break;
                case "托盘材料纳期计划":
                    MyDataSet = new DataSet();
                    if (projectStr != "-所有项目-")
                    {
                        wheresql = " where PROJECTID = '" + projectStr + "' AND VERSION = 'Y' ORDER BY  ID ASC";
                    }
                    else
                    {
                        wheresql = " WHERE VERSION = 'Y' ORDER BY  ID ASC";
                    }
                    sqlStr = "SELECT PROJECTID 项目号,TRAYNO 托盘编号,BLOCKNO 分段号,REFER_DRAWING 参考图,TRAY_WEIGHT 托盘重量,TRAY_DELIVERYTIME_START 托盘纳期开始时间,TRAY_DELIVERYTIME_END 托盘纳期结束时间,WEEK 第XX周,WEEKDAY 周X,REAL_TRAYNO 实托盘号,RESPONSIBLE_USER 负责人,PROGRESS_STATE 进度状态, PREPAREDBY 录入人,PREPAREDATE 录入日期 FROM TRAY_MATERIAL_DELIVERYTIME_TAB ";
                    sqlStr = sqlStr + wheresql; 
                    User.DataBaseConnect(sqlStr, MyDataSet);
                    this.PlanDGV.DataSource = MyDataSet.Tables[0].DefaultView;
                    MyDataSet.Dispose();
                    break;
                case "托盘预制计划":
                    MyDataSet = new DataSet();
                    if (projectStr != "-所有项目-")
                    {
                        wheresql = " where PROJECT_ID = '" + projectStr + "' ORDER BY  ID ASC";
                    }
                    else
                    {
                        wheresql = " ORDER BY  ID ASC";
                    }
                    sqlStr = "SELECT PROJECT_ID 项目号,TRAYNO 托盘编号,BLOCKNO 分段号,REFER_DRAWING 参考图,SPOOL_NUMBER 小票数量, TRAY_WEIGHT 托盘重量,PREPLAN_STARTTIME 托盘预制计划开始,PREPLAN_ENDTIME 托盘预制计划结束,WEEK 第XX周,WEEKDAY 周X,WEEK_REAL_HOUR 本周实动工时,SUM_REAL_HOUR 累计实动工时,HOUR_PERCENT 工时消耗百分比,PROGRESS_PERCENT 进度完成百分比,PROGRESS_HOUR 进度完成工时,KPI KPI,DATA_SOURCE 数据来源进展状态,PROGRESS_STATE 进展状态, PREPAREDBY 录入人,PREPAREDATE  录入日期 FROM TRAY_PREPLAN_TAB ";
                    sqlStr = sqlStr + wheresql; 
                    User.DataBaseConnect(sqlStr, MyDataSet);
                    this.PlanDGV.DataSource = MyDataSet.Tables[0].DefaultView;
                    MyDataSet.Dispose();
                    break;
                case "托盘安装计划":
                    MyDataSet = new DataSet();
                    if (projectStr != "-所有项目-")
                    {
                        wheresql = " where PROJECT_ID = '" + projectStr + "' ORDER BY  ID ASC";
                    }
                    else
                    {
                        wheresql = " ORDER BY  ID ASC";
                    }
                    sqlStr = "SELECT PROJECT_ID 项目号,TRAYNO 托盘编号,BLOCKNO 分段号,REFER_DRAWING 参考图, SPOOL_NUMBER 小票数量,TRAY_WEIGHT 托盘重量,INSTALLATION_STARTTIME 安装开始时间,INSTALLATION_ENDTIME 安装结束时间,WEEK 第XX周,WEEKDAY 周X,WEEK_REAL_HOUR 本周实动工时,SUM_REAL_HOUR 累计实动工时,HOUR_PERCENT 工时消耗百分比,PROGRESS_PERCENT 进度完成百分比, PROGRESS_HOUR  进度完成工时,KPI ,DATA_SOURCE 数据来源, PROGRESS_STATE 进展状态,PREPAREDBY 录入人, PREPAREDATE   录入时间 FROM TRAY_INSTALLATION_TAB";
                    sqlStr = sqlStr + wheresql; 
                    User.DataBaseConnect(sqlStr, MyDataSet);
                    this.PlanDGV.DataSource = MyDataSet.Tables[0].DefaultView;
                    MyDataSet.Dispose();
                    break;
                //case "托盘材料配套明细":
                //    MyDataSet = new DataSet();
                //    if (projectStr != "-所有项目-")
                //    {
                //        wheresql = " where PROJECT_ID = '" + projectStr + "' ORDER BY  ID ASC";
                //    }
                //    else
                //    {
                //        wheresql = " ORDER BY  ID ASC";
                //    }
                //    sqlStr = "SELECT PROJECT_ID 项目号,PROJECT_NAME 项目名称,TRAYNO 托盘编号,TYPE_NAME 类别,TRAY_DELIVERY_PLACE 托盘收纳地点,RESPONSIBLE_USER 负责人,REAL_TRAYNO 实托盘号,ISSUED_TIME 下发时间,ACCEPT_USER 接收人,BLOCKNO 分段号,TRAY_DELIVERYTIME_START 托盘纳期开始,TRAY_DELIVERYTIME_END 托盘纳期结束,INGREDIENT_USER 配料负责人,MODULE_NAME 名称,DRAWINGNO 图号,FORMAT 规格,LENGTH 长度,NUM 数量,WEIGHT 重量,MEO MEO,PICKING_BILL 领料单,ISSUED_STATE 下发状态,PICKING_USER 领料人,UPDATE_TIME 更新时间 FROM TRAY_MATERIAL_DETAIL_TAB ";
                //    sqlStr = sqlStr + wheresql; 
                //    User.DataBaseConnect(sqlStr, MyDataSet);
                //    this.PlanDGV.DataSource = MyDataSet.Tables[0].DefaultView;
                //    MyDataSet.Dispose();
                //    break;
                //case "托盘预制明细":
                //    MyDataSet = new DataSet();
                //    if (projectStr != "-所有项目-")
                //    {
                //        wheresql = " where PROJECT_ID = '" + projectStr + "' ORDER BY  ID ASC";
                //    }
                //    else
                //    {
                //        wheresql = " ORDER BY  ID ASC";
                //    }
                //    sqlStr = "SELECT PROJECT_ID 项目号,TRAYNO 托盘编号,PRE_STARTTIME 托盘预制计划开始,PRE_ENDTIME 托盘预制计划结束,PRE_DEPARTMENT 托盘预制单位,INSTALL_DELIVERYTIME_START 托盘安装纳期计划开始,INSTALL_DELIVERYTIME_END 托盘安装纳期计划结束,CHECK_USER 托盘收检负责人,SYSTEM_NAME 系统名称,MODULE_NAME 组件名称,SPOOLNO 标识号,DRAWINGNO 图号,MAIN_MATERIAL 主材,FORMAT 规格,WEIGHT 重量,TEST_PRESS 测试压力,MATERIAL_STATE 下料状态,MATERIAL_CHECK 下料验收,MATERIAL_HOUR 下料工时,ELBOW_STATE 弯管状态,ELBOW_CHECK 弯管验收,ELBOW_HOUR 弯管工时,INSTALL_STATE 组装状态,INSTALL_CHECK 组装验收,INSTALL_HOUR 组装工时,WELDING_STATE 焊接状态,WELDING_CHECK 焊接验收,WELDING_HOUR 焊接工时,POLISHED_STATE 打磨状态,POLISHED_CHECK 打磨验收,POLISHED_HOUR 打磨工时,PRESS_STATE 试压状态,PRESS_CHECK 试压验收,PRESS_HOUR 试压工时,SURFACE_TREATMENT_STATE 表面处理状态,SURFACE_TREATMENT_CHECK 表面处理验收,SURFACE_TREATMENT_HOUR 表面处理工时,REMARK 备注,TABLENO 表单号,TRANSFER_DATE 交接日期 FROM TRAY_PRE_DETAIL_TAB ";
                //    sqlStr = sqlStr + wheresql; 
                //    User.DataBaseConnect(sqlStr, MyDataSet);
                //    this.PlanDGV.DataSource = MyDataSet.Tables[0].DefaultView;
                //    MyDataSet.Dispose();
                //    break;
                //case "托盘安装明细":
                //    MyDataSet = new DataSet();
                //    if (projectStr != "-所有项目-")
                //    {
                //        wheresql = " where PROJECT_ID = '" + projectStr + "' ORDER BY  ID ASC";
                //    }
                //    else
                //    {
                //        wheresql = " ORDER BY  ID ASC";
                //    }
                //    sqlStr = "SELECT PROJECT_ID 项目号,TRAYNO 托盘编号,ISSUED_STARTTIME 托盘下发计划开始,ISSUED_ENDTIME 托盘下发计划结束,RECEIVED_DEPARTMENT 托盘接收单位,TRANSFER_DATE 交接日期,INSTALLATION_STARTTIME 托盘安装计划开始,INSTALLATION_ENDTIME 托盘安装计划结束,TRANSFER_USER 托盘交接负责人,RATED_HOUR 额定工时,SYSTEM_NAME 系统名称,MODULE_NAME 组件名称,SPOOLNO 标识号,DRAWINGNO 图号,MAIN_MATERIAL 主材,FORMAT 规格,WEIGHT 重量,TEST_PRESS 测试压力,MATERIAL_STATE 下料状态,MATERIAL_CHECK 下料验收,MATERIAL_HOUR 下料工时,LOCATE_STATE 弯管状态,LOCATE_CHECK 弯管验收,LOCATE_HOUR 弯管工时,WELDING_STATE 焊接状态,WELDING_CHECK 焊接验收,WELDING_HOUR 焊接工时,POLISHED_STATE 打磨状态,POLISHED_CHECK 打磨验收,POLISHED_HOUR 打磨工时,NDT_STATE NDT状态,NDT_CHECK NDT验收,NDT_HOUR NDT工时,PRESS_STATE 试压状态,PRESS_CHECK 试压验收,PRESS_HOUR 试压工时,REMARK 备注,TABLENO 表单号,UPDATE_TIME 交接日期 FROM TRAY_INSTALL_DETAIL_TAB ";
                //    sqlStr = sqlStr + wheresql; 
                //    User.DataBaseConnect(sqlStr, MyDataSet);
                //    this.PlanDGV.DataSource = MyDataSet.Tables[0].DefaultView;
                //    MyDataSet.Dispose();
                //    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 保存计划到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {

            if (this.splitContainer1.Panel1.Controls.Count == 0)
            {
                return;
            }
            string TrayNo = string.Empty;
            string blockStr = string.Empty;
            string drawingStr = string.Empty;
            decimal trayWeight = 0;
            int weekNo = 0;
            string weekDay = string.Empty;
            string markStr = string.Empty;
            string createrStr = User.cur_user;

            DialogResult result;
            result = MessageBox.Show("确定要保存吗？", "信息提示！", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                string obj = this.ProjectComb.ComboBox.Text.ToString();
                if (string.IsNullOrEmpty(obj) || obj == "-所有项目-")
                {
                    MessageBox.Show("请选择项目！","信息提示！",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                string project = obj;
                string GroupBoxName = this.splitContainer1.Panel1.Controls[0].Name.ToString();
                switch (GroupBoxName)
                {
                    case "BlockContructionControl":
                        blockStr = blockcontrol.textBox1.Text.ToUpper().ToString();
                        drawingStr = blockcontrol.textBox2.Text.ToUpper().ToString();
                        if (blockStr == string.Empty || drawingStr == string.Empty)
                        {
                            MessageBox.Show("请确定没有空值存在！","信息提示！",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return;
                        }
                        DateTime planStart, planFinish, actualStart, actualFinish, planAssStart, planAssFinish, actualAssStart, actualAssFinish;
                        if (blockcontrol.dateTimePicker1.Checked == true)
                        {
                            planStart = blockcontrol.dateTimePicker1.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (blockcontrol.dateTimePicker2.Checked == true)
                        {
                            planFinish = blockcontrol.dateTimePicker2.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (blockcontrol.dateTimePicker3.Checked == true)
                        {
                            actualStart = blockcontrol.dateTimePicker3.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (blockcontrol.dateTimePicker4.Checked == true)
                        {
                            actualFinish = blockcontrol.dateTimePicker4.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (blockcontrol.dateTimePicker5.Checked == true)
                        {
                            planAssStart = blockcontrol.dateTimePicker5.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (blockcontrol.dateTimePicker6.Checked == true)
                        {
                            planAssFinish = blockcontrol.dateTimePicker6.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (blockcontrol.dateTimePicker7.Checked == true)
                        {
                            actualAssStart = blockcontrol.dateTimePicker7.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (blockcontrol.dateTimePicker8.Checked == true)
                        {
                            actualAssFinish = blockcontrol.dateTimePicker8.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        markStr = blockcontrol.textBox3.Text.ToString();
                        PlanDBConnection.BlockConstructionTab(project, blockStr, drawingStr, planStart, planFinish, actualStart, actualFinish, planAssStart, planAssFinish, actualAssStart, actualAssFinish, createrStr, markStr);
                        MessageBox.Show("数据已经保存成功！","信息提示！",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        break;
                        
                    case "TRAY_MATERIAL_DELIVERYTIMEControl":
                        DateTime timeStart, timeFinish;
                        TrayNo = traymaterialcontrol.textBox1.Text.ToUpper().ToString();
                        blockStr = traymaterialcontrol.textBox2.Text.ToUpper().ToString();
                        drawingStr = traymaterialcontrol.textBox3.Text.ToUpper().ToString();
                        if (traymaterialcontrol.textBox4.Text != string.Empty)
                        {
                            trayWeight = Convert.ToDecimal(traymaterialcontrol.textBox4.Text.ToString());
                        }
                        else
                        {
                            trayWeight = 0;
                        }
                        if (traymaterialcontrol.textBox5.Text != string.Empty)
                        {
                            weekNo = Convert.ToInt16(traymaterialcontrol.textBox5.Text.ToString());
                        }
                        else
                        {
                            weekNo = 0;
                        }
                        weekDay = traymaterialcontrol.textBox6.Text.ToString();
                        string realTrayNo = traymaterialcontrol.textBox7.Text.ToUpper().ToString();
                        string charger = traymaterialcontrol.textBox8.Text.ToString();
                        string status = traymaterialcontrol.textBox9.Text.ToString();
                        if (traymaterialcontrol.dateTimePicker1.Checked == true)
                        {
                            timeStart = traymaterialcontrol.dateTimePicker1.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (traymaterialcontrol.dateTimePicker2.Checked == true)
                        {
                            timeFinish = traymaterialcontrol.dateTimePicker2.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        markStr = traymaterialcontrol.textBox10.Text.ToString();
                        if (TrayNo == "" || blockStr == "" || drawingStr == "" || trayWeight == 0 || weekNo ==0 || weekDay =="" || realTrayNo == "" || charger == "" || status == "")
                        {
                            MessageBox.Show("请确定没有空值存在！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        PlanDBConnection.TrayMaterialDeliveryTimeTab(project, TrayNo, blockStr, drawingStr, trayWeight, weekNo, weekDay, realTrayNo, charger, status, timeStart, timeFinish, markStr, createrStr);
                        MessageBox.Show("数据已经保存成功！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case "TRAY_PREPLANControl":
                        TrayNo = traypreplancontrol.textBox1.Text.ToUpper().ToString();
                        blockStr = traypreplancontrol.textBox2.Text.ToUpper().ToString();
                        drawingStr = traypreplancontrol.textBox3.Text.ToUpper().ToString();
                        int spoolcount = 0;
                        if (traypreplancontrol.textBox4.Text != string.Empty)
                        {
                            spoolcount = Convert.ToInt16(traypreplancontrol.textBox4.Text.ToString());
                        }

                        if (traypreplancontrol.textBox5.Text != string.Empty)
                        {
                            trayWeight = Convert.ToDecimal(traypreplancontrol.textBox5.Text.ToString());
                        }

                        int manHour = 0;
                        if (traypreplancontrol.textBox6.Text != string.Empty)
                        {
                            manHour = Convert.ToInt16(traypreplancontrol.textBox6.Text.ToString());
                        }

                        if (traypreplancontrol.textBox7.Text != string.Empty)
                        {
                            weekNo = Convert.ToInt16(traypreplancontrol.textBox7.Text.ToString());
                        }

                        weekDay = traypreplancontrol.textBox8.Text.ToString();

                        decimal realManHour = 0;
                        if (traypreplancontrol.textBox9.Text != string.Empty)
                        {
                            realManHour = Convert.ToDecimal(traypreplancontrol.textBox9.Text.ToString());
                        }

                        int totalRealManHour = 0;
                        if (traypreplancontrol.textBox10.Text != string.Empty)
                        {
                            totalRealManHour = Convert.ToInt16(traypreplancontrol.textBox10.Text.ToString());
                        }

                        string ManHourPotential = traypreplancontrol.textBox11.Text.ToString();
                        string ProgressPotential = traypreplancontrol.textBox12.Text.ToString();


                        decimal completeManHour = 0;
                        if (traypreplancontrol.textBox13.Text != string.Empty)
                        {
                            completeManHour = Convert.ToDecimal(traypreplancontrol.textBox13.Text.ToString());
                        }


                        string kpiStr = traypreplancontrol.textBox14.Text.ToString();
                        string dataSoure = traypreplancontrol.textBox15.Text.ToString();
                        string ProStatus = traypreplancontrol.textBox16.Text.ToString();

                        if (TrayNo == "" || blockStr == "" || drawingStr == "" || spoolcount == 0 || trayWeight == 0 || manHour == 0)
                        {
                            MessageBox.Show("请确定没有空值存在！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DateTime prefabricateStart, prefabricateFinish;
                        if (traypreplancontrol.dateTimePicker1.Checked == true)
                        {
                            prefabricateStart = traypreplancontrol.dateTimePicker1.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (traypreplancontrol.dateTimePicker2.Checked == true)
                        {
                            prefabricateFinish = traypreplancontrol.dateTimePicker2.Value.Date;
                        }
                        else
                        {
                            return;
                        }

                        PlanDBConnection.TrayPrePlanTab(project, TrayNo, blockStr, drawingStr, spoolcount, trayWeight, manHour, weekNo, weekDay, realManHour, totalRealManHour, ManHourPotential, ProgressPotential, completeManHour, kpiStr, dataSoure, ProgressPotential, prefabricateStart, prefabricateFinish,createrStr);
                        MessageBox.Show("数据已经保存成功！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case "TRAY_INSTALLATIONControl":
                        TrayNo = trayinstallcontrol.textBox1.Text.ToUpper().ToString();
                        blockStr = trayinstallcontrol.textBox2.Text.ToUpper().ToString();
                        drawingStr = trayinstallcontrol.textBox3.Text.ToUpper().ToString();
                        int nspoolcount = 0;
                        if (trayinstallcontrol.textBox4.Text != string.Empty)
                        {
                            nspoolcount = Convert.ToInt16(trayinstallcontrol.textBox4.Text.ToString());
                        }

                        if (trayinstallcontrol.textBox5.Text != string.Empty)
                        {
                            trayWeight = Convert.ToDecimal(trayinstallcontrol.textBox5.Text.ToString());
                        }

                        int nmanHour = 0;
                        if (trayinstallcontrol.textBox6.Text != string.Empty)
                        {
                            nmanHour = Convert.ToInt16(trayinstallcontrol.textBox6.Text.ToString());
                        }

                        if (trayinstallcontrol.textBox7.Text != string.Empty)
                        {
                            weekNo = Convert.ToInt16(trayinstallcontrol.textBox7.Text.ToString());
                        }

                        weekDay = trayinstallcontrol.textBox8.Text.ToString();

                        decimal nrealManHour = 0;
                        if (trayinstallcontrol.textBox9.Text != string.Empty)
                        {
                            nrealManHour = Convert.ToDecimal(trayinstallcontrol.textBox9.Text.ToString());
                        }

                        int ntotalRealManHour = 0;
                        if (trayinstallcontrol.textBox10.Text != string.Empty)
                        {
                            ntotalRealManHour = Convert.ToInt16(trayinstallcontrol.textBox10.Text.ToString());
                        }

                        string nManHourPotential = trayinstallcontrol.textBox11.Text.ToString();
                        string nProgressPotential = trayinstallcontrol.textBox12.Text.ToString();


                        decimal ncompleteManHour = 0;
                        if (trayinstallcontrol.textBox13.Text != string.Empty)
                        {
                            ncompleteManHour = Convert.ToDecimal(trayinstallcontrol.textBox13.Text.ToString());
                        }


                        string nkpiStr = trayinstallcontrol.textBox14.Text.ToString();
                        string ndataSoure = trayinstallcontrol.textBox15.Text.ToString();
                        string nProStatus = trayinstallcontrol.textBox16.Text.ToString();

                        if (TrayNo == "" || blockStr == "" || drawingStr == "" || nspoolcount == 0 || trayWeight == 0 || nmanHour == 0)
                        {
                            MessageBox.Show("请确定没有空值存在！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DateTime nprefabricateStart, nprefabricateFinish;
                        if (trayinstallcontrol.dateTimePicker1.Checked == true)
                        {
                            nprefabricateStart = trayinstallcontrol.dateTimePicker1.Value.Date;
                        }
                        else
                        {
                            return;
                        }
                        if (trayinstallcontrol.dateTimePicker2.Checked == true)
                        {
                            nprefabricateFinish = trayinstallcontrol.dateTimePicker2.Value.Date;
                        }
                        else
                        {
                            return;
                        }

                        PlanDBConnection.TrayPrePlanTab(project, TrayNo, blockStr, drawingStr, nspoolcount, trayWeight, nmanHour, weekNo, weekDay, nrealManHour, ntotalRealManHour, nManHourPotential, nProgressPotential, ncompleteManHour, nkpiStr, ndataSoure, nProgressPotential, nprefabricateStart, nprefabricateFinish, createrStr);
                        MessageBox.Show("数据已经保存成功！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;

                    //case "TRAY_MATERIAL_DETAILControl":
                    //    break;

                    //case "TRAY_PRE_DETAILControl":
                    //    break;

                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 激活窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkShopPlan_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkShopPlan_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }
    }
}