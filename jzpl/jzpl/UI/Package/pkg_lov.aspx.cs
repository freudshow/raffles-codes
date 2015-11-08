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
    public partial class pkg_arrival_db : System.Web.UI.Page
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
                        bindDDLData();
                        DBGridEmptyDataBind();
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
            //if (m_perimission[(int)Authentication.PERMDEFINE.PKG_PART] == '1') return true;
            //return false;
            return true;
        }
     
        private void bindDDLData()
        {
            baseInfoLoader.ProjectDropDownListLoad(DDLProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);            
        }        

        protected void BtnResult_Click(object sender, EventArgs e)
        {
            DBGridDataBind();
        }

        private void DBGridDataBind()
        {
            StringBuilder sql = new StringBuilder("select * from gen_part_package where 1=1");
            if (DDLProject.SelectedValue == "0")
            {
                Misc.Message(this.GetType(), ClientScript, "请选择项目,再进行查询。");
                return;
            }
            sql.Append(string.Format(" and project_id='{0}'", DDLProject.SelectedValue));
           
            if (TxtDBbh.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_no like '{0}'",TxtDBbh.Text));
            }
            if (TxtDBmc.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_name like '{0}'",TxtDBmc.Text));
            }
            DBGrid.DataSource = DBHelper.createGridView(sql.ToString());
            DBGrid.DataKeyNames = new string[] { "package_no" };
            DBGrid.DataBind();
        }

        private void DBGridEmptyDataBind()
        {
            DBGrid.DataSource = null;
            DBGrid.DataBind();
        }
    }
}
