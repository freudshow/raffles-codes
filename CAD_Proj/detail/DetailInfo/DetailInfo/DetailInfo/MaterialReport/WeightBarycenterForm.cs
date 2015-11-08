using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Collections;
using CrystalDecisions.Shared;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
namespace DetailInfo.MaterialReport
{
    public partial class WeightBarycenterForm : Form
    {
        public WeightBarycenterForm()
        {
            InitializeComponent();
        }
        private string DrawingNo;

        public string _DrawingNo
        {
            get { return DrawingNo; }
            set { DrawingNo = value; }
        }
        private string ProjectId;

        public string _ProjectId
        {
            get { return ProjectId; }
            set { ProjectId = value; }
        }
        private int flag;

        public int _Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        ArrayList xp = new ArrayList();
        ArrayList zp = new ArrayList();
        ArrayList xpF = new ArrayList();
        ArrayList zpF = new ArrayList();
        ArrayList SF = new ArrayList();
        ArrayList EF = new ArrayList();

        double minx = 0;
        int m;
        double xsum = 0;
        double ysum = 0;
        double zsum = 0;
        //static string DBConfig_sql = "Data Source=oidsnew;User ID=plm;Password=123!feed;Unicode=True";
        OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
        private WeightBarycenterCR cr;
        private void WeightBarycenterForm_Load(object sender, EventArgs e)
        {
            //推模式
            string sql="";
            if (flag == 0)
            {
                sql = "SELECT * FROM SP_SPOOL_TAB where DRAWINGNO='" + DrawingNo + "'and FLAG='Y' and ProjectId='" + ProjectId + "'";
            }
            if (flag == 1)
            {
                sql = "SELECT * FROM SP_SPOOL_TAB where MODIFYDRAWINGNO='" + DrawingNo + "'and FLAG='Y' and ProjectId='" + ProjectId + "'and MODIFYDRAWINGNO is not null";
            }
            DataSet ds = new DataSet();
            OracleConnection sqlCon = new OracleConnection(DataAccess.OIDSConnStr);
            OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            OracleDataAdapter sqlAd = new OracleDataAdapter();
            sqlAd.SelectCommand = sqlCmd;
            sqlAd.Fill(ds, "sql");
            cr = new WeightBarycenterCR();
            //cr.Load(Application.StartupPath + "CrystalReport1.rpt");
            cr.SetDataSource(ds.Tables["sql"]);
            crystalReportViewer1.ReportSource = cr;
            SetArray();
            xsum = weightcenter("XPOS");
            ysum = weightcenter("YPOS");
            zsum = weightcenter("ZPOS");
            xpF[0] = paixu(xsum, xp, xpF, zp, zpF, SF, EF);
            
            //传递参数
            ParameterFields drawparam = crystalReportViewer1.ParameterFieldInfo;
            ParameterField drawparamFrame = drawparam["DRAWING_MODIFY_NO"];
            ParameterValues drawcurValues = drawparamFrame.CurrentValues;
            ParameterDiscreteValue discreteValued = new ParameterDiscreteValue();
            discreteValued.Value = DrawingNo;
            drawcurValues.Add(discreteValued);

            ParameterFields paramFields = crystalReportViewer1.ParameterFieldInfo;
            ParameterField paramFrame = paramFields["肋位："];
            ParameterValues FcurValues = paramFrame.CurrentValues;
            ParameterDiscreteValue discreteValueF = new ParameterDiscreteValue();
            discreteValueF.Value = xpF[0].ToString() + " " + Math.Round(minx, 2);
            FcurValues.Add(discreteValueF);

            ParameterField paramH = paramFields["高度:"];
            ParameterValues HcurValues = paramH.CurrentValues;
            ParameterDiscreteValue discreteValueH = new ParameterDiscreteValue();

            for (m = 1; m <= zpF.Count; m++)
            {
                zpF[m - 1] = paixu(zsum, zp, zpF, xp, xpF, SF, EF);
                if (Convert.ToDouble(xpF[m - 1].ToString().Substring(2)) >= Convert.ToDouble(SF[m - 1].ToString().Substring(2)) && Convert.ToDouble(xpF[m - 1].ToString().Substring(2)) <= Convert.ToDouble(EF[m].ToString().Substring(2)))
                {
                    discreteValueH.Value = zpF[m - 1].ToString() + " " + Math.Round(minx, 2);
                    break;
                }

            }

            HcurValues.Add(discreteValueH);

            ParameterField paramMiddle = paramFields["距舯:"];
            ParameterValues McurValues = paramMiddle.CurrentValues;
            ParameterDiscreteValue discreteValueM = new ParameterDiscreteValue();
            discreteValueM.Value = Math.Round(ysum, 2);
            McurValues.Add(discreteValueM);          
            crystalReportViewer1.ParameterFieldInfo = paramFields;
            crystalReportViewer1.Refresh();
            //ReplaceExportButton();
      
        }
        public void SetArray()
        {
            int i;
            string sql = "SELECT * FROM SP_FRAME_TAB where  ProjectId='" + ProjectId + "'";
            DataSet dsf = new DataSet();
            //OracleConnection sqlCon = new OracleConnection(DBConfig_sql);
            OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            OracleDataAdapter sqlAd = new OracleDataAdapter();
            sqlAd.SelectCommand = sqlCmd;
            sqlAd.Fill(dsf, "sqlf");
            for (i = 0; i < dsf.Tables["sqlf"].Rows.Count; i++)
            {
                if (dsf.Tables["sqlf"].Rows[i]["DECK"].ToString()!= "#")
                {
                    zp.Add(dsf.Tables["sqlf"].Rows[i]["LOCATION1"].ToString());
                    zpF.Add(dsf.Tables["sqlf"].Rows[i]["DECK"].ToString());
                    SF.Add(dsf.Tables["sqlf"].Rows[i]["SECTIONSTART"].ToString());
                    EF.Add(dsf.Tables["sqlf"].Rows[i]["ENDAT"].ToString());
                }
                xp.Add(dsf.Tables["sqlf"].Rows[i]["LOCATION2"].ToString());
                xpF.Add(dsf.Tables["sqlf"].Rows[i]["FRAME"].ToString());
            }
        }
        String paixu(double pos, ArrayList xparr, ArrayList xfarr, ArrayList zparr, ArrayList zfarr, ArrayList sfarr, ArrayList efarr)
        {
            int i;
            int j;
            object minabs;
            for (i = 0; i < xparr.Count; i++)
            {
                for (j = i; j < xparr.Count; j++)
                {

                    if (Math.Abs(Convert.ToDouble(pos - Convert.ToDouble(xparr[i]))) > Math.Abs(Convert.ToDouble(pos - Convert.ToDouble(xparr[j]))))
                    {
                        //minabs = Convert.ToDouble(min[i]);
                        //min[i] = min[j];
                        //min[j] = minabs;
                        minabs = Convert.ToString(xparr[i]);
                        xparr[i] = xparr[j];
                        xparr[j] = minabs;
                        minabs = Convert.ToString(xfarr[i]);
                        xfarr[i] = xfarr[j];
                        xfarr[j] = minabs;
                        if (xparr.Count < zparr.Count)
                        {
                            minabs = Convert.ToString(zparr[i]);
                            zparr[i] = zparr[j];
                            zparr[j] = minabs;
                            minabs = Convert.ToString(zfarr[i]);
                            zfarr[i] = zfarr[j];
                            zfarr[j] = minabs;
                            minabs = Convert.ToString(sfarr[i]);
                            sfarr[i] = sfarr[j];
                            sfarr[j] = minabs;
                            minabs = Convert.ToString(efarr[i]);
                            efarr[i] = efarr[j];
                            efarr[j] = minabs;
                        }
                    }
                }
            }

            minx = Convert.ToDouble(Convert.ToDouble(pos - Convert.ToDouble(xparr[0])));
            return Convert.ToString(xfarr[0]);
        }
        double weightcenter(string pos)
        {
            int i;
            double sum = 0;
            double wt = 0;
            string sql="";
            if (flag == 0)
            {
                sql = "SELECT * FROM SP_SPOOL_TAB where DRAWINGNO='" + DrawingNo + "'and FLAG='Y' and ProjectId='" + ProjectId + "'";
            }
            if (flag == 1)
            {
                sql = "SELECT * FROM SP_SPOOL_TAB where MODIFYDRAWINGNO='" + DrawingNo + "'and FLAG='Y' and ProjectId='" + ProjectId + "'and MODIFYDRAWINGNO is not null";
            }
            DataSet ds = new DataSet();
            OracleCommand sqlCmd = new OracleCommand(sql, sqlCon);
            OracleDataAdapter sqlAd = new OracleDataAdapter();
            sqlAd.SelectCommand = sqlCmd;
            sqlAd.Fill(ds, "sql");
            for (i = 1; i <= ds.Tables["sql"].Rows.Count; i++)
            {
                if (ds.Tables["sql"].Rows[i - 1][pos] == DBNull.Value)
                {
                    ds.Tables["sql"].Rows[i - 1][pos] = 0;
                }
                if(ds.Tables["sql"].Rows[i - 1]["SPOOLWEIGHT"] == DBNull.Value)
                {
                    ds.Tables["sql"].Rows[i - 1]["SPOOLWEIGHT"] = 0;
                }
                sum = sum + Convert.ToDouble(ds.Tables["sql"].Rows[i - 1][pos]) ;
                //sum = sum + Convert.ToDouble(ds.Tables["sql"].Rows[i - 1][pos]);
                wt = wt + Convert.ToDouble(ds.Tables["sql"].Rows[i - 1]["SPOOLWEIGHT"]);

            }
            sum = sum / wt;
            return sum;
        }
        //private void EXPORTTOPDF(object sender, EventArgs e)
        //{
        //    try
        //    {  
        //        string exportPath;
        //        string fileName;
        //        exportPath = Application.StartupPath + "\\" + DrawingNo + "附页";
        //        if (!Directory.Exists(exportPath))
        //        {
        //            System.IO.Directory.CreateDirectory(exportPath);
        //        }

        //        fileName = "WeightBarycenterCR.pdf";

        //        CrystalDecisions.Shared.DiskFileDestinationOptions opts = new CrystalDecisions.Shared.DiskFileDestinationOptions();
        //        //导出为磁盘文件
        //        CrystalDecisions.Shared.ExportOptions myExportOptions = cr.ExportOptions;
        //        myExportOptions.DestinationOptions = opts;
        //        myExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
        //        //导出为pdf格式
        //        cr.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
        //        //目的路径
        //        fileName = exportPath + "\\" + fileName;
        //        opts.DiskFileName = fileName;
        //        if (File.Exists(fileName))
        //            File.Delete(fileName);
        //        //导出操作
        //        cr.Export();
        //        MessageBox.Show("--------转换成功---------");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("导出PDF时：" + ex.Message.ToString());

        //        this.Close();
        //    }
        //}
        //private void ReplaceExportButton()
        //{
        //    //遍历crystalReportViewer1控件里的控件
        //    foreach (object ctl in crystalReportViewer1.Controls)
        //    {
        //        string sControl = ctl.GetType().Name.ToString().ToLower();
        //        if (sControl == "toolstrip")
        //        {
        //            ToolStrip tab1 = (ToolStrip)ctl;
        //            for (int i = 0; i <= tab1.Items.Count - 1; i++)
        //            {
        //                if (tab1.Items[i].ToolTipText == "导出报表")
        //                {
        //                    ToolStripButton tbutton = new ToolStripButton();
        //                    Image img1 = tab1.Items[i].Image;
        //                    tab1.Items.Remove(tab1.Items[i]);
        //                    tbutton.Image = img1;
        //                    tbutton.ToolTipText = "导出报表";
        //                    tab1.Items.Insert(0, tbutton);
        //                    tbutton.Click += new System.EventHandler(this.EXPORTTOPDF);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string exportPath;
                string fileName;
                exportPath = Application.StartupPath + "\\" + DrawingNo + "附页";
                if (!Directory.Exists(exportPath))
                {
                    System.IO.Directory.CreateDirectory(exportPath);
                }

                fileName = "WeightBarycenterCR.pdf";

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

    }
}
