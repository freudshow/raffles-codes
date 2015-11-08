using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework;
using DetailInfo;
namespace DetailInfo.MaterialManage
{
    public partial class MaterialRationAdd : Form
    {
        private TreeNode tn;
        string ProjectId = string.Empty, ecprojectid = "";
        public string MEOID = string.Empty, MEOSTATE = string.Empty, ActivityName = string.Empty, Str_Project = string.Empty, Str_Discipline = string.Empty, Str_Area = string.Empty, ActivityId, Str_FX = string.Empty;
        public MaterialRationAdd(string cur_meoid, string cur_state)
        {
            InitializeComponent();
            MEOID = cur_meoid;
            MEOSTATE = cur_state;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void MaterialRationAdd_Load(object sender, EventArgs e)
        {
            ProjectCmbItem.ProjectCmbBind(cmb_project);
            //ProjectCmbItem.ReasonCmbBind(cmb_reason);
            ProjectCmbItem.SiteCmbBind(cmb_site2);
            ProjectCmbItem.SiteCmbBind(cmb_site); 
            ProjectCmbItem.BindDiscipline(cmb_discipline);
            ProjectCmbItem.BindDiscipline(cmb_discipline2);
            ecprojectid = ProjectSystem.FindProjectid(ProjectId).ToString();
            
            UnitCmbBind();
            ReasonCmbBind();
            listviewTitleBind();
            if (MEOID == "0")
            {
                btn_save.Enabled = true;
                btn_select.Enabled = true;
            }
            else
            {
                if (MEOSTATE == "1")
                {
                    btn_save.Enabled = true;
                    btn_select.Enabled = true;
                }
            }
        }
        
        /// <summary>
        /// 获取ERP中的计量单位列表
        /// </summary>
        public void UnitCmbBind()
        {
            //cmb_unit.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cmb_unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmb_unit.Items.Clear();
            //DataSet PartDS =Unit.FindUnitDataset();
            //cmb_unit.DataSource = PartDS.Tables[0].DefaultView;
            //cmb_unit.DisplayMember = "UNIT_CODE";
            //cmb_unit.ValueMember = "UNIT_CODE";
        }

        /// <summary>
        /// 获取ERP中的原因代码
        /// </summary>
        public void ReasonCmbBind()
        {
            //cmb_reason.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cmb_reason.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmb_reason.Items.Clear();
            DataGridViewComboBoxColumn dgvcom1 = new DataGridViewComboBoxColumn();
            DataSet PartDS = ReasonCode.FindReasonDataset();
            dgvcom1.DataSource = PartDS.Tables[0].DefaultView;
            dgvcom1.HeaderText = "需求原因";
            dgvcom1.Name = "需求原因";
            dgvcom1.DisplayMember = "DESCRIPTION";
            dgvcom1.ValueMember = "DESCRIPTION";
            //dgvcom1.sle = "正常申请";
            dgv1.Columns.Add(dgvcom1);
            //cmb_reason.DataSource = PartDS.Tables[0].DefaultView;
            //cmb_reason.DisplayMember = "DESCRIPTION";
            //cmb_reason.ValueMember = "REASON_CODE";
            //cmb_reason.SelectedValue = "1001";
        }
        public void listviewTitleBind()
        {

            dgv1.AutoGenerateColumns = false;
            if (MEOID != "0")
            {
                //tn_meono.Enabled = true;

                MEOmain meolist = MEOmain.Find(int.Parse(MEOID));
                if (meolist != null)
                {
                    //tn_meono.Text = meolist.REQUIRE_NO;
                    cmb_site.SelectedValue = meolist.CONTRACT;
                    //tb_project.Text = meolist.PROJECT_ID;
                    //tb_user.Text = meolist.CREATER;
                    //txt_designcode.Text = meolist.INFORMATION;
                    //txt_remark.Text = meolist.REMARK;
                    //dateTimePicker1.Value = meolist.CREATE_DATE;
                    cmb_discipline.SelectedValue = meolist.DISCIPLINEID;
                    List<MEOsub> meosublist = MEOsub.FindMEOList(MEOID);
                    if (meosublist.Count != 0)
                    {
                        for (int i = 0; i < meosublist.Count; i++)
                        {
                            dgv1.Rows.Add(1);
                            dgv1.Rows[i].Cells["MEO_ID"].Value = MEOID;
                            dgv1.Rows[i].Cells["零件号"].Value = meosublist[i].PART_NO;
                            dgv1.Rows[i].Cells["申请数量"].Value = meosublist[i].REQUIRE_QTY;
                            dgv1.Rows[i].Cells["需求日期"].Value = meosublist[i].REQUIRE_DATE;
                            dgv1.Rows[i].Cells["需求原因"].Value = meosublist[i].REASON_CODE;
                            dgv1.Rows[i].Cells["MEO_ERP"].Value = meosublist[i].MEO_ERP;
                            dgv1.Rows[i].Cells["零件描述"].Value = meosublist[i].PART_NAME;
                            dgv1.Rows[i].Cells["单位"].Value = meosublist[i].UNIT_MEAS;
                            //dgv1.Rows[i].Cells["单位"].Value = meosublist[i].UNIT_MEAS;
                            //dgv1.Rows[i].Cells["inventory_qty"].Value = GetInventory(pp.PART_NO, pp.CONTRACT);
                            //dgv1.Rows[i].Cells["meo_qty"].Value = GetMEOqty(pp.ID, ecprojectid);
                            //dgv1.Rows[i].Cells["prediction_qty"].Value = GetParaqty(pp.ID, ecprojectid);
                            
                        }
                    }
                }
            }
            else
            {
                dgv1.Rows.Add(5);
                for (int i = 0; i < 5; i++)
                {
                    dgv1.Rows[i].Cells["MEO_ID"].Value = MEOID;
                    dgv1.Rows[i].Cells["需求原因"].Value = "正常申请";
                }
            }
            SetRowNo();

        }
        /// <summary>
        /// 产生手工MEO号
        /// </summary>
        /// <returns></returns>
        private string MakeMEOManual(int dispname)
        {

            string ecproject = project.FindECDMSID(ProjectId);
            //int dispname;
            string pjstr, dispstr, odstr;
            odstr = MEOMSS_discipline_new.GetManualNo(dispname, Convert.ToInt32(ecproject));
            int cid = MEOMSS_discipline_new.UPManualNo(dispname, Convert.ToInt32(ecproject));
            for (int i = odstr.Length; i < 6; i++)
            {
                odstr = "0" + odstr;
            }

            pjstr = ProjectId;
            //取出二级部门ID
            //dpstr = leaveinfor.GetDeptENName(pd.Department_Id, 2);
            dispstr = MEOMSS_discipline_new.GetDPENname(dispname);
            string MEO_MANUAL = "MEO" + "-" + pjstr + "-" + dispstr + "-" + odstr;
            return MEO_MANUAL;
        }
        /// <summary>
        /// 设置行号
        /// </summary>
        private void SetRowNo()
        {
            if (dgv1.RowCount != 0)
            {
                for (int i = 0; i < dgv1.RowCount; i++)
                    dgv1.Rows[i].Cells["行"].Value = i + 1;
            }
        }
        /// <summary>
        /// 更换项目时重新构建项目结构树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvMType.Nodes.Clear();
            tn = new TreeNode();
            ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择项目", "提示");
                return;
            }
            ProjectId = item.Value;
            tn.Tag = ProjectId;
            project p = project.Find(ProjectId);
            if (p != null)
            {
                tn.Text = p.description;
                tn.ImageIndex = 3;
                tn.SelectedImageIndex = 3;
            }
            else
            {
                tn.Text = "项目";
            }
            this.tvMType.Nodes.Add(tn);
            ecprojectid = ProjectSystem.FindProjectid(ProjectId).ToString();
            DataTable dt = new DataTable();
            DataTable dtact = new DataTable();
            //int projectid = ProjectSystem.FindProjectid(ProjectId);
            dt = SubProject.FindAllSubPro(ProjectId).Tables[0];
            dtact = Activity.FindActivityDs(ProjectId).Tables[0];
            //string virroot = ProjectSystem.GetProName(projectID);
            CreateTreeViewRecursiveNew(tn, dt, dtact, "0");
            ProjectCmbItem.BlockBind(cmb_block, ecprojectid);



        }
        public void CreateTreeViewRecursiveNew(TreeNode nodes, DataTable dataSource, DataTable dataAct, string parentId)
        {

            DataView dv = new DataView(dataSource);
            if (parentId == "0")
                dv.RowFilter = "parent_sub_project_id='0'";
            else
                dv.RowFilter = "parent_sub_project_id='" + parentId + "'";
            foreach (DataRowView dr in dv)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["sub_project_id"].ToString() + " " + dr["description"].ToString();
                node.Tag = dr["sub_project_id"].ToString();
                node.Name = dr["description"].ToString();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                nodes.Nodes.Add(node);
                DataView dv1 = new DataView(dataAct);
                dv1.RowFilter = "sub_project_id='" + dr["sub_project_id"].ToString() + "'";
                foreach (DataRowView act in dv1)
                {
                    TreeNode tn_act = new TreeNode();
                    tn_act.Name = "activity";
                    tn_act.Tag = act["activity_seq"].ToString();
                    tn_act.Text = act["activity_no"].ToString() + " " + act["description"].ToString();
                    tn_act.SelectedImageIndex = 4;
                    tn_act.ImageIndex = 4;
                    node.Nodes.Add(tn_act);
                }
                CreateTreeViewRecursiveNew(node, dataSource, dataAct, dr["sub_project_id"].ToString());

            }
            if (parentId == "0")
                nodes.Expand();
        }
        /// <summary>
        /// 选择最后的材料种类时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvMType_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "activity")
            {

                Str_Project = ProjectId;
                Str_Discipline = e.Node.Parent.Name.ToString();
                Str_FX = e.Node.Parent.Parent.Name.ToString();
                Str_Area = e.Node.Parent.Parent.Parent.Name.ToString();
                ActivityId = e.Node.Tag.ToString();
                string tempstr = e.Node.Text.ToString();
                string[] actlist = tempstr.Split(' ');
                ActivityName = tempstr.Substring(actlist[0].Length + 1);
                //tmpshow.Text = Str_Area + "\\" + Str_FX + "\\" + Str_Discipline + "\\" + ActivityName;
                #region 新增材料申请
                if (tabControl1.SelectedIndex == 0)
                {
                    tb_area.Text = Str_Area;
                    tb_fx.Text = Str_FX;
                    tb_discipline.Text = Str_Discipline;
                    tb_type.Text = ActivityName;
                    btn_save.Enabled = true;
                    btn_select.Enabled = true;
                }
                #endregion

                #region 材料申请汇总与更新
                if (tabControl1.SelectedIndex == 1)
                {
                    textBox5.Text = Str_Area;
                    textBox4.Text = Str_FX;
                    textBox2.Text = Str_Discipline;
                    textBox3.Text = ActivityName;
                    //btn_save.Enabled = true;
                    dgv2.DataSource=null;
                    //dgv2.Rows.Clear();
                    //btn_select.Enabled = true;
                }
                #endregion

            }
        }
        /// <summary>
        /// 点击Dgv前面的X号可以删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MEOSTATE == "1")
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewColumn column = dgv1.Columns[e.ColumnIndex];
                    if (column is DataGridViewImageColumn)
                    {
                        //这里可以编写你需要的任意关于按钮事件的操作~
                        dgv1.Rows.Remove(dgv1.CurrentRow);
                        //MessageBox.Show("按钮被点击");
                    }
                }
                SetRowNo();
            }
        }
        /// <summary>
        /// 取得零件库存
        /// </summary>
        public string GetInventory(string partNo, string Site)
        {
            //partNo=cmb_partno.SelectedValue.ToString();
            //Site = cmb_site.SelectedValue.ToString();
            //string isInv = InventoryPart.GetIsInventory(Site, partNo);
            //if (isInv == "1")
            //    checkBox1.Checked = true;
            //else
            //    checkBox1.Checked = false;
            string invQty = InventoryPart.GetInventoryqty(Site, partNo);
            return invQty;
        }
        /// <summary>
        /// 取得预估重量
        /// </summary>
        public decimal GetParaqty(int partid, string pid)
        {
            //partNo=cmb_partno.SelectedValue.ToString();
            //Site = cmb_site.SelectedValue.ToString();
            //string isInv = InventoryPart.GetIsInventory(Site, partNo);
            //if (isInv == "1")
            //    checkBox1.Checked = true;
            //else
            //    checkBox1.Checked = false;
            decimal invQty = PartParameter.FindPartParaSum(partid, pid);
            return invQty;
        }
        /// <summary>
        /// 取得已下MEO的数量
        /// </summary>
        public decimal GetMEOqty(int partid, string pid)
        {
            //partNo=cmb_partno.SelectedValue.ToString();
            //Site = cmb_site.SelectedValue.ToString();
            //string isInv = InventoryPart.GetIsInventory(Site, partNo);
            //if (isInv == "1")
            //    checkBox1.Checked = true;
            //else
            //    checkBox1.Checked = false;
            decimal invQty = MEOmain.GetMEOqty(partid, pid);
            return invQty;
        }
        /// <summary>
        /// 控制申请数量只能填写数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int i = e.ColumnIndex;
                DataGridViewColumn column = dgv1.Columns[e.ColumnIndex];
                if (column.Name == "申请数量")
                {
                    if (dgv1.Rows[e.RowIndex].Cells[i].Value != null)
                    {
                        string rqty = dgv1.Rows[e.RowIndex].Cells[i].Value.ToString();
                        if (BaseClass.validateNum(rqty) == false)
                        {
                            MessageBox.Show("请输入数字!!!", "提示");
                            dgv1.Rows[e.RowIndex].Cells[i].Value = "";
                        }

                    }

                }
                else if (column.Name == "零件号")
                {
                    if (dgv1.Rows[e.RowIndex].Cells[i].Value != null)
                    {
                        string rqty = dgv1.Rows[e.RowIndex].Cells[i].Value.ToString();
                        string Site = cmb_site.SelectedValue.ToString();
                        if (ProjectId == string.Empty)
                        {
                            MessageBox.Show("请选择项目");
                            return;
                        }
                        if (Str_Area == string.Empty)
                        {
                            MessageBox.Show("请选择区域");
                            return;
                        }
                        if (ActivityName == string.Empty)
                        {
                            MessageBox.Show("请选择分项");
                            return;
                        }
                        InventoryPart invpartnew = InventoryPart.FindInvInfor(rqty, Site);
                        if (invpartnew != null)
                        {
                            dgv1.Rows[e.RowIndex].Cells[i].Value = invpartnew.PART_NO;
                            dgv1.Rows[e.RowIndex].Cells[i + 1].Value = invpartnew.description;
                            dgv1.Rows[e.RowIndex].Cells["单位"].Value = invpartnew.unit_meas;

                        }
                        else
                        {
                            MessageBox.Show("无此材料编码");
                            return;
                        }
                        InventoryPart invpart = InventoryPart.GetRequiredqty(Site, rqty, ProjectId);
                        if (invpart != null)
                        {
                            dgv1.Rows[e.RowIndex].Cells["reserved_qty"].Value = invpart.qty_reserved;
                            dgv1.Rows[e.RowIndex].Cells["total_qty"].Value = invpart.qty_onhand;
                            dgv1.Rows[e.RowIndex].Cells["meo_qty"].Value = invpart.qty_issued;
                            dgv1.Rows[e.RowIndex].Cells["left_qty"].Value = Convert.ToDecimal(invpart.qty_onhand) - Convert.ToDecimal(invpart.qty_issued);
                        }
                    }

                }

            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {


            if (ProjectId == string.Empty)
            {
                MessageBox.Show("请选择项目");
                return;
            }
            if (Str_Area == string.Empty)
            {
                MessageBox.Show("请选择区域");
                return;
            }
            if (ActivityName == string.Empty)
            {
                MessageBox.Show("请选择分项");
                return;
            }
            string Site = cmb_site.SelectedValue.ToString();
            if (Site == string.Empty)
            {
                MessageBox.Show("请选择域");
                return;
            }
            int rowcou = dgv1.RowCount;
            if (rowcou == 0)
            {
                MessageBox.Show("请选择所要申请的材料", "操作提示");
                return;
            }

            rowcou = dgv1.RowCount;

            //for (int i = 0; i < rowcou; i++)
            //{

            //    if (dgv1.Rows[i].Cells["序号"].Value == null)
            //    {

            //        MessageBox.Show("第" + (i + 1) + "行请填写材料", "提示");
            //        return;
            //    }


            //}
            SetRowNo();
            for (int i = 0; i < rowcou; i++)
            {

                #region 检查必填项以及数据的合法性
                string rowid = dgv1.Rows[i].Cells["行"].Value.ToString();
                if (dgv1.Rows[i].Cells["申请数量"].Value != null)
                {
                    if (!BaseClass.validateNum(dgv1.Rows[i].Cells["申请数量"].Value.ToString().Trim()))
                    {
                        MessageBox.Show("第 " + rowid + " 行申请数量请填写数字", "提示");
                        dgv1.Rows[i].Selected = true;
                        return;
                    }
                    if (dgv1.Rows[i].Cells["申请数量"].Value.ToString().Trim().Contains("-"))
                    {
                        MessageBox.Show("第 " + rowid + " 行申请数量为负,请检查", "提示");
                        dgv1.Rows[i].Selected = true;
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("第 " + rowid + " 行请填写申请数量", "提示");
                    //dgv1.Rows[i].Selected = true;
                    dgv1.CurrentCell = dgv1.Rows[i].Cells["申请数量"];
                    return;
                }

                if (dgv1.Rows[i].Cells["MEO_ERP"].Value != null)
                {
                    if (!BaseClass.validateNum(dgv1.Rows[i].Cells["MEO_ERP"].Value.ToString().Trim()))
                    {
                        MessageBox.Show("第 " + rowid + " 行MEO号请填写数字", "提示");
                        dgv1.Rows[i].Selected = true;
                        return;
                    }
                }
                else
                {
                    //if (MEOID != "0")
                    //{
                    //    MessageBox.Show("第 " + rowid + " 行请填写MEO号", "提示");
                    //    //dgv1.Rows[i].Selected = true;
                    //    dgv1.CurrentCell = dgv1.Rows[i].Cells["MEO_ERP"];
                    //    return;
                    //}
                }

                if (dgv1.Rows[i].Cells["需求日期"].Value == null)
                {
                    MessageBox.Show("第 " + rowid + " 行请填写需求日期", "提示");
                    dgv1.Rows[i].Selected = true;
                    return;
                }
                if (dgv1.Rows[i].Cells["需求原因"].Value == null)
                {
                    MessageBox.Show("第 " + rowid + " 行请填写需求原因", "提示");
                    dgv1.Rows[i].Selected = true;
                    return;
                }
                if (dgv1.Rows[i].Cells["需求日期"].Value != null)
                {
                    if (Convert.ToDateTime(dgv1.Rows[i].Cells["需求日期"].Value.ToString()) < DateTime.Today)
                    {
                        MessageBox.Show("第 " + rowid + " 行需求日期不能小于当前日期", "提示");
                        dgv1.Rows[i].Selected = true;
                        return;
                    }
                }
                if (dgv1.Rows[i].Cells["MEO_ERP"].Value != null)
                {
                    string partno = dgv1.Rows[i].Cells["零件号"].Value.ToString();
                    if (MEOsub.meomssExistERP(dgv1.Rows[i].Cells["MEO_ERP"].Value.ToString(), partno) == 0)
                    {
                        MessageBox.Show("第 " + rowid + " 行ERP中的MEO号不存在", "提示");
                        dgv1.Rows[i].Selected = true;
                        return;
                    }
                }
                #endregion


            }

            MEOmain MM_MEO = new MEOmain();
            string contractno = cmb_site.SelectedValue.ToString();
            MM_MEO.CONTRACT = contractno;
            //string drawinfor = txt_designcode.Text.Trim();
            MM_MEO.SYSTEM_ID = Str_Area;
            MM_MEO.REQUIRE_NO = MakeMEOManual(int.Parse(cmb_discipline.SelectedValue.ToString()));
            //MM_MEO.INFORMATION = drawinfor;
            MM_MEO.CREATER = User.cur_user;
            MM_MEO.PARTTYPE_ID = Str_FX;
            MM_MEO.DISCIPLINEID = cmb_discipline.SelectedValue.ToString();
            MM_MEO.PROJECT_ID = ProjectId;
            MM_MEO.ECPROJECTID = ProjectSystem.FindProjectid(ProjectId).ToString();
            MM_MEO.ERP_DISCIPLINE = Str_Discipline;
            MM_MEO.ERP_PARTTYPE = ActivityName;
            
            #region 新增保存MEO
            if (MEOID == "0")
            {

                int mmreqid = MM_MEO.REQUIRE_Add();

                if (mmreqid > 0)
                {
                    #region 保存MEO从表
                    for (int i = 0; i < rowcou; i++)
                    {
                        //string partid = dgv1.Rows[i].Cells["序号"].Value.ToString();
                        string partno = dgv1.Rows[i].Cells["零件号"].Value.ToString();
                        string unitmeas = dgv1.Rows[i].Cells["单位"].Value.ToString();
                        string part_desc = dgv1.Rows[i].Cells["零件描述"].Value.ToString();
                        string reqdate = dgv1.Rows[i].Cells["需求日期"].Value.ToString();
                        string reqreason = dgv1.Rows[i].Cells["需求原因"].Value.ToString();
                        string preQty = dgv1.Rows[i].Cells["申请数量"].Value.ToString().Trim();
                        string remark = dgv1.Rows[i].Cells["备注"].Value.ToString();
                        decimal fpreQty = decimal.Parse(preQty);
                        MEOsub MM_submeo = new MEOsub();
                        MM_submeo.REQUIRE_QTY = decimal.Round(fpreQty, 2);
                        //MM_submeo.PART_ID = int.Parse(partid);
                        MM_submeo.REQUIRE_DATE = Convert.ToDateTime(reqdate);
                        MM_submeo.REASON_CODE = reqreason;
                        MM_submeo.CREATER = User.cur_user;
                        MM_submeo.REQUIRE_ID = mmreqid;
                        MM_submeo.PROJECT_ID = ProjectId;
                        MM_submeo.PART_NO = partno;
                        MM_submeo.UNIT_MEAS = unitmeas;
                        MM_submeo.PART_NAME = part_desc;
                        MM_submeo.REMARK = remark;
                        MM_submeo.REQUIRELINE_Add();
                    }
                    MEOID = mmreqid.ToString();
                    //XmlOper.setXML("Block", blockId);
                    MessageBox.Show("保存MEO成功！", "提示");
                    //lbl_manual.Visible = true;
                    //tb_manul.Visible = true;
                    //tb_manul.Text = MM_MEO.REQUIRE_NO;
                    return;
                    #endregion
                }
                else
                {
                    MessageBox.Show("保存失败！", "提示");
                    return;
                }
            }
            #endregion

            #region 更新保存MEO
            //if (MEOID != "0")
            //{
            //    MM_MEO.REQUIRE_ID = int.Parse(MEOID);
            //    MM_MEO.REQUIRE_Update();
            //    MM_MEO.MEODelete(int.Parse(MEOID));
            //    #region 更新保存MEO从表
            //    for (int i = 0; i < rowcou; i++)
            //    {
            //        //string partid = dgv1.Rows[i].Cells["序号"].Value.ToString();
            //        string reqdate = dgv1.Rows[i].Cells["需求日期"].Value.ToString();
            //        string reqreason = dgv1.Rows[i].Cells["需求原因"].Value.ToString();
            //        string preQty = dgv1.Rows[i].Cells["申请数量"].Value.ToString().Trim();
            //        string meonoerp = "";
            //        if (dgv1.Rows[i].Cells["MEO_ERP"].Value != null)
            //        {
            //            meonoerp = dgv1.Rows[i].Cells["MEO_ERP"].Value.ToString().Trim();
            //        }
            //        decimal fpreQty = decimal.Parse(preQty);
            //        MEOsub MM_submeo = new MEOsub();
            //        MM_submeo.REQUIRE_QTY = decimal.Round(fpreQty, 2);
            //        //MM_submeo.PART_ID = int.Parse(partid);
            //        MM_submeo.REQUIRE_DATE = Convert.ToDateTime(reqdate);
            //        MM_submeo.REASON_CODE = reqreason;
            //        MM_submeo.CREATER = User.cur_user;
            //        MM_submeo.REQUIRE_ID = int.Parse(MEOID);
            //        MM_submeo.PROJECT_ID = ProjectId;
            //        MM_submeo.MEO_ERP = meonoerp;
            //        MM_submeo.REQUIRELINE_Add();
            //    }
            //    MessageBox.Show("保存MEO成功！", "提示");
            //    #endregion
            //}

            #endregion

            //    QuerydataBind();
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgv1.Rows.Insert(e.RowIndex, 1);
                dgv1.Rows[e.RowIndex].Cells["MEO_ID"].Value = MEOID;
                dgv1.Rows[e.RowIndex].Cells["需求原因"].Value = "正常申请";
            }
            if (dgv1.RowCount == 0)
            {
                dgv1.Rows.Add();
                dgv1.Rows[0].Cells["MEO_ID"].Value = MEOID;
                dgv1.Rows[0].Cells["需求原因"].Value = "正常申请";
            }
            SetRowNo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                textBox5.Text = Str_Area;
                textBox4.Text = Str_FX;
                textBox2.Text = Str_Discipline;
                textBox3.Text = ActivityName;
                //btn_save.Enabled = true;
                //btn_select.Enabled = true;
            }
            if (tabControl1.SelectedIndex == 0)
            {
                tb_area.Text = Str_Area;
                tb_fx.Text = Str_FX;
                tb_discipline.Text = Str_Discipline;
                tb_type.Text = ActivityName;
                //btn_save.Enabled = true;
                //btn_select.Enabled = true;
            }
        }

        private void btn_query_Click(object sender, EventArgs e)
        {

            
            //string ProjectId = cmb_project.SelectedValue.ToString();
            if (ProjectId == string.Empty)
            {
                MessageBox.Show("请选择项目");
                return;
            }

            //string ActivityId = cmb_activity.SelectedValue.ToString();
            if (ActivityName == string.Empty)
            {
                MessageBox.Show("请选择种类");
                return;
            }
            string meono = txt_meono.Text.Trim().ToLower();
            string partno = cmb_partno.Text.Trim().ToString().ToLower();
            string PartName = cmb_partname.Text.Trim().ToString().ToLower();
            string ReqDateFrom = dateTimePicker3.Checked == true ? dateTimePicker3.Value.ToString("yyyy-MM-dd") : string.Empty;
            string ReqDateTo = dateTimePicker4.Checked == true ? dateTimePicker4.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateFrom = dateTimePicker1.Checked == true ? dateTimePicker1.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateTo = dateTimePicker2.Checked == true ? dateTimePicker2.Value.ToString("yyyy-MM-dd") : string.Empty;
            //string ReasonCode = cmb_reason.SelectedValue.ToString();
            //string DesignCode = txt_designcode.Text.Trim().ToString().ToLower();
            string Creator = txt_creator.Text.Trim().ToString().ToLower();
            string meostate = cmb_state.Text == null ? "" : cmb_state.Text;
            string Site2 = cmb_site2.SelectedValue.ToString();
            string manualno = tb_manual.Text.Trim().ToLower();
            string dispname = cmb_discipline2.SelectedValue.ToString();
            StringBuilder sb = new StringBuilder();
            if (meostate != string.Empty && meostate != null)
            {
                sb.Append(meostate == "已审核" ? " and approveflag='Y'" : "and approveflag='N'");
            }
            if (meono != string.Empty) sb.Append(" and MEO_ERP like '%" + meono + "%'");
            if (ProjectId != string.Empty) sb.Append(" AND PROJECT_ID = '" + Str_Project + "'");
            if (ActivityName != string.Empty) sb.Append(" and ERP_PARTTYPE='" + ActivityName + "'");
            if (Str_Area != string.Empty) sb.Append(" and system_id='" + Str_Area + "'");
            if (Str_Discipline != string.Empty) sb.Append(" and ERP_DISCIPLINE='" + Str_Discipline + "'");
            if (Str_FX != string.Empty) sb.Append(" and parttype_id='" + Str_FX + "'");
            if (Site2 != string.Empty) sb.Append(" and contract='" + Site2 + "'");
            if (partno != string.Empty) sb.Append(" AND   lower(part_no) like '%" + partno + "%'");
            if (PartName != string.Empty) sb.Append(" AND  lower(part_name) like'%" + PartName + "%'");
            if (Creator != string.Empty) sb.Append(" AND  lower(CREATER) like'%" + Creator + "%'");
            if (dispname != string.Empty) sb.Append(" AND disciplineid =" + dispname);
            //if (DesignCode != string.Empty) sb.Append(" AND  lower(DESIGN_CODE) like'%" + DesignCode + "%'");
            if (ReqDateFrom != string.Empty) sb.Append(" and REQUIRE_DATE>=" + Util.GetOracelDateSql(ReqDateFrom));
            if (ReqDateTo != string.Empty) sb.Append(" and REQUIRE_DATE -interval '1' day   <=" + Util.GetOracelDateSql(ReqDateTo));
            if (CreateDateFrom != string.Empty) sb.Append(" and CREATE_DATE>=" + Util.GetOracelDateSql(CreateDateFrom));
            if (CreateDateTo != string.Empty) sb.Append(" and CREATE_DATE -interval '1' day   <=" + Util.GetOracelDateSql(CreateDateTo));
            if (manualno != string.Empty) sb.Append(" AND  lower(require_no) like'%" + manualno + "%'");
            
            string sqlSelect = "select *  from plm.mmm_part_require_view  where 1=1 ";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql;
            listviewBindInter(sqlSelect);
            //XmlOper.setXML("Type", mtype);
        }
        public void listviewBindInter(string sql)
        {
            this.dgv2.AutoGenerateColumns = false;
            //this.dgv1.Rows.Clear();
            DataSet ds = MEOsub.QueryPartMiscProcListEPR(sql);
            if (ds != null)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dgv2.DataSource = dv;
            }

        }

        private void 导出MEO创建申请单ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 审核MEO创建申请单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv2.RowCount != 0 && dgv2.SelectedRows.Count!=0)
            {


                if (MessageBox.Show("确定要审核选中的创建申请吗?", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    for (int i = 0; i < dgv2.RowCount; i++)
                    {
                        if (dgv2.Rows[i].Selected == true)
                        {
                            string requireid = dgv2.Rows[i].Cells["require_id"].Value.ToString();
                            if (MEOmain.ApproveMEO(int.Parse(requireid), "4") != 1)
                            {
                                MessageBox.Show("审核MEO单据失败!!", "错误提示");
                                return;
                            }
                        }
                    }
                    MessageBox.Show("审核MEO单据成功!!", "操作提示");
                    //刷新数据
                    btn_query_Click(sender, e);
                    //this.Hide();
                }
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dgv2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv2_DoubleClick(object sender, EventArgs e)
        {
            if (dgv2.RowCount > 0)
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
                string requireid = dgv2.CurrentRow.Cells["require_id"].Value.ToString();
                string statename = dgv2.CurrentRow.Cells["state"].Value.ToString();
                string stateid = "4";
                if (statename == "待审")
                    stateid = "1";
                MaterialRequireUpdate detailform = new MaterialRequireUpdate(requireid, stateid);
                detailform.Text = "材料维护页面";
                detailform.MdiParent = this.MdiParent;
                detailform.Show();

            }
            else
            {
                return;
            }
        }

        private void btn_queryERP_Click(object sender, EventArgs e)
        {

        }
    }
        
    
}
