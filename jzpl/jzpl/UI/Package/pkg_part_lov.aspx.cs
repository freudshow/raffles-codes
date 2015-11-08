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

namespace jzpl.UI.Package
{
    public partial class pkg_arrival_xj : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TxtDBbh.Text = Request.QueryString["dbbh"].ToString();
                XJGrid.DataSource = null;
                XJGrid.DataBind();
            }
        }
        private string getSQL()
        {
            string SQL = "", XJBH = "", XJMC = "", HTH = "", GDH = "",XJGG="", PONO="";
            if (TxtXJbh.Text != "") XJBH = " and part_no like '"+TxtXJbh.Text.Trim()+"'";
            if (TxtXJmc.Text != "") XJMC=" and (part_name like '" + TxtXJmc.Text + "' or part_name_e like '"+TxtXJmc.Text+"')";
            if (TxtXJgg.Text != "") XJGG = " and part_spec like '" + TxtXJgg.Text + "'";
            if (TxtHTno.Text != "") HTH = " and contract_no like '" + TxtHTno.Text + "'";
            if (TxtGDno.Text != "") GDH = " and dec_no like '" + TxtGDno.Text + "'";
            if (TxtPoNo.Text.Trim() != "") PONO = " and po_no='" + TxtPoNo + "'";
            SQL = "select * from gen_part_package_item where package_no='" + TxtDBbh.Text + "'" + XJBH + XJMC + XJGG + HTH + GDH + PONO;
            return SQL;
        }
        protected void BtnResult_Click(object sender, EventArgs e)
        {
            DataView view = DBHelper.createGridView(this.getSQL());
            XJGrid.DataSource = view;
            XJGrid.DataBind();           
        }
    }
}
