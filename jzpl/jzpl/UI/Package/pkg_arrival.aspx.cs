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
    public partial class pkg_arrival : System.Web.UI.Page
    {
        private string m_perimission;        

        private const string VS_LOCATION_TB = "locationtb";
        //VeiwState中到货位置

        private const string SS_ARRID = "arrivalid";

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
                        DdlCompanyDataBind();
                        DdlAreaDataBind();                        
                        DdlPartUnitDataBind();
                        GVOKEmptyDataBind();
                        GVSubmitEmptyDataBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_ARR] == '1') return true;
            return false;
        }

        private void DdlCompanyDataBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.CompanyDropDrownListLoad(DdlCompany, false, true, false, "YRO");
        }

        private void DdlAreaDataBind()
        {
            string company = DdlCompany.SelectedValue;

            DDLArea.DataSource = DBHelper.createGridView(string.Format("select area_id,area from jp_wh_area where state='1' and company_id='{0}'",company));
            DDLArea.DataValueField = "area_id";
            DDLArea.DataTextField = "area";
            DDLArea.DataBind();
        } 

        private void DdlPartUnitDataBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.PartUnitDropDownListLoad(DdlPartUnit, false, true);
        }

        protected void BtnAddRows_Click(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            string locationId;

            locationId = CheckLocationExist();

            if (locationId == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "alertScript", "alert('货位不存在。')", true);
                return;
            }

            if (ViewState[VS_LOCATION_TB] == null)
            {
                DataColumn col1 = new DataColumn("location_id");
                DataColumn col2 = new DataColumn("company_id");
                DataColumn col3 = new DataColumn("area");
                DataColumn col4 = new DataColumn("location");
                DataColumn col5 = new DataColumn("amount");
                DT.Columns.Add(col1);
                DT.Columns.Add(col2);
                DT.Columns.Add(col3);
                DT.Columns.Add(col4);
                DT.Columns.Add(col5);

                LblTotal.Text = "0"; ;
            }
            else
            {
                DT = (DataTable)ViewState[VS_LOCATION_TB];                
            }

            DataRow[] Rows = DT.Select("location_id='" + locationId + "'");

            if (Rows.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "alertScript", "SetGVBoxHeight('gvbox','GVSubmitData');alert('此货位已经添加。');", true);
                return;
            }           

            DataRow NewRow = DT.NewRow();

            NewRow["location_id"] = locationId;
            NewRow["company_id"] = DdlCompany.SelectedValue;
            NewRow["area"] = DDLArea.SelectedItem.Text;
            NewRow["location"] = TxtLocationInput.Text;
            NewRow["amount"] = TxtAmount.Text;            

            DT.Rows.Add(NewRow);

            LblTotal.Text = Convert.ToString(Convert.ToDecimal(LblTotal.Text) + Convert.ToDecimal(TxtAmount.Text));
            
            ViewState[VS_LOCATION_TB] = DT;
            OKGrid.DataSource = DT;
            OKGrid.DataKeyNames = new string[] { "location_id" };
            OKGrid.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myscript", "SetGVBoxHeight('gvbox','GVSubmitData');", true);

        }

        protected void OKGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DataTable DT = (DataTable)ViewState[VS_LOCATION_TB];
            LblTotal.Text = Convert.ToString(decimal.Parse(LblTotal.Text) - decimal.Parse(DT.Rows[e.NewEditIndex]["amount"].ToString()));
            DT.Rows.RemoveAt(e.NewEditIndex);

            ViewState[VS_LOCATION_TB] = DT;            
            OKGrid.DataSource = DT;
            OKGrid.DataKeyNames = new string[] { "location_id" };
            OKGrid.DataBind();
        }

        ////存储到货位置
        //private void insertArrivalLocation(OleDbCommand comm,string ArrivalID)
        //{
        //    comm.Parameters.Clear();
        //    DataTable DT = (DataTable)ViewState["LocationTB"];
            
        //    comm.CommandType = CommandType.StoredProcedure;
        //    comm.CommandText = "gen_package_arrival_api.insert_package_arrival_l_";            
            
        //    for (int i = 0; i < DT.Rows.Count; i++)
        //    {
        //        comm.Parameters.Clear();
        //        comm.Parameters.Add("v_arrived_id", OleDbType.VarChar, 20).Value = ArrivalID;
        //        comm.Parameters.Add("v_location_id", OleDbType.VarChar, 20).Value = DT.Rows[i]["location_id"].ToString();
        //        comm.Parameters.Add("v_reg_qty", OleDbType.Numeric).Value = decimal.Parse(DT.Rows[i]["location_amount"].ToString());
        //        try
        //        {
        //            comm.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }            
        //}
        //private void insert_ArrivalData()
        //{
        //    OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
        //    OleDbCommand comm = new OleDbCommand();
        //    comm.Connection = conn;
        //    if (conn.State == ConnectionState.Closed) conn.Open();
        //    OleDbTransaction otran = conn.BeginTransaction();
        //    comm.Transaction = otran;
        //    string partNo = TxtXJbh.Value;
        //    if (partNo=="")//判断为输入小件信息，添加小件信息到数据库
        //    {
        //        partNo = InputXJInfo(comm);
        //    }
        //    comm.Parameters.Clear();
        //    comm.CommandType = CommandType.StoredProcedure;
        //    comm.CommandText = "gen_package_arrival_api.insert_package_arrival_";
            
            
        //    comm.Parameters.Add("v_arrived_id", OleDbType.VarChar, 20).Direction = ParameterDirection.Output;           
        //    comm.Parameters.Add("v_package_no", OleDbType.VarChar, 20).Value = TxtDBbh.Value;
        //    comm.Parameters.Add("v_part_no", OleDbType.VarChar, 20).Value = partNo;
        //    comm.Parameters.Add("v_req_qty", OleDbType.Numeric).Value = Convert.ToDecimal(LblTotal.Text);
        //    comm.Parameters.Add("v_arrived_date", OleDbType.Date).Value = Convert.ToDateTime(TxtDHrq.Text);
        //    comm.Parameters.Add("v_rowversion", OleDbType.VarChar, 20).Value = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    try
        //    {
        //        comm.ExecuteNonQuery();
        //        string arrivedId = comm.Parameters["v_arrived_id"].Value.ToString();
        //        insertArrivalLocation(comm,arrivedId);                
        //        otran.Commit();
        //        Misc.Message(this.GetType(),ClientScript,"数据提交成功！");
        //    }
        //    catch (Exception ex)
        //    {
        //        otran.Rollback();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //        conn.Dispose();
        //        comm.Dispose();
        //    }
        //}
        ////当小心信息为输入时，先数据库中添加小件信息
        //private string InputXJInfo(OleDbCommand comm)
        //{   
        //    comm.CommandType = CommandType.StoredProcedure;
        //    comm.CommandText = "gen_part_package_item_api.insert__";

        //    comm.Parameters.Add("v_Package_no", OleDbType.VarChar, 20).Value = TxtDBbh.Value;
        //    comm.Parameters.Add("v_Part_name", OleDbType.VarChar, 500).Value = TxtXJmccn.Text;
        //    comm.Parameters.Add("v_Part_name_e", OleDbType.VarChar, 500).Value = TxtXJmcen.Text;
        //    comm.Parameters.Add("v_Part_spec", OleDbType.VarChar, 100).Value = TxtXJgg.Text;
        //    comm.Parameters.Add("v_unit", OleDbType.VarChar, 20).Value = DdlPartUnit.SelectedValue;
        //    comm.Parameters.Add("v_Dec_no", OleDbType.VarChar, 100).Value = TxtXJgdh.Text;
        //    comm.Parameters.Add("v_Contract_no", OleDbType.VarChar, 100).Value = TxtXJhth.Text;
        //    comm.Parameters.Add("v_part_no", OleDbType.VarChar, 100);
        //    comm.Parameters["v_part_no"].Direction = ParameterDirection.Output;
        //    try
        //    {
        //        comm.ExecuteNonQuery();
        //        return comm.Parameters["v_part_no"].Value.ToString();
        //    }
        //    catch (Exception ex)
        //    {                
        //        throw ex ;
        //    }   
        //}
        //protected void BtnSubmit_Click(object sender, EventArgs e)
        //{
        //    insert_ArrivalData();
        //    DataTable DT = (DataTable)ViewState["LocationTB"];
        //    DT.Rows.Clear();
        //    OKGrid.DataSource = DT;
        //    OKGrid.DataBind();
        //    ViewState["dhsl"] = 0;
        //    //TxtDHsl.Text = "";
        //    TxtDBbh.Value = "";
        //    TxtXJbh.Value = "";
        //    //TxtArea.Value = "";
        //    //TxtLocation.Value = "";
        //    TxtAmount.Text = "";
        //    TxtXJmccn.Text = "";
        //    TxtXJmcen.Text = "";
        //    TxtXJgg.Text = "";
        //    DdlPartUnit.SelectedValue="0";
        //}

        private void GVSubmitDataDataBind()
        {
            ArrayList arr_ = (ArrayList)Session[SS_ARRID];
            if (arr_.Count > 0)
            {
                StringBuilder sql = new StringBuilder("select * from gen_pkg_arr_v where check_mark='init' and arrived_id in ( ");

                for (int i = 0; i < arr_.Count; i++)
                {
                    if (i == arr_.Count - 1)
                    {
                        sql.Append("'" + arr_[i].ToString() + "')");
                    }
                    else
                    {
                        sql.Append("'" + arr_[i].ToString() + "',");
                    }
                }

                GVSubmitData.DataSource = DBHelper.createGridView(sql.ToString());
                GVSubmitData.DataKeyNames = new string[] { "arrived_id" };
                GVSubmitData.DataBind();
            }
            else
            {
                GVSubmitData.DataSource = null;
                GVSubmitData.DataBind();
            }
        }

        protected void ArrivalIDSessionHandler(string mode, string ArrivalID)
        {
            ArrayList arr_id;
            if (Session[SS_ARRID] == null)
            {                 
                Session[SS_ARRID] = new ArrayList();
            }
            arr_id = (ArrayList)Session[SS_ARRID];
            switch (mode)
            {
                case "ADD":
                    arr_id.Add(ArrivalID);
                    break;
                case "REMOVE":
                    arr_id.Remove(ArrivalID);
                    break;
                default:
                    break;
            }
        }
       
        private string CheckLocationExist()
        {
            object temp  = DBHelper.getObject(string.Format("select location_id from jp_wh_loc_v t where area_id='{0}' and location = '{1}'", DDLArea.SelectedValue, TxtLocationInput.Text));
            if (temp == null) 
            {
                return string.Empty; 
            }
            
            return temp.ToString();
        }

        protected void DdlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlAreaDataBind();
        }

        private void GVSubmitEmptyDataBind()
        {
            GVSubmitData.DataSource = null;
            GVSubmitData.DataBind();
        }

        private void GVOKEmptyDataBind()
        {
            LblTotal.Text = "0";
            OKGrid.DataSource = null;
            OKGrid.DataBind();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {            
            string partNo = TxtXJbh.Value;
            string arrivalid;

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    if (partNo == string.Empty)
                    {
                        //写小件表，返回partNO
                        cmd.CommandText = "gen_part_package_item_api.insert__";
                        cmd.Parameters.Clear();

                        cmd.Parameters.Add("v_Package_no", OleDbType.VarChar).Value = TxtDBbh.Value;
                        cmd.Parameters.Add("v_Part_name", OleDbType.VarChar).Value = TxtXJmccn.Text;
                        cmd.Parameters.Add("v_Part_name_e", OleDbType.VarChar).Value = TxtXJmcen.Text;
                        cmd.Parameters.Add("v_Part_spec", OleDbType.VarChar).Value = TxtXJgg.Text;
                        cmd.Parameters.Add("v_unit", OleDbType.VarChar).Value = DdlPartUnit.SelectedValue;
                        cmd.Parameters.Add("v_Dec_no", OleDbType.VarChar).Value = TxtXJgdh.Text;
                        cmd.Parameters.Add("v_Contract_no", OleDbType.VarChar).Value = TxtXJhth.Text;
                        cmd.Parameters.Add("v_po", OleDbType.VarChar).Value = TxtPO.Text;    
                        cmd.Parameters.Add("v_pay_flag", OleDbType.VarChar).Value = ChkPayFlag.Checked ? "1" : "0";
                        cmd.Parameters.Add("v_part_no", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;

                        

                        cmd.ExecuteNonQuery();

                        partNo = cmd.Parameters["v_part_no"].Value.ToString();

                    }

                    //写到货表，返回arrival_id
                    cmd.CommandText = "gen_package_arrival_api.insert_package_arrival_";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("v_arrived_id", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("v_package_no", OleDbType.VarChar).Value = TxtDBbh.Value;
                    cmd.Parameters.Add("v_part_no", OleDbType.VarChar).Value = partNo;
                    cmd.Parameters.Add("v_req_qty", OleDbType.Decimal).Value = Convert.ToDecimal(LblTotal.Text);
                    cmd.Parameters.Add("v_arrived_date", OleDbType.VarChar).Value = TxtDHrq.Text;
                    cmd.Parameters.Add("v_ext1", OleDbType.VarChar).Value = TxtExt1.Text;

                    cmd.ExecuteNonQuery();

                    arrivalid = cmd.Parameters["v_arrived_id"].Value.ToString();
                    //写到货位置
                    cmd.CommandText = "gen_package_arrival_api.insert_package_arrival_l_";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("v_arrived_id", OleDbType.VarChar).Value = arrivalid;
                    cmd.Parameters.Add("v_location_id", OleDbType.VarChar);
                    cmd.Parameters.Add("v_reg_qty", OleDbType.Decimal);

                    foreach (GridViewRow gvr in OKGrid.Rows)
                    {
                        cmd.Parameters["v_location_id"].Value = OKGrid.DataKeys[gvr.RowIndex].Value;
                        cmd.Parameters["v_reg_qty"].Value = Convert.ToDecimal(gvr.Cells[4].Text);
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                    ArrivalIDSessionHandler("ADD", arrivalid);
                    GVSubmitDataDataBind();
                    GVOKEmptyDataBind();
                    ViewState[VS_LOCATION_TB] = null;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myscript", "SetGVBoxHeight('gvbox','GVSubmitData');EnterAgain();", true);
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myscript", string.Format("alert('{0}')",Misc.GetDBCustomException(ex)), true);                        
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

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            GVOKEmptyDataBind();
            ViewState[VS_LOCATION_TB] = null;
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "myscript", "SetGVBoxHeight('gvbox','GVSubmitData');EnterAgain();", true);
        }

        protected void GVSubmitData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string arrivalid;
            if (true)
            {
                arrivalid = GVSubmitData.DataKeys[e.NewEditIndex].Value.ToString();
                ArrivalIDSessionHandler("REMOVE", arrivalid);
                using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "gen_package_arrival_api.delete_";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_arrival_id", OleDbType.VarChar).Value = arrivalid;

                    cmd.ExecuteNonQuery();
                }
                //UpdatePanel3.Update();  
                GVSubmitDataDataBind();
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel3, this.GetType(), "myscript", "SetGVBoxHeight('gvbox','GVSubmitData');", true);

            }  
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVSubmitQueryDataDataBind();
        }

        private void GVSubmitQueryDataDataBind()
        {


            StringBuilder sql = new StringBuilder("select * from gen_pkg_arr_v where check_mark='init'");

            if (TxtDBbh.Value != string.Empty)
            {
                sql.Append(string.Format(" and package_no='{0}'", TxtDBbh.Value));
            }

            if (TxtXJbh.Value != string.Empty)
            {
                sql.Append(string.Format(" and part_no='{0}'", TxtXJbh.Value));
            }

            if (TxtXJmcen.Text != string.Empty)
            {
                sql.Append(string.Format(" and part_name_e like '{0}'", TxtXJmcen.Text));
            }

            if (TxtXJmccn.Text != string.Empty)
            {
                sql.Append(string.Format(" and part_name like '{0}'", TxtXJmccn.Text));
            }

            if (TxtXJgg.Text != string.Empty)
            {
                sql.Append(string.Format(" and part_spec like '{0}'", TxtXJgg.Text));
            }

            if (TxtXJgdh.Text != string.Empty)
            {
                sql.Append(string.Format(" and dec_no like '{0}'", TxtXJgdh.Text));
            }

            if (TxtPO.Text != string.Empty)
            {
                sql.Append(string.Format(" and po_no = '{0}'", TxtPO.Text));
            }

            if (TxtDHrq.Text != string.Empty)
            {
                sql.Append(string.Format(" and arrived_date = to_date('{0}','yyyy-mm-dd')", TxtDHrq.Text));
            }
            GVSubmitData.DataSource = DBHelper.createGridView(sql.ToString());
            GVSubmitData.DataKeyNames = new string[] { "arrived_id" };
            GVSubmitData.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "myscript", "SetGVBoxHeight('gvbox','GVSubmitData');EnterAgain();", true);
        }

    }
}
