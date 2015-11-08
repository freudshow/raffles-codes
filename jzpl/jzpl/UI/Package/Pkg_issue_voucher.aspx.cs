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
using System.Text;
using jzpl.Lib;

namespace jzpl.UI.Package
{
    public partial class Pkg_issue_voucher : System.Web.UI.Page
    {
        
        private string m_perimission;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
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
                if (CheckAccessAble())
                {
                    if (!IsPostBack)
                    {
                        DdlProdSiteBind();
                        DdlReceiptDeptBind();
                        DdlProjectDataBind();
                        DdlReqGroupDataBind();

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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_ISS_VCHR] == '1') return true;
            return false;
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
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true,string.Empty);
        }

        private void DdlReqGroupDataBind()
        {
            DdlReqGroup.DataSource = DBHelper.createDDLView("select group_id,company_id||' '||group_name group_name from jp_req_group");
            DdlReqGroup.DataTextField = "group_name";
            DdlReqGroup.DataValueField = "group_id";
            DdlReqGroup.DataBind();
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            Session["iss_vchr_req"] = null;
            ArrayList reqids = new ArrayList();
            
            foreach (GridViewRow gvr in GVData.Rows)
            {
                if (((CheckBox)gvr.FindControl("ChkSelect")).Checked)
                {
                    reqids.Add(GVData.DataKeys[gvr.RowIndex].Value.ToString());
                }
            }

            if (reqids.Count > 0)
            {
                Session["iss_vchr_req"] = reqids;
                StringBuilder Html_ = new StringBuilder();
                Html_.Append("<script type='text/javascript'>");
                Html_.Append("\nwindow.open('pkg_iss_vchr_report.aspx','xdreport','height=600px, width=800px, toolbar =no, menubar=no, scrollbars=no, resizable=yes, location=no, status=no').focus()");
                Html_.Append("\n</script>");

                ClientScript.RegisterStartupScript(this.GetType(), "script1", Html_.ToString());
            }
        }

        private void GVDataDataBind()
        {
            string m_ReqID = "";
            StringBuilder sql = new StringBuilder("select * from jp_pkg_requisition_v where 1=1");
            m_ReqID = TxtReqID.Text.Trim();
            //if (TxtReqID.Text.Trim() != "")
            //{
            //    sql.Append(string.Format(" and upper(requisition_id)=upper('{0}')", TxtReqID.Text.Trim()));
            //}
            if (m_ReqID != "")
            {
                string[] _mtrs;
                int _p = m_ReqID.IndexOf("..");
                if (_p > 0)
                {
                    sql.Append(string.Format(" and upper(requisition_id)>='{0}' and upper(requisition_id)<='{1}'", m_ReqID.Substring(0, _p), m_ReqID.Substring(_p + 2)));
                }
                else
                {
                    _mtrs = m_ReqID.Split(';');
                    if (_mtrs.Length > 1)
                    {
                        sql.Append(" and upper(requisition_id) in (");
                        for (int i = 0; i < _mtrs.Length; i++)
                        {
                            sql.Append(string.Format("'{0}'", _mtrs[i]));
                            if (i < _mtrs.Length - 1) sql.Append(",");
                        }
                        sql.Append(")");
                    }
                    else
                    {
                        sql.Append(string.Format(" and upper(requisition_id)=upper('{0}')", m_ReqID));
                    }
                }
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_no)=upper('{0}')", TxtPartNo.Text.Trim()));
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
            if (TxtReqDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and to_char(record_time,'yyyy-mm-dd') = to_char(to_date('{0}','yyyy-mm-dd'),'yyyy-mm-dd')", TxtReqDate.Text));
            }
            if (TxtReqState.Text.Trim() != "")
            {
                sql.Append(string.Format(" and rowstate in ({0})", StateWhereString(TxtReqState.Text.Trim())));
            }
            GVData.DataSource = DBHelper.createGridView(sql.ToString());
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
                case "IU":
                    return "issued";
                default:
                    break;
            }
            return null;
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataDataBind();
        }

        
    }
}
