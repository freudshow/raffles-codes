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

namespace jzpl.UI.FrameUI
{
    public partial class ProjectLoader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DdlProject.DataSource = Lib.DBHelper.createDDLView("select project_id,project_id||'    ' ||name project_des from ifsapp.project@erp_prod where probability_to_win <> 1");
            DdlProject.DataTextField = "project_des";
            DdlProject.DataValueField = "project_id";
            DdlProject.DataBind();
        }
    }
}