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
using System.Data.OleDb;
namespace jzpl
{
    public partial class wzxqjh_mod : System.Web.UI.Page
    {
        private string m_perimission;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                        req_id.Value = Misc.GetHtmlRequestValue(Request, "id");
                        objid.Value = Misc.GetHtmlRequestValue(Request, "objid");
                        rowversion.Value = Misc.GetHtmlRequestValue(Request, "ver");

                        DemandRequisition m_req = new DemandRequisition(Misc.GetHtmlRequestValue(Request, "id"));
                        if (m_req.REQUISITION_ID == "")
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "<script>alert('申请未找到！');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                        }
                        else
                        {
                            if (m_req.ROWSTATE != "init")
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "<script>alert('申请状态已改变，不能修改！');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                            }
                            else
                            {
                                DdlProdSiteBind();
                                DdlReceiptDeptBind();
                                DdlReceiptPersonBind();
                                contract.InnerText = m_req.CONTRACT;
                                part_no.InnerText = m_req.PART_NO;
                                part_desc.InnerText = m_req.PART_DESCRIPTION;
                                project_id.InnerText = m_req.PROJECT_ID;
                                project_desc.InnerText = m_req.PROJECT_DESCRIPTION;
                                mtr_seq_no.InnerText = m_req.MATR_SEQ_NO;
                                mtr_line_no.InnerText = m_req.MATR_SEQ_LINE_NO;
                                ration_qty.InnerText = DBHelper.getObject(string.Format("select ration_qty from IFSAPP.PROJ_PROCU_RATION@erp_prod where misc_tab_ref_no='{0}' and material_req_seq_no='{1}'", m_req.MATR_SEQ_NO, m_req.MATR_SEQ_LINE_NO)).ToString();
                                issued_qty.InnerText = DBHelper.getObject(string.Format("select nvl(issued_qty,0) from IFSAPP.PROJ_PROCU_RATION@erp_prod where misc_tab_ref_no='{0}' and material_req_seq_no='{1}'", m_req.MATR_SEQ_NO, m_req.MATR_SEQ_LINE_NO)).ToString();
                                required_qty.InnerText = (Convert.ToDecimal(DBHelper.getObject(string.Format("select jp_requisition_api.get_required_num_of_mtr('{0}','{1}') from dual", m_req.MATR_SEQ_NO, m_req.MATR_SEQ_LINE_NO))) - Convert.ToDecimal(m_req.REQUIRE_QTY)).ToString();

                                DdlProdSite.SelectedIndex = DdlProdSite.Items.IndexOf(DdlProdSite.Items.FindByValue(m_req.PLACE));
                                DDL_QH.SelectedIndex = DDL_QH.Items.IndexOf(DDL_QH.Items.FindByValue(m_req.LACK_TYPE));
                                DdlReceiptDept.SelectedIndex = DdlReceiptDept.Items.IndexOf(DdlReceiptDept.Items.FindByValue(m_req.RECEIPT_DEPT));
                                TxtDate.Text = m_req.RECEIVE_DATE;
                                DdlReceiptPerson.SelectedIndex  = DdlReceiptPerson.Items.IndexOf(DdlReceiptPerson.Items.FindByText(m_req.RECEIVER));
                                TxtIC.Text = m_req.RECEIVER_IC;
                                TxtContact.Text = m_req.RECEIVER_CONTACT;
                                TxtBlock.Text = m_req.PROJECT_BLOCK;
                                TxtSystem.Text = m_req.PROJECT_SYSTEM;
                                ChkDz.Checked = m_req.CRANE == "1" ? true : false;
                                TxtRequireQty.Text = m_req.REQUIRE_QTY;
                                TxtWorkContent.Text = m_req.WORK_CONTENT;
                            }
                        }
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PART_JP_ADD] == '1' || m_perimission[(int)Authentication.PERMDEFINE.PART_JP_QUERY] == '1') return true;
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand("jp_demand_api.Modify_demand", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Transaction = tr;

                cmd.Parameters.Add("v_require_qty", OleDbType.Decimal).Value = Convert.ToDecimal(TxtRequireQty.Text);
                cmd.Parameters.Add("project_block", OleDbType.VarChar).Value = TxtBlock.Text;
                cmd.Parameters.Add("v_project_system", OleDbType.VarChar).Value = TxtSystem.Text;
                cmd.Parameters.Add("v_place", OleDbType.VarChar).Value = DdlProdSite.SelectedValue;
                cmd.Parameters.Add("v_place_description", OleDbType.VarChar).Value = DdlProdSite.SelectedItem.Text.ToString();
                cmd.Parameters.Add("v_receiver", OleDbType.VarChar).Value = DdlReceiptPerson.SelectedItem.Text;
                cmd.Parameters.Add("v_receiver_ic", OleDbType.VarChar).Value = TxtIC.Text;
                cmd.Parameters.Add("v_receive_date", OleDbType.VarChar).Value = TxtDate.Text;
                cmd.Parameters.Add("v_receiver_contact", OleDbType.VarChar).Value = TxtContact.Text;
                cmd.Parameters.Add("v_receipt_dept", OleDbType.VarChar).Value = DdlReceiptDept.SelectedValue;
                cmd.Parameters.Add("v_crance", OleDbType.VarChar).Value = ChkDz.Checked == true ? "1" : "0";
                cmd.Parameters.Add("v_recorder", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                cmd.Parameters.Add("v_req_group", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).GroupID;
                //cmd.Parameters.Add("v_psflag", OleDbType.VarChar).Value = ChkPS.Checked ? "1" : "0";
                cmd.Parameters.Add("v_work_content", OleDbType.VarChar).Value = TxtWorkContent.Text;
                cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = objid.Value;
                cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion.Value;
                cmd.Parameters.Add("v_lack_type", OleDbType.VarChar).Value = DDL_QH.SelectedValue;
                cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    Misc.RegisterClientScript(this.GetType(), "refresh", ClientScript, "<script>alert('数据更新成功！');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                }
                catch (Exception ex)
                {
                    tr.Rollback();

                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.RegisterClientScript(this.GetType(), "refresh", ClientScript, "<script>alert('" + Misc.GetDBCustomException(ex)+ "');window.opener.__doPostBack('BtnQuery','');window.close();</script>");               
                        //Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
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
            //using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            //{
            //    OleDbCommand cmd = new OleDbCommand();
            //    cmd.CommandText = string.Format("update jp_requisition set require_qty = to_number('{0}')," +
            //        "project_block = '{1}'," +
            //        "project_system = '{2}'," +
            //        "place = '{3}'," +
            //        "place_description = JP_RECEIPT_PLACE_API.get_name('{3}')," +
            //        "receiver = '{4}'," +
            //        "receiver_ic = '{5}'," +
            //        "receive_date = to_date('{6}','yyyy-mm-dd')," +
            //        "receiver_contact = '{7}'," +
            //        "crane = '{8}'," +
            //        "recorder = '{9}'," +
            //        "receipt_dept='{10}'," +
            //        "req_group='{11}'," +
            //        "record_time = sysdate " +
            //        " where rowstate='init' and requisition_id ='{12}'",
            //        TxtRequireQty.Text,
            //        TxtBlock.Text,
            //        TxtSystem.Text,
            //        DdlProdSite.SelectedValue,
            //        DdlReceiptPerson.SelectedItem.Text,
            //        TxtIC.Text,
            //        TxtDate.Text,
            //        TxtContact.Text,
            //        ChkDz.Checked == true ? "1" : "0",
            //        ((Authentication.LOGININFO)Session["USERINFO"]).UserID,
            //        DdlReceiptDept.SelectedValue,
            //        ((Authentication.LOGININFO)Session["USERINFO"]).GroupID,
            //        req_id.Value);
            //    cmd.Connection = conn;
            //    cmd.CommandType = CommandType.Text;
            //    if (conn.State != ConnectionState.Open) conn.Open();
            //    try
            //    {
            //        cmd.ExecuteNonQuery();
            //        this.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "<script>alert('数据更新成功！');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        conn.Close();
            //    }
            //}
        }

        protected void mBtnSave_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand("jp_demand_api.Modify_demand", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Transaction = tr;

                cmd.Parameters.Add("v_require_qty", OleDbType.Decimal).Value = Convert.ToDecimal(TxtRequireQty.Text);
                cmd.Parameters.Add("project_block", OleDbType.VarChar).Value = TxtBlock.Text;
                cmd.Parameters.Add("v_project_system", OleDbType.VarChar).Value = TxtSystem.Text;
                cmd.Parameters.Add("v_place", OleDbType.VarChar).Value = DdlProdSite.SelectedValue;
                cmd.Parameters.Add("v_place_description", OleDbType.VarChar).Value = DdlProdSite.SelectedItem.Text.ToString();
                cmd.Parameters.Add("v_receiver", OleDbType.VarChar).Value = DdlReceiptPerson.SelectedItem.Text;
                cmd.Parameters.Add("v_receiver_ic", OleDbType.VarChar).Value = TxtIC.Text;
                cmd.Parameters.Add("v_receive_date", OleDbType.VarChar).Value = TxtDate.Text;
                cmd.Parameters.Add("v_receiver_contact", OleDbType.VarChar).Value = TxtContact.Text;
                cmd.Parameters.Add("v_receipt_dept", OleDbType.VarChar).Value = DdlReceiptDept.SelectedValue;
                cmd.Parameters.Add("v_crance", OleDbType.VarChar).Value = ChkDz.Checked == true ? "1" : "0";
                cmd.Parameters.Add("v_recorder", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                cmd.Parameters.Add("v_req_group", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).GroupID;
                //cmd.Parameters.Add("v_psflag", OleDbType.VarChar).Value = ChkPS.Checked ? "1" : "0";
                cmd.Parameters.Add("v_work_content", OleDbType.VarChar).Value = TxtWorkContent.Text;
                cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = objid.Value;
                cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion.Value;
                cmd.Parameters.Add("v_lack_type", OleDbType.VarChar).Value = DDL_QH.SelectedValue;
                cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    Misc.RegisterClientScript(this.GetType(), "refresh", ClientScript, "<script>alert('数据更新成功！');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                }
                catch (Exception ex)
                {
                    tr.Rollback();

                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.RegisterClientScript(this.GetType(), "refresh", ClientScript, "<script>alert('" + Misc.GetDBCustomException(ex) + "');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                        //Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
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
