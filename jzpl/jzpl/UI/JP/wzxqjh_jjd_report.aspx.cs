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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using CrystalDecisions.ReportSource;
using System.Data.OleDb;
using System.IO;
using Microsoft.Win32;
using System.Text;
using jzpl.Lib;
namespace jzpl
{
    public partial class wzxqjh_jjd_report : System.Web.UI.Page
    {
        //private string[] m_perimission_array;
        string m_jjd_no;
        protected void Page_Load(object sender, EventArgs e)
        {
            m_jjd_no = Misc.GetHtmlRequestValue(Request, "jjdno");
            //Response.BufferOutput = true;
            //Authentication auth = new Authentication(this);
            //if (auth.LoadSession() == false)
            //{
                //auth.RemoveSession();
                //Response.Redirect("../../UI/FrameUI/login.htm");
                //Response.End();
            //}

            //else
            //{
                //string permission_ = ((Authentication.LOGININFO)Session["USERINFO"]).Permission;
                //m_perimission_array = permission_.Split('|');
                //if (permission_[(int)Authentication.FUN_INTERFACE.wzxqjh_add] == '2')
                //if (CheckAccessAble())
                //{
                    PrintPDF();
                //}
                //else
                //{
                    //auth.RemoveSession();
                    //Response.Redirect("../../UI/FrameUI/login.htm");
                    //Response.End();
                //}
            //}





        }

        //protected Boolean CheckAccessAble()
        //{
        //    if (m_perimission_array[(int)Authentication.FUN_INTERFACE.wzxqjh_jjd_report][0] == '1') return true;
        //    return false;
        //}

        private void PrintPDF()
        {
            ReportDocument rpt_doc = new ReportDocument();
            DataSet ds = new DataSet();
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("select requisition_id req_id,matr_seq_no mtr_no, to_char(matr_seq_line_no)||' ' line_no,");//2013-03-22 ming.li 将数字轻质转换为字符串，避免行号格式为1.00
            sqlstr.Append(" part_no, part_description part_name,project_id proj_id, nvl(project_block,'') proj_block,req_qty qty, part_unit, zh_qty,");//2013-03-22 ming.li 分段为空，打印空格，而不打印错误
            sqlstr.Append(string.Format("xh_qty from jp_jjd_line where jjd_no ='{0}'", m_jjd_no));

            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            //DeliveryVoucher dvchr = (DeliveryVoucher)Session["delivery_voucher"];

            Jjd objJjd = new Jjd(m_jjd_no);

            //sqlstr.Append(" and requisition_id in (");
            //for (int i = 0; i < dvchr.DeliveryItems.Count; i++)
            //{
            //    if (i == dvchr.DeliveryItems.Count - 1)
            //    {
            //        sqlstr.Append(string.Format("'{0}'", dvchr.DeliveryItems[i].ToString()));
            //    }
            //    else
            //    {
            //        sqlstr.Append(string.Format("'{0}',", dvchr.DeliveryItems[i].ToString()));
            //    }
            //}
            //sqlstr.Append(" )");

            cmd.Connection = conn;
            cmd.CommandText = sqlstr.ToString();

            da.Fill(ds);
            
            rpt_doc.Load(Request.PhysicalApplicationPath+"\\UI\\Report\\CrysJj.rpt");
            rpt_doc.SetDataSource(ds.Tables[0]);
            //dvchr.SetDeliveryVoucherNo();
            rpt_doc.SetParameterValue("jjd_no", m_jjd_no);
            //rpt_doc.SetParameterValue("kuwei", "");
            rpt_doc.SetParameterValue("place", objJjd.PlaceName);
            rpt_doc.SetParameterValue("receiver", objJjd.ReceiptPerson);
            rpt_doc.SetParameterValue("recieve_date", objJjd.ReceiptDateStr);
            rpt_doc.SetParameterValue("receiver_contact", objJjd.ReceiptContract);
            rpt_doc.SetParameterValue("ZHd",objJjd.ZhPlace);
            rpt_doc.SetParameterValue("ZHr",objJjd.ZhPerson);
            rpt_doc.SetParameterValue("ZHdh",objJjd.ZhContract);
            rpt_doc.SetParameterValue("ZHArrTime",objJjd.ZhArrTime);
            rpt_doc.SetParameterValue("XQbm",objJjd.XQDept);
            rpt_doc.SetParameterValue("XQlxr",objJjd.XQPerson);
            rpt_doc.SetParameterValue("XQdh",objJjd.XQContract);
            rpt_doc.SetParameterValue("CYgs",objJjd.CyCompany);
            rpt_doc.SetParameterValue("CYr",objJjd.CyPerson);
            rpt_doc.SetParameterValue("CYdh",objJjd.CyContract);
            rpt_doc.SetParameterValue("CYpz",objJjd.CycCarNo);
            rpt_doc.SetParameterValue("CYjz", objJjd.CyDoc);

            rpt_doc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            rpt_doc.PrintOptions.PaperSize = PaperSize.PaperA4;

            using (MemoryStream fp = (MemoryStream)(rpt_doc.ExportToStream(ExportFormatType.PortableDocFormat)))
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(fp.ToArray());
                fp.Close();
                Response.End();
            }
            
            rpt_doc.Close();
            rpt_doc.Dispose();

        }

        //#region IBasePage 成员

        //public string[] GetFun()
        //{
        //    return null;
        //}

        //public string GetPermissionType()
        //{
        //    return Authentication.PERMISSION_TYPE.page_.ToString();
        //}

        //#endregion
    }
}
