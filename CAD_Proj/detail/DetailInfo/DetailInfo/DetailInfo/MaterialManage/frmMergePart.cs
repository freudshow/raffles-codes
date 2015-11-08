using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework;
using System.Collections;

namespace DetailInfo
{
    public partial class frmMergePart : Form
    {
        string Projectid, SubPro, sub2pro, Str_activity,LogUser;
        public frmMergePart()
        {
            string Username="",  paraProjectid="",  ParaSubPro="",  parasub2project="",  Paraactivity="";
            LogUser = Username;
            Projectid = paraProjectid;
            SubPro = ParaSubPro;
            sub2pro = parasub2project;
            Str_activity = Paraactivity;
            if (Projectid == string.Empty)
            {
                //Projectid = MTypeTreeView.Str_Project;
                //SubPro = MTypeTreeView.ParentSubProjectId;
                //sub2pro = MTypeTreeView.SubProjectId;
                //Str_activity = MTypeTreeView.ActivityId;
            }
            InitializeComponent();
        }
        public void listviewTitleBind()
        {
            DataGridViewCheckBoxColumn chc = new DataGridViewCheckBoxColumn();
            chc.HeaderText = "选择";
            chc.ReadOnly = false;
            dgv1.Columns.Insert(0, chc);
            this.dgv1.Columns.Add("Id", "序号");
            this.dgv1.Columns.Add("site", "域");
            this.dgv1.Columns.Add("partno", "零件编号");
            this.dgv1.Columns.Add("partname", "零件名称及规格");
            this.dgv1.Columns.Add("unit", "计量单位");
            this.dgv1.Columns.Add("preqty", "预估用量");
            this.dgv1.Columns.Add("singleWeight", "单件重");
            this.dgv1.Columns.Add("IsMerged", "是否已被合并");
            this.dgv1.Columns.Add("IsStandard", "是否标准件");
            this.dgv1.Columns.Add("RevStnPart", "对应标准件号");
            //this.dgv1.Columns.Add("meono", "MEO NO.");
            //this.dgv1.Columns.Add("meoQty", "MEO数量");
            //this.dgv1.Columns.Add("meoPcs", "MEO件数");
            //this.dgv1.Columns.Add("reqDate", "申请日期");
            //this.dgv1.Columns.Add("restQty", "剩余数量");
            //this.dgv1.Columns.Add("rationQty", "定额量");
            this.dgv1.Visible = true;
        }
        public void QuerydataBind(string ProjectId, string SubProId, string Sub2ProId, string ActivityId, string partno, string PartName)
        {
            StringBuilder sb = new StringBuilder();
            if (ProjectId != string.Empty) sb.Append(" AND PROJECT_ID = '" + ProjectId + "'");
            if (SubProId != string.Empty) sb.Append(" and s.parent_sub_project_id='" + SubProId + "'");
            if (Sub2ProId != string.Empty) sb.Append(" and s.sub_project_id='" + Sub2ProId + "'");
            if (ActivityId != string.Empty && ActivityId != "0") sb.Append("  and a.activity_seq='" + ActivityId + "'");
            if (partno != string.Empty) sb.Append(" AND misc.part_no like '%" + partno + "%'");
            if (PartName != string.Empty) sb.Append(" AND IFSAPP.PURCHASE_PART_API.Get_Description(SITE, misc.PART_NO) like'%" + PartName + "%'");

            string sqlSelect = "select distinct  misc.site, PROJECT_ID, misc.PART_NO,       IFSAPP.PURCHASE_PART_API.Get_Description(SITE, misc.PART_NO),       IFSAPP.INVENTORY_PART_API.Get_Unit_Meas(SITE, misc.PART_NO)  from IFSAPP.PROJECT_MISC_PROCUREMENT misc  left join IFSAPP.activity a on a.activity_seq = misc.activity_seq and misc.project_id = a.project_id left join IFSAPP.sub_project s on s.sub_project_id = a.sub_project_id and s.project_id = a.project_id    WHERE 1=1 ";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql;
            listviewBind(sqlSelect);
        }
        public void listviewBind(string sql)
        {
            
            this.dgv1.AutoGenerateColumns = false;
            this.dgv1.Rows.Clear();
            DataSet ds = MEOsub.QueryPartMiscProcListEPR(sql);
            DataView dv = ds.Tables[0].DefaultView;
            int i = 1;
            foreach (DataRow dr in dv.Table.Rows)
            {

                string projectid = dr[1].ToString();
                string partno = dr[2].ToString();
                string site = dr[0].ToString();
                PartParameter pp = PartParameter.Find(0,projectid, partno, site, "weijun.qu");
                decimal preQty = 0;
                decimal singleW = 0;
                decimal preAlert = 0;
                if (pp != null)
                {
                    preQty = pp.PREDICTION_QTY;
                    singleW = pp.WEIGHT_SINGLE;
                    preAlert = pp.PREDICTION_ALERT;
                }
                PartRelative pr = new PartRelative();
                pr.ERP_PART_NO = partno;
                pr.STA_PART_NO = partno;
                pr.SITE = site;
                pr.PROJECTID = projectid;
                pr.ACTIVITYSEQ =Convert.ToInt32( Str_activity);
                string isStandPart="";
                string isMerged = "";
                if (pr.IFStandardPart())
                    isStandPart = "是";
                if (pr.IFmerged1())
                    isMerged = "是";
                string StnPartno = PartRelative.FindRelativeStnPartno(partno, projectid, Convert.ToInt32(Str_activity), site);
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dgv1);
                r.Cells[1].Value = i.ToString();
                r.Cells[2].Value = dr[0].ToString();
                r.Cells[3].Value = dr[2].ToString();
                r.Cells[4].Value = dr[3].ToString();
                r.Cells[5].Value = dr[4].ToString();
                dgv1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                r.Cells[6].Value = pp == null ? string.Empty : pp.PREDICTION_QTY.ToString();
                r.Cells[7].Value = pp == null ? string.Empty : pp.WEIGHT_SINGLE.ToString();
                r.Cells[8].Value=isMerged;
                r.Cells[9].Value = isStandPart;
                r.Cells[10].Value = StnPartno;
                //r.Cells[8].Value = MeoNo;
                //r.Cells[9].Value = MeoQty.ToString();
                //r.Cells[10].Value = Str_MeoPcs;

                //r.Cells[11].Value = Str_Meotime;
                //r.Cells[12].Value = restQty.ToString();
                //r.Cells[13].Value = MssQty.ToString();

                this.dgv1.Rows.Add(r);
                i++;
            }


        }

        private void frmRequisitionRationStatistic_Load(object sender, EventArgs e)
        {
            ComboBind();
            BindPartNobyAct();
            listviewTitleBind();

        }
        private void BindPartNobyAct()
        {
            cmb_partno.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_partno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DataSet PartDS = PartBasicInfo.PartBasicDs(Projectid, Str_activity);
            DataRow row = PartDS.Tables[0].NewRow();
            row[0] = "";
            PartDS.Tables[0].Rows.InsertAt(row, 0);
            cmb_partno.DataSource = PartDS.Tables[0].DefaultView;
            cmb_partno.DisplayMember = "part_no";
            cmb_partno.ValueMember = "part_no";

            cmb_partname.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_partname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DataSet PartNameDS = PartBasicInfo.PartNameDs(Projectid, Str_activity);
            DataRow rowName = PartNameDS.Tables[0].NewRow();
            rowName[0] = "";
            PartNameDS.Tables[0].Rows.InsertAt(rowName, 0);
            cmb_partname.DataSource = PartNameDS.Tables[0].DefaultView;
            cmb_partname.DisplayMember = "PART_NAME";
            cmb_partname.ValueMember = "PART_NAME";
        }
        private void btn_query_Click(object sender, EventArgs e)
        {

            if (Projectid == string.Empty)
            {
                MessageBox.Show("请选择项目！");
                return;
            }
            if (SubPro == string.Empty)
            {
                MessageBox.Show("请选择分系统");
                return;
            }
            if (sub2pro == string.Empty)
            {
                MessageBox.Show("请选择子系统！");
                return;
            }
            if (Str_activity == string.Empty)
            {
                MessageBox.Show("请选择专业！");
                return;
            }
            string partno = cmb_partno.Text.Trim().ToString();
            string Partname = cmb_partname.Text.Trim().ToString();
            QuerydataBind(Projectid, SubPro, sub2pro, Str_activity, partno, Partname);
        }

        //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
        //    {
        //        //Console.WriteLine( "没有点在列头上 ");

        //        int colIndex = e.ColumnIndex;
        //        string meoNo = dgv1.CurrentRow.Cells[8].Value.ToString();
        //        string site = dgv1.CurrentRow.Cells[2].Value.ToString();
        //        // string projectid = cmb_project.SelectedValue.ToString();
        //        string partno = dgv1.CurrentRow.Cells[3].Value.ToString();
        //        string RationQty = dgv1.CurrentRow.Cells[13].Value.ToString();
        //        if (colIndex == 8 && meoNo != "库存")
        //        {
        //            frmMisc_procurement_list frmPro = new frmMisc_procurement_list(site, Projectid, partno, Str_activity);
        //            frmPro.Show();

        //        }
        //        if (colIndex == 13 && RationQty != "0")
        //        {
        //            frmRation_list frmRation = new frmRation_list(site, Projectid, partno, Str_activity);
        //            frmRation.Show();
        //        }
        //    }



        //}      
        private void ComboBind()
        {
            cmb_parttype.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_parttype.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DataSet TypeDS = PartType.PartTypeDs();
            cmb_parttype.DataSource = TypeDS.Tables[0].DefaultView;
            DataRow dr = TypeDS.Tables[0].NewRow();
            dr[0] = 0;
            TypeDS.Tables[0].Rows.InsertAt(dr, 0);
            cmb_parttype.DisplayMember = "TYPE_DESC";
            cmb_parttype.ValueMember = "TYPEID";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.ColumnIndex == 0 && e.RowIndex != -1)
              {
              if (Convert.ToBoolean(dgv1.Rows[e.RowIndex].Cells[0].Value) == false)
              dgv1.Rows[e.RowIndex].Cells[0].Value = true;
              else
              dgv1.Rows[e.RowIndex].Cells[0].Value = false;
              }
        }

        private void btn_merge_Click(object sender, EventArgs e)
        {
            int count=0;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                if ((bool)dgv1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                MessageBox.Show("请至少选择一条数据。", "提示");
                return;
            }
            else
            {
                ArrayList partList =new ArrayList();
                string site = string.Empty;
                for (int j = 0; j < this.dgv1.Rows.Count; j++)
                {
                   
                    if ((bool)dgv1.Rows[j].Cells[0].EditedFormattedValue == true)
                    {
                        if (site != string.Empty && site != dgv1.Rows[j].Cells[2].Value.ToString())
                        {
                            MessageBox.Show("域必须相同，请确认。", "提示");
                            return;
                        }
                         site = dgv1.Rows[j].Cells[2].Value.ToString();

                        string erp_partno = dgv1.Rows[j].Cells[3].Value.ToString();
                        string erp_partname = dgv1.Rows[j].Cells[4].Value.ToString();
                        partList.Add(site+","+ erp_partno + "," + erp_partname);
                  
                    }
                }
                frmERPpartlist Frmerpparlist = new frmERPpartlist(LogUser, site, Projectid, Str_activity, partList);
                Frmerpparlist.ShowDialog();

            }
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            if (groupBox3.Visible == false)
            {
                groupBox3.Visible = true;
            }
            if (cmb_parttype.Text == string.Empty)
            {
                MessageBox.Show("请选择材料种类。", "提示");
                return;
            }
            int count=0;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                if ((bool)dgv1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                MessageBox.Show("请至少选择一条数据。", "提示");
                return;
            }
            else
            {
                ArrayList partList =new ArrayList();
                string site = string.Empty;
                for (int j = 0; j < this.dgv1.Rows.Count; j++)
                {
                   
                    if ((bool)dgv1.Rows[j].Cells[0].EditedFormattedValue == true)
                    {
                       site = dgv1.Rows[j].Cells[2].Value.ToString();

                        string erp_partno = dgv1.Rows[j].Cells[3].Value.ToString();
                        string erp_partname = dgv1.Rows[j].Cells[4].Value.ToString();
                        //partList.Add(site+","+ erp_partno + "," + erp_partname);
                        PartRelative pr = new PartRelative();
                        pr.CREATOR = LogUser;
                        pr.ACTIVITYSEQ = Convert.ToInt32(Str_activity);
                        pr.ERP_PART_NO = erp_partno;
                        pr.PART_NAME = erp_partname;
                        pr.PROJECTID = Projectid;
                        pr.SITE = site;
                        pr.STA_PART_NO = erp_partno; 
                        pr.STA_IF = "Y";

                        if (pr.IFmerged1())
                        {
                            MessageBox.Show("零件" + erp_partno + "已经被合并，请确认", "Error");
                            return;
                        }
                        pr.STA_PART_NO = erp_partno;
                        if (pr.IFStandardPart())
                        {

                            MessageBox.Show("零件" + erp_partno + "已经是标准件，不能被合并", "Error");
                            return;

                        }
                        pr.STA_PART_NO = erp_partno;
                        if (!pr.FindExistRelative())
                            pr.Add();
                        StandartPart sp = new StandartPart();
                        sp.PART_NAME = erp_partname;
                        sp.PROJECTID = Projectid;
                        sp.SITE =site;
                        sp.STA_PART_NO = erp_partno;
                        sp.TYPEID = Convert.ToInt32(cmb_parttype.SelectedValue.ToString());
                        sp.CREATOR = LogUser;
                        if (!sp.FindExistStanPart())
                            sp.Add();
                    }                   
                    
                   // FindErpParts(textBox1.Text.ToString(), txt_site.Text.ToString());
                  
                    }
                    MessageBox.Show("选中零件已经成功复制到标准零件库");
                }
        }

        private void btn_release_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                if ((bool)dgv1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                MessageBox.Show("请至少选择一条数据。", "提示");
                return;
            }
            else
            {
                string site = string.Empty;
                for (int j = 0; j < this.dgv1.Rows.Count; j++)
                {

                    if ((bool)dgv1.Rows[j].Cells[0].EditedFormattedValue == true)
                    {
                        site = dgv1.Rows[j].Cells[2].Value.ToString();

                        string erp_partno = dgv1.Rows[j].Cells[3].Value.ToString();
                        string erp_partname = dgv1.Rows[j].Cells[4].Value.ToString();
                        //partList.Add(site+","+ erp_partno + "," + erp_partname);
                        PartRelative pr = new PartRelative();
                        pr.CREATOR = LogUser;
                        pr.ACTIVITYSEQ = Convert.ToInt32(Str_activity);
                        pr.ERP_PART_NO = erp_partno;
                        pr.PART_NAME = erp_partname;
                        pr.PROJECTID = Projectid;
                        pr.SITE = site;
                        if (pr.IFmerged1())
                            pr.Delete();

                    }

                }
                MessageBox.Show("绑定关系解除成功");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}