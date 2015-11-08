using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace jzpl.Lib
{
    public class TimeRun1
    {
        //#region 数据库操作
        //public string dbType = "0";
        //public SqlConnection conn = new SqlConnection("server=.;uid=sa;pwd=123;database=DBCT_Dev");
      
        //public SqlCommand cmd;
        //public SqlDataAdapter da;
        //关闭所有资源
        //public void Reset()
        //{
        //    cmd.Dispose();
        //    da.Dispose();
        //    conn.Dispose();
        //    conn.Close();
        //}
        // <summary>
        // 根据传入的语句，得到DataSet
        // </summary>
        // <param name="strsql"></param>
        // <returns></returns>
        //public DataSet GetDataSet(string strsql)
        //{
        //    this.Reset();
        //    cmd = new SqlCommand("", conn);
        //    cmd.CommandText = strsql;
        //    DataSet ds = new DataSet();
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    da.Fill(ds);
        //    return ds;
        //}
        // <summary>
        // 根据传入的语句，得到DataTable
        // </summary>
        // <param name="strsql"></param>
        // <returns></returns>
        //public DataTable GetDataTable(string strsql)
        //{
        //    DataSet ds = GetDataSet(strsql);
        //    return ds.Tables[0];
        //}
        // <summary>
        // 语句执行删除，插入，修改操作.
        // </summary>
        // <param name="strsql"></param>
        // <returns></returns>
        //public bool ExecuteSql(string strsql)
        //{
        //    this.Reset();
        //    conn.Close();
        //    conn.Open();
        //    cmd = new SqlCommand("", conn);
        //    cmd.CommandText = strsql;
        //    return cmd.ExecuteNonQuery() != 0;
        //}
        // <summary>
        // 根据传入语句，取得相应数量，一般传入值为select count(*) from table
        // </summary>
        // <param name="strsql"></param>
        // <returns></returns>
        //public int ExecuteScalarSql(string strsql)
        //{
        //    conn.Open();
        //    cmd = new SqlCommand("", conn);
        //    cmd.CommandText = strsql;
        //    return Convert.ToInt32(cmd.ExecuteScalar());
        //    conn.Close();
        //}
        // <summary>
        // 根据传进来的值，取相应的字段
        // </summary>
        // <param name="GetTable">所要查询的表</param>
        // <param name="GetValue">所要得到的字段值</param>
        // <param name="Wherestr">条件语句</param>
        // <returns></returns>
        //public string GetSingleValue(string GetTable, string GetValue, string Wherestr)
        //{
        //    string strsql = "select " + GetValue + " from " + GetTable + " where " + Wherestr + "";
        //    DataTable dt = this.GetDataTable(strsql);
        //    if (dt.Rows.Count > 0)
        //        return dt.Rows[0][0].ToString();
        //    else
        //        return "无信息";
        //}
        //#endregion
        //private static ManualResetEvent allDone = new ManualResetEvent(false);
        //public static string singleip;
        
        //public void getPost(string id)
        //{
        //    singleid 传入的用户信息ID
        //    singleip 根据用户信息ID，而取到的IP
        //    strAction post必传值之一
        //    strCode　传入用户信息表中的code值
        //    string singleid = id;
        //    通过GetSingleValue方法，得到相应的值，这里GetSingleValue（表名，要得到的值，条件语句）
        //    singleip = GetSingleValue("UserCode", "UserIP", "id=" + singleid).Trim();
        //    建立HttpWebRequest
        //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://www2.ip138.com/ips8.asp");
        //    myRequest.Method = "POST";
        //    myRequest.ContentType = "application/x-www-form-urlencoded";
        //    开始调用异步操作
        //    myRequest.BeginGetRequestStream(new AsyncCallback(ReadCallback), myRequest);
        //    allDone.WaitOne();
        //    异步回调方法使用 EndGetResponse 方法返回实际的 WebResponse。 
        //    HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse();
        //    Stream streamResponse = response.GetResponseStream();
        //    StreamReader streamRead = new StreamReader(streamResponse, Encoding.Default);
        //    string content = streamRead.ReadToEnd();
        //    streamResponse.Close();
        //    streamRead.Close();
        //    response.Close();
        //    判断是否是有省市的信息，如果信息中有省，市，则进行存入数据库。其它信息，则为0035进入其它
        //    string singleip = "";
        //    string strCode = "";
        //    if (singleip == "127.0.0.1")
        //    {
        //        strCode = "0008";
        //    }
        //    else
        //    {
        //        if (content.IndexOf("省") == -1 && content.IndexOf("市") == -1 && singleid != "127.0.0.1")
        //        {
        //            if (content.IndexOf("查询太频繁") != -1)
        //            {
        //                strCode = "查询太频繁";
        //            }
        //            else
        //            {
        //                strCode = "0035";
        //            }
        //        }
        //        else
        //        {
        //            if (content.IndexOf("省") != -1)
        //            {
        //                string con = content.Substring(content.IndexOf("本站主数据") + 6, content.IndexOf("</li><li>参考数据一") - content.IndexOf("本站主数据") - 1);
        //                string strpro = con.Substring(0, con.IndexOf("省") + 1);
        //                strCode = GetSingleValue("S_Province", "ProvinceCode", "ProvinceName='" + strpro + "'").Trim();
        //                strCode = strpro;
        //            }
        //            if (content.IndexOf("市") != -1)
        //            {
        //                string con = content.Substring(content.IndexOf("本站主数据") + 6, content.IndexOf("</li><li>参考数据一") - content.IndexOf("本站主数据") - 1);
        //                string strpro = con.Substring(con.IndexOf("省") + 1, con.IndexOf("市") - con.IndexOf("省"));
        //                strCode += GetSingleValue("S_City", "ZipCode", "CityName='" + strpro + "'").Trim(); ;
        //            }
        //        }
        //    }
        //    if (strCode == "")
        //    {
        //        strCode = "0035";
        //    }
        //    将信息存入hashtable
        //    string strsql = "update UserCode set Code='" + strCode + "' where id=" + id + "";
        //    try
        //    {
        //        ExecuteSql(strsql);
        //    }
        //    catch
        //    {
        //    }
        //}
        //private void ReadCallback(IAsyncResult asynchronousResult)
        //{
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
        //        Stream postStream = request.EndGetRequestStream(asynchronousResult);
        //        ASCIIEncoding encoding = new ASCIIEncoding();
        //        string postData = "ip=" + singleip;
        //        postData += "&action=2";
        //        byte[] data = encoding.GetBytes(postData);
        //        postStream.Write(data, 0, postData.Length);
        //        postStream.Close();
        //        allDone.Set();
        //    }
        //    catch
        //    { return; }
        //}
        
    }
}