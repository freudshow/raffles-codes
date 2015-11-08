using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DetailInfo.Categery;
using System.IO;
using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace DetailInfo.MaterialReport
{
    public partial class AccessoriesRpt : Form
    {
        public AccessoriesRpt()
        {
            InitializeComponent();
        }
        private string drawingno;

        public string Drawingno
        {
            get { return drawingno; }
            set { drawingno = value; }
        }
        private int flag;

        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        private string blockno;

        public string Blockno
        {
            get { return blockno; }
            set { blockno = value; }
        }
        private string project;

        public string Project
        {
            get { return project; }
            set { project = value; }
        }
        private Accessories tpm;
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            reportDataBind();
            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField1 = new ParameterField();
            ParameterDiscreteValue discreteVal1 = new ParameterDiscreteValue();
            paramField1.ParameterFieldName = "drawingno";
            discreteVal1.Value = drawingno;
            paramField1.CurrentValues.Add(discreteVal1);
            paramFields.Add(paramField1);

            ParameterField paramField2 = new ParameterField();
            ParameterDiscreteValue discreteVal2 = new ParameterDiscreteValue();
            paramField2.ParameterFieldName = "blockno";
            discreteVal2.Value = blockno;
            paramField2.CurrentValues.Add(discreteVal2);
            paramFields.Add(paramField2);

            ParameterField paramField3 = new ParameterField();
            ParameterDiscreteValue discreteVal3 = new ParameterDiscreteValue();
            paramField3.ParameterFieldName = "project";
            discreteVal3.Value = project;
            paramField3.CurrentValues.Add(discreteVal3);
            paramFields.Add(paramField3);
            crystalReportViewer1.ParameterFieldInfo = paramFields;
        }
        private DataSet GetDs()
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand("sp_getacceemp", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("p_cursor", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("drawing_no", OracleType.VarChar).Value = drawingno;
            cmd.Parameters["drawing_no"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("flag", OracleType.Number).Value = flag;
            cmd.Parameters["flag"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            AccessoriesDS ds = new AccessoriesDS();
            adapter.Fill(ds, "ACCESSORIES_TAB");
            ////List<Acceemp> acceemplist = Acceemp.Find(drawingno, flag);
            ////ds.Tables.Add(new DataTable());
            ////ds.Tables[0].Columns.Add(new DataColumn(""));
            ////ds.Tables[0].Columns.Add(new DataColumn(""));
            ////ds.Tables[0].Columns.Add(new DataColumn(""));
            ////ds.Tables[0].Columns.Add(new DataColumn(""));
            ////for (int i = 0; i < acceemplist.Count; i++)
            ////{
            ////    if (ds.Tables[0].Rows.Count == 0)
            ////    {
            ////        if (acceemplist[i].DoubleScrewBolt=="B")
            ////            ds.Tables[0].Rows.Add("螺栓", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            ////        else 
            ////            ds.Tables[0].Rows.Add("双头螺柱", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            ////        ds.Tables[0].Rows.Add("螺母", acceemplist[i].NutStandard, (acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].NutWeight.ToString());
            ////        ds.Tables[0].Rows.Add("垫片", acceemplist[i].GasketStandard, acceemplist[i].TotalNum.ToString(), string.Empty);
            ////    }
            ////    else
            ////    {
            ////        string flag1 = "N";
            ////        string flag2 = "N";
            ////        string flag3 = "N";
            ////        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            ////        {
            ////            if (acceemplist[i].DoubleScrewBolt == "B"&&ds.Tables[0].Rows[j][0].ToString() == "螺栓" && ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].BoltStandard)
            ////            {
            ////                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString();
            ////                flag1 = "Y";
            ////            }
            ////            if (acceemplist[i].DoubleScrewBolt == "DB"&&ds.Tables[0].Rows[j][0].ToString() == "双头螺柱" && ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].BoltStandard)
            ////            {
            ////                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString();
            ////                flag1 = "Y";
            ////            }
            ////            if (ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].NutStandard)
            ////            {
            ////                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString();
            ////                flag2 = "Y";
            ////            }
            ////            if (ds.Tables[0].Rows[j][1].ToString() == acceemplist[i].GasketStandard)
            ////            {
            ////                ds.Tables[0].Rows[j][2] = (Convert.ToInt32(ds.Tables[0].Rows[j][2]) + acceemplist[i].TotalNum).ToString();
            ////                flag3 = "Y";
            ////            }
            ////        }
            ////        if (flag1 == "N")
            ////        {
            ////            if (acceemplist[i].DoubleScrewBolt == "B")
            ////            {
            ////                ds.Tables[0].Rows.Add("螺栓", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            ////            }
            ////            else
            ////            {
            ////                ds.Tables[0].Rows.Add("双头螺柱", acceemplist[i].BoltStandard, (acceemplist[i].BoltNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].BoltWeight.ToString());
            ////            }
            ////        }
            ////        if (flag2 == "N")
            ////            ds.Tables[0].Rows.Add("螺母", acceemplist[i].NutStandard, (acceemplist[i].NutNumber * acceemplist[i].TotalNum).ToString(), acceemplist[i].NutWeight.ToString());
            ////        if (flag3 == "N")
            ////            ds.Tables[0].Rows.Add("垫片", acceemplist[i].GasketStandard, acceemplist[i].TotalNum.ToString(), string.Empty);
            ////    }
            ////}
            return ds;
        }

        private void reportDataBind()
        {
            
            tpm = new Accessories();
            DataSet ds = GetDs();
            if (ds.Tables[0].Rows.Count != 0)
            {
                tpm.SetDataSource(ds);
                crystalReportViewer1.ReportSource = tpm;
            }
        }

        /// <summary>
        /// 转成PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToPDFbtn_Click(object sender, EventArgs e)
        {
            try
            {
                //reportDataBind();
                string ExportPath;
                string Fname;
                ExportPath = User.rootpath + "\\" + drawingno +"附页";
                if (!Directory.Exists(ExportPath))
                {
                    System.IO.Directory.CreateDirectory(ExportPath);
                }

                Fname = "Accessories";
                CrystalDecisions.Shared.DiskFileDestinationOptions opts = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                //导出为磁盘文件
                CrystalDecisions.Shared.ExportOptions myExportOptiions = tpm.ExportOptions;
                myExportOptiions.ExportDestinationOptions = opts;
                myExportOptiions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                tpm.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                Fname = Fname + ".PDF";
                opts.DiskFileName = ExportPath + "\\"+ Fname;
                tpm.Export();
                MessageBox.Show("--------转换成功---------");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
