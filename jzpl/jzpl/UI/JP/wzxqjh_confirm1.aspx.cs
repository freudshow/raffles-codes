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
using System.Data.OleDb;
using jzpl.Lib;

namespace jzpl
{
    public partial class wzxqjh_confirm1 : System.Web.UI.Page
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

        protected void DdlProjectBind() 
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);            
        }       

        protected Boolean CheckAccessAble()
        {
            if (m_perimission[(int)Authentication.PERMDEFINE.PART_JP_CONFIRM1] == '1') return true;
            return false;
        }
        
        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataBind();
        }

        protected void GVDataBind()
        {
            DataView dv;
            StringBuilder sqlstr = new StringBuilder("select a.*,");
            sqlstr.Append("decode(release_qty,null,require_qty,release_qty) release_qty_1,");
            sqlstr.Append("IFS_DATA_API.GET_BAY_NO_FOR_PART(part_no,contract,project_id) location ");
            sqlstr.Append(" from jp_requisition_vw a ");
            sqlstr.Append(" where 1=1 ");
            if (ChkOnlyShowConfirmingState.Checked)
            {
                sqlstr.Append(" and rowstate ='confirming' ");
            }
            else
            {
                sqlstr.Append(" and (rowstate='confirming' or (rowstate='cancelled' and release_qty is null))");
            }
            
            if (DdlProject.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and project_id = '{0}'", DdlProject.SelectedValue));
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
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (DdlReqGroup.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and req_group='{0}'", DdlReqGroup.SelectedValue));
            }
            dv = DBHelper.createGridView(sqlstr.ToString());
            GVData.DataSource = dv;
            GVData.DataKeyNames = new string[] { "requisition_id" };
            GVData.DataBind();
        }  
        
        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            GVDataBind();
        } 
        
        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
            {
                try
                {
                    string[] temp;
                    

                    if (conn.State != ConnectionState.Open) conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (e.CommandName == "Release")
                    {
                        temp = e.CommandArgument.ToString().Split('^');
                        if (temp.Length != 2)
                        {
                            Misc.Message(this.GetType(), ClientScript, "操作失败，错误参数。");
                            return;
                        }

                        GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).Parent.Parent);
                        float _releaseQty = Convert.ToSingle(gvr.Cells[2].Text);
                        cmd.CommandText = "jp_requisition_api.sc_pass_";
                        cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = temp[0];
                        cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = temp[1];
                        //cmd.Parameters.Add("v_release_qty", OleDbType.Decimal).Value = _releaseQty;
                        cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Label)gvr.FindControl("GVLblReleaseUser")).Text;

                        cmd.ExecuteNonQuery();
                        //Misc.Message(Response, "下达！");
                        GVDataBind();
                    }
                    else
                    {
                        if (e.CommandName == "CancelX")
                        {
                            temp = e.CommandArgument.ToString().Split('^');
                            if (temp.Length != 2)
                            {
                                Misc.Message(this.GetType(), ClientScript, "操作失败，错误参数。");
                                return;
                            }
                            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).Parent.Parent);
                            float _releaseQty = Convert.ToSingle(gvr.Cells[2].Text);
                            cmd.CommandText = "jp_requisition_api.sc_cancel_";
                            cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = temp[0];
                            cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = temp[1];
                            cmd.Parameters.Add("v_release_qty", OleDbType.Decimal).Value = _releaseQty;
                            cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Label)gvr.FindControl("GVLblReleaseUser")).Text;
                            cmd.ExecuteNonQuery();
                            //Misc.Message(Response, "取消下达！");
                            GVDataBind();
                        }
                    }
                }

                catch (Exception ex)
                {
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
                    }
                    else
                    {
                        throw ex;
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        
        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //double releaseQty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(DataBinder.Eval(e.Row.DataItem, "release_qty").ToString()=="")                
                {
                    e.Row.FindControl("ImgOK").Visible = false;
                    e.Row.FindControl("ImgCancel").Visible = false;
                }
            }
        } 
    }
}
