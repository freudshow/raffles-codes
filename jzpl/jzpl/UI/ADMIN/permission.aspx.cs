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

namespace jzpl.UI.ADMIN
{
    public partial class permission : System.Web.UI.Page
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
                if (((Authentication.LOGININFO)Session["USERINFO"]).Admin == "1")
                {                   
                    if (!IsPostBack)
                    {
                        BtnSetPer.Enabled = false;
                        GVCurrPer.Visible = false;
                        GVAllPer.Visible = false;
                        Pnl.Visible = false;
                        string user = Misc.GetHtmlRequestValue(Request, "id");
                        DdlUserDataBind();
                        if (user != "")
                        {                            
                            DdlUser.SelectedIndex = DdlUser.Items.IndexOf(DdlUser.Items.FindByValue(user));                            
                            GVCurrPerDataBind(user);
                        }
                        
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
        
        
        private void DdlUserDataBind()
        {
            DdlUser.DataSource = DBHelper.createDDLView("select user_id value,user_id text from jp_user where admin='0' order by user_id");
            DdlUser.DataTextField = "text";
            DdlUser.DataValueField = "value";
            DdlUser.DataBind();
        }

        protected void DdlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlUser.SelectedValue == "0") return;
            GVCurrPerDataBind(DdlUser.SelectedValue);
        }

        private void GVCurrPerDataBind(string user)
        {
            //显示当前用户已赋予的权限 
            GVCurrPer.Visible = true;
            GVAllPer.Visible = false;
            Pnl.Visible = false;
            BtnSetPer.Enabled = true;

            string strPermission = DBHelper.getObject(string.Format("select role from jp_user where user_id='{0}'", user)).ToString();
            strPermission = strPermission.PadRight(100, '0');
            DataTable currPer = Authentication.GetPermissionTable();
            currPer.TableName = "PerTable";
            DataView viewCurrPer = new DataView();
            viewCurrPer.Table = currPer;
            for (int i = 0; i < currPer.Rows.Count; i++)
            {
                if (strPermission[(int)((Authentication.PERMDEFINE)Enum.Parse(typeof(Authentication.PERMDEFINE), currPer.Rows[i]["code"].ToString(), true))] == '1')
                {
                    currPer.Rows[i]["permission"] = "1";
                }
                else
                {
                    currPer.Rows[i]["permission"] = "0";
                }
            }
            viewCurrPer.RowFilter = "permission='1'";

            GVCurrPer.DataSource = viewCurrPer;
            GVCurrPer.DataBind();            
        }

        private void GVAllPerDataBind()
        {
            //显示全部权限，标识当前用户已赋予的权限 
            GVCurrPer.Visible = false;
            BtnSetPer.Enabled = false;
            GVAllPer.Visible = true;
            Pnl.Visible = true;
            string strPermission  = DBHelper.getObject(string.Format("select role from jp_user where user_id='{0}'", DdlUser.SelectedValue)).ToString();
            strPermission = strPermission.PadRight(100, '0');
            DataTable currPer = Authentication.GetPermissionTable();
            DataView viewCurrPer = new DataView();
            currPer.TableName = "perTable";
            viewCurrPer.Table = currPer;
            for (int i = 0; i < currPer.Rows.Count; i++)
            {
                if (strPermission[(int)((Authentication.PERMDEFINE)Enum.Parse(typeof(Authentication.PERMDEFINE), currPer.Rows[i]["code"].ToString(), true))] == '1')
                {
                    currPer.Rows[i]["permission"] = "1";
                }
                else
                {
                    currPer.Rows[i]["permission"] = "0";
                }
            }

            GVAllPer.DataSource = viewCurrPer;
            GVAllPer.DataBind();
            DdlOtherUserDataBind();
        }

        protected void GVCurrPer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkBox = e.Row.FindControl("ChkPer") as CheckBox;
                if (DataBinder.Eval(e.Row.DataItem, "permission").ToString() == "1")
                {
                    chkBox.Checked = true;
                }
                else
                {
                    chkBox.Checked = false;
                }
                chkBox.Enabled = false;
            }
        }

        protected void GVAllPer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkBox = e.Row.FindControl("ChkPer") as CheckBox;
                if (DataBinder.Eval(e.Row.DataItem, "permission").ToString() == "1")
                {
                    chkBox.Checked = true;
                }
                else
                {
                    chkBox.Checked = false;
                }                
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //保存权限设置 
            string strPerssion = "".PadRight(Authentication.MaxPermisonLength, '0');
            char[] cPerssion = strPerssion.ToCharArray();
            foreach (GridViewRow gvr in GVAllPer.Rows)
            {
                if (((CheckBox)gvr.FindControl("ChkPer")).Checked)
                {
                    cPerssion[(int)((Authentication.PERMDEFINE)Enum.Parse(typeof(Authentication.PERMDEFINE), gvr.Cells[0].Text, true))] = '1';
                }
            }
            strPerssion = new string(cPerssion);
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("update jp_user set role='{0}' where user_id='{1}'", strPerssion, DdlUser.SelectedValue);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            GVCurrPerDataBind(DdlUser.SelectedValue);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GVCurrPerDataBind(DdlUser.SelectedValue);
        }

        private void DdlOtherUserDataBind()
        {
            DdlOtherUser.DataSource = DBHelper.createDDLView(string.Format("select user_id value,user_id text from jp_user where admin='0' and user_id<>'{0}'", DdlUser.SelectedValue));
            DdlOtherUser.DataTextField = "text";
            DdlOtherUser.DataValueField = "value";
            DdlOtherUser.DataBind();
        }

        protected void BtnCopyPer_Click(object sender, EventArgs e)
        {
            if (DdlOtherUser.SelectedValue == "0")
            {
                Misc.Message(this.GetType(), ClientScript, "Please select a user first.");
                return;
            }
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("update jp_user set role=(select role from jp_user where user_id='{0}') where user_id='{1}'", DdlOtherUser.SelectedValue, DdlUser.SelectedValue);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            GVCurrPerDataBind(DdlUser.SelectedValue);
        }

        protected void BtnSetPer_Click(object sender, EventArgs e)
        {
            GVAllPerDataBind();
        }
    }
}
