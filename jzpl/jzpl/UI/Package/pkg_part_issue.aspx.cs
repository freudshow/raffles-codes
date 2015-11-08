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

namespace jzpl.UI.Package
{
    public partial class pkg_part_issue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GVDataEmptyDataBind();
            }
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataDataBind();
        }

        private void GVLocationInfoDataBind(string partNo)
        {

            StringBuilder sql = new StringBuilder("select * from gen_pkg_part_in_store_v where 1=1");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            TextBox txtOk;
            TextBox txtBad;
            TextBox txtNocheck;

           

            Label lblArrivalId;

            bool mark = false;

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {

                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "gen_package_part_in_store_api.issue_out";

                try
                {
                    foreach (GridViewRow gvr in GVData.Rows)
                    {
                        txtOk = (TextBox)gvr.FindControl("GVTxtOkQty");
                        txtBad = (TextBox)gvr.FindControl("GVTxtBadQty");
                        txtNocheck = (TextBox)gvr.FindControl("GVTxtNocheckQty");

                        if (txtOk.Text.Trim() != string.Empty && Convert.ToDecimal(txtOk.Text) < 0)
                        {
                            Misc.Message(this.GetType(), ClientScript, "保存失败，填入了无效数值。");
                            return;
                        }

                        if (txtBad.Text.Trim() != string.Empty && Convert.ToDecimal(txtBad.Text) < 0)
                        {
                            Misc.Message(this.GetType(), ClientScript, "保存失败，填入了无效数值。");
                            return;
                        }

                        if (txtNocheck.Text.Trim() != string.Empty && Convert.ToDecimal(txtNocheck.Text) < 0)
                        {
                            Misc.Message(this.GetType(), ClientScript, "保存失败，填入了无效数值。");
                            return;
                        }

                        lblArrivalId = (Label)gvr.FindControl("GVLblArrivalId");

                        if (txtOk.Text.Trim() == "" && txtBad.Text.Trim() == "" && txtNocheck.Text.Trim() == "")
                        {
                            continue;
                        }
                        else
                        {
                            mark = true;
                            cmd.Parameters.Clear();

                            cmd.Parameters.Add("v_package_no", OleDbType.VarChar).Value = TxtPkgNo.Text;
                            cmd.Parameters.Add("v_part_no", OleDbType.VarChar).Value = TxtPartNo.Text;
                            cmd.Parameters.Add("v_location_id", OleDbType.VarChar).Value = GVData.DataKeys[gvr.RowIndex].Value.ToString();
                            cmd.Parameters.Add("v_ok_qty", OleDbType.Numeric).Value = Misc.DBStrToNumber(txtOk.Text);
                            cmd.Parameters.Add("v_bad_qty", OleDbType.Numeric).Value = Misc.DBStrToNumber(txtBad.Text);
                            cmd.Parameters.Add("v_nocheck_qty", OleDbType.Numeric).Value = Misc.DBStrToNumber(txtNocheck.Text);
                            cmd.Parameters.Add("v_arrival_id", OleDbType.VarChar).Value = lblArrivalId == null ? "*" : lblArrivalId.Text;
                            cmd.Parameters.Add("v_user_id", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;

                            cmd.ExecuteNonQuery();
                        }
                    }
                    if (mark)
                    {
                        tr.Commit();
                        Misc.Message(this.GetType(), ClientScript, "保存成功。");
                    }
                    else
                    {
                        Misc.Message(this.GetType(), ClientScript, "无接收数量。");
                    }
                }
                catch
                {
                    tr.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            GVDataDataBind();
        }


        private void GVDataDataBind()
        {
            if (TxtPartNo.Text.Trim() == string.Empty)
            {
                Misc.Message(this.GetType(), ClientScript, "请选择零件。");
                return;
            }
            string sql = "select * from gen_pkg_part_in_store_v where part_no='" + TxtPartNo.Text + "'";
            GVData.DataSource = DBHelper.createGridView(sql);
            GVData.DataKeyNames = new string[] { "location_id" };
            GVData.DataBind();
        }

        private void GVDataEmptyDataBind()
        {
            GVData.DataSource = null;
            GVData.DataBind();
        }
    }
}
