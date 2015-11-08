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

namespace Package
{
    public partial class jp_pkg_mod : System.Web.UI.Page
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
                        string req_id_ = Misc.GetHtmlRequestValue(Request, "id");
                        objid.Value = Misc.GetHtmlRequestValue(Request, "objid");
                        rowversion.Value = Misc.GetHtmlRequestValue(Request, "ver");

                        

                        PkgRequisition m_req = new PkgRequisition(req_id_);

                        if (m_req.RequisitionId == "")
                        {
                            //this.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "<script>alert('申请未找到！');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                            Misc.Message(this.GetType(), ClientScript, "错误，申请未找到。");
                            Misc.RegisterClientScript(this.GetType(), "opener_shx", ClientScript, "<script type='text/javascript'>window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                            return;
                        }
                        else
                        {
                            if (m_req.RowState != "init")
                            {
                                //this.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "<script>alert('申请状态已改变，不能修改！');window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                                Misc.Message(this.GetType(), ClientScript, "申请状态已改变，不能修改。");
                                Misc.RegisterClientScript(this.GetType(), "opener_shx", ClientScript, "<script>window.opener.__doPostBack('BtnQuery','');window.close();</script>");
                            }
                            else
                            {
                                GenPkgPart m_pkg_part = new GenPkgPart(m_req.PackageNo, m_req.PartNo);

                                DdlProdSiteBind();
                                DdlReceiptDeptBind();
                                DdlReceiptPersonBind();

                                requisition_id.InnerText = m_req.RequisitionId;
                                project_id.InnerText = m_req.ProjectId;
                                project_desc.InnerText = m_pkg_part.ProjectName;
                                package_no.InnerText = m_req.PackageNo;
                                package_name.InnerText = m_req.PackageName;
                                part_no.InnerText = m_req.PartNo;
                                part_name.InnerText = m_req.PartNameE;
                                onhand_qty.InnerText = m_pkg_part.OnhandQty.ToString();
                                avai_qty.InnerText = Convert.ToString(m_pkg_part.AvaiQty + m_req.RequireQty);
                                reserved_qty.InnerText = Convert.ToString(m_pkg_part.ReservedQty - m_req.RequireQty);

                                DdlProdSite.SelectedIndex = DdlProdSite.Items.IndexOf(DdlProdSite.Items.FindByValue(m_req.PlaceId));
                                DdlReceiptDept.SelectedIndex = DdlReceiptDept.Items.IndexOf(DdlReceiptDept.Items.FindByValue(m_req.ReceiptDept));
                                TxtDate.Text = m_req.ReceiptDateStr;
                                DdlReceiptPerson.SelectedIndex = DdlReceiptPerson.Items.IndexOf(DdlReceiptPerson.Items.FindByText(m_req.Receiver));
                                TxtIC.Text = m_req.ReceiverIc;
                                TxtContact.Text = m_req.ReceiverContract;
                                TxtBlock.Text = m_req.ProjectBlock;
                                TxtSystem.Text = m_req.ProjectSystem;
                                ChkDz.Checked = m_req.Crance == "1" ? true : false;
                                ChkPS.Checked = m_req.Psflag == "1" ? true : false;
                                TxtRequireQty.Text = m_req.RequireQty.ToString();
                                TxtWorkContent.Text = m_req.WorkContent;
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_ADD] == '1' || m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_QUERY] == '1') return true;
            return false;
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

        protected void DdlReceiptPersonBind()
        {
            string sql = "select id per_id,person from jp_receipt_person order by last_time desc,use_times desc";
            DdlReceiptPerson.DataSource = DBHelper.createDDLView(sql);
            DdlReceiptPerson.DataTextField = "person";
            DdlReceiptPerson.DataValueField = "per_id";
            DdlReceiptPerson.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand("jp_pkg_requisition_api.Modify_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Transaction = tr;

                cmd.Parameters.Add("v_require_qty", OleDbType.Decimal).Value = Convert.ToDecimal(TxtRequireQty.Text);
                cmd.Parameters.Add("project_block", OleDbType.VarChar).Value = TxtBlock.Text;
                cmd.Parameters.Add("v_project_system", OleDbType.VarChar).Value = TxtSystem.Text;
                cmd.Parameters.Add("v_place", OleDbType.VarChar).Value = DdlProdSite.SelectedValue;
                cmd.Parameters.Add("v_receiver", OleDbType.VarChar).Value = DdlReceiptPerson.SelectedItem.Text;
                cmd.Parameters.Add("v_receiver_ic", OleDbType.VarChar).Value = TxtIC.Text;
                cmd.Parameters.Add("v_receive_date", OleDbType.VarChar).Value = TxtDate.Text;
                cmd.Parameters.Add("v_receiver_contact", OleDbType.VarChar).Value = TxtContact.Text;
                cmd.Parameters.Add("v_receipt_dept", OleDbType.VarChar).Value = DdlReceiptDept.SelectedValue;
                cmd.Parameters.Add("v_crance", OleDbType.VarChar).Value = ChkDz.Checked == true ? "1" : "0";
                cmd.Parameters.Add("v_recorder", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                cmd.Parameters.Add("v_req_group", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).GroupID;
                cmd.Parameters.Add("v_psflag", OleDbType.VarChar).Value = ChkPS.Checked ? "1" : "0";
                cmd.Parameters.Add("v_work_content", OleDbType.VarChar).Value = TxtWorkContent.Text;
                cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = objid.Value;
                cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion.Value;
                cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    Misc.RegisterClientScript(this.GetType(), "refresh", ClientScript, "<script type='text/javascript'>window.dialogArguments.refresh();window.close();</script>");
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw ex;
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