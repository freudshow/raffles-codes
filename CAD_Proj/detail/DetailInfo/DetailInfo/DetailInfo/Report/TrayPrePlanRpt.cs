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
    public partial class TrayPrePlanRpt : Form
    {
        public TrayPrePlanRpt()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            TrayPrePlan TPP = new TrayPrePlan();

            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                TPP.SetDataSource(ds);
                crystalReportViewer1.ReportSource = TPP ;
            }
        }

        private DataSet GetDs()
        {
            TrayPrePlanDs ds = new TrayPrePlanDs();
            string strSql = @"select t.*, t.rowid from tray_preplan_tab t";
            string whereSql = User.traypreplan;
            strSql += whereSql;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "TRAY_PREPLAN_TAB");
            conn.Close();
            return ds;
        }

        private void TrayPrePlanRpt_Activated(object sender, EventArgs e)
        {
            MDIForm.pMainWin.toolStrip1.Items[0].Enabled = false;
        }
    }
}