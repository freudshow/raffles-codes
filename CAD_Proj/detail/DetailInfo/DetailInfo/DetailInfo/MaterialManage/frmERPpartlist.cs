using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Framework;

namespace DetailInfo
{
    public partial class frmERPpartlist : Form
    {
        protected string Site, ProjectId, ActivitySeq,Username;
        protected ArrayList StrPartList;
        public frmERPpartlist(string LogUser,string parasite, string strProjectId,string strActivitySeq,ArrayList PartList)
        {
            Username = LogUser;
            Site = parasite;
            ProjectId = strProjectId;
            ActivitySeq = strActivitySeq;
            StrPartList = PartList;
            InitializeComponent();
        }
        private void ColumnHeadBind()
        {
            
            this.dataGridView1.Columns.Add("site", "域");
            this.dataGridView1.Columns.Add("partno", "零件编号");
            this.dataGridView1.Columns.Add("partname", "零件名称及规格");

        }
        private void ComboBind()
        {

            cmb_parttype.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_parttype.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DataSet TypeDS = PartType.PartTypeDs();

            cmb_parttype.DataSource = TypeDS.Tables[0].DefaultView;
            cmb_parttype.DisplayMember = "TYPE_DESC";
            cmb_parttype.ValueMember = "TYPEID";

        }
        private void RowBind()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < StrPartList.Count; i++)
            {
                string strOne = StrPartList[i].ToString();
                string site = strOne.Split(',')[0];
                string partno = strOne.Split(',')[1];
                string partname = strOne.Split(',')[2];
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].Cells[0].Value = site;
                this.dataGridView1.Rows[i].Cells[1].Value = partno;
                this.dataGridView1.Rows[i].Cells[2].Value = partname;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; 
            }       
        }
        private void FindErpParts(string Partno,string site)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Rows.Clear();
            DataSet ds =PartRelative.FindERPPartDataset(Partno,site,ProjectId,ActivitySeq);
            DataView dv = ds.Tables[0].DefaultView;
            int i = 1;
            foreach (DataRow dr in dv.Table.Rows)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dataGridView1);
                r.Cells[0].Value = dr[2].ToString();
                r.Cells[1].Value = dr[0].ToString();
                r.Cells[2].Value = dr[1].ToString();
                this.dataGridView1.Rows.Add(r);
                i++;
            }
        }
        private void frmERPpartlist_Load(object sender, EventArgs e)
        {
            ComboBind();
            ColumnHeadBind();
            RowBind();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string partno = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string partname = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string site = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = partno;
                txt_partname.Text = partname;
                txt_site.Text = site;
            }
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("请选择要合并的标准件编号", "Error");
                return;
            }
            if (cmb_parttype.Text.ToString() == string.Empty)
            {
                MessageBox.Show("请选择要合并的零件类型", "Error");
                return;
            }
            for (int i = 0; i < StrPartList.Count; i++)
            {
                string strOne = StrPartList[i].ToString();
                string site = strOne.Split(',')[0];
                string partno = strOne.Split(',')[1];
                string partname = strOne.Split(',')[2];    
                PartRelative pr = new PartRelative();
                pr.CREATOR = Username;
                pr.ACTIVITYSEQ =Convert.ToInt32( ActivitySeq);
                pr.ERP_PART_NO = partno;
                pr.PART_NAME = partname;
                pr.PROJECTID = ProjectId;
                pr.SITE = site;
                pr.STA_PART_NO = textBox1.Text.ToString();
                if (partno == textBox1.Text.ToString())
                    pr.STA_IF = "Y";
                else
                    pr.STA_IF = "N";
                if (pr.IFmerged1())
                {
                    if (partno != textBox1.Text.ToString())
                    {
                        MessageBox.Show("零件" + partno + "已经被合并，请确认", "Error");
                        return;
                    }
                }
                pr.STA_PART_NO = partno;
                if (pr.IFStandardPart())
                {
                    if (partno != textBox1.Text.ToString())
                    {
                    MessageBox.Show("零件" + partno + "已经是标准件，不能被合并", "Error");
                    return;
                        }
                }
                pr.STA_PART_NO = textBox1.Text.ToString();
                if (!pr.FindExistRelative())
                    pr.Add();
            }
            StandartPart sp = new StandartPart();
            sp.PART_NAME = txt_partname.Text.ToString();
            sp.PROJECTID = ProjectId;
            sp.SITE = txt_site.Text.ToString();
            sp.STA_PART_NO = textBox1.Text.ToString();
            sp.TYPEID = Convert.ToInt32(cmb_parttype.SelectedValue.ToString());
            sp.CREATOR = Username;
            if (!sp.FindExistStanPart())
                sp.Add();
            MessageBox.Show("合并成功");
            FindErpParts(textBox1.Text.ToString(), txt_site.Text.ToString());
           
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            frmSelectStnPart stnP = new frmSelectStnPart( ProjectId,Site);
            stnP.partList = this;
            stnP.ShowDialog();

        }

    }
}