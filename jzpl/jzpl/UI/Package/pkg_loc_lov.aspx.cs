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
    public partial class pkg_loc_lov : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string company_;
                string areaid_;
                BaseInfoLoader infoLoader = new BaseInfoLoader();

                company_ = Misc.GetHtmlRequestValue(Request, "company");
                areaid_ = Misc.GetHtmlRequestValue(Request, "areaid");

                TxtCompany.Text = company_;
                //infoLoader.CompanyDropDrownListLoad(DdlCompany, false, true, true, company_);

                DdlArea.DataSource = DBHelper.createGridView(string.Format("select area_id value_,area text_ from jp_wh_area  where company_id='{0}' and state='1'", company_));
                DdlArea.DataTextField = "text_";
                DdlArea.DataValueField = "value_";
                DdlArea.DataBind();

                DdlArea.SelectedIndex = DdlArea.Items.IndexOf(DdlArea.Items.FindByValue(areaid_));

                GVLocDataBind();
            }
        }

        protected void BtnLocationQuery_Click(object sender, EventArgs e)
        {
            GVLocDataBind();
        }

        private void GVLocDataBind()
        {
            StringBuilder sql = new StringBuilder(string.Format("select * from jp_wh_loc_v where area_id = '{0}'",DdlArea.SelectedValue));
            if (TxtLocation.Text.Trim() != "")
            {
                sql.Append(string.Format("and location like '{0}'", TxtLocation.Text));
            }
            GVLocation.DataSource = DBHelper.createGridView(sql.ToString());
            GVLocation.DataBind();
        }

        
    }
}
