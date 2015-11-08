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
    public partial class TrayMaterialDeliveryTimeRpt : Form
    {
        public TrayMaterialDeliveryTimeRpt()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            TrayMaterialDeliveryTime TMDT = new  TrayMaterialDeliveryTime ();
           
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                TMDT.SetDataSource(ds);
                crystalReportViewer1.ReportSource = TMDT;
            }

        }

        private DataSet GetDs()
        {
            TrayMaterialDeliveryTimeDs ds = new TrayMaterialDeliveryTimeDs();
            string strSql = @"select t.*, t.rowid from tray_material_deliverytime_tab t";
            string whereSql = User.traymatrialplan;
            strSql += whereSql;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "TRAY_MATERIAL_DELIVERYTIME_TAB");
            conn.Close();
            return ds;
        }

        private void TrayMaterialDeliveryTimeRpt_Activated(object sender, EventArgs e)
        {
            MDIForm.pMainWin.toolStrip1.Items[0].Enabled = false;
        }
    }
}