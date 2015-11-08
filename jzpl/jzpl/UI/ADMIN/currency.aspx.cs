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
using System.Data.OracleClient;
using jzpl.Lib;

namespace ADMIN
{
    public partial class currency : System.Web.UI.Page
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
                        GVDataBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.BS_CURRENCY] == '1') return true;
            return false;
        }
       

        private void GVDataBind()
        {
            GV.DataSource = DBHelper.createGridView("select * from jp_currency");
            GV.DataKeyNames = new string[] { "currency" };
            GV.DataBind();

        }

        protected void GV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV.EditIndex = -1;
            GVDataBind();
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = GV.Rows[e.RowIndex];
            using (OracleConnection conn = new OracleConnection(DBHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("delete from jp_currency where currency='{0}'", GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.ExecuteNonQuery();
            }
            GVDataBind();
        }

        protected void GV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV.EditIndex = e.NewEditIndex;
            GVDataBind();
        }

        protected void GV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (OracleConnection conn = new OracleConnection(DBHelper.ConnectionString))
            {
                string currency = GV.DataKeys[e.RowIndex].Values[0].ToString();
                string currency_desc = ((TextBox)GV.Rows[e.RowIndex].FindControl("TxtDesc")).Text;
                string active = ((CheckBox)(GV.Rows[e.RowIndex].FindControl("ChkActive"))).Checked == true ? "1" : "0";
                string sqlupdate = "update jp_currency set currency_desc = '" + currency_desc + "',is_valid='" + active + "' where currency = '" + currency + "' ";
                OracleCommand updatecomm = new OracleCommand(sqlupdate, conn);
                try
                {
                    conn.Open();
                    updatecomm.ExecuteNonQuery();

                    GV.EditIndex = -1;
                    GVDataBind();

                }
                catch (Exception ex)
                {
                    conn.Close();
                    Response.Write("<script language=javascript>alert('" + ex.Message + "')</script>");
                }
                finally
                {
                    updatecomm.Dispose();
                    conn.Dispose();
                    conn.Close();
                }
            }
        }

        protected void BtnAddCurrency_Click(object sender, EventArgs e)
        {
            using (OracleConnection conn = new OracleConnection(DBHelper.ConnectionString))
            {
                if (Convert.ToInt32(DBHelper.getObject(string.Format("select count(*) from jp_currency where currency='{0}'", this.TxtCurrencyID.Text))) > 0)
                {
                    Response.Write("<script type='text/javascript'>alert('±“÷÷÷ÿ∏¥£°')</script>");
                    return;
                }
                OracleCommand cmd = new OracleCommand(string.Format("insert into jp_currency(currency,currency_desc,is_valid) values('{0}','{1}','1')", this.TxtCurrencyID.Text, this.TxtCurrencyDesc.Text), conn);
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.ExecuteNonQuery();
            }
            GVDataBind();
        }
    }
}
