using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace OutSourcing
{
    public class OracleHelper
    {
        /// <summary>
        /// ORACLE链接字符串
        /// </summary>
        public static string OraConnectString
        {
            get
            {
                return "Data Source=HDMS;User ID=hrdemo;Password=hrdemo";
            }
        }
        /// <summary>
        /// OLEDB链接字符串
        /// </summary>
        public static string OleConnectString
        {
            get
            {
                return "Provider=MSDAORA;Data Source=HDMS;Password=hrdemo;User ID=hrdemo";
            }
        }
        /// <summary>
        /// 返回数据集合
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string SQL)
        {
            OracleConnection conn = new OracleConnection(OraConnectString);
            try
            {
                OracleDataAdapter Oda = new OracleDataAdapter();
                DataSet ds = new DataSet();
                Oda.SelectCommand = new OracleCommand(SQL, conn);
                Oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 返回数据表
        /// </summary>
        /// <param name="SQL">SQL语句</param>
        /// <returns></returns>
        public DataTable GetDataTable(string SQL)
        {
            return GetDataSet(SQL).Tables[0];
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（整数）。
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static int GetCount(string strSQL)
        {
            using (OleDbConnection connection = new OleDbConnection(OleConnectString))
            {
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    OleDbDataReader result = cmd.ExecuteReader();
                    int i = 0;
                    while (result.Read())
                    {
                        i = result.GetInt32(0);
                    }
                    result.Close();
                    return i;
                }
                catch (OleDbException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public static object GetSingle(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(OleConnectString))
            {
                OleDbCommand cmd = new OleDbCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (OleDbException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(string strSQL)
        {
            using (OleDbConnection connection = new OleDbConnection(OleConnectString))
            {
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);
                OleDbDataReader myReader;
                try
                {
                    connection.Open();
                    myReader = cmd.ExecuteReader();
                    return myReader;
                }
                catch (OleDbException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public static DataSet Query(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(OleConnectString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataView
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns></returns>
        public static DataView QuerySQL(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(OleConnectString))
            {
                DataSet ds = new DataSet();
                DataView view = new DataView();
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                    view = ds.Tables["ds"].DefaultView;
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                return view;
            }
        }

        public static DataView Query_(string SQLString)
        {
            return Query(SQLString).Tables[0].DefaultView;
        }
    }
}
