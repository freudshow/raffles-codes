using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;
using System.Timers;
using jzpl.Lib;
using System.Data.OleDb;

namespace jzpl
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                string info = "into Application_Start:";
                string sql = "insert into b(b, c_date) values('" + info + "',sysdate)";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.ExecuteNonQuery();
            }
            jzpl.Lib.TimeRun.Instance().Start();
            int s = 1000 * 20;
            System.Timers.Timer timer = new System.Timers.Timer(s);//20秒
            timer.Elapsed += new System.Timers.ElapsedEventHandler(test_timer);
            
        }
        
        protected void Application_End(object sender, EventArgs e)
        {
            
        }
        //test_timer
        private void test_timer(object sender, ElapsedEventArgs e)
        {

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                string info = "into test_timer:";
                string sql = "insert into b(b, c_date) values('" + info + "',sysdate)";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                if (conn.State != ConnectionState.Open) 
                    conn.Open();
                cmd.ExecuteNonQuery();
            }


        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception err = Server.GetLastError();
            if (err == null)
            {
                err = new Exception("Unknown Error");
            }
            StringBuilder errMsg = new StringBuilder();            
                   
            errMsg.Append(string.Format("<p style='font-size:8pt;'>{0}</p>",err.Message));
            errMsg.Append(string.Format("<p style='font-size:8pt;'>{0}</p>", err.StackTrace));
            errMsg.Append(string.Format("<p style='font-size:8pt;'>{0}</p>", err.InnerException.ToString()));
          
            Application["err"] = errMsg;

            /*//给管理员发邮件*/
            Mail mail = new Mail();
            StringBuilder errBody = new StringBuilder();
            Authentication.LOGININFO user = (Authentication.LOGININFO)Session["USERINFO"];

            errBody.Append(DateTime.Now.ToString());
            errBody.Append("<br>");
            errBody.Append(user.UserID);
            errBody.Append("@");
            errBody.Append(user.ServerName);
            errBody.Append("<br>");
            errBody.Append(err.InnerException.ToString());
            errBody.Append("<br>");
            errBody.Append(err.StackTrace);

            mail.Send("JP ERROR", errBody.ToString(), "yias@yantai-raffles.net", "ming.li@cimc-raffles.com", "", true, false, null, null);        
        }
    }
}