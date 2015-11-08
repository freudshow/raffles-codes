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
using System.IO;

namespace jzpl
{
    public partial class wzxqjh_query : System.Web.UI.Page
    {  
        private string m_perimission;
        private BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                
                if(CheckAccessAble())
                {
                    if (!IsPostBack)
                    {
                        DdlProjectBind();
                        DdlProdSiteBind();
                        DdlReceiptDeptBind();
                        DdlReqGroupBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PART_JP_QUERY] == '1') return true;
            return false;
        }

        protected void DdlProdSiteBind()
        {
            DdlProdSite.DataSource = Lib.DBHelper.createDDLView("select place_id,place_name from jp_receipt_place  where state='1'");
            DdlProdSite.DataTextField = "place_name";
            DdlProdSite.DataValueField = "place_id";
            DdlProdSite.DataBind();
        }

        protected void DdlReceiptDeptBind()
        {
            DdlReceiptDept.DataSource = Lib.DBHelper.createDDLView("select dept_id,company||' '||dept_desc dept_desc from jp_receipt_dept t where state='1'");
            DdlReceiptDept.DataTextField = "dept_desc";
            DdlReceiptDept.DataValueField = "dept_id";
            DdlReceiptDept.DataBind();
        }

        protected void DdlReqGroupBind()
        {
            DdlReqGroup.DataSource = DBHelper.createDDLView("select group_id,company_id||'  '||group_name group_name from jp_req_group where state='1'");
            DdlReqGroup.DataTextField = "group_name";
            DdlReqGroup.DataValueField = "group_id";
            DdlReqGroup.DataBind();
        }
        //private void SetAuthentication()
        //{
        //    switch (((Authentication.LOGININFO)Session["USERINFO"]).Permission.Substring((int)Authentication.FUN_INTERFACE.wzxqjh_query, 1))
        //    {
        //        case "0":
        //            Response.Write("\n<HTML>");
        //            Response.Write("\n<HEAD></HEAD>");
        //            Response.Write("\n<BODY>");
        //            Response.Write("\n<SCRIPT LANGUAGE='Javascript'>");
        //            Response.Write(string.Format("\nalert('缺少权限！');"));
        //            Response.Write(string.Format("\nparent.document.location='../../UI/FrameUI/login.htm'"));
        //            Response.Write("\n</SCRIPT>");
        //            Response.Write("\n</BODY>");
        //            Response.Write("\n</HTML>");
        //            break;
        //        case "1":
        //            GVData.Columns[15].Visible = false;
        //            GVData.Columns[16].Visible = false;
        //            break;
        //        case "2":
        //            GVData.Columns[15].Visible = true;
        //            GVData.Columns[16].Visible = true;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        protected void DdlProjectBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);            
        }

        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        
            string[] temp;
            string objid;
            string rowversion;
            int row_index;
            string id;

            if (e.CommandName == "ReqLineDelete")
            {
                temp = e.CommandArgument.ToString().Split('^');
                if (temp.Length != 2)
                {
                    Misc.Message(this.GetType(), ClientScript, "删除失败，错误参数。");
                    return;
                }

                objid = temp[0];
                rowversion = temp[1];

                row_index = ((GridViewRow)(((ImageButton)e.CommandSource).Parent.Parent)).RowIndex;
                id = GVData.DataKeys[row_index].Value.ToString();
                using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    OleDbCommand cmd = new OleDbCommand("jp_demand_api.delete_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    OleDbTransaction tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = objid;
                    cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        tr.Commit();
                        GVDataBind();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        if (Misc.CheckIsDBCustomException(ex))
                        {
                            Misc.Message(this.GetType(),ClientScript, Misc.GetDBCustomException(ex));
                            GVDataBind();
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
            }

            if (e.CommandName == "ReqLineCancel")
            {
                temp = e.CommandArgument.ToString().Split('^');
                if (temp.Length != 2)
                {
                    Misc.Message(this.GetType(), ClientScript, "取消失败，错误参数。");
                    return;
                }

                objid = temp[0];
                rowversion = temp[1];

                row_index = ((GridViewRow)(((ImageButton)e.CommandSource).Parent.Parent)).RowIndex;
                id = GVData.DataKeys[row_index].Value.ToString();
                using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    OleDbCommand cmd = new OleDbCommand("jp_demand_api.cancel_demand_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    OleDbTransaction tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = objid;
                    cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        tr.Commit();
                        GVDataBind();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        if (Misc.CheckIsDBCustomException(ex))
                        {
                            Misc.Message(this.GetType(),ClientScript, Misc.GetDBCustomException(ex));
                            GVDataBind();
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
            }

        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {            
            GVDataBind();
        }

        protected void GVDataBind()
        {
            DataView dv;
            StringBuilder sqlstr = new StringBuilder("select demand_id,");
            sqlstr.Append("matr_seq_no,");
            sqlstr.Append("matr_seq_line_no,");
            sqlstr.Append("part_no, ");
            sqlstr.Append("part_description,");
            sqlstr.Append("require_qty,");
            sqlstr.Append("issued_qty,");
            sqlstr.Append("project_id,");
            sqlstr.Append("project_description,");
            sqlstr.Append("project_block,");
            sqlstr.Append("project_system,");
            sqlstr.Append("place,");
            sqlstr.Append("place_description,");
            sqlstr.Append("receiver,");
            sqlstr.Append("receiver_ic,");
            sqlstr.Append("to_char(receive_date,'yyyy-mm-dd') receive_date,");
            sqlstr.Append("receiver_contact,");
            sqlstr.Append("crane,");
            sqlstr.Append("recorder,");
            sqlstr.Append("record_time, ");
            sqlstr.Append("IFS_DATA_API.GET_BAY_NO_FOR_PART(part_no,contract,project_id) location ,");
            sqlstr.Append("decode(rowstate, 'init','初始','released','下发','finished','完成','cancelled','取消','confirming','确认','lack','缺料') rowstate,");
            sqlstr.Append("rowversion,");
            sqlstr.Append("decode(lack_type,'1','继续配送','2','取消配送','3','需确认') lack_type,");
            sqlstr.Append("rowid objid ");
            sqlstr.Append(" from jp_demand ");
            sqlstr.Append(" where rowstate in ('init','lack') ");
            if (DdlProject.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and project_id = '{0}'",DdlProject.SelectedValue));
            }
            if (DdlProdSite.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and place='{0}' ", DdlProdSite.SelectedValue));            
            }
            if (TxtLocation.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and IFS_DATA_API.GET_BAY_NO_FOR_PART(part_no,contract,project_id)='{0}'", TxtLocation.Text.Trim()));
            }
            if (TxtMtrSeqNo.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and matr_seq_no like '{0}'", TxtMtrSeqNo.Text.Trim()));
            }
            if (TxtMtrLineNo.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and matr_seq_line_no='{0}'", TxtMtrLineNo.Text.Trim()));
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and part_no like '{0}'", TxtPartNo.Text.Trim()));
            }
            if (TxtRecieveDate.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and receive_date = to_date('{0}','yyyy-mm-dd')", TxtRecieveDate.Text.Trim()));
            }
            if (TxtReciever.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and receiver = '{0}'", TxtReciever.Text.Trim()));
            }
            if (DdlReqGroup.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and req_group='{0}'", DdlReqGroup.SelectedValue));
            }
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
                      
            dv = DBHelper.createGridView(sqlstr.ToString());
            GVData.DataSource = dv;
            GVData.DataKeyNames = new string[] { "demand_id" };
            GVData.DataBind();

            //SetAuthentication();
        }

        //protected void GVData_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    string id = GVData.DataKeys[e.NewEditIndex].Value.ToString();
        //    Response.Write("<script type='text/javascript'>");
        //    Response.Write(string.Format("\nwindow.open('wzxqjh_mod.aspx?id={0}','mod1','height=600px, width=480px, toolbar =no, menubar=no, scrollbars=no, resizable=yes, location=no, status=no')",id));
        //    Response.Write("\n</script>");
            
        //}

        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            GVDataBind();
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string state_;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = e.Row.FindControl("ChkCrane") as CheckBox;
                DataRowView drv = e.Row.DataItem as DataRowView;
                state_ = DataBinder.Eval(e.Row.DataItem, "rowstate").ToString();
                if (state_ != "初始")
                {
                    ((ImageButton)e.Row.FindControl("ImageButton2")).Visible = false;
                    if (state_ == "缺料")
                    {
                        ((ImageButton)e.Row.FindControl("ImageButton2")).Visible = false;
                    }
                }
                else
                {
                    ((ImageButton)e.Row.FindControl("ImgBtnCancel")).Visible = false;
                }
                if (drv["crane"].ToString() == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }

        protected void BtnHtmlToExcel_Click(object sender, EventArgs e)
        {
            
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=wuzixuqiu.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            
            GVData.AllowPaging = false;
            GVData.Columns[GVData.Columns.Count - 2].Visible = false;
            GVData.Columns[GVData.Columns.Count - 1].Visible = false;
            GVDataBind();

            GVData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            GVData.AllowPaging = true;
            GVData.Columns[GVData.Columns.Count - 2].Visible = true;
            GVData.Columns[GVData.Columns.Count - 1].Visible = true;
            GVDataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        
              
        
    }
}
