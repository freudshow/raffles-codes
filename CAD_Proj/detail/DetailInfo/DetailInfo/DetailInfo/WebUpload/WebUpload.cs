using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
namespace DetailInfo.WebUpload
{
    public partial class WebUpload : Form
    {
        public WebUpload()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 单个文件上传至服务器
        /// </summary>
        /// <param name="uriAddress">接收文件资源的URI, 例如: http://xxxx/Upload.aspx</param>
        /// <param name="filePath">要发送的资源文件, 例如: @"D:\workspace\WebService 相关.doc</param>
        /// <returns>返回文件保存的相对路径, 例如: "upload/xxxxx.jpg" 或者出错返回 ""</returns>
        private string UploadFile(string uriAddress, string filePath)
        {
            //利用 WebClient
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                byte[] responseArray = webClient.UploadFile(uriAddress, "POST", filePath);
                //webClient.DownloadData();
                string savePath = Encoding.UTF8.GetString(responseArray);
                return savePath;
            }
            catch (Exception)
            {
                return "上传失败";
            }
        }
        public bool UriExists(string url)
        {
            try
            {
                new WebClient().OpenRead(url);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "|.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string aimfile = openFileDialog1.FileName.ToString();
                textBox1.Text = aimfile;
                string filename = openFileDialog1.SafeFileName.ToString();

            }
            else
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeDrawingState("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        /// <summary>
        /// 改变图纸状态，从Planned到Approving状态，插入图纸状态变更的日志，添加打印申请
        /// </summary>
        /// <param name="drawingno"></param>
        private void ChangeDrawingState(string drawingno)
        {
            DialogResult result = MessageBox.Show("确定要上传吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string filename = openFileDialog1.SafeFileName.ToString();
                string Cuser = User.cur_user;

                string filepath = this.UploadFile("http://172.20.64.3/ClientUpload.aspx?drawingno=&filename=" + filename + "&user=" + Cuser, textBox1.Text);
                //MessageBox.Show(filepath);
            }
           
        }
        
    }
}
