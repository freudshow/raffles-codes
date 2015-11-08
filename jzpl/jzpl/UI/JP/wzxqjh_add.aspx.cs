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
    public partial class wzxqjh_add : System.Web.UI.Page
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
                        GVRation.DataSource = null;
                        GVRation.DataBind();
                        DdlProjectBind();
                        DdlProdSiteBind();
                        DdlReceiptDeptBind();
                        DdlReceiptPersonBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PART_JP_ADD] == '1') return true;
            return false;
        }

        protected void DdlProjectBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);            
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
            DdlReceiptDept.DataSource = Lib.DBHelper.createDDLView("select dept_id,company||' '||dept_desc dept_desc from jp_receipt_dept t where state='1' order by NLSSORT(dept_desc,'NLS_SORT = SCHINESE_PINYIN_M')");
            DdlReceiptDept.DataTextField = "dept_desc";
            DdlReceiptDept.DataValueField = "dept_id";
            DdlReceiptDept.DataBind();
        }

        protected void DdlReceiptPersonBind()
        {
            string sql = "select id per_id,person from jp_receipt_person order by NLSSORT(person,'NLS_SORT = SCHINESE_PINYIN_M')";
            DdlReceiptPerson.DataSource = DBHelper.createDDLView(sql);
            DdlReceiptPerson.DataTextField = "person";
            DdlReceiptPerson.DataValueField = "per_id";
            DdlReceiptPerson.DataBind();
        }

        protected void LnkShowRation_Click(object sender, EventArgs e)
        {
            GVRationDataBind();
            RecoverPageView();
        }

        protected void BtnShowRation_Click(object sender, EventArgs e)
        {
            GVRationDataBind();
            RecoverPageView();
        }

        private void GVRationDataBind()
        {
            string projectId = this.DdlProject.SelectedValue;
            string partNo = this.TxtPartNo.Text;
            string mtrNo = Misc.ToDBC(TxtMtrNo.Text.Trim());
            string mtrSeqNo = Misc.ToDBC(TxtMtrSeqNo.Text.Trim());
            StringBuilder sqlstr = new StringBuilder("select ");
            sqlstr.Append("b.project_id,");
            sqlstr.Append("a.part_no,a.c_partial_info block,");
            sqlstr.Append("ifsapp.inventory_part_api.get_description@erp_prod(a.site,a.part_no) part_description,");
            sqlstr.Append("b.misc_tab_ref_no,");
            sqlstr.Append("b.material_req_seq_no,");
            sqlstr.Append("b.ration_qty,");
            sqlstr.Append("nvl(b.issued_qty,0) issued_qty,");
            sqlstr.Append("b.activity_seq,");
            sqlstr.Append("ifs_data_api.get_all_activity_desc(b.activity_seq) activity_desc,");
            /*用api取jp_requisition表对应ration行的申请总数，考虑jp_requisition行的状态,状态限制不等于“cancelled”,如果有并发用户，校验时这个数有可能已经不准确了*/
            sqlstr.Append("nvl(jp_requisition_api.get_required_num_of_mtr(b.misc_tab_ref_no,b.material_req_seq_no),0) REQUESTED_QTY,");//配送单中的数量
            sqlstr.Append("b.ration_qty-nvl(jp_requisition_api.get_required_num_of_mtr(b.misc_tab_ref_no,b.material_req_seq_no),0) REMAIN_QTY,");
            sqlstr.Append("a.issue_from_inv ");
            sqlstr.Append(",a.DESIGN_CODE,a.C_PARTIAL_INFO PARTIAL_INFO "); //ming.li 20130321 10:50 增加物料属性
            sqlstr.Append(" from ");
            sqlstr.Append("IFSAPP.PROJECT_MISC_PROCUREMENT@erp_prod a,");
            sqlstr.Append("IFSAPP.PROJ_PROCU_RATION@erp_prod b ");
            sqlstr.Append(" where ");
            sqlstr.Append(" a.matr_seq_no=b.misc_tab_ref_no ");
            if (!(projectId == "" || projectId == "0"))
            {
                sqlstr.Append(string.Format(" and a.project_id='{0}'", projectId));
            }
            if (partNo != "")
            {
                sqlstr.Append(string.Format(" and a.part_no ='{0}'",partNo));
            }
            if (mtrNo != "")
            {
                string[] _mtrs ;
                int _p = mtrNo.IndexOf("..");
                if (_p > 0)
                {
                    sqlstr.Append(string.Format(" and b.misc_tab_ref_no>=to_number('{0}') and b.misc_tab_ref_no<=to_number('{1}')", mtrNo.Substring(0, _p), mtrNo.Substring(_p + 2)));
                }
                else
                {
                    _mtrs = mtrNo.Split(';');
                    if (_mtrs.Length > 1)
                    {
                        sqlstr.Append(" and b.misc_tab_ref_no in (");
                        for (int i = 0; i < _mtrs.Length; i++)
                        {
                            sqlstr.Append(string.Format("'{0}'", _mtrs[i]));
                            if (i < _mtrs.Length - 1) sqlstr.Append(",");
                        }
                        sqlstr.Append(")");
                    }
                    else
                    {
                        sqlstr.Append(string.Format(" and to_char(b.misc_tab_ref_no)='{0}'", mtrNo));
                    }
                }
            }
            if (mtrSeqNo != "")
            {
                string[] _mtrs;
                int _p = mtrSeqNo.IndexOf("..");
                if (_p > 0)
                {
                    sqlstr.Append(string.Format(" and b.material_req_seq_no>=to_number('{0}') and b.material_req_seq_no<=to_number('{1}')", mtrSeqNo.Substring(0, _p), mtrSeqNo.Substring(_p + 2)));
                }
                else
                {
                    _mtrs = mtrSeqNo.Split(';');
                    if (_mtrs.Length > 1)
                    {
                        sqlstr.Append(" and b.material_req_seq_no in (");
                        for (int i = 0; i < _mtrs.Length; i++)
                        {
                            sqlstr.Append(string.Format("'{0}'", _mtrs[i]));
                            if (i < _mtrs.Length - 1) sqlstr.Append(",");
                        }
                        sqlstr.Append(")");
                    }
                    else
                    {
                        sqlstr.Append(string.Format(" and to_char(b.material_req_seq_no)='{0}'", mtrSeqNo));
                    }
                }
            }
            sqlstr.Append(" and b.ration_qty > nvl(b.issued_qty,0)");
            //sqlstr.Append(" and ((a.issue_from_inv=0");
            //sqlstr.Append(" and ifsapp.PROJ_PROCU_RATION_API.Get_Misc_Tab_Num_Info@erp_prod(b.MISC_TAB_REF_NO, 'QTY_RECEIVED') > nvl(b.issued_qty,0))");
            //sqlstr.Append(" or a.issue_from_inv = 1)");

            GVRation.DataSource = Lib.DBHelper.createGridView(sqlstr.ToString());
            GVRation.DataBind();
        }

        protected void RecoverPageView()
        {
            TxtPartName.Text = Request.Form["TxtPartName"];
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ReqQtyValidation()) return;

            TextBox txtBox_ = new TextBox();
            DropDownList ddl_lack_type_ = new DropDownList(); //ming.li 增加缺货处理类型
            double reqQty;
            using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open(); 
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand("jp_demand_api.new_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("v_demand_id", OleDbType.VarChar,20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("v_matr_seq_no", OleDbType.VarChar);
                cmd.Parameters.Add("v_matr_seq_line_no", OleDbType.Numeric);
                cmd.Parameters.Add("v_require_qty", OleDbType.Numeric);
                cmd.Parameters.Add("v_project_block", OleDbType.VarChar);
                cmd.Parameters.Add("v_project_system", OleDbType.VarChar).Value=TxtSystem.Text;
                cmd.Parameters.Add("v_work_content", OleDbType.VarChar).Value = TxtWorkContent.Text;
                cmd.Parameters.Add("v_place", OleDbType.VarChar).Value = DdlProdSite.SelectedValue;
                cmd.Parameters.Add("v_receiver", OleDbType.VarChar).Value = DdlReceiptPerson.SelectedItem.Text;
                cmd.Parameters.Add("v_receiver_ic", OleDbType.VarChar).Value = TxtIC.Text;
                cmd.Parameters.Add("v_receive_date", OleDbType.VarChar).Value = TxtDate.Text;
                cmd.Parameters.Add("v_receiver_contact", OleDbType.VarChar).Value = TxtContact.Text;
                cmd.Parameters.Add("v_receipt_dept", OleDbType.VarChar).Value = DdlReceiptDept.SelectedValue;
                cmd.Parameters.Add("v_crane", OleDbType.VarChar).Value = ChkDz.Checked == true ? "1" : "0";
                cmd.Parameters.Add("v_recorder", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                cmd.Parameters.Add("v_req_group", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).GroupID;
                cmd.Parameters.Add("v_lack_type", OleDbType.VarChar);
                foreach (GridViewRow gvr in GVRation.Rows)
                {
                    txtBox_ = (TextBox)(gvr.FindControl("TxtReqQty"));
                    if (txtBox_.Text == "" || txtBox_.Text == null) continue;
                    reqQty = Convert.ToDouble(txtBox_.Text);
                    ddl_lack_type_ = (DropDownList)(gvr.FindControl("DDL_QH")); //ming.li 增加缺货处理类型
                    if (reqQty > 0)
                    {
                        cmd.Parameters["v_matr_seq_no"].Value = gvr.Cells[1].Text;
                        cmd.Parameters["v_matr_seq_line_no"].Value = gvr.Cells[2].Text;
                        cmd.Parameters["v_require_qty"].Value = reqQty;
                        cmd.Parameters["v_project_block"].Value = gvr.Cells[13].Text;
                        cmd.Parameters["v_lack_type"].Value = ddl_lack_type_.SelectedValue;
                        cmd.ExecuteNonQuery();
                        ReqIDSessionHandler("ADD", cmd.Parameters["v_demand_id"].Value.ToString());//ming.li 20130327
                    }
                }
                tr.Commit();
            }
            GVRationDataBind();
            RecoverPageView();
            GVDataBind();
        }

        private Boolean ReqQtyValidation()
        {
            decimal _rationQty;
            decimal _issueQty;
            decimal _remainQty;
            decimal _currRequireQty;

            TextBox _txtBox;

            foreach (GridViewRow gvr in GVRation.Rows)
            {
                _txtBox = (TextBox)(gvr.FindControl("TxtReqQty"));
                if (_txtBox.Text == "" || _txtBox.Text == null) continue;
                _currRequireQty = Convert.ToDecimal(_txtBox.Text);

                if(_currRequireQty <= 0 ) continue;

                _rationQty = Convert.ToDecimal(gvr.Cells[5].Text);
                _issueQty = Convert.ToDecimal(gvr.Cells[6].Text);
                _remainQty = Convert.ToDecimal(gvr.Cells[8].Text);

                if (_currRequireQty > Math.Min(_rationQty - _issueQty, _remainQty))
                {
                    Misc.Message(this.GetType(), this.ClientScript, string.Format("第{0}行，需求数量超出最大可申请量{1},参考{2} {3}", gvr.RowIndex + 1, Math.Min(_rationQty - _issueQty, _remainQty), gvr.Cells[1].Text, gvr.Cells[2].Text));
                    return false;
                }                
            }

            return true;
        }

        protected void ReqIDSessionHandler(string mode, string reqID)
        {
            ArrayList req_id;
            if (Session["reqid"] == null)
            {
                ArrayList reqId_ = new ArrayList();
                Session["reqid"] = reqId_;
            }
            req_id = (ArrayList)Session["reqid"];
            switch (mode)
            {
                case "ADD":                    
                    req_id.Add(reqID); 
                    break;
                case "REMOVE":
                    req_id.Remove(reqID);
                    break;
                default:
                    break;
            }                       
        }

        protected void GVDataBind()
        {            
            if (Session["reqid"] != null)
            {                
                ArrayList arr_ = (ArrayList)Session["reqid"];
                if (arr_.Count != 0)
                {
                    StringBuilder sqlstr = new StringBuilder("select demand_id,");//ming.li 20130327
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
                    sqlstr.Append("work_content,");
                    sqlstr.Append("place,");
                    sqlstr.Append("place_description,");
                    sqlstr.Append("receiver,");
                    sqlstr.Append("receiver_ic,");
                    sqlstr.Append("to_char(receive_date,'yyyy-mm-dd') receive_date,");
                    sqlstr.Append("receiver_contact,");
                    sqlstr.Append("crane,");
                    sqlstr.Append("recorder,");
                    sqlstr.Append("record_time, ");
                    sqlstr.Append("rowstate, ");
                    sqlstr.Append("rowversion,");
                    sqlstr.Append("rowid objid");
                    sqlstr.Append(",decode(lack_type,'1','继续配送','2','取消配送','3','需确认') lack_type");
                    sqlstr.Append(" from jp_demand ");//ming.li 20130327
                    sqlstr.Append(" where rowstate = 'init' and demand_id in (");//ming.li 20130327
                    for (int i = 0; i < arr_.Count; i++)
                    {
                        if (i == arr_.Count - 1)
                        {
                            sqlstr.Append("'" + arr_[i].ToString() + "')");
                        }
                        else
                        {
                            sqlstr.Append("'" + arr_[i].ToString() + "',");
                        }
                    }
                    GVData.DataSource = Lib.DBHelper.createGridView(sqlstr.ToString());
                    GVData.DataKeyNames = new string[] { "demand_id" };
                    GVData.DataBind();
                    return;
                }
            }           
            GVData.DataSource = null;
            GVData.DataBind();
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
                        ReqIDSessionHandler("REMOVE", id);
                        GVRationDataBind();
                        RecoverPageView();
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

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            GVDataBind();
            GVRationDataBind();
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                CheckBox chk = e.Row.FindControl("ChkCrane") as CheckBox;
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["crane"].ToString() == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                chk.Enabled = false;
            }
        }

        protected void GVRation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

                ((TextBox)e.Row.FindControl("TxtReqQty")).Attributes.Add("onblur", string.Format("return checkRationNum(this,'{0}','{1}','{2}');",
                    DataBinder.Eval(e.Row.DataItem,"ration_qty"),
                    DataBinder.Eval(e.Row.DataItem, "issued_qty"),
                    DataBinder.Eval(e.Row.DataItem, "REQUESTED_QTY")
                    ));
            }
        }


        protected void BtnAddPerson_Click(object sender, EventArgs e)
        {
            string receiptName = TxtReceiptPersonName.Text;
            string receiptIC = TxtReceiptPersonIC.Text;
            string receiptContact = TxtReceiptPersonContact.Text;
            string receiptPersonId;

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand("jp_receipt_person_api.new_", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (conn.State != ConnectionState.Open) conn.Open();

                cmd.Parameters.Add("v_receipt_name", OleDbType.VarChar).Value = receiptName;
                cmd.Parameters.Add("v_receipt_ic", OleDbType.VarChar).Value = receiptIC;
                cmd.Parameters.Add("v_receipt_contact", OleDbType.VarChar).Value = receiptContact;
                cmd.Parameters.Add("v_id", OleDbType.VarChar, 100).Direction = ParameterDirection.Output;


                try
                {
                    cmd.ExecuteNonQuery();
                    receiptPersonId = cmd.Parameters["v_id"].Value.ToString();
                    TxtIC.Text = receiptIC;
                    TxtContact.Text = receiptContact;

                    DdlReceiptPersonBind();
                    DdlReceiptPerson.SelectedIndex = DdlReceiptPerson.Items.IndexOf(DdlReceiptPerson.Items.FindByValue(receiptPersonId));
                    SetReceiptPersonInfo(receiptPersonId);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void SetReceiptPersonInfo(string id)
        {
            DataView dv = DBHelper.createDataset(string.Format("select id, person, contact, company_id, ic, state, use_times, last_time from jp_receipt_person where id='{0}'", id)).Tables[0].DefaultView;
            TxtIC.Text = dv[0]["ic"].ToString();
            TxtContact.Text = dv[0]["contact"].ToString();
        }

        protected void DdlReceiptPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            string receiptPersonId = DdlReceiptPerson.SelectedValue;
            SetReceiptPersonInfo(receiptPersonId);
        }

       
       
    }
}
