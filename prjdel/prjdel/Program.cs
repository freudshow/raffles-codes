using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.IO;
using System.Data;

namespace prjdel
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 314; i++)
            //{
            //    if (DeckIsRefD(i))
            //    {
            //        System.Console.WriteLine(" found : "+ i );
            //    }
            //    else
            //        System.Console.WriteLine(" Not found : " + i);                
            //}

            //DataTable std_table = GetTable("GB5783.csv");
            
            //for (int i = 0; i < std_table.Rows.Count; i++)
            //{
            //    for (int j = 0; j < std_table.Columns.Count; j++)
            //    {
            //        System.Console.Write(std_table.Rows[i][j]);
            //        System.Console.Write("\t");
            //        System.Diagnostics.Debug.Write(std_table.Rows[i][j]);
            //        System.Diagnostics.Debug.Write("\t");
            //    }
            //    System.Console.WriteLine("\n");
            //    System.Diagnostics.Debug.WriteLine("\n");
            //}
            //System.Console.WriteLine(CalcDN(12.5));
            while(true)
            {
                System.Console.WriteLine("input length:");

                double length = Convert.ToDouble(System.Console.ReadLine());
                System.Console.WriteLine("input ScrewPitch:");
                double ScrewPitch = Convert.ToDouble(System.Console.ReadLine());
                System.Console.WriteLine(length + "\t" + ScrewPitch);
                //for (int i = 3; i < 6;i++ )
                //{
                //    System.Console.WriteLine(i + ":");
                //    double L = length + i * ScrewPitch;
                //    System.Console.WriteLine(L);
                //    System.Console.WriteLine(L % 5);
                //    System.Console.WriteLine(L - L%5);
                //}
                double L = length + 3 * ScrewPitch;
                System.Console.WriteLine(L);
                System.Console.WriteLine(CalcLength(length, ScrewPitch));
                if (System.Console.ReadLine()=="0")
                {
                    break;
                }
            }
            //System.Console.ReadKey();
        }

        static double CalcLength(double length, double ScrewPitch)
        {
            double RecommendLength = length + 3 * ScrewPitch;
            int i = 0;
            while ((RecommendLength - (RecommendLength % 5) + i * 5) < RecommendLength)
            {
                i++;
            }
            return (RecommendLength - (RecommendLength % 5) + i * 5);
        }

        static string CalcDN(double Hole)
        {
            DataTable std_table = GetTable("GB5783.csv");
            int i = 0;
            while ( Hole > Convert.ToDouble(std_table.Rows[i][0]) )
            {
                i++;
            }
            return std_table.Rows[i][0].ToString();
        }

        static DataTable GetTable(string TextTableName)
        {
            DataTable std_table = new DataTable();
            StreamReader sr = new StreamReader(TextTableName);
            string txt = sr.ReadLine();
            string[] heads = txt.Split(',');
            for (int i = 0; i < heads.Length; i++)
            {
                std_table.Columns.Add(heads[i]);
            }

            while (!sr.EndOfStream)
            {
                txt = sr.ReadLine();
                string[] datas = txt.Split(',');
                DataRow DTRow = std_table.NewRow();
                for (int i = 0; i < datas.Length; i++)
                {
                    DTRow[heads[i]] = datas[i];
                }
                std_table.Rows.Add(DTRow);
            }
            sr.Close();
            return std_table;
        }

        public static bool PrjIsRefD(int prj_id)
        {
            try
            {
                /*
                 * 得到引用过PROJECT_ID字段的表(查找每个表的字段中有无类似PROJECT_ID字样)
                 */
                OracleConnection OraCon = new OracleConnection("Data Source=oidsnew;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM USE_PROJECTID_TABLES_VIEW T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                /*
                 * 遍历每个表
                 * {
                 *      表中的PROJECT_ID字样的列中查找是否出现项目号
                 *      如果找到此项目号，则返回 true
                 *      如果没有，则继续查找下一个表
                 * }
                 * 默认返回 false
                 */
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + prj_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool BlkIsRefD(int blk_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_blockid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + blk_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool SysIsRefD(int sys_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_systemid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + sys_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool DeckIsRefD(int Deck_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_deckid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + Deck_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool RoomIsRefD(int room_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_roomid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + room_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool ZoneIsRefD(int zone_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_zoneid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + zone_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool DiscipIsRefD(int Discip_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_disciplineid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + Discip_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool DepartIsRefD(int Depart_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_departmentid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + Depart_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool PosIsRefD(int pos_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_positionid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + pos_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }

        public static bool UserIsRefD(int user_id)
        {
            try
            {
                OracleConnection OraCon = new OracleConnection("Data Source=oids;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM use_userid_tables_view T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + user_id + ")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.Message);
                return true;
            }
        }
    }
}
