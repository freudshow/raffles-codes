using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Net.Mail;
namespace DetailInfo.MaterialReport
{
    public partial class SpPipeMaterialForm : Form
    {
        public SpPipeMaterialForm()
        {
            InitializeComponent();
        }

        private string drawing;

        public string Drawing
        {
            get { return drawing; }
            set { drawing = value; }
        }

        private int flag;

        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        private SpPipeMaterial BCPR ;
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            BCPR = new SpPipeMaterial();
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                BCPR.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BCPR;
            }

            //传递参数
            ParameterFields paramFields = crystalReportViewer1.ParameterFieldInfo;
            ParameterField paramFrame = paramFields["drawingno"];
            ParameterValues FcurValues = paramFrame.CurrentValues;
            ParameterDiscreteValue discreteValueF = new ParameterDiscreteValue();
            discreteValueF.Value = drawing;
            FcurValues.Add(discreteValueF);


            crystalReportViewer1.ParameterFieldInfo = paramFields;
            crystalReportViewer1.Refresh();

        }
        private DataSet GetDs()
        {
            SpPipeMaterialDS ds = new SpPipeMaterialDS();
            string strSql = "select t.* from spl_view t";
            string whereSql = User.bcProDrawSql;
            strSql += " "+whereSql;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = strSql;
            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds, "spl_view");
            conn.Close();
            return ds;
        }
        
        private void SpPipeMaterialForm_Load(object sender, EventArgs e)
        {
            BCPR = new SpPipeMaterial();
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                BCPR.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BCPR;
            }
            //crystalReportViewer1.DisplayGroupTree = false;
        }
        private void ExportPDF()
        {
            try
            {
                
                string exportPath;
                string fileName;
                exportPath = Application.StartupPath + "\\"+Drawing+"附页";
                if (!Directory.Exists(exportPath))
                {
                    System.IO.Directory.CreateDirectory(exportPath);
                }

                fileName = "SpPipeMaterial.pdf";

                CrystalDecisions.Shared.DiskFileDestinationOptions opts = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                //导出为磁盘文件
                CrystalDecisions.Shared.ExportOptions myExportOptions = BCPR.ExportOptions;
                myExportOptions.DestinationOptions = opts;
                myExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                //导出为pdf格式
                BCPR.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                //目的路径
                fileName = exportPath + "\\"+fileName;
                opts.DiskFileName = fileName;
                if (File.Exists(fileName))
                    File.Delete(fileName);
                //导出操作
                BCPR.Export();
                MessageBox.Show("--------转换成功---------");
                button1.Enabled = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show( "导出PDF时：" + ex.Message.ToString());
                
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ExportPDF();



            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
