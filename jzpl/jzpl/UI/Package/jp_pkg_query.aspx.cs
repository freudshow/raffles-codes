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
using System.Text;


namespace Package
{
    public partial class jp_pkg_query : System.Web.UI.Page
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
                        DdlProdSiteBind();
                        DdlReceiptDeptBind();
                        DdlProjectDataBind();
                        DdlReqGroupDataBind();
                        //for (int i = 0; i < DdlProdSite.Items.Count; i++)
                        //{
                        //    DdlProdSite.Items[i].Attributes.Add("title", DdlProdSite.Items[i].Text);
                        //}
                        PreQuery();
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
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_QUERY] == '1') return true;
            return false;
        }
        
        protected void DdlProdSiteBind()
        {
            DdlProdSite.DataSource = DBHelper.createDDLView("select place_id,place_name from jp_receipt_place  where state='1'");
            DdlProdSite.DataTextField = "place_name";
            DdlProdSite.DataValueField = "place_id";
            DdlProdSite.DataBind();
        }

        protected void DdlReceiptDeptBind()
        {
            DdlReceiptDept.DataSource = DBHelper.createDDLView("select dept_id,company||' '||dept_desc dept_desc from jp_receipt_dept t where state='1'");
            DdlReceiptDept.DataTextField = "dept_desc";
            DdlReceiptDept.DataValueField = "dept_id";
            DdlReceiptDept.DataBind();
        }

        private void DdlProjectDataBind()
        {
            BaseInfoLoader baseInfoLoader = new BaseInfoLoader();
            baseInfoLoader.ProjectDropDownListLoad(DdlProject, false, true, ((Authentication.LOGININFO)Session["USERINFO"]).UserID);    

        }

        private void DdlReqGroupDataBind()
        {
            DdlReqGroup.DataSource = DBHelper.createDDLView("select group_id,company_id||' '||group_name group_name from jp_req_group");
            DdlReqGroup.DataTextField = "group_name";
            DdlReqGroup.DataValueField = "group_id";
            DdlReqGroup.DataBind();
        }

        private void PreQuery()
        {
            //当前登录用户
            TxtReqUser.Text = "";
            //当前日期
            //TxtReqDate.Text = DateTime.Now.ToString();
            //当前用户的用户组
            //DdlReqGroup.SelectedIndex = DdlReqGroup.Items.FindByValue("");

        }

        protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] temp;
            string objid;
            string rowversion;
            int row_index;
            string req_id;

            if (e.CommandName == "ReqLineDelete")
            {
                temp = e.CommandArgument.ToString().Split('^');
                if (temp.Length != 2)
                {
                    Misc.Message(this.GetType(), ClientScript, "删除失败，错误参数。");
                    return;
                }

                objid = temp[0];
                rowversion = temp[1];
                row_index = ((GridViewRow)(((ImageButton)e.CommandSource).Parent.Parent)).RowIndex;
                req_id = GVData.DataKeys[row_index].Value.ToString();

                using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand("jp_pkg_requisition_api.delete_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    OleDbTransaction tr = conn.BeginTransaction();
                    cmd.Transaction = tr;

                    cmd.Parameters.Add("v_objid", OleDbType.VarChar).Value = objid;
                    cmd.Parameters.Add("v_rowversion", OleDbType.VarChar).Value = rowversion;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        tr.Commit();                        
                        GVDataDataBind();

                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                CheckBox chk = e.Row.FindControl("ChkCrane") as CheckBox;
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["crance"].ToString() == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                chk.Enabled = false;
            }
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVDataDataBind();
        }

        private void GVDataDataBind()
        {
            StringBuilder sql = new StringBuilder("select * from jp_pkg_requisition_v where rowstate='init'");
            if (TxtReqID.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(requisition_id)=upper('{0}')", TxtReqID.Text.Trim()));
            }
            if (TxtPartNo.Text.Trim()!="")
            {
                sql.Append(string.Format(" and upper(part_no)=upper('{0}')", TxtPartNo.Text.Trim()));
            }
            if (DdlPSType.SelectedValue != "-1")
            {
                sql.Append(string.Format(" and psflag='{0}'", DdlPSType.SelectedValue));
            }
            if (DdlProject.SelectedValue!="0")
            {
                sql.Append(string.Format(" and project_id = '{0}'", DdlProject.SelectedValue));
            }
            else
            {
                sql.Append(string.Format(" and project_id in ({0})", ProjectWhereString()));
            }
            if (TxtPkgNo.Text.Trim()!="")
            {
                sql.Append(string.Format(" and upper(package_no)=upper('{0}')", TxtPkgNo.Text.Trim()));
            }
            if (TxtPkgName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(package_name) like upper('{0}')", TxtPkgName.Text));
            }
            if (TxtPartName.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_name) like upper('{0}')", TxtPartName.Text));
            }
            if (TxtPartNameE.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_name_e) like upper('{0}')", TxtPartNameE.Text));
            }
            if (TxtPartSpec.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(part_spec) like upper('{0}')", TxtPartSpec.Text));
            }
            if (DdlProdSite.SelectedValue!="0")
            {
                sql.Append(string.Format(" and place_id='{0}'", DdlProdSite.SelectedValue));
            }
            if (DdlReceiptDept.SelectedValue != "0")
            {
                sql.Append(string.Format(" and receipt_dept='{0}'", DdlReceiptDept.SelectedValue));
            }
            if (TxtReceiver.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(receiver)=upper('{0}')", TxtReceiver.Text));
            }
            if (TxtBlock.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(project_block) = upper('{0}')", TxtBlock.Text));
            }
            if (TxtSystem.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(project_system)=upper('{0}')", TxtSystem.Text));
            }
            if (TxtWorkContent.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(work_content)=upper('{0}')", TxtWorkContent.Text));
            }
            if (TxtDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and receipt_date=to_date('{0}','yyyy-mm-dd')", TxtDate.Text));
            }
            if (TxtIC.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(receiver_ic)=upper('{0}')", TxtIC.Text));
            }
            if (DdlReqGroup.SelectedValue != "0")
            {
                sql.Append(string.Format(" and req_group='{0}'", DdlReqGroup.SelectedValue));
            }
            if (TxtReqUser.Text.Trim() != "")
            {
                sql.Append(string.Format(" and upper(recorder)=upper('{0}')", TxtReqUser.Text));
            }
            if (TxtReqDate.Text.Trim() != "")
            {
                sql.Append(string.Format(" and to_char(record_time,'yyyy-mm-dd') = to_char(to_date('{0}','yyyy-mm-dd'),'yyyy-mm-dd')", TxtReqDate.Text));
            }

            GVData.DataSource = DBHelper.createGridView(sql.ToString());
            GVData.DataKeyNames = new string[] { "requisition_id" };
            GVData.DataBind();
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

        protected void DdlProdSite_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < DdlProdSite.Items.Count; i++)
            {
                DdlProdSite.Items[i].Attributes.Add("title", DdlProdSite.Items[i].Text);
            }
        }
        
    }
}
