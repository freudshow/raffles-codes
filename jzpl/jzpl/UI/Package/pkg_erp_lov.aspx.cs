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
    public partial class pkg_erp_lov : System.Web.UI.Page
    {
        private string m_perimission;
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
                        DBGrid.DataSource = null;
                        DBGrid.DataBind();
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
        private void bindDDLData()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DDLProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);
        }
        private string getSQL()
        {
            string SQL = "", Project = "", PO = "", DBBH = "", DBMC = "";
            if (DDLProject.SelectedValue != "0")
            {
                Project = " and project_id='" + this.DDLProject.SelectedValue + "'";
            }
            else
            {
                Project = string.Format(" and project_id in ({0})", ProjectWhereString());
            }

            if (TxtPO.Text != "") PO = " and order_no = '" + TxtPO.Text.Trim() + "'";
            if (TxtDBbh.Text != "") DBBH = " and part_no like '" + TxtDBbh.Text + "'";
            if (TxtDBmc.Text != "") DBMC = " and description like '" + TxtDBmc.Text + "'";

            SQL = "select project_id,order_no as po_no,description as package_name,fbuy_tax_unit_price as package_value,currency_code as currency,part_no as package_no from IFSAPP.PURCHASE_ORDER_LINE_ALL@erp_prod where state <>'Cancelled' and part_no is not null " + Project + PO + DBBH + DBMC;
            return SQL;
        }

        protected string ProjectWhereString()
        {
            StringBuilder ret = new StringBuilder();

            ListItemCollection items_ = new ListItemCollection();
            foreach (ListItem itm in DDLProject.Items)
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

        private void bindGV()
        {
            DataView view = DBHelper.createGridView(this.getSQL());
            DBGrid.DataSource = view;
            DBGrid.DataKeyNames = new string[] { "package_no" };
            DBGrid.DataBind();
            if (view.Count == 0)
            {
                Misc.Message(this.GetType(), ClientScript, "未找到符合条件的数据。");
                return;
            }
        }
        protected void BtnResult_Click(object sender, EventArgs e)
        {
            if (!PrepareQuery()) return;
            bindGV();
        }

        protected void DBGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DBGrid.PageIndex = e.NewPageIndex;
            bindGV();
        }

        private Boolean PrepareQuery()
        {
            if (DDLProject.SelectedValue == "-1")
            {
                Misc.Message(this.GetType(), ClientScript, "无可访问的项目，请联系管理员添加项目访问权限。");
                return false;
            }
            return true;
        }
    }
}
