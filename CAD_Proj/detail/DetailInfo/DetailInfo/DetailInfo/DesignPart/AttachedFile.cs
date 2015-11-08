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
using Microsoft.Office.Interop.Excel;



namespace DetailInfo
{
    public partial class AttachedFile : Form
    {
        //[DllImport("user32.dll")]
        //internal static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);

        //[DllImport("user32.dll")]
        //internal static extern int GetMenuItemCount(IntPtr hMenu);

        //[DllImport("user32.dll")]
        //internal static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        public AttachedFile()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile =System.Windows.Forms.Application.StartupPath + "\\Resources\\" + User.skinstr;
            //CloseButtonEnable();
        }

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


        private string traystart = string.Empty;
        private string trayend = string.Empty;
        private string partstart = string.Empty;
        private string partend = string.Empty;
        private string materialstart = string.Empty;
        private string materialend = string.Empty;
        private string weightstart = string.Empty;
        private string weightend = string.Empty;
        private string flangestart = string.Empty;
        private string flangend = string.Empty;

        private void AttachedFile_Load(object sender, EventArgs e)
        {
            this.AttachFiledgv.Rows.Add(5);
            this.AttachFiledgv.Rows[0].Cells[0].Value = "管系托盘表";
            this.AttachFiledgv.Rows[1].Cells[0].Value = "管系附件表";
            this.AttachFiledgv.Rows[2].Cells[0].Value = "附件材料表";
            this.AttachFiledgv.Rows[3].Cells[0].Value = "重量重心表";
            this.AttachFiledgv.Rows[4].Cells[0].Value = "腹板套料";

            this.Quitbtn.Text = "取消";

        }

        private void Quitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Startbtn_Click(object sender, EventArgs e)
        {
            this.Startbtn.Enabled = false;
            this.Quitbtn.Enabled = false;
            ArrayList imagelist = new ArrayList();

            object A = this.AttachFiledgv.Rows[0].Cells["StartPage"].Value;
            if (A != null)
            {
                traystart = A.ToString();
            }

            object B = this.AttachFiledgv.Rows[0].Cells["EndPage"].Value;
            if (B != null)
            {
                trayend = B.ToString();
            }

            object C = this.AttachFiledgv.Rows[1].Cells["StartPage"].Value;
            if (C != null)
            {
                partstart = C.ToString();
            }
            object D = this.AttachFiledgv.Rows[1].Cells["EndPage"].Value;
            if (D != null)
            {
                partend = D.ToString();
            }

            object E = this.AttachFiledgv.Rows[2].Cells["StartPage"].Value;
            if (E != null)
            {
                materialstart = E.ToString();
            }
            object F = this.AttachFiledgv.Rows[2].Cells["EndPage"].Value;
            if (F != null)
            {
                materialend = F.ToString();
            }

            object G = this.AttachFiledgv.Rows[3].Cells["StartPage"].Value;
            if (G != null)
            {
                weightstart = G.ToString();
            }
            object H = this.AttachFiledgv.Rows[3].Cells["EndPage"].Value;
            if (H != null)
            {
                weightend = H.ToString();
            }

            object J = this.AttachFiledgv.Rows[4].Cells["StartPage"].Value;
            if (J != null)
            {
                flangestart = J.ToString();   
            }
            object K = this.AttachFiledgv.Rows[4].Cells["EndPage"].Value;
            if (K != null)
            {
                flangend = K.ToString();
            }

            DownLoadFrontPage();

            string pdfTemplate = User.rootpath + "\\" + drawing + ".pdf";
            if (string.IsNullOrEmpty(pdfTemplate))
            {
                return;
            }
            string pdfnewfile = pdfTemplate.Substring(0, pdfTemplate.LastIndexOf('.'));
            string newFile = pdfnewfile + "new.pdf";
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            string cardstr = GetImageName();
            string[] cardno = cardstr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= cardno.Length - 1; i++)
            {
                string chartLoc = string.Format(@"\\ecdms\sign$\jpg\{0}.jpg", cardno[i]);
                imagelist.Add(chartLoc);
            }
            Single X = 0, Y = 43; 
            if (imagelist.Count == 0)
            {
                MessageBox.Show("系统没有查询到相关电子签名，请与管理员联系");
                return;
            }
            else if(imagelist.Count == 4)
            {
                foreach (string item in imagelist)
                {
                    iTextSharp.text.Image chartImg = iTextSharp.text.Image.GetInstance(item);
                    iTextSharp.text.pdf.PdfContentByte underContent;
                    iTextSharp.text.Rectangle rect;

                    try
                    {
                        rect = pdfReader.GetPageSizeWithRotation(1);
                        X = 190;
                        Y += 13;
                        chartImg.ScalePercent(20);
                        chartImg.SetAbsolutePosition(X, Y);
                        underContent = pdfStamper.GetOverContent(1);
                        underContent.AddImage(chartImg);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            else if (imagelist.Count == 5)
            {
                for (int i = 0; i < imagelist.Count; i++)
                {
                    iTextSharp.text.Image chartImg = iTextSharp.text.Image.GetInstance(imagelist[i].ToString());
                    iTextSharp.text.pdf.PdfContentByte underContent;
                    iTextSharp.text.Rectangle rect;
                    rect = pdfReader.GetPageSizeWithRotation(1);
                    chartImg.ScalePercent(20);
                    if (i == 0)
                    {
                        X = 190;Y = 56;
                    }
                    else if (i == 1)
                    {
                        X = 190; Y = 69;
                    }
                    else if (i == 2)
                    {
                        X = 180; Y = 82;
                    }
                    else if (i == 3)
                    {
                        X = 205; Y = 82;
                    }
                    else if (i == 4)
                    {
                        X = 190; Y = 95;
                    }
                    chartImg.SetAbsolutePosition(X, Y);
                    underContent = pdfStamper.GetOverContent(1);
                    underContent.AddImage(chartImg);
                }
            }

            else if (imagelist.Count == 6)
            {
                for (int i = 0; i < imagelist.Count; i++)
                {
                    iTextSharp.text.Image chartImg = iTextSharp.text.Image.GetInstance(imagelist[i].ToString());
                    iTextSharp.text.pdf.PdfContentByte underContent;
                    iTextSharp.text.Rectangle rect;
                    rect = pdfReader.GetPageSizeWithRotation(1);
                    chartImg.ScalePercent(20);
                    if (i == 0)
                    {
                        X = 190; Y = 56;
                    }
                    else if (i == 1)
                    {
                        X = 180; Y = 69;
                    }
                    else if (i == 2)
                    {
                        X = 205; Y = 69;
                    }
                    else if (i == 3)
                    {
                        X = 180; Y = 82;
                    }
                    else if (i == 4)
                    {
                        X = 205; Y = 82;
                    }
                    else if (i == 5)
                    {
                        X = 190; Y = 95;
                    }
                    chartImg.SetAbsolutePosition(X, Y);
                    underContent = pdfStamper.GetOverContent(1);
                    underContent.AddImage(chartImg);
                }
            }

            InsertCharacteristics(pdfStamper, traystart, 340, 394);
            InsertCharacteristics(pdfStamper, trayend, 395, 394);

            InsertCharacteristics(pdfStamper, partstart, 340, 383);
            InsertCharacteristics(pdfStamper, partend, 395, 383);

            InsertCharacteristics(pdfStamper, materialstart, 340, 371);
            InsertCharacteristics(pdfStamper, materialend, 395, 371);

            InsertCharacteristics(pdfStamper, weightstart, 340, 360);
            InsertCharacteristics(pdfStamper, weightend, 395, 360);

            InsertCharacteristics(pdfStamper, flangestart, 340, 348);
            InsertCharacteristics(pdfStamper, flangend, 395, 348);

            pdfStamper.Close();
            pdfReader.Close();
            FileInfo fi = new FileInfo(pdfTemplate);
            fi.Delete();
            File.Move(newFile, pdfTemplate);

            Thread.Sleep(2000);
            string comText = "UPDATE SP_CREATEPDFDRAWING SET UPDATEDFRONTPAGE = :dfd WHERE PROJECTID = '" + projectid + "' AND DRAWINGNO = '" + drawing + "' and flag = 'Y'";
            InsertFrontPage.UpdateFrontPage(comText, User.rootpath + "\\" + drawing + ".pdf");
            FileInfo file = new FileInfo(pdfTemplate);
            file.Delete();

            this.Quitbtn.Enabled = true;
            this.Quitbtn.Text = "完成";

        }

        private void InsertCharacteristics(PdfStamper Stamper, string str, int X, int Y)
        {
            iTextSharp.text.pdf.PdfContentByte underContent;
            underContent = Stamper.GetOverContent(1);

            PdfTemplate template = underContent.CreateTemplate(500, 300);
            template.BeginText();
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            template.SetFontAndSize(bf, 9);
            template.SetTextMatrix(100, 100);
            template.ShowText(str);
            template.EndText();
            underContent.AddTemplate(template, X, Y);

        }


        private void DownLoadFrontPage()
        {
            using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandText = "select * from  SP_CREATEPDFDRAWING WHERE 1=1 AND PROJECTID = '" + projectid + "' AND DRAWINGNO = '" + drawing + "' and flag = 'Y'";
                OracleDataReader dr = command.ExecuteReader();
                string filepath = string.Empty;
                while (dr.Read())
                {
                    if (dr["FRONTPAGE"] != null)//如果文章内容为空 不能转二进制
                    {
                        try
                        {
                            byte[] b1 = (byte[])dr["FRONTPAGE"];

                            filepath = User.rootpath + "\\" + dr["DRAWINGNO"] + ".pdf";
                            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(b1, 0, b1.Length);
                            bw.Close();
                            fs.Close();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("系统没有查询到相关文件，请与管理员联系导入文件");
                            return;
                        }
                    }

                }
                dr.Close();
            }
        }

        public string GetImageName()
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = new DataSet();
            //string sqlstr = "SELECT T.ICARD FROM USER_TAB T WHERE T.STATE = 'NORMAL' AND T.NAME IN (SELECT S.ASSESSOR FROM DRAWING_APPROVETEMPLATE_TAB S WHERE S.DRAWING_ID IN (SELECT A.DRAWING_ID FROM PROJECT_DRAWING_TAB A WHERE A.LASTFLAG = 'Y' AND A.DRAWING_NO = '" + drawing + "' and A.delete_flag = 'N'))";
            string sqlstr = string.Format(@"select T.ICARD
  from USER_TAB T,
       (SELECT 0 INDEX_ID, A.RESPONSIBLE_USER ASSESSOR
          FROM PROJECT_DRAWING_TAB A
         WHERE A.LASTFLAG = 'Y'
           AND A.DRAWING_NO = '{0}' 
           and A.delete_flag = 'N'
        UNION ALL
        SELECT S.INDEX_ID, S.ASSESSOR
          FROM DRAWING_APPROVETEMPLATE_TAB S
         WHERE S.DRAWING_ID IN (SELECT A.DRAWING_ID
                                  FROM PROJECT_DRAWING_TAB A
                                 WHERE A.LASTFLAG = 'Y'
                                   AND A.DRAWING_NO = '{0}' 
                                   and A.delete_flag = 'N')) U
 WHERE U.ASSESSOR = T.NAME
   and T.STATE = 'NORMAL'
 ORDER BY U.INDEX_ID DESC", drawing);
            User.DataBaseConnect(sqlstr, ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append(ds.Tables[0].Rows[i][0].ToString() + ',');
            }
            ds.Dispose();
            return sb.ToString();
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

        /// <summary>
        /// 生成附页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdditonPageBtm_Click(object sender, EventArgs e)
        {
            //

        }
       

    }
}
