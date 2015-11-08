using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework;

namespace DetailInfo
{
    public partial class frmSelectStnPart : Form
    {
        public string Projectid, site;
        public frmERPpartlist partList;
        public frmSelectStnPart( string ParaPro,string paraSite)
        {
            Projectid = ParaPro;
            site = paraSite;
            InitializeComponent();
        }

        private void frmSelectStnPart_Load(object sender, EventArgs e)
        {
            txt_project.Text = project.FindName(Projectid);
            txt_site.Text = project.FindSiteName(site);
            ComboBind();
            dataGridView1.Columns.Add("type", "材料类别");
            dataGridView1.Columns.Add("partno", "材料编号");
            dataGridView1.Columns.Add("partname", "材料规格及名称");
            dataGridView1.Columns.Add("typeid", "材料类别id");
            dataGridView1.Columns[3].Visible = false;
            DataSet partDS = StandartPart.FindStnPartDataset(Projectid, site);
            DataView  dv= partDS.Tables[0].DefaultView;
            int i = 1;
            foreach (DataRow dr in dv.Table.Rows)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dataGridView1);
                r.Cells[0].Value =PartType.FindPartTypeDesc(Convert.ToInt32(  dr[0].ToString()));
                r.Cells[1].Value = dr[1].ToString();
                r.Cells[2].Value = dr[2].ToString();
                r.Cells[3].Value = dr[0].ToString();     
                this.dataGridView1.Rows.Add(r);
                i++;
            }
            //设置列表标题



        }
        private void ComboBind()
        {
            cmb_type.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_type.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DataSet TypeDS = PartType.PartTypeDs();
            DataRow dr = TypeDS.Tables[0].NewRow();
            dr[0] = 0;
            TypeDS.Tables[0].Rows.InsertAt(dr, 0);
            cmb_type.DataSource = TypeDS.Tables[0].DefaultView;

            cmb_type.DisplayMember = "TYPE_DESC";
            cmb_type.ValueMember = "TYPEID";

        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            int PartType =Convert.ToInt32( cmb_type.SelectedValue.ToString());
            string PartNo =txt_partno.Text.Trim().ToString();
            string partname =txt_partname.Text.Trim().ToString();

            StringBuilder sb = new StringBuilder();

            if (PartType != 0) sb.Append(" AND a.TYPEID = '" + PartType + "'");

            if (PartNo != string.Empty) sb.Append(" AND  STA_PART_NO like '%" + PartNo + "%'");
            if (partname != string.Empty) sb.Append(" AND PART_NAME like'%" + partname + "%'");
            string sqlSelect = "select typeid, STA_PART_NO, PART_NAME  from plm.MM_STA_PART_TAB   a where a.projectid='" + Projectid + "' and a.site='" + site + "' ";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql;
            GridviewBind(sqlSelect);
        }
        public void GridviewBind(string sql)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Rows.Clear();
            DataSet ds = MEOsub.QueryPartMiscProcList(sql);
            DataView dv = ds.Tables[0].DefaultView;
            int i = 1;
            foreach (DataRow dr in dv.Table.Rows)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dataGridView1);
                r.Cells[0].Value =PartType.FindPartTypeDesc(Convert.ToInt32( dr[0].ToString()));
                r.Cells[1].Value = dr[1].ToString();
                r.Cells[2].Value = dr[2].ToString();
                r.Cells[3].Value = dr[0].ToString();  
                this.dataGridView1.Rows.Add(r);
                i++;
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                partList.textBox1.Text = this.dataGridView1[1, e.RowIndex].Value.ToString();
                partList.txt_partname.Text = this.dataGridView1[2, e.RowIndex].Value.ToString();
                partList.txt_site.Text = site;
                
                partList.cmb_parttype.SelectedValue = this.dataGridView1[3, e.RowIndex].Value.ToString();
                this.Close();

            }
        }

    }
}