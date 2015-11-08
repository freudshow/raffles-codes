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
    public partial class ProjectPipeProgressRpt : Form
    {
        public ProjectPipeProgressRpt()
        {
            InitializeComponent();
            ProjectBind();
            crystalReportViewer1.DisplayGroupTree = false;
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                ProjectPipeProgress ProPipePrg = new ProjectPipeProgress();
                ProPipePrg.SetDataSource(GetDs());
                crystalReportViewer1.ReportSource = ProPipePrg;
            }
            ds.Dispose();
        }
        private DataSet GetDs()
        {
            ProjectPipeProgressDs ds = new ProjectPipeProgressDs();
            #region Sql”Ôæ‰
            string strSql = @"select sysid,
       nvl(sysname,0)sysname,
       nvl(init_count,0)init_count,
       nvl(approving_count,0)approving_count,
       nvl(approved_count,0)approved_count,
       nvl(assigning_count,0)assigning_count,
       nvl(processing_count,0)processing_count,
       nvl(feedback_count,0)feedback_count,
       nvl(feedbackd_count,0)feedbackd_count,
        nvl(testing_count,0)testing_count,
        nvl(apfeedback_count,0)apfeedback_count,
        nvl(failure_count,0)failure_count,
        nvl(wset_count,0)wset_count,
        nvl(seting_count,0)seting_count,
        nvl(wdebug_count,0)wdebug_count,
        nvl(debuged_count,0)debuged_count,
        nvl(debugfail_count,0)debugfail_count,
       nvl(sum_count,0)sum_count
  from (select sysid, sysname
          from sp_system
         where project_id = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
         group by sysid, sysname) s
  left join (select systemid, count(t.rowid) init_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 0
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) init on s.sysid = init.systemid
  left join (select systemid, count(t.rowid) approving_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 1
                and t.projectid ='" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) approving on s.sysid = approving.systemid
  left join (select t.systemid, count(t.rowid) approved_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 2
                and t.projectid ='" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) approved on s.sysid = approved.systemid
  left join (select t.systemid, count(t.rowid) assigning_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 3
                and t.projectid ='" +toolStripComboBox1 .ComboBox .Text .ToString ()+@"'
              group by t.systemid) assigning on s.sysid = assigning.systemid
              left join (select t.systemid, count(t.rowid) processing_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 4
                and t.projectid ='" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) processing on s.sysid = processing.systemid
               left join (select t.systemid, count(t.rowid) feedback_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 5
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) feedback on s.sysid = feedback.systemid
               left join (select t.systemid, count(t.rowid) feedbackd_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 6
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) feedbackd on s.sysid = feedbackd.systemid
               left join (select t.systemid, count(t.rowid) testing_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 7
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) testing on s.sysid = testing.systemid
               left join (select t.systemid, count(t.rowid) dealdesign_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 8
                and t.projectid ='" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) dealdesign on s.sysid = dealdesign.systemid
               left join (select t.systemid, count(t.rowid) apfeedback_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 9
                and t.projectid ='" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) apfeedback on s.sysid = apfeedback.systemid
               left join (select t.systemid, count(t.rowid) failure_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 10
                and t.projectid = '" + toolStripComboBox1.ComboBox.ValueMember.ToString() + @"'
              group by t.systemid) failure on s.sysid = failure.systemid
               left join (select t.systemid, count(t.rowid) wset_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 11
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) wset on s.sysid = wset.systemid
               left join (select t.systemid, count(t.rowid) seting_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 12
                and t.projectid ='" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) seting on s.sysid = seting.systemid
               left join (select t.systemid, count(t.rowid)wdebug_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 13
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) wdebug on s.sysid = wdebug.systemid
               left join (select t.systemid, count(t.rowid) debuged_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 14
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) debuged on s.sysid = debuged.systemid
               left join (select t.systemid, count(t.rowid) debugfail_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.flowstatus = 15
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) debugfail on s.sysid = debugfail.systemid
  left join (select t.systemid, count(t.rowid) sum_count
               from SP_SPOOL_TAB t
              where t.flag = 'Y'
                and t.projectid = '" + toolStripComboBox1.ComboBox.Text.ToString() + @"'
              group by t.systemid) s2 on s.sysid = s2.systemid
";
            #endregion
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "ProjectPipeProgress");
            conn.Close();
            return ds;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                ProjectPipeProgress ProPipePrg = new ProjectPipeProgress();
                ProPipePrg.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ProPipePrg;
            }

        }
        private void ProjectBind()
        {
            DataSet ds1 = new DataSet();
            ds1= User.getds("select distinct projectid,projectid from SP_SPOOL_TAB t");
            //User.DataBaseConnect("select distinct projectid from SP_SPOOL_TAB t", ds1);
            toolStripComboBox1.ComboBox.DataSource = ds1.Tables [0];
            toolStripComboBox1.ComboBox.ValueMember = "projectid";
            toolStripComboBox1.ComboBox.DisplayMember = "projectid";
            toolStripComboBox1.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

       

        private void toolStripComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                ProjectPipeProgress ProPipePrg = new ProjectPipeProgress();
                ProPipePrg.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ProPipePrg;
            }
            ds.Dispose();
        }

        private void ProjectPipeProgressRpt_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
        }

       


      
    }
}