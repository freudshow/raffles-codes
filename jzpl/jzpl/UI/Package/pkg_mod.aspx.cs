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

namespace jzpl.UI.Package
{
    public partial class pkg_mod : System.Web.UI.Page
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

                        bindDDLData();
                        bindDDLcurr();
                        init();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG] == '1') return true;
            return false;
        }
        

        private void init()
        {
            this.TxtPackageNo.Text = Misc.GetHtmlRequestValue(Request, "id");
            if (this.TxtPackageNo.Text != "")
            {
                using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    string sql = "select * from gen_part_package where package_no = '" + this.TxtPackageNo.Text + "'";
                    DataView dv = DBHelper.createGridView(sql);

                    DdlProject.SelectedIndex = DdlProject.Items.IndexOf(DdlProject.Items.FindByValue(dv[0]["project_id"].ToString()));                    
                    TxtPackageName.Text = dv[0]["package_name"].ToString();
                    TxtPO.Text = dv[0]["po_no"].ToString();
                    TxtAmount.Text = dv[0]["package_value"].ToString();
                    DdlCurrency.SelectedIndex = DdlCurrency.Items.IndexOf(DdlCurrency.Items.FindByValue(dv[0]["currency"].ToString()));                                      
                }
            }
        }

        private void bindDDLData()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);
        }

        private void bindDDLcurr()
        {
            baseInfoLoader.CurrencyDropDownListLoad(DdlCurrency, false, true);            
        }

        protected void ButtSave_Click(object sender, EventArgs e)
        {
            string v_out="";
            decimal amount;
            if (DdlProject.SelectedValue == "0")
            {
                Misc.Message(this.GetType(), ClientScript, "保存失败，请选择项目。");
                return;
            }
            if (TxtPackageNo.Text.Trim() == "")
            {
                Misc.Message(this.GetType(), ClientScript, " 保存失败，请输入大包物资编码。");
            }
            if (TxtPackageName.Text.Trim() == "")
            {
                Misc.Message(this.GetType(), ClientScript, " 保存失败，请输入大包物资描述。");
            }
            if (TxtPO.Text.Trim() == "")
            {
                Misc.Message(this.GetType(), ClientScript, "保存失败，请输入PO");
            }
            if (TxtAmount.Text == "")
            {
                amount = 0;
            }
            else
            {
                try
                {
                    amount = Convert.ToDecimal(TxtAmount.Text);
                }
                catch (Exception ex)
                {
                    Misc.Message(this.GetType(), ClientScript, "保存失败，请为大包价值输入正确的数值。");
                    return;
                }
            }
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand("gen_part_package_api.update_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("v_project_id", OleDbType.VarChar, 20).Value = DdlProject.SelectedValue;
                cmd.Parameters.Add("v_po_no", OleDbType.VarChar, 20).Value = this.TxtPO.Text;
                cmd.Parameters.Add("v_Package_no", OleDbType.VarChar, 20).Value = this.TxtPackageNo.Text;
                cmd.Parameters.Add("v_Package_name", OleDbType.VarChar, 500).Value = this.TxtPackageName.Text;
                cmd.Parameters.Add("v_Package_value", OleDbType.Numeric).Value = amount;
                cmd.Parameters.Add("v_currency", OleDbType.VarChar, 20).Value = DdlCurrency.SelectedValue;
                cmd.Parameters.Add("v_out", OleDbType.VarChar, 200).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    v_out = cmd.Parameters["v_out"].Value.ToString();

                    if (v_out != "1")
                    {
                        Misc.Message(this.GetType(), ClientScript, string.Format("保存失败，错误：{0}", v_out));
                        return;
                    }
                    tr.Commit();
                    Misc.RegisterClientScript(this.GetType(), "pkgmod_refresh", ClientScript, "<script type='text/javascript'>window.dialogArguments.refresh();window.close();</script>");
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    Misc.Message(this.GetType(), ClientScript, string.Format("保存失败，错误：{0}", v_out));
                }
                finally
                {
                    conn.Close();
                }                
            }
        }
    }
}
