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

namespace jzpl
{
    public partial class wzxqjh_confirm : System.Web.UI.Page
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
                if(CheckAccessAble())
                {
                    if (!IsPostBack)
                    {
                        DdlProjectBind();
                        DdlProdSiteBind();
                        DdlReqGroupBind();
                        DdlReceiptDeptBind();
                        DdlLackMsgBind();

                        TxtRowstate.Text = "i";
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PART_JP_CONFIRM] == '1') return true;
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

        protected void DdlProjectBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);           
        }

        protected void DdlLackMsgBind()
        {
            DataView dv = DBHelper.createDataset("select reason_id,reason_desc from jp_lack_reason where is_valid='1'").Tables[0].DefaultView;
            DdlLackMsg.DataSource = dv;
            DdlLackMsg.DataTextField = "reason_desc";
            DdlLackMsg.DataValueField = "reason_id";
            DdlLackMsg.DataBind();            
        }
        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataBind();
        }
        //查询后显示
        protected void GVDataBind()
        {
            DataView dv;
            StringBuilder sqlstr = new StringBuilder("select a.*,");           
            sqlstr.Append("decode(release_qty,null,require_qty,release_qty) release_qty_1,");
            sqlstr.Append("IFS_DATA_API.GET_BAY_NO_FOR_PART(part_no,contract,project_id) location ");
            sqlstr.Append(" from jp_requisition_vw a ");
            sqlstr.Append(" where 1=1 ");
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
            if (DdlReqGroup.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and req_group='{0}'", DdlReqGroup.SelectedValue));
            }
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sqlstr.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (TxtRowstate.Text.Trim() != "")
            {                
                sqlstr.Append(string.Format(" and rowstate in ({0})", StateWhereString(TxtRowstate.Text.Trim())));
            }

            dv = DBHelper.createGridView(sqlstr.ToString());
            GVData.DataSource = dv;
            GVData.DataKeyNames = new string[] { "requisition_id" };
            GVData.DataBind();         
        }

        protected string StateWhereString(string states)
        {
            string states_ = states.Trim().ToUpper();
            StringBuilder ret = new StringBuilder();
            string[] states__ = states_.Split(new char[] { ';', ' ', ',' });

            for (int i = 0; i < states__.Length; i++)
            {
                ret.Append(string.Format("'{0}'", CovertStateCharToString(states__[i])));
                if (i < states__.Length - 1)
                {
                    ret.Append(",");
                }
            }
            return ret.ToString();
        }

        protected string CovertStateCharToString(string stateChar)
        {
            switch (stateChar.ToUpper())
            {
                case "I":
                    return "init";
                case "R":
                    return "released";
                case "F":
                    return "finished";
                case "C":
                    return "confirming";
                case "CA":
                    return "cancelled";
                default:
                    break;
            }
            return null;
        }
        //gridview取消、下达按钮显示
        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string state_;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");               
                ((ImageButton)e.Row.FindControl("ImgRelease")).Attributes.Add("reqid", GVData.DataKeys[e.Row.RowIndex].Value.ToString());
                ((ImageButton)e.Row.FindControl("ImgRelease")).Attributes.Add("onclick",
                    string.Format("return nextdo(this,'{0}','{1}')",                     
                    DataBinder.Eval(e.Row.DataItem, "objid"),
                    DataBinder.Eval(e.Row.DataItem, "rowversion")));

                state_ = DataBinder.Eval(e.Row.DataItem, "rowstate").ToString();

                if (state_ == "init")
                {
                    ((ImageButton)e.Row.FindControl("ImgRelease")).Visible = true ;
                    ((ImageButton)e.Row.FindControl("ImgCancelRelease")).Visible = false;
                    ((TextBox)e.Row.FindControl("TxtReleaseQty")).Visible = true;
                    ((Label)e.Row.FindControl("LblReleaseQty")).Visible = false;
                }
                else
                {

                    if (state_ == "released")
                    {
                        ((ImageButton)e.Row.FindControl("ImgRelease")).Visible = false;
                        ((ImageButton)e.Row.FindControl("ImgCancelRelease")).Visible = true ;
                        ((TextBox)e.Row.FindControl("TxtReleaseQty")).Visible = false;
                        ((Label)e.Row.FindControl("LblReleaseQty")).Visible = true;
                    }
                    else
                    {
                        ((ImageButton)e.Row.FindControl("ImgRelease")).Visible = false;
                        ((ImageButton)e.Row.FindControl("ImgCancelRelease")).Visible = false;
                        ((TextBox)e.Row.FindControl("TxtReleaseQty")).Visible = false;
                        ((Label)e.Row.FindControl("LblReleaseQty")).Visible = true;
                    }
                }
                
                if (!((ImageButton)e.Row.FindControl("ImgRelease")).Visible && !((ImageButton)e.Row.FindControl("ImgCancelRelease")).Visible)
                {
                    ((Image)e.Row.FindControl("ImgNotAccess")).Visible = true;
                }
                else
                {
                    ((Image)e.Row.FindControl("ImgNotAccess")).Visible = false;
                }
            }
        }

        //protected Boolean CheckPermissionOnFunction(int funIndex)
        //{
        //    if (m_perimission_array[(int)Authentication.FUN_INTERFACE.wzxqjh_confirm][funIndex] == '1') return true;
        //    return false;
        //}

        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            GVDataBind();
        }
        //提交下达命令
        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] temp;            
            
            using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
            {

                try
                {
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
                        float _releaseQty = Convert.ToSingle(((TextBox)(gvr.Cells[1].FindControl("TxtReleaseQty"))).Text);
                        float _requirQty = Convert.ToSingle(gvr.Cells[2].Text);
                        if (_releaseQty != _requirQty) return;
                        cmd.CommandText = "jp_requisition_api.release_all_";//足额下达ok
                        cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = temp[0];
                        cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = temp[1];
                        cmd.Parameters.Add("v_release_qty", OleDbType.Decimal).Value = _releaseQty;
                        cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                        cmd.ExecuteNonQuery();
                        //Misc.Message(Response, "下达！");
                        GVDataBind();
                    }
                    else
                    {
                        if (e.CommandName == "CancelRelease")
                        {
                            temp = e.CommandArgument.ToString().Split('^');
                            if (temp.Length != 2)
                            {
                                Misc.Message(this.GetType(), ClientScript, "操作失败，错误参数。");
                                return;
                            }

                            cmd.CommandText = "jp_requisition_api.cancel_release_";//取消下达ok
                            cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = temp[0];
                            cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = temp[1];
                            cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                            cmd.ExecuteNonQuery();
                            Misc.Message(Response, "已标记为缺品！");
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
            }
        } 
        //缺料下达
        protected void BtnLackOK_Click(object sender, EventArgs e)
        {
            string reqId;
            string doType;
            decimal releaseNum;
            string objid;
            string ver;

            reqId = Request.Form["reqid"];
            doType = Request.Form["do"];
            objid = HidObjId.Value;
            ver = HidRowversion.Value;

            releaseNum = Convert.ToDecimal(Request.Form["releaseNum"]);

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    if (conn.State != ConnectionState.Open) conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    switch (doType)
                    {
                        case "cancel":
                            cmd.CommandText = "jp_requisition_api.cancel_";//取消该配送 当下达数量为0时，自动触发ok
                            //cmd.Parameters.Add("v_req_id", OleDbType.Integer).Value=reqId;
                            cmd.Parameters.Add("v_objid", OleDbType.VarChar, 50).Value = objid;
                            cmd.Parameters.Add("v_rowversion", OleDbType.VarChar, 50).Value = ver;
                            //cmd.Parameters.Add("v_lack_msg", OleDbType.VarChar, 500).Value = TxtLackMsg.Text;
                            cmd.Parameters.Add("v_lack_msg", OleDbType.VarChar, 500).Value = DdlLackMsg.SelectedItem.Text;
                            cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;

                            cmd.ExecuteNonQuery();
                            break;
                        case "confirm":
                            cmd.CommandText = "jp_requisition_api.release_lack_";//待生产确认 部分下达时，自动触发 原confirm_
                            //cmd.Parameters.Add("v_req_id", OleDbType.Integer).Value = reqId;
                            cmd.Parameters.Add("v_objid", OleDbType.VarChar, 50).Value = objid;
                            cmd.Parameters.Add("v_rowversion", OleDbType.VarChar, 50).Value = ver;
                            //cmd.Parameters.Add("v_lack_msg", OleDbType.VarChar, 500).Value = TxtLackMsg.Text;
                            cmd.Parameters.Add("v_lack_msg", OleDbType.VarChar, 500).Value = DdlLackMsg.SelectedItem.Text;
                            cmd.Parameters.Add("v_release_qty", OleDbType.Numeric).Value = releaseNum;
                            cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;

                            cmd.ExecuteNonQuery();
                            break;
                        default:
                            break;
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

            GVDataBind();
            
        }       
    }
}
