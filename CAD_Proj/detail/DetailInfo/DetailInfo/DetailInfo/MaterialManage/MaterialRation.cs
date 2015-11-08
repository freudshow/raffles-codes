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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
namespace DetailInfo.MaterialManage
{
    public partial class MaterialRation : Form
    {
        string ProjectId = string.Empty, ecprojectid = "";
        string cSite = "";
        public string MEOID = string.Empty, MEOSTATE = string.Empty, ActivityName = string.Empty, Str_Project = string.Empty, Str_Discipline = string.Empty, Str_Area = string.Empty, ActivityId, Str_FX = string.Empty,Block_id = string.Empty;
        public MaterialRation(string cur_meoid, string cur_state)
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
            //ProjectCmbItem.SiteCmbBind(cmb_site2);
            ProjectCmbItem.SiteCmbBind(cmb_site); 
            ProjectCmbItem.BindDisciplineName(cmb_discipline);
            //ProjectCmbItem.BindDiscipline(cmb_discipline2);
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
            dgvcom1.ValueMember = "REASON_CODE";
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
                //dgv1.Rows.Add(5);
                //for (int i = 0; i < 5; i++)
                //{
                //    dgv1.Rows[i].Cells["MEO_ID"].Value = MEOID;
                //    dgv1.Rows[i].Cells["需求原因"].Value = "正常申请";
                //}
            }
            //SetRowNo();

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
            
            ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择项目", "提示");
                return;
            }
            ProjectId = item.Value;
            ecprojectid = ProjectSystem.FindProjectid(ProjectId).ToString();
            
            ProjectCmbItem.BlockBind(cmb_block, ecprojectid);



        }
        

        /// <summary>
        /// 点击Dgv前面的X号可以删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (MEOSTATE == "1")
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        DataGridViewColumn column = dgv1.Columns[e.ColumnIndex];
            //        if (column is DataGridViewImageColumn)
            //        {
            //            //这里可以编写你需要的任意关于按钮事件的操作~
            //            dgv1.Rows.Remove(dgv1.CurrentRow);
            //            //MessageBox.Show("按钮被点击");
            //        }
            //    }
            //    SetRowNo();
            //}
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
            if (dgv1.RowCount < 1) return;

            if (ProjectId == string.Empty)
            {
                MessageBox.Show("请选择项目");
                return;
            }
            
            int rowcou = dgv1.RowCount;
            if (rowcou == 0)
            {
                MessageBox.Show("请选择所要下发的材料", "操作提示");
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
            try
            {
                for (int i = 0; i < rowcou; i++)
                {

                    #region 检查必填项以及数据的合法性
                    string rowid = dgv1.Rows[i].Cells["行"].Value.ToString();
                    if (dgv1.Rows[i].Cells["需求数量"].Value != null)
                    {
                        if (!BaseClass.validateNum(dgv1.Rows[i].Cells["需求数量"].Value.ToString().Trim()))
                        {
                            MessageBox.Show("第 " + rowid + " 行需求数量请填写数字", "提示");
                            dgv1.Rows[i].Selected = true;
                            return;
                        }
                        if (dgv1.Rows[i].Cells["需求数量"].Value.ToString().Trim().Contains("-"))
                        {
                            MessageBox.Show("第 " + rowid + " 行需求数量为负,请检查", "提示");
                            dgv1.Rows[i].Selected = true;
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("第 " + rowid + " 行请填写需求数量", "提示");
                        //dgv1.Rows[i].Selected = true;
                        dgv1.CurrentCell = dgv1.Rows[i].Cells["需求数量"];
                        return;
                    }

                    //if (dgv1.Rows[i].Cells["MEO_ERP"].Value != null)
                    //{
                    //    if (!BaseClass.validateNum(dgv1.Rows[i].Cells["MEO_ERP"].Value.ToString().Trim()))
                    //    {
                    //        MessageBox.Show("第 " + rowid + " 行MEO号请填写数字", "提示");
                    //        dgv1.Rows[i].Selected = true;
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    //if (MEOID != "0")
                    //    //{
                    //    //    MessageBox.Show("第 " + rowid + " 行请填写MEO号", "提示");
                    //    //    //dgv1.Rows[i].Selected = true;
                    //    //    dgv1.CurrentCell = dgv1.Rows[i].Cells["MEO_ERP"];
                    //    //    return;
                    //    //}
                    //}

                    if (dgv1.Rows[i].Cells["需求日期"].Value == null)
                    {
                        MessageBox.Show("第 " + rowid + " 行请填写需求日期", "提示");
                        dgv1.Rows[i].Selected = true;
                        return;
                    }
                    //if (dgv1.Rows[i].Cells["需求日期"].Value != null)
                    //{
                    //    if (Convert.ToDateTime(dgv1.Rows[i].Cells["需求日期"].Value.ToString()) < DateTime.Today)
                    //    {
                    //        MessageBox.Show("第 " + rowid + " 行需求日期不能小于当前日期", "提示");
                    //        dgv1.Rows[i].Selected = true;
                    //        return;
                    //    }
                    //}
                    if (dgv1.Rows[i].Cells["需求原因"].Value == null)
                    {
                        MessageBox.Show("第 " + rowid + " 行请填写需求原因", "提示");
                        dgv1.Rows[i].Selected = true;
                        return;
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
                for (int i = 0; i < rowcou; i++)
                {
                    #region 循环保存数据
                    string Rationid = dgv1.Rows[i].Cells["Ration_id"].Value.ToString();
                    string contractno = cmb_site.SelectedValue.ToString();
                    string Partno = dgv1.Rows[i].Cells["零件号"].Value.ToString();
                    Decimal Reqqty = Decimal.Parse(dgv1.Rows[i].Cells["需求数量"].Value.ToString());
                    DateTime ReqDate = Convert.ToDateTime(dgv1.Rows[i].Cells["需求日期"].Value.ToString());
                    //string purpose = txt_purpose.Text.Trim().ToString();
                    string blockId = cmb_block.SelectedValue.ToString();
                    string ReasonCode = dgv1.Rows[i].Cells["需求原因"].Value.ToString();
                    //string DesignCode = txt_designcode.Text.Trim().ToString();
                    string ReqNo = dgv1.Rows[i].Cells["MEO_ERP"].Value.ToString();
                    string remark = dgv1.Rows[i].Cells["备注"].Value.ToString();
                    //string isInventory = chb_useInv.Checked == true ? "是" : "否";
                    string Partname = dgv1.Rows[i].Cells["零件描述"].Value.ToString();
                    string zyname = dgv1.Rows[i].Cells["dt_zy"].Value.ToString();
                    string qyname = dgv1.Rows[i].Cells["dt_qy"].Value.ToString();
                    string fxname = dgv1.Rows[i].Cells["dt_fx"].Value.ToString();
                    string zlname = dgv1.Rows[i].Cells["dt_zl"].Value.ToString();
                    Ration mp = new Ration();
                    mp.PROJECT_ID = ProjectId;
                    mp.RATION_ID = Convert.ToInt32(Rationid);
                    mp.PART_NO = Partno;
                    mp.ISSUED_QTY = decimal.Round(Reqqty, 2);
                    mp.ISSUED_DATE = ReqDate;
                    //mp.IF_INVENTORY = isInventory;
                    //mp.PURPOSE = purpose;
                    mp.BLOCK_ID = blockId;
                    mp.REASON_CODE = ReasonCode;
                    //mp.DESIGN_CODE = DesignCode;
                    mp.REMARK = remark;
                    mp.INFORMATION = remark;
                    mp.CONTRACT = contractno;
                    mp.CREATER = User.cur_user;
                    mp.PART_NAME = Partname;
                    mp.ECPROJECTID = ecprojectid;
                    mp.PART_ZONE = qyname;
                    mp.PART_FX = fxname;
                    mp.PART_DISCIPLINE = zyname;
                    mp.PART_TYPE = zlname;
                    XmlOper.setXML("Block", blockId);
                    if (Rationid != "0")
                    {
                        //mp.RATION_ID = Convert.ToInt32(lbl_rationid.Text);
                        int n = mp.Update();


                    }
                    else
                    {
                        int n = mp.Add();

                    }
                    #endregion
                }
                MessageBox.Show("保存成功!!!");
            }
            catch (Exception err)
            {
                MessageBox.Show("错误原因：" + err.Message, "错误提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //    QuerydataBind();
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    dgv1.Rows.Insert(e.RowIndex, 1);
            //    dgv1.Rows[e.RowIndex].Cells["MEO_ID"].Value = MEOID;
            //    dgv1.Rows[e.RowIndex].Cells["需求原因"].Value = "正常申请";
            //    dgv1.Rows[e.RowIndex].Cells["Ration_id"].Value = 0;
            //}
            //if (dgv1.RowCount == 0)
            //{
            //    dgv1.Rows.Add();
            //    dgv1.Rows[0].Cells["MEO_ID"].Value = MEOID;
            //    dgv1.Rows[0].Cells["需求原因"].Value = "正常申请";
            //}
            //SetRowNo();
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
                //textBox5.Text = Str_Area;
                //textBox4.Text = Str_FX;
                //textBox2.Text = Str_Discipline;
                //textBox3.Text = ActivityName;
                ////btn_save.Enabled = true;
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
            //string meono = txt_meono.Text.Trim().ToLower();
            //string partno = cmb_partno.Text.Trim().ToString().ToLower();
            //string PartName = cmb_partname.Text.Trim().ToString().ToLower();
            //string ReqDateFrom = dateTimePicker3.Checked == true ? dateTimePicker3.Value.ToString("yyyy-MM-dd") : string.Empty;
            //string ReqDateTo = dateTimePicker4.Checked == true ? dateTimePicker4.Value.ToString("yyyy-MM-dd") : string.Empty;
            //string CreateDateFrom = dateTimePicker1.Checked == true ? dateTimePicker1.Value.ToString("yyyy-MM-dd") : string.Empty;
            //string CreateDateTo = dateTimePicker2.Checked == true ? dateTimePicker2.Value.ToString("yyyy-MM-dd") : string.Empty;
            ////string ReasonCode = cmb_reason.SelectedValue.ToString();
            ////string DesignCode = txt_designcode.Text.Trim().ToString().ToLower();
            //string Creator = txt_creator.Text.Trim().ToString().ToLower();
            //string meostate = cmb_state.Text == null ? "" : cmb_state.Text;
            //string Site2 = cmb_site2.SelectedValue.ToString();
            //string manualno = tb_manual.Text.Trim().ToLower();
            //string dispname = cmb_discipline2.SelectedValue.ToString();
            //StringBuilder sb = new StringBuilder();
            //if (meostate != string.Empty && meostate != null)
            //{
            //    sb.Append(meostate == "已审核" ? " and approveflag='Y'" : "and approveflag='N'");
            //}
            //if (meono != string.Empty) sb.Append(" and MEO_ERP like '%" + meono + "%'");
            //if (ProjectId != string.Empty) sb.Append(" AND PROJECT_ID = '" + Str_Project + "'");
            //if (ActivityName != string.Empty) sb.Append(" and ERP_PARTTYPE='" + ActivityName + "'");
            //if (Str_Area != string.Empty) sb.Append(" and system_id='" + Str_Area + "'");
            //if (Str_Discipline != string.Empty) sb.Append(" and ERP_DISCIPLINE='" + Str_Discipline + "'");
            //if (Str_FX != string.Empty) sb.Append(" and parttype_id='" + Str_FX + "'");
            //if (Site2 != string.Empty) sb.Append(" and contract='" + Site2 + "'");
            //if (partno != string.Empty) sb.Append(" AND   lower(part_no) like '%" + partno + "%'");
            //if (PartName != string.Empty) sb.Append(" AND  lower(part_name) like'%" + PartName + "%'");
            //if (Creator != string.Empty) sb.Append(" AND  lower(CREATER) like'%" + Creator + "%'");
            //if (dispname != string.Empty) sb.Append(" AND disciplineid =" + dispname);
            ////if (DesignCode != string.Empty) sb.Append(" AND  lower(DESIGN_CODE) like'%" + DesignCode + "%'");
            //if (ReqDateFrom != string.Empty) sb.Append(" and REQUIRE_DATE>=" + Framework.Util.GetOracelDateSql(ReqDateFrom));
            //if (ReqDateTo != string.Empty) sb.Append(" and REQUIRE_DATE -interval '1' day   <=" + Framework.Util.GetOracelDateSql(ReqDateTo));
            //if (CreateDateFrom != string.Empty) sb.Append(" and CREATE_DATE>=" + Framework.Util.GetOracelDateSql(CreateDateFrom));
            //if (CreateDateTo != string.Empty) sb.Append(" and CREATE_DATE -interval '1' day   <=" + Framework.Util.GetOracelDateSql(CreateDateTo));
            //if (manualno != string.Empty) sb.Append(" AND  lower(require_no) like'%" + manualno + "%'");
            
            //string sqlSelect = "select *  from plm.mmm_part_require_view  where 1=1 ";
            //string wheresql = sb.ToString();
            //sqlSelect = sqlSelect + wheresql;
            
        }
        

        private void 导出MEO创建申请单ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 审核MEO创建申请单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dgv2.RowCount != 0 && dgv2.SelectedRows.Count!=0)
            //{


            //    if (MessageBox.Show("确定要审核选中的创建申请吗?", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //    {

            //        for (int i = 0; i < dgv2.RowCount; i++)
            //        {
            //            if (dgv2.Rows[i].Selected == true)
            //            {
            //                string requireid = dgv2.Rows[i].Cells["require_id"].Value.ToString();
            //                if (MEOmain.ApproveMEO(int.Parse(requireid), "4") != 1)
            //                {
            //                    MessageBox.Show("审核MEO单据失败!!", "错误提示");
            //                    return;
            //                }
            //            }
            //        }
            //        MessageBox.Show("审核MEO单据成功!!", "操作提示");
            //        //刷新数据
            //        btn_query_Click(sender, e);
            //        //this.Hide();
            //    }
            //} 
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
            //if (dgv2.RowCount > 0)
            //{
            //    foreach (Form form in this.ParentForm.MdiChildren)
            //    {
            //        if (form.Text == "材料维护页面")
            //        {
            //            if (form.WindowState == FormWindowState.Minimized)
            //            {
            //                form.WindowState = FormWindowState.Normal;
            //            }
            //            form.Activate();
            //            return;
            //        }
            //    }
            //    string requireid = dgv2.CurrentRow.Cells["require_id"].Value.ToString();
            //    string statename = dgv2.CurrentRow.Cells["state"].Value.ToString();
            //    string stateid = "4";
            //    if (statename == "待审")
            //        stateid = "1";
            //    MaterialRequireUpdate detailform = new MaterialRequireUpdate(requireid, stateid);
            //    detailform.Text = "材料维护页面";
            //    detailform.MdiParent = this.MdiParent;
            //    detailform.Show();

            //}
            //else
            //{
            //    return;
            //}
        }

        private void btn_queryERP_Click(object sender, EventArgs e)
        {

        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            if (ProjectId == string.Empty)
            {
                MessageBox.Show("请选择项目");
                return;
            }
            if (cmb_block.SelectedValue!=null)
            Block_id = cmb_block.SelectedValue.ToString();
            if (Block_id == string.Empty)
            {
                MessageBox.Show("请选择分段");
                return;
            }
            QuerydataBind();
        }
        public void QuerydataBind()
        {
            //MessageBox.Show(cmb_discipline.Text);
            //return;
            cSite = cmb_site.SelectedValue.ToString();
            //string partno = tb_partno.Text.ToString();
            //string PartName = tb_partname.Text.ToString();
            //string ApplyDate = dateTimePicker1.Checked == true ? dateTimePicker1.Value.ToString("yyyy-MM-dd") : string.Empty;
            //string ReasonCode = tb_reason.SelectedValue.ToString();
            //string DesignCode = txt_designcode.Text.Trim().ToString();
            //string Remark = txt_remark.Text.Trim().ToString();
            string str_discipline = cmb_discipline.SelectedValue.ToString();
            Block_id = cmb_block.SelectedValue.ToString();

            StringBuilder sb = new StringBuilder();
            if (cSite != string.Empty) sb.Append(" AND contract = '" + cSite + "'");
            if (ProjectId != string.Empty) sb.Append(" AND PROJECT_ID = '" + ProjectId + "'");
            if (str_discipline != string.Empty) sb.Append(" and PART_DISCIPLINE ='" + str_discipline + "'");
            //if (partid != string.Empty) sb.Append("  and partid='" + partid + "'");
            //if (partno != string.Empty) sb.Append(" AND part_no like '%" + partno + "%'");
            //if (PartName != string.Empty) sb.Append(" and PART_NAME like'%" + PartName + "%'");
            //if (ApplyDate != string.Empty) sb.Append(" AND ISSUED_DATE =" + Util.GetOracelDateSql(ApplyDate));
            //if (ReasonCode != string.Empty) sb.Append(" AND REASON_CODE='" + ReasonCode + "'");
            //if (DesignCode != string.Empty) sb.Append(" AND DESIGN_CODE like'%" + DesignCode + "%'");
            //if (Remark != string.Empty) sb.Append(" AND REMARK like'%" + Remark + "%'");
            //if (Purpose != string.Empty) sb.Append(" AND PURPOSE like'%" + Purpose + "%'");
            if (Block_id != string.Empty) sb.Append(" AND BLOCK_ID like'%" + Block_id + "%'");//and if_inventory='0' 
            string sqlSelect = "select ration_id,part_no,part_name,issued_qty,unit,ERP_MEO,DESIGN_CODE,block_id,issued_date,remark,REASON_CODE,PART_ZONE,PART_FX,PART_DISCIPLINE,PART_TYPE from plm.MM_PART_RATION_TAB  p where state<>'5' and deleteflag='N' and creater='" + User.cur_user + "'";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql;
            listviewBind(sqlSelect);
        }
        public void listviewBind(string sql)
        {
            this.dgv1.AutoGenerateColumns = false;
            this.dgv1.Rows.Clear();
            DataSet ds = Ration.QueryPartRationList(sql);
            DataView dv = ds.Tables[0].DefaultView;
            int i = 1;
            foreach (DataRow dr in dv.Table.Rows)
            {
                string partno = dr[1].ToString(); 
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dgv1);
                //行号                                                                                                                                                                                               
                r.Cells[0].Value = i;
                //Ration_id
                r.Cells[1].Value = dr[0].ToString();
                //区域,分项,专业,种类
                //Block与 区域对应表；取得区域
                r.Cells[2].Value = dr[11].ToString();
                //根据ERP Code来区分是材料还是设备,取得分项
                r.Cells[3].Value = dr[12].ToString();
                //专业可以让设计员自己填写或者默认
                r.Cells[4].Value = dr[13].ToString();
                //可以根据ERP Code来确定 材料种类
                r.Cells[5].Value = dr[14].ToString();

                //part_id零件号
                r.Cells[6].Value = partno;
                //part_name零件描述
                r.Cells[7].Value = dr[2].ToString();
                if (partno != string.Empty && partno != null)
                {
                    //InventoryPart invpart = InventoryPart.GetRequiredqty(cSite, partno, ProjectId);
                    //if (invpart != null)
                    //{
                    //    //总申请数量
                    //    r.Cells[8].Value = invpart.qty_onhand;
                    //    //已下发申请数量
                    //    r.Cells[9].Value = invpart.qty_issued;
                    //    //余下申请数量
                    //    r.Cells[10].Value = Convert.ToDecimal(invpart.qty_onhand) - Convert.ToDecimal(invpart.qty_issued);
                    //    //可用项目预留数量
                    //    r.Cells[11].Value = invpart.qty_reserved;
                    //}
                }
                //需求数量
                r.Cells[12].Value = dr[3].ToString();
                //单位
                r.Cells[13].Value = dr[4].ToString();
                //ERPMEONO
                r.Cells[14].Value = dr[5].ToString();
                //图纸号
                r.Cells[15].Value = dr[6].ToString();
                //分段号
                r.Cells[16].Value = dr[7].ToString();
                //需求日期
                r.Cells[17].Value = dr[8].ToString();
                //备注
                r.Cells[18].Value = dr[9].ToString();
                //需求原因
                r.Cells[20].Value = dr[10].ToString();
                
                
                
                this.dgv1.Rows.Add(r);
                i++;
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount < 1) return;
            InitializeWorkbook(@"Template/项目MSS明细表.xls");
            ISheet sheet1 = hssfworkbook.GetSheet("MSS明细表");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            //sheet1.GetRow(1).GetCell(1).SetCellValue(200200);
            //sheet1.GetRow(2).GetCell(1).SetCellValue(300);
            //sheet1.GetRow(3).GetCell(1).SetCellValue(500050);
            //sheet1.GetRow(4).GetCell(1).SetCellValue(8000);
            for (int i = 0; i < dgv1.RowCount; i++)
            {

                string partno = dgv1.Rows[i].Cells["零件号"].Value.ToString();
                string partname = dgv1.Rows[i].Cells["零件描述"].Value.ToString();
                DateTime reqdatestr = Convert.ToDateTime(dgv1.Rows[i].Cells["需求日期"].Value);
                string reqdate = reqdatestr.ToString("yyyy-MM-dd");
                string reqreason = dgv1.Rows[i].Cells["需求原因"].Value.ToString();
                string unitname = dgv1.Rows[i].Cells["单位"].Value.ToString();
                string preQty = dgv1.Rows[i].Cells["需求数量"].Value.ToString();
                string remark = "";
                if (dgv1.Rows[i].Cells["备注"].Value != null)
                    remark = dgv1.Rows[i].Cells["备注"].Value.ToString();
                sheet1.GetRow(i + 5).GetCell(0).SetCellValue(i + 1);
                sheet1.GetRow(i + 5).GetCell(1).SetCellValue(ProjectId);
                sheet1.GetRow(i + 5).GetCell(2).SetCellValue(Str_Area);
                sheet1.GetRow(i + 5).GetCell(3).SetCellValue(Str_FX);
                sheet1.GetRow(i + 5).GetCell(4).SetCellValue(Str_Discipline);
                sheet1.GetRow(i + 5).GetCell(5).SetCellValue(ActivityName);
                sheet1.GetRow(i + 5).GetCell(6).SetCellValue(partno);
                sheet1.GetRow(i + 5).GetCell(7).SetCellValue(partname);
                sheet1.GetRow(i + 5).GetCell(9).SetCellValue(preQty);
                sheet1.GetRow(i + 5).GetCell(10).SetCellValue(unitname);
                //sheet1.GetRow(i + 5).GetCell(11).SetCellValue(reqdate);
                sheet1.GetRow(i + 5).GetCell(12).SetCellValue(reqreason);
                sheet1.GetRow(i + 5).GetCell(13).SetCellValue(reqreason);
                //sheet1.GetRow(i + 5).GetCell(14).SetCellValue(meolist.INFORMATION);
                sheet1.GetRow(i + 5).GetCell(15).SetCellValue(remark);
                if (i + 5 > 14)
                    sheet1.CreateRow(i + 6);
                #region 循环保存数据
                string Rationid = dgv1.Rows[i].Cells["Ration_id"].Value.ToString();
                Ration.UpdateMSSState(Rationid, 5);
                #endregion

            }

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            try
            {
                WriteToFile("MSS导出/" + "MSSList.xls");
                //MessageBox.Show("MSSList.xls" + "导出成功!", "提示消息");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            InitializeWorkbook(@"Template/项目MEO明细表.xls");
            ISheet sheet2 = hssfworkbook.GetSheet("MEO明细表");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            //sheet1.GetRow(1).GetCell(1).SetCellValue(200200);
            //sheet1.GetRow(2).GetCell(1).SetCellValue(300);
            //sheet1.GetRow(3).GetCell(1).SetCellValue(500050);
            //sheet1.GetRow(4).GetCell(1).SetCellValue(8000);
            int j = 0; 
            for (int i = 0; i < dgv1.RowCount; i++)
            {
                string preQty = dgv1.Rows[i].Cells["需求数量"].Value.ToString();
                decimal left_reqqty =Convert.ToDecimal( dgv1.Rows[i].Cells["left_qty"].Value.ToString());
                decimal reserved_qty = Convert.ToDecimal(dgv1.Rows[i].Cells["reserved_qty"].Value.ToString());

                if (Convert.ToDecimal(preQty) > left_reqqty + reserved_qty)
                {
                    string partno = dgv1.Rows[i].Cells["零件号"].Value.ToString();
                    string partname = dgv1.Rows[i].Cells["零件描述"].Value.ToString();
                    DateTime reqdatestr = Convert.ToDateTime(dgv1.Rows[i].Cells["需求日期"].Value);
                    string reqdate = reqdatestr.ToString("yyyy-MM-dd");
                    string reqreason = dgv1.Rows[i].Cells["需求原因"].Value.ToString();
                    string unitname = dgv1.Rows[i].Cells["单位"].Value.ToString();
                    string remark = "";
                    if (dgv1.Rows[i].Cells["备注"].Value != null)
                        remark = dgv1.Rows[i].Cells["备注"].Value.ToString();
                    sheet2.GetRow(j + 5).GetCell(0).SetCellValue(i + 1);
                    sheet2.GetRow(j + 5).GetCell(2).SetCellValue(ProjectId);
                    sheet2.GetRow(j + 5).GetCell(3).SetCellValue(Str_Area);
                    sheet2.GetRow(j + 5).GetCell(4).SetCellValue(Str_FX);
                    sheet2.GetRow(j + 5).GetCell(5).SetCellValue(Str_Discipline);
                    sheet2.GetRow(j + 5).GetCell(6).SetCellValue(ActivityName);
                    sheet2.GetRow(j + 5).GetCell(7).SetCellValue(partno);
                    sheet2.GetRow(j + 5).GetCell(8).SetCellValue(partname);
                    sheet2.GetRow(j + 5).GetCell(9).SetCellValue(preQty);
                    sheet2.GetRow(j + 5).GetCell(10).SetCellValue(unitname);
                    sheet2.GetRow(j + 5).GetCell(11).SetCellValue(reqdate);
                    sheet2.GetRow(j + 5).GetCell(12).SetCellValue(reqreason);
                    sheet2.GetRow(j + 5).GetCell(13).SetCellValue(reqreason);
                    //sheet1.GetRow(i + 5).GetCell(14).SetCellValue(meolist.INFORMATION);
                    sheet2.GetRow(j + 5).GetCell(15).SetCellValue(remark);
                    if (i + 5 > 14)
                        sheet2.CreateRow(j + 6);
                    j++;
                }
            }

            //Force excel to recalculate all the formula while open
            sheet2.ForceFormulaRecalculation = true;

            try
            {
                WriteToFile("MSS导出/" + "不足材料列表.xls");
                MessageBox.Show("MSSList.xls和不足材料列表.xls" + "导出成功!", "提示消息");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void WriteToFile(string exportpath)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(exportpath, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        static HSSFWorkbook hssfworkbook;
        static void InitializeWorkbook(string templatepath)
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            FileStream file = new FileStream(templatepath, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }
    }
        
    
}
