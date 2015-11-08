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
using System.Text;

namespace Package
{
    public partial class jp_pkg_add : System.Web.UI.Page
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
                        DdlProdSiteBind();
                        DdlReceiptDeptBind();
                        DdlReceiptPersonBind();
                        
                        ChkPS.Checked = true;
                        prompt.Visible = true;
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_ADD] == '1') return true;
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
            string sql="select id per_id,person from jp_receipt_person order by last_time desc,use_times desc" ;
            DdlReceiptPerson.DataSource = DBHelper.createDDLView(sql);
            DdlReceiptPerson.DataTextField = "person";
            DdlReceiptPerson.DataValueField = "per_id";
            DdlReceiptPerson.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand("jp_pkg_requisition_api.new_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Clear();

                cmd.Parameters.Add("v_requisition_id", OleDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("v_package_no", OleDbType.VarChar).Value = TxtPkgNo.Text.Trim();
                cmd.Parameters.Add("v_part_no", OleDbType.VarChar).Value = TxtPartNo.Text.Trim();
                cmd.Parameters.Add("v_require_qty", OleDbType.Numeric).Value = Convert.ToDecimal(TxtReqNum.Text.Trim());
                cmd.Parameters.Add("v_project_block", OleDbType.VarChar).Value = TxtBlock.Text;
                cmd.Parameters.Add("v_project_system", OleDbType.VarChar).Value = TxtSystem.Text;
                cmd.Parameters.Add("v_place", OleDbType.VarChar).Value = DdlProdSite.SelectedValue;
                cmd.Parameters.Add("v_receiver", OleDbType.VarChar).Value = DdlReceiptPerson.SelectedItem.Text;
                cmd.Parameters.Add("v_receiver_ic", OleDbType.VarChar).Value = TxtIC.Text;
                cmd.Parameters.Add("v_receive_date", OleDbType.VarChar).Value = TxtDate.Text;
                cmd.Parameters.Add("v_receiver_contact", OleDbType.VarChar).Value = TxtContact.Text;
                cmd.Parameters.Add("v_receipt_dept", OleDbType.VarChar).Value = DdlReceiptDept.SelectedValue;
                cmd.Parameters.Add("v_crane", OleDbType.VarChar).Value = ChkDz.Checked == true ? "1" : "0";
                cmd.Parameters.Add("v_recorder", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                cmd.Parameters.Add("v_req_group", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).GroupID;
                cmd.Parameters.Add("v_psflag", OleDbType.VarChar).Value = ChkPS.Checked ? "1" : "0";
                cmd.Parameters.Add("v_work_content", OleDbType.VarChar).Value = TxtWorkContent.Text;
                cmd.Parameters.Add("v_msg", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["v_msg"].Value.ToString() == "1")
                {
                    ReqIDSessionHandler("ADD", cmd.Parameters["v_requisition_id"].Value.ToString());
                    tr.Commit();
                }
                else
                {
                    Misc.Message(this.GetType(), ClientScript, cmd.Parameters["v_msg"].Value.ToString());
                    return;
                }
            }
            GVDataDataBind();
            PartDataBind();
        }

        private void GVDataDataBind()
        {
            StringBuilder sqlstr = new StringBuilder("select * from jp_pkg_requisition_v where rowstate = 'init'");

            if (Session["reqid"] != null)
            {
                ArrayList arr_ = (ArrayList)Session["reqid"];
                if (arr_.Count != 0)
                {
                    sqlstr.Append(" and requisition_id in (");
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
                    GVData.DataSource = DBHelper.createGridView(sqlstr.ToString());
                    GVData.DataKeyNames = new string[] { "requisition_id" };
                    GVData.DataBind();
                    return;
                }
            }
            else
            {
                GVData.DataSource = null;
                GVData.DataBind();
            }
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

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                CheckBox chk = e.Row.FindControl("ChkCrane") as CheckBox;
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["crance"].ToString() == "1")
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

        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[]  temp;
            string objid;
            string rowversion;
            int row_index;
            string req_id;            

            if (e.CommandName == "ReqLineDelete")
            {
                temp = e.CommandArgument.ToString().Split('^');
                if (temp.Length != 2)
                {
                    Misc.Message(this.GetType(), ClientScript, "É¾³ýÊ§°Ü£¬´íÎó²ÎÊý¡£");
                    return;
                }

                objid = temp[0];
                rowversion = temp[1];
                row_index =((GridViewRow)(((ImageButton)e.CommandSource).Parent.Parent)).RowIndex;
                req_id = GVData.DataKeys[row_index].Value.ToString();

                using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand("jp_pkg_requisition_api.delete_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    OleDbTransaction tr = conn.BeginTransaction();
                    cmd.Transaction = tr;

                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = objid;
                    cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        tr.Commit();
                        ReqIDSessionHandler("REMOVE", req_id);
                        GVDataDataBind();
                        PartDataBind();

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
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            PartDataBind();
            GVDataDataBind();
        }

        private void PartDataBind()
        {
            GenPkgPart part_ = new GenPkgPart(TxtPkgNo.Text, TxtPartNo.Text);
            TxtProject.Text = part_.ProjectId;
            TxtPO.Text = part_.PoNo;
            TxtPkgName.Text = part_.PackageName;
            TxtPartName.Text = part_.PartName;
            TxtPartNameE.Text = part_.PartNameE;
            TxtPartSpec.Text = part_.PartSpec;
            TxtPartUnit.Text = part_.Unit;
            TxtOnHandQty.Text = part_.OnhandQty.ToString();
            TxtAvaiQty.Text = part_.AvaiQty.ToString();
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
                    DdlReceiptPerson.SelectedIndex =DdlReceiptPerson.Items.IndexOf(DdlReceiptPerson.Items.FindByValue(receiptPersonId));
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
            DataView dv = DBHelper.createDataset(string.Format("select id, person, contact, company_id, ic, state, use_times, last_time from jp_receipt_person where id='{0}'",id)).Tables[0].DefaultView;
            TxtIC.Text= dv[0]["ic"].ToString();
            TxtContact.Text = dv[0]["contact"].ToString();
        }

        protected void DdlReceiptPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            string receiptPersonId = DdlReceiptPerson.SelectedValue;
            SetReceiptPersonInfo(receiptPersonId);
        }
    }
}
