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
    public partial class MaterAtt : Form
    {
        public MaterAtt()
        {
            InitializeComponent();
        }
        private DataSet GetDs()
        {
            MaterAttDS ds = new MaterAttDS();
            string strSql = "select * from materialattachment_view  " + User.pipepart + "";
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "materialattachment");
            conn.Close();
            return ds;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            MaterAttReport MaCR = new MaterAttReport();
            MaCR.SetDataSource(GetDs());
            crystalReportViewer1.ReportSource = MaCR;

        }

        private void MaterAtt_Activated(object sender, EventArgs e)
        {
            MDIForm.pMainWin.toolStrip1.Items[0].Enabled = false;
        }
    }
}