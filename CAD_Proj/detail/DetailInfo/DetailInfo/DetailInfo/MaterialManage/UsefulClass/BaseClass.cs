using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Framework;
namespace DetailInfo
{
    public class BaseClass
    {
        #region  绑定ComboBox控件
        /// <summary>
        /// 对ComboBox控件进行数据绑定
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <param name="M_str_tbMember">数据表中字段名</param>
        /// <param name="cbox">ComboBox控件ID</param>
        public static void cboxBind(string M_str_sqlstr, string M_str_tbValue, string M_str_tbMember, ComboBox cbox)
        {
            DataSet myds = PartParameter.QueryPartPara(M_str_sqlstr);
            DataRow rowdim = myds.Tables[0].NewRow();
            rowdim[0] = 0;
            myds.Tables[0].Rows.InsertAt(rowdim, 0);
            cbox.DataSource = myds.Tables[0].DefaultView;
            cbox.DisplayMember = M_str_tbMember;
            cbox.ValueMember = M_str_tbValue;
        }
        #endregion

        #region  绘制饼图
        /// <summary>
        /// 根据货物所占百分比画饼图
        /// </summary>
        /// <param name="objgraphics">Graphics类对象</param>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <param name="M_str_Num">数据表中货物数</param>
        /// <param name="M_str_tbGName">数据表中货物名称</param>
        /// <param name="M_str_title">饼图标题</param>
        public void drawPic(Graphics objgraphics, string M_str_sqlstr, string M_str_table, string M_str_Num, string M_str_tbGName, string M_str_title)
        {
            DataSet myds = PartParameter.QueryPartPara(M_str_sqlstr);
            float M_flt_total = 0.0f, M_flt_tmp;
            int M_int_iloop;
            for (M_int_iloop = 0; M_int_iloop < myds.Tables[0].Rows.Count; M_int_iloop++)
            {
                M_flt_tmp = Convert.ToSingle(myds.Tables[0].Rows[M_int_iloop][M_str_Num]);
                M_flt_total += M_flt_tmp;
            }
            Font fontlegend = new Font("verdana", 9), fonttitle = new Font("verdana", 10, FontStyle.Bold);//设置字体
            int M_int_width = 275;//白色背景宽
            const int Mc_int_bufferspace = 15;
            int M_int_legendheight = fontlegend.Height * (myds.Tables[0].Rows.Count + 1) + Mc_int_bufferspace;
            int M_int_titleheight = fonttitle.Height + Mc_int_bufferspace;
            int M_int_height = M_int_width + M_int_legendheight + M_int_titleheight + Mc_int_bufferspace;//白色背景高
            int M_int_pieheight = M_int_width;
            Rectangle pierect = new Rectangle(0, M_int_titleheight, M_int_width, M_int_pieheight);
            //加上各种随机色
            Bitmap objbitmap = new Bitmap(M_int_width, M_int_height);//创建一个bitmap实例
            objgraphics = Graphics.FromImage(objbitmap);
            ArrayList colors = new ArrayList();
            Random rnd = new Random();
            for (M_int_iloop = 0; M_int_iloop < myds.Tables[0].Rows.Count; M_int_iloop++)
                colors.Add(new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))));
            objgraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, M_int_width, M_int_height);//画一个白色背景
            objgraphics.FillRectangle(new SolidBrush(Color.LightYellow), pierect);//画一个亮黄色背景
            //以下为画饼图(有几行row画几个)
            float M_flt_currentdegree = 0.0f;
            for (M_int_iloop = 0; M_int_iloop < myds.Tables[0].Rows.Count; M_int_iloop++)
            {
                objgraphics.FillPie((SolidBrush)colors[M_int_iloop], pierect, M_flt_currentdegree,
                  Convert.ToSingle(myds.Tables[0].Rows[M_int_iloop][M_str_Num]) / M_flt_total * 360);
                M_flt_currentdegree += Convert.ToSingle(myds.Tables[0].Rows[M_int_iloop][M_str_Num]) / M_flt_total * 360;
            }
            //以下为生成主标题
            SolidBrush blackbrush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            objgraphics.DrawString(M_str_title, fonttitle, blackbrush, new Rectangle(0, 0, M_int_width, M_int_titleheight), stringFormat);
            objgraphics.DrawRectangle(new Pen(Color.Black, 2), 0, M_int_height - M_int_legendheight, M_int_width, M_int_legendheight);
            for (M_int_iloop = 0; M_int_iloop < myds.Tables[0].Rows.Count; M_int_iloop++)
            {
                objgraphics.FillRectangle((SolidBrush)colors[M_int_iloop], 5, M_int_height - M_int_legendheight + fontlegend.Height * M_int_iloop + 5, 10, 10);
                objgraphics.DrawString(((String)myds.Tables[0].Rows[M_int_iloop][M_str_tbGName]) + " —— "
                    + Convert.ToString(Convert.ToSingle(myds.Tables[0].Rows[M_int_iloop][M_str_Num]) * 100 / M_flt_total) + "%", fontlegend, blackbrush,
                20, M_int_height - M_int_legendheight + fontlegend.Height * M_int_iloop + 1);
            }
            objgraphics.DrawString("总货物数是：" + Convert.ToString(M_flt_total), fontlegend, blackbrush, 5, M_int_height - fontlegend.Height);
            string P_str_imagePath = Application.StartupPath.Substring(0, Application.StartupPath.Substring(0,
                Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\"));
            P_str_imagePath += @"\Image\image\" + DateTime.Now.ToString("yyyyMMddhhmss") + ".jpg";
            objbitmap.Save(P_str_imagePath, ImageFormat.Jpeg);
            objgraphics.Dispose();
            objbitmap.Dispose();
        }
        #endregion

        #region  文件压缩
        /// <summary>
        /// 文件压缩
        /// </summary>
        /// <param name="M_str_DFile">压缩前文件及路径</param>
        /// <param name="M_str_CFile">压缩后文件及路径</param>
        public void compressFile(string M_str_DFile, string M_str_CFile)
        {
            if (!File.Exists(M_str_DFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(M_str_DFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                byte[] buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                if (checkCounter != buffer.Length) throw new ApplicationException();
                using (FileStream destinationStream = new FileStream(M_str_CFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (GZipStream compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true))
                    {
                        compressedStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
        #endregion

        #region  验证文本框输入为数字
        /// <summary>
        /// 验证文本框输入为数字
        /// </summary>
        /// <param name="M_str_num">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool validateNum(string M_str_num)
        {
            return Regex.IsMatch(M_str_num, "^[0-9.-]*$");
        }
        #endregion

        #region  验证文本框输入为电话号码
        /// <summary>
        /// 验证文本框输入为电话号码
        /// </summary>
        /// <param name="M_str_phone">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool validatePhone(string M_str_phone)
        {
            return Regex.IsMatch(M_str_phone, @"\d{3,4}-\d{7,8}");
        }
        #endregion

        #region  验证文本框输入为传真号码
        /// <summary>
        /// 验证文本框输入为传真号码
        /// </summary>
        /// <param name="M_str_fax">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool validateFax(string M_str_fax)
        {
            return Regex.IsMatch(M_str_fax, @"86-\d{2,3}-\d{7,8}");
        }
        #endregion

        
    }


}
