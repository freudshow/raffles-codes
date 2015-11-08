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

namespace Package
{
    public partial class pkg_jjd_report : System.Web.UI.Page
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
            //sqlstr.Append("select a.jjd_no,a.requisition_id req_id,a.rowstate,a.remark,a.package_no,a.part_no,b.part_name part_name,");
            //sqlstr.Append("b.part_name_e part_name_e,gen_part_package_api.get_package_name(package_no) package_name,");
            //sqlstr.Append("gen_part_package_item_api.get_unit(package_no,part_no) part_unit,gen_part_package_item_api.get_part_spec(package_no,part_no) part_spec,");
            //sqlstr.Append("zh_qty,xh_qty,finish_time,rowversion,project_id proj_id from jp_pkg_jjd_line a,jp_pkg_requisition b where a.requisition_id=b.requisition_id");          
            sqlstr.Append(@"select a.jjd_no,a.requisition_id req_id,a.rowstate,a.remark,a.package_no,a.part_no,       
       b.part_name   part_name, b.part_name_e part_name_e,   b.package_name package_name,
       b.part_unit part_unit, b.part_spec part_spec, b.released_qty xq_qty,a.zh_qty,
       a.xh_qty, a.finish_time, a.rowversion,a.project_id proj_id
  from jp_pkg_jjd_line a, jp_pkg_requisition_v b where a.requisition_id = b.requisition_id");
            sqlstr.Append(string.Format(" and a.jjd_no ='{0}' order by a.part_no", m_jjd_no));

            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            //DeliveryVoucher dvchr = (DeliveryVoucher)Session["delivery_voucher"];

            PkgJjd objJjd = new PkgJjd(m_jjd_no);

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

            rpt_doc.Load(Request.PhysicalApplicationPath + "\\UI\\Report\\CrysPkgJjd.rpt");
            rpt_doc.SetDataSource(ds.Tables[0]);
            //dvchr.SetDeliveryVoucherNo();
            rpt_doc.SetParameterValue("jjd_no", m_jjd_no);
            //rpt_doc.SetParameterValue("kuwei", "");
            rpt_doc.SetParameterValue("place", objJjd.PlaceName);
            rpt_doc.SetParameterValue("receiver", objJjd.ReceiptPerson);
            rpt_doc.SetParameterValue("recieve_date", objJjd.ReceiptDate);
            rpt_doc.SetParameterValue("receiver_contact", objJjd.ReceiptContract);
            rpt_doc.SetParameterValue("ZHd", objJjd.ZhPlace);
            rpt_doc.SetParameterValue("ZHr", objJjd.ZhPerson);
            rpt_doc.SetParameterValue("ZHdh", objJjd.ZhContract);
            rpt_doc.SetParameterValue("ZHArrTime", objJjd.ZhArrTime);
            rpt_doc.SetParameterValue("XQbm", objJjd.XQDept);
            rpt_doc.SetParameterValue("XQlxr", objJjd.XQPerson);
            rpt_doc.SetParameterValue("XQdh", objJjd.XQContract);
            rpt_doc.SetParameterValue("CYgs", objJjd.CyCompany);
            rpt_doc.SetParameterValue("CYr", objJjd.CyPerson);
            rpt_doc.SetParameterValue("CYdh", objJjd.CyContract);
            rpt_doc.SetParameterValue("CYpz", objJjd.CycCarNo);
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

        //#region IBasePage ³ÉÔ±

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