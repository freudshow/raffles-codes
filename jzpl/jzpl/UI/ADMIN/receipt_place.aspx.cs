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
using System.Data.OleDb;

namespace ADMIN
{
    public partial class receipt_place : System.Web.UI.Page
    {
        private string m_perimission;
        private BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
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
                        bindDDL();
                        bindGV();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.BS_RCPT_PLACE] == '1') return true;
            return false;
        }
        
        private void bindDDL()
        {
            baseInfoLoader.CompanyDropDrownListLoad(DDL_company, false, true,true,string.Empty);
            //DDL_company.DataSource = DBHelper.createDDLView("select company_id,company from jp_company where state='1'");
            //DDL_company.DataTextField = "company";
            //DDL_company.DataValueField = "company_id";
            //DDL_company.DataBind();
        }

        private void bindGV()
        {
            string sql = "select company_id, place_id, place_name, state from jp_receipt_place where 1=1";
            if (DDL_company.SelectedValue != "0")
            {
                sql = sql + " and company_id='" + DDL_company.SelectedValue + "'";
            }
            if (TxtCompanyID.Text.Trim() != "")
            {
                sql = sql + " and place_id='" + TxtCompanyID.Text + "'";
            }
            if (TxtCompanyName.Text.Trim() != "")
            {
                sql = sql + " and place_name like '%" + TxtCompanyName.Text + "%'";
            }
            GV.DataSource = DBHelper.createGridView(sql);
            GV.DataKeyNames = new string[] { "place_id" };
            GV.DataBind();
        }

        protected void BtnAddCompany_Click(object sender, EventArgs e)
        {
            if (DDL_company.SelectedValue != "0")
            {
                using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
                {

                    if (Convert.ToInt32(DBHelper.getObject(string.Format("select count(*) from jp_receipt_place where place_id='{0}'", this.TxtCompanyID.Text))) > 0)
                    {
                        Response.Write("<script type='text/javascript'>alert('接收地点编号重复！')</script>");
                        return;
                    }
                    OleDbCommand cmd = new OleDbCommand(string.Format("insert into jp_receipt_place( company_id, place_id, place_name, state) values('{0}','{1}','{2}','1')",  DDL_company.SelectedValue, this.TxtCompanyID.Text, this.TxtCompanyName.Text), conn);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("clientscript", "<script>alert('数据保存成功！')</script>");
                }
                bindGV();
            }
            else
            {
                Page.RegisterClientScriptBlock("clientscript", "<script>alert('清选择公司！')</script>");
                return;
            }
        }

        protected void GV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV.EditIndex = e.NewEditIndex;
            bindGV();
        }

        protected void GV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV.EditIndex = -1;
            bindGV();
        }

        protected void GV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open) conn.Open();
                string newActive = ((CheckBox)(GV.Rows[e.RowIndex].FindControl("ChkActive"))).Checked == true ? "1" : "0";
                cmd.CommandText = string.Format("update jp_receipt_place set state='{0}' where place_id='{1}'", newActive, GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("clientscript", "<script>alert('数据修改成功！')</script>");
            }
            GV.EditIndex = -1;
            bindGV();
        }

        protected void GV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除此项吗?')");
                }
            }
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = GV.Rows[e.RowIndex];
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("delete from jp_receipt_place where place_id='{0}'", GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.ExecuteNonQuery();
            }
            bindGV();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            bindGV();
        }
    }
}
