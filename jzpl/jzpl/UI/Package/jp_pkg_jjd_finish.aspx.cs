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

namespace Package
{
    public partial class jp_pkg_jjd_finish : System.Web.UI.Page
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
                        divJjdQuery.Visible = true;
                        divJjdDisplay.Visible = false;

                        DdlQReceiptPlaceDataBind();
                        DdlQReceiptDeptDataBind();
                        dataBox.Visible = false;
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_JJD_FINISH ]== '1') return true;
            return false;
        }
        

        protected void BtnQJjdQuery_Click(object sender, EventArgs e)
        {
            GVJjdListDataBind();
        }

        private void DdlQReceiptPlaceDataBind()
        {
            DdlQReceiptPlace.DataSource = DBHelper.createDDLView("select  place_id, company_id||' '||place_name place_name from jp_receipt_place where state='1'");
            DdlQReceiptPlace.DataTextField = "place_name";
            DdlQReceiptPlace.DataValueField = "place_id";
            DdlQReceiptPlace.DataBind();
        }

        private void DdlQReceiptDeptDataBind()
        {
            DdlQReceiptDept.DataSource = DBHelper.createDDLView("select dept_id,company||' '||dept_desc dept_name from jp_receipt_dept where  state='1'");
            DdlQReceiptDept.DataTextField = "dept_name";
            DdlQReceiptDept.DataValueField = "dept_id";
            DdlQReceiptDept.DataBind();
        }

        private void GVJjdListDataBind()
        {
            dataBox.Visible = true;
            StringBuilder sql = new StringBuilder();

            sql.Append("select jjd_no,place_id||' '||place_name receipt_place,receipt_person,receipt_date_str,state,receipt_dept_name from jp_pkg_jjd_v where 1=1");
            if (TxtQJjdNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and jjd_no='{0}'", TxtQJjdNo.Text.Trim()));
            }
            if (TxtQReceiptDate.Text != "")
            {
                sql.Append(string.Format(" and receipt_date=to_date('{0}','yyyy-mm-dd')", TxtQReceiptDate.Text));
            }
            if (DdlQReceiptPlace.SelectedValue != "0")
            {
                sql.Append(string.Format(" and place_id='{0}'", DdlQReceiptPlace.SelectedValue));
            }
            if (DdlQReceiptDept.SelectedValue != "0")
            {
                sql.Append(string.Format(" and receipt_dept='{0}'", DdlQReceiptDept.SelectedValue));
            }
            if (TxtQReceiptPerson.Text.Trim() != "")
            {
                sql.Append(string.Format(" and receipt_person like '{0}'", TxtQReceiptPerson.Text.Trim()));
            }
            if (TxtState.Text.Trim() != "")
            {
                sql.Append(string.Format(" and state in ({0})", StateWhereString(TxtState.Text.Trim())));
            }
            sql.Append(" order by jjd_no");

            GVJjdList.DataSource = DBHelper.createGridView(sql.ToString());
            GVJjdList.DataBind();
            if (GVJjdList.Rows.Count == 0)
            {
                dataBox.Visible = false;
            }            
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
                case "P":
                    return "partial finished";
                case "F":
                    return "finished";
                default:
                    break;
            }
            return null;
        }

        protected void GVDisplayJjdLine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string state_;
            TextBox objtext_;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("TxtIssueQty") != null)
                {
                    objtext_ = (TextBox)(e.Row.FindControl("TxtIssueQty"));
                    objtext_.Attributes.Add("onblur", string.Format("CheckNum('{0}')", objtext_.ClientID));
                }
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");


                state_ = DataBinder.Eval(e.Row.DataItem, "rowstate").ToString();
                if (state_ == "init")
                {
                    ((ImageButton)e.Row.FindControl("LnkIssue")).Visible = true;
                    ((Image)e.Row.FindControl("ImgNotIssue")).Visible = false;
                    ((TextBox)e.Row.FindControl("TxtIssueQty")).Visible = true;
                    ((Label)e.Row.FindControl("LblIssueQty")).Visible = false;
                }
                if (state_ == "finished")
                {
                    ((ImageButton)e.Row.FindControl("LnkIssue")).Visible = false;
                    ((Image)e.Row.FindControl("ImgNotIssue")).Visible = true;
                    ((TextBox)e.Row.FindControl("TxtIssueQty")).Visible = false;
                    ((Label)e.Row.FindControl("LblIssueQty")).Visible = true;
                }
            }
        }

        protected void GVJjdList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "displayJjd")
            {
                divJjdQuery.Visible = false;
                divJjdDisplay.Visible = true;

                DisplayJjd(e.CommandArgument.ToString());
            }
        }

        protected void GVJjdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVJjdList.PageIndex = e.NewPageIndex;
            GVJjdListDataBind();
        }

        private void DisplayJjd(string jjd_no_)
        {
            PkgJjd objJjd_ = new PkgJjd(jjd_no_);

            divJjdQuery.Visible = false;
            divJjdDisplay.Visible = true;

            JjdHeadBaseInfoDataBind(objJjd_);
            GVDisplayJjdLineDataBind(jjd_no_);

            if (objJjd_.State != "init")
            {
                BtnAllFinsh.Enabled = false;
            }
        }

        private void JjdHeadBaseInfoDataBind(PkgJjd objJjd_)
        {
            TxtJjdNo1.Text = objJjd_.JjdNo;
            TxtReceiptDate1.Text = objJjd_.ReceiptDate;
            TxtReceiptPerson1.Text = objJjd_.ReceiptPerson;
            TxtReceiptDept1.Text = objJjd_.ReceiptDept;
            TxtReceiptPlace1.Text = objJjd_.PlaceId;
            TxtState1.Text = objJjd_.StateCh;
        }

        private void GVDisplayJjdLineDataBind(string jjd_no_)
        {
            string sql;
            sql = string.Format("select * from jp_pkg_jjd_line_v where jjd_no ='{0}'", jjd_no_);

            GVDisplayJjdLine.DataSource = DBHelper.createGridView(sql);
            GVDisplayJjdLine.DataKeyNames = new string[] { "objid" };
            GVDisplayJjdLine.DataBind();
        }

        protected void GVDisplayJjdLine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                int idx;
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Transaction = tr;
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                if (e.CommandName == "Finish")
                {
                    idx = ((GridViewRow)(((ImageButton)(e.CommandSource)).Parent.Parent)).RowIndex;
                    decimal issueQty = Convert.ToDecimal(((TextBox)((GridView)sender).Rows[idx].FindControl("TxtIssueQty")).Text);

                    cmd.CommandText = "jp_pkg_jjd_line_api.finish_";
                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = e.CommandArgument.ToString();
                    cmd.Parameters.Add("v_finish_qty", OleDbType.Numeric).Value = issueQty;
                    cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                    cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["v_msg"].Value.ToString() != "1")
                    {
                        tr.Rollback();
                        Misc.Message(this.GetType(), ClientScript, "MSG:"+cmd.Parameters["v_msg"].Value.ToString());
                    }
                    else
                    {
                        tr.Commit();
                        DisplayJjd(TxtJjdNo1.Text);
                    }
                }
            }
        }

        protected void LnkBtnBackCreateJjd_Click(object sender, EventArgs e)
        {
            divJjdQuery.Visible = true;
            divJjdDisplay.Visible = false;

            GVJjdListDataBind();
        }

        protected void BtnAllFinsh_Click(object sender, EventArgs e)
        {
            string msg_;
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {

                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand("jp_pkg_jjd_api.finish_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Add("v_jjd_no", OleDbType.VarChar).Value = TxtJjdNo1.Text;
                //cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    DisplayJjd(TxtJjdNo1.Text);
                }
                //msg_ = cmd.Parameters["v_msg"].Value.ToString();
                //if (msg_ != "1")
                catch (Exception ex)
                {
                    tr.Rollback();
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
                    }
                    else
                    {
                        throw;
                    }
                }

            }
        }
    }
}
