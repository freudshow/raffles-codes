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
    public partial class project_acc_per : System.Web.UI.Page
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
                        bindGVuser();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.BS_PRJ_ACC] == '1') return true;
            return false;
        }        
        
        private void bindGVuser()
        {           
            DdlUser.DataSource = DBHelper.createDDLView("select user_id,user_id user_text from jp_user");
            DdlUser.DataTextField = "user_text";
            DdlUser.DataValueField = "user_id";
            DdlUser.DataBind();
           
        }      

        private void bindGVproject(string user)
        {
            string sql = @"select a.project_id,a.project_name,decode(b.project_id,'','0','%','1','1') judge from
                                (select project_id,project_name from jp_project where state='1'
                                union
                                select '%','所有项目' from dual) a,

                                (select project_id from jp_project_access_person where user_id='" +user+"') b"+
                                @" where a.project_id=b.project_id(+)
                                order by 1";
            gv_project.DataSource = DBHelper.createGridView(sql);
            gv_project.DataKeyNames = new string[] { "project_id" };
            gv_project.DataBind();
        }

        protected void BtnAddCompany_Click(object sender, EventArgs e)
        {
            if (DdlUser.SelectedValue == "0")
            {
                Misc.Message(this.GetType(), ClientScript, "保存失败，请选择用户。");
                return;
            }
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("jp_project_access_person_api.deleteUser_", conn);
                OleDbTransaction odt = conn.BeginTransaction();
                cmd.Transaction = odt;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("v_user_id", OleDbType.VarChar, 50).Value = DdlUser.SelectedValue;
                try
                {
                    cmd.ExecuteNonQuery();

                    Boolean AllProject = false;

                    foreach (GridViewRow gvr in gv_project.Rows)
                    {
                        CheckBox cb = (CheckBox)gvr.FindControl("CheckBox1");
                        if (cb.Checked && gv_project.DataKeys[gvr.DataItemIndex].Value.ToString() == "%")
                        {
                            AllProject = true;
                            break;
                        }
                    }

                    if (AllProject)
                    {
                        cmd.CommandText = "jp_project_access_person_api.insert_";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("v_user_id", OleDbType.VarChar, 50).Value = DdlUser.SelectedValue;
                        cmd.Parameters.Add("v_project_id", OleDbType.VarChar, 20).Value = "%";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (GridViewRow gvr in gv_project.Rows)
                        {
                            CheckBox cb = (CheckBox)gvr.FindControl("CheckBox1");
                            if (cb.Checked)
                            {
                                cmd.CommandText = "jp_project_access_person_api.insert_";
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add("v_user_id", OleDbType.VarChar, 50).Value = DdlUser.SelectedValue;
                                cmd.Parameters.Add("v_project_id", OleDbType.VarChar, 20).Value = gv_project.DataKeys[gvr.DataItemIndex].Value.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    odt.Commit();
                    Page.RegisterClientScriptBlock("clientscript", "<script>alert('添加成功！')</script>");
                }
                catch (Exception ex)
                {
                    odt.Rollback();
                    throw ex;
                }

            }
        }

        protected void DdlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGVproject(DdlUser.SelectedValue);
        }
    }
}
