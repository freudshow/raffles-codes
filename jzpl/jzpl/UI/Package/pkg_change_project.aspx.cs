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
using System.Data.OleDb;
using jzpl.Lib;

namespace jzpl.UI.Package
{
    public partial class pkg_change_project : System.Web.UI.Page
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
                        bindDDLData();
                        GVEmptyDataBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_CHG_PRJ] == '1') return true;
            return false;
        }
        
        private void bindDDLData()
        {
            baseInfoLoader.ProjectDropDownListLoad(DDLProject, false, true, string.Empty);
        }
        private string getSQL()
        {
            string SQL = "";
            SQL = "select * from gen_pkg_part_in_store_v where package_no='" + TxtDBbh.Value + "' and part_no='" + TxtXJbh.Value + "'";
            return SQL;
        }
        private void bindGridData()
        {
            DataView view = DBHelper.createGridView(this.getSQL());
            PartGrid.DataSource = view;
            PartGrid.DataKeyNames = new string[] { "objid", "rowversion" };
            PartGrid.DataBind();
        }

        protected void btnViewdata_Click(object sender, EventArgs e)
        {
            bindGridData();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool haveSubmit_ = false;  //记录是否有提交的数据
            string message_;

            if (DDLProject.SelectedValue == "0")
            {
                Misc.Message(this.GetType(), ClientScript, "提示，未选择目标项目。");
                return;
            }
            if (PartGrid.Rows.Count == 0)
            {
                Misc.Message(this.GetType(), ClientScript, "提交失败，无转移零件。");
                return;
            }

            foreach (GridViewRow gvr in PartGrid.Rows)
            {
                TextBox TextJYsl = (TextBox)gvr.FindControl("TxtJYsl");
                Label LblNocheckQty = (Label)gvr.FindControl("LbNoCheckQty");
                Label LblOkQty = (Label)gvr.FindControl("LbOkQty");

                if (!Misc.CheckNumber(TextJYsl.Text))
                {
                    Misc.Message(this.GetType(), ClientScript, "错误，输入非数值。");
                    return;
                }
                if (Misc.DBStrToNumber(TextJYsl.Text) > Misc.DBStrToNumber(LblNocheckQty.Text) + Misc.DBStrToNumber(LblOkQty.Text))
                {
                    Misc.Message(this.GetType(), ClientScript, "错误，输入的借用数量超出了现有数量。");
                    return;
                }
                if (Misc.DBStrToNumber(TextJYsl.Text) > 0)
                {
                    haveSubmit_ = true;
                }
            }

            if (!haveSubmit_)
            {
                Misc.Message(this.GetType(), ClientScript, "提示，无提交数据。");
                return;
            }

            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed) conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "gen_package_part_in_store_api.change_part_project";
            OleDbTransaction otran = conn.BeginTransaction();
            comm.Transaction = otran;
            
            foreach (GridViewRow grdRow in PartGrid.Rows)
            {
                string OBJID = PartGrid.DataKeys[grdRow.RowIndex].Values["objid"].ToString();                
                string Rowversion = PartGrid.DataKeys[grdRow.RowIndex].Values["rowversion"].ToString();
                TextBox TextJYsl = (TextBox)grdRow.FindControl("TxtJYsl");
                Label LblNocheckQty = (Label)grdRow.FindControl("LbNoCheckQty");
                Label LblOkQty = (Label)grdRow.FindControl("LbOkQty");

                //DataView view=DBHelper.createGridView("select * from gen_pkg_part_in_store_v where objid='"+OBJID+"'");
                //string ArrivalID=view[0]["arrival_id"].ToString();
                //string LocationID=view[0]["location_id"].ToString();
                //string Rowversion=view[0]["rowversion"].ToString();

                if (TextJYsl.Text != "" && TextJYsl.Text != "0")
                {

                    comm.Parameters.Clear();
                    comm.Parameters.Add("v_project_d", OleDbType.VarChar, 50).Value = DDLProject.SelectedValue;
                    comm.Parameters.Add("v_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TextJYsl.Text);
                    comm.Parameters.Add("v_objid", OleDbType.VarChar, 50).Value = OBJID;
                    comm.Parameters.Add("v_rowversion", OleDbType.VarChar, 100).Value = Rowversion;
                    comm.Parameters.Add("v_msg", OleDbType.VarChar, 200);
                    comm.Parameters["v_msg"].Direction = ParameterDirection.Output;

                    try
                    {
                        comm.ExecuteNonQuery();
                        message_ = comm.Parameters["v_msg"].Value.ToString();
                        if (message_ != "1")
                        {                            
                            Misc.Message(this.GetType(), ClientScript, message_);
                            otran.Rollback();
                            return;
                        }                        
                    }
                    catch (Exception ex)
                    {
                        otran.Rollback();
                        conn.Close();
                        conn.Dispose();
                        comm.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
            }
            otran.Commit();
            conn.Close();
            conn.Dispose();
            comm.Dispose();
            Misc.Message(this.GetType(), ClientScript, "提交成功！");
            //if(Msg=="") Response.Write("<script language=javascript>alert('提交成功！')</script>");
            bindGridData();
        }

        protected void PartGrid_PreRender(object sender, EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;
            foreach (GridViewRow grdRow in PartGrid.Rows)
            {
                Label LbelOkQty = (Label)grdRow.FindControl("LbOKQty");
                Label lbelNoCheckQty = (Label)grdRow.FindControl("LbNoCheckQty");
                TextBox TextJYsl = (TextBox)grdRow.FindControl("TxtJYsl");
                TextJYsl.Attributes.Add("onblur", "return CheckTextValue()");
                TextJYsl.Text = "0";
                cs.RegisterArrayDeclaration("grd_LbOkQty", String.Concat("'", LbelOkQty.ClientID, "'"));
                cs.RegisterArrayDeclaration("grd_LbNoCheckQty",String.Concat("'",lbelNoCheckQty.ClientID,"'"));
                cs.RegisterArrayDeclaration("grd_TxtJYsl",String.Concat("'",TextJYsl.ClientID,"'"));
            }
        }

        private void GVEmptyDataBind()
        {
            PartGrid.DataSource = null;
            PartGrid.DataBind();
        }
    }
}
