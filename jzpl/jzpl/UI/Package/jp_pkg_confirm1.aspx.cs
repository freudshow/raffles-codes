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
using System.Text;
using jzpl.Lib;

namespace Package
{
    public partial class jp_pkg_confirm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlProdSiteBind();
                DdlReceiptDeptBind();
                DdlProjectDataBind();
                DdlReqGroupDataBind();

                PreQuery();
            }
        }

        protected void DdlProdSiteBind()
        {
            DdlProdSite.DataSource = DBHelper.createDDLView("select place_id,place_name from jp_receipt_place  where state='1'");
            DdlProdSite.DataTextField = "place_name";
            DdlProdSite.DataValueField = "place_id";
            DdlProdSite.DataBind();
        }

        protected void DdlReceiptDeptBind()
        {
            DdlReceiptDept.DataSource = DBHelper.createDDLView("select dept_id,company||' '||dept_desc dept_desc from jp_receipt_dept t where state='1'");
            DdlReceiptDept.DataTextField = "dept_desc";
            DdlReceiptDept.DataValueField = "dept_id";
            DdlReceiptDept.DataBind();
        }

        private void DdlProjectDataBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);    
        }

        private void DdlReqGroupDataBind()
        {
            DdlReqGroup.DataSource = DBHelper.createDDLView("select group_id,company_id||' '||group_name group_name from jp_req_group");
            DdlReqGroup.DataTextField = "group_name";
            DdlReqGroup.DataValueField = "group_id";
            DdlReqGroup.DataBind();
        }


        private void PreQuery()
        {
            //当前登录用户
            TxtReqUser.Text = "";
            //当前日期
            TxtReqDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //当前用户的用户组
            //DdlReqGroup.SelectedIndex = DdlReqGroup.Items.FindByValue("");
            TxtReqState.Text = "c;ca";
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
                case "IS":
                    return "issued";
                default:
                    break;
            }
            return null;
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "rowstate").ToString() == "confirming")
                {
                    e.Row.FindControl("ImgOK").Visible = true;
                    e.Row.FindControl("ImgCancel").Visible = true;
                }
                else
                {
                    e.Row.FindControl("ImgOK").Visible = false;
                    e.Row.FindControl("ImgCancel").Visible = false;
                }
            }
        }

        private void GVDataDataBind()
        {
            StringBuilder sql = new StringBuilder("select * from jp_pkg_requisition_v where 1=1");
            if (TxtReqID.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(requisition_id)=upper('{0}')", TxtReqID.Text.Trim()));
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_no)=upper('{0}')", TxtPartNo.Text.Trim()));
            }
            if (DdlPSType.SelectedValue != "-1")
            {
                sql.Append(string.Format(" and psflag='{0}'", DdlPSType.SelectedValue));
            }
            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id = '{0}'", DdlProject.SelectedValue));
            }
            if (TxtPkgNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(package_no)=upper('{0}')", TxtPkgNo.Text.Trim()));
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(package_name) like upper('{0}')", TxtPkgName.Text));
            }
            if (TxtPartName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_name) like upper('{0}')", TxtPartName.Text));
            }
            if (TxtPartNameE.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_name_e) like upper('{0}')", TxtPartNameE.Text));
            }
            if (TxtPartSpec.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_spec) like upper('{0}')", TxtPartSpec.Text));
            }
            if (DdlProdSite.SelectedValue != "0")
            {
                sql.Append(string.Format(" and place_id='{0}'", DdlProdSite.SelectedValue));
            }
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sql.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (TxtReceiver.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(receiver)=upper('{0}')", TxtReceiver.Text));
            }
            if (TxtBlock.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(project_block) = upper('{0}')", TxtBlock.Text));
            }
            if (TxtSystem.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(project_system)=upper('{0}')", TxtSystem.Text));
            }
            if (TxtWorkContent.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(work_content)=upper('{0}')", TxtWorkContent.Text));
            }
            if (TxtDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and receipt_date=to_date('{0}','yyyy-mm-dd')", TxtDate.Text));
            }
            if (TxtIC.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(receiver_ic)=upper('{0}')", TxtIC.Text));
            }
            if (DdlReqGroup.SelectedValue != "0")
            {
                sql.Append(string.Format(" and req_group='{0}'", DdlReqGroup.SelectedValue));
            }
            if (TxtReqUser.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(recorder)=upper('{0}')", TxtReqUser.Text));
            }
            if (TxtReqDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and to_char(record_time,'yyyy-mm-dd') = to_char(to_date('{0}','yyyy-mm-dd'),'yyyy-mm-dd')", TxtReqDate.Text));
            }
            if (TxtReqState.Text.Trim() != "")
            {
                sql.Append(string.Format(" and rowstate in ({0})", StateWhereString(TxtReqState.Text.Trim())));
            }
            GVData.DataSource = DBHelper.createGridView(sql.ToString());
            GVData.DataKeyNames = new string[] { "requisition_id" };
            GVData.DataBind();
        }

        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
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
                    decimal _releaseQty = Convert.ToDecimal(gvr.Cells[2].Text);
                    cmd.CommandText = "jp_pkg_requisition_api.release_";
                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = temp[0];
                    cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = temp[1];
                    cmd.Parameters.Add("v_release_qty", OleDbType.Decimal).Value = _releaseQty;
                    cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = "";
                    cmd.ExecuteNonQuery();
                    //Misc.Message(Response, "下达！");
                    GVDataDataBind();
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
                        cmd.CommandText = "jp_pkg_requisition_api.cancel_";
                        cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = temp[0];
                        cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = temp[1];
                        cmd.ExecuteNonQuery();
                        //Misc.Message(Response, "取消下达！");
                        GVDataDataBind();
                    }
                }
            }
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataDataBind();
        }

    }    
}