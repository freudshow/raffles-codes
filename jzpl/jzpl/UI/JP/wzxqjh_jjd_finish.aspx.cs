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

namespace jzpl
{
    public partial class wzxqjh_jjd_finish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divJjdQuery.Visible = true;
                divJjdDisplay.Visible = false;

                DdlQReceiptPlaceDataBind();
                DdlQReceiptDeptDataBind();
            }
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
            StringBuilder sql = new StringBuilder();

            sql.Append("select jjd_no,place_id||' '||place_name receipt_place,receipt_person,receipt_date_str,state,receipt_dept_name from jp_jjd_v where 1=1");
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
            //Label objtext_;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.FindControl("TxtIssueQty") != null)
                //{
                //    objtext_ = (Label)(e.Row.FindControl("TxtIssueQty"));
                //    objtext_.Attributes.Add("onblur", string.Format("CheckNum('{0}')", objtext_.ClientID));
                //}
                //e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                //e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                

                state_ = DataBinder.Eval(e.Row.DataItem, "rowstate").ToString();
                if (state_ == "init")
                {
                    ((ImageButton)e.Row.FindControl("LnkIssue")).Visible = true;
                    ((Image)e.Row.FindControl("ImgNotIssue")).Visible = false;
                    ((Label)e.Row.FindControl("TxtIssueQty")).Visible = false;
                    ((Label)e.Row.FindControl("LblIssueQty")).Visible = true;
                }
                if (state_ == "finished")
                {
                    ((ImageButton)e.Row.FindControl("LnkIssue")).Visible = false;
                    ((Image)e.Row.FindControl("ImgNotIssue")).Visible = true;
                    ((Label)e.Row.FindControl("TxtIssueQty")).Visible = false;
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
            Jjd objJjd_ = new Jjd(jjd_no_);
           
            divJjdQuery.Visible = false;
            divJjdDisplay.Visible = true;            

            JjdHeadBaseInfoDataBind(objJjd_);
            GVDisplayJjdLineDataBind(jjd_no_);

            BtnAllFinsh.Enabled = true;

            if (objJjd_.State != "init")
            {
                BtnAllFinsh.Enabled = false;
            }
            
        }

        private void JjdHeadBaseInfoDataBind(Jjd objJjd_)
        {
            TxtJjdNo1.Text = objJjd_.JjdNo;
            TxtReceiptDate1.Text = objJjd_.ReceiptDate;
            TxtReceiptPerson1.Text = objJjd_.ReceiptPerson;
            TxtReceiptDept1.Text = objJjd_.ReceiptDept;
            TxtReceiptPlace1.Text = objJjd_.PlaceId;
            TxtState1.Text = objJjd_.StateCh;
            hiddenJjdObjid.Value = objJjd_.Objid;
            hiddenJjdRowversion.Value = objJjd_.RowVersion;
            hiddenJjdNo.Value = objJjd_.JjdNo;
        }

        private void GVDisplayJjdLineDataBind(string jjd_no_)
        {
            string sql;
            sql = string.Format("select * from jp_jjd_line_v where jjd_no ='{0}'", jjd_no_);

            GVDisplayJjdLine.DataSource = DBHelper.createGridView(sql);
            GVDisplayJjdLine.DataKeyNames = new string[] { "objid" };
            GVDisplayJjdLine.DataBind();
        }

        protected void GVDisplayJjdLine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] temp;
            string objid;
            string rowversion;

            temp = e.CommandArgument.ToString().Split('^');
            if (temp.Length != 3)
            {
                Misc.Message(this.GetType(), ClientScript, "错误参数。");
                return;
            }

            objid = temp[0];
            rowversion = temp[1];

            using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
            {
                try
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
                        //ming.li 2013-04-01 配送数量必须等于实际送达数量
                        //decimal issueQty = Convert.ToDecimal(((TextBox)((GridView)sender).Rows[idx].FindControl("TxtIssueQty")).Text);
                        decimal Lblzh_qty = Convert.ToDecimal(((Label)((GridView)sender).Rows[idx].FindControl("Lblzh_qty")).Text);
                        cmd.CommandText = "jp_jjd_line_api.finish_";
                        cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value =objid;
                        cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion;
                        cmd.Parameters.Add("v_finish_qty", OleDbType.Numeric).Value = Lblzh_qty;//issueQty;
                        cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        if (cmd.Parameters["v_msg"].Value.ToString() != "1")
                        {
                            tr.Rollback();
                            Misc.Message(this.GetType(), ClientScript, cmd.Parameters["v_msg"].Value.ToString());
                        }
                        else
                        {
                            tr.Commit();
                            DisplayJjd(TxtJjdNo1.Text);
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

        protected void LnkBtnBackCreateJjd_Click(object sender, EventArgs e)
        {
            divJjdQuery.Visible = true;
            divJjdDisplay.Visible = false;
        }

        protected void BtnAllFinsh_Click(object sender, EventArgs e)
        {
            string msg_;
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
               
                    if (conn.State != ConnectionState.Open) conn.Open();
                    OleDbTransaction tr = conn.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand("jp_jjd_api.finish_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = tr;
                    cmd.Parameters.Add("v_jjd_no", OleDbType.VarChar).Value = hiddenJjdNo.Value;
                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = hiddenJjdObjid.Value;
                    cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = hiddenJjdRowversion.Value;
                    cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        msg_ = cmd.Parameters["v_msg"].Value.ToString();
                        if (msg_ != "1")
                        {
                            tr.Rollback();
                            Misc.Message(this.GetType(), ClientScript, msg_);
                        }
                        else
                        {
                            tr.Commit();
                            DisplayJjd(TxtJjdNo1.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Misc.CheckIsDBCustomException(ex))
                        {
                            Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
                            tr.Rollback();
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
    }
}
