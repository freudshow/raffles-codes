using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class ModifyDrawingControl : UserControl
    {
        public ModifyDrawingControl()
        {
            InitializeComponent();
        }
        private int projectid;

        public int Projectid
        {
            get { return projectid; }
            set { projectid = value; }
        }

        private void ModifyDrawingControl_Load(object sender, EventArgs e)
        {
            BindReason();
            BindStatus();
            BindResponser();
            BindMaterialType();
            this.labelColor7.Text = "";
        }

        /// <summary>
        /// 填充原因
        /// </summary>
        private void BindReason()
        {
            DataSet ds = new DataSet();
            string usersqlstr = @"select t.reason_id,
       case t.parent_id
         when 0 then t.reason_desc
           else '    -' || t.reason_desc   end redesc
  from mf_reasontype_tab t
 order by t.reason_id
";
            User.DataBaseConnect(usersqlstr, ds);
            this.reason_cb.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            this.reason_cb.DisplayMember = "redesc";
            this.reason_cb.ValueMember = "reason_id";
            this.reason_cb.SelectedIndex = -1;
        }

        /// <summary>
        /// 填充产前产后
        /// </summary>
        private void BindStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status", typeof(String));
            dt.Columns.Add("Statusid", typeof(String));
            DataRow dr1 = dt.NewRow(); dr1["Status"] = "产前"; dr1["Statusid"] = "B"; dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow(); dr2["Status"] = "产后"; dr2["Statusid"] = "A"; dt.Rows.Add(dr2);
            this.status_cb.DataSource = dt.DefaultView;
            this.status_cb.DisplayMember = "Status";
            this.status_cb.ValueMember = "Statusid";
            dt.Dispose();
            this.status_cb.SelectedIndex = -1;
        }

        /// <summary>
        /// 填充责任人
        /// </summary>
        private void BindResponser()
        {
            DataSet ds = new DataSet();
            string usersqlstr = "select username,id from project_user_tab where project_id = '" + User.projectid + "' and catalog = 'Modify'";
            User.DataBaseConnect(usersqlstr, ds);
            this.responsercomb.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            this.responsercomb.DisplayMember = "username";
            this.responsercomb.ValueMember = "id";
            this.responsercomb.SelectedIndex = -1;
        }

        /// <summary>
        /// 填充损耗的材料类型
        /// </summary>
        private void BindMaterialType()
        {
            DataSet ds = new DataSet();
            string matypesqlstr = "select type_id, type_desc from mf_materaltype_tab";
            User.DataBaseConnect(matypesqlstr, ds);
            this.typecob.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            this.typecob.DisplayMember = "type_desc";
            this.typecob.ValueMember = "type_id";
            this.typecob.SelectedIndex = -1;
        }

        /// <summary>
        /// 添加原因行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModifyDrawingControl mdcontrol = new ModifyDrawingControl();
            mdcontrol.Anchor = AnchorStyles.Top;
            mdcontrol.Anchor = AnchorStyles.Left;
            ((TableLayoutPanel)(this.Parent)).Controls.Add(mdcontrol, 0, 2);
        }

        /// <summary>
        /// 删除原因行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int count = 0;
            foreach (Control control in ((TableLayoutPanel)(this.Parent)).Controls)
            {
                if (control.Name == "ModifyDrawingControl")
                {
                    count += 1;
                }
            }
            if (count > 1)
            {
                this.Parent.Controls.Remove(this);
            }
        }

        /// <summary>
        /// 损耗类型选择时，生产损耗无量单位变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typecob_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (typecob.SelectedIndex == -1)
            {
                this.labelColor7.Text = "";
            }
             else
             {
                 if (typecob.SelectedValue.ToString() == "5")
                 {
                     this.labelColor7.Text = "mm";
                 }
                 else
                 {
                     this.labelColor7.Text = "kg";
                 }
             }
        }

        /// <summary>
        /// 控制损耗材料类型和生产损耗物量是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void status_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.status_cb.SelectedIndex == 0)
            {
                this.typecob.SelectedIndex = -1;
                this.materialcost_tb.Text = "";
                this.typecob.Enabled = this.materialcost_tb.Enabled = false;
                
            }
            else
            {
                this.typecob.Enabled = this.materialcost_tb.Enabled = true;
            }
        }

        private bool IsNumbers(string str)
        {
            try
            {
                double d = Convert.ToDouble(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void materialcost_tb_TextChanged(object sender, EventArgs e)
        {
            string materialcost = this.materialcost_tb.Text.ToString();
            if (materialcost == string.Empty)
            {
                return;
            }
            else if (IsNumbers(materialcost) == false)
            {
                MessageBox.Show(this.label1.Text.ToString()+"只允许输入数字或小数!");
                this.materialcost_tb.Text = "";
            }
        }

        private void techcost_tb_TextChanged(object sender, EventArgs e)
        {
            string techhr = this.techcost_tb.Text.ToString();
            if (techhr == string.Empty)
            {
                return;
            }
            else if (IsNumbers(techhr) == false)
            {
                MessageBox.Show(this.label11.Text.ToString() + "只允许输入数字或小数!");
                this.techcost_tb.Text = "";
            }
        }

        /// <summary>
        /// 修改原因编号控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reason_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comcode_tb.Clear();
            object reasonvalue = this.reason_cb.SelectedValue;
            if (IsNumber(reasonvalue))
            {
                if (Convert.ToInt16(reasonvalue) > 1 && Convert.ToInt16(reasonvalue) < 6)
                {
                    this.comcode_tb.Enabled = true;
                }
                else
                {
                    this.comcode_tb.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 判断object是否是数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsNumber(object obj)
        {
            try
            {
                Convert.ToDecimal(obj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

     }
}
