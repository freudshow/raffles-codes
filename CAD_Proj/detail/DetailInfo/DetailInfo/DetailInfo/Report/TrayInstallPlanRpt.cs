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
    public partial class TrayInstallPlanRpt : Form
    {
        public TrayInstallPlanRpt()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            TrayInstallPlan TIP = new TrayInstallPlan();

            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                TIP.SetDataSource(ds);
                crystalReportViewer1.ReportSource = TIP;
            }
        }

        private DataSet GetDs()
        {
            TrayInstallPlanDs ds = new TrayInstallPlanDs();
            string strSql = @"select t.*, t.rowid from tray_installation_tab t";
            string whereSql = User.trayinstallplan;
            strSql += whereSql;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "TRAY_INSTALLATION_TAB");
            conn.Close();
            return ds;
        }

        private void TrayInstallPlanRpt_Activated(object sender, EventArgs e)
        {
            MDIForm.pMainWin.toolStrip1.Items[0].Enabled = false;
        }
    }
}