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
using System.IO;


namespace jzpl.UI.Package
{
    public partial class pkg_loc_history : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
                //OleDbCommand cmd = new OleDbCommand("jp_public.set_g_user", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("v_g_user", OleDbType.VarChar).Value = "zhang yj";
                //conn.Open();
                //cmd.ExecuteNonQuery();

                DdlHisTypeDataBind();
            }
        }

        private void DdlHisTypeDataBind()
        {
            DdlHisType.Items.Add(new ListItem("全部", "0"));
            DdlHisType.Items.Add(new ListItem("调整出库", "man_iss"));
            DdlHisType.Items.Add(new ListItem("调整入库", "man_receipt"));
            DdlHisType.Items.Add(new ListItem("检验入库", "check_in"));
            DdlHisType.Items.Add(new ListItem("领料下发", "iss"));
            DdlHisType.Items.Add(new ListItem("库位转移", "trans"));
            DdlHisType.Items.Add(new ListItem("项目借用", "proj_trans"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString);
            conn.Open();
            SetDBUser(conn);
            OleDbCommand cmd = new OleDbCommand("jp_public.get_g_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("v_g_user", OleDbType.VarChar,100).Direction = ParameterDirection.Output;            
            cmd.ExecuteNonQuery();

            Response.Write(cmd.Parameters["v_g_user"].Value.ToString());
        }

        private void SetDBUser(OleDbConnection conn)
        {
            OleDbCommand cmd = new OleDbCommand("jp_public.set_g_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("v_g_user", OleDbType.VarChar).Value = "zhang yj";            
            cmd.ExecuteNonQuery();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            GVPkgHisDataBind();
        }

        private void GVPkgHisDataBind()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from gen_pkg_inv_his_v where 1=1");
            if (DdlHisType.SelectedValue!="0")
            {
                sql.Append(" and his_type='" + DdlHisType.SelectedValue + "'");
            }
            if (TxtPackageNo.Text.Trim() != "")
            {
                sql.Append(" and package_no like '" + TxtPackageNo.Text + "'");
            }
            if (TxtPartNo.Text.Trim() != "")
            {
                sql.Append(" and part_no like '" + TxtPartNo.Text + "'");
            }
            if (TxtTime.Text.Trim() != "" || TxtDateEnd.Text.Trim() != "")
            {
                if (TxtDateEnd.Text.Trim() == TxtTime.Text.Trim())
                {
                    sql.Append(string.Format(" and to_char(his_time,'yyyy-mm-dd')='{0}'", TxtTime.Text.Trim()));
                }
                else
                {
                    sql.Append(string.Format(" and to_char(his_time,'yyyy-mm-dd') >= '{0}' and to_char(his_time,'yyyy-mm-dd') < '{1}'",
                        TxtTime.Text.Trim() == "" ? "2010-01-01" : TxtTime.Text, TxtDateEnd.Text.Trim() == "" ? string.Format("{0:yyyy-MM-dd}", DateTime.Now) : TxtDateEnd.Text));

                }

            }
            
            if (TxtArea.Text.Trim() != "")
            {
                sql.Append(" and area like '" + TxtArea.Text + "'");
            }
            if (TxtLocation.Text.Trim() != "")
            {
                sql.Append(" and location like '" + TxtLocation.Text + "'");
            }
            sql.Append(" order by his_time");
            GVPkgHis.DataSource = DBHelper.createGridView(sql.ToString());
            GVPkgHis.DataBind();


        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            Response.ClearContent();

            Response.AddHeader("content-disposition", "attachment; filename=" + string.Format("{0:yyyyMMddHHmmss}", dt) + ".xls");
            Response.ContentType = "application/excel";
            Response.Write(@"<style> .TextCell {mso-number-format:\@;}</style>");

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GVPkgHis.AllowPaging = false;
            GVPkgHisDataBind();
            GVPkgHis.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

            //GVData.AllowPaging = true;

            GVPkgHisDataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void GVPkgHis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].Attributes.Add("class", "TextCell");
                e.Row.Cells[10].Attributes.Add("class", "TextCell");
            } 
        }
    }
}
