using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text.RegularExpressions;

namespace PracticeOracle
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (IsRef(4)==1)
            //    System.Console.WriteLine("found");
            //string[] mstr = GetApproveMan("Y001728", "YTRS316-395-030", 0);
            //for( int i=0 ; i < mstr.Length ; i++)
            //    System.Console.WriteLine(mstr[i]);
            decimal result = 0;
            string str = "namakdkd2003";
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace(str, @"[^/d./d]", "");
                // 如果是数字，则转换为decimal类型 
                if (Regex.IsMatch(str, @"^[+-]?/d*[.]?/d*$"))
                {
                    result = decimal.Parse(str);
                }
            }


            string name = @"刘志军";
            string[] tests001 = { "刘志军", "jeck002", "刘志军001", "jeck100 USD", "jeck0.01", "jeck12.03", "jeckU40.112" };
            Regex rrx = new Regex( name + @"\d^\D");
            foreach (string test in tests001)
            {
                if (rrx.IsMatch(test))
                {
                    Console.WriteLine("{0} is a currency value.", test);
                }
                else
                {
                    Console.WriteLine("{0} is not a currency value.", test);
                }
            }



            //// Define a regular expression for currency values.
            //Regex rx = new Regex(@"^-?\d+(\.\d{2})?$");
            //// Define some test strings.
            //string[] tests = { "-42", "19.99", "0.001", "100 USD", "0.01", "12.03", "U40.112" };

            //// Check each test string against the regular expression.
            //foreach (string test in tests)
            //{
            //    if (rx.IsMatch(test))
            //    {
            //        Console.WriteLine("{0} is a currency value.", test);
            //    }
            //    else
            //    {
            //        Console.WriteLine("{0} is not a currency value.", test);
            //    }
            //}


            System.Console.WriteLine("done");
            System.Console.ReadKey();
        }

        private static DataSet GetDataSet()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string Sql = "SELECT T.ID_NO,T.ID_PREFIX, T.NAME, T.USED_NAME, T.CREATE_DATE FROM TABLE T WHERE T.CREATE_DATE BETWEEN :START AND :END";
            DbCommand cmd = db.GetSqlStringCommand(Sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetProjectID";

            return db.ExecuteDataSet(cmd);
        }

        private static int IsRef(int id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sql = "SELECT TABLE_NAME,COLUMN_NAME FROM PLM.USE_PROJECTID_TABLES_VIEW";                
                DbCommand cmd = db.GetSqlStringCommand(sql);
                DataSet Mydata = new DataSet();
                Mydata = db.ExecuteDataSet(cmd);                
                string QueryStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryStr = @"SELECT T." + Mydata.Tables[0].Rows[i]["COLUMN_NAME"] + " FROM " + Mydata.Tables[0].Rows[i]["TABLE_NAME"] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i]["COLUMN_NAME"] + ")=TO_CHAR(" + id + ")";                     
                    
                    if (db.ExecuteDataSet(db.GetSqlStringCommand(QueryStr)).Tables[0].Rows.Count > 0)
                        return 1;
                }
                return 0;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private static string[] GetApproveMan(string IC_Card, string dwg_id, int dwg_edition)
        {            
            return (new mWebReference.Drawing()).GetApproveTemplate(IC_Card, dwg_id, dwg_edition.ToString()).Split(';');            
        }
    }
}