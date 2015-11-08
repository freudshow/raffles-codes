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
using System.Text;
using System.Data.OleDb;
using System.IO;

using jzpl.Lib;


namespace jzpl.UI.Package
{
    public partial class pkg_iss_vchr_report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["iss_vchr_req"] == null)
            {
                Misc.Message(this.GetType(), ClientScript, "error:no print item.");
                return;
            }
            PrintPDF();
        }

        private void PrintPDF()
        { 
            ReportDocument rpt_doc = new ReportDocument();
            DataSet ds = new DataSet();
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("select requisition_id,package_no,package_name,part_no,part_name_e part_name,part_spec,require_qty,decode(released_qty,null,require_qty,released_qty) release_qty,");
            sqlstr.Append("issued_qty issue_qty,project_id,rowstate,po_no,contract_no  from jp_pkg_requisition_v");
            sqlstr.Append(string.Format("  where requisition_id in ({0})",GetSeqIdWhereStr()));

            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);           

            cmd.Connection = conn;
            cmd.CommandText = sqlstr.ToString();

            da.Fill(ds);

            rpt_doc.Load(Request.PhysicalApplicationPath + "\\UI\\Report\\CryPkgXd.rpt");
            rpt_doc.SetDataSource(ds.Tables[0]);
           
            //rpt_doc.SetParameterValue("CYjz", objJjd.CyDoc);

            //rpt_doc.PrintOptions.PaperOrientation = PaperOrientation.;
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
        }

        private string GetSeqIdWhereStr()
        {
            StringBuilder where_ = new StringBuilder();
            ArrayList reqids = (ArrayList)Session["iss_vchr_req"];

            for (int i = 0;  i < reqids.Count;i++)
            {
                where_.Append(string.Format("'{0}'",reqids[i].ToString()));
                if (i < reqids.Count - 1)
                {
                    where_.Append(",");
                }
            }

            Session["iss_vchr_req"] = null;

            return where_.ToString();
        }
    }
}
