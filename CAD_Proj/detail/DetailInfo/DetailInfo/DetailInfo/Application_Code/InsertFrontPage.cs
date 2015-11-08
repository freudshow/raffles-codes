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

namespace DetailInfo
{
    class InsertFrontPage
    {

        public static string drawingno = string.Empty;
        //public static void GetDrawingno(string drawingstr)
        //{
        //    drawingno = drawingstr;
        //}
        /// <summary>
        /// 生成新的图纸封面并插入到数据库
        /// </summary>
        /// <param name="sql"></param>
        public static void GenerateFrontPage(string sql)
        {
            ArrayList imagelist = new ArrayList();
            string pdfTemplate = MyOpenFileDialog();
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
                string chartLoc = string.Format(@"\\172.16.7.55\sign$\jpg\{0}.jpg", cardno[i]);
                imagelist.Add(chartLoc);
            }

            if (imagelist.Count ==0)
            {
                MessageBox.Show("系统没有查询到相关电子签名，请与管理员联系");
                return;
            }

            Single X = 0, Y = 43; int pageCount = 0;
            foreach (string item in imagelist)
            {
                iTextSharp.text.Image chartImg = iTextSharp.text.Image.GetInstance(item);
                iTextSharp.text.pdf.PdfContentByte underContent;
                iTextSharp.text.Rectangle rect;

                try
                {
                    rect = pdfReader.GetPageSizeWithRotation(1);
                    //if (chartImg.Width > rect.Width || chartImg.Height > rect.Height)
                    //{
                    //    chartImg.ScaleToFit(rect.Width, rect.Height);
                    //    X = (rect.Width - chartImg.ScaledWidth) / 4;
                    //    Y += (rect.Height - chartImg.ScaledHeight) / 4;
                    //}

                    //else
                    //{
                    //    X = (rect.Width - chartImg.Width) / 4;
                    //    Y += (rect.Height - chartImg.Height) / 4;
                    //}
                    //else
                    //{
                    //X = (rect.Width - chartImg.Width) / 4 ;
                    X = 190;
                    //Y += (rect.Height - chartImg.Height) / 4;
                    Y -= 52;
                    //}
                    chartImg.ScalePercent(20);
                    chartImg.SetAbsolutePosition(X, Y);
                    pageCount = pdfReader.NumberOfPages;
                    for (int i = 1; i <= pageCount; i++)
                    {
                        underContent = pdfStamper.GetOverContent(1);
                        underContent.AddImage(chartImg);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            pdfStamper.Close();
            pdfReader.Close();
            FileInfo fi = new FileInfo(pdfTemplate);
            fi.Delete();
            File.Move(newFile, pdfTemplate);
           
            BinaryReader reader = null;
            FileStream myfilestream = new FileStream(pdfTemplate, FileMode.Open, FileAccess.Read);
            try
            {
                reader = new BinaryReader(myfilestream);
                byte[] file = reader.ReadBytes((int)myfilestream.Length);
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        //cmd.CommandText = "INSERT INTO CREATEPDFDRAWING (PROJECTID, DRAWINGNO, FRONTPAGE) VALUES ('" + pid + "', '" + drawno + "', :dfd)";
                        cmd.CommandText = sql;
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
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            MessageBox.Show("电子签名插入操作完成！");
        }

        /// <summary>
        /// 通过图纸号获取电子签名图片
        /// </summary>
        /// <returns></returns>
        public static string GetImageName()
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = new DataSet();
            string sqlstr = "SELECT T.ICARD FROM USER_TAB T WHERE T.STATE = 'NORMAL' AND T.NAME IN (SELECT S.ASSESSOR FROM DRAWING_APPROVETEMPLATE_TAB S WHERE S.DRAWING_ID IN (SELECT A.DRAWING_ID FROM PROJECT_DRAWING_TAB A WHERE A.LASTFLAG = 'Y' AND A.DRAWING_NO = '" + drawingno + "'))";
            User.DataBaseConnect(sqlstr, ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append(ds.Tables[0].Rows[i][0].ToString() + ',');
            }
            ds.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// 打开要插入电子签名的图纸封面
        /// </summary>
        /// <returns></returns>
        public static string MyOpenFileDialog()
        {
            string filepath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文档文件(*.pdf)| *.pdf";
            ofd.InitialDirectory = @"D:\";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filepath = ofd.FileName.ToString();
            }
            return filepath;
        }

        public static void UpdateFrontPage(string sqlstr, string path)
        {
            BinaryReader reader = null;
            FileStream myfilestream = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                reader = new BinaryReader(myfilestream);
                byte[] file = reader.ReadBytes((int)myfilestream.Length);
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        //cmd.CommandText = "INSERT INTO CREATEPDFDRAWING (PROJECTID, DRAWINGNO, FRONTPAGE) VALUES ('" + pid + "', '" + drawno + "', :dfd)";
                        cmd.CommandText = sqlstr;
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
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
