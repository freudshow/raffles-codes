using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace DetailInfo
{
    class FillTreeViewFunction
    {
        public static void FillTreeView(TreeNode node, string sql)
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
                    while (dr.Read())
                    {
                        node.Nodes.Add(dr[0].ToString());
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

        public static void FillTree(TreeView tv, string sql)
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
                    while (dr.Read())
                    {
                        tv.Nodes.Add(dr[0].ToString());
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
