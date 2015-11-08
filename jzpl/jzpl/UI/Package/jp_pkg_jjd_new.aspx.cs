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
    public partial class pkg_jjd_new : System.Web.UI.Page
    {
        private BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
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
                        divCreateJjd.Visible = true;
                        divReqData.Visible = false;

                        BtnJoinJjd.Disabled = true;
                        BtnNewJjd.Enabled = false;
                        divJjdDisplay.Visible = false;
                        divJjdQuery.Visible = false;

                        DdlProjectDataBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_JJD_NEW] == '1') return true;
            return false;
        }

        protected void TxtRecieveDate_TextChanged(object sender, EventArgs e)
        {
            InitQuery();
        }

        private void DdlReceiptPlaceDataBind(Boolean _date, Boolean _dept, Boolean _person)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct place_id,jp_receipt_place_api.get_name(place_id) place_name from jp_pkg_requisition where rowstate in ('released','issued') and psflag='1' and nvl(jjd_qty,0)<released_qty");
            if (_date)
            {
                sql.Append(string.Format(" and receipt_date = to_date('{0}','yyyy-mm-dd')", TxtRecieveDate.Text));
            }
            if (_dept)
            {
                sql.Append(string.Format(" and receipt_dept ='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (_person)
            {
                sql.Append(string.Format(" and receiver ='{0}'", DdlReceiptPerson.SelectedValue));
            }
            DdlReceiptPlace.DataSource = DBHelper.createDDLView(sql.ToString());
            DdlReceiptPlace.DataTextField = "place_name";
            DdlReceiptPlace.DataValueField = "place_id";
            DdlReceiptPlace.DataBind();
        }

        private void DdlReceiptDeptDataBind(Boolean _date, Boolean _place, Boolean _person)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select distinct receipt_dept,jp_receipt_dept_api.get_name(receipt_dept) dept_name from jp_pkg_requisition where  rowstate in ('released','issued') and psflag='1' and nvl(jjd_qty,0)<released_qty");
            if (_date)
            {
                sql.Append(string.Format(" and receipt_date = to_date('{0}','yyyy-mm-dd')", TxtRecieveDate.Text));
            }
            if (_place)
            {
                sql.Append(string.Format(" and place_id='{0}'", DdlReceiptPlace.SelectedValue));
            }
            if (_person)
            {
                sql.Append(string.Format(" and receiver='{0}'", DdlReceiptPerson.SelectedValue));
            }
            DdlReceiptDept.DataSource = DBHelper.createDDLView(sql.ToString());
            DdlReceiptDept.DataTextField = "dept_name";
            DdlReceiptDept.DataValueField = "receipt_dept";
            DdlReceiptDept.DataBind();
        }

        private void DdlReceiptPersonDataBind(Boolean _date, Boolean _place, Boolean _dept)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct receiver,receiver receiver_name from jp_pkg_requisition where  rowstate in ('released','issued') and psflag='1' and nvl(jjd_qty,0)<released_qty");
            if (_date)
            {
                sql.Append(string.Format(" and receipt_date = to_date('{0}','yyyy-mm-dd')", TxtRecieveDate.Text));
            }
            if (_place)
            {
                sql.Append(string.Format(" and place_id='{0}'", DdlReceiptPlace.SelectedValue));
            }
            if (_dept)
            {
                sql.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            DdlReceiptPerson.DataSource = DBHelper.createDDLView(sql.ToString());
            DdlReceiptPerson.DataTextField = "receiver_name";
            DdlReceiptPerson.DataValueField = "receiver";
            DdlReceiptPerson.DataBind();
        }

        protected void DdlReceiptPlace_SelectedIndexChanged(object sender, EventArgs e)
        {

            //列表选定后Enalbed设置为false
            //列表Enabled为true,重新邦定
            //列表Enable为true,不能作为其他列表邦定的条件
            Boolean deptEnabled_;
            Boolean personEnabled_;
            deptEnabled_ = DdlReceiptDept.Enabled;
            personEnabled_ = DdlReceiptPerson.Enabled;
            DdlReceiptPlace.Enabled = false;

            if (deptEnabled_)
            {
                DdlReceiptDeptDataBind(true, true, !personEnabled_);
            }
            if (personEnabled_)
            {
                DdlReceiptPersonDataBind(true, true, !deptEnabled_);
            }

        }

        protected void DdlReceiptDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean placeEnabled_;
            Boolean personEnabled_;
            placeEnabled_ = DdlReceiptPlace.Enabled;
            personEnabled_ = DdlReceiptPerson.Enabled;
            DdlReceiptDept.Enabled = false;

            if (placeEnabled_)
            {
                DdlReceiptPlaceDataBind(true, true, !personEnabled_);
            }
            if (personEnabled_)
            {
                DdlReceiptPersonDataBind(true, !placeEnabled_, true);
            }
        }

        protected void DdlReceiptPerson_SelectedIndexChanged(object sender, EventArgs e)
        {

            Boolean placeEnabled_;
            Boolean deptEnabled_;
            placeEnabled_ = DdlReceiptPlace.Enabled;
            deptEnabled_ = DdlReceiptDept.Enabled;
            DdlReceiptPerson.Enabled = false;

            if (placeEnabled_)
            {
                DdlReceiptPlaceDataBind(true, !deptEnabled_, true);
            }
            if (deptEnabled_)
            {
                DdlReceiptDeptDataBind(true, !placeEnabled_, true);
            }
        }

        protected void ImgBtnQuery_Click(object sender, ImageClickEventArgs e)
        {
            InitQuery();
        }

        private void InitQuery()
        {
            DdlReceiptPlace.Enabled = true;
            DdlReceiptPerson.Enabled = true;
            DdlReceiptDept.Enabled = true;

            DdlReceiptPlaceDataBind(true, false, false);
            DdlReceiptDeptDataBind(true, false, false);
            DdlReceiptPersonDataBind(true, false, false);
        }

        protected void LnkBtnInitQuery_Click(object sender, EventArgs e)
        {
            InitQuery();
        }

        private void DdlProjectDataBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, string.Empty);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVReqDataDataBind();
            GVJjdNoDataBind();

            if (GVReqData.Rows.Count > 0)
            {
                divReqData.Visible = true;

                BtnNewJjd.Enabled = true;
                BtnJoinJjd.Disabled = false;
            }
            else
            {
                divReqData.Visible = false;

                BtnJoinJjd.Disabled = true;
                BtnNewJjd.Enabled = false;
            }
        }

        private void GVReqDataDataBind()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("select t.*,released_qty - nvl(jjd_qty,0) kp_qty,nvl(jjd_qty,0) yp_qty from jp_pkg_requisition_v t");
            sql.Append(" where rowstate in ('released','issued') and psflag='1' and nvl(jjd_qty,0)<released_qty");
            //sql.Append(" and nvl(jjd_qty,0)<issued_qty");

            if (TxtRecieveDate.Text == "" || DdlReceiptDept.SelectedValue == "0" || DdlReceiptPlace.SelectedValue == "0" || DdlReceiptPerson.SelectedValue == "0")
            {
                Misc.Message(this.GetType(), ClientScript, "请输入必添的项目后，再查询！");
                return;
            }
            if (DdlReceiptPlace.SelectedValue != "0")
            {
                sql.Append(string.Format(" and place_id='{0}' ", DdlReceiptPlace.SelectedValue));
            }
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sql.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (DdlReceiptPerson.SelectedValue != "0")
            {
                sql.Append(string.Format(" and receiver='{0}'", DdlReceiptPerson.SelectedValue));
            }
            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id='{0}'", DdlProject.SelectedValue));
            }
            sql.Append(string.Format(" and receipt_date = to_date('{0}','yyyy-mm-dd')", TxtRecieveDate.Text));
            sql.Append(" order by part_no");

            GVReqData.DataSource = DBHelper.createGridView(sql.ToString());
            GVReqData.DataKeyNames = new string[] { "objid" };
            GVReqData.DataBind();
        }

        protected void GVReqData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string kpqty_;
            string clientid_;
            TextBox objtext_;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                kpqty_ = DataBinder.Eval(e.Row.DataItem, "kp_qty").ToString();
                objtext_ = ((TextBox)(e.Row.FindControl("TxtJjdLineQty")));
                clientid_ = objtext_.ClientID;
                objtext_.Attributes.Add("onclick", string.Format("SetPSQty('{0}','{1}')", kpqty_, clientid_));
                objtext_.Attributes.Add("onblur", string.Format("CheckNum('{0}')", clientid_));

                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }

        protected void BtnNewJjd_Click(object sender, EventArgs e)
        {
            //调用存储过程
            string jjdNo_;
            string psQty_;
            string recDate_;
            string recPlace_;
            string recPerson_;
            string recDept_;
            string reqObjid_ = "";
            string reqMsg_;

            Boolean selectedFlag_ = false;

            recDate_ = TxtRecieveDate.Text;
            recPlace_ = DdlReceiptPlace.SelectedValue;
            recDept_ = DdlReceiptDept.SelectedValue;
            recPerson_ = DdlReceiptPerson.SelectedValue;


            foreach (GridViewRow grv in GVReqData.Rows)
            {
                psQty_ = ((TextBox)(grv.FindControl("TxtJjdLineQty"))).Text;

                if (Misc.DBStrToNumber(psQty_) > 0)
                {
                    selectedFlag_ = true;
                    //reqObjid_ = grv.Cells[9].Text;
                    reqObjid_ = GVReqData.DataKeys[grv.DataItemIndex].Value.ToString();
                    break;
                }
            }

            if (!selectedFlag_)
            {

                Misc.Message(this.GetType(), ClientScript, "没有添加项！");
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Transaction = tr;
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "jp_pkg_jjd_api.new_";

                cmd.Parameters.Add("v_jjd_no", OleDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = reqObjid_;
                cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                jjdNo_ = cmd.Parameters[0].Value.ToString();

                if (jjdNo_ == "")
                {
                    Misc.Message(this.GetType(), ClientScript, "err01 创建交接单失败!");
                    return;
                }

                cmd.CommandText = "jp_pkg_jjd_line_api.new_";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("v_jjd_no", OleDbType.VarChar).Value = jjdNo_;
                cmd.Parameters.Add("v_req_objid", OleDbType.VarChar);
                cmd.Parameters.Add("v_qty", OleDbType.Decimal);
                cmd.Parameters.Add("v_msg", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                foreach (GridViewRow grv in GVReqData.Rows)
                {
                    psQty_ = ((TextBox)(grv.FindControl("TxtJjdLineQty"))).Text;
                    if (Misc.DBStrToNumber(psQty_) == 0) continue;
                    //reqObjid_ = grv.Cells[9].Text;
                    reqObjid_=GVReqData.DataKeys[grv.DataItemIndex].Value.ToString();
                    cmd.Parameters["v_req_objid"].Value = reqObjid_;
                    cmd.Parameters["v_qty"].Value = Convert.ToDecimal(psQty_);

                    cmd.ExecuteNonQuery();

                    reqMsg_ = cmd.Parameters["v_msg"].Value.ToString();

                    if (reqMsg_ != "1")
                    {
                        Misc.Message(this.GetType(), ClientScript, string.Format("err02 创建交接单失败！{0}", reqMsg_));
                        return;
                    }
                }
                tr.Commit();
            }
            DisplayJjd(jjdNo_);
        }

        private void GVJjdNoDataBind()
        {
            StringBuilder sql = new StringBuilder("select jjd_no from jp_pkg_jjd where state='init'");
            sql.Append(string.Format(" and place_id='{0}' and receipt_date=to_date('{1}','yyyy-mm-dd') and receipt_dept='{2}' and receipt_person ='{3}'",
                DdlReceiptPlace.SelectedValue, TxtRecieveDate.Text, DdlReceiptDept.SelectedValue, DdlReceiptPerson.SelectedValue));
            GVJjdNo.DataSource = DBHelper.createGridView(sql.ToString());
            GVJjdNo.DataBind();
        }

        protected void GVJjdNo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string jjdNo_;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                jjdNo_ = DataBinder.Eval(e.Row.DataItem, "jjd_no").ToString();
                e.Row.Attributes.Add("onclick", "myf('TxtJjdNo').value='" + jjdNo_ + "'");

                //e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                //e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

                e.Row.Cells[0].Attributes.Add("onclick", "SetSelectedColor(this);");
            }
        }

        protected void BtnInsertJjd_Click(object sender, EventArgs e)
        {
            string jjdNo_;
            string psQty_;
            string reqObjid_;
            string reqMsg_;

            jjdNo_ = TxtJjdNo.Text;

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Transaction = tr;
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                if (jjdNo_ == "")
                {
                    Misc.Message(this.GetType(), ClientScript, "err01 创建交接单失败!");
                    return;
                }

                cmd.CommandText = "jp_pkg_jjd_line_api.new_";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("v_jjd_no", OleDbType.VarChar).Value = jjdNo_;
                cmd.Parameters.Add("v_req_objid", OleDbType.VarChar);
                cmd.Parameters.Add("v_qty", OleDbType.Decimal);
                cmd.Parameters.Add("v_msg", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                foreach (GridViewRow grv in GVReqData.Rows)
                {
                    psQty_ = ((TextBox)(grv.FindControl("TxtJjdLineQty"))).Text;
                    if (psQty_ == "") continue;
                    //reqObjid_ = grv.Cells[9].Text;
                    reqObjid_ = GVReqData.DataKeys[grv.DataItemIndex].Value.ToString();
                    cmd.Parameters["v_req_objid"].Value = reqObjid_;
                    cmd.Parameters["v_qty"].Value = Convert.ToDecimal(psQty_);

                    cmd.ExecuteNonQuery();

                    reqMsg_ = cmd.Parameters["v_msg"].Value.ToString();

                    if (reqMsg_ != "1")
                    {
                        Misc.Message(this.GetType(), ClientScript, string.Format("err02 创建交接单失败！{0}", reqMsg_));
                        return;
                    }
                }
                tr.Commit();
            }

            DisplayJjd(jjdNo_);

        }

        private void GVDisplayJjdLineDataBind(string jjd_no_)
        {
            string sql;
            sql = string.Format("select * from jp_pkg_jjd_line_v where jjd_no ='{0}'", jjd_no_);

            GVDisplayJjdLine.DataSource = DBHelper.createGridView(sql);
            GVDisplayJjdLine.DataBind();
        }

        //交接单显示区域显示 其他区域不可见
        private void DisplayJjd(string jjd_no_)
        {
            PkgJjd objJjd_ = new PkgJjd(jjd_no_);

            divCreateJjd.Visible = false;
            divJjdQuery.Visible = false;
            divJjdDisplay.Visible = true;
            divJjdHeadInfo.Visible = false;

            JjdHeadBaseInfoDataBind(objJjd_);
            GVDisplayJjdLineDataBind(jjd_no_);
        }

        //交接单基本信息附值
        private void JjdHeadBaseInfoDataBind(PkgJjd objJjd_)
        {
            TxtJjdNo1.Text = objJjd_.JjdNo;
            TxtReceiptDate1.Text = objJjd_.ReceiptDate;
            TxtReceiptPerson1.Text = objJjd_.ReceiptPerson;
            TxtReceiptDept1.Text = objJjd_.ReceiptDept;
            TxtReceiptPlace1.Text = objJjd_.PlaceId;
            TxtState1.Text = objJjd_.StateCh;
        }

        //显示交接单附加信息 非编辑
        private void DisplayJjdExtHeadInfo()
        {
            divJjdHeadInfo.Visible = true;

            JjdHeadExtInfoDataBind(new PkgJjd(TxtJjdNo1.Text));
        }

        //交接单附加信息附值 非编辑
        private void JjdHeadExtInfoDataBind(PkgJjd objJjd_)
        {
            BtnSaveJjdHeadInfo.Visible = false;
            BtnJjdHeadExtInfoEditQuit.Visible = false;

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

            LblZHd.Visible = true;
            LblZHr.Visible = true;
            LblZHDh.Visible = true;
            LblZHtime.Visible = true;
            LblZHSTime.Visible = true;
            LblZHETime.Visible = true;

            LblXQbm.Visible = true;
            LblXQlxr.Visible = true;
            LblXQdh.Visible = true;

            LblCYgs.Visible = true;
            LblCYPer.Visible = true;
            LblCYdh.Visible = true;
            LblCYph.Visible = true;
            LblCYjz.Visible = true;

            LblXHSTime.Visible = true;
            LblXHETime.Visible = true;

            ChkSafe.Enabled = false;

            TxtZHd.Visible = false;
            TxtZHr.Visible = false;
            TxtZHDh.Visible = false;
            TxtZHTime.Visible = false;
            TxtZHSTime.Visible = false;
            TxtZHETime.Visible = false;

            TxtXQbm.Visible = false;
            TxtXQlxr.Visible = false;
            TxtXQdh.Visible = false;

            TxtCYgs.Visible = false;
            TxtCYPer.Visible = false;
            TxtCYdh.Visible = false;
            TxtCYph.Visible = false;
            TxtCYjz.Visible = false;

            TxtXHSTime.Visible = false;
            TxtXHETime.Visible = false;
        }

        //编辑交接单附加信息 事件处理 编辑状态
        private void JjdHeadExtInfoEdit(PkgJjd objJjd_)
        {
            BtnSaveJjdHeadInfo.Visible = true;
            BtnJjdHeadExtInfoEditQuit.Visible = true;

            ChkSafe.Checked = objJjd_.Safe == "1" ? true : false;

            LblZHd.Visible = false;
            LblZHr.Visible = false;
            LblZHDh.Visible = false;
            LblZHtime.Visible = false;
            LblZHSTime.Visible = false;
            LblZHETime.Visible = false;

            LblXQbm.Visible = false;
            LblXQlxr.Visible = false;
            LblXQdh.Visible = false;

            LblCYgs.Visible = false;
            LblCYPer.Visible = false;
            LblCYdh.Visible = false;
            LblCYph.Visible = false;
            LblCYjz.Visible = false;

            LblXHSTime.Visible = false;
            LblXHETime.Visible = false;

            ChkSafe.Enabled = true;

            TxtZHd.Visible = true;
            TxtZHr.Visible = true;
            TxtZHDh.Visible = true;
            TxtZHTime.Visible = true;
            TxtZHSTime.Visible = true;
            TxtZHETime.Visible = true;

            TxtXQbm.Visible = true;
            TxtXQlxr.Visible = true;
            TxtXQdh.Visible = true;

            TxtCYgs.Visible = true;
            TxtCYPer.Visible = true;
            TxtCYdh.Visible = true;
            TxtCYph.Visible = true;
            TxtCYjz.Visible = true;

            TxtXHSTime.Visible = true;
            TxtXHETime.Visible = true;

            TxtZHd.Text = objJjd_.ZhPlace; ;
            TxtZHr.Text = objJjd_.ZhPerson;
            TxtZHDh.Text = objJjd_.ZhContract;
            TxtZHTime.Text = objJjd_.ZhArrTime;
            TxtZHSTime.Text = objJjd_.ZhStarDate;
            TxtZHETime.Text = objJjd_.ZhEndDate;

            TxtXQbm.Text = objJjd_.XQDept;
            TxtXQlxr.Text = objJjd_.XQPerson;
            TxtXQdh.Text = objJjd_.XQContract;

            TxtCYgs.Text = objJjd_.CyCompany;
            TxtCYPer.Text = objJjd_.CyPerson;
            TxtCYdh.Text = objJjd_.CyContract;
            TxtCYph.Text = objJjd_.CycCarNo;
            TxtCYjz.Text = objJjd_.CyDoc;

            TxtXHSTime.Text = objJjd_.XhStarDate;
            TxtXHETime.Text = objJjd_.XhEndDate;
        }

        //保存交接单附加信息的编辑结果 事件
        protected void BtnSaveJjdHeadInfo_Click(object sender, EventArgs e)
        {
            string jjdNo_ = TxtJjdNo1.Text;
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand("jp_pkg_jjd_api.jjdinfo_update", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("v_jjd_no", OleDbType.VarChar).Value = jjdNo_;
                cmd.Parameters.Add("v_zh_place", OleDbType.VarChar).Value = TxtZHd.Text;
                cmd.Parameters.Add("v_zh_person", OleDbType.VarChar).Value = TxtZHr.Text;
                cmd.Parameters.Add("v_zh_contract", OleDbType.VarChar).Value = TxtZHDh.Text;
                cmd.Parameters.Add("v_zh_star_date", OleDbType.VarChar).Value = TxtZHSTime.Text;
                cmd.Parameters.Add("v_zh_end_date", OleDbType.VarChar).Value = TxtZHETime.Text;
                cmd.Parameters.Add("v_cy_company", OleDbType.VarChar).Value = TxtCYgs.Text;
                cmd.Parameters.Add("v_cy_person", OleDbType.VarChar).Value = TxtCYPer.Text;
                cmd.Parameters.Add("v_cy_per_contract", OleDbType.VarChar).Value = TxtCYdh.Text;
                cmd.Parameters.Add("v_cy_car_no", OleDbType.VarChar).Value = TxtCYph.Text;
                cmd.Parameters.Add("v_cy_doc", OleDbType.VarChar).Value = TxtCYjz.Text;
                cmd.Parameters.Add("v_xh_star_date", OleDbType.VarChar).Value = TxtXHSTime.Text;
                cmd.Parameters.Add("v_xh_end_date", OleDbType.VarChar).Value = TxtXHETime.Text;
                cmd.Parameters.Add("v_safe", OleDbType.VarChar).Value = (ChkSafe.Checked == true) ? "1" : "0";
                cmd.Parameters.Add("v_zh_arr_time", OleDbType.VarChar).Value = TxtZHTime.Text;
                cmd.Parameters.Add("v_xq_dept", OleDbType.VarChar).Value = TxtXQbm.Text;
                cmd.Parameters.Add("v_xq_person", OleDbType.VarChar).Value = TxtXQlxr.Text;
                cmd.Parameters.Add("v_xq_contract", OleDbType.VarChar).Value = TxtXQdh.Text;

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            DisplayJjdExtHeadInfo();
        }

        //编辑交接单附加信息 事件
        protected void LnkBtnJjdExtHeadInfoEdit_Click(object sender, EventArgs e)
        {
            JjdHeadExtInfoEdit(new PkgJjd(TxtJjdNo1.Text));
        }

        //取消编辑交接单附加信息 事件
        protected void BtnJjdHeadExtInfoEditQuit_Click(object sender, EventArgs e)
        {
            JjdHeadExtInfoDataBind(new PkgJjd(TxtJjdNo1.Text));
        }

        //显示交接单附加信息 事件
        protected void LnkBtnJjdExtHeadInfoDisplay_Click(object sender, EventArgs e)
        {
            DisplayJjdExtHeadInfo();
        }

        protected void LnkBtnBackCreateJjd_Click(object sender, EventArgs e)
        {
            divCreateJjd.Visible = true;
            divReqData.Visible = false;

            BtnJoinJjd.Disabled = true;
            BtnNewJjd.Enabled = false;

            divJjdDisplay.Visible = false;
            divJjdQuery.Visible = false;

            InitQuery();

            ViewState["GVReqData"] = null;
            GVReqData.DataBind();
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
            sql.Append(" order by jjd_no");

            GVJjdList.DataSource = DBHelper.createGridView(sql.ToString());
            GVJjdList.DataBind();
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

        protected void BtnJjdQuery_Click(object sender, EventArgs e)
        {
            DisplayJjdQuery();
            ViewState["GVJjdList"] = null;
        }

        private void DisplayJjdQuery()
        {
            divCreateJjd.Visible = false;
            divJjdQuery.Visible = true;

            DdlQReceiptPlaceDataBind();
            DdlQReceiptDeptDataBind();
        }

        protected void LnkBtnQBackCreate_Click(object sender, EventArgs e)
        {
            divCreateJjd.Visible = true;
            divReqData.Visible = false;

            BtnJoinJjd.Disabled = true;
            BtnNewJjd.Enabled = false;

            divJjdDisplay.Visible = false;
            divJjdQuery.Visible = false;

            InitQuery();

            ViewState["GVReqData"] = null;
            GVReqData.DataBind();
        }

        protected void GVJjdList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "displayJjd")
            {
                DisplayJjd(e.CommandArgument.ToString());
            }
        }

        protected void GVJjdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVJjdList.PageIndex = e.NewPageIndex;
            GVJjdListDataBind();
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            StringBuilder Html_ = new StringBuilder();
            Html_.Append("<script type='text/javascript'>");
            Html_.Append("\nwindow.open('jp_pkg_jjd_report.aspx?jjdno=" + TxtJjdNo1.Text + "','jjdreport','height=600px, width=800px, toolbar =no, menubar=no, scrollbars=no, resizable=yes, location=no, status=no').focus()");
            Html_.Append("\n</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "script1", Html_.ToString());
        }

        protected void GVDisplayJjdLine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string state_;
            string headState_;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

                state_ = DataBinder.Eval(e.Row.DataItem, "rowstate").ToString();
                headState_ = TxtState1.Text;

                if (headState_ == "初始" && state_ == "init")
                {
                    ((ImageButton)e.Row.FindControl("ImgDelete")).Visible = true;
                    ((Image)e.Row.FindControl("ImgNotAccess")).Visible = false;
                }
                else
                {
                    ((ImageButton)e.Row.FindControl("ImgDelete")).Visible = false;
                    ((Image)e.Row.FindControl("ImgNotAccess")).Visible = true;
                }
            }
        }

        protected void GVDisplayJjdLine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteJjdLine")
            {
                //掉存储过程
                using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
                {
                    conn.Open();
                    OleDbTransaction tr = conn.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand("jp_pkg_jjd_line_api.delete_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = tr;
                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = e.CommandArgument;
                    cmd.Parameters.Add("v_msg", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["v_msg"].Value.ToString() != "1")
                    {
                        Misc.Message(this.GetType(), ClientScript, cmd.Parameters["v_msg"].Value.ToString());
                        tr.Rollback();
                        return;
                    }
                    tr.Commit();
                }
            }
            //重新邦定head
            //重新邦定line
            GVDisplayJjdLineDataBind(TxtJjdNo1.Text);
        }
    }
}
