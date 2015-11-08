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
    public partial class pkg_loc_move : System.Web.UI.Page
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
                        //ddl_area_bind();
                        DdlProjectBind();
                        //PnlTopInit();
                        PnlTop.Visible = true;
                        Panel1.Visible = false;
                       
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_MOV] == '1') return true;
            return false;
        }

        public void ddl_area_bind()
        {
            ddl_area.DataSource = DBHelper.createDDLView("select area_id,company_id||'--'||area company_area from jp_wh_area where state='1'");
            ddl_area.DataValueField = "area_id";
            ddl_area.DataTextField = "company_area";
            ddl_area.DataBind();
        }

        protected void DdlProjectBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);
        }

        //public void ddl_location_bind()
        //{
        //    ddl_location.DataSource = DBHelper.createDDLView("select location_id,location from jp_wh_location where state='1'");
        //    ddl_location.DataValueField = "location_id";
        //    ddl_location.DataTextField = "location";
        //    ddl_location.DataBind();
        //}

        private string getSql()
        {
            StringBuilder sql = new StringBuilder("select * from gen_pkg_part_in_store_v where 1=1");
            if (TxtArrDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and arrived_date ='{0}'", TxtArrDate.Text.Trim()));
            }
            if (TxtPackageNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_no='{0}'", TxtPackageNo.Text.Trim()));
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_name like '{0}'", TxtPkgName.Text.Trim()));
            }
            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id='{0}'", DdlProject.SelectedValue));
            }
            if (TxtPO.Text.Trim() != "")
            {
                sql.Append(string.Format(" and po_no= '{0}'", TxtPO.Text.Trim()));
            }            
            if (TxtDec.Text.Trim() != "")
            {
                sql.Append(string.Format(" and dec_no like '{0}'", TxtDec.Text.Trim()));
            }
            if (TxtPart.Text.Trim() != "")
            {
                sql.Append(string.Format(" and (part_name_e like '{0}' or part_name like '{0}')", TxtPart.Text.Trim()));
            }
            if (TxtSpec.Text.Trim() != "")
            {
                sql.Append(string.Format(" and spec like '{0}'", TxtSpec.Text.Trim()));
            }
            return sql.ToString();            
        }

        private void bindGV()
        {
            string sql = this.getSql();
            DataView dv = DBHelper.createGridView(sql);
            GVData.DataSource = dv;
            GVData.DataKeyNames = new string[] { "package_no", "part_no", "arrival_id", "location_id" };
            GVData.DataBind();

            if (dv.Count > 0)
            {
                divBox.Visible = true;
            }
            else
            {
                divBox.Visible = false;
            }
        }

        protected void ddl_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddl_location.DataSource = DBHelper.createDDLView("select location_id,location from jp_wh_location where state='1' and area_id='" + ddl_area.SelectedValue + "' ");
            TxtLocationInput.Text = "";
            //ddl_location.DataValueField = "location_id";
            //ddl_location.DataTextField = "location";
            //ddl_location.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            string package = LblPackageNo.Text;
            string part = LblPartNo.Text;
            string arrival = LblArrivedId.Text;
            string locationS = LblLocationId.Text;

            decimal okQtyS = Misc.DBStrToNumber(LblOkQty.Text);
            decimal badQtyS = Misc.DBStrToNumber(LblBadQty.Text);
            decimal nocheckQtyS = Misc.DBStrToNumber(LblNocheckQty.Text);

            decimal okQtyT = Misc.DBStrToNumber(TxtOkQtyT.Text);
            decimal badQtyT = Misc.DBStrToNumber(TxtBadQtyT.Text);
            decimal nocheckQtyT = Misc.DBStrToNumber(TxtNocheckQtyT.Text);            

            //string locationD = ddl_location.SelectedValue;
            string locationD;



            locationD = CheckLocationExist();

            if (locationD == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('货位不存在。')", true);
                return;
            }

            if (ddl_area.SelectedValue == "0" || locationD == "")
            {
                Misc.Message(this.GetType(), ClientScript, "保存失败，目标库位信息不完整！");               
                return;
            }
            if (TxtOkQtyT.Text.Trim() == "" && TxtBadQtyT.Text.Trim() == "" && TxtNocheckQtyT.Text.Trim() == "")
            {
                Misc.Message(this.GetType(), ClientScript, "保存失败，无移库数量！");                
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                conn.Open();

                if (locationD == locationS)
                {
                    Misc.Message(this.GetType(), ClientScript, "保存失败，目标库位与源库位相同。");
                    return;
                }

                if (okQtyT <= okQtyS && badQtyT <= badQtyS && nocheckQtyT <= nocheckQtyS)
                {
                    OleDbCommand cmd = new OleDbCommand("gen_package_part_in_store_api.trans_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    OleDbTransaction odt;
                    odt = conn.BeginTransaction();
                    cmd.Transaction = odt;

                    cmd.Parameters.Add("v_package_no", OleDbType.VarChar, 20).Value = package;
                    cmd.Parameters.Add("v_part_no", OleDbType.VarChar, 20).Value = part;
                    cmd.Parameters.Add("v_arrival_id", OleDbType.VarChar, 20).Value = arrival;
                    cmd.Parameters.Add("v_locations_id", OleDbType.VarChar, 20).Value = locationS;
                    cmd.Parameters.Add("v_locationd_id", OleDbType.VarChar, 20).Value = locationD;
                    cmd.Parameters.Add("v_ok", OleDbType.Numeric).Value = okQtyT;
                    cmd.Parameters.Add("v_bad", OleDbType.Numeric).Value = badQtyT;
                    cmd.Parameters.Add("v_nocheck", OleDbType.Numeric).Value = nocheckQtyT;
                    cmd.Parameters.Add("v_user", OleDbType.VarChar).Value = ((Authentication.LOGININFO)Session["USERINFO"]).UserID;
                    cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        if (cmd.Parameters["v_msg"].Value.ToString() == "1")
                        {
                            odt.Commit();
                            Misc.Message(this.GetType(), ClientScript, "移位成功！");
                            PnlTop.Visible = true;
                            Panel1.Visible = false;
                            bindGV();
                        }
                        else
                        {
                            odt.Rollback();
                            Misc.Message(this.GetType(), ClientScript, cmd.Parameters["v_msg"].Value.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        odt.Rollback();
                        string error = ex.Message.Substring(0, ex.Message.IndexOf("."));
                        Misc.Message(this.GetType(), ClientScript, "保存失败，" + error.Substring(0, 30));
                        return;
                    }
                }
                else
                {
                    Misc.Message(this.GetType(), ClientScript, "保存失败，移位数量不能大于原有库位数量！");
                    return;
                }
            }

        }

        //protected void ddl_location_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string package = LblPackageNo.Text;
        //    string part = LblPartNo.Text;
        //    string arrival = LblArrivedId.Text;
        //    string location = ddl_location.SelectedValue;
        //    string sql = "select * from gen_package_part_in_store where package_no='" + package + "' and part_no='" + part + "' and arrival_id='" + arrival + "' and location_id='" + location + "'";
        //    DataView dv = DBHelper.createGridView(sql);
        //    if (dv.Count > 0)
        //    {
        //        LblOkQtyD.Text = dv[0]["part_ok_qty"].ToString() == "" ? "0" : dv[0]["part_ok_qty"].ToString();
        //        LblBadQtyD.Text = dv[0]["part_bad_qty"].ToString() == "" ? "0" : dv[0]["part_bad_qty"].ToString();
        //        LblNochkQtyD.Text = dv[0]["no_check_qty"].ToString() == "" ? "0" : dv[0]["no_check_qty"].ToString();
        //    }
        //    else
        //    {

        //        LblOkQtyD.Text = "0";
        //        LblBadQtyD.Text = "0";
        //        LblNochkQtyD.Text = "0";
        //        return;
        //    }
        //}

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }

        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            bindGV();
        }

        
        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            bindGV();
        }       

        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "trans")
            {
                int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
                string pkgNo = GVData.DataKeys[rowindex].Values["package_no"].ToString();
                string partNo = GVData.DataKeys[rowindex].Values["part_no"].ToString();
                string arrivedId = GVData.DataKeys[rowindex].Values["arrival_id"].ToString();
                string locationId = GVData.DataKeys[rowindex].Values["location_id"].ToString();

                DataView dv = DBHelper.createDataset(string.Format("select * from gen_pkg_part_in_store_v where package_no='{0}' and part_no='{1}' and arrival_id='{2}' and location_id='{3}'",
                    pkgNo, partNo, arrivedId, locationId)).Tables[0].DefaultView;
                LblPackageNo.Text = dv[0]["package_no"].ToString();
                LblPackageName.Text = dv[0]["package_name"].ToString();
                LblPartNo.Text = dv[0]["part_no"].ToString();
                LblPartName.Text = dv[0]["part_name_e"].ToString();
                LblArea.Text = dv[0]["area"].ToString();
                LblArrivedDate.Text = dv[0]["arrived_date"].ToString();
                LblArrivedId.Text = dv[0]["arrival_id"].ToString();
                LblBadQty.Text = dv[0]["part_bad_qty"].ToString();
                LblCompany.Text = dv[0]["company_id"].ToString();
                LblLocation.Text = dv[0]["location"].ToString();
                LblLocationId.Text = dv[0]["location_id"].ToString();
                LblNocheckQty.Text = dv[0]["no_check_qty"].ToString();
                LblOkQty.Text = dv[0]["part_ok_qty"].ToString();
                LblPartSpec.Text = dv[0]["spec"].ToString();
                LblUnit.Text = dv[0]["unit"].ToString();

                PnlBottomInit();               
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            PnlTop.Visible = true;
            Panel1.Visible = false;
        }

        private void PnlTopInit()
        {
            PnlTop.Visible = true;
            Panel1.Visible = false;

            bindGV();
        }

        private void PnlBottomInit()
        {
            Panel1.Visible = true;
            PnlTop.Visible = false;

            ddl_area_bind();
            //ViewState["ddl_location"] = null;
            //ddl_location.Items.Clear();
            //ddl_location.DataBind();

            LblOkQtyD.Text = "";
            LblBadQtyD.Text = "";
            LblNochkQtyD.Text = "";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string package = LblPackageNo.Text;
            string part = LblPartNo.Text;
            string arrival = LblArrivedId.Text;
            string location = HiddenLocId.Value;
            string sql = "select * from gen_package_part_in_store where package_no='" + package + "' and part_no='" + part + "' and arrival_id='" + arrival + "' and location_id='" + location + "'";
            DataView dv = DBHelper.createGridView(sql);
            if (dv.Count > 0)
            {
                LblOkQtyD.Text = dv[0]["part_ok_qty"].ToString() == "" ? "0" : dv[0]["part_ok_qty"].ToString();
                LblBadQtyD.Text = dv[0]["part_bad_qty"].ToString() == "" ? "0" : dv[0]["part_bad_qty"].ToString();
                LblNochkQtyD.Text = dv[0]["no_check_qty"].ToString() == "" ? "0" : dv[0]["no_check_qty"].ToString();
            }
            else
            {

                LblOkQtyD.Text = "0";
                LblBadQtyD.Text = "0";
                LblNochkQtyD.Text = "0";
                return;
            }
        }

        private string CheckLocationExist()
        {
            object temp = DBHelper.getObject(string.Format("select location_id from jp_wh_loc_v t where area_id='{0}' and location = '{1}'", ddl_area.SelectedValue, TxtLocationInput.Text));
            if (temp == null)
            {
                return string.Empty;
            }

            return temp.ToString();
        }
    }
}
