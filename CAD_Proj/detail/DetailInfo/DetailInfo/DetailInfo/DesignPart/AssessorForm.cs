using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace DetailInfo
{
    public partial class AssessorForm : Form
    {
        public AssessorForm()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }
        public string pid;
        public string PID
        {
            get { return pid; }
            set { pid = value; }
        }

        public int count;
        public string personstr = "";
        private void button1_Click(object sender, EventArgs e)
        {
            count = approvedgv.Rows.Count;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(approvedgv.Rows[i].Cells["审核人"].Value.ToString()+",");
            }
            personstr = sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            return;
        }

        private void AssessorForm_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql1 = "select (SELECT s.INDEXNAME FROM projectapproveindex s WHERE s.IND = t.index_id) 审核级别,  t.assesor 审核人 from projectapprove t where t.projectid = '"+pid+"' order by t.index_id";
            User.DataBaseConnect(sql1,ds);
            approvedgv.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
        }

    }
}