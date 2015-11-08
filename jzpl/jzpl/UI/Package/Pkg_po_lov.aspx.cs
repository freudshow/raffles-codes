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
using System.Text;

namespace jzpl.UI.Package
{
    public partial class Pkg_po_lov : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            DdlProjectBind();
        }

        protected void DdlProjectBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject_, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);
        }

        protected void BtnResult_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder("select rownum,project_id,order_no,description,part_no from IFSAPP.PURCHASE_ORDER_LINE_ALL@erp_prod where state <>'Cancelled' and part_no is not null");
            if (DdlProject_.SelectedValue == "0" && TxtPO.Text.Trim() == "" && TxtDBbh.Text.Trim() == "")
            {
                Misc.Message(this.GetType(), ClientScript, "项目、PO、零件编号不能都为空。");
                return;
            }

            if (TxtDBbh.Text.Trim() != "")
            {
                sql.Append(string.Format(" and part_no like '{0}'", TxtDBbh.Text));
            }
            if (TxtDBmc.Text.Trim() != "")
            {
                sql.Append(string.Format(" and description like '{0}'", TxtDBmc.Text));
            }
            if (TxtPO.Text.Trim() != "")
            {
                sql.Append(string.Format(" and order_no = '{0}'", TxtPO.Text));
            }
            if (DdlProject_.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id='{0}'", DdlProject_.SelectedValue));
            }
            GVERPPart.DataSource = DBHelper.createGridView(sql.ToString());
            GVERPPart.DataKeyNames = new string[] { "part_no" };
            GVERPPart.DataBind();
        }
    }
}
