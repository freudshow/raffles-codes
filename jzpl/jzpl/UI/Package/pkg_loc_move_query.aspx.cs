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
using System.Data.OleDb;
using System.Text;

namespace jzpl.UI.Package
{
    public partial class pkg_loc_move_query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PnlData.Visible = false;
                LblMsg.Visible = false;
            }
        }

        private string getSql()
        {
            string sql = "", part_no = "", person = "", rowversion = "";
            if(txt_part_no.Text.Trim()!="")part_no=" and part_no like '%"+txt_part_no.Text.Trim()+"%'";
            if(txt_person.Text.Trim()!="")person=" and person='"+txt_person.Text.Trim()+"'";
            if (txt_arrival_date.Text.Trim() != "") rowversion = " and rowversion like '%" + txt_arrival_date.Text.Trim() + "%'";
            if (part_no + person + rowversion != "")
            {
                sql = "select * from gen_loc_mov_his_v where 1=1 " + part_no + person + rowversion;
                return sql;
            }
            else
            {
                sql = "select * from gen_loc_mov_his_v";
                return sql;
            }
        }

        private void bindGV()
        {
            string sql = getSql();
            GridView1.DataSource = DBHelper.createGridView(sql);
            GridView1.DataBind();

            if (GridView1.Rows.Count == 0)
            {
                LblMsg.Text = "未找到符合条件的数据。";
                LblMsg.Visible = true;
                PnlData.Visible = false;
            }
            else
            {
                LblMsg.Visible = false;
                PnlData.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bindGV();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindGV();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        //public void ddl_area_bind()
        //{
        //    ddl_area.DataSource = DBHelper.createDDLView("select area_id,company_id||'--'||area company_area from jp_wh_area where state='1'");
        //    ddl_area.DataValueField = "area_id";
        //    ddl_area.DataTextField = "company_area";
        //    ddl_area.DataBind();
        //}

        //protected void ddl_area_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddl_location.DataSource = DBHelper.createDDLView("select location_id,location from jp_wh_location where state='1' and area_id='" + ddl_area.SelectedValue + "' ");
        //    ddl_location.DataValueField = "location_id";
        //    ddl_location.DataTextField = "location";
        //    ddl_location.DataBind();
        //}
    }
}
