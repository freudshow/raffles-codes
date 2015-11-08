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
    public partial class frmPartAdd : Form
    {
        private string ProjectId;
        private string site, sub2pro, activity, LoginUser;
        private string UserName = User.cur_user;
        private string ParaProject, ParaSubPro, parasub2project, Paraactivity= string.Empty;
        private int gboxheight =0;
        public frmPartAdd()
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
        private void frmPartAdd_Load(object sender, EventArgs e)
        {
            ProjectCmbBind();
            AddMaterialTree();
            partnocmb = this.cmb_partno;
            //MessageBox.Show(site);
            DataSet unitds = PartParameter.QueryPartPara("select name from mm_unit_tab");
            tb_unit.DataSource = unitds.Tables[0].DefaultView;
            tb_unit.DisplayMember = "name";
            tb_unit.ValueMember = "name";
            SiteCmbBind();
            //GridViewTitleBind();
            gboxheight = btn_del.Top;
            
        }
        /// <summary>
        /// 生成材料树
        /// </summary>
        private void AddMaterialTree()
        {
            TreeNode tn = new TreeNode();
            tn.Tag = "材料列表";
            //tn.Text = project.Find(ProjectId).description;
            tn.Text = "材料列表";
            this.tvMType.Nodes.Add(tn);
            AddFirstSubType(tn);
        }
        public void AddFirstSubType(TreeNode Ptn)
        {
            Ptn.Nodes.Clear();
            List<PartType> FirstType = PartType.Find1STPartType();
            foreach (PartType SP in FirstType)
            {
                TreeNode Subtn = new TreeNode();
                Subtn.Tag = SP.TYPEID;
                Subtn.Text = SP.TYPE_DESC + "(" + SP.TYPE_NO + ")";
                Subtn.ImageIndex = 4;
                Subtn.SelectedImageIndex = 4;
                List<PartType> SecondPro = PartType.Find2STPartType(SP.TYPEID);
                if (SecondPro.Count != 0)
                {
                    Ptn.Nodes.Add(Subtn);
                    foreach (PartType SSP in SecondPro)
                    {
                        TreeNode SSubtn = new TreeNode();
                        SSubtn.Name = "activity";
                        SSubtn.Tag = SSP.TYPEID;
                        SSubtn.ImageIndex = 4;
                        SSubtn.SelectedImageIndex = 4;
                        SSubtn.Text = SSP.TYPE_DESC + "(" + SSP.TYPE_NO + ")";
                        Subtn.Nodes.Add(SSubtn);
                        
                    }
                }
                else
                {
                    Subtn.Name = "activity";
                    Subtn.ImageIndex = 4;
                    Subtn.SelectedImageIndex = 4;
                    Ptn.Nodes.Add(Subtn);
                }
                Subtn.Expand();
            }
            Ptn.Expand();
        }
        private void BindPartNobyAct()
        {
            cmb_partno.Items.Clear();
            cmb_partno.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_partno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            if (activity == string.Empty) return;
            DataSet PartDS = PartType.PartNoData(activity,"07");
            DataRow row = PartDS.Tables[0].NewRow();
            row[0] = "";
            PartDS.Tables[0].Rows.InsertAt(row, 0);
            cmb_partno.DataSource = PartDS.Tables[0].DefaultView;
            cmb_partno.DisplayMember = "part_no";
            cmb_partno.ValueMember = "part_no";
            cmb_partname.Items.Clear();
            cmb_partname.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_partname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DataSet PartNameDS = PartType.PartSpecData(activity,"07");
            DataRow rowName = PartNameDS.Tables[0].NewRow();
            rowName[0] = "";
            PartNameDS.Tables[0].Rows.InsertAt(rowName, 0);
            cmb_partname.DataSource = PartNameDS.Tables[0].DefaultView;
            cmb_partname.DisplayMember = "part_spec";
            cmb_partname.ValueMember = "part_no";
        }
        private void relocation()
        {
            if (btn_del.Top == gboxheight)
            {
                btn_del.Top = btn_del.Top - btn_del.Height;
                btnsave.Top = btnsave.Top - btnsave.Height;
                btn_query.Top = btn_query.Top - btn_query.Height;
                btn_update.Top = btn_update.Top - btn_update.Height;
                btn_new.Top = btn_new.Top - btn_new.Height;
                btn_close.Top = btn_close.Top - btn_close.Height;
                btn_export.Top = btn_export.Top - btn_export.Height;

                //groupBox1.Height -= btn_export.Height;
            }
            else
            {
                btn_del.Top = btn_del.Top + btn_del.Height;
                btnsave.Top = btnsave.Top + btnsave.Height;
                btn_query.Top = btn_query.Top + btn_query.Height;
                btn_update.Top = btn_update.Top + btn_update.Height;
                btn_new.Top = btn_new.Top + btn_new.Height;
                btn_close.Top = btn_close.Top + btn_close.Height;
                btn_export.Top = btn_export.Top + btn_export.Height;
                //groupBox1.Height += btn_export.Height;
            }
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
            cmb_site.SelectedValue = "07";
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
            string PartName=cmb_partname.Text.Trim().ToString();
            string parttype = tb_type.Text.Trim().ToString();
            string partmat = tb_mat.Text.Trim().ToString();
            string partlevel = tb_level.Text.Trim().ToString();
            string partcert = tb_cert.Text.Trim().ToString();
            //string spec4 = tb_spec4.Text.Trim().ToString();
            string spec1 = tb_spec1.Text.Trim().ToString();
            string spec2 = tb_spec2.Text.Trim().ToString();
            string spec3 = tb_spec3.Text.Trim().ToString();
            string spec4 = tb_spec4.Text.Trim().ToString();
            StringBuilder sb = new StringBuilder();
            string spec_namestr = PartParameter.GetSpecName(activity);
            string pluscolum = string.Empty;
            //if (cb_showqty.Checked == true)
            //    pluscolum = ",'' 项目可用库存量,'' 项目采购数量,'' 项目预留数量";
            //if (ProjectId != string.Empty) sb.Append(" AND PROJECTID = '" + ProjectId + "'");
            if (site != string.Empty) sb.Append(" AND CONTRACT = '" + site + "'");
            if (partno != string.Empty) sb.Append(" AND part_no like '%" + partno + "%'");
            if (PartName != string.Empty) sb.Append(" AND part_spec like'%" + PartName + "%'");
            if (parttype != string.Empty) sb.Append(" AND part_type like'%" + parttype + "%'");
            if (spec1 != string.Empty) sb.Append(" AND part_spec1 like '%" + spec1 + "%'");
            if (spec2 != string.Empty) sb.Append(" AND part_spec2 like '%" + spec2 + "%'");
            if (spec3 != string.Empty) sb.Append(" AND part_spec3 like'%" + spec3 + "%'");
            if (spec4 != string.Empty) sb.Append(" AND part_spec4 like'%" + spec4 + "%'");
            if (partmat != string.Empty) sb.Append(" AND part_mat like '%" + partmat + "%'");
            if (partcert != string.Empty) sb.Append(" AND part_cert like'%" + partcert + "%'");
            if (partlevel != string.Empty) sb.Append(" AND part_level like'%" + partlevel + "%'");
            //string sqlSelect = "SELECT '','',pp.*,'' FROM  PLM.MM_PART_TAB pp WHERE 1=1 and parentid= " + activity;
            string sqlSelect = "select t.ID 序号,t.part_no 零件号,t.part_type 零件类别,t.part_spec 零件规格," +spec_namestr+
"t.part_mat 材质,t.part_cert 证书,t.part_unit 单位,t.part_unitdensity 单位密度,t.part_densityunit 密度单位,t.part_level 等级,t.parentid,t.contract 域" +
",t.supplycircle 采购周期,t.replace_code 替代码" + pluscolum + " from mm_part_tab t WHERE 1=1 and parentid= " + activity;
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql + " order by t.parentid,t.part_type";
            listviewBind(sqlSelect);
        }
        public void listviewBind(string sql)
        {
            //更改并设置列名称以及属性
            DataSet ds = PartParameter.QueryPartPara(sql);
            dgv1.DataSource = ds.Tables[0];
            //DataGridViewComboBoxColumn dgvcom = new DataGridViewComboBoxColumn();
            //dgvcom.DataSource = PartParameter.QueryPartPara("select name from mm_unit_tab").Tables[0].DefaultView;;
            //dgvcom.DisplayMember = "name";
            //dgvcom.ValueMember = "name";
            ////dgv1.Columns.Insert(1, dgvcom);
            //dgv1.GridColumnStyles[1]=dgvcom;
            //dgv1.Columns["零件认证"].Width = 100;
            //dgv1.Columns["单位密度"].ValueType = typeof();
            //dgv1.Columns["零件材质"].ValueType = typeof(float);
            dgv1.Columns["域"].ReadOnly = true;
            dgv1.Columns["序号"].ReadOnly = true;
            dgv1.Columns["零件号"].ReadOnly = true;
            //dgv1.Columns["零件类别"].ReadOnly = true;
            //dgv1.Columns["零件规格"].ReadOnly = true;
            //dgv1.Columns["材质"].ReadOnly = true;
            //dgv1.Columns["证书"].ReadOnly = true;
            //dgv1.Columns["单位"].ReadOnly = true;
            
            dgv1.Columns["parentid"].Visible = false;
            #region 将库存值填入表格
            if (cb_showqty.Checked == true)
            {
                dgv1.Columns["项目预留数量"].ReadOnly = true;
                dgv1.Columns["项目采购数量"].ReadOnly = true;
                dgv1.Columns["项目可用库存量"].ReadOnly = true;
                if (dgv1.RowCount != 0)
                {
                    ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
                    if (item == null)
                    {       
                        MessageBox.Show("请选择所查询的项目");
                        return;
                    }
                    string projectname  = item.Value;

                    for (int i = 0; i < dgv1.Rows.Count; i++)
                    {

                        string partno = dgv1.Rows[i].Cells["零件号"].Value.ToString();
                        InventoryPart part_kucun = InventoryPart.GetOnhandqty(site, partno,projectname.Substring(projectname.Length -3,3)); ;
                        if (part_kucun != null)
                        {
                            dgv1.Rows[i].Cells["项目可用库存量"].Value = part_kucun.qty_onhand;
                            dgv1.Rows[i].Cells["项目采购数量"].Value = Convert.ToDecimal(part_kucun.qty_onhand) - Convert.ToDecimal(part_kucun.qty_reserved);
                            dgv1.Rows[i].Cells["项目预留数量"].Value = part_kucun.qty_reserved;
                        }
                        else
                        {
                            dgv1.Rows[i].Cells["预估量"].Value = 0;
                            //dgv1.Rows[i].Cells[2].Value = "M";
                        }

                    }
                    //}
                }
            }
            #endregion

            //#region 设置列的只读性
            //int specnum = PartParameter.GetSpecCou(activity);
            //if (specnum != 0)
            //{
            //    if (specnum > 0)
            //    {
            //        string colstr = PartParameter.GetSpecName(activity, "1").Trim();
            //        dgv1.Columns[colstr].ReadOnly = true;
            //    }
            //    if (specnum > 1)
            //    {
            //        string colstr = PartParameter.GetSpecName(activity, "2").Trim();
            //        dgv1.Columns[colstr].ReadOnly = true;
            //    }
            //    if (specnum > 2)
            //    {
            //        string colstr = PartParameter.GetSpecName(activity, "3").Trim();
            //        dgv1.Columns[colstr].ReadOnly = true;
            //    }
            //    if (specnum > 3)
            //    {
            //        string colstr = PartParameter.GetSpecName(activity, "4").Trim();
            //        dgv1.Columns[colstr].ReadOnly = true;
            //    }
            //}
            //#endregion
           
            #region 将已设置过的预估值填入表格
            //if (dgv1.RowCount != 0)
            //{
            //    List<PartParameter> pp = PartParameter.FindPartList(activity, ProjectId,LoginUser,1);
            //    //if (pp.Count != 0)
            //    // {
            //    for (int i = 0; i < dgv1.Rows.Count; i++)
            //    {

            //        int partid = int.Parse(dgv1.Rows[i].Cells["序号"].Value.ToString());
            //        PartParameter pone = pp.Find(delegate(PartParameter bb) { return bb.ID == partid; });
            //        if (pone != null)
            //        {
            //            dgv1.Rows[i].Cells["预估量"].Value = pone.PREDICTION_QTY;
            //            //dgv1.Rows[i].Cells[2].Value = pone.UNIT;
            //        }
            //        else
            //        {
            //            dgv1.Rows[i].Cells["预估量"].Value = 0;
            //            //dgv1.Rows[i].Cells[2].Value = "M";
            //        }

            //    }
            //    //}
            //}
            #endregion



        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            //string projectid = cmb_project.SelectedValue.ToString();
            
            if (ProjectId == string.Empty)
            {
                MessageBox.Show("请选择项目！");
                return;
            }
            if (dgv1.RowCount == 0)
            {
                MessageBox.Show("请选择数据！");
                return;
            }
            string site = cmb_site.SelectedValue.ToString();
            int kqrow;
            kqrow = dgv1.RowCount - 1;
            try
            {

                DataSet unitds=PartParameter.QueryPartPara("select name from mm_unit_tab"); 
                #region 循环表,保存数据
                for (int i = 0; i <= kqrow; i++)
                {

                    string partno = dgv1.Rows[i].Cells["零件号"].Value.ToString().Trim();
                    int partid =int.Parse( dgv1.Rows[i].Cells["序号"].Value.ToString());
                    if (Part.Exist(partno, partid))
                    {
                        MessageBox.Show("第"+partid.ToString()+"行编码重复，请检查！");
                        return;
                    }
                    Part ppn = new Part();
                    string partspec = dgv1.Rows[i].Cells["零件规格"].Value.ToString().Trim();
                    string parttype = dgv1.Rows[i].Cells["零件类别"].Value.ToString().Trim();
                    string partmat = dgv1.Rows[i].Cells["材质"].Value.ToString().Trim();
                    string partlevel = dgv1.Rows[i].Cells["等级"].Value.ToString().Trim();
                    string partcert = dgv1.Rows[i].Cells["证书"].Value.ToString().Trim();
                    string partpurchase = dgv1.Rows[i].Cells["采购周期"].Value.ToString().Trim();
                    string destinyunit = dgv1.Rows[i].Cells["密度单位"].Value.ToString().Trim();
                    string replacecode = dgv1.Rows[i].Cells["替代码"].Value.ToString().Trim();
                    string spec1="",spec2="",spec3="",spec4="";
                    if(lspec1.Visible==true)
                        spec1 = dgv1.Rows[i].Cells[lspec1.Text].Value.ToString().Trim();
                    if (lspec2.Visible == true)
                        spec2 = dgv1.Rows[i].Cells[lspec2.Text].Value.ToString().Trim();
                    if (lspec3.Visible == true)
                        spec3 = dgv1.Rows[i].Cells[lspec3.Text].Value.ToString().Trim();
                    if (lspec4.Visible == true)
                        spec4 = dgv1.Rows[i].Cells[lspec4.Text].Value.ToString().Trim();
                    string partunit = dgv1.Rows[i].Cells["单位"].Value.ToString().Trim();
                    string partunitdestiny = dgv1.Rows[i].Cells["单位密度"].Value.ToString().Trim();
                    ppn.PART_NO = partno;
                    ppn.CONTRACT = site;
                    ppn.PARENTID = int.Parse(activity);
                    ppn.PART_CERT = partcert;
                    decimal temaa = decimal.Parse(partunitdestiny);
                    //temaa = decimal.Round(decimal.Parse("0.3333333"), 2);
                    ppn.PART_UNITDENSITY = decimal.Round(temaa, 2);
                    ppn.PART_LEVEL = partlevel;
                    ppn.PART_SPEC = partspec;
                    ppn.PART_SPEC1 = spec1;
                    ppn.PART_SPEC2 = spec2;
                    ppn.PART_SPEC3 = spec3;
                    ppn.PART_SPEC4 = spec4;
                    ppn.PART_DENSITYUNIT = destinyunit;
                    ppn.PART_UNIT = partunit;
                    ppn.PART_MAT = partmat;
                    ppn.PART_TYPE = parttype;
                    ppn.UPDATEDATE = DateTime.Today;
                    ppn.UPDATER = LoginUser;
                    ppn.REPLACE_CODE = replacecode;
                    if (partpurchase != string.Empty)
                        ppn.SUPPLYCIRCLE = int.Parse(partpurchase);
                    else
                        ppn.SUPPLYCIRCLE = 10;
                    ppn.ID =int.Parse( dgv1.Rows[i].Cells["序号"].Value.ToString());
                    #region 判断Unit是否是合格格式
                    if (dgv1.Rows[i].Cells["单位"].Value != null && dgv1.Rows[i].Cells["单位"].Value.ToString()!=string.Empty)
                    {
                        string punit = dgv1.Rows[i].Cells["单位"].Value.ToString().Trim().ToLower();
                        DataRow[] pone = unitds.Tables[0].Select("name ='" + punit + "'");
                        if (pone.Length != 0)
                        {
                            ppn.PART_UNIT = punit;
                        }
                        else
                        {
                            MessageBox.Show("第" + (i + 1) + "行单位名称不规范,请检查！", "错误提示");
                            dgv1.Rows[i].Selected = true;
                            return;
                        }
                    }
                    #endregion
                    int count = ppn.Update();
                    if (count == 0)
                    {
                        MessageBox.Show("第"+partid.ToString()+"行更新失败");
                    }
                   
                    

                }
                #endregion

                MessageBox.Show("保存成功!! ", "温馨提示!");
            }
            catch (Exception err)
            {

                MessageBox.Show("错误原因：" + err.Message, "错误提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            
            QuerydataBind();
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            //if (ProjectId == string.Empty)
            //{
               // MessageBox.Show("请选择项目！");
                //return;
            //}
            if (activity == string.Empty)
            {
                MessageBox.Show("请选择材料类！");
                return;
            }
            QuerydataBind();
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv1.RowCount == 0) return;
            string partid = dgv1.CurrentRow.Cells["序号"].Value.ToString();
            Part ptshow = Part.Find(partid);
            if (ptshow == null)
            {
                MessageBox.Show("此材料已删除或者不存在！！");
                return;
            }
            cmb_site.SelectedValue = ptshow.CONTRACT;
            cmb_partno.Text = ptshow.PART_NO;
            cmb_partname.Text = ptshow.PART_SPEC;
            tb_type.Text = ptshow.PART_TYPE;
            tb_mat.Text= ptshow.PART_MAT;
            tb_level.Text= ptshow.PART_LEVEL;
            tb_cert.Text = ptshow.PART_CERT;
            tb_pc.Text = ptshow.SUPPLYCIRCLE.ToString();
            tb_spec1.Text = ptshow.PART_SPEC1;
            tb_spec2.Text = ptshow.PART_SPEC2;
            tb_spec3.Text = ptshow.PART_SPEC3;
            tb_spec4.Text = ptshow.PART_SPEC4;
            tb_unit.Text = ptshow.PART_UNIT;
            tb_unitdestiny.Text = ptshow.PART_UNITDENSITY.ToString();
            txt_destinyunit.Text = ptshow.PART_DENSITYUNIT;
            txt_unitdestiny.Text = ptshow.PART_UNITDENSITY.ToString();
            //if (PartParameter.GetPartParaCou(partid, LoginUser, ProjectId) == 0)
            //{
            //    MessageBox.Show("无此材料的预估记录！");
            //    return;
            //}
            //frmParaLog frm = new frmParaLog(LoginUser, ProjectId, int.Parse(partid), "", activity);
            
            ////frm.MdiParent = this.ParentForm;
            ////frm.Show();

            ////frm.TopLevel = false;
            ////sc.Panel2.Controls.Add(frm);
            ////frm.Dock = DockStyle.Fill;
            //frm.Show();

        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int dgrow = dgv1.RowCount;
            if (dgrow == 0)
            {
                MessageBox.Show("您查询要删除的数据！");
                return;
            }
            string checkstr="",delidstr="";
            for (int i = 0; i < dgrow; i++)
            {
                if (dgv1.Rows[i].Cells["checkbox"].Value != null)
                {
                    if (dgv1.Rows[i].Cells["checkbox"].Value.ToString() == "1")
                    {
                        string partid = dgv1.Rows[i].Cells["序号"].Value.ToString();
                        delidstr = delidstr + partid + ",";
                        checkstr = "checked";
                    }
                }
            }

            if (checkstr == "")
            {
                MessageBox.Show("您还没有选择数据！");
                return;
            }
            //MessageBox.Show("",);
            string  partnolist =" ("+ delidstr.Substring(0,delidstr.Length-1)+")";
            if (Part.Delete(partnolist) > 0)
            {
                lbl_seqno.Text = string.Empty;
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
            QuerydataBind();
        }

        private void cmb_partno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //string projectid = cmb_project.SelectedValue.ToString();
                string site = cmb_site.SelectedValue.ToString();
                string PartNo = cmb_partno.Text.Trim().ToString();
                if (PartNo == string.Empty)
                {
                    MessageBox.Show("请写入零件编号");
                    return;
                }
                DataSet PartNameDS = InventoryPart.FindInvPartDataset(PartNo, site);
                DataRow row = PartNameDS.Tables[0].NewRow();
                row[0] = "";
                PartNameDS.Tables[0].Rows.InsertAt(row, 0);

                cmb_partno.DataSource = PartNameDS.Tables[0].DefaultView;
                cmb_partno.DisplayMember = "PART_NO";
                cmb_partno.ValueMember = "PART_NO";

                cmb_partname.DataSource = PartNameDS.Tables[0].DefaultView;
                
                if (PartNameDS.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("不存在此零件编号！");
                    return;
                }
                cmb_partname.DisplayMember = "DESCRIPTION";
                cmb_partname.ValueMember = "PART_NO";


                
                //InventoryPart pp = InventoryPart.FindInvInfor(partno, site);
                //if (pp != null)
                //{
                //    lbl_name.Text = pp.description;
                //    txt_unit.Text = pp.unit_meas;
                //}
                //else
                //    MessageBox.Show("不存在此零件编号！");
            }
        }

        private void cmb_partno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string site = cmb_site.SelectedValue.ToString();
            if (activity == string.Empty) return;

            String PartNo = cmb_partno.SelectedValue.ToString();

            if (PartNo != string.Empty)
            {

                DataSet PartNameDS = PartType.PartSpecDs(PartNo, site);
                cmb_partname.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_partname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb_partname.DataSource = PartNameDS.Tables[0].DefaultView;
                if (PartNameDS.Tables[0].Rows.Count == 0)
                {
                    //MessageBox.Show("不存在此零件编号！");
                    return;
                }
                cmb_partname.DisplayMember = "part_spec";
                cmb_partname.ValueMember = "part_no";
                //    //txt_unit.Text = InventoryPart.FindInvInfor(PartNo, site).unit_meas;

            }
            
        }

        private void cmb_partname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activity == string.Empty) return;
            string site = cmb_site.SelectedValue.ToString();
            DataRowView drv = cmb_partname.SelectedItem as DataRowView;
            DataRow dr = drv.Row;
            string partno = dr["PART_NO"].ToString();
            if (partno != string.Empty)
            {
                cmb_partno.Text = partno;
                //txt_unit.Text = InventoryPart.FindInvInfor(partno,site).unit_meas;
            }
            
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            lbl_seqno.Text = string.Empty;
            cmb_partno.Text = string.Empty;
            cmb_partname.Text = string.Empty;
            txt_destinyunit.Text = string.Empty;
            txt_prealert.Text = string.Empty;
            txt_preqty.Text = string.Empty;
            txt_unitdestiny.Text = string.Empty;
            tb_cert.Text = string.Empty;
            tb_mat.Text = string.Empty;
            tb_level.Text = string.Empty;
            tb_type.Text = string.Empty;
            tb_unit.SelectedValue = "";
            tb_unitdestiny.Text = string.Empty;
            txt_destinyunit.Text = string.Empty;
            tb_pc.Text = string.Empty;
            tb_spec1.Text = string.Empty; 
            tb_spec2.Text = string.Empty; 
            tb_spec3.Text = string.Empty; 
            tb_spec4.Text = string.Empty; 
            
        }

        private void cmb_partname_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    //string projectid = cmb_project.SelectedValue.ToString();
            //    string site = cmb_site.SelectedValue.ToString();
            //    string PartName =cmb_partname.Text.Trim().ToString();
            //    if (PartName == string.Empty)
            //    {
            //        MessageBox.Show("请写入零件编号");
            //        return;
            //    }
            //    DataSet PartNameDS = InventoryPart.FindInvPartInfByPartNameDataset(PartName, site);
            //    DataRow row = PartNameDS.Tables[0].NewRow();
            //    row[0] = "";
            //    PartNameDS.Tables[0].Rows.InsertAt(row, 0);

            //    cmb_partname.DataSource = PartNameDS.Tables[0].DefaultView;

            //    cmb_partname.DisplayMember = "DESCRIPTION";
            //    cmb_partname.ValueMember = "DESCRIPTION";

            //    cmb_partno.DataSource = PartNameDS.Tables[0].DefaultView;

            //    if (PartNameDS.Tables[0].Rows.Count == 0)
            //    {
            //        MessageBox.Show("不存在此零件编号！");
            //        return;
            //    }
            //    cmb_partno.DisplayMember = "PART_NO";
            //    cmb_partno.ValueMember = "PART_NO";

            //}
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //dgv1.DataSource = DataSet.Talbe[0];//重新初始化DataGridView
            if (dgv1.RowCount != 0)
            {
                for (int i = 0; i < dgv1.RowCount; i++)
                {
                    //int vint = int.Parse(dgv1.Rows[i].Cells["checkbox"].Value.ToString());
                    if(SelectAll.Checked==true)
                        dgv1.Rows[i].Cells["checkbox"].Value=1;
                    else
                        dgv1.Rows[i].Cells["checkbox"].Value = 0;
                }

            }
            
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount == 0) return;
            pb1.Visible = true;
            bool restr = PartParameter.ExportToTxt(dgv1, pb1);
            pb1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string site=cmb_site.SelectedValue.ToString();
            string partno=cmb_partno.Text.Trim().ToString();
            string partspec=cmb_partname.Text.Trim().ToString();
            string parttype = tb_type.Text.Trim().ToString();
            string partmat = tb_mat.Text.Trim().ToString();
            string partlevel = tb_level.Text.Trim().ToString();
            string partcert = tb_cert.Text.Trim().ToString();
            string purchasec = tb_pc.Text.Trim().ToString();
            string spec1 = tb_spec1.Text.Trim().ToString();
            string spec2 = tb_spec2.Text.Trim().ToString();
            string spec3 = tb_spec3.Text.Trim().ToString();
            string spec4 = tb_spec4.Text.Trim().ToString();
            string partunit = tb_unit.Text.Trim().ToString();
            string partunitdestiny = tb_unitdestiny.Text.Trim().ToString();
            string partdestinyunit = txt_destinyunit.Text.Trim().ToString();
            //if (ProjectId == string.Empty)
            //{
            //    MessageBox.Show("请选择项目！");
            //    return;
            //}
            if (partno == string.Empty)
            {
                MessageBox.Show("请填写零件编号！");
                return;
            }
            if (Part.Exist(partno))
            {
                MessageBox.Show("此零件编号已经存在，请检查！");
                return;
            }
            if (partspec == string.Empty)
            {
                MessageBox.Show("请填写零件规格！");
                return;
            }
            if (parttype == string.Empty)
            {
                MessageBox.Show("请填写零件类型！");
                return;
            }
            if (partmat == string.Empty)
            {
                MessageBox.Show("请填写零件材质！");
                return;
            }
            if (partcert == string.Empty)
            {
                MessageBox.Show("请填写零件证书！");
                return;
            }
            if (partlevel == string.Empty)
            {
                MessageBox.Show("请填写零件等级！");
                return;
            }
            try
            {

                DataSet unitds=PartParameter.QueryPartPara("select name from mm_unit_tab"); 
                //string partno = dgv1.Rows[i].Cells["零件号"].Value.ToString().Trim();
                //PartParameter pp = PartParameter.Find(ProjectId, partno, site, LoginUser);
                Part ppn = new Part();
                ppn.PART_NO = partno;
                ppn.CONTRACT = site;
                ppn.PARENTID = int.Parse(activity);
                ppn.PART_CERT = partcert;
                if (partunitdestiny!=string.Empty)
                ppn.PART_UNITDENSITY = decimal.Parse(partunitdestiny);
                if (partdestinyunit != string.Empty)
                ppn.PART_DENSITYUNIT = partdestinyunit;
                ppn.PART_LEVEL = partlevel;
                ppn.PART_SPEC = partspec;
                ppn.PART_SPEC1 = spec1;
                ppn.PART_SPEC2 = spec2;
                ppn.PART_SPEC3 = spec3;
                ppn.PART_SPEC4 = spec4;
                ppn.CREATOR = LoginUser;
                ppn.PART_UNIT = partunit;
                ppn.PART_MAT = partmat;
                ppn.PART_TYPE = parttype;
                if (purchasec != string.Empty)
                    ppn.SUPPLYCIRCLE = int.Parse(purchasec);
                else
                    ppn.SUPPLYCIRCLE = 10;
                #region 判断Unit是否是合格格式

                DataRow[] pone = unitds.Tables[0].Select("name ='" + partunit + "'");
                if (pone.Length != 0)
                {
                    ppn.PART_UNIT = partunit;
                }
                else
                {
                    MessageBox.Show("单位名称不规范,请检查！","错误提示");
                    tb_unit.Focus();
                    return;
                }
                
                #endregion
                int count = ppn.Add();
                if (count == 0)
                {
                    MessageBox.Show("添加失败");
                    return;
                }
                MessageBox.Show("保存零件成功!! ", "温馨提示!");
                QuerydataBind();
                #region
                //int cou = PartParameter.GetSpecCou(activity);
                //DataGridViewRow newrow = new DataGridViewRow();
                //dgv1.Rows.Insert(0, newrow);
                
                //newrow.CreateCells(dgv1);
                //newrow.Cells[11 + cou].Value = site;
                //if (cou > 0)
                //{
                //    newrow.Cells[4].Value = spec1;
                //}
                //if (cou > 1)
                //{
                //    newrow.Cells[5].Value = spec2;
                //}
                //if (cou > 2)
                //{
                //    newrow.Cells[6].Value = spec3;
                //}
                //if (cou > 3)
                //{
                //    newrow.Cells[7].Value = spec4;
                //}
                
                //newrow.Cells[2].Value = parttype;
                //newrow.Cells[3].Value = partspec;
                //newrow.Cells[4+cou].Value = partmat;
                //newrow.Cells[5 + cou].Value = partcert;
                //newrow.Cells[6 + cou].Value = partunit;
                ////newrow.Cells["密度单位"].Value = partunit;
                //newrow.Cells[9 + cou].Value = partlevel;
                //newrow.Cells[7 + cou].Value = partunitdestiny;
                //newrow.Cells[10 + cou].Value = activity;
                //dgv1.Rows.Add(newrow);
                #endregion
            }
            catch (Exception err)
            {

                MessageBox.Show("错误原因：" + err.Message, "错误提示信息",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tb_spec3_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tvMType_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            #region 根据不同材料设置不同的材料属性
            if (e.Node.Name == "activity")
            {
                activity =e.Node.Tag.ToString();
                int specnum = PartParameter.GetSpecCou(activity);
                if (specnum != 0)
                {
                    if (specnum > 0)
                    {
                        lspec1.Visible = true;
                        tb_spec1.Visible = true;
                        lspec2.Visible = false;
                        tb_spec2.Visible = false;
                        lspec3.Visible = false;
                        tb_spec3.Visible = false;
                        lspec4.Visible = false;
                        tb_spec4.Visible = false;
                        lspec1.Text = PartParameter.GetSpecName(activity, "1").Trim();
                        //relocation();
                    }
                    if (specnum > 1)
                    {
                        lspec2.Visible = true;
                        tb_spec2.Visible = true;
                        lspec3.Visible = false;
                        tb_spec3.Visible = false;
                        lspec4.Visible = false;
                        tb_spec4.Visible = false;
                        lspec2.Text = PartParameter.GetSpecName(activity, "2").Trim();
                        //relocation();
                    }
                    if (specnum > 2)
                    {
                        lspec3.Visible = true;
                        tb_spec3.Visible = true;
                        lspec4.Visible = false;
                        tb_spec4.Visible = false;
                        lspec3.Text = PartParameter.GetSpecName(activity, "3").Trim();
                        //relocation();
                    }
                    if (specnum > 3)
                    {
                        lspec4.Visible = true; tb_spec4.Visible = true; lspec4.Text = PartParameter.GetSpecName(activity, "4").Trim();
                    }
                }
                else
                {
                    lspec1.Visible = false;
                    tb_spec1.Visible = false;
                    lspec2.Visible = false;
                    tb_spec2.Visible = false;
                    lspec3.Visible = false;
                    tb_spec3.Visible = false;
                    lspec4.Visible = false;
                    tb_spec4.Visible = false;
                    //relocation();

                }


                
                //BindPartNobyAct();
                //cmb_partno.Text = site;
                //if (sub2pro == "")
                //{
                //    sub2pro = "03";
                //}
                //cmb_site.SelectedValue = sub2pro;
                //QuerydataBind();
            }
            #endregion
        }

        private void tvMType_AfterSelect(object sender, TreeViewEventArgs e)
        {
 
        }

        


    }
}