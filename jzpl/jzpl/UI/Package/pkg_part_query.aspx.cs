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
    public partial class pkg_part_query : System.Web.UI.Page
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_PART_Q] == '1') return true;
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
            
            StringBuilder strWhereA = new StringBuilder();
            StringBuilder strWhereB = new StringBuilder();
            if (TxtPackageNo.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and a.package_no='{0}'", TxtPackageNo.Text.Trim()));
                strWhereB.Append(string.Format(" and b.package_no='{0}'", TxtPackageNo.Text.Trim()));
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and a.package_name like '{0}'", TxtPkgName.Text.Trim()));
                strWhereB.Append(string.Format(" and b.package_name like '{0}'", TxtPkgName.Text.Trim()));
            }
            if (DdlProject.SelectedValue != "0")
            {
                strWhereA.Append(string.Format(" and a.project_id= '{0}'", DdlProject.SelectedValue));
                strWhereB.Append(string.Format(" and b.project_id= '{0}'", DdlProject.SelectedValue));
            }
            if (TxtPO.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and a.po_no= '{0}'", TxtPO.Text.Trim()));
                strWhereB.Append(string.Format(" and b.po_no= '{0}'", TxtPO.Text.Trim()));
            }
            if (TxtContract.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and a.contract_no like '{0}'", TxtContract.Text.Trim()));
                strWhereB.Append(string.Format(" and b.contract_no like '{0}'", TxtContract.Text.Trim()));
            }
            if (TxtDec.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and a.dec_no like '{0}'", TxtDec.Text.Trim()));
                strWhereB.Append(string.Format(" and b.dec_no like '{0}'", TxtDec.Text.Trim()));
            }
            if (TxtPart.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and (a.part_name_e like '{0}' or a.part_name like '{0}')", TxtPart.Text.Trim()));
                strWhereB.Append(string.Format(" and (b.part_name_e like '{0}' or b.part_name like '{0}')", TxtPart.Text.Trim()));
            }
            if (TxtSpec.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and a.part_spec like '{0}'", TxtSpec.Text.Trim()));
                strWhereB.Append(string.Format(" and b.part_spec like '{0}'", TxtSpec.Text.Trim()));
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                strWhereA.Append(string.Format(" and a.part_no like '{0}'", TxtPartNo.Text.Trim()));
                strWhereB.Append(string.Format(" and b.part_no like '{0}'", TxtPartNo.Text.Trim()));
            }
            //sql.Append(" order by package_no,part_no");

            String sql = string.Format(@"select b.*,nvl(c.sum_ok_qty, 0) sum_ok_qty,nvl(c.sum_bad_qty, 0) sum_bad_qty,nvl(c.sum_nochk_qty, 0) sum_nochk_qty
                from (select package_no,
                             part_no,
                             sum(part_ok_qty) sum_ok_qty,
                             sum(part_bad_qty) sum_bad_qty,
                             sum(no_check_qty) sum_nochk_qty
                        from gen_pkg_part_in_store_v
                       where package_no || part_no in
                        (select package_no || part_no
                           from gen_part_package_item_v a
                          where 1 = 1 {0})
                    group by package_no, part_no) c,
                gen_part_package_item_v b
                where c.part_no(+) = b.part_no
                and c.package_no(+) = b.package_no
                {1}  order by b.package_no,b.part_no", strWhereA.ToString(),strWhereB.ToString());

            GVData.DataSource = DBHelper.createGridView(sql);
            GVData.DataKeyNames = new string[] { "package_no","part_no" };
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