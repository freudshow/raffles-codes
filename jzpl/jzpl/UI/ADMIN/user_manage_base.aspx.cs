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

namespace jzpl.UI.ADMIN
{
    public partial class user_manage_base : System.Web.UI.Page
    {        

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
                if (((Authentication.LOGININFO)Session["USERINFO"]).Admin=="1")
                {
                    DdlUserBind();
                    if (!IsPostBack)
                    {
                        DdlGroupBind();
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

        protected void DdlUserBind()
        {
            GV_User.DataSource =  DBHelper.createGridView("select user_id,decode(group_id,'%','%',jp_req_group_api.get_name(group_id)) group_name from jp_user");
            GV_User.DataKeyNames = new string[] { "user_id" };
            GV_User.DataBind();
        }

        protected void DdlGroupBind()
        {
            DdlGroup.Items.Clear();
            DdlGroup.Items.Add(new ListItem("%", "%"));
            string sql = "select * from jp_req_group  where state='1'";
            DataView dv_ = DBHelper.createGridView(sql);
            foreach (DataRow dr in dv_.Table.Rows)
            {
                DdlGroup.Items.Add(new ListItem(dr["group_name"].ToString(),dr["group_id"].ToString()));
            }
            DdlGroup.DataBind();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("insert into jp_user(user_id,admin,group_id) values('{0}','0','{1}' )", TxtUserID.Text,DdlGroup.SelectedValue);
                cmd.ExecuteNonQuery();
            }
            DdlUserBind();
        }

        protected void GV_User_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("delete jp_user where user_id ='{0}'", GV_User.DataKeys[e.RowIndex].Value.ToString());
                cmd.ExecuteNonQuery();
            }
            DdlUserBind();
        }
       
    }
}
