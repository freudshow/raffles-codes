using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using System.Threading;


namespace DetailInfo
{
    public partial class PackageDrawing : Form
    {

        //[DllImport("user32.dll")]
        //internal static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);
        
        //[DllImport("user32.dll")]
        //internal static extern int GetMenuItemCount(IntPtr hMenu);

        //[DllImport("user32.dll")]
        //internal static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        public PackageDrawing()
        {
            InitializeComponent();
            //CloseButtonEnable();
            this.skinEngine1.SkinFile = System.Windows.Forms.Application.StartupPath + "\\Resources\\" + User.skinstr;
            this.previewbtn.Enabled = false;
        }

        string projectid;

        public string Projectid
        {
            get { return projectid; }
            set { projectid = value; }
        }

        string drawing;

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

        private void mergebtn_Click(object sender, EventArgs e)
        {
            this.completebtn.Enabled = false;

            this.mergebtn.Enabled = false;

            if (indicator == 0)
            {
                string pathstr = User.rootpath + "\\" + drawing;

                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(pathstr); //新建文件夹   
                }

                string sqlpdf = "select PDFDRAWING from SP_CREATEPDFDRAWING where PROJECTID = '" + projectid + "' AND DRAWINGNO = '" + drawing + "' AND FLAG ='Y'";
                string pathpdf = pathstr + "\\" + drawing + ".pdf";
                DownLoadFiles(sqlpdf, pathpdf);

                string sqlexcel = "select MATERIALPDF from SP_CREATEPDFDRAWING where PROJECTID = '" + projectid + "' AND DRAWINGNO = '" + drawing + "' AND FLAG ='Y'";
                string pathexcel = pathstr + "\\" + drawing + "附页"+ ".pdf";
                DownLoadFiles(sqlexcel, pathexcel);

                //ZipFile zf = new ZipFile();
                //zf.Zip(destr + ".zip", 1, pathpdf, pathexcel);

                ZipHelper.CreateZip(pathstr, pathstr);

                if (Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                {
                    Directory.Delete(pathstr, true); //新建文件夹   
                }
            }

            if (indicator == 1)
            {
                //string pathstr = User.rootpath + "\\" + "modify_" +  drawing;
                string pathstr = User.rootpath + "\\" + drawing;
                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(pathstr); //新建文件夹   
                }

                //string sqlpdf = "select MODIFYDRAWINGS from SP_CREATEPDFDRAWING where PROJECTID = '" + projectid + "' AND DRAWINGNO = (select distinct drawingno from sp_spool_tab where flag = 'Y' and  MODIFYDRAWINGNO = '" + drawing + "') AND FLAG ='Y'";
                string sqlpdf = "select MODIFYDRAWINGS from SP_CREATEPDFDRAWING where PROJECTID = '" + projectid + "' AND DRAWINGNO =  '" + drawing + "' AND FLAG ='Y'";
                string pathpdf = pathstr + "\\" + drawing + ".pdf";
                DownLoadFiles(sqlpdf, pathpdf);

                //string sqlexcel = "select MODIFYMATERIALPDF from SP_CREATEPDFDRAWING where PROJECTID = '" + projectid + "' AND DRAWINGNO = (select distinct drawingno from sp_spool_tab where flag = 'Y' and MODIFYDRAWINGNO = '" + drawing + "') AND FLAG ='Y'";
                string sqlexcel = "select MODIFYMATERIALPDF from SP_CREATEPDFDRAWING where PROJECTID = '" + projectid + "' AND DRAWINGNO = '" + drawing + "' AND FLAG ='Y'";
                string pathexcel = pathstr + "\\" + drawing + "附页" + ".pdf";
                DownLoadFiles(sqlexcel, pathexcel);

                //ZipFile zf = new ZipFile();
                //zf.Zip(destr + ".zip", 1, pathpdf, pathexcel);

                ZipHelper.CreateZip(pathstr, pathstr);

                if (Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                {
                    Directory.Delete(pathstr, true); //新建文件夹   
                }
            }
            this.label3.Text = string.Format("提醒:{0}", "打包完成");
            this.completebtn.Enabled = true;
            this.previewbtn.Enabled = true;
            this.completebtn.Text = "完成";
        }

        private void DownLoadFiles(string sqlstr, string filepath)
        {
            OracleDataReader dr = null;
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            conn.Open();
            try
            {
                //OracleDataReader dr = null;
                //OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
                //conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                dr = cmd.ExecuteReader();
                byte[] File = null;
                if (dr.Read())
                {
                    File = (byte[])dr[0];
                }


                //string filepath = pathstr + "\\" + drawno + ".pdf";
                FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write(File, 0, File.Length);

                bw.Close();
                fs.Close();
                //conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message, ToString());
            }

            finally
            {
                conn.Close();
            }
        }

        private void PackageDrawing_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = projectid;
            this.textBox2.Text = drawing;
            this.label3.Text = string.Format("提醒:{0}", "打包需要几秒钟时间，请耐心等待...");
            this.completebtn.Text = "取消";
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

        private void completebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void previewbtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(User.rootpath + "\\" + drawing + ".zip");
        }


    }
}
