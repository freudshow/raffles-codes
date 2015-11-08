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
    public partial class pkg_arrival_query : System.Web.UI.Page
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_ARR_Q] == '1') return true;
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
            StringBuilder sql = new StringBuilder("select * from gen_pkg_arr_v where 1=1 ");
            if (TxtArrDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and arr_date_ch ='{0}'", TxtArrDate.Text.Trim()));
            }
            if (TxtPackageNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_no='{0}'", TxtPackageNo.Text.Trim()));
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and pkg_name like '{0}'", TxtPkgName.Text.Trim()));
            }
            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id='{0}'", DdlProject.SelectedValue));
            }
            if (TxtPO.Text.Trim() != "")
            {
                sql.Append(string.Format(" and po_no= '{0}'", TxtPO.Text.Trim()));
            }
            if (TxtContract.Text.Trim() != "")
            {
                sql.Append(string.Format(" and contract_no like '{0}'", TxtContract.Text.Trim()));
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
                sql.Append(string.Format(" and part_spec like '{0}'", TxtSpec.Text.Trim()));
            }
            if (ChkBoxChecked.Checked && ChkBoxNoChk.Checked)
            {
                sql.Append(" and check_mark in ('nocheck','checked')");
            }
            else
            {
                if (ChkBoxNoChk.Checked)
                {
                    sql.Append(" and check_mark='nocheck'");
                }
                if (ChkBoxChecked.Checked)
                {
                    sql.Append(" and check_mark='checked'");
                }
            }
            sql.Append(" order by to_number(arrived_id) ");
            GVData.DataSource = DBHelper.createGridView(sql.ToString());
            GVData.DataKeyNames = new string[] { "arrived_id" };
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

        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {       
            DateTime dt = DateTime.Now;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + string.Format("{0:yyyyMMddHHmmss}", dt) + ".xls");
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