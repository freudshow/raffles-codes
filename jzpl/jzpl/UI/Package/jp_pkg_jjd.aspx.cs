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

namespace Package
{
    public partial class pkg_jjd : System.Web.UI.Page
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
                        DdlQReceiptPlaceDataBind();
                        DdlQReceiptDeptDataBind();
                        divJjdQuery.Visible = true;
                        divJjdDisplay.Visible = false;
                        divJjdHeadInfo.Visible = false;
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_JJD] == '1') return true;
            return false;
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


        protected void BtnQJjdQuery_Click(object sender, EventArgs e)
        {
            GVJjdListDataBind();
        }

        private void GVJjdListDataBind()
        {
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
                sql.Append(string.Format(" and state in ({0})", StateWhereString(TxtState.Text)));
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

        protected void GVJjdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVJjdList.PageIndex = e.NewPageIndex;
            GVJjdListDataBind();
        }

        protected void GVJjdList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "displayJjd")
            {
                DisplayJjd(e.CommandArgument.ToString());
            }
        }

        private void DisplayJjd(string jjd_no_)
        {
            PkgJjd objJjd_ = new PkgJjd(jjd_no_);

            
            divJjdQuery.Visible = false;
            divJjdDisplay.Visible = true;
            divJjdHeadInfo.Visible = false;

            JjdHeadBaseInfoDataBind(objJjd_);
            GVDisplayJjdLineDataBind(jjd_no_);
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
            sql = string.Format("select * from jp_pkg_jjd_line_v where jjd_no ='{0}' order by part_no", jjd_no_);

            GVDisplayJjdLine.DataSource = DBHelper.createGridView(sql);
            GVDisplayJjdLine.DataBind();
        }

        protected void LnkBtnBackCreateJjd_Click(object sender, EventArgs e)
        {
            divJjdQuery.Visible = true;
            divJjdDisplay.Visible = false;
            divJjdHeadInfo.Visible = false;
        }

        protected void LnkBtnJjdExtHeadInfoDisplay_Click(object sender, EventArgs e)
        {
            divJjdHeadInfo.Visible = true;

            PkgJjd objJjd_ = new PkgJjd(TxtJjdNo1.Text);

            LblZHd.Text = objJjd_.ZhPlace;
            LblZHr.Text = objJjd_.ZhPerson;
            LblZHDh.Text = objJjd_.ZhContract;
            LblZHtime.Text = objJjd_.ZhArrTime;
            LblZHSTime.Text = objJjd_.ZhStarDate;
            LblZHETime.Text = objJjd_.ZhEndDate;

            LblXQbm.Text = objJjd_.XQDept;
            LblXQlxr.Text = objJjd_.XQPerson;
            LblXQdh.Text = objJjd_.XQContract;

            LblCYgs.Text = objJjd_.CyCompany;
            LblCYPer.Text = objJjd_.CyPerson;
            LblCYdh.Text = objJjd_.CyContract;
            LblCYph.Text = objJjd_.CycCarNo;
            LblCYjz.Text = objJjd_.CyDoc;

            LblXHSTime.Text = objJjd_.XhStarDate;
            LblXHETime.Text = objJjd_.XhEndDate;

            ChkSafe.Checked = objJjd_.Safe == "1" ? true : false;

            ChkSafe.Enabled = false;
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            StringBuilder Html_ = new StringBuilder();
            Html_.Append("<script type='text/javascript'>");
            Html_.Append("\nwindow.open('jp_pkg_jjd_report.aspx?jjdno=" + TxtJjdNo1.Text + "','jjdreport','height=600px, width=800px, toolbar =no, menubar=no, scrollbars=no, resizable=yes, location=no, status=no').focus()");
            Html_.Append("\n</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "script1", Html_.ToString());
        }
    }
}
