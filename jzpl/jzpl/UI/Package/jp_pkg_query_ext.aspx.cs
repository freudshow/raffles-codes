
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
using System.Data.OleDb;
using System.IO;

namespace Package
{
    public partial class jp_pkg_query_ext : System.Web.UI.Page
    {
        private BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlProdSiteBind();
                DdlReceiptDeptBind();
                DdlProjectDataBind();
                DdlReqGroupDataBind();

                PreQuery();
            }
        }

        protected void DdlProdSiteBind()
        {
            DdlProdSite.DataSource = DBHelper.createDDLView("select place_id,place_name from jp_receipt_place  where state='1'");
            DdlProdSite.DataTextField = "place_name";
            DdlProdSite.DataValueField = "place_id";
            DdlProdSite.DataBind();
        }

        protected void DdlReceiptDeptBind()
        {
            DdlReceiptDept.DataSource = DBHelper.createDDLView("select dept_id,company||' '||dept_desc dept_desc from jp_receipt_dept t where state='1'");
            DdlReceiptDept.DataTextField = "dept_desc";
            DdlReceiptDept.DataValueField = "dept_id";
            DdlReceiptDept.DataBind();
        }

        private void DdlProjectDataBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, string.Empty);            
        }

        private void DdlReqGroupDataBind()
        {
            DdlReqGroup.DataSource = DBHelper.createDDLView("select group_id,company_id||' '||group_name group_name from jp_req_group");
            DdlReqGroup.DataTextField = "group_name";
            DdlReqGroup.DataValueField = "group_id";
            DdlReqGroup.DataBind();
        }


        private void PreQuery()
        {
            BtnExport.Enabled = false;
        }

        protected string StateWhereString(string states)
        {
            string states_ = states.Trim().ToUpper();
            StringBuilder ret = new StringBuilder();
            string[] states__ = states_.Split(new char[] { ';', ' ', ',' });

            for (int i = 0; i < states__.Length; i++)
            {
                ret.Append(string.Format("'{0}'", CovertStateCharToString(states__[i])));
                if (i < states__.Length - 1)
                {
                    ret.Append(",");
                }
            }
            return ret.ToString();
        }

        protected string CovertStateCharToString(string stateChar)
        {
            switch (stateChar.ToUpper())
            {
                case "I":
                    return "init";
                case "R":
                    return "released";
                case "F":
                    return "finished";
                case "C":
                    return "confirming";
                case "CA":
                    return "cancelled";
                case "IS":
                    return "issued";
                default:
                    break;
            }
            return null;
        }
        

        private void GVDataDataBind()
        {
            StringBuilder sql = new StringBuilder("select * from jp_pkg_requisition_v where 1=1");
            if (TxtReqID.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(requisition_id)=upper('{0}')", TxtReqID.Text.Trim()));
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_no)=upper('{0}')", TxtPartNo.Text.Trim()));
            }
            if (DdlPSType.SelectedValue != "-1")
            {
                sql.Append(string.Format(" and psflag='{0}'", DdlPSType.SelectedValue));
            }
            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id = '{0}'", DdlProject.SelectedValue));
            }
            if (TxtPkgNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(package_no)=upper('{0}')", TxtPkgNo.Text.Trim()));
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(package_name) like upper('{0}')", TxtPkgName.Text));
            }
            if (TxtPartName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_name) like upper('{0}')", TxtPartName.Text));
            }
            if (TxtPartNameE.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_name_e) like upper('{0}')", TxtPartNameE.Text));
            }
            if (TxtPartSpec.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_spec) like upper('{0}')", TxtPartSpec.Text));
            }
            if (DdlProdSite.SelectedValue != "0")
            {
                sql.Append(string.Format(" and place_id='{0}'", DdlProdSite.SelectedValue));
            }
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sql.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (TxtReceiver.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(receiver)=upper('{0}')", TxtReceiver.Text));
            }
            if (TxtBlock.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(project_block) = upper('{0}')", TxtBlock.Text));
            }
            if (TxtSystem.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(project_system)=upper('{0}')", TxtSystem.Text));
            }

            if (TxtWorkContent.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(work_content)=upper('{0}')", TxtWorkContent.Text));
            }
            if (TxtDate.Text.Trim() != "" || TxtRecDateEnd.Text.Trim() != "")
            {
                if (TxtRecDateEnd.Text.Trim() == TxtDate.Text.Trim())
                {
                    sql.Append(string.Format(" and receipt_date = to_date('{0}','yyyy-mm-dd')", TxtDate.Text.Trim()));
                }
                else
                {
                    sql.Append(string.Format(" and receipt_date >= to_date('{0}','yyyy-mm-dd') and receipt_date < to_date('{1}','yyyy-mm-dd')",
                        TxtDate.Text.Trim() == "" ? "2010-01-01" : TxtDate.Text, TxtRecDateEnd.Text.Trim() == "" ? string.Format("{0:yyyy-MM-dd}", DateTime.Now) : TxtRecDateEnd.Text));

                }

            }
            if (TxtDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and receipt_date=to_date('{0}','yyyy-mm-dd')", TxtDate.Text));
            }
            if (TxtIC.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(receiver_ic)=upper('{0}')", TxtIC.Text));
            }
            if (DdlReqGroup.SelectedValue != "0")
            {
                sql.Append(string.Format(" and req_group='{0}'", DdlReqGroup.SelectedValue));
            }
            if (TxtReqUser.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(recorder)=upper('{0}')", TxtReqUser.Text));
            }
            //申请日期改为时间段 TxtReqDate_1
            //if (TxtReqDate.Text.Trim() != "")
            //{
            //    sql.Append(string.Format(" and to_char(record_time,'yyyy-mm-dd') = to_char(to_date('{0}','yyyy-mm-dd'),'yyyy-mm-dd')", TxtReqDate.Text));
            //}
            if (TxtReqDate_1.Text.Trim() != "" || TxtReqDate_2.Text.Trim() != "")
            {
                if (TxtReqDate_2.Text.Trim() == TxtReqDate_1.Text.Trim())
                {
                    sql.Append(string.Format(" and to_char(record_time,'yyyy-mm-dd') = '{0}'", TxtReqDate_1.Text.Trim()));
                }
                else
                {
                    sql.Append(string.Format(" and record_time >= to_date('{0}','yyyy-mm-dd') and record_time < to_date('{1}','yyyy-mm-dd')",
                        TxtReqDate_1.Text.Trim() == "" ? "2010-01-01" : TxtReqDate_1.Text, TxtReqDate_2.Text.Trim() == "" ? string.Format("{0:yyyy-MM-dd}", DateTime.Now) : TxtReqDate_2.Text));

                }
            }

            if (TxtReqState.Text.Trim() != "")
            {
                sql.Append(string.Format(" and rowstate in ({0})", StateWhereString(TxtReqState.Text.Trim())));
            }
            if (TxtKeeper.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(release_user) =upper('{0}')", TxtKeeper.Text));
            }
            if (DdlPsState.SelectedValue == "0")
            {
                //配送完成
                sql.Append(" and nvl(receive_qty,0) >= released_qty and nvl(receive_qty,0)>0");
            }
            if (DdlPsState.SelectedValue == "1")
            {
                //部分配送
                sql.Append(" and nvl(receive_qty,0) < released_qty and nvl(receive_qty,0)>0");
            }
            if (DdlPsState.SelectedValue == "2")
            {
                //未配送
                sql.Append(" and nvl(receive_qty,0)=0");
            }
            if (DdlPsState.SelectedValue == "3")
            {
                //已完成
                sql.Append(" and finish_time>to_date('2000-01-01','yyyy-mm-dd')");
            }
            GVData.DataSource = DBHelper.createGridView(sql.ToString());
            GVData.DataKeyNames = new string[] { "requisition_id" };
            GVData.DataBind();
        }        

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataDataBind();

            if (GVData.Rows.Count > 0) 
                BtnExport.Enabled = true;
            else
                BtnExport.Enabled = false;
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {

            DateTime dt = DateTime.Now;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + string.Format("{0:yyyyMMddHHmmss}", dt) + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GVData.Page.EnableViewState= false;

            GVData.AllowPaging = false;
            GVData.Columns[15].Visible = false;

            GVDataDataBind();

            GVData.RenderControl(htw);
            
            Response.Write(sw.ToString());
            Response.End();

            GVData.Page.EnableViewState = true;
            GVData.AllowPaging = true;
            GVData.Columns[15].Visible = true;

            GVDataDataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            GVDataDataBind();
        }

    }
}