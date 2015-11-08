using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Data;

namespace DetailInfo
{
    class WorkShopStatusFlow
    {
        public static void UpdatePersonStatus( string queryString, string project, string drawing, string spool, string person, DateTime dt, string frmtxt)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            //string queryString = "SP_UpdateBlankingStatus";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            cmd.Parameters.Add("person_in", OracleType.VarChar).Value = person;
            cmd.Parameters.Add("datetime_in", OracleType.DateTime).Value = dt;
            cmd.Parameters.Add("frmtxt_in", OracleType.VarChar).Value = frmtxt;
            

            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
 
        }

        public static void UpdateStatus(string queryString, string project, string drawing, string spool, DateTime dt, string frmtxt)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            //string queryString = "SP_UpdateBlankingStatus";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            cmd.Parameters.Add("datetime_in", OracleType.DateTime).Value = dt;
            cmd.Parameters.Add("frmtxt_in", OracleType.VarChar).Value = frmtxt;


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

        public static void AddTrayORClass(string queryString, string project, string drawing, string spool,string trayorclass)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters.Add("spool_in", OracleType.VarChar).Value = spool;
            cmd.Parameters.Add("trayorclass_in", OracleType.VarChar).Value = trayorclass;
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

        public static void GetWorkShopReport(string queryString, string projectid, int flag, DataGridView dgv)
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
            cmd.Parameters.Add("flag_in", OracleType.Number).Value = flag;
            cmd.Parameters["flag_in"].Direction = System.Data.ParameterDirection.Input;

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet myDS = new DataSet();
            adapter.Fill(myDS, "test");
            dgv.DataSource = myDS.Tables["test"];
            myDS.Dispose();
        }
    }
}
