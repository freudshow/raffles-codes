using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Windows.Forms;

namespace DetailInfo
{
    class WorkShopClass
    {
        public static void GetSpoolTrace(string queryString,string project, string drawing, DataGridView dgv)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters["project_in"].Direction = System.Data.ParameterDirection.Input;

            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters["drawing_in"].Direction = System.Data.ParameterDirection.Input;

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet myDS = new DataSet();
            adapter.Fill(myDS, "test");
            dgv.DataSource = myDS.Tables["test"];
            myDS.Dispose();
        }
        public static void GetMaterialReports(string queryString, string projectid, string block,  DataGridView dgv)
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

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet myDS = new DataSet();
            adapter.Fill(myDS, "test");
            dgv.DataSource = myDS.Tables["test"];
            myDS.Dispose();
        }

        public static string GetDrawingWithBlock(string projectid, string block)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_GetDrawingWithBlock";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("drawing_out", OracleType.VarChar,100);
            cmd.Parameters["drawing_out"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters.Add("block_in", OracleType.VarChar).Value = block;

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
            return cmd.Parameters["drawing_out"].Value.ToString();
        }

        public static void GetModifyDelInfo(string queryString, string projectid, string drawing, DataGridView dgv,int indicator)
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
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters["drawing_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("indicator_in", OracleType.Number).Value = indicator;
            cmd.Parameters["indicator_in"].Direction = System.Data.ParameterDirection.Input;

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet myDS = new DataSet();
            adapter.Fill(myDS, "test");
            dgv.DataSource = myDS.Tables["test"];
            myDS.Dispose();
        }

        public static void TraceabilityIII(string queryString, string project, string block, string sysid, DataGridView dgv)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters["project_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("block_in", OracleType.VarChar).Value = block;
            cmd.Parameters["block_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("sysid_in", OracleType.VarChar).Value = sysid;
            cmd.Parameters["sysid_in"].Direction = System.Data.ParameterDirection.Input;

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet myDS = new DataSet();
            adapter.Fill(myDS, "test");
            dgv.DataSource = myDS.Tables["test"];
            myDS.Dispose();
        }

        public static void GetModifyStatistics(string queryString, string project, string drawing, int indicator,DataGridView dgv)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters["project_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters["drawing_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("indicator_in", OracleType.Number).Value = indicator;
            cmd.Parameters["indicator_in"].Direction = System.Data.ParameterDirection.Input;

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet myDS = new DataSet();
            adapter.Fill(myDS, "test");
            dgv.DataSource = myDS.Tables["test"];
            myDS.Dispose();
        }
    }
}
