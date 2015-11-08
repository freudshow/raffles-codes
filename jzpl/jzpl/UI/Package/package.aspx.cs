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
using System.Data.OracleClient;
using jzpl.Lib;
using System.Text;

namespace Package
{
    public partial class package : System.Web.UI.Page
    {
        private string m_perimission;
        BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
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
                        DdlProjectBind();  
                        divNewPackage.Visible = true;
                        divQeruyPackage.Visible = false;
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

        protected void DdlProjectBind()
        {            
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);  
        }        

        protected void GVBaleEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string no = GVBaleEdit.DataKeys[e.RowIndex].Value.ToString();
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                OleDbCommand cmd = new OleDbCommand("gen_part_package_api.delete_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbTransaction tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd.Parameters.Add("v_Package_no", OleDbType.VarChar, 20).Value = no;
                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    GVBaleEditDataBind();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
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

        protected void ButtQuery_Click(object sender, EventArgs e)
        {
            if (!PrepareQuery()) return;
            GVBaleEditDataBind();
        }

        private Boolean PrepareQuery()
        {
            if (DdlProjectQ.SelectedValue == "-1")
            {
                Misc.Message(this.GetType(), ClientScript, "无可访问的项目，请联系管理员添加项目访问权限。");
                return false;
            }
            return true;
        }

        private void GVBaleEditDataBind()
        {
            UserSaveData.Visible = true;
            StringBuilder sql = new StringBuilder("select t.*,jp_project_api.get_name(t.project_id) project_name from gen_part_package t where 1=1");
            if (DdlProjectQ.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id ='{0}'", DdlProjectQ.SelectedValue));
            }
            else
            {
                sql.Append(string.Format(" and project_id in ({0})", ProjectWhereString()));
            }           
            if (TxtPackageNoQ.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_no like '{0}'", TxtPackageNoQ.Text));
            }
            if (TxtPackageNameQ.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_name like '{0}'", TxtPackageNameQ.Text));
            }
            GVBaleEdit.DataSource = DBHelper.createGridView(sql.ToString());
            GVBaleEdit.DataKeyNames = new string[] { "package_no" };
            GVBaleEdit.DataBind();
            //if (GVBaleEdit.Rows.Count == 0) UserSaveData.Visible = false;

        }

        private void GVBaleEditDataBind_(string objid_)
        {
            StringBuilder sql = new StringBuilder("select t.*,jp_project_api.get_name(t.project_id) project_name from gen_part_package t where rowid='" + objid_ + "'");
            
            GVBaleEdit.DataSource = DBHelper.createGridView(sql.ToString());
            GVBaleEdit.DataKeyNames = new string[] { "package_no" };
            GVBaleEdit.DataBind();
        }

        protected string ProjectWhereString()
        {
            StringBuilder ret = new StringBuilder();

            ListItemCollection items_ = new ListItemCollection();
            foreach (ListItem itm in DdlProjectQ.Items)
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

        protected void GVBaleEdit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVBaleEdit.PageIndex = e.NewPageIndex;
            GVBaleEditDataBind();
        }
        
        protected void BtnBack_Click(object sender, EventArgs e)
        {
         
            divNewPackage.Visible = true;
            divQeruyPackage.Visible = false;

            TxtPackageNo.Text = string.Empty;
            TxtPackageName.Text = string.Empty;
            DdlProjectBind();
        }   

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string strObjid;
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

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand("gen_part_package_api.new_", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("v_project_id", OleDbType.VarChar, 20).Value = DdlProject.SelectedValue;                
                cmd.Parameters.Add("v_Package_no", OleDbType.VarChar, 20).Value = this.TxtPackageNo.Text;
                cmd.Parameters.Add("v_Package_name", OleDbType.VarChar, 500).Value = this.TxtPackageName.Text;
                cmd.Parameters.Add("v_objid", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    strObjid = cmd.Parameters["v_objid"].Value.ToString();
                    GVBaleEditDataBind_(strObjid);
                    
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.Message(this.GetType(), ClientScript, Misc.GetDBCustomException(ex));
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void LnkBtnGoToQuery_Click(object sender, EventArgs e)
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProjectQ, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID); 
            divQeruyPackage.Visible = true;
            divNewPackage.Visible = false;
        }

        protected void TxtPackageNo_TextChanged(object sender, EventArgs e)
        {
            string partNo = TxtPackageNo.Text;
            object objPart = DBHelper.getObject("select description from ifsapp.inventory_part@erp_prod where contract = '03' and part_no = '" + partNo + "'");
            string partName = objPart == null ? "" : objPart.ToString();
            TxtPackageName.Text = partName;

        }

        protected void BtnResult_Click(object sender, EventArgs e)
        {
            if (PrepareQuery())
            {
                GVBaleEditDataBind();
            }
        }
    }
}
