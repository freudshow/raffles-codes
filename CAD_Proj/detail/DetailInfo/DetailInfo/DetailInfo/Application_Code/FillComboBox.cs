using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Collections;
using System.Windows.Forms;

namespace DetailInfo.Application_Code
{
    class FillComboBox
    {
        public static void GetFlowStatus(ComboBox cbox, string sql)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            try
            {
                //OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                OracleDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    cbox.Items.Add(datareader[0].ToString());
                }

                datareader.Close();
                //conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //return;
            }
            finally { conn.Close(); }
        }

        public static void FillComb(ComboBox cb, string sql)
        {
            try
            {
                OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    cb.Items.Clear();
                    cb.Items.Add("");
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                    conn.Close();
                    dr.Close();
                }
            }
            catch (OracleException ox)
            {
                MessageBox.Show(ox.Message.ToString());
                return;
            }
        }
      
    }
}
