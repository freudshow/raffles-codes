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
using System.Data.OleDb;
using jzpl.Lib;

namespace jzpl.UI.JP
{
    public partial class wzxqjh_ration_lov : System.Web.UI.Page
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
                        DataBox.Visible = false;
                        LblMsg.Text = "";
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PART_JP_ADD] == '1') return true;
            return false;
        }

        protected void DdlProjectBind()
        {
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);            
        }
        protected void BtnShowRation_Click(object sender, EventArgs e)
        {
            DataBox.Visible = true;
            LblMsg.Text = "";

            string projectId = this.DdlProject.SelectedValue;
            string partNo = this.TxtPartNo.Text;
            string mtrNo = TxtMtrNo.Text;

            StringBuilder sqlstr = new StringBuilder("select a.site,a.part_no,ifsapp.inventory_part_api.Get_Description@erp_prod(a.site,a.part_no) part_name, ");

            sqlstr.Append("b.project_id,");
            sqlstr.Append("b.purch_part_no,");
            sqlstr.Append("b.misc_tab_ref_no,");
            sqlstr.Append("b.material_req_seq_no,");
            sqlstr.Append("b.ration_qty,");
            sqlstr.Append("nvl(b.issued_qty,0) issued_qty,");
            sqlstr.Append("b.activity_seq,");
            sqlstr.Append("ifs_data_api.get_all_activity_desc(b.activity_seq) activity_desc,");
            /*用api取jp_requisition表对应ration行的申请总数，考虑jp_requisition行的状态,状态限制不等于“canceled”,如果有并发用户，校验时这个数有可能已经不准确了*/
            sqlstr.Append("nvl(jp_requisition_api.get_required_num_of_mtr(b.misc_tab_ref_no,b.material_req_seq_no),0) REQUESTED_QTY,");
            sqlstr.Append("b.ration_qty-nvl(jp_requisition_api.get_required_num_of_mtr(b.misc_tab_ref_no,b.material_req_seq_no),0) REMAIN_QTY,");
            sqlstr.Append("a.issue_from_inv ");
            sqlstr.Append(",a.DESIGN_CODE,a.C_PARTIAL_INFO PARTIAL_INFO "); //ming.li 20130321 10:50 增加物料属性
            sqlstr.Append(" from ");
            sqlstr.Append("IFSAPP.PROJECT_MISC_PROCUREMENT@erp_prod a,");
            sqlstr.Append("IFSAPP.PROJ_PROCU_RATION@erp_prod b ");
            sqlstr.Append(",ifsapp.purchase_part@erp_prod c");// ming.li  20130318 11:35 添加c表
            sqlstr.Append(" where ");
            sqlstr.Append(" a.matr_seq_no=b.misc_tab_ref_no ");
            sqlstr.Append(" and c.contract=a.site and a.part_no=c.part_no "); // ming.li  20130318 11:35 c表与a表的关联
            //ming.li 20130318 10:55 项目改成非必填
            //sqlstr.Append(string.Format(" and a.project_id='{0}'", projectId));
            if (!(projectId == "" || projectId == "0"))
            {
                sqlstr.Append(string.Format(" and a.project_id='{0}'", projectId));
            }
            if (partNo != "")
            {
                sqlstr.Append(string.Format(" and a.part_no ='{0}'", partNo));
            }
            if (mtrNo != "")
            {
                sqlstr.Append(string.Format(" and b.misc_tab_ref_no='{0}'", mtrNo));
            }
            sqlstr.Append(" and b.ration_qty > nvl(b.issued_qty,0)");
            //sqlstr.Append(" and (");
            //sqlstr.Append(" ifsapp.PROJ_PROCU_RATION_API.Get_Misc_Tab_Num_Info@erp_prod(b.MISC_TAB_REF_NO, 'QTY_RECEIVED') > nvl(b.issued_qty,0)");
            //sqlstr.Append(" or a.issue_from_inv = 1)");
            if (TxtPartName.Text.Trim() != "")
            {
                sqlstr.Append(string.Format(" and c.description like '{0}'", TxtPartName.Text));
            }

            GVRation.DataSource = Lib.DBHelper.createGridView(sqlstr.ToString());
            GVRation.DataBind();

            if (GVRation.Rows.Count == 0)
            {
                DataBox.Visible = false;
                LblMsg.Text = "未找到符合条件数据。";
            }
        }        
    }
}
