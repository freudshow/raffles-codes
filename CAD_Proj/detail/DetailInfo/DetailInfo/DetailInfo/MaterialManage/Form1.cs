using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework;

namespace DetailInfo.MaterialManage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string Projectid = string.Empty,mSite= string.Empty;
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            
            ProjectCmbItem item = (ProjectCmbItem)cmb_project.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选择项目", "提示");
                return;
            }
            Projectid = item.Value;
            if (Projectid == string.Empty)
            {
                MessageBox.Show("请选择项目");
                return;
            }
            mSite = cmb_site2.SelectedValue.ToString();
            string meono = txt_meono.Text.Trim().ToLower();
            string partno = cmb_partno.Text.Trim().ToString().ToLower();
            string PartName = cmb_partname.Text.Trim().ToString().ToLower();
            string ReqDateFrom = dateTimePicker3.Checked == true ? dateTimePicker3.Value.ToString("yyyy-MM-dd") : string.Empty;
            string ReqDateTo = dateTimePicker4.Checked == true ? dateTimePicker4.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateFrom = dateTimePicker1.Checked == true ? dateTimePicker1.Value.ToString("yyyy-MM-dd") : string.Empty;
            string CreateDateTo = dateTimePicker2.Checked == true ? dateTimePicker2.Value.ToString("yyyy-MM-dd") : string.Empty;
            //ReasonCode Ritem = (ReasonCode)cmb_reason.SelectedValue;
            string ReasonCode = cmb_reason.SelectedValue.ToString();
            string DesignCode = txt_designcode.Text.Trim().ToString().ToLower();
            string Creator = txt_creator.Text.Trim().ToString().ToLower();
            string meostate = cmb_state.Text == null ? "" : cmb_state.Text;
            string manualno = tb_manual.Text.Trim().ToLower();
            StringBuilder sb = new StringBuilder();
            if (meostate != string.Empty && meostate != null)
            {
                sb.Append(meostate == "已审核" ? " and state='4'" : "and state='1'");
            }
            if (meono != string.Empty) sb.Append(" and MEO_ERP like '%" + meono + "%'");
            if (Projectid != string.Empty) sb.Append(" AND PROJECT_ID = '" + Projectid + "'");
            if (partno != string.Empty) sb.Append(" AND   lower(part_no) like '%" + partno + "%'");
            if (PartName != string.Empty) sb.Append(" AND  lower(part_name) like'%" + PartName + "%'");
            if (Creator != string.Empty) sb.Append(" AND  lower(CREATER) like'%" + Creator + "%'");
            if (manualno != string.Empty) sb.Append(" AND  lower(require_no) like'%" + manualno + "%'");
            if (ReasonCode != string.Empty) sb.Append(" AND REASON_CODE ='" + ReasonCode + "'");
            if (DesignCode != string.Empty) sb.Append(" AND  lower(DESIGN_CODE) like'%" + DesignCode + "%'");
            if (ReqDateFrom != string.Empty) sb.Append(" and REQUIRE_DATE>=" + Util.GetOracelDateSql(ReqDateFrom));
            if (ReqDateTo != string.Empty) sb.Append(" and REQUIRE_DATE -interval '1' day   <=" + Util.GetOracelDateSql(ReqDateTo));
            if (CreateDateFrom != string.Empty) sb.Append(" and CREATE_DATE>=" + Util.GetOracelDateSql(CreateDateFrom));
            if (CreateDateTo != string.Empty) sb.Append(" and CREATE_DATE -interval '1' day   <=" + Util.GetOracelDateSql(CreateDateTo));

            string sqlSelect = "select *  from plm.mmm_part_require_view  where 1=1 ";
            string wheresql = sb.ToString();
            sqlSelect = sqlSelect + wheresql;
            listviewBindInter(sqlSelect);
        }
        public void listviewBindInter(string sql)
        {
            this.dgv1.AutoGenerateColumns = false;
            //this.dgv1.Rows.Clear();
            DataSet ds = MEOsub.QueryPartMiscProcListEPR(sql);
            DataView dv = ds.Tables[0].DefaultView;
            dgv1.DataSource = dv;
            //方法一：逐行比对ERP中申请的数据然后返回比对结果；
            if (dgv1.RowCount > 0)
            {
                for(int i=0;i<dgv1.RowCount;i++)
                {
                    string part_no = dgv1.Rows[i].Cells["part_no"].Value.ToString();
                    string requir_qty = dgv1.Rows[i].Cells["require_qty"].Value.ToString();
                    string meo_no = dgv1.Rows[i].Cells["meo_erp"].Value.ToString();
                    string sqlstr = "select  t.p_requisition_no MEO号,t.request_qty as 申请数量 from IFSAPP.PROJECT_MISC_PROCUREMENT t where PROJECT_ID = '" + Projectid + "' and site = '" + mSite + "' and issue_from_inv = 0 and PART_NO ='" + part_no + "'and p_requisition_no ="+meo_no+" and (select state from ifsapp.purchase_req_line_part q where q.requisition_no =p_requisition_no and q.part_no=t.part_no) <>'Cancelled'";
                    DataSet dsnew = PartParameter.QueryPartERPInventory(sqlstr);
                    if (dsnew.Tables[0].Rows.Count !=0)
                    {
                        dgv1.Rows[i].Cells["ISEXISTED"].Value = "存在";
                        string req_qty = dsnew.Tables[0].Rows[i]["申请数量"].ToString();
                        decimal ecqty= decimal.Parse(requir_qty);
                        decimal erpqty = decimal.Parse(req_qty);
                        if (decimal.Round(ecqty, 2) == decimal.Round(erpqty, 2))
                        {
                            dgv1.Rows[i].Cells["ERP_QTY"].Value = erpqty;
                            dgv1.Rows[i].Cells["compare_result"].Value = "数量一致";
                        }
                        else
                        {
                            dgv1.Rows[i].Cells["ERP_QTY"].Value = erpqty;
                            dgv1.Rows[i].Cells["compare_result"].Value = "数量不一致";
                        }
                                            

                    }
                    else
                    {
                        dgv1.Rows[i].Cells["ISEXISTED"].Value = "不存在";
                    }
                }
            }
            //方法二：将符合域与项目条件的所有ERP申请数据整合到DataSet中去，然后遍历比对数据
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProjectCmbItem.ProjectCmbBind(cmb_project);
            ProjectCmbItem.ReasonCmbBind(cmb_reason);
            ProjectCmbItem.SiteCmbBind(cmb_site2);
            ProjectCmbItem.ProjectCmbBind(cmb_project);
            ProjectCmbItem.BindDiscipline(cmb_discipline2);
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
