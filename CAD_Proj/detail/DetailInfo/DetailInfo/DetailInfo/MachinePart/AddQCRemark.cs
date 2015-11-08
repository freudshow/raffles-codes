using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class AddQCRemark : Form
    {
        public AddQCRemark()
        {
            InitializeComponent();
        }

        public string ToQCRemark_str = "";

        private void btn_ToQCRemark_Click(object sender, EventArgs e)
        {
            ToQCRemark_str = tb_ToQCRemark.Text.ToString();
        }

        public string GetQCRemark()
        {
            string QCRemark_str = ToQCRemark_str;
            return QCRemark_str;
        }

        private void btn_ToQCCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}