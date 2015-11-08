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
using System.Data.OleDb;

namespace jzpl.UI.Package
{
    public partial class pkg_part_receive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GVDataEmptyDataBind();
                DdlCompanyDataBind();
                DdlAreaDataBind();
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

        private void DdlCompanyDataBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.CompanyDropDrownListLoad(DdlCompany, false, true, false, "YRO");
        }

        private void DdlAreaDataBind()
        {
            string company = DdlCompany.SelectedValue;

            DDLArea.DataSource = DBHelper.createGridView(string.Format("select area_id,area from jp_wh_area where state='1' and company_id='{0}'", company));
            DDLArea.DataValueField = "area_id";
            DDLArea.DataTextField = "area";
            DDLArea.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            decimal _partOkQty;
            decimal _partBadQty;
            decimal _partNocheckQty;
            
            string locationId;

            _partOkQty = Misc.DBStrToNumber(TxtPartOk.Text.Trim());
            _partBadQty = Misc.DBStrToNumber(TxtPartBad.Text.Trim());
            _partNocheckQty=Misc.DBStrToNumber(TxtPartNocheck.Text.Trim());

            if (_partOkQty == 0 && _partBadQty == 0 && _partNocheckQty == 0)
            {
                Misc.Message(this.GetType(), ClientScript, "请先输入接收数量。");
                return;
            }

            

            locationId = CheckLocationExist();

            if (locationId == string.Empty)
            {
                Misc.Message(this.GetType(), ClientScript, "错误，库位不存在。");
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {

                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "gen_package_part_in_store_api.receive_in";

                try
                {
                    
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("v_package_no", OleDbType.VarChar).Value = TxtPkgNo.Text;
                    cmd.Parameters.Add("v_part_no", OleDbType.VarChar).Value = TxtPartNo.Text;
                    cmd.Parameters.Add("v_location_id", OleDbType.VarChar).Value = locationId;
                    cmd.Parameters.Add("v_ok_qty", OleDbType.Numeric).Value = _partOkQty;
                    cmd.Parameters.Add("v_bad_qty", OleDbType.Numeric).Value = _partBadQty;
                    cmd.Parameters.Add("v_nocheck_qty", OleDbType.Numeric).Value =_partNocheckQty;
                    cmd.Parameters.Add("v_arrival_id", OleDbType.VarChar).Value = "*";
                    cmd.Parameters.Add("v_user_id", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;

                    cmd.ExecuteNonQuery();

                    tr.Commit();
                    Misc.Message(this.GetType(), ClientScript, "保存成功。");

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

        protected void DdlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlAreaDataBind();
        }

        private string CheckLocationExist()
        {
            object temp = DBHelper.getObject(string.Format("select location_id from jp_wh_loc_v t where area_id='{0}' and location = '{1}'", DDLArea.SelectedValue, TxtLocationInput.Text));
            if (temp == null)
            {
                return string.Empty;
            }

            return temp.ToString();
        }
    }
}
