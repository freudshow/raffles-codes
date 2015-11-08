using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace DetailInfo.Report
{
    public partial class SPLPIPEReprot : Form
    {
        public SPLPIPEReprot()
        {
            InitializeComponent();
        }

        private void SPLPIPEReprot_Load(object sender, EventArgs e)
        {
            SPLPipeCrystalReport spr = new SPLPipeCrystalReport();
            spr.SetDataSource(GetDs());
            crystalReportViewer1.ReportSource = spr;

            //ParameterFields paramFields = new ParameterFields();
            //ParameterField paramField1 = new ParameterField();
            //paramField1.ParameterFieldName = "Project";
            //DataSet ProjectDS = GetProject();
            //for (int i = 0; i < ProjectDS.Tables[0].Rows.Count; i++)
            //{
            //    ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
            //    discreteVal.Value = ProjectDS.Tables[0].Rows[i][0].ToString();
            //    paramField1.DefaultValues.Add(discreteVal);
            //}
            //paramFields.Add(paramField1);
            //crystalReportViewer1.ParameterFieldInfo = paramFields;
        }

        private DataSet GetDs()
        {
            SPLPIPEDataSet ds = new SPLPIPEDataSet();
            string strSql = "select * from SPL_VIEW  "+User.pipematerial+"";
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "SPLPIPE_TAB");
            conn.Close();
            return ds;
        }
        //private DataSet GetProject()
        //{
        //    DataSet ProjectDs = new DataSet();
        //    OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
        //    string sql="select distinct projectid from spool_tab ";
        //    ProjectDs = db.ExecuteDataSet(CommandType.Text, sql);
        //    return ProjectDs;

        //}

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void SPLPIPEReprot_Activated(object sender, EventArgs e)
        {
            MDIForm.pMainWin.toolStrip1.Items[0].Enabled = false;
        }
    }
}