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
    public partial class pkg_part_lov1 : System.Web.UI.Page
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
                        DdlProjectDataBind();
                        GVPart.DataSource = null;
                        GVPart.DataBind();
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
            //if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_ADD] == '1') return true;
            //return false;
            return true;
        }   

        private void DdlProjectDataBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);
        }

        protected void BtnResult_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("select * from gen_pkg_onhand where 1=1");

            if (DdlProject.SelectedValue != "0")
            {
                sql.Append(string.Format(" and project_id='{0}'", DdlProject.SelectedValue));
            }
            else
            {
                sql.Append(string.Format(" and project_id in ({0})", ProjectWhereString()));
            }
            if (TxtPkgNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_no like '{0}'", TxtPkgNo.Text.Trim()));
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and package_name like '{0}'", TxtPkgName.Text.Trim()));
            }
            if (TxtPO.Text.Trim() != "")
            {
                sql.Append(string.Format(" and  po_no='{0}'", TxtPO.Text.Trim()));
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and part_no like '{0}'", TxtPartNo.Text.Trim()));
            }
            if (TxtPartName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and (part_name_e like '{0}' or part_name like '{0}')", TxtPartName.Text.Trim()));
            }
            if (TxtPartSpec.Text.Trim() != "")
            {
                sql.Append(string.Format(" and spec like '{0}'", TxtPartSpec.Text.Trim()));

            }
            if (TxtContractNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and contract_no like '{0}'", TxtContractNo.Text));
            }

            if (TxtDocNo.Text.Trim() != "")
            {
                sql.Append(string.Format(" and dec_no like '{0}'", TxtDocNo.Text));
            }
            
            GVPart.DataSource = DBHelper.createGridView(sql.ToString());
            GVPart.DataBind();           

        }

        protected string ProjectWhereString()
        {  
            StringBuilder ret = new StringBuilder();

            ListItemCollection items_ = new ListItemCollection();
            foreach (ListItem itm in DdlProject.Items)
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
    }
}
