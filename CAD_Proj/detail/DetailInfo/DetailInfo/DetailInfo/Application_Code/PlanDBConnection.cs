using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace DetailInfo
{
    class PlanDBConnection
    {
        /// <summary>
        /// 分段施工计划
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="block"></param>
        /// <param name="drawing"></param>
        /// <param name="plan_pre_start"></param>
        /// <param name="plan_pre_finish"></param>
        /// <param name="act_pre_start"></param>
        /// <param name="act_pre_finish"></param>
        /// <param name="plan_ass_start"></param>
        /// <param name="plan_ass_finish"></param>
        /// <param name="act_ass_start"></param>
        /// <param name="act_ass_finish"></param>
        /// <param name="username"></param>
        /// <param name="remark"></param>
        public static void BlockConstructionTab(string projectid, string block,string drawing,DateTime plan_pre_start,DateTime plan_pre_finish, DateTime act_pre_start,DateTime act_pre_finish, DateTime plan_ass_start, DateTime plan_ass_finish, DateTime act_ass_start, DateTime act_ass_finish,string username,string remark)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_BlockConstructionTab";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("projectid_in",OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("block_in", OracleType.VarChar).Value = block;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters.Add("plan_pre_start_in", OracleType.DateTime).Value = plan_pre_start;
            cmd.Parameters.Add("plan_pre_finish_in", OracleType.DateTime).Value = plan_pre_finish;
            cmd.Parameters.Add("act_pre_start_in", OracleType.DateTime).Value = act_pre_start;
            cmd.Parameters.Add("act_pre_finish_in",OracleType.DateTime).Value = act_pre_finish;
            cmd.Parameters.Add("plan_ass_start_in",OracleType.DateTime).Value = plan_ass_start;
            cmd.Parameters.Add("plan_ass_finish_in",OracleType.DateTime).Value = plan_ass_finish;
            cmd.Parameters.Add("act_ass_start_in",OracleType.DateTime).Value = act_ass_start;
            cmd.Parameters.Add("act_ass_finish_in",OracleType.DateTime).Value = act_ass_finish;
            cmd.Parameters.Add("username_in",OracleType.VarChar).Value = username;
            cmd.Parameters.Add("remark_in",OracleType.VarChar).Value = remark;

            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 托盘材料纳期计划
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="trayno"></param>
        /// <param name="block"></param>
        /// <param name="drawing"></param>
        /// <param name="weight"></param>
        /// <param name="weekno"></param>
        /// <param name="weekday"></param>
        /// <param name="realtrayno"></param>
        /// <param name="personname"></param>
        /// <param name="state"></param>
        /// <param name="timestart"></param>
        /// <param name="timefinish"></param>
        /// <param name="mark"></param>
        /// <param name="username"></param>
        public static void TrayMaterialDeliveryTimeTab(string projectid, string trayno, string block, string drawing, decimal weight, int weekno,string weekday, string realtrayno, string personname, string state,DateTime timestart, DateTime timefinish, string mark, string username )
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_TrayMaterialDeliveryTimeTab";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("trayno_in", OracleType.VarChar).Value = trayno;
            cmd.Parameters.Add("block_in", OracleType.VarChar).Value = block;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters.Add("weight_in", OracleType.Number).Value = weight;
            cmd.Parameters.Add("weekno_in", OracleType.Number).Value = weekno;
            cmd.Parameters.Add("weekday_in", OracleType.VarChar).Value = weekday;
            cmd.Parameters.Add("realtrayno_in", OracleType.VarChar).Value = realtrayno;
            cmd.Parameters.Add("personname_in", OracleType.VarChar).Value = personname;
            cmd.Parameters.Add("state_in", OracleType.VarChar).Value = state;
            cmd.Parameters.Add("timestart_in", OracleType.DateTime).Value = timestart;
            cmd.Parameters.Add("timefinish_in", OracleType.DateTime).Value = timefinish;
            cmd.Parameters.Add("mark_in", OracleType.VarChar).Value = mark;
            cmd.Parameters.Add("username_in",OracleType.VarChar).Value = username;

            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 托盘预制计划
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="trayno"></param>
        /// <param name="block"></param>
        /// <param name="drawing"></param>
        /// <param name="spcount"></param>
        /// <param name="weight"></param>
        /// <param name="manhour"></param>
        /// <param name="weekno"></param>
        /// <param name="weekday"></param>
        /// <param name="realmanhour"></param>
        /// <param name="totalhour"></param>
        /// <param name="manhourRatio"></param>
        /// <param name="progressRatio"></param>
        /// <param name="completeHour"></param>
        /// <param name="kpi"></param>
        /// <param name="datafrom"></param>
        /// <param name="proState"></param>
        /// <param name="timestart"></param>
        /// <param name="timefinish"></param>
        /// <param name="username"></param>
        public static void TrayPrePlanTab(string projectid, string trayno, string block, string drawing, int spcount, decimal weight, int manhour, int weekno, string weekday, decimal realmanhour, decimal totalhour, string manhourRatio, string progressRatio, decimal completeHour, string kpi, string datafrom, string proState, DateTime timestart, DateTime timefinish, string username)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_TrayPrePlanTab";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("trayno_in", OracleType.VarChar).Value = trayno;
            cmd.Parameters.Add("block_in", OracleType.VarChar).Value = block;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters.Add("spcount_in", OracleType.Number).Value = spcount;
            cmd.Parameters.Add("weight_in", OracleType.Number).Value = weight;
            cmd.Parameters.Add("manhour_in", OracleType.Number).Value = manhour;
            cmd.Parameters.Add("weekno_in", OracleType.Number).Value = weekno;
            cmd.Parameters.Add("weekday_in", OracleType.VarChar).Value = weekday;
            cmd.Parameters.Add("realmanhour_in", OracleType.Number).Value = realmanhour;
            cmd.Parameters.Add("totalhour_in", OracleType.Number).Value = totalhour;
            cmd.Parameters.Add("manhourRatio_in", OracleType.VarChar).Value = manhourRatio;
            cmd.Parameters.Add("progressRatio_in", OracleType.VarChar).Value = progressRatio;
            cmd.Parameters.Add("completeHour_in", OracleType.Number).Value = completeHour;
            cmd.Parameters.Add("kpi_in", OracleType.VarChar).Value = kpi;
            cmd.Parameters.Add("datefrom_in", OracleType.VarChar).Value = datafrom;
            cmd.Parameters.Add("proState", OracleType.VarChar).Value = proState;
            cmd.Parameters.Add("timestart_in", OracleType.DateTime).Value = timestart;
            cmd.Parameters.Add("timefinish_in", OracleType.DateTime).Value = timefinish;
            cmd.Parameters.Add("username_in", OracleType.VarChar).Value = username;

            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 托盘安装计划
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="trayno"></param>
        /// <param name="block"></param>
        /// <param name="drawing"></param>
        /// <param name="spcount"></param>
        /// <param name="weight"></param>
        /// <param name="manhour"></param>
        /// <param name="weekno"></param>
        /// <param name="weekday"></param>
        /// <param name="realmanhour"></param>
        /// <param name="totalhour"></param>
        /// <param name="manhourRatio"></param>
        /// <param name="progressRatio"></param>
        /// <param name="completeHour"></param>
        /// <param name="kpi"></param>
        /// <param name="datafrom"></param>
        /// <param name="proState"></param>
        /// <param name="timestart"></param>
        /// <param name="timefinish"></param>
        /// <param name="username"></param>
        public static void TrayInstallTab(string projectid, string trayno, string block, string drawing, int spcount, decimal weight, int manhour, int weekno, string weekday, decimal realmanhour, decimal totalhour, string manhourRatio, string progressRatio, decimal completeHour, string kpi, string datafrom, string proState, DateTime timestart, DateTime timefinish, string username)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_TrayInstallTab";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("trayno_in", OracleType.VarChar).Value = trayno;
            cmd.Parameters.Add("block_in", OracleType.VarChar).Value = block;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters.Add("spcount_in", OracleType.Number).Value = spcount;
            cmd.Parameters.Add("weight_in", OracleType.Number).Value = weight;
            cmd.Parameters.Add("manhour_in", OracleType.Number).Value = manhour;
            cmd.Parameters.Add("weekno_in", OracleType.Number).Value = weekno;
            cmd.Parameters.Add("weekday_in", OracleType.VarChar).Value = weekday;
            cmd.Parameters.Add("realmanhour_in", OracleType.Number).Value = realmanhour;
            cmd.Parameters.Add("totalhour_in", OracleType.Number).Value = totalhour;
            cmd.Parameters.Add("manhourRatio_in", OracleType.VarChar).Value = manhourRatio;
            cmd.Parameters.Add("progressRatio_in", OracleType.VarChar).Value = progressRatio;
            cmd.Parameters.Add("completeHour_in", OracleType.Number).Value = completeHour;
            cmd.Parameters.Add("kpi_in", OracleType.VarChar).Value = kpi;
            cmd.Parameters.Add("datefrom_in", OracleType.VarChar).Value = datafrom;
            cmd.Parameters.Add("proState", OracleType.VarChar).Value = proState;
            cmd.Parameters.Add("timestart_in", OracleType.DateTime).Value = timestart;
            cmd.Parameters.Add("timefinish_in", OracleType.DateTime).Value = timefinish;
            cmd.Parameters.Add("username_in", OracleType.VarChar).Value = username;

            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        
    }
}
