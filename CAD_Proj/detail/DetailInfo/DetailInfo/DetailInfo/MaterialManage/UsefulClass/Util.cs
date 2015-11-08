using System;
using System.Web;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DreamStu.Common;
using DreamStu.Common.Reg;
using System.ComponentModel;


namespace Framework
{
    public class Util
    {
        /// <summary>
        /// 将日期字符串处理成Oracle中的日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetOracelDateSql(string dateTime)
        {
            return GetOracelDateSql(dateTime, false);
        }

        /// <summary>
        /// 将日期处理成Oracle中的日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetOracelDateSql(DateTime dateTime)
        {
            return GetOracelDateSql(dateTime, false);
        }

        /// <summary>
        /// 将日期字符串处理成Oracle中的日期（加一天）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="addOneDay"></param>
        /// <returns></returns>
        public static string GetOracelDateSql(string dateTime, bool addOneDay)
        {
            return GetOracelDateSql(DateTime.Parse(dateTime), addOneDay);
        }

        /// <summary>
        /// 将日期处理成Oracle中的日期（加一天）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="addOneDay"></param>
        /// <returns></returns>
        public static string GetOracelDateSql(DateTime dateTime, bool addOneDay)
        {
            if (addOneDay)
                return "to_date('" + dateTime.AddDays(1d).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
            else
                return "to_date('" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetOraDateSql(string field)
        {
            return "to_date(" + field + ",'yyyy-mm-dd hh24:mi:ss')";
        }

        /// <summary>
        /// 去除字符串中的HTML代码
        /// </summary>
        /// <param name="html">字符串</param>
        /// <returns></returns>
        public static string StripHtmlCode(string html)
        {
            string stroutput = html;
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            stroutput = regex.Replace(stroutput, string.Empty);
            return stroutput;
        }

        /// <summary>
        /// JS特殊字符过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JsStrReplace(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return str.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\"", "\\\"").Replace("'", "\\'");
        }


        /// <summary>
        /// 转换字符串为首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertFirstUpper(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            string str1 = str.Substring(0, 1);
            string str2 = str.Substring(1, str.Length - 1);
            string retStr = str1.ToUpper() + str2.ToLower();
            return retStr.Replace("_a", " A");
        }

        public static bool IsNumberic(string s)
        {
            string text1 = @"^\-?[0-9]+$";
            return Regex.IsMatch(s, text1);
            //return Regex.IsMatch(s, RegexManager.GetRegex(ValidateType.IntZeroPostive).Regex);
        }

        /// <summary>
        /// 剔除重复的Item
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string DistinctItem(string strArray, string separator)
        {
            List<string> list = new List<string>();
            foreach (string s in strArray.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }
            return String.Join(separator, list.ToArray());
        }

        public static string GetPreviousMonth(string month)
        {
            return Convert.ToDateTime(month).AddMonths(-1).ToString("yyyy-MM");
        }


        public static bool IsSameFormParameter(string p1, string p2)
        {
            string[] a_p1 = p1.Split(','); string[] a_p2 = p2.Split(',');
            if (a_p1.Length != a_p2.Length) return false;
            foreach (string pp1 in a_p1)
            {
                if (!DreamStu.Common.Util.IsArrayContainStr(a_p2, pp1))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
