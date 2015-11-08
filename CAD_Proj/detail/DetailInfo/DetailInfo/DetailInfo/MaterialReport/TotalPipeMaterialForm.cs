using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using CrystalDecisions.Shared;
using System.IO;

namespace DetailInfo.MaterialReport
{
    public partial class TotalPipeMaterialForm : Form
    {
        public TotalPipeMaterialForm()
        {
            InitializeComponent();
        }

        private string _drawingno;
        public string DrawingNo
        { get; set; }
        private string _projectno;
        public string ProjectNo
        { get; set; }
        private int _dtortzd;
        public int Flag
        { get; set; }

        private TotalPipeMaterialCR cr;
        private void TotalPipeMaterialForm_Load(object sender, EventArgs e)
        {

   


           // string sql = "SELECT * FROM fun_pipematerial_lsh where DRAWINGNO='" + DrawingNo + "' and projectid='" + ProjectNo + "'" + DTTZD + " order  by material desc ";
            //string DBConfig_sql = "Data Source=oidsnew;User ID=plm;Password=123!feed;Unicode=True";

            string sql = "";
     
            if (Flag == 0)
            {
                sql = "SELECT * FROM fun_pipematerial_lsh where DRAWINGNO='" + DrawingNo + "' and projectid='" + ProjectNo + "' and modifydrawingno is null  order  by material desc ";
            }
            if (Flag == 1)
            {
                sql = "SELECT * FROM fun_pipematerial_lsh where MODIFYDRAWINGNO='" + DrawingNo + "' and projectid='" + ProjectNo + "' and modifydrawingno is not  null  order  by material desc ";

            }

   



            DataSet ds = new DataSet();
            OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
            OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            OracleDataAdapter sqlAd = new OracleDataAdapter();
            sqlAd.SelectCommand = sqlCmd;
            sqlAd.Fill(ds, "sql");
         
                cr = new TotalPipeMaterialCR();
                cr.Load(Application.StartupPath + "CrystalReport1.rpt");
                cr.SetDataSource(ds.Tables["sql"]);
                crystalReportViewer1.ReportSource = cr;



          

                //传递参数
                ParameterFields paramFields = crystalReportViewer1.ParameterFieldInfo;
                ParameterField paramFrame = paramFields["drawingno"];
                ParameterValues FcurValues = paramFrame.CurrentValues;
                ParameterDiscreteValue discreteValueF = new ParameterDiscreteValue();
                discreteValueF.Value = DrawingNo;
                FcurValues.Add(discreteValueF);


                crystalReportViewer1.ParameterFieldInfo = paramFields;
                crystalReportViewer1.Refresh();
        }

        private void ExportPDF()
        {
            try
            {

                string exportPath;
                string fileName;
                exportPath = User.rootpath + "\\" + DrawingNo + "附页"; 
                if (!Directory.Exists(exportPath))
                {
                    System.IO.Directory.CreateDirectory(exportPath);
                }

                fileName = "TotalPipeMaterialCR.pdf";

                CrystalDecisions.Shared.DiskFileDestinationOptions opts = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                //导出为磁盘文件
                CrystalDecisions.Shared.ExportOptions myExportOptions = cr.ExportOptions;
                myExportOptions.DestinationOptions = opts;
                myExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                //导出为pdf格式
                cr.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                //目的路径
                fileName = exportPath + "\\" + fileName;
                opts.DiskFileName = fileName;
                if (File.Exists(fileName))
                    File.Delete(fileName);
                //导出操作
                cr.Export();
                MessageBox.Show("--------转换成功---------");
                button1.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("导出PDF时：" + ex.Message.ToString());

                this.Close();
            }
        }
  
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ExportPDF();
        }

      
    }
}
