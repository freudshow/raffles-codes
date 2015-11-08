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
    public partial class wzxqjh_query_ext : System.Web.UI.Page
    {
        private string m_perimission;
        private BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
        protected void Page_Load(object sender, EventArgs e)
        {            
            Authentication auth = new Authentication(this);
            if (auth.LoadSession() == false)
            {
                auth.RemoveSession();
                Response.Redirect("../../UI/FrameUI/login.htm");
                Response.End();
            }
            else
            {
                m_perimission = ((Authentication.LOGININFO)Session["USERINFO"]).Permission;
                
                if(CheckAccessAble())
                {
                    if (!IsPostBack)
                    {
                        DdlProjectBind();
                        DdlProdSiteBind();
                        DdlReqGroupBind();
                        DdlReceiptDeptBind();
                    }
                }
                else
                {
                    auth.RemoveSession();
                    Response.Redirect("../../UI/FrameUI/login.htm");
                    Response.End();
                }
            }
        }

        protected Boolean CheckAccessAble()
        {
            if (m_perimission[(int)Authentication.PERMDEFINE.PART_JP_QUERY1] == '1') return true;
            return false;
        }

        protected void DdlProdSiteBind()
        {
            DdlProdSite.DataSource = Lib.DBHelper.createDDLView("select place_id,place_name from jp_receipt_place  where state='1'");
            DdlProdSite.DataTextField = "place_name";
            DdlProdSite.DataValueField = "place_id";
            DdlProdSite.DataBind();
        }

        protected void DdlReceiptDeptBind()
        {
            DdlReceiptDept.DataSource = Lib.DBHelper.createDDLView("select dept_id,company||' '||dept_desc dept_desc from jp_receipt_dept t where state='1'");
            DdlReceiptDept.DataTextField = "dept_desc";
            DdlReceiptDept.DataValueField = "dept_id";
            DdlReceiptDept.DataBind();
        }

        protected void DdlReqGroupBind()
        {
            DdlReqGroup.DataSource = DBHelper.createDDLView("select group_id,company_id||'  '||group_name group_name from jp_req_group where state='1'");
            DdlReqGroup.DataTextField = "group_name";
            DdlReqGroup.DataValueField = "group_id";
            DdlReqGroup.DataBind();
        }

        protected void DdlProjectBind()
        {
            //查询页面，不限制项目选择
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, string.Empty);            
        }
        

        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            GVDataBind();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataBind();
        }

        protected void BtnHtmlToExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=wuzixuqiu.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GVData.Columns[15].Visible = false;

            GVData.AllowPaging = false;
            
            GVDataBind();
            
            GVData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            GVData.AllowPaging = true;
            GVData.Columns[15].Visible = true;
            GVDataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void GVDataBind()
        {
            DataView dv;
            StringBuilder sqlstr = new StringBuilder("select a.requisition_id, a.matr_seq_no, a.matr_seq_line_no, a.part_no, a.part_description, a.require_qty,a.issued_qty, a.project_id, a.project_description, a.project_block, a.project_system,a.work_content, a.place, a.place_description, a.receiver, a.receiver_ic, a.receive_date, a.receive_date_str, a.receiver_contact, a.crane, a.recorder, a.record_time, a.record_time_str, a.finish_time, a.finish_time_str, a.contract, a.rowstate, a.rowstate_zh, a.stop, a.part_unit, a.release_qty, a.release_time, a.release_date, a.jjd_qty,a.release_time_str, a.lack_msg, a.receipt_dept, a.req_group, a.req_group_name, a.objid, a.rowversion, a.release_user, a.receipt_dept_name, a.receipt_company, a.jjd_no,");
            if (DDL_Ration.SelectedValue == "yes")
            {
                sqlstr.Append(" a.ration_qty ration_qty,");
            }
            else {
                sqlstr.Append(" ('未查询') ration_qty,");
            
            }
            sqlstr.Append("IFS_DATA_API.GET_BAY_NO_FOR_PART(a.part_no,a.contract,a.project_id) location ");
            sqlstr.Append(" from jp_requisition_vw a ");
            sqlstr.Append(" where 1=1 ");
            if (DdlProject.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and project_id = '{0}'", DdlProject.SelectedValue));
            }
            if (DdlProdSite.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and place='{0}' ", DdlProdSite.SelectedValue));
            }
            if (TxtLocation.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and IFS_DATA_API.GET_BAY_NO_FOR_PART(part_no,contract,project_id)='{0}'", TxtLocation.Text.Trim()));
            }
            if (TxtMtrSeqNo.Text.Trim() != "")
            {
                if (TxtMtrSeqNo.Text.IndexOf("%") != -1)
                    sqlstr.Append(string.Format(" and matr_seq_no like '{0}'", TxtMtrSeqNo.Text.Trim()));
                else
                    sqlstr.Append(string.Format(" and matr_seq_no = '{0}'", TxtMtrSeqNo.Text.Trim()));
            }
            if (TxtMtrLineNo.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and matr_seq_line_no='{0}'", TxtMtrLineNo.Text.Trim()));
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                if (TxtPartNo.Text.IndexOf("%") != -1)
                    sqlstr.Append(string.Format(" and part_no like '{0}'", TxtPartNo.Text.Trim()));
                else
                    sqlstr.Append(string.Format(" and part_no = '{0}'", TxtPartNo.Text.Trim()));
            }
            if (TxtRecieveDate.Text.Trim() != "" || TxtRecDataEnd.Text.Trim() != "")
            {
                if (TxtRecDataEnd.Text.Trim() == TxtRecieveDate.Text.Trim())
                {
                    sqlstr.Append(string.Format(" and receive_date = to_date('{0}','yyyy-mm-dd')", TxtRecieveDate.Text.Trim()));
                }
                else
                {
                    sqlstr.Append(string.Format(" and receive_date >= to_date('{0}','yyyy-mm-dd') and receive_date <= to_date('{1}','yyyy-mm-dd')",
                        TxtRecieveDate.Text.Trim() == "" ? "2010-01-01" : TxtRecieveDate.Text, TxtRecDataEnd.Text.Trim() == "" ? string.Format("{0:yyyy-MM-dd}", DateTime.Now) : TxtRecDataEnd.Text));

                }
                
            }
            if (TxtReleaseDateS.Text.Trim() != "" || TxtReleaseDateE.Text.Trim() != "")
            {
                if (TxtReleaseDateS.Text.Trim() == TxtReleaseDateE.Text.Trim())
                {
                    sqlstr.Append(string.Format(" and release_date = to_date('{0}','yyyy-mm-dd')", TxtReleaseDateE.Text.Trim()));
                }
                else
                {
                    sqlstr.Append(string.Format(" and release_date >= to_date('{0}','yyyy-mm-dd') and release_date < to_date('{1}','yyyy-mm-dd')",
                        TxtReleaseDateS.Text.Trim() == "" ? "2010-01-01" : TxtReleaseDateS.Text, TxtReleaseDateE.Text.Trim() == "" ? string.Format("{0:yyyy-MM-dd}", DateTime.Now) : TxtReleaseDateE.Text));

                }

            }
            if (TxtReciever.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and receiver = '{0}'", TxtReciever.Text.Trim()));
            }
            if (TxtRowstate.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and rowstate in ({0})", StateWhereString(TxtRowstate.Text.Trim())));
            }
            if (DdlDz.SelectedValue != "all")
            {
                sqlstr.Append(string.Format(" and crane ='{0}'", DdlDz.SelectedValue == "Y" ? "1" : "0"));
            }
            if (DdlReqGroup.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and req_group='{0}'", DdlReqGroup.SelectedValue));
            }
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (TxtRecorder.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and recorder = '{0}'", TxtRecorder.Text));
            }
            if (TxtReleaseUser.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and release_user='{0}'", TxtReleaseUser.Text));
            }
            if (Txtfd.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and project_block like'{0}'", Txtfd.Text));
            }
            if (Txtxt.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and project_system like '{0}'", Txtxt.Text));
            }
            if (Txtsg.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and work_content like '{0}'", Txtsg.Text));
            }
            if (TxtReqDate.Text != "")
            {
                sqlstr.Append(string.Format(" and to_date(to_char(record_time,'yyyy-mm-dd'),'yyyy-mm-dd') = to_date('{0}','yyyy-mm-dd')", TxtReqDate.Text));
            }

            dv = DBHelper.createGridView(sqlstr.ToString());
            GVData.DataSource = dv;
            GVData.DataKeyNames = new string[] { "requisition_id" };
            GVData.DataBind();
        }

        protected string StateWhereString(string states)
        {
            string states_ = states.Trim().ToUpper();
            StringBuilder ret = new StringBuilder();
            string[] states__ = states_.Split(new char[] { ';', ' ', ',' });
            
            for (int i = 0; i < states__.Length; i++)
            {
                ret.Append(string.Format("'{0}'", CovertStateCharToString(states__[i])));
                if (i < states__.Length-1 )
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
                default:
                    break;
            }
            return null;
        }

        //private string 
     

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = e.Row.FindControl("ChkCrane") as CheckBox;
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["crane"].ToString() == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }        
    }
}
