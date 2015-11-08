using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class Add_Remark : Form
    {
        public Add_Remark()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        private void btn_remark_Click(object sender, EventArgs e)
        {
            string remark = tb_remark.Text.ToString().Trim();
        }
        public string remark_str = "";

        public string GetRemark()
        {
            string remark_str = tb_remark.Text.Trim();
            return remark_str;
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}