using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Text;
using System.Net.Mail;
using System.Data.OleDb;
using jzpl.Lib;

namespace JP
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //GVSubmitData.DataSource = DBHelper.createGridView("select * from gen_pkg_arr_v where check_mark='init' ");
            //GVSubmitData.DataKeyNames = new string[] { "arrived_id"};
            //GVSubmitData.DataBind();

            Response.Write("abc".IndexOf("1"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MailAddress from_ = new MailAddress("yias@yantai-raffles.net");
            MailAddress to_ = new MailAddress("yuanjun.zhang@yantai-raffles.net");
            MailMessage mm = new MailMessage(from_, to_);
          
            mm.Subject = "集中配料系统TEST";
            mm.Body = "TEST";
            mm.Attachments.Add(new Attachment(HttpRuntime.AppDomainAppPath + "\\log\\log.txt"));
            SmtpClient ss = new SmtpClient();

            try
            {
                ss.Send(mm);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            } 
        }

        protected void sendMail_Click(object sender, EventArgs e)
        {            
            try
            {
                jzpl.Lib.Mail.SendTest();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }            
        }

        protected void BtnStopTimer_Click(object sender, EventArgs e)
        {
            jzpl.Lib.TimeRun.Instance().Stop();
        }

        protected void BtnStart_Click(object sender, EventArgs e)
        {
            jzpl.Lib.TimeRun.Instance().Start();
        }

        protected void temp_Click(object sender, EventArgs e)
        {
            StringBuilder cmdText = new StringBuilder("select a.part_no,a.mtr_no,PART_DESCRIPTION,part_unit,a.dms_issue_qty,b.erp_issue_qty  from (");
            
            cmdText.Append("select part_no,PART_DESCRIPTION,part_unit,matr_seq_no mtr_no,sum(nvl(issued_qty,0)) dms_issue_qty from jp_requisition t where rowstate in ('released','finished') ");
            cmdText.Append(string.Format(" and (finish_time is null or (finish_time>to_date('{0}','yyyy-mm-dd') and finish_time<to_date('{1}','yyyy-mm-dd')+1)) ", "2009-01-01", "2009-10-01"));
            cmdText.Append(" group by part_no,matr_seq_no,PART_DESCRIPTION,part_unit) a,(select t.sequence_no,sum(quantity) erp_issue_qty from ifsapp.inventory_transaction_hist2@erp_prod t ");
            cmdText.Append(" where t.dated>=to_date('2009-9-1','yyyy-mm-dd')  and t.transaction_code='PROJISS' group by t.sequence_no ) b ");
            cmdText.Append(" where a.mtr_no=b.sequence_no(+)");  
            cmdText.Append(" and a.dms_issue_qty <> nvl(b.erp_issue_qty,0)");
            
            //jzpl.Lib.Misc.DBDataToTxtFile(cmdText.ToString(), @"e:\t1.txt");
            jzpl.Lib.Misc.DBDataToXls(cmdText.ToString(), @"e:\");
        }

        protected void test1_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            OleDbCommand cmd = new OleDbCommand("jp_public.new_", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("v_matr_seq_no", OleDbType.VarChar).Value = "100583698";
            cmd.Parameters.Add("v_matr_seq_line_no", OleDbType.VarChar).Value = "1";
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                Misc.Message(Response, ex.ErrorCode.ToString()+ex.Message);
                
            }
        }

        
        
    }
}
