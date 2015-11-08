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
using jzpl.Lib;
using System.Text;

namespace Package
{
    public partial class pkg_check : System.Web.UI.Page
    {        
        private string m_perimission;

        private enum CheckType { FirstCheck,NonCheck,SecondCheck };
        
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
                        MxPl.Visible = false;
                        bindDDLData();
                        DdlJcrDataBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_CHK] == '1') return true;
            return false;
        }
        
        private void bindDDLData()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DDLProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);    
        }

        private void DdlJcrDataBind()
        {
            DdlJcr.DataSource = DBHelper.createDDLView("select id value_,person_name text_ from jp_check_person where state='1'");
            DdlJcr.DataTextField = "text_";
            DdlJcr.DataValueField = "value_";
            DdlJcr.DataBind();
        }

        protected string ProjectWhereString()
        {
            StringBuilder ret = new StringBuilder();

            ListItemCollection items_ = new ListItemCollection();
            foreach (ListItem itm in DDLProject.Items)
            {
                items_.Add(itm);
            }

            items_.Remove(items_.FindByValue("0"));

            for (int i = 0; i < items_.Count; i++)
            {
                ret.Append(string.Format("'{0}'", items_[i].Value));

                if (i < items_.Count - 1)
                {
                    ret.Append(",");
                }
            }
            return ret.ToString();
        }
        
        private void BindGridData()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("select * from gen_pkg_arr_v where 1=1");

            if (DDLProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id='{0}'",this.DDLProject.SelectedValue));
            }
            else
            {
                sql.Append(string.Format(" and project_id in ({0})", ProjectWhereString()));
            }
            if (TxtArrivalId.Text.Trim() != "")
            {
                sql.Append(string.Format(" and arrived_id='{0}'", TxtArrivalId.Text));
            }
            if (TxtDHrq.Text.Trim() != "")
            {
                sql.Append(string.Format(" and to_char(arrived_date,'yyyy-mm-dd')='{0}'", TxtDHrq.Text));
            }
            if (TxtDBbh.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_no like '{0}'",TxtDBbh.Text));
            }
            if (TxtPONo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and po_no ='{0}'", TxtPONo.Text));
            }
            if (TxtGDH.Text.Trim() != "")
            {
                sql.Append(string.Format(" and dec_no like '{0}'", TxtGDH.Text));
            }
            if (TxtHTH.Text.Trim() != "")
            {
                sql.Append(string.Format(" and contract_no='{0}'", TxtHTH.Text));
            }
            if (TxtDBmc.Text.Trim() != "")
            {
                sql.Append(string.Format(" and pkg_name like '{0}'", TxtDBmc.Text));
            }
            if (TxtLJmc.Text.Trim() != "")
            {
                sql.Append(string.Format(" and (part_name_e like '{0}' or part_name like '{0}')", TxtLJmc.Text));
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and part_no like '{0}'", TxtPartNo.Text));
            }
            if (ChkCheck.Checked)
            {
                sql.Append(" and check_mark not in ('checked','nocheck')");
            }
            DataView view = DBHelper.createGridView(sql.ToString());

            CKGrid.DataSource = view;
            CKGrid.DataKeyNames = new string[] { "arrived_id", "check_mark" };
            CKGrid.DataBind();
        }

        protected void BtnResult_Click(object sender, EventArgs e)
        {
            BindGridData();
        }
        
        protected void CKGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {   
            string ArrivalID = CKGrid.DataKeys[e.NewEditIndex].Values[0].ToString();
            string state = CKGrid.DataKeys[e.NewEditIndex].Values[1].ToString();

            if (state == "nocheck" || state == "checked")
            {
                Misc.Message(this.GetType(), ClientScript, "已检验完成。");
                return;
            }
            LoadArrivalInfo(ArrivalID);
        }
       
        private void LoadArrivalInfo(string ArrivalID)
        {
            string state;
            DataView view;
            decimal okQty;
            decimal badQty;
            
            view = DBHelper.createGridView("select * from gen_pkg_arr_v where arrived_id='" + ArrivalID + "'");
            if (view.Count <= 0)
            {
                Misc.Message(this.GetType(), ClientScript, "到货ID参数错误。");
                return;
            }
            //加载到货信息
            LtDHID.Text = ArrivalID;
            LtDHrq.Text = view[0]["arr_date_ch"].ToString();
            LtDHChkMark.Text = view[0]["check_mark"].ToString();
            LtProject.Text = view[0]["project_id"].ToString() + "  " + view[0]["project_name"].ToString();
            LtDBbh.Text = view[0]["package_no"].ToString();
            LtDbms.Text = view[0]["pkg_name"].ToString();
            LtXJbh.Text = view[0]["part_no"].ToString();
            LtXJmcen.Text = view[0]["part_name_e"].ToString();
            LtXJgg.Text = view[0]["part_spec"].ToString();
            LtUnit.Text = view[0]["part_unit"].ToString();
            LtGDH.Text = view[0]["dec_no"].ToString();
            LtHTH.Text = view[0]["contract_no"].ToString();
            LtDJsl.Text = view[0]["req_qty"].ToString();
            LtDhsl.Text = view[0]["arrived_qty"].ToString() == "" ? "-" : view[0]["arrived_qty"].ToString();
            LtHgsl.Text = view[0]["ok_qty"].ToString() == "" ? "-" : view[0]["ok_qty"].ToString();
            LtBhgsl.Text = view[0]["bad_qty"].ToString() == "" ? "-" : view[0]["bad_qty"].ToString();

            state = view[0]["check_mark"].ToString();
            okQty = Convert.ToDecimal(view[0]["ok_qty"].ToString() == "" ? "0" : view[0]["ok_qty"].ToString());
            badQty = Convert.ToDecimal(view[0]["bad_qty"].ToString() == "" ? "0" : view[0]["bad_qty"].ToString());

            TxtArrQty.Text = view[0]["arrived_qty"].ToString() == "" ? view[0]["req_qty"].ToString() : view[0]["arrived_qty"].ToString();

            ChkBoxMj.Visible = true;
            ChkBoxMj.Checked = false;
            //加载检验信息
            //TxtJCR.Text = "";
            TxtJCrq.Text = "";
            TxtBZ.Text = "";
            HiddenCheckId.Value = "";            

            GVCheckRecDataBind();

            view = DBHelper.createGridView(string.Format("select * from gen_pkg_chk_v where state='init' and arrived_id='{0}'", ArrivalID));
            if (view.Count > 0)
            {
                TxtJCrq.Text = view[0]["chk_date_ch"].ToString();
                DdlJcr.SelectedIndex = DdlJcr.Items.IndexOf(DdlJcr.Items.FindByValue(view[0]["check_person"].ToString()));                
                TxtBZ.Text = view[0]["remark"].ToString();
                HiddenCheckId.Value = view[0]["check_id"].ToString();
            }

            //用户录入控制

            LnkBtnCancelRegResult.Visible = true;
            LnkBtnRegCheckResult.Visible = false;
            HiddenCheckMark.Value = "check";


            if (state == "init")
            {
                TxtArrQty.Enabled = true;
                TxtArrQty.BorderStyle = BorderStyle.NotSet;

                ChkBoxMj.Enabled = true;
                
                //HiddenGVType.Value = CheckType.FirstCheck.ToString();
                RegCheckResult(CheckType.FirstCheck);
            }

            if (state == "checking")
            {
                TxtArrQty.Enabled = false;
                TxtArrQty.BorderStyle = BorderStyle.Groove;
                
                ChkBoxMj.Enabled = false;
                
                //HiddenGVType.Value = CheckType.SecondCheck.ToString();
                RegCheckResult(CheckType.SecondCheck);             
            }

            if (state == "checked" || state=="nocheck")
            {
                Misc.Message(this.GetType(), ClientScript, "已检验完成。");
                return;
            }

            PnlCheckResult.Visible = true;
            QueryPl.Visible = false;
            MxPl.Visible = true;
        }        

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            QueryPl.Visible = true;
            MxPl.Visible = false;
        }
        
        //更新 库存表 数据
        //private void UpdateInStoreQty(OleDbConnection conn,OleDbTransaction tran)
        //{
        //    OleDbCommand comm = new OleDbCommand();
        //    comm.Connection = conn;
        //    if (conn.State == ConnectionState.Closed) conn.Open();
        //    comm.CommandType = CommandType.StoredProcedure;
        //    comm.CommandText = "gen_package_part_in_store_api.update_location_qty_";
        //    comm.Transaction = tran;
        //    GridView TmpGrid=new GridView();
        //    decimal OK_QTY = 0, BAD_QTY = 0, NO_CHECK_QTY = 0;
        //    if(CkbMJ.Checked)
        //    {
        //        TmpGrid=MJGrid;
        //    }
        //    else
        //    {
        //        TmpGrid=LSLGrid;
        //    }
        //    foreach (GridViewRow row in TmpGrid.Rows)
        //    {
        //        string LocationID = TmpGrid.DataKeys[row.RowIndex].Value.ToString();
        //        if (CkbMJ.Checked)
        //        {
        //            TextBox TextMJsl = (TextBox)row.FindControl("TxtMJsl");
        //            NO_CHECK_QTY = Convert.ToDecimal(TextMJsl.Text);
        //        }
        //        else
        //        {
        //            TextBox TextHGsl = (TextBox)row.FindControl("TxtHGsl");
        //            TextBox TextBHGsl = (TextBox)row.FindControl("TxtBuHGsl");
        //            OK_QTY = Convert.ToDecimal(TextHGsl.Text);
        //            BAD_QTY = Convert.ToDecimal(TextBHGsl.Text);
        //        }
        //        comm.Parameters.Clear();
        //        comm.Parameters.Add("v_package_no", OleDbType.VarChar, 20).Value = LtDBbh.Text;
        //        comm.Parameters.Add("v_part_no", OleDbType.VarChar, 20).Value = LtXJbh.Text;
        //        comm.Parameters.Add("v_location_id", OleDbType.VarChar, 20).Value = LocationID;
        //        comm.Parameters.Add("v_part_ok_qty", OleDbType.Numeric).Value = OK_QTY;
        //        comm.Parameters.Add("v_part_bad_qty", OleDbType.Numeric).Value = BAD_QTY;
        //        comm.Parameters.Add("v_arrival_id", OleDbType.VarChar, 20).Value = HdArrivalID.Value;
        //        comm.Parameters.Add("v_no_check_qty", OleDbType.Numeric).Value = NO_CHECK_QTY;

        //        comm.ExecuteNonQuery();
        //    }
        //}
        //protected void BtnSubmit_Click(object sender, EventArgs e)
        //{
        //    OleDbConnection JCconn = new OleDbConnection(DBHelper.OleConnectionString);
        //    //判断为免检
        //    if (CkbMJ.Checked)
        //    {
        //        OleDbCommand MJcomm = new OleDbCommand();
        //        MJcomm.Connection = JCconn;
        //        if (JCconn.State == ConnectionState.Closed) JCconn.Open();
        //        MJcomm.CommandType = CommandType.StoredProcedure;
        //        MJcomm.CommandText = "gen_package_check_api.insert_no_check_";
        //        OleDbTransaction mjTran = JCconn.BeginTransaction();
        //        MJcomm.Transaction = mjTran;
        //        MJcomm.Parameters.Add("v_arrived_id", OleDbType.VarChar, 20).Value = HdArrivalID.Value;
        //        MJcomm.Parameters.Add("v_check_date", OleDbType.Date).Value = Convert.ToDateTime(TxtJCrq.Text);
        //        MJcomm.Parameters.Add("v_check_person", OleDbType.VarChar, 20).Value = TxtJCR.Text;
        //        MJcomm.Parameters.Add("v_remark", OleDbType.VarChar, 200).Value = TxtBZ.Text;
        //        MJcomm.Parameters.Add("v_arrived_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtDHsl.Text);
        //        try
        //        {
        //            MJcomm.ExecuteNonQuery();
        //            UpdateInStoreQty(JCconn, mjTran);
        //            mjTran.Commit();
        //            Response.Write("<script language=javascript>alert('提交成功！')</script>");
        //        }
        //        catch (Exception ex)
        //        {
        //            mjTran.Rollback();
        //            throw new Exception(ex.Message);
        //        }
        //        finally
        //        {
        //            JCconn.Close();
        //            JCconn.Dispose();
        //            MJcomm.Dispose();
        //        }
        //        //return;
        //    }
        //    else
        //    {
        //        //第一次检查
        //        if (JudgeIsFirstCheck(HdArrivalID.Value))
        //        {
        //            //判断是否填写检查 合格/不合格 数量
        //            if (TxtGdHGzs.Value.ToString() == "0" && TxtGdBHGzs.Value.ToString() == "0")
        //            {
        //                //判断是否填写清点数量,没填写“清点数量” 只操作“检查表”，填写“清点数量”操作“检查表”、“到货表”
        //                if (TxtDHsl.Text.Trim() == "")
        //                {
        //                    //InsertCheckDataInit();
        //                    Misc.Message(this.GetType(), ClientScript, "保存失败，到货数量为必填项。");
        //                    return;
        //                }
        //                else
        //                {
        //                    OleDbCommand ckComm = new OleDbCommand();
        //                    ckComm.Connection = JCconn;
        //                    if (JCconn.State == ConnectionState.Closed) JCconn.Open();
        //                    ckComm.CommandType = CommandType.StoredProcedure;
        //                    OleDbTransaction ckTran = JCconn.BeginTransaction();
        //                    ckComm.Transaction = ckTran;
        //                    ckComm.CommandText = "gen_package_check_api.first_insert_init_";
        //                    ckComm.Parameters.Add("v_arrived_id", OleDbType.VarChar, 20).Value = HdArrivalID.Value;
        //                    ckComm.Parameters.Add("v_check_date", OleDbType.Date).Value = Convert.ToDateTime(TxtJCrq.Text);
        //                    ckComm.Parameters.Add("v_arrived_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtDHsl.Text);
        //                    ckComm.Parameters.Add("v_check_person", OleDbType.VarChar, 20).Value = TxtJCR.Text;
        //                    try
        //                    {
        //                        ckComm.ExecuteNonQuery();
        //                        ckTran.Commit();
        //                        Response.Write("<script language=javascript>alert('提交成功！')</script>");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ckTran.Rollback();
        //                        throw new Exception(ex.Message);
        //                    }
        //                    finally
        //                    {
        //                        JCconn.Close();
        //                        JCconn.Dispose();
        //                        ckComm.Dispose();
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //操作数据库表有：“检查表”、“到货表”、“库存表in_store”
        //                //“到货表” 更新：清点数量、合格、不合格 数量
        //                OleDbCommand comm = new OleDbCommand();
        //                comm.Connection = JCconn;
        //                if (JCconn.State == ConnectionState.Closed) JCconn.Open();
        //                comm.CommandType = CommandType.StoredProcedure;
        //                comm.CommandText = "gen_package_check_api.first_insert_finish_";
        //                OleDbTransaction otran = JCconn.BeginTransaction();
        //                comm.Transaction = otran;
        //                comm.Parameters.Add("v_arrived_id", OleDbType.VarChar, 20).Value = HdArrivalID.Value;
        //                comm.Parameters.Add("v_check_date", OleDbType.Date).Value = Convert.ToDateTime(TxtJCrq.Text);
        //                comm.Parameters.Add("v_ok_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtGdHGzs.Value);
        //                comm.Parameters.Add("v_bad_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtGdBHGzs.Value);
        //                comm.Parameters.Add("v_remark", OleDbType.VarChar, 200).Value = TxtBZ.Text;
        //                comm.Parameters.Add("v_state", OleDbType.VarChar, 20).Value = "finished";
        //                comm.Parameters.Add("v_check_person", OleDbType.VarChar, 20).Value = TxtJCR.Text;
        //                comm.Parameters.Add("v_arrived_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtDHsl.Text);
        //                try
        //                {
        //                    comm.ExecuteNonQuery();
        //                    UpdateInStoreQty(JCconn, otran);
        //                    otran.Commit();
        //                    Response.Write("<script language=javascript>alert('提交成功！')</script>");
        //                }
        //                catch (Exception ex)
        //                {
        //                    otran.Rollback();
        //                    throw new Exception(ex.Message);
        //                }
        //                finally
        //                {
        //                    JCconn.Close();
        //                    JCconn.Dispose();
        //                    comm.Dispose();
        //                }
        //            }
        //        }
        //        else //多次检查//不更新 到货表中的 清点数量 只更新 合格数量、不合格数量(总数)
        //        {
        //            //判断是否填写检查合格数量
        //            if (TxtGdHGzs.Value.ToString() == "0")//没填写合格数量
        //            {
        //                //操作：“检查表”
        //                InsertCheckDataInit();
        //            }
        //            else
        //            {
        //                //操作：“检查表”、“到货表”、“库存表”
        //                //“到货表” 更新：合格、不合格 数量
        //                OleDbCommand comm = new OleDbCommand();
        //                comm.Connection = JCconn;
        //                if (JCconn.State == ConnectionState.Closed) JCconn.Open();
        //                comm.CommandType = CommandType.StoredProcedure;
        //                comm.CommandText = "gen_package_check_api.repeat_insert_finish_";
        //                OleDbTransaction otran = JCconn.BeginTransaction();
        //                comm.Transaction = otran;
        //                comm.Parameters.Add("v_arrived_id", OleDbType.VarChar, 20).Value = HdArrivalID.Value;
        //                comm.Parameters.Add("v_check_date", OleDbType.Date).Value = Convert.ToDateTime(TxtJCrq.Text);
        //                comm.Parameters.Add("v_ok_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtGdHGzs.Value);
        //                comm.Parameters.Add("v_bad_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtGdBHGzs.Value);
        //                comm.Parameters.Add("v_remark", OleDbType.VarChar, 200).Value = TxtBZ.Text;
        //                comm.Parameters.Add("v_state", OleDbType.VarChar, 20).Value = "finished";
        //                comm.Parameters.Add("v_check_person", OleDbType.VarChar, 20).Value = TxtJCR.Text;
        //                try
        //                {
        //                    comm.ExecuteNonQuery();
        //                    UpdateInStoreQty(JCconn, otran);
        //                    otran.Commit();
                            
        //                    Response.Write("<script language=javascript>alert('提交成功！')</script>");
        //                }
        //                catch (Exception ex)
        //                {
        //                    otran.Rollback();
        //                    throw new Exception(ex.Message);
        //                }
        //                finally
        //                {
        //                    JCconn.Close();
        //                    JCconn.Dispose();
        //                    comm.Dispose();
        //                }
        //            }
        //        }
        //    }
        //    QueryPl.Visible = true;
        //    MxPl.Visible = false;
        //    BindGridData();
        //}
        //private void InsertCheckDataInit()
        //{
        //    OleDbConnection JCconn = new OleDbConnection(DBHelper.OleConnectionString);
        //    OleDbCommand ckComm = new OleDbCommand();
        //    ckComm.Connection = JCconn;
        //    if (JCconn.State == ConnectionState.Closed) JCconn.Open();
        //    ckComm.CommandType = CommandType.StoredProcedure;
        //    OleDbTransaction ckTran = JCconn.BeginTransaction();
        //    ckComm.Transaction = ckTran;
        //    ckComm.CommandText = "gen_package_check_api.insert_init_";
        //    ckComm.Parameters.Add("v_arrived_id", OleDbType.VarChar, 20).Value = HdArrivalID.Value;
        //    ckComm.Parameters.Add("v_check_date", OleDbType.Date).Value = Convert.ToDateTime(TxtJCrq.Text);
        //    ckComm.Parameters.Add("v_check_person", OleDbType.VarChar, 20).Value = TxtJCR.Text;

        //    try
        //    {
        //        ckComm.ExecuteNonQuery();
        //        ckTran.Commit();
        //        Response.Write("<script language=javascript>alert('提交成功！')</script>");
        //    }
        //    catch (Exception ex)
        //    {
        //        ckTran.Rollback();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        JCconn.Close();
        //        JCconn.Dispose();
        //        ckComm.Dispose();
        //    }
        //}
        private void PageGoBack()
        {
            QueryPl.Visible = true;
            MxPl.Visible = false;
            
            BindGridData();
        }        

        private void RegCheckResult(CheckType checkType)
        {
            if (checkType == CheckType.FirstCheck)
            {
                GVSecondCheck.Visible = false;
                GVNonCheck.Visible = false;
                GVFirstCheckDataBind();
            }
            if (checkType == CheckType.NonCheck)
            {
                GVFirstCheck.Visible = false;
                GVSecondCheck.Visible = false;
                GVNonCheckDataBind();
            }
            if (checkType == CheckType.SecondCheck)
            {
                GVFirstCheck.Visible = false;
                GVNonCheck.Visible = false;
                GVSecondCheckDataBind();
            }            
        }

        private void GVFirstCheckDataBind()
        {
            GVFirstCheck.Visible = true;           

            string arrivedId = LtDHID.Text;
            DataView dv;
            decimal total = 0;
            
            StringBuilder sql = new StringBuilder("select b.company_id company,b.area,b.location,a.reg_qty,a.location_id from gen_pkg_arrival_l a,jp_wh_loc_v b where a.location_id = b.location_id");
            sql.Append(string.Format(" and arrived_id = '{0}'", arrivedId));
            dv = DBHelper.createGridView(sql.ToString());

            GVFirstCheck.DataSource = dv;
            GVFirstCheck.DataKeyNames = new string[] { "location_id" };
            GVFirstCheck.DataBind();

            for (int i = 0; i < dv.Count; i++)
            {
                total += Decimal.Parse(dv[i]["reg_qty"].ToString());
            }

            LblTotalTitle.Text = "到货合计:";
            LblTotal.Text = total.ToString();
            LblTotalOk.Text = total.ToString();
            LblTotalBad.Text = "0";
            HiddenGVType.Value = CheckType.FirstCheck.ToString();
        }

        private void GVNonCheckDataBind()
        {
            GVNonCheck.Visible = true;
            
            string arrivedId = LtDHID.Text;
            DataView dv;
            decimal total = 0;
            StringBuilder sql = new StringBuilder("select t.location_id,t1.company_id company,t1.area,t1.location,t.reg_qty from gen_pkg_arrival_l t,jp_wh_loc_v t1 where t.location_id = t1.location_id");
            sql.Append(string.Format(" and t.arrived_id = '{0}'",arrivedId));
            dv = DBHelper.createGridView(sql.ToString());
            GVNonCheck.DataSource = dv;
            GVNonCheck.DataKeyNames = new string[] {"location_id" };
            GVNonCheck.DataBind();

            LblTotalTitle.Text = "到货合计:";
            LblTotalOk.Text = "-";
            LblTotalBad.Text = "-";

            for (int i = 0; i < dv.Count; i++)
            {
                total += Decimal.Parse(dv[i]["reg_qty"].ToString());
            }

            LblTotal.Text = total.ToString();
            HiddenGVType.Value = CheckType.NonCheck.ToString();
        }

        private void GVSecondCheckDataBind()
        {
            GVSecondCheck.Visible = true;                      

            string arrivedId = LtDHID.Text;
            DataView dv;
            decimal total = 0;

            StringBuilder sql = new StringBuilder();
            sql.Append("select a.rowid,a.rowversion,b.company_id company,b.area,b.location,a.location_id,part_ok_qty+part_bad_qty+no_check_qty onhand_qty,part_bad_qty checking_qty");
            sql.Append(string.Format(" from gen_package_part_in_store a,jp_wh_loc_v b where a.location_id=b.location_id and a.arrival_id = '{0}' and part_bad_qty > 0",arrivedId));
           
            dv = DBHelper.createGridView(sql.ToString());

            GVSecondCheck.DataSource = dv;
            GVSecondCheck.DataKeyNames = new string[] { "rowid","rowversion" };
            GVSecondCheck.DataBind();

            for (int i = 0; i < dv.Count; i++)
            {
                total += Decimal.Parse(dv[i]["checking_qty"].ToString());
            }

            LblTotalTitle.Text = "待检合计:";
            LblTotal.Text = total.ToString();
            LblTotalOk.Text = total.ToString();
            LblTotalBad.Text = "0";
            HiddenGVType.Value = CheckType.SecondCheck.ToString();  
        }

        private void GVCheckRecDataBind()
        {
            string arrivedId = LtDHID.Text;
            string sql = string.Format("select * from gen_pkg_chk_v where arrived_id='{0}' order by to_number(check_id)", arrivedId);

            GVCheckRec.DataSource = DBHelper.createGridView(sql);
            GVCheckRec.DataBind();
        }

        private void GVCheckRecEmptyDataBind()
        {
            GVCheckRec.DataSource = null;
            GVCheckRec.DataBind();
        }

        protected void GVSecondCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string okQtyCId;
            string badQtyCid;
            string onhandQty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                okQtyCId = e.Row.FindControl("GVTxtOkQty").ClientID;
                badQtyCid = e.Row.FindControl("GVLblBadQty").ClientID;
                onhandQty = DataBinder.Eval(e.Row.DataItem, "checking_qty").ToString();

                ((TextBox)e.Row.FindControl("GVTxtOkQty")).Attributes.Add("onblur",
                   string.Format("SecondCheck_GVTxtOkQty_onblur('{0}','{1}','{2}')",
                   onhandQty,okQtyCId, badQtyCid));

                ((TextBox)e.Row.FindControl("GVTxtOkQty")).Attributes.Add("onkeyup",
                   string.Format("SecondCheck_GVTxtOkQty_onblur('{0}','{1}','{2}')",
                   onhandQty, okQtyCId, badQtyCid));
            }
        }

        protected void GVFirstCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string onhandQtyCId;
            string okQtyCId;
            string badQtyCid;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                onhandQtyCId = e.Row.FindControl("GVTxtOnhandQty").ClientID;
                okQtyCId = e.Row.FindControl("GVTxtOkQty").ClientID;
                badQtyCid = e.Row.FindControl("GVLblBadQty").ClientID;

                ((TextBox)e.Row.FindControl("GVTxtOnhandQty")).Attributes.Add("onblur",
                    string.Format("FirstCheck_GVTxtOnhandQty_onblur('{0}','{1}','{2}')",
                    onhandQtyCId, okQtyCId, badQtyCid));

                ((TextBox)e.Row.FindControl("GVTxtOkQty")).Attributes.Add("onblur",
                    string.Format("FirstCheck_GVTxtOkQty_onblur('{0}','{1}','{2}')",
                    onhandQtyCId, okQtyCId, badQtyCid));
            }
        }

        protected void GVNonCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {  
            if (e.Row.RowType == DataControlRowType.DataRow)
            {  
                ((TextBox)e.Row.FindControl("GVTxtOnhandQty")).Attributes.Add("onblur",
                    "NonCheck_GVTxtOnhandQty_onblur()");
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string checkMark;   //"check":用户打开登记检验数量表格；"no check"：用户不登记检验数量；            
            string checkId;
            string arrivalId;

            decimal totalOk;       //登记合格总是
            decimal totalBad;      //登记不合格总数
            decimal totalArrival;  //登记到货总数（清点总数）

            totalOk = 0;
            totalBad = 0;
            totalArrival = 0;

            checkMark = HiddenCheckMark.Value;
            checkId = HiddenCheckId.Value;
            arrivalId = LtDHID.Text;

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                cmd.CommandType = CommandType.StoredProcedure;
                
                try
                {
                    //checkMark == "check" 用户要填写检验结果
                    if (checkMark == "check")
                    {
                        //操作库存
                        if (!NonCheckInfoValidator()) return;    //校验检验信息是否完整

                        decimal arrivalNum_;                     //gridview每行记录的清点数量
                        decimal okNum_;                          //gridview每行记录的合格数量
                        decimal badNum_;                         //gridview每行记录的不合格数量

                        string strArrivalNum_;                   
                        string strOkNum_;
                        string strBadNum_;

                        //免检 只填写清点数量
                        if (HiddenGVType.Value == CheckType.NonCheck.ToString())
                        {
                            foreach (GridViewRow grv in GVNonCheck.Rows)
                            {
                                strArrivalNum_ = ((TextBox)grv.FindControl("GVTxtOnhandQty")).Text;

                                if (!Misc.CheckNumber(strArrivalNum_))
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误,填写的清点数量为非数值。");
                                    return;
                                }
                                if (Misc.DBStrToNumber(strArrivalNum_) < 0)
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误，填写的清点数量为负值。");
                                    return;
                                }
                            }
                            cmd.Parameters.Clear();
                            cmd.CommandText = "gen_package_part_in_store_api.noncheck_in";
                            cmd.Parameters.Add("v_package_no", OleDbType.VarChar);
                            cmd.Parameters.Add("v_part_no", OleDbType.VarChar);
                            cmd.Parameters.Add("v_arrival_id", OleDbType.VarChar);
                            cmd.Parameters.Add("v_location_id", OleDbType.VarChar);
                            cmd.Parameters.Add("v_nocheck_qty", OleDbType.Decimal);
                            foreach (GridViewRow grv in GVNonCheck.Rows)
                            {
                                arrivalNum_ = Misc.DBStrToNumber(((TextBox)grv.FindControl("GVTxtOnhandQty")).Text);
                                if (arrivalNum_ == 0) { continue; }                                
                                totalArrival += arrivalNum_;

                                //cmd.Parameters.Clear();
                                //cmd.CommandText = "gen_package_part_in_store_api.noncheck_in";
                                cmd.Parameters["v_package_no"].Value = LtDBbh.Text;
                                cmd.Parameters["v_part_no"].Value = LtXJbh.Text;
                                cmd.Parameters["v_arrival_id"].Value = LtDHID.Text;
                                cmd.Parameters["v_location_id"].Value = GVNonCheck.DataKeys[grv.RowIndex].Value.ToString();
                                cmd.Parameters["v_nocheck_qty"].Value = arrivalNum_;

                                cmd.ExecuteNonQuery();
                            }
                            if (totalArrival > 0)//totalArrival>0用户填写了检验结果,本次检验完成,到货也全部检验完成
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "gen_package_check_api.nocheck";
                                cmd.Parameters.Add("v_arrived_id", OleDbType.VarChar).Value = arrivalId;
                                cmd.Parameters.Add("v_check_date", OleDbType.VarChar).Value = TxtJCrq.Text;
                                cmd.Parameters.Add("v_check_person", OleDbType.VarChar).Value = DdlJcr.SelectedValue == "0" ? ((Authentication.LOGININFO)Session["USERINFO"]).UserID : DdlJcr.SelectedValue;
                                cmd.Parameters.Add("v_remark", OleDbType.VarChar).Value = TxtBZ.Text;
                                cmd.Parameters.Add("v_arrived_qty", OleDbType.Decimal).Value = totalArrival;

                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                Misc.Message(this.GetType(), ClientScript, "错误,请输入清点数量后再做提交。");
                                tr.Rollback();
                                return;
                            }
                        }

                        //第一次检验 填写清点数量、合格数量
                        if (HiddenGVType.Value == CheckType.FirstCheck.ToString())
                        {
                            foreach (GridViewRow gvr in GVFirstCheck.Rows)
                            {
                                strArrivalNum_ = ((TextBox)gvr.FindControl("GVTxtOnhandQty")).Text;
                                strOkNum_ = ((TextBox)gvr.FindControl("GVTxtOkQty")).Text;

                                if (!Misc.CheckNumber(strArrivalNum_))
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误,填写的清点数量为非数值。");
                                    return;
                                }
                                if (!Misc.CheckNumber(strOkNum_))
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误,填写的合格数量为非数值。");
                                    return;
                                }
                                if (Misc.DBStrToNumber(strArrivalNum_) < 0)
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误,填写的清点数量不能为负。");
                                    return;
                                }                                
                                if (Misc.DBStrToNumber(strOkNum_) > Misc.DBStrToNumber(strArrivalNum_) || Misc.DBStrToNumber(strOkNum_) < 0)
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误，合格数量不能为负，不能大于清点数量。");
                                    return;
                                }
                            }

                            cmd.Parameters.Clear();
                            cmd.CommandText = "gen_package_part_in_store_api.check_in_first";
                            cmd.Parameters.Add("v_package_no", OleDbType.VarChar);
                            cmd.Parameters.Add("v_part_no", OleDbType.VarChar);
                            cmd.Parameters.Add("v_arrival_id", OleDbType.VarChar);
                            cmd.Parameters.Add("v_location_id", OleDbType.VarChar);
                            cmd.Parameters.Add("v_ok_qty", OleDbType.Decimal);
                            cmd.Parameters.Add("v_bad_qty", OleDbType.Decimal);

                            foreach (GridViewRow gvr in GVFirstCheck.Rows)
                            {

                                arrivalNum_ = Misc.DBStrToNumber(((TextBox)gvr.FindControl("GVTxtOnhandQty")).Text);
                                if (arrivalNum_ == 0) { continue; }
                                okNum_ = Misc.DBStrToNumber(((TextBox)gvr.FindControl("GVTxtOkQty")).Text);
                                badNum_ = arrivalNum_ - okNum_;                                

                                totalArrival += arrivalNum_;
                                totalOk += okNum_;
                                

                                //cmd.Parameters.Clear();
                                //cmd.CommandText = "gen_package_part_in_store_api.check_in_first";
                                cmd.Parameters["v_package_no"].Value = LtDBbh.Text;
                                cmd.Parameters["v_part_no"].Value = LtXJbh.Text;
                                cmd.Parameters["v_arrival_id"].Value = LtDHID.Text;
                                cmd.Parameters["v_location_id"].Value = GVFirstCheck.DataKeys[gvr.RowIndex].Value.ToString();
                                cmd.Parameters["v_ok_qty"].Value = okNum_;
                                cmd.Parameters["v_bad_qty"].Value = badNum_;

                                cmd.ExecuteNonQuery();
                            }

                            totalBad = totalArrival - totalOk;

                            if (totalArrival > 0)//用户填写检验结果 
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "gen_package_check_api.first_check_finish";
                                cmd.Parameters.Add("v_check_id", OleDbType.VarChar).Value = checkId;
                                cmd.Parameters.Add("v_arrived_id", OleDbType.VarChar).Value = arrivalId;
                                cmd.Parameters.Add("v_check_date", OleDbType.VarChar).Value = TxtJCrq.Text;
                                cmd.Parameters.Add("v_ok_qty", OleDbType.Decimal).Value = totalOk;
                                cmd.Parameters.Add("v_bad_qty", OleDbType.Decimal).Value = totalBad;
                                cmd.Parameters.Add("v_remark", OleDbType.VarChar).Value = TxtBZ.Text;
                                cmd.Parameters.Add("v_check_person", OleDbType.VarChar).Value = DdlJcr.SelectedValue;
                                cmd.Parameters.Add("v_arrived_qty", OleDbType.Decimal).Value = totalArrival;

                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                Misc.Message(this.GetType(), ClientScript, "错误,请输入清点数量后再做提交。");
                                tr.Rollback();
                                return;
                            }
                        }

                        //再次检验 填写合格数量
                        if (HiddenGVType.Value == CheckType.SecondCheck.ToString())
                        {
                            foreach (GridViewRow gvr in GVSecondCheck.Rows)
                            {
                                strOkNum_ = ((TextBox)gvr.FindControl("GVTxtOkQty")).Text;

                                if (!Misc.CheckNumber(strOkNum_))
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误,填写的合格数量为非数值。");
                                    return;
                                }
                                if (Misc.DBStrToNumber(strOkNum_) < 0)
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误,填写的合格数量不能为负值。");
                                    return;
                                }
                                if (Misc.DBStrToNumber(strOkNum_) > Misc.DBStrToNumber(gvr.Cells[4].Text))
                                {
                                    Misc.Message(this.GetType(), ClientScript, "错误,填写的合格数量不能大于待检数量。");
                                    return;
                                }
                            }
                            cmd.Parameters.Clear();
                            cmd.CommandText = "gen_package_part_in_store_api.check_in_again";
                            cmd.Parameters.Add("v_ok_qty", OleDbType.Decimal);
                            cmd.Parameters.Add("v_objid", OleDbType.VarChar);
                            cmd.Parameters.Add("v_rowversion", OleDbType.VarChar);
                            foreach (GridViewRow gvr in GVSecondCheck.Rows)
                            {
                                arrivalNum_ = Misc.DBStrToNumber(gvr.Cells[4].Text);//arrivalNum_ 为待检数量                                
                                okNum_ = Misc.DBStrToNumber(((TextBox)gvr.FindControl("GVTxtOkQty")).Text);
                                if (okNum_ == 0) { continue; }                                
                                badNum_ = arrivalNum_ - okNum_;
                                
                                totalOk += okNum_;
                                totalBad += badNum_;

                                //cmd.Parameters.Clear();
                                
                                cmd.Parameters["v_ok_qty"].Value = okNum_;
                                cmd.Parameters["v_objid"].Value = GVSecondCheck.DataKeys[gvr.RowIndex].Values["rowid"].ToString();
                                cmd.Parameters["v_rowversion"].Value = GVSecondCheck.DataKeys[gvr.RowIndex].Values["rowversion"].ToString();

                                cmd.ExecuteNonQuery();
                            }

                            if (totalOk > 0)
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "gen_package_check_api.repeat_check_finish";
                                cmd.Parameters.Add("v_check_id", OleDbType.VarChar).Value = checkId;
                                cmd.Parameters.Add("v_arrived_id", OleDbType.VarChar).Value = arrivalId;
                                cmd.Parameters.Add("v_check_date", OleDbType.VarChar).Value = TxtJCrq.Text;
                                cmd.Parameters.Add("v_ok_qty", OleDbType.Decimal).Value = totalOk;
                                cmd.Parameters.Add("v_bad_qty", OleDbType.Decimal).Value = totalBad;
                                cmd.Parameters.Add("v_remark", OleDbType.VarChar).Value = TxtBZ.Text;
                                cmd.Parameters.Add("v_check_person", OleDbType.VarChar).Value = DdlJcr.SelectedValue;

                                cmd.ExecuteNonQuery();

                            }
                            else
                            {
                                Misc.Message(this.GetType(), ClientScript, "错误,请输入合格数量后再做提交。");
                                tr.Rollback();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (TxtArrQty.Text.Trim() == "")
                        {
                            Misc.Message(this.GetType(), ClientScript, "错误，请输入清点数量。");
                            return;
                        }
                        if (!Misc.CheckNumber(TxtArrQty.Text))
                        {
                            Misc.Message(this.GetType(), ClientScript, "错误，清点数量为非数值。");
                            return;
                        }
                        totalArrival = Misc.DBStrToNumber(TxtArrQty.Text);
                        cmd.Parameters.Clear();
                        cmd.CommandText = "gen_package_check_api.check_init";
                        cmd.Parameters.Add("v_check_id",OleDbType.VarChar).Value = checkId  ;
                        cmd.Parameters.Add("v_arrived_id",OleDbType.VarChar).Value = arrivalId;
                        cmd.Parameters.Add("v_check_date",OleDbType.VarChar).Value = TxtJCrq.Text;
                        cmd.Parameters.Add("v_arrived_qty",OleDbType.Decimal).Value = totalArrival;
                        cmd.Parameters.Add("v_check_person", OleDbType.VarChar).Value = DdlJcr.SelectedValue;
                        cmd.Parameters.Add("v_remark", OleDbType.VarChar).Value = TxtBZ.Text;

                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                    Misc.Message(this.GetType(), ClientScript, "检验数据提交成功。");
                    QueryPl.Visible = true;
                    MxPl.Visible = false;
                    BindGridData();

                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
                        return;
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

        private bool NonCheckInfoValidator()
        {
            
            if (DdlJcr.SelectedValue == "0"&&!ChkBoxMj.Checked)
            {
                Misc.Message(this.GetType(), ClientScript, "检验人为空，提交失败。");
                return false;
            }

            if (TxtJCrq.Text.Trim() == "")
            {
                Misc.Message(this.GetType(), ClientScript, "检验日期为空，提交失败。");
                return false;
            }
            return true;
        }

        protected void CKGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CKGrid.PageIndex = e.NewPageIndex;
            BindGridData();
        }

        protected void ChkBoxMj_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBoxMj.Checked)
            {
                RegCheckResult(CheckType.NonCheck);
                //if (DdlJcr.SelectedValue == "0")
                //{
                //    DdlJcr.SelectedIndex = DdlJcr.Items.IndexOf(DdlJcr.Items.FindByValue(((Authentication.LOGININFO)Session["USERINFO"]).UserID));
                //}
                if (TxtJCrq.Text == "")
                {
                    TxtJCrq.Text = string.Format("{0:yyyy-MM-dd}",DateTime.Now);
                }
            }
            else
            {
                RegCheckResult(CheckType.FirstCheck);
            }
        }

        protected void LnkBtnRegCheckResult_Click(object sender, EventArgs e)
        {
            LnkBtnRegCheckResult.Visible = false;
            LnkBtnCancelRegResult.Visible = true;

            ChkBoxMj.Visible = true;
            PnlCheckResult.Visible = true;

            HiddenCheckMark.Value = "check";
        }

        protected void LnkBtnCancelRegResult_Click(object sender, EventArgs e)
        {
            LnkBtnRegCheckResult.Visible = true;
            LnkBtnCancelRegResult.Visible = false;

            ChkBoxMj.Visible = false;
            PnlCheckResult.Visible = false;

            HiddenCheckMark.Value = "nocheck";
        }
    }
}
