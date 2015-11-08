using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;


namespace DetailInfo
{
    public partial class GenerateMaterialForm : Form
    {
        public GenerateMaterialForm()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = System.Windows.Forms.Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        delegate void SetValueCallback(int i);

        delegate void SetValue(string[] p1, string p2);

        private List<string> filename = new List<string>();

        private string projectid;

        public string Projectid
        {
            get { return projectid; }
            set { projectid = value; }
        }

        private string drawing;

        public string Drawing
        {
            get { return drawing; }
            set { drawing = value; }
        }

        private int indicator;

        public int Indicator
        {
            get { return indicator; }
            set { indicator = value; }
        }

        private int version;

        public int Version
        {
            get { return version; }
            set { version = value; }
        }


        private static string pageNoStr = string.Empty;
        private static int NoCount = 0;

        private void MaterialPDFbtn_Click(object sender, EventArgs e)
        {
            this.Closebtn.Enabled = false;
            this.backgroundWorker1.RunWorkerAsync();
            string MergePath = User.rootpath + "\\" + drawing + "附页";
            
            if (!Directory.Exists(MergePath) && listBox1.Items.Count == 0)
            {
                this.Closebtn.Enabled = true;
                MessageBox.Show("系统没有检测到PDF格式的材料附页信息", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (Directory.Exists(MergePath) && listBox1.Items.Count == 0)
            {
                filename = GetPdfFiles(MergePath);
            }
            else if (Directory.Exists(MergePath) && listBox1.Items.Count != 0)
            {
                filename = GetPdfFiles(MergePath);
                GetListBoxItems(listBox1, MergePath,filename);
            }

            else if (!Directory.Exists(MergePath) && listBox1.Items.Count != 0)
            {
                System.IO.Directory.CreateDirectory(MergePath);
                filename = GetPdfFiles(MergePath);
                GetListBoxItems(listBox1, MergePath,filename);
            }
            SetValue sv = new SetValue(MaterialMergePDF);
            sv.BeginInvoke(filename.ToArray(), MergePath + "\\" + drawing + "-附页" + ".pdf", null, null);
            this.InsertPageNo.Enabled = true;
            this.MergePDFbtn.Enabled = false;
        }

        /// <summary>
        /// 从LISTBOX获取要添加的PDF文件
        /// </summary>
        /// <param name="box"></param>
        /// <param name="DesPath"></param>
        private void GetListBoxItems(ListBox box, string DesPath,List<string> ListStr)
        {
            for (int i = 0; i < box.Items.Count; i++)
            {
                int slashIndex = box.Items[i].ToString().LastIndexOf("\\");
                string AddedFile = box.Items[i].ToString().Substring(slashIndex + 2);
                ListStr.Add(AddedFile);
                System.IO.File.Copy(box.Items[i].ToString(), DesPath + "\\" + AddedFile,true);
            }
        }

        /// <summary>
        /// 获取要合并的PDF文件名字
        /// </summary>
        /// <param name="DirFullPath"></param>
        /// <returns></returns>
        private List<string> GetPdfFiles(string DirFullPath)
        {
            List<string> filelist = new List<string>();
            DirectoryInfo mydir = new DirectoryInfo(DirFullPath);
            FileInfo[] files = mydir.GetFiles("*.pdf");
            List<FileInfo> fileInfoList = new List<FileInfo>();
            for (int i = 0; i < files.Length; i++)
            {
                fileInfoList.Add(files[i]);
            }
            fileInfoList.Sort(delegate(FileInfo f1, FileInfo f2) { return f1.CreationTime.CompareTo(f2.CreationTime); });
            foreach (FileInfo fi in fileInfoList)
            {
                filelist.Add(fi.Name);
            }
            string StrName = drawing + "-附页" + ".pdf";
            if (filelist.Contains(StrName))
            {
                filelist.Remove(StrName);
            }
            
            return filelist;
        }

        /// <summary>
        /// 合并材料附页成PDF格式
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="outMergeFile"></param>
        private void MaterialMergePDF(string[] fileList, string outMergeFile)
        {
            PdfReader reader;
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage newPage;
            double totalcount = fileList.Length;
            for (int i = 0; i < fileList.Length; i++)
            {
                int count = outMergeFile.Substring(outMergeFile.LastIndexOf("\\") + 1).Length;

                reader = new PdfReader(outMergeFile.Substring(0, outMergeFile.Length - count) + "\\" + fileList[i]);

                int iPageNum = reader.NumberOfPages;
                for (int j = 1; j <= iPageNum; j++)
                {
                    document.NewPage();
                    newPage = writer.GetImportedPage(reader, j);
                    cb.AddTemplate(newPage, 0, 0);
                }
                SetProcessBarValue((int)(Math.Round((i + 1) / totalcount, 2) * 100));
                reader.Close();
            }

            document.Close();
        }

        /// <summary>
        /// 异步开启进度条
        /// </summary>
        /// <param name="value"></param>
        private void SetProcessBarValue(int value)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetProcessBarValue);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                this.progressBar1.Value = value;
            }
        }

        private void InsertPageNo_Click(object sender, EventArgs e)
        {
            string MergePath = User.rootpath + "\\" + drawing + "附页";
            string pdfTemplate = MergePath + "\\" + drawing + "-附页" + ".pdf";
            if (string.IsNullOrEmpty(pdfTemplate))
            {
                return;
            }
            string pdfnewfile = pdfTemplate.Substring(0, pdfTemplate.LastIndexOf('.'));
            string newFile = pdfnewfile + "new.pdf";
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            int pageCount = pdfReader.NumberOfPages;
            pageNoStr = "共" + pageCount.ToString() + "页";
            InsertCharacteristics(pdfStamper, pageCount, pageNoStr, 415, 677, 350, 677, -80, 717, 418, 667);
            pdfStamper.Close();
            pdfReader.Close();
            FileInfo fi = new FileInfo(pdfTemplate);
            fi.Delete();
            System.IO.File.Move(newFile, pdfTemplate);
            string sql = "update sp_createpdfdrawing set MATERIALPDFPAGES = " + pageCount + " where PROJECTID = '" + projectid + "' and DRAWINGNO = '"+drawing+"' and FLAG = 'Y'";
            User.UpdateCon(sql, DataAccess.OIDSConnStr);
            this.Savebtn.Enabled = this.Previewbtn.Enabled = true;
            this.InsertPageNo.Enabled = false;
        }

        private void InsertCharacteristics(PdfStamper Stamper, int totalpages, string totalstr, int X, int Y, int Z, int W, int D_x, int D_y, int R_x, int R_y)
        {
            iTextSharp.text.pdf.PdfContentByte underContent;
            for (int i = 1; i <= totalpages; i++)
            {
                underContent = Stamper.GetOverContent(i);
                PdfTemplate template = underContent.CreateTemplate(500, 300);
                template.BeginText();
                BaseFont bf = BaseFont.CreateFont(Application.StartupPath + "\\Resources\\simkai.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                template.SetFontAndSize(bf, 9);
                template.SetTextMatrix(100, 100);
                template.ShowText(totalstr);
                template.EndText();
                underContent.AddTemplate(template, X, Y);

                string NoStr = "第" + i + "页";
                PdfTemplate newtemplate = underContent.CreateTemplate(500, 300);
                newtemplate.BeginText();
                BaseFont newbf = BaseFont.CreateFont(Application.StartupPath + "\\Resources\\simkai.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                newtemplate.SetFontAndSize(newbf, 9);
                newtemplate.SetTextMatrix(100, 100);
                newtemplate.ShowText(NoStr);
                newtemplate.EndText();
                underContent.AddTemplate(newtemplate, Z, W);


                string DrawingStr = "图号： " + drawing;
                PdfTemplate drawingtemplate = underContent.CreateTemplate(500, 300);
                drawingtemplate.BeginText();
                BaseFont drawingbf = BaseFont.CreateFont(Application.StartupPath + "\\Resources\\simkai.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                drawingtemplate.SetFontAndSize(drawingbf, 12);
                drawingtemplate.SetTextMatrix(100, 100);
                drawingtemplate.ShowText(DrawingStr);
                drawingtemplate.EndText();
                underContent.AddTemplate(drawingtemplate, D_x, D_y);

                string RevStr = "REV."+ version.ToString();
                PdfTemplate revtemplate = underContent.CreateTemplate(500, 300);
                revtemplate.BeginText();
                BaseFont revbf = BaseFont.CreateFont(Application.StartupPath + "\\Resources\\simkai.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                revtemplate.SetFontAndSize(revbf, 9);
                revtemplate.SetTextMatrix(100, 100);
                revtemplate.ShowText(RevStr);
                revtemplate.EndText();
                underContent.AddTemplate(revtemplate, R_x, R_y);

            }

        }

        /// <summary>
        /// 保存到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Savebtn_Click(object sender, EventArgs e)
        {
            this.Savebtn.Enabled = false;
            this.Previewbtn.Enabled = false;
            
            string MergePath = User.rootpath + "\\" + drawing + "附页";

            if (IsOpenedFile(MergePath + "\\" + drawing + "-附页" + ".pdf") == true)
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process item in ps)
                {
                    if (item.ProcessName == "AcroRd32")
                    {
                        item.Kill();
                    }
                }
            }

            InsertAttachedMaterial(MergePath + "\\" + drawing + "-附页" + ".pdf");


            Thread.Sleep(2500);
            System.IO.Directory.Delete(MergePath, true);
            this.Closebtn.Enabled = true;
            this.Closebtn.Text = "完成";
        }

        /// <summary>
        /// 预览合成的材料附件的PDF文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Previewbtn_Click(object sender, EventArgs e)
        {
            string MergePath = User.rootpath + "\\" + drawing + "附页";
            System.Diagnostics.Process.Start(MergePath + "\\" + drawing + "-附页" + ".pdf");
       }

        #region
        private void DownLoadFiles(string sqlstr, string filepath)
        {
            //OracleDataReader dr = null;
            //OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            //conn.Open();
            //try
            //{
            //    OracleCommand cmd = conn.CreateCommand();
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = sqlstr;
            //    dr = cmd.ExecuteReader();
            //    byte[] File = null;
            //    if (dr.Read())
            //    {
            //        File = (byte[])dr[0];
            //    }
            //    FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            //    BinaryWriter bw = new BinaryWriter(fs);

            //    bw.Write(File, 0, File.Length);

            //    bw.Close();
            //    fs.Close();
            //}
            //catch (OracleException ex)
            //{
            //    MessageBox.Show(ex.Message, ToString());
            //}

            //finally
            //{
            //    conn.Close();
            //}
        }

        //private XmlDocument XLSConvertToPDF(string sourceFile, string worksheet)
        //{
            //XmlDocument excelData = new XmlDocument();
            //DataSet excelTableDataSet = new DataSet();
            //StreamReader excelContent = new StreamReader(sourceFile, System.Text.Encoding.Default);
            //string stringConnectToExcelFile = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourceFile + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
            //System.Data.OleDb.OleDbConnection oleConnectionToExcelFile = new System.Data.OleDb.OleDbConnection(stringConnectToExcelFile);
            //oleConnectionToExcelFile.Open();
            //System.Data.OleDb.OleDbDataAdapter oleDataAdapterForGetExcelTable = new System.Data.OleDb.OleDbDataAdapter(string.Format("select * from " + worksheet + ""), oleConnectionToExcelFile);
            //try
            //{
            //    oleDataAdapterForGetExcelTable.Fill(excelTableDataSet);
            //}
            //catch
            //{
            //    return null;
            //}
            //MessageBox.Show("----------------aaaaaa-----------------");
            //string excelOutputXml = Path.GetTempFileName();
            //excelTableDataSet.WriteXml(excelOutputXml);
            //excelData.Load(excelOutputXml);
            //File.Delete(excelOutputXml);
            //return excelData;
            //return null;
        //}
        #endregion

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 添加新目标文件到LISTVIEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.InitialDirectory = "D:";
            ofdialog.Filter = "Supported Image Types (*.pdf)|*.pdf";
            if (ofdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.listBox1.Items.Add(ofdialog.FileName.ToString());
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateMaterialForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string pathstr = User.rootpath + "\\" + drawing + "附页";
            if (Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
            {
                foreach (string file in Directory.GetFiles(pathstr))
                {
                    System.IO.File.Delete(file);
                }
                Directory.Delete(pathstr, true); //新建文件夹   
            }
        }

        /// <summary>
        /// 判断文件是否处于打开状态
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool IsOpenedFile(string file)
        {
            bool result = false;
            try
            {
                FileStream fs = File.OpenWrite(file);
                fs.Close();
            }
            catch (SystemException ex)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 把合并后的材料附件PDF文件插入到数据库
        /// </summary>
        /// <param name="drawingpath"></param>
        private void InsertAttachedMaterial(string drawingpath)
        {
            string sqlStr = "SELECT COUNT(*) FROM SP_CREATEPDFDRAWING T WHERE T.PROJECTID = '" + projectid + "' AND T.DRAWINGNO = '" + drawing + "' AND T.FLAG = 'Y' ";
            object rowcount = User.GetScalar(sqlStr, DataAccess.OIDSConnStr);
            if (int.Parse(rowcount.ToString()) == 0)
            {
                MessageBox.Show("数据库没有查询到该图纸记录，可重新在CATIA端打印封页！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BinaryReader reader = null;
            FileStream myfilestream = new FileStream(drawingpath, FileMode.Open, FileAccess.Read);
            try
            {
                reader = new BinaryReader(myfilestream);
                byte[] file = reader.ReadBytes((int)myfilestream.Length);
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        if (indicator == 0)
                        {
                            cmd.CommandText = "update SP_CREATEPDFDRAWING set MATERIALPDF = :dfd where   projectid = '" + projectid + "' and drawingno = '" + drawing + "' and flag = 'Y'";
                        }
                        else if (indicator == 1)
                        {
                            //cmd.CommandText = "update SP_CREATEPDFDRAWING set MODIFYMATERIALPDF = :dfd where projectid = '" + projectid + "' and drawingno = (select distinct drawingno from sp_spool_tab where MODIFYDRAWINGNO = '"+drawing+"' and flag = 'Y') and flag = 'Y'";
                            cmd.CommandText = "update SP_CREATEPDFDRAWING set MODIFYMATERIALPDF = :dfd   where projectid = '" + projectid + "' and drawingno = '" + drawing + "' and flag = 'Y'";

                        }
                        OracleParameter op = new OracleParameter("dfd", OracleType.Blob);
                        op.Value = file;
                        if (file.Length == 0)
                        {
                            MessageBox.Show("插入文档不能为空！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        else
                        {
                            cmd.Parameters.Add(op);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                    myfilestream.Close();
                    conn.Close();

                }
            }
            catch (IOException ee)
            {
                MessageBox.Show(ee.Message.ToString());
                return;
            }
            //finally
            //{
            //    if (reader != null)
            //    {
            //        reader.Close();
            //    }
            //}
        }

        /// <summary>
        /// 从listbox删除添加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            object[] selected_objs = new object[this.listBox1.SelectedItems.Count];
            this.listBox1.SelectedItems.CopyTo(selected_objs, 0);
            foreach (object item in selected_objs)
            {
                this.listBox1.Items.Remove(item);
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateMaterialStatus(projectid,drawing);
            DataSet MyDs = null;
            MyDs = GetMaterialRationDS("SP_GETMATERIALRATION", projectid, drawing, indicator);
            MultiInsertData(MyDs);
        }

        /// <summary>
        /// 获取材料设备定额表数据集
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="projectid"></param>
        /// <param name="drawing"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DataSet GetMaterialRationDS(string queryString, string projectid, string drawing, int flag)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("V_CS", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("projectid_in", OracleType.VarChar).Value = projectid;
            cmd.Parameters["projectid_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("drawing_in", OracleType.VarChar).Value = drawing;
            cmd.Parameters["drawing_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("flag_in", OracleType.VarChar).Value = flag;
            cmd.Parameters["flag_in"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 保存材料设备定额表到数据库
        /// </summary>
        /// <param name="ds"></param>
        private void MultiInsertData(DataSet ds)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接
            conn.Open();
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            try
            {
                cmd.CommandText = "INSERT INTO SP_MATERIALEQUIPRATION_TAB (PROJECTID,NAME,ERPCODE,NORM,WEIGHT,WEIGHTUNIT,LENCOUNT,LENCOUNTUNIT,BLOCKNO,DRAWINGNO)  VALUES (:project_in, :name_in, :erpcode_in, :norm_in, :weight_in, :weightunit_in, :lencount_in, :lencountunit_in, :blockno_in, :drawingno_in)";
                cmd.Parameters.Add("project_in", OracleType.VarChar);
                cmd.Parameters.Add("name_in", OracleType.VarChar);
                cmd.Parameters.Add("erpcode_in", OracleType.VarChar);
                cmd.Parameters.Add("norm_in", OracleType.VarChar);
                cmd.Parameters.Add("weight_in", OracleType.Number);
                cmd.Parameters.Add("weightunit_in", OracleType.VarChar);
                cmd.Parameters.Add("lencount_in", OracleType.Number);
                cmd.Parameters.Add("lencountunit_in", OracleType.VarChar);
                cmd.Parameters.Add("blockno_in", OracleType.VarChar);
                cmd.Parameters.Add("drawingno_in", OracleType.VarChar);

                int dsrowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < dsrowcount; i++)
                {
                    cmd.Parameters["project_in"].Value = ds.Tables[0].Rows[i][0];
                    cmd.Parameters["name_in"].Value = ds.Tables[0].Rows[i][1];
                    cmd.Parameters["erpcode_in"].Value = ds.Tables[0].Rows[i][2];
                    cmd.Parameters["norm_in"].Value = ds.Tables[0].Rows[i][3];
                    cmd.Parameters["weight_in"].Value = ds.Tables[0].Rows[i][4];
                    cmd.Parameters["weightunit_in"].Value = ds.Tables[0].Rows[i][5];
                    cmd.Parameters["lencount_in"].Value = ds.Tables[0].Rows[i][6];
                    cmd.Parameters["lencountunit_in"].Value = ds.Tables[0].Rows[i][7];
                    cmd.Parameters["blockno_in"].Value = ds.Tables[0].Rows[i][8];
                    cmd.Parameters["drawingno_in"].Value = ds.Tables[0].Rows[i][9];
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void UpdateMaterialStatus(string project, string drawingno)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            string queryString = "SP_UpdateMaterialStatus";
            OracleTransaction trans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("project_in", OracleType.VarChar).Value = project;
            cmd.Parameters["project_in"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("drawingno_in", OracleType.VarChar).Value = drawingno;
            cmd.Parameters["drawingno_in"].Direction = System.Data.ParameterDirection.Input;

            cmd.Transaction = trans;

            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
