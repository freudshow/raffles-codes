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
using System.IO;
using System.Text;
using System.Xml;

namespace Package
{
    public partial class pkg_loc_query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlProjectDataBind();
            }
        }

        private void DdlProjectDataBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, string.Empty);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            query();
            string part_name = Request.Form["TxtPartNameE"];
            TxtPartNameE.Text = part_name;
        }

        private void query()
        {
            GVData.DataSource = DBHelper.createGridView(this.getsql());
            GVData.DataBind();
        }

        private string getsql()
        {
            string sql = "", packageno = "", partno = "", querydate = "";
            if (TxtQueDate.Text != "") querydate = " and ARRIVED_DATE='" + TxtQueDate.Text + "'";
            if (TxtPackageNo.Text != "") packageno = " and package_no like'" + TxtPackageNo.Text + "'";
            if (TxtPartNo.Text != "") partno = "and part_no like '" + TxtPartNo.Text + "'";
           
            sql = "select * from gen_pkg_part_in_store_v where 1=1" + querydate + packageno + partno;
            if (DdlProject.SelectedValue != "0")
            {
                sql = sql + string.Format(" and project_id='{0}'", DdlProject.SelectedValue);
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                sql = sql + string.Format(" and package_name like '{0}'", TxtPkgName.Text);
            }
            if (TxtDec.Text.Trim() != "")
            {
                sql = sql + string.Format(" and dec_no like '{0}'", TxtDec.Text);
            }
            if (TxtSpec.Text.Trim() != "")
            {
                sql = sql + string.Format(" and spec like '{0}'", TxtSpec.Text);
            }
            if (TxtArea.Text.Trim() != "")
            {
                sql = sql + string.Format(" and (area like '{0}' or area_id like '{0}')", TxtArea.Text);
            }
            if (TxtLoc.Text.Trim() != "")
            {
                sql = sql + string.Format(" and location like '{0}'", TxtLoc.Text);
            }
            return sql;
        }


        protected void GVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVData.PageIndex = e.NewPageIndex;
            query();
        }

        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {

            DateTime dt = DateTime.Now;            
            
            Response.ClearContent();
           
            Response.AddHeader("content-disposition", "attachment; filename=" + string.Format("{0:yyyyMMddHHmmss}", dt) + ".xls");
            Response.ContentType = "application/excel";
            Response.Write(@"<style> .TextCell {mso-number-format:\@;}</style>");

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GVData.AllowPaging = false;
            query();
            GVData.RenderControl(htw);             
            Response.Write(sw.ToString());
            Response.End();

            GVData.AllowPaging = true;

            query();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].Attributes.Add("class", "TextCell");
                e.Row.Cells[10].Attributes.Add("class", "TextCell");
            }           
            
        }

        protected void page_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            //取得显示分页界面的那一行
            GridViewRow pagerRow = GVData.BottomPagerRow;
            //从显示页数的行中取得显示页数的DropDownList控件
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");
            //将GridView移至用户所选择的页数
            GVData.PageIndex = pageList.SelectedIndex;
            query();
        }

        protected void GVData_DataBound(object sender, EventArgs e)
        {


            //取得显示分页界面的那一行
            GridViewRow pagerRow = GVData.BottomPagerRow;
            if (pagerRow != null)
            {
                //取得第一页。上一页。下一页。最后一页的超级链接
                LinkButton lnkBtnFirst = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnFirst");
                LinkButton lnkBtnPrev = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnPrev");
                LinkButton lnkBtnNext = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnNext");
                LinkButton lnkBtnLast = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnLast");

                //设置何时应该禁用第一页。上一页。下一页。最后一页的超级链接
                if (GVData.PageIndex == 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                }
                else if (GVData.PageIndex == GVData.PageCount - 1)
                {
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                else if (GVData.PageCount <= 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                //从显示分页的行中取得用来显示页次与切换分页的DropDownList控件
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");

                //根据欲显示的数据源的总页数，创建DropDownList控件的下拉菜单内容
                if (pageList != null)
                {
                    int intPage;
                    for (intPage = 0; intPage <= GVData.PageCount - 1; intPage++)
                    {
                        //创建一个ListItem对象来存放分页列表
                        int pageNumber = intPage + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        //交替显示背景颜色
                        switch (pageNumber % 2)
                        {
                            case 0: item.Attributes.Add("style", "background:#CDC9C2;");
                                break;
                            case 1: item.Attributes.Add("style", "color:red; background:white;");
                                break;
                        }
                        if (intPage == GVData.PageIndex)
                        {
                            item.Selected = true;
                        }
                        pageList.Items.Add(item);
                    }
                }
                //显示当前所在页数与总页数
                Label pagerLabel = (Label)pagerRow.Cells[0].FindControl("lblCurrrentPage");

                if (pagerLabel != null)
                {

                    int currentPage = GVData.PageIndex + 1;
                    pagerLabel.Text = "第" + currentPage.ToString() + "页（共" + GVData.PageCount.ToString() + " 页）";

                }
            }

        }

    }
}
