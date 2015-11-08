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
using jzpl.Lib;
using System.Text;
using System.IO;

namespace jzpl
{
    public partial class wzxqjh_iss_compare : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataBind();
        }

        protected void GVDataBind()
        {
            GV1.DataSource = GVData();
            GV1.DataBind();
        }

        protected DataView GVData()
        {
            DataView dv_;
            string submit_date_start;
            string submit_date_end;
            DateTime date1;
            DateTime date2;
            StringBuilder cmdText = new StringBuilder("select a.part_no,a.mtr_no,PART_DESCRIPTION,part_unit,a.dms_issue_qty,b.erp_issue_qty  from (");            
            
            if (TxtDate1.Text != "" && TxtDate2.Text != "")
            {
                submit_date_start = TxtDate1.Text;
                submit_date_end = TxtDate2.Text;
            }
            else
            {
                if (TxtDate1.Text == "" && TxtDate2.Text == "")
                {
                    date1 = new DateTime(1981, 1, 1);
                    date2 = new DateTime(2099, 1, 1);
                    submit_date_start = date1.ToString("yyyy-MM-dd");
                    submit_date_end = date2.ToString("yyyy-MM-dd");
                }
                else if (TxtDate1.Text == "")
                {
                    date1 = new DateTime(1981, 1, 1);
                    submit_date_start = date1.ToString("yyyy-MM-dd");
                    submit_date_end = TxtDate2.Text;
                }
                else
                {
                    date2 = new DateTime(2099, 1, 1);
                    submit_date_start = TxtDate1.Text;
                    submit_date_end = date2.ToString("yyyy-MM-dd");
                }
            }
            cmdText.Append("select part_no,PART_DESCRIPTION,part_unit,matr_seq_no mtr_no,sum(nvl(issued_qty,0)) dms_issue_qty from jp_requisition t where rowstate in ('released','finished') ");
            cmdText.Append(string.Format(" and (finish_time is null or (finish_time>to_date('{0}','yyyy-mm-dd') and finish_time<to_date('{1}','yyyy-mm-dd')+1)) ", submit_date_start, submit_date_end));
            cmdText.Append(" group by part_no,matr_seq_no,PART_DESCRIPTION,part_unit) a,(select t.sequence_no,sum(quantity) erp_issue_qty from ifsapp.inventory_transaction_hist2@erp_prod t ");
            cmdText.Append(" where t.dated>=to_date('2009-9-1','yyyy-mm-dd')  and t.transaction_code='PROJISS' group by t.sequence_no ) b ");
            cmdText.Append(" where a.mtr_no=b.sequence_no(+)");
            if (ChkOnlyNotMatch.Checked)
            {
                cmdText.Append(" and a.dms_issue_qty <> nvl(b.erp_issue_qty,0)");
            }
            
            dv_ = Lib.DBHelper.createGridView(cmdText.ToString());
            return dv_;
        }

        protected void GV1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }

        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            if (GV1.Rows.Count < 1)
            {
                Misc.Message(Response, "无数据要导出！");
                return;
            }
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=wuzixuqiu.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GV1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();           
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }       

    }
}
