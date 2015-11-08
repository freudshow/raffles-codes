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
    public partial class frmParaLog : Form
    {

        string projectid;

        public string ProjectId
        {
            get { return projectid; }
            set { projectid = value; }
        }
        string partno;

        public string Part_No
        {
            get { return partno; }
            set { partno = value; }
        }
        string _disciplineid;
        public string DisciplineId
        {
            get { return _disciplineid; }
            set { _disciplineid = value; }
        }
        DataSet ds;
        public frmParaLog()
        {
            InitializeComponent();
            
            
        }

        private void frmParaLog_Load(object sender, EventArgs e)
        {
            //StringBuilder sb = new StringBuilder();

            //string spec_namestr = PartParameter.GetSpecName(activity);
            string sqlSelect =  "SELECT * from MM_PART_PARAMETER_TAB where projectid='" + ProjectId + "' and part_no=" + Part_No + "  and discipline =" + DisciplineId +"order by createdate";;
            //string wheresql = sb.ToString();
            ds = PartParameter.QueryPartPara(sqlSelect);
            dgv1.DataSource = ds.Tables[0].DefaultView;
            SetrowStyle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string fdate =this.dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //DateTime tdate =  Convert.ToDateTime(this.dateTimePicker1.Value.AddDays(1).ToString("yyyy-MM-dd"));
            string datesr = "to_char(createdate,'yyyy-mm-dd')='" + fdate+"'";
            string sqlSelect = " order by t.createdate";
            
            ds = PartParameter.QueryPartPara(sqlSelect);
            dgv1.DataSource = ds.Tables[0];
            SetrowStyle();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlSelect = " order by t.createdate";
            //string wheresql = sb.ToString();
            ds = PartParameter.QueryPartPara(sqlSelect);
            dgv1.DataSource = ds.Tables[0];
            SetrowStyle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv1.RowCount == 0) return;
            pb1.Visible = true;
            bool restr = PartParameter.ExportToTxt(dgv1,pb1);
            pb1.Visible = false;
        }

        private void SetrowStyle()
        {
            int rownum = dgv1.RowCount;
            if (rownum > 0)
            {
                dgv1.Rows[rownum - 1].DefaultCellStyle.Font = new Font("ËÎÌו", 9, FontStyle.Bold);
                dgv1.Rows[rownum - 1].DefaultCellStyle.ForeColor = Color.Yellow;
                dgv1.Rows[rownum - 1].DefaultCellStyle.BackColor = Color.Red;
            }
 
        }
    }
}