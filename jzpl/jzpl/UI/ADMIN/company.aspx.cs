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
    public partial class company : System.Web.UI.Page
    {
        private string m_perimission;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("1---------------------------");
            Response.BufferOutput = true;
            Authentication auth = new Authentication(this);
            if (auth.LoadSession() == false)
            {
                auth.RemoveSession();
                //Response.Write("2------load session false");
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
            if (m_perimission[(int)Authentication.PERMDEFINE.BS_COMPANY] == '1') return true;
            return false;
        }
        
       
        private void bindGV()
        {
            GV.DataSource = DBHelper.createGridView("select * from jp_company");
            GV.DataKeyNames = new string[] { "company_id" };
            GV.DataBind();
        }

        protected void BtnAddCompany_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {

                if (Convert.ToInt32(DBHelper.getObject(string.Format("select count(*) from jp_company where company_id='{0}'", this.TxtCompanyID.Text))) > 0)
                {
                    Response.Write("<script type='text/javascript'>alert('公司编号重复！')</script>");
                    return;
                }
                OleDbCommand cmd = new OleDbCommand(string.Format("insert into jp_company(company_id, company,address, state) values('{0}','{1}','{2}','1')", TxtCompanyID.Text, TxtCompanyName.Text, TxtAddress.Text), conn);
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("clientscript", "<script>alert('数据保存成功！')</script>");
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
                string newPlaceDescription = ((TextBox)GV.Rows[e.RowIndex].FindControl("TxtDesc")).Text;
                string newAddress = ((TextBox)GV.Rows[e.RowIndex].FindControl("TxtAddress")).Text;
                string newActive = ((CheckBox)(GV.Rows[e.RowIndex].FindControl("ChkActive"))).Checked == true ? "1" : "0";
                cmd.CommandText = string.Format("update jp_company set company='{0}',state='{1}',address='{2}' where company_id='{3}'", newPlaceDescription, newActive,newAddress, GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("clientscript", "<script>alert('数据修改成功！')</script>");
            }
            GV.EditIndex = -1;
            bindGV();
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = GV.Rows[e.RowIndex];
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("delete from jp_company where company_id='{0}'", GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.ExecuteNonQuery();
            }
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
    }
}
