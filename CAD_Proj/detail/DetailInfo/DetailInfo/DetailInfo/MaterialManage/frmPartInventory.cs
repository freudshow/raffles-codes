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
    public partial class frmPartInventory : Form
    {
        private string ProjectId;
        private string site, sub2pro, activity, LoginUser;
        private string UserName = User.cur_user;
        private string ParaProject, ParaSubPro, parasub2project, Paraactivity= string.Empty;
        private int gboxheight =0;
        public frmPartInventory()
        {
            InitializeComponent();
            ProjectId = ParaProject;
            site = ParaSubPro;
            sub2pro = parasub2project;
            activity = Paraactivity;
            if (ProjectId == string.Empty)
            {
            //    ProjectId = NTypeTreeView.Str_Project;
            ////    site = NTypeTreeView.ParentSubProjectId;
            ////    sub2pro = NTypeTreeView.SubProjectId;
            //    activity = NTypeTreeView.ActivityId;
            }
            LoginUser = UserName;
        }
        public static ComboBox partnocmb = null;
        private void frmPartInventory_Load(object sender, EventArgs e)
        {
            ProjectCmbBind();
            
            partnocmb = this.cmb_partno;
            //MessageBox.Show(site);
            DataSet unitds = PartParameter.QueryPartPara("select name from mm_unit_tab");
            SiteCmbBind();
            //GridViewTitleBind();
            
            
        }
        
        
        /// <summary>
        /// 获取ERP域列表
        /// </summary>
        public void SiteCmbBind()
        {

            cmb_site.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_site.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_site.Items.Clear();
            DataSet PartDS = project.FindSiteDataset();
            DataRow rowdim = PartDS.Tables[0].NewRow();
            rowdim[0] = "";
            PartDS.Tables[0].Rows.InsertAt(rowdim, 0);
            cmb_site.DataSource = PartDS.Tables[0].DefaultView;
            cmb_site.DisplayMember = "CONTRACT_REF";
            cmb_site.ValueMember = "CONTRACT";
            cmb_site.SelectedValue = "03";
        }
        /// <summary>
        /// 获取ERP中项目列表
        /// </summary>
        public void ProjectCmbBind()
        {
            cmb_project.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_project.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_project.Items.Clear();
            DataSet PartDS = project.FindProDataset();
            DataTable dt = PartDS.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                ProjectCmbItem item = new ProjectCmbItem(row["description"].ToString(), row["project_id"].ToString());
                cmb_project.Items.Add(item);
            }
            //ProjectCmbItem itemn = new ProjectCmbItem("COSLProspector 半潜式钻井平台", "YCRO11-256");
            ////cmb_project.SelectedIndex = 7;
            //cmb_project.SelectedItem = itemn;

        }
        /// <summary>
        /// 查找材料以及库存
        /// </summary>
        public void QuerydataBind()
        {
            //string ProjectId = cmb_project.SelectedValue.ToString();
            site=cmb_site.SelectedValue.ToString();
            string partno=cmb_partno.Text.Trim().ToString();
            string parttype = tb_type.Text.Trim().ToString();
            string projectname = string.Empty ;
            string designcode = tb_designcode.Text.Trim();
            ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
            if (item != null)
            {
                projectname = item.Value;
            }
            else
            {
                MessageBox.Show("请选择项目！", "查询提醒");
                return;
            }
            if (string.IsNullOrEmpty(partno) && string.IsNullOrEmpty(parttype))
            {
                MessageBox.Show("请输入零件编码或者零件描述！","查询提醒");
                return;
            }
            string sprojectname = projectname.Substring(projectname.Length - 3, 3);
            StringBuilder sb = new StringBuilder();
            if (site != string.Empty) sb.Append(" AND CONTRACT = '" + site + "'");
            if (partno != string.Empty) sb.Append(" AND part_no like '" + partno + "%'");
            if (parttype != string.Empty) sb.Append(" AND description like'" + parttype + "%'");
            //if (designcode != string.Empty) sb.Append(" AND design_code like'" + designcode + "%'");
            //string sqlSelect = "SELECT '','',pp.*,'' FROM  PLM.MM_PART_TAB pp WHERE 1=1 and parentid= " + activity;
            string sqlSelect = "select p.contract 域,p.part_no 零件号,p.description 零件描述,p.unit_meas 单位,nvl((select sum(tt.qty_onhand - tt.qty_reserved) from ifsapp.yr_inv_on_hand_vw tt WHERE tt.part_no = p.part_no and tt.contract = '03' and tt.req_dept like 'YL" + sprojectname + "%'), 0) 项目预留可用,nvl((select sum(REQUIRE_QTY ) from IFSAPP.PROJECT_MISC_PROCUREMENT where design_code like '%" + designcode + "%' and PROJECT_ID = '" + projectname + "'  and site = '03' and  issue_from_inv = 0 and PART_NO = p.part_no  and (select state from ifsapp.purchase_req_line_part q where q.requisition_no =p_requisition_no and q.part_no=p.part_no) <>'Cancelled' ),0) 申请数量,nvl((select sum(IFSAPP.PROJ_PROCU_RATION_API.Get_Accu_Ration_Qty(MATR_SEQ_NO)) from IFSAPP.PROJECT_MISC_PROCUREMENT where design_code like '%" + designcode + "%' and  PROJECT_ID = '" + projectname + "'  and site = '03' and  issue_from_inv = 0 and PART_NO = p.part_no and (select state from ifsapp.purchase_req_line_part q where q.requisition_no =p_requisition_no and q.part_no=p.part_no) <>'Cancelled'),0) 已下发数量,nvl((select sum(REQUIRE_QTY -IFSAPP.PROJ_PROCU_RATION_API.Get_Accu_Ration_Qty(MATR_SEQ_NO)) from IFSAPP.PROJECT_MISC_PROCUREMENT where design_code like '%" + designcode + "%' and  PROJECT_ID = '" + projectname + "'  and site = '03' and  issue_from_inv = 0 and PART_NO = p.part_no and (select state from ifsapp.purchase_req_line_part q where q.requisition_no =p_requisition_no and q.part_no=p.part_no) <>'Cancelled'),0) 余下申请数量,nvl((select sum(tt.qty_onhand ) from ifsapp.yr_inv_on_hand_vw tt WHERE tt.part_no = p.part_no and tt.contract = '03' and tt.req_dept like 'YL" + sprojectname + "%'), 0) 项目预留  from ifsapp.inventory_part p where  p.part_status='A' ";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql + "order by p.part_no";
            listviewBind(sqlSelect);
        }
        public void listviewBind(string sql)
        {
            //更改并设置列名称以及属性
            DataSet ds = PartParameter.QueryPartERPInventory(sql);
           // dgv1.DataSource = ds.Tables[0];
            //DataGridViewComboBoxColumn dgvcom = new DataGridViewComboBoxColumn();
            //dgvcom.DataSource = PartParameter.QueryPartPara("select name from mm_unit_tab").Tables[0].DefaultView;;
            //dgvcom.DisplayMember = "name";
            //dgvcom.ValueMember = "name";
            ////dgv1.Columns.Insert(1, dgvcom);
            //dgv1.GridColumnStyles[1]=dgvcom;
            //dgv1.Columns["零件认证"].Width = 100;
            //dgv1.Columns["单位密度"].ValueType = typeof();
            //dgv1.Columns["零件材质"].ValueType = typeof(float);
            if (ds != null)
            {
                this.dgv1.AutoGenerateColumns = false;
                this.dgv1.Rows.Clear();
                this.dgv1.Columns.Clear();
                this.dgv1.Columns.Add("Id", "序号");
                this.dgv1.Columns.Add("域", "域");
                this.dgv1.Columns.Add("零件号", "零件号");
                this.dgv1.Columns.Add("零件描述", "零件描述");
                this.dgv1.Columns.Add("申请数量", "申请数量");
                this.dgv1.Columns.Add("已下发数量", "已下发数量");
                this.dgv1.Columns.Add("余下申请数量", "余下申请数量");
                this.dgv1.Columns.Add("项目预留可用", "项目预留可用");
                this.dgv1.Columns.Add("项目总的数量", "项目总的数量");
                this.dgv1.Columns.Add("单位", "单位");
                dgv1.Columns["Id"].ReadOnly = true;
                dgv1.Columns["域"].ReadOnly = true;
                dgv1.Columns["零件号"].ReadOnly = true;
                dgv1.Columns["申请数量"].ReadOnly = true;
                dgv1.Columns["已下发数量"].ReadOnly = true;
                dgv1.Columns["余下申请数量"].ReadOnly = true;
                dgv1.Columns["项目预留可用"].ReadOnly = true;
                dgv1.Columns["项目总的数量"].ReadOnly = true;
                dgv1.Columns["单位"].ReadOnly = true;
                //dgv1.Columns["单位"].ReadOnly = true;

                dgv1.Columns["零件描述"].ReadOnly = true;

                DataView dv = ds.Tables[0].DefaultView;
                int i = 1;
                foreach (DataRow dr in dv.Table.Rows)
                {
                    DataGridViewRow r = new DataGridViewRow();
                    r.CreateCells(dgv1);
                    r.Cells[0].Value = i.ToString();
                    r.Cells[1].Value = dr[0].ToString();
                    r.Cells[2].Value = dr[1].ToString();
                    r.Cells[3].Value = dr[2].ToString();
                    r.Cells[4].Value = dr[5].ToString();
                    r.Cells[5].Value = dr[6].ToString();
                    r.Cells[6].Value = dr[7].ToString();
                    r.Cells[7].Value = dr[4].ToString();
                    r.Cells[8].Value = Convert.ToDecimal(dr[4].ToString()) +Convert.ToDecimal(dr[7].ToString());
                    r.Cells[9].Value = dr[3].ToString();
                    if (Convert.ToDecimal(dr[5].ToString()) + Convert.ToDecimal(dr[8].ToString()) != 0)
                    {
                        this.dgv1.Rows.Add(r);
                        i++;
                    }
                }

            }
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            
            QuerydataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount == 0) return;
            pb1.Visible = true;
            bool restr = PartParameter.ExportToTxt(dgv1, pb1);
            pb1.Visible = false;
        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            if (dgv1.RowCount > 0)
            {
                foreach (Form form in this.ParentForm.MdiChildren)
                {
                    if (form.Text == "材料维护页面")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                string project_name = "";
                string site_no = dgv1.CurrentRow.Cells["域"].Value.ToString();
                string partno = dgv1.CurrentRow.Cells["零件号"].Value.ToString();
                string partname = dgv1.CurrentRow.Cells["零件描述"].Value.ToString();
                string designcode = tb_designcode.Text.Trim();
                ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
                if (item != null)
                {
                    project_name = item.Value;
                }
                else
                {
                    MessageBox.Show("请选择项目！", "查询提醒");
                    return;
                }
                DetailInfo.MaterialManage.MaterialDetail detailform = new DetailInfo.MaterialManage.MaterialDetail(project_name, partno, partname,site_no,designcode);
                detailform.Text = "材料详情页面";
                detailform.MdiParent = this.MdiParent;
                detailform.Show();

            }
            else
            {
                return;
            }
        }


        


    }
}