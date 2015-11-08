using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace DetailInfo
{
    class Statistics
    {
        public static string GetReturnValue(string str, string sql)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand command = conn.CreateCommand();
            command.CommandText = sql + str;
            string value = command.ExecuteOracleScalar().ToString();
            conn.Close();
            return value;
        }

        public static void ComboBoxIndexChanged(GroupBox gbox)
        {
            foreach (Control ctl in gbox.Controls)
            {
                if (ctl is TextBox)
                {
                    if (ctl.Text == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        ctl.Text = null;
                    }
                }
            }
        }
    }
}
