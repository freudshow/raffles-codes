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
    public partial class SpoolReport : Form
    {
        public SpoolReport()
        {
            InitializeComponent();
        }

        private void SpoolReport_Load(object sender, EventArgs e)
        {
            Spool_CrystalReport scr = new Spool_CrystalReport();
            scr.SetDataSource(GetDs());
            this.crystalReportViewer1.ReportSource = scr;
            
        }

        private DataSet GetDs()
        {
            SpoolDataSet ds = new SpoolDataSet();
            string strSql = "select * from SPOOLSTATISTICS_TAB";
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds,"SPOOLSTATISTICS_TAB");  
            conn.Close();
            return ds;
        }

     
    }
}