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

namespace jzpl.UI.Package
{
    public partial class pkg_value_query : System.Web.UI.Page
    {
        private BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) DdlProjectDataBind();
        }

        private void DdlProjectDataBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, string.Empty);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVPkgValueDataBind();
        }    

        private void GVPkgValueDataBind()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select pkg_no,po_no,project_id,buy_qty_due,curr_code,po_line,round(fbuy_unit_price,2) fbuy_unit_price,
 round(fbuy_tax_unit_price,2) fbuy_tax_unit_price,
round(buy_unit_price,2) buy_unit_price,
round(total_base,2) total_base from gen_pkg_po where 1=1");
            if (TxtPackageNo.Text.Trim() != "")
            {
                sql.Append(" and pkg_no='" + TxtPackageNo.Text + "'");
            }
            if (TxtPO.Text.Trim() != "")
            {
                sql.Append(" and po_no='" + TxtPO.Text + "'");
            }
            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(" and project_id='" + DdlProject.SelectedValue + "'");
            }
            GVPkgValue.DataSource = DBHelper.createGridView(sql.ToString());
            GVPkgValue.DataBind();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + string.Format("{0:yyyyMMddHHmmss}", dt) + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GVPkgValue.AllowPaging = false;

            GVPkgValueDataBind();

            GVPkgValue.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            //GVData.AllowPaging = true;

            GVPkgValueDataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
    }
}
