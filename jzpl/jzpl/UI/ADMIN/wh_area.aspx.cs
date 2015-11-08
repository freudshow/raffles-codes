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
using System.Data.OleDb;

namespace ADMIN
{
    public partial class wh_area : System.Web.UI.Page
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
                        DdlCompanyBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.BS_WH_AREA] == '1') return true;
            return false;
        }        

        protected void GVDataBind()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select area_id,company_id,area,state from jp_wh_area where 1=1");
            if(DdlCompany.SelectedValue!="0")
            {
                sql.Append(string.Format(" and company_id='{0}'", DdlCompany.SelectedValue));
            }
            if(TxtAreaID.Text!="")
            {
                sql.Append(string.Format(" and area_id='{0}'", TxtAreaID.Text));
            }
            if (TxtArea.Text != "")
            {
                sql.Append(string.Format(" and area like '%{0}%'", TxtArea.Text));
            }
            GV.DataSource = DBHelper.createGridView(sql.ToString());
            GV.DataKeyNames = new string[] { "area_id" };
            GV.DataBind();
        }

        private void DdlCompanyBind()
        {
            baseInfoLoader.CompanyDropDrownListLoad(DdlCompany, false, true,true,string.Empty);
            //DdlCompany.DataSource = DBHelper.createDDLView("select company_id,company from jp_company where state='1'");
            //DdlCompany.DataTextField = "company";
            //DdlCompany.DataValueField = "company_id";
            //DdlCompany.DataBind();
        }               

        protected void GV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV.EditIndex = e.NewEditIndex;
            GVDataBind();
        }

        protected void GV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV.EditIndex = -1;
            GVDataBind();
        }

        protected void GV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            { 
                OleDbCommand cmd = new OleDbCommand();
                string state = ((CheckBox)(GV.Rows[e.RowIndex].FindControl("ChkActive"))).Checked == true ? "1" : "0";
                cmd.CommandText = string.Format("update jp_wh_area set state='{0}'where area_id='{1}'", state, GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.ExecuteNonQuery();                
            }
            GV.EditIndex = -1;
            GVDataBind();
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = GV.Rows[e.RowIndex];
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;                
                cmd.CommandText = string.Format("delete from jp_wh_area where area_id='{0}'", GV.DataKeys[e.RowIndex].Values[0].ToString());
                cmd.ExecuteNonQuery();                
            }
            GVDataBind();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {            
            GVDataBind();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (DdlCompany.SelectedValue == "0")
            {
                Misc.Message(Response, "请选择公司。");
                return;
            }
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {

                if (Convert.ToInt32(DBHelper.getObject(string.Format("select count(*) from jp_wh_area where company_id='{2}' and (area_id='{0}' or area='{1}')", TxtAreaID.Text,TxtArea.Text,DdlCompany.SelectedValue))) > 0)
                {
                    Response.Write("<script type='text/javascript'>alert('错误，填加重复区域！')</script>");
                    return;
                }
                OleDbCommand cmd = new OleDbCommand(string.Format("insert into jp_wh_area(company_id,area_id,area,state) values('{0}','{1}','{2}','1')",DdlCompany.SelectedValue, TxtAreaID.Text,TxtArea.Text), conn);
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.ExecuteNonQuery();                
            }

            GVDataBind();
        }
    }
}
