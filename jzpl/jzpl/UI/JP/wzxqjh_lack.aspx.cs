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
    public partial class wzxqjh_lack : System.Web.UI.Page
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
            sqlstr.Append(" from jp_demand_vw a ");
            sqlstr.Append(" where rowstate='lack' ");
            
            if (TxtDemandId.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and demand_id like '{0}'", TxtDemandId.Text.Trim()));
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
                //((ImageButton)e.Row.FindControl("ImgRelease")).Attributes.Add("onclick", string.Format("return nextdo(this,'{0}','{1}')",                     
                 //   DataBinder.Eval(e.Row.DataItem, "objid"),
                 //   DataBinder.Eval(e.Row.DataItem, "rowversion")));

                state_ = DataBinder.Eval(e.Row.DataItem, "rowstate").ToString();

                if (state_ == "lack")
                {
                    ((ImageButton)e.Row.FindControl("ImgRelease")).Visible = true ;
                    ((ImageButton)e.Row.FindControl("ImgCancelRelease")).Visible = false;
                    ((TextBox)e.Row.FindControl("TxtReleaseQty")).Visible = true;
                    ((Label)e.Row.FindControl("LblReleaseQty")).Visible = false;
                }
                else
                {
                    ((ImageButton)e.Row.FindControl("ImgRelease")).Visible = false;
                    ((ImageButton)e.Row.FindControl("ImgCancelRelease")).Visible = false;
                    ((TextBox)e.Row.FindControl("TxtReleaseQty")).Visible = false;
                    ((Label)e.Row.FindControl("LblReleaseQty")).Visible = false;
                }
                

            }
        }



        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            GVDataBind();
        }
        //提交下达命令
        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).Parent.Parent);
           //获取参数
            string _releasedate_str = ((TextBox)(gvr.Cells[1].FindControl("TextReceiveDate"))).Text;
            float _releaseQty = Convert.ToSingle(((TextBox)(gvr.Cells[1].FindControl("TxtReleaseQty"))).Text);
            float _lack_Qty = Convert.ToSingle(gvr.Cells[4].Text);
            string _reciever = gvr.Cells[9].Text;
            if (_releaseQty == 0 || _releaseQty  > _lack_Qty)
            {            
                    Misc.Message(this.GetType(), ClientScript, "下达数量非法。");
                    return;
            }
            //验证时间
            DateTime releasedate = new DateTime(1900, 1, 1);
            if (!string.IsNullOrEmpty(_releasedate_str))
            {
                try
                {
                    releasedate = Convert.ToDateTime(_releasedate_str);
                }
                catch (Exception em)
                {
                    Misc.Message(this.GetType(), ClientScript, "请选择配送时间.");
                    return;
                }
            }
            else
            {
                Misc.Message(this.GetType(), ClientScript, "请选择配送时间。");
                return;

            }
            

            string[] temp;            
            
            using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
            {

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                        temp = e.CommandArgument.ToString().Split('^');
                        if (temp.Length != 2)
                        {
                            Misc.Message(this.GetType(), ClientScript, "操作失败，错误参数。");
                            return;
                        }
                        cmd.CommandText = "jp_demand_api.release_lack_";//进行缺料下达
                        cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = temp[0];
                        cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = temp[1];
                        cmd.Parameters.Add("v_release_qty", OleDbType.Decimal).Value = _releaseQty;
                        cmd.Parameters.Add("v_release_date", OleDbType.Date).Value = releasedate;
                        cmd.Parameters.Add("v_release_user", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                        cmd.Parameters.Add("v_requistion_id_", OleDbType.VarChar, 200).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        String v_requistion_id_ = cmd.Parameters["v_requistion_id_"].Value.ToString();
                        if (v_requistion_id_.Length > 0)
                        {
                            Lib.Mail myMail = new Lib.Mail();
                            myMail.Send("您有一笔缺品物料配送需确认", _reciever + "你好，" + ((Authentication.LOGININFO)Session["USERINFO"]).UserID + "刚才为你生成一笔缺品配送单，配送日期为:" + releasedate.ToString().Split(' ')[0]+"。请尽快登录【集中配料系统】进行确认", "yias@cimc-raffles.com", _reciever + "@cimc-raffles.com", ((Authentication.LOGININFO)Session["USERINFO"]).UserID + "@cimc-raffles.com", true, false, null, null);
                        }
                        //Misc.Message(Response, "下达！");
                        GVDataBind();
    

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
              
    }
}
