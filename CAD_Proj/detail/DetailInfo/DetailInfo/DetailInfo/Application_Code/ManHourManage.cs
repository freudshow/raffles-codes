using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Data;

namespace DetailInfo
{
    class ManHourManage
    {

        /// <summary>
        /// 获取小票中管子规格，弯头以及弯管个数
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="projectid"></param>
        /// <param name="spoolname"></param>
        /// <returns></returns>
        public static DataSet GetPipeMaterialDS(string queryString, string projectid, string spoolname)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters["projectid_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spoolname;
            cmd.Parameters["spool_in"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 获取下料的工时系数
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static string GetPreMaterialFacor(double factor)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_GetPreMaterialFacor";
            //OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("lineation_out", OracleType.Number);
            cmd.Parameters["lineation_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("makebevel_out", OracleType.Number);
            cmd.Parameters["makebevel_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("makesyphon_out", OracleType.Number);
            cmd.Parameters["makesyphon_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("factor_in", OracleType.Number).Value = factor;
            try
            {
                cmd.ExecuteNonQuery();
                return cmd.Parameters["lineation_out"].Value.ToString() + "-" + cmd.Parameters["makebevel_out"].Value.ToString() + "-" + cmd.Parameters["makesyphon_out"].Value.ToString();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
                
            }
            finally
            {
                conn.Close();
            }
            //return cmd.Parameters["lineation_out"].Value.ToString() + "-" + cmd.Parameters["makebevel_out"].Value.ToString() + "-" + cmd.Parameters["makesyphon_out"].Value.ToString();
        }

        /// <summary>
        /// 更新指定小票指定材料的下料工时定额
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="spool"></param>
        /// <param name="pipe"></param>
        /// <param name="time"></param>
        public static void UpdateMaterialPrepareTime(string projectid, string spool, string pipe, double time)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_UpdateMaterialPrepareTime";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            cmd.Parameters.Add("pipe_in", OracleType.VarChar).Value = pipe;
            cmd.Parameters.Add("time_in", OracleType.Number).Value = time;

            cmd.Transaction = trans;
            try
            {
                //conn.Open();
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
        /// 根据项目号和图纸号获取小票集合
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="projectid"></param>
        /// <param name="drawing"></param>
        /// <returns></returns>
        public static DataSet GetSpoolDS(string queryString, string projectid, string drawing)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            //string queryString = "SP_GetSpoolStatus";
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters["projectid_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters["drawing_in"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 获取指定小票指定规格管子的数量
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="spool"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static string GetPipeCount(string projectid, string spool, double factor)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_GetPipeCount";
            //OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("pipecount_out", OracleType.Number);
            cmd.Parameters["pipecount_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            cmd.Parameters.Add("factor_in", OracleType.Number).Value = factor;
            try
            {
                cmd.ExecuteNonQuery();
                return cmd.Parameters["pipecount_out"].Value.ToString();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";

            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获取小票上的管材信息
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="spool"></param>
        /// <returns></returns>
        public static string GetPipMaxNorm(string projectid, string spool)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_GetPipMaxNorm";
            //OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("pipenorm_out", OracleType.Number);
            cmd.Parameters["pipenorm_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            try
            {
                cmd.ExecuteNonQuery();
                return cmd.Parameters["pipenorm_out"].Value.ToString();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";

            }
            finally
            {
                conn.Close();
            }
        }

        public static string GetQCORTransORPresFactor(double factor)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_GetQCORTransORPresFactor";
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("qc_out", OracleType.Number);
            cmd.Parameters["qc_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("trans_out", OracleType.Number);
            cmd.Parameters["trans_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("pressure_out", OracleType.Number);
            cmd.Parameters["pressure_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("factor_in", OracleType.Number).Value = factor;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                conn.Close();
            }
            return cmd.Parameters["qc_out"].Value.ToString() + "-" + cmd.Parameters["trans_out"].Value.ToString() + "-" + cmd.Parameters["pressure_out"].Value.ToString();
        }

        /// <summary>
        /// 判断外场或是内场管
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="spool"></param>
        /// <returns></returns>
        public static string JudgePipeCheckField(string projectid, string spool)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_JudgePipeCheckField";
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("filed_out", OracleType.VarChar,5);
            cmd.Parameters["filed_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("spool_in",OracleType.VarChar).Value = spool;
            try
            {
                cmd.ExecuteNonQuery();
                return cmd.Parameters["filed_out"].Value.ToString();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return " ";

            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获取材料规格以及相应规格的个数
        /// </summary>
        /// <param name="query"></param>
        /// <param name="projectid"></param>
        /// <param name="spool"></param>
        /// <returns></returns>
        public static DataSet GetNormCount(string query, string projectid, string spool)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters["projectid_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            cmd.Parameters["spool_in"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            conn.Close();
            return ds;
        }
        /// <summary>
        /// 获取相应材料规格的系数
        /// </summary>
        /// <param name="query"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetMaterialFactor(string fmtxt, string query,double flag)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("factor_out", OracleType.Number);
            cmd.Parameters["factor_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("fmtxt_in", OracleType.VarChar).Value = fmtxt;
            cmd.Parameters.Add("flag_in", OracleType.Number).Value = flag;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return cmd.Parameters["factor_out"].Value.ToString();
        }

        public static string GetMaterialStrFactor(string query, string flag)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("factor_out", OracleType.Number);
            cmd.Parameters["factor_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("flag_in", OracleType.VarChar).Value = flag;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return cmd.Parameters["factor_out"].Value.ToString();
        }


        /// <summary>
        /// 更新小票表里对应小票的工时定额
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="projectid"></param>
        /// <param name="spool"></param>
        /// <param name="time"></param>
        public static void UpdateSpoolQuotaTime(string fmtxt,string projectid, string spool, double time)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_UpdateSpoolQuotaTime";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("fmtxt_in", OracleType.VarChar).Value = fmtxt;
            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            cmd.Parameters.Add("time_in", OracleType.Number).Value = time;

            cmd.Transaction = trans;
            try
            {
                //conn.Open();
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

        public static string GetInPolishFactor(string queryStr, double ratio)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            //string queryStr = "SP_GetInWeldFactor";
            OracleCommand cmd = new OracleCommand(queryStr, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("factor_out", OracleType.Number);
            cmd.Parameters["factor_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("ratio_in", OracleType.Number).Value = ratio;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return cmd.Parameters["factor_out"].Value.ToString();
        }

    }
}
