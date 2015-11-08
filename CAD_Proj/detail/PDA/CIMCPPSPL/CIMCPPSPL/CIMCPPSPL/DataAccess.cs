using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.OleDb;
namespace CIMCPPSPL
{
    public class DataAccess
    {
        public static string fpath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase; 
        /// <summary>
        /// PDA链接字符串
        /// </summary>
        public static string PDAConnStr = "Data Source=" + DataAccess.fpath.Substring(0, DataAccess.fpath.Length - 13) + "MyPDAdata.db3";
        //public static string PDAConnStr = @"Data Source=/My Documents/MyPDAdata.db3";
        /// <summary>
        /// PPSPL链接字符串
        /// </summary>
        public static string PPSPLConnStr = "Data Source=oidsnew ;User ID=plm;Password=123!feed;Unicode=True";
        
    }
}