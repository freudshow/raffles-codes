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
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Text;

namespace Package
{
    public partial class pkg_part : System.Web.UI.Page
    {
        private string m_perimission;
        private BaseInfoLoader baseInfoLoader = new BaseInfoLoader();

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
                        DdlUnitBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_PART] == '1') return true;
            return false;
        }        

        private void DdlUnitBind()
        {
            baseInfoLoader.PartUnitDropDownListLoad(DdlUnit, false, true);
        }

        protected void ButtSave_Click(object sender, EventArgs e)
        {
            //string objid;
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand("gen_part_package_item_api.New_", conn);                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Clear();

                cmd.Parameters.Add("v_Package_no", OleDbType.VarChar, 20).Value = TxtPackageNo.Text;
                cmd.Parameters.Add("v_Part_name", OleDbType.VarChar, 500).Value = TxtPartName.Text;
                cmd.Parameters.Add("v_Part_name_e", OleDbType.VarChar, 500).Value = TxtPartNameE.Text;
                cmd.Parameters.Add("v_Part_spec", OleDbType.VarChar, 100).Value = TxtPartSpec.Text;
                cmd.Parameters.Add("v_UNIT", OleDbType.VarChar, 20).Value =DdlUnit.SelectedValue;
                cmd.Parameters.Add("v_Dec_no", OleDbType.VarChar, 100).Value = TxtDecNo.Text;
                cmd.Parameters.Add("v_Contract_no", OleDbType.VarChar, 100).Value = TxtContractNo.Text;
                cmd.Parameters.Add("v_po_no", OleDbType.VarChar).Value = TxtPO.Text;
                cmd.Parameters.Add("v_pay_flag", OleDbType.VarChar).Value = ChkNoPay.Checked ? "1" : "0";
                cmd.Parameters.Add("v_objid", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    //v_out = cmd.Parameters["v_out"].Value.ToString();
                   
                    tr.Commit();
                    PartRowIdAdd(cmd.Parameters["v_objid"].Value.ToString());
                    HiddenResultSender.Value = "save";
                    GVDataBind();
                }
                catch (Exception ex)
                {
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.Message(this.GetType(),ClientScript,Misc.GetDBCustomException(ex));
                        return;
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
        }          
           
        protected void GVBaleItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string packageNo = GVBaleItem.DataKeys[e.RowIndex].Values["package_no"].ToString();
            string partNo = GVBaleItem.DataKeys[e.RowIndex].Values["part_no"].ToString();

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbCommand cmd = new OleDbCommand("gen_part_package_item_api.delete_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd.Parameters.Add("v_package_no", OleDbType.VarChar).Value = packageNo;
                cmd.Parameters.Add("v_part_no", OleDbType.VarChar).Value = partNo;
                cmd.Parameters.Add("v_objid", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    PartRowIdRemove(cmd.Parameters["v_objid"].Value.ToString());
                    GVDataBind();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.Message(this.GetType(),ClientScript,Misc.GetDBCustomException(ex));
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

        protected void GVBaleItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string id = GVBaleItem.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Write("<script type='text/javascript'>");
            Response.Write(string.Format("\nwindow.open('pkg_part_mod.aspx?id={0}','mod1','height=800px, width=600px, toolbar =no, menubar=no, scrollbars=no, resizable=yes, location=yes, status=no')", id));
            Response.Write("\n</script>");
 
        }

        protected void ButtQuerry_Click(object sender, EventArgs e)
        {
            HiddenResultSender.Value = "query";
            GVDataBind();
            if (GVBaleItem.Rows.Count == 0)
            {
                Misc.Message(this.GetType(), ClientScript, "未找到符合条件的数据。");
            }
        }        

        private void GVDataBind()
        {
            string resultSender = HiddenResultSender.Value;
            string[] partRowIds = HiddenNewId.Value.Split('^');
            StringBuilder sql = new StringBuilder("select * from gen_part_package_item_v where 1=1 ");

            if (resultSender == "query")
            {

                if (TxtPackageNo.Text.Trim() != "")
                {
                    sql.Append(string.Format(" and package_no='{0}'", TxtPackageNo.Text.Trim()));
                }
                if (TxtContractNo.Text.Trim() != "")
                {
                    sql.Append(string.Format(" and contract_no like '{0}'", TxtContractNo.Text.Trim()));
                }
                if (TxtDecNo.Text.Trim() != "")
                {
                    sql.Append(string.Format(" and dec_no like '{0}'", TxtDecNo.Text.Trim()));
                }
                if (TxtPartName.Text.Trim() != "")
                {
                    sql.Append(string.Format(" and part_name like '{0}'", TxtPartName.Text));
                }
                if (TxtPartNameE.Text.Trim() != "")
                {
                    sql.Append(string.Format(" and part_name_e like '{0}'", TxtPartNameE.Text));
                }
                if (DdlUnit.SelectedValue != "0")
                {
                    sql.Append(string.Format(" and unit= '{0}'", DdlUnit.SelectedValue));
                }
                if (TxtPartSpec.Text.Trim() != "")
                {
                    sql.Append(string.Format(" and part_spec like '{0}'", TxtPartSpec.Text.Trim()));
                }

            }
            else
            {
                if (resultSender == "save")
                {
                    if (partRowIds.Length > 0)
                    {
                        sql.Append(" and rowid in (");
                        for (int i = 0; i < partRowIds.Length; i++)
                        {
                            if (i == partRowIds.Length - 1)
                            {
                                sql.Append(string.Format("'{0}')", partRowIds[i]));
                            }
                            else
                            {
                                sql.Append(string.Format("'{0}',", partRowIds[i]));
                            }
                        }
                    }
                }
            }

            sql.Append(" order by package_no,part_no");
            GVBaleItem.DataSource = DBHelper.createGridView(sql.ToString());
            GVBaleItem.DataKeyNames = new string[] { "package_no", "part_no" };
            GVBaleItem.DataBind();
        }

        protected void GVBaleItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVBaleItem.PageIndex = e.NewPageIndex;
            GVDataBind();           
        }

        private string  PartRowIdAdd(string rowid)
        {
            string _rowid = HiddenNewId.Value;
            HiddenNewId.Value = _rowid + "^" + rowid;
            return HiddenNewId.Value;
        }

        private string PartRowIdRemove(string rowid)
        {
            string _rowid = HiddenNewId.Value;
            int star_ = _rowid.IndexOf(rowid);
            if (star_ == -1)
            {
                return _rowid;
            }
            _rowid.Remove(star_, rowid.Length);
            HiddenNewId.Value = _rowid;
            return HiddenNewId.Value;
        }
    }
}

