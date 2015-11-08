using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CIMCPPSPL
{
    public partial class Form1 : Form
    {
        public string spoolname, projectid;
        public Form1(string spname,string pjname)
        {
            spoolname = spname;
            projectid = pjname;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请填写备注！！");
                return;
            }
            else
            {
                string sqlstr = "update spool_tab set flowstatusremark='" + textBox1.Text + "'  where spoolname = '" + spoolname + "' and projectid = '" + projectid + "'" +
                                "and locked= 1 and flowstatus=10";
                if (MessageBox.Show("确定不合格备注无误吗？确定继续，取消重新填写", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int flag = User.RUNSQLCommon(sqlstr);
                    this.DialogResult = DialogResult.Yes;
                }

                
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlstr = "update spool_tab set flowstatus=7,flowstatusremark='" + textBox1.Text + "'  where spoolname = '" + spoolname + "' and projectid = '" + projectid + "'" +
                                "and locked= 1 and flowstatus=10";
            int flag = User.RUNSQLCommon(sqlstr);
            //this.DialogResult = DialogResult.No;
            //this.Close();
            //this.Dispose();
            
        }
    }
}