using System;
using System.Collections.Generic;
using System.Text;

namespace DetailInfo
{
    class DataAccess
    {
        //public const string OIDSConnStr = "Data Source=oids;User ID=plm;Password=123!feed;Unicode=True";
        public static string severstr = string.Empty;

        public static string OIDSConnStr =  string.Empty;    

        public static void GetSeverName(string sql)
        {
            severstr = sql;
            OIDSConnStr = "Data Source=" + severstr + ";User ID=plm;Password=123!feed;Unicode=True";
        }

    }
}
