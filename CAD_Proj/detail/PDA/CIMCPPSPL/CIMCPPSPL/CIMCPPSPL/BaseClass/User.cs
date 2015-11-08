using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace CIMCPPSPL
{
    public  partial  class User
    {
        /// <summary>
        /// 根据SQL语句跟表名来返回SQLITE表内数据集
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="tabname"></param>
        /// <returns></returns>
        public static DataSet GetDataSetCommon(string sqlString,string tabname)
        {
            SQLiteConnection conn = new SQLiteConnection();
            DataSet Ds = new DataSet();
            string connectstr = DataAccess.PDAConnStr;
            conn.ConnectionString = connectstr;
            conn.Open();
            SQLiteDataAdapter dr = new SQLiteDataAdapter(sqlString, conn);
            dr.Fill(Ds, tabname);
            conn.Close();
            return Ds;  
        }
        
        /// <summary>
        /// 根据SQL语句来执行之
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="tabname"></param>
        /// <returns></returns>
        public static int RUNSQLCommon(string sqlString)
        {
            SQLiteConnection conn = new SQLiteConnection();
            string connectstr = DataAccess.PDAConnStr;
            conn.ConnectionString = connectstr;
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sqlString,conn);
            int flag =  cmd.ExecuteNonQuery();
            conn.Close();
            return flag;
        }
        /// <summary>
        /// 检查用户密码问题
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string CheckLogin(string UUser, string PPwd)
        {
            string Querystring = "select username from online_user_tab where username='" + UUser.Trim() + "' and password='" + PPwd.Trim() + "'";
            SQLiteConnection conn = new SQLiteConnection();
            string connectstr = DataAccess.PDAConnStr;
            conn.ConnectionString = connectstr;
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(Querystring, conn);
            SQLiteDataReader Datareader = cmd.ExecuteReader();
            Datareader.Read();
            //Method 1
            //OracleNumber oraclenumber = reader.GetOracleNumber(0);
            //Response.Write("OracleNum " + oraclenumber.ToString());
            //string aa = oraclenumber.ToString();
            //if (aa=="1")
            //{
            //    Response.Redirect("mainform.aspx");
            //    reader.Close();
            //}
            //else
            //{
            //    Response.Write("Username and password do not match!Please try it again!"); 
            //    reader.Close();
            //    TextBox1.Text = "";
            //    TextBox2.Text = "";
            //    TextBox1.Focus();

            //}
            //Method 2;
            if (Datareader.HasRows)
            {
                //Response.Write(cmd.ExecuteScalar().ToString());
                //reader.Close();
                //conn.Close();
                //Response.Redirect("mainform.aspx");
                Datareader.Close();
                conn.Close();
                return "1";

            }
            else
            {
                //Response.Write("Invalid Password or Username,Please Check!!");
                //reader.Close();
                //conn.Close();
                Datareader.Close();
                conn.Close();
                return "2";

            }
        }
    }
}
