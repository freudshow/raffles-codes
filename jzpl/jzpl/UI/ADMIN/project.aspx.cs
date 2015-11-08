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
    public partial class project : System.Web.UI.Page
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
                        Panel1.Visible = false;
                        PnlTop.Visible = true;
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
            if (m_perimission[(int)Authentication.PERMDEFINE.BS_PROJECT] == '1') return true;
            return false;
        }
       
        private void bindGV()
        {
            GV.DataSource = DBHelper.createGridView("select * from jp_project order by project_id desc");
            GV.DataKeyNames = new string[] { "project_id" };
            GV.DataBind();
        }

        protected void BtnAddCompany_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {               
                try
                {
                    OleDbCommand cmd = new OleDbCommand("jp_project_api.new_");
                    cmd.Connection = conn;
                    cmd.Parameters.Add("v_project_id", OleDbType.VarChar).Value = TxtProjectID.Text;
                    cmd.Parameters.Add("v_project_name", OleDbType.VarChar).Value = TxtProjectName.Text;
                    cmd.Parameters.Add("v_short_name", OleDbType.VarChar).Value = txt_short_name.Text;
                   
                    if (conn.State != ConnectionState.Open) conn.Open();
                    cmd.ExecuteNonQuery(); 
                    Page.RegisterClientScriptBlock("clientscript", "<script>alert('数据保存成功！')</script>");
                    bindGV();
                }
                catch(Exception ex)
                {
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        //Page.RegisterClientScriptBlock("clientscript","<script>
                    }
                    else
                    {
                        throw;
                    }
                   
                }               
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
                OleDbDataReader reader;
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open) conn.Open();
                string newPlaceDescription = ((TextBox)GV.Rows[e.RowIndex].FindControl("TxtDesc")).Text;
                string shortName = ((TextBox)GV.Rows[e.RowIndex].FindControl("TxtShort")).Text;
                string newActive = ((CheckBox)(GV.Rows[e.RowIndex].FindControl("ChkActive"))).Checked == true ? "1" : "0";
                cmd.CommandText = string.Format("update jp_project set project_name='{0}',state='{1}',short_name='{2}' where project_id='{3}'", newPlaceDescription, newActive, shortName, GV.DataKeys[e.RowIndex].Values[0].ToString());
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
                cmd.CommandText = string.Format("delete from jp_project where project_id='{0}'", GV.DataKeys[e.RowIndex].Values[0].ToString());
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            //GV.Visible = false;
            PnlTop.Visible = false;
            this.Panel1.Visible = true;
            bindGV1();
        }

        private void bindGV1()
        {
            GridView1.DataSource = DBHelper.createGridView("select project_id,name project_name from ifsapp.project@erp_prod where probability_to_win <> 1 and project_id not in (select project_id from jp_project)");
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindGV1();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //GV.Visible = true;
            PnlTop.Visible = true;
            this.Panel1.Visible = false;
            bindGV();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("jp_project_api.insert_",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbTransaction odt;
            odt = conn.BeginTransaction();
            cmd.Transaction = odt;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("ChkActive");
                if (cb.Checked)
                {
                    string project_id = gvr.Cells[1].Text;
                    string project_desc = gvr.Cells[2].Text;
                    string sql_judge = "select count(*) from jp_project where project_id='"+project_id+"'";
                    int count = DBHelper.getCount(sql_judge);
                    if (count <= 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("v_project_id", OleDbType.VarChar, 20).Value = project_id;
                        cmd.Parameters.Add("v_project_name", OleDbType.VarChar, 300).Value = project_desc;
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            odt.Rollback();
                            string error = ex.Message.Substring(0, ex.Message.IndexOf("."));
                            Response.Write("<script language=javascript>alert('操作错误，原因为：" + error.Substring(0, 30) + "')</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('你导入的项目编号:" + project_id + "已存在,请重新导入!')</script>");
                        return;
                    }
                }
            }
            odt.Commit();
            conn.Close();
            conn.Dispose();
            cmd.Dispose();
            Page.RegisterClientScriptBlock("clientscript", "<script>alert('数据导入成功！')</script>");
            bindGV();
            //GV.Visible = true;
            PnlTop.Visible = true;
            Panel1.Visible = false;
            
        }
    }
}
