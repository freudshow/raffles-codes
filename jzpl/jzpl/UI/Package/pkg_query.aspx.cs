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
using System.IO;

namespace Package
{
    public partial class pkg_query : System.Web.UI.Page
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
                        DdlProjectBind();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_Q] == '1') return true;
            return false;
        }

        protected void DdlProjectBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, string.Empty);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataBind();
        }

        private void GVDataBind()
        {
            StringBuilder sql = new StringBuilder("select project_id,jp_project_api.get_name(project_id) project_name,package_no,package_name,po_no,package_value,currency from gen_part_package");
            sql.Append(" where 1=1");
            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id='{0}'", DdlProject.SelectedValue));
            }
            if (TxtPackageNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_no like '{0}'", TxtPackageNo.Text.Trim()));
            }
            //if (TxtPO.Text.Trim() != "")
            //{
            //    sql.Append(string.Format(" and po_no='{0}'", TxtPO.Text.Trim()));
            //}
            if (TxtPkgName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_name like '{0}'", TxtPkgName.Text.Trim()));
            }
            GVData.DataSource = DBHelper.createGridView(sql.ToString());
            GVData.DataKeyNames = new string[] { "package_no" };
            GVData.DataBind();
        }

        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            GVDataBind();
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename="+string.Format("{0:yyyyMMddHHmmss}",dt)+".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GVData.AllowPaging = false;

            GVDataBind();

            GVData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            GVData.AllowPaging = true;

            GVDataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
    }
}
