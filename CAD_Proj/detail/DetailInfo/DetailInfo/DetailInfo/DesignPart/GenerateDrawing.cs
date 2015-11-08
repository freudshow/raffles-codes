using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OracleClient;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DetailInfo
{
    public partial class GenerateDrawing : Form
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);

        [DllImport("user32.dll")]
        internal static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        internal static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        

        delegate void SetValueCallback(int i);

        delegate void SetValue(string[] p1, string p2);

        delegate void SetPageNoValue(PdfStamper stamper, int tCountNo, string CountStr, int x, int y);

        public GenerateDrawing()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = System.Windows.Forms.Application.StartupPath + "\\Resources\\" + User.skinstr;
            this.savebtn.Enabled = false;
            this.previewbtn.Enabled = false;
            this.InsertPageNobtn.Enabled = false;
            this.DelRowConSM.Items[0].Visible = false;
        }

        private string pid;

        public string Pid
        {
            get { return pid; }
            set { pid = value; }
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
        private int progress = 0;
        private static string pageNoStr = string.Empty;
        private int NoCount = 0;
        List<string> filename = new List<string>();
        public static string pathstr = string.Empty;
        private void mergebtn_Click(object sender, EventArgs e)
        {
            this.mergebtn.Enabled = false;
            this.cbtn.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
            //Thread.Sleep(5000);
            //SetValue sv = new SetValue(this.StartMerge);
            //sv.BeginInvoke(filename.ToArray(), pathstr + "\\" + drawing + ".pdf", null, null);

            //this.InsertPageNobtn.Enabled = true;
            this.toolStripStatusLabel1.Text = "提示：点击插入页码按钮后需几秒钟启动时间，请耐心等待！";

        }
        /// <summary>
        /// 下载PDF小票到本地
        /// </summary>
        private void DowdLoadSpool()
        {
            DataSet ds = new DataSet();
            if (indicator == 0)
            {
                pathstr = User.rootpath + "\\" + drawing;
                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(pathstr); //新建文件夹   
                }
                string spoolsql = "select SPOOLNAME from SP_SPOOL_TAB where FLAG = 'Y' and PROJECTID = '" + pid + "' and DRAWINGNO = '" + drawing + "'   order by SPOOLNAME";
                User.DataBaseConnect(spoolsql, ds);
            }

            else if (indicator ==1)
            {
                pathstr = User.rootpath + "\\"  + drawing;
                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(pathstr); //新建文件夹   
                }
                string spoolsql = "select SPOOLNAME from SP_SPOOL_TAB where FLAG = 'Y' and PROJECTID = '" + pid + "' and MODIFYDRAWINGNO = '" + drawing + "'  order by SPOOLNAME";
                User.DataBaseConnect(spoolsql, ds);
            }

            OracleDataReader dr = null;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string spoolstr = ds.Tables[0].Rows[i][0].ToString();
                cmd.CommandText = "select T.SPOOLPICTURE from SP_PDF_TAB T where T.FLAG = 'Y' AND T.PROJECTID = '" + pid + "' AND T.SPOOLNAME = '" + spoolstr + "'";
                dr = cmd.ExecuteReader();
                byte[] File = null;

                while (dr.Read())
                {
                    if (dr[0] != null)
                    {
                        try
                        {
                            File = (byte[])dr[0];
                            FileStream fs = new FileStream(pathstr + "\\" + spoolstr + ".pdf", FileMode.Create);
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(File, 0, File.Length);
                            bw.Close();
                            fs.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }


            }
            conn.Close();
        }

        private void GetPFDSpool()
        {
            string pdfPathSql = string.Empty;
            pathstr = User.rootpath + "\\" + drawing;
            if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
            {
                Directory.CreateDirectory(pathstr); //新建文件夹   
            }
            DataSet ds = new DataSet();
            if (indicator == 0)
            {
                pdfPathSql = @"select t.pdfpath,t.spoolname 
  from sp_pdf_tab t
 where t.spoolname in
       (select s.spoolname
          from sp_spool_tab s
         where s.flag = 'Y'
           and s.drawingno = '" + drawing + "' and s.projectid = '"+pid+"' ) and t.flag = 'Y' order by t.spoolname";
                User.DataBaseConnect(pdfPathSql,ds);
            }
            if (indicator == 1)
            {
                pdfPathSql = @"select t.pdfpath
  from sp_pdf_tab t
 where t.spoolname in
       (select s.spoolname
          from sp_spool_tab s
         where s.flag = 'Y'
           and s.MODIFYDRAWINGNO = '" + drawing + "' and s.projectid = '" + pid + "' ) and t.flag = 'Y' order by t.spoolname";
                User.DataBaseConnect(pdfPathSql, ds);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string pdfpath = ds.Tables[0].Rows[i][0].ToString();
                if (string.IsNullOrEmpty(pdfpath))
                {
                    MessageBox.Show("该小票缺少目标存储地址，请与管理员联系！");
                    continue;
                }
                string filenamestr = pdfpath.Substring(pdfpath.LastIndexOf("\\"));
                if (filenamestr.Contains("_"))
                {
                    filenamestr = filenamestr.Substring(filenamestr.IndexOf("_") + 1);
                    System.IO.File.Copy(pdfpath, pathstr + "\\" + filenamestr, true);
                }
                else
                {
                    System.IO.File.Copy(pdfpath, pathstr + "\\" + filenamestr, true);
                }
            }
            if (indicator == 0)
            {
                string drawingstatus = DBConnection.GetDrawingStatus("SP_GETDRAWINGSTATUS",drawing,version);
                if (drawingstatus == "1")
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string spoolstr = ds.Tables[0].Rows[j][1].ToString();
                        int page_no = j + 3;
                        DBConnection.InsertSpoolPageNo("SP_InsertSpoolPageNo", pid, drawing, spoolstr, page_no);
                    }
                }
            }
            ds.Dispose();
        }

        public  List<string> PdfFiles()
        {
            string sqlstr = string.Empty;
            List<string> filelist = new List<string>();
            DataSet ds = new DataSet();
            if (indicator == 0)
            {
                sqlstr = "select t.spoolname from SP_SPOOL_TAB t where  t.projectid = '" + pid + "' and t.drawingno = '" + drawing + "'  and t.flag = 'Y'  order by SPOOLNAME";
            }
            else if (indicator ==1)
            {
                sqlstr = "select t.spoolname from SP_SPOOL_TAB t where  t.projectid = '" + pid + "' and t.modifydrawingno = '" + drawing + "'  and t.flag = 'Y'  order by SPOOLNAME";
            }
            User.DataBaseConnect(sqlstr, ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                filelist.Add(ds.Tables[0].Rows[i][0].ToString() + ".pdf"); 
            }
            ds.Dispose();
            return filelist;
        }

        public void StartMerge(string[] fileList, string outMergeFile)
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

                try
                {
                    reader = new PdfReader(outMergeFile.Substring(0, outMergeFile.Length - count) + "\\" + fileList[i]);
                }
                catch (SystemException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    reader = null;
                }
                if (reader == null)
                {
                    continue;                    
                }

                int iPageNum = reader.NumberOfPages;
                for (int j = 1; j <= iPageNum; j++)
                {
                    document.NewPage();
                    newPage = writer.GetImportedPage(reader, j);
                    cb.AddTemplate(newPage, 0, 0);
                }
                
                if (indicator == 0)
                {
                    progress = fileList.Length - 2 + i;
                }
                else
                {
                    progress = fileList.Length - 1 + i;
                }
                this.backgroundWorker1.ReportProgress(progress, String.Format("当前值是 {0} ", progress));
                //SetProcessBarValue((int)(Math.Round((i + 1) / totalcount, 2) * 100));
                reader.Close();
            }
            
            document.Close();

        }

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

        private void InsertDrawings(string drawingpath)
        {
            BinaryReader reader = null;
            FileStream myfilestream = new FileStream(drawingpath, FileMode.Open, FileAccess.Read);
            try
            {
                reader = new BinaryReader(myfilestream);
                byte[] file = new byte[(int)myfilestream.Length];
                myfilestream.Read(file, 0, (int)myfilestream.Length); //读取二进制到缓冲区
                //byte[] file = reader.ReadBytes((int)myfilestream.Length); //把文件转成byte类型二进制流
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        if (indicator == 0)
                        {
                            cmd.CommandText = "update SP_CREATEPDFDRAWING set PDFDRAWING = :dfd where projectid = '" + pid + "' and drawingno = '" + drawing + "' and flag = 'Y'";
                        }
                        else if (indicator ==1)
                        {
                            cmd.CommandText = "update SP_CREATEPDFDRAWING set MODIFYDRAWINGS = :dfd where projectid = '" + pid + "' and drawingno = '" + drawing + "'  and flag = 'Y'";
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
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            this.savebtn.Enabled = false;
            this.previewbtn.Enabled = false;

            if (IsOpenedFile(User.rootpath + "\\" + drawing + "\\" + drawing + ".pdf") == true)
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

            InsertDrawings(User.rootpath + "\\" + drawing + "\\" + drawing + ".pdf");


            Thread.Sleep(3000);
            System.IO.Directory.Delete(User.rootpath + "\\" + drawing, true);
     
            this.cbtn.Enabled = true;
            this.cbtn.Text = "完成";
            this.toolStripStatusLabel1.Text = "提示：该图纸合并操作已全部完成！";
        }

        private void GenerateDrawing_Load(object sender, EventArgs e)
        {
            string sql = string.Empty;
            if (indicator == 0)
            {
                sql = "select count(*) from sp_spool_tab t where t.drawingno = '" + drawing + "' and t.flag ='Y' ";
                this.progressBar1.Maximum = Convert.ToInt32(User.GetScalar(sql, DataAccess.OIDSConnStr).ToString()) * 2 + 3;
            }
            else
            {
                sql = "select count(*) from sp_spool_tab t where t.modifydrawingno = '" + drawing + "' and t.flag ='Y' ";
                this.progressBar1.Maximum = Convert.ToInt32(User.GetScalar(sql, DataAccess.OIDSConnStr).ToString()) * 2 + 2;
            }
            this.cbtn.Text = "关闭";
        }

        //protected void CloseButtonEnable()
        //{
        //    //   默认窗口去除关闭按钮 
        //    const int MF_BYPOSITION = 0x00000400;

        //    IntPtr hWindow = this.Handle;
        //    IntPtr hMenu = GetSystemMenu(hWindow, false);
        //    int count = GetMenuItemCount(hMenu);
        //    RemoveMenu(hMenu, count - 1, MF_BYPOSITION);
        //    RemoveMenu(hMenu, count - 2, MF_BYPOSITION);
        //}

        private void cbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        ///预览图纸 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previewbtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(User.rootpath + "\\" + drawing + "\\" + drawing + ".pdf");
        }

        private bool IsOpenedFile(string file)
        {
            bool result = false;
            try
            {
                FileStream fs = File.OpenWrite(file);
                fs.Close();
            }
            catch(SystemException ex)
            {
                result = true;
            }
    
            return result;
        }


        private void InsertCharacteristics(PdfStamper Stamper, int totalpages, string totalstr,  int X, int Y, int Z, int W, int D_x, int D_y, int R_x, int R_y)
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

                //if (i == 2 || i == totalpages)
                if (pid == "COSL H256" && i == 2 && indicator == 0)
                {
                    string DrawingStr = "图号:"+ drawing;
                    PdfTemplate drawingtemplate = underContent.CreateTemplate(500, 300);
                    drawingtemplate.BeginText();
                    BaseFont drawingbf = BaseFont.CreateFont(Application.StartupPath + "\\Resources\\simkai.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    drawingtemplate.SetFontAndSize(drawingbf, 12);
                    drawingtemplate.SetTextMatrix(100, 100);
                    drawingtemplate.ShowText(DrawingStr);
                    drawingtemplate.EndText();
                    underContent.AddTemplate(drawingtemplate, D_x, D_y);

                    string RevStr = "REV." + version.ToString();
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

        }

        /// <summary>
        /// 插入页码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertPageNobtn_Click(object sender, EventArgs e)
        {
            //this.progressBar1.Value = 0;
            this.InsertPageNobtn.Enabled = false;
            string pdfTemplate = pathstr + "\\" + drawing + ".pdf";
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
            InsertCharacteristics(pdfStamper, pageCount, pageNoStr, 415, 677, 350, 677, -40, 707, 418,667);
            pdfStamper.Close();
            pdfReader.Close();
            FileInfo fi = new FileInfo(pdfTemplate);
            fi.Delete();
            System.IO.File.Move(newFile, pdfTemplate);
            this.InsertPageNobtn.Enabled = false;
            this.savebtn.Enabled = true;
            this.previewbtn.Enabled = true;
            MessageBox.Show("-------------------完成页码插入------------------");
            this.toolStripStatusLabel1.Text = "提示：点击保存到数据库按钮后需几秒钟时间完成，请耐心等待！";

        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateDrawing_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (IsOpenedFile(User.rootpath + "\\" + drawing + "\\" + drawing + ".pdf") == true)
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

                pathstr = User.rootpath + "\\" + drawing;
                if (Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                {
                    foreach (string file in Directory.GetFiles(pathstr))
                    {
                        System.IO.File.Delete(file);
                    }
                    Directory.Delete(pathstr, true); //新建文件夹   
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message.ToString() +"可尝试关闭所有打开的PDF文档！");
                return;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string pdfPathSql = string.Empty;
            pathstr = User.rootpath + "\\" + drawing;
            if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
            {
                Directory.CreateDirectory(pathstr); //新建文件夹   
            }
            DataSet ds = new DataSet();
            if (indicator == 0)
            {
                pdfPathSql = @"select t.pdfpath,t.spoolname 
  from sp_pdf_tab t
 where t.spoolname in
       (select s.spoolname
          from sp_spool_tab s
         where s.flag = 'Y'
           and s.drawingno = '" + drawing + "' and s.projectid = '" + pid + "' ) and t.flag = 'Y' order by t.spoolname";
                User.DataBaseConnect(pdfPathSql, ds);
            }
            if (indicator == 1)
            {
                pdfPathSql = @"select t.pdfpath
  from sp_pdf_tab t
 where t.spoolname in
       (select s.spoolname
          from sp_spool_tab s
         where s.flag = 'Y'
           and s.MODIFYDRAWINGNO = '" + drawing + "' and s.projectid = '" + pid + "' ) and t.flag = 'Y' order by t.spoolname";
                User.DataBaseConnect(pdfPathSql, ds);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string pdfpath = ds.Tables[0].Rows[i][0].ToString();
                if (string.IsNullOrEmpty(pdfpath))
                {
                    MessageBox.Show("该小票缺少目标存储地址，请与管理员联系！");
                    continue;
                }
                if (System.IO.File.Exists(@pdfpath))
                {
                    string filenamestr = pdfpath.Substring(pdfpath.LastIndexOf("\\"));
                    if (filenamestr.Contains("_"))
                    {
                        filenamestr = filenamestr.Substring(filenamestr.IndexOf("_") + 1);
                        System.IO.File.Copy(pdfpath, pathstr + "\\" + filenamestr, true);
                        //this.backgroundWorker1.ReportProgress(progressStr, String.Format("当前值是 {0} ", progressStr));
                        this.backgroundWorker1.ReportProgress(i + 1, String.Format("当前值是 {0} ", i + 1));
                    }
                    else
                    {
                        System.IO.File.Copy(pdfpath, pathstr + "\\" + filenamestr, true);
                        //this.backgroundWorker1.ReportProgress(progressStr, String.Format("当前值是 {0} ", progressStr));
                        this.backgroundWorker1.ReportProgress(i + 1, String.Format("当前值是 {0} ", i + 1));
                    }
                }
                else
                {
                    //MessageBox.Show("目标地址未能查找到该管路小票，请与管理员联系！");
                    continue;
                }
            }
            if (indicator == 0)
            {
                string drawingstatus = DBConnection.GetDrawingStatus("SP_GETDRAWINGSTATUS", drawing, version);
                if (drawingstatus == "1")
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string spoolstr = ds.Tables[0].Rows[j][1].ToString();
                        int page_no = j + 3;
                        DBConnection.InsertSpoolPageNo("SP_InsertSpoolPageNo", pid, drawing, spoolstr, page_no);
                    }
                }
            }
            ds.Dispose();

            pathstr = User.rootpath + "\\" + drawing;
            if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
            {
                MessageBox.Show("请确认小票是否已上传");
                return;
            }

            OracleDataReader dr = null;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (indicator == 0)
            {
                cmd.CommandText = "select UPDATEDFRONTPAGE from SP_CREATEPDFDRAWING where PROJECTID = '" + pid + "' AND DRAWINGNO = '" + drawing + "' and FLAG = 'Y'";
            }
            else
            {
                cmd.CommandText = "select MODIFYDRAWINGFRONTPAGE from SP_CREATEPDFDRAWING where PROJECTID = '" + pid + "' AND DRAWINGNO = '" + drawing + "' and FLAG = 'Y'";
            }
            dr = cmd.ExecuteReader();
            byte[] File = null;
            if (dr.Read())
            {
                File = (byte[])dr[0];
            }
            string filestr = User.rootpath + "\\" + drawing + "_frontpage" + ".pdf";
            FileStream fs = new FileStream(filestr, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(File, 0, File.Length);
            bw.Close();
            fs.Close();
            conn.Close();

            filename = PdfFiles();

            if (System.IO.File.Exists(pathstr + "\\" + drawing + "_frontpage" + ".pdf"))
            {
                filename.Insert(0, drawing + "_frontpage" + ".pdf");
                System.IO.File.Delete(filestr);
            }
            else
            {
                System.IO.File.Move(filestr, pathstr + "\\" + drawing + "_frontpage" + ".pdf");
                filename.Insert(0, drawing + "_frontpage" + ".pdf");
            }
            if (pid == "COSL H256")
            {
                if (indicator == 0)
                {
                    if (System.IO.File.Exists(pathstr + "\\" + drawing + "CSL4附图.pdf"))
                    {
                        filename.Insert(1, drawing + "CSL4附图.pdf");
                    }
                    else
                    {
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\CSL4附图.pdf", pathstr + "\\" + drawing + "CSL4附图.pdf");
                        filename.Insert(1, drawing + "CSL4附图.pdf");
                    }
                }
            }
            else
            {
                if (System.IO.File.Exists(pathstr + "\\" + drawing + "附图1.pdf"))
                {
                    filename.Insert(1, drawing + "附图1.pdf");
                }
                else
                {
                    System.IO.File.Copy(Application.StartupPath + "\\Resources\\附图1.pdf", pathstr + "\\" + drawing + "附图1.pdf");
                    filename.Insert(1, drawing + "附图1.pdf");
                }
            }
            if (pid == "COSL H256")
            {
                if (System.IO.File.Exists(pathstr + "\\" + drawing + "CSL4附图1.pdf"))
                {
                    filename.Add(drawing + "CSL4附图1.pdf");
                }
                else
                {
                    System.IO.File.Copy(Application.StartupPath + "\\Resources\\CSL4附图1.pdf", pathstr + "\\" + drawing + "CSL4附图1.pdf");
                    filename.Add(drawing + "CSL4附图1.pdf");
                }
            }
            else
            {
                if (System.IO.File.Exists(pathstr + "\\" + drawing + "附图2.pdf"))
                {
                    filename.Add(drawing + "附图2.pdf");
                }
                else
                {
                    System.IO.File.Copy(Application.StartupPath + "\\Resources\\附图2.pdf", pathstr + "\\" + drawing + "附图2.pdf");
                    filename.Add(drawing + "附图2.pdf");
                }
            }

            int totalpages = filename.Count;
            NoCount = totalpages + 1;
            if (pid == "COSL H256" && indicator == 1)
            {
                pageNoStr = "共" + NoCount.ToString() + "页";
            }
            else
            {
                pageNoStr = "共" + totalpages.ToString() + "页";
            }

            DirectoryInfo mydir = new DirectoryInfo(pathstr);
            FileInfo[] files = mydir.GetFiles();
            if (files.Length <= 1)
            {
                MessageBox.Show("数据库中暂没有查询到相关数据");
                this.Close();
                Directory.Delete(pathstr, true);
                return;
            }

            Thread.Sleep(1000);
            StartMerge(filename.ToArray(), pathstr + "\\" + drawing + ".pdf");

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.labelColor1.Text = e.UserState.ToString();
            this.labelColor1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InsertPageNobtn.Enabled = true;
        }
    }
}
