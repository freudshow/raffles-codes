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
    public partial class BlockConstructPlanRpt : Form
    {
        public BlockConstructPlanRpt()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
           
            BlockConstructPlan BCPR = new BlockConstructPlan();
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                BCPR.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BCPR;
            }
            crystalReportViewer1.DisplayGroupTree = false;
        }
        private DataSet GetDs()
        {
            BlockConstructPlanDs ds = new BlockConstructPlanDs();
            string strSql = "select * from blockconstructionplan_tab t";
            string whereSql = User.bcplanSql;
            strSql += whereSql;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "BLOCKCONSTRUCTIONPLAN_TAB");
            conn.Close();
            return ds;
        }

        private void BlockConstructPlanRpt_Activated(object sender, EventArgs e)
        {
            MDIForm.pMainWin.toolStrip1.Items[0].Enabled = false;
        }
    }
}