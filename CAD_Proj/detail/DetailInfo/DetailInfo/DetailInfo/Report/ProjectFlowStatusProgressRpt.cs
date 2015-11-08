using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace DetailInfo.Report
{
    public partial class ProjectFlowStatusProgressRpt : Form
    {
        public ProjectFlowStatusProgressRpt()
        {
            InitializeComponent();
            crystalReportViewer1.DisplayGroupTree = false;
            ProjectBind();
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                ProjectFlowStatusProgress PFSP = new ProjectFlowStatusProgress();
                PFSP.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PFSP;
            }
        }

        private DataSet GetDs()
        {
            ProjectFlowStatusProgressDs ds = new ProjectFlowStatusProgressDs();
            string strSql = @"select f.id, f.name ,nvl(p.count,0) count
  from spflowstatus_tab f
  left join (select flowstatus, count(*) count
               from SP_SPOOL_TAB t where projectid='" + toolStripComboBox1.ComboBox.Text.ToString() + @"' group by flowstatus
              ) p on f.id = p.flowstatus
order by f.id";
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "ProjectFlowStatusProgress");
            conn.Close();
            return ds;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ProjectFlowStatusProgress PFSP = new ProjectFlowStatusProgress();
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                PFSP.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PFSP;
            }

        }
        private void ProjectBind()
        {
            DataSet ds1 = new DataSet();
            ds1 = User.getds("select distinct projectid,projectid from SP_SPOOL_TAB t");
            toolStripComboBox1.ComboBox.DataSource = ds1.Tables[0];
            toolStripComboBox1.ComboBox.ValueMember = "projectid";
            toolStripComboBox1.ComboBox.DisplayMember = "projectid";
            toolStripComboBox1.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void toolStripComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            ProjectFlowStatusProgress PFSP = new ProjectFlowStatusProgress();
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                PFSP.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PFSP;
            }

        }

        private void ProjectFlowStatusProgressRpt_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
        }

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {

        }

        
    }
}