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
    public partial class receipt_person : System.Web.UI.Page
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
            if (m_perimission[(int)Authentication.PERMDEFINE.BS_RCPT_PER] == '1') return true;
            return false;
        }       

        private void bindGV()
        {
            GV.DataSource = DBHelper.createGridView("select * from jp_receipt_person");
            GV.DataKeyNames = new string[] { "id" };
            GV.DataBind();
        }

        protected void BtnAddCompany_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("jp_receipt_person_api.new_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("v_receipt_name", OleDbType.VarChar).Value = TxtName.Text;
                cmd.Parameters.Add("v_receipt_ic", OleDbType.VarChar).Value = TxtIC.Text;
                cmd.Parameters.Add("v_receipt_contact", OleDbType.VarChar).Value = TxtContact.Text;
                cmd.Parameters.Add("v_id", OleDbType.VarChar,20).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
            }
            bindGV();
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
                string newContact = ((TextBox)(GV.Rows[e.RowIndex].FindControl("GV_TxtContact"))).Text;
                cmd.CommandText = string.Format("update jp_receipt_person set state='{0}',contact='{1}' where id='{2}'", newActive, newContact,GV.DataKeys[e.RowIndex].Values[0].ToString());
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
                    ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除此项吗?')");
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
                cmd.CommandText = string.Format("delete from jp_receipt_person where id='{0}'", GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.ExecuteNonQuery();
            }
            bindGV();
        }
    }
}