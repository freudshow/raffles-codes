using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;

namespace DigitalSign
{
    /// <summary>   
    /// 更新完成触发的事件   
    /// </summary>   
    public delegate void UpdateState();

    /// <summary>   
    /// 程序更新   
    /// </summary>   

    public class SoftUpdate
    {

        private string download;
        private const string updateUrl = "http://172.16.7.55/Services/PipingLifeUpdate/Update.xml";//升级配置的XML文件地址
        private List<string> newFilePath = new List<string>();
        private List<string> oldFilePath = new List<string>();
        /// <summary>
        /// 更新的文件个数
        /// </summary>
        public int updatefilecount = 0;
        /// <summary>
        /// 更新的文件名
        /// </summary>
        public List<string> updatefilename = new List<string>();
        /// <summary>
        /// 更新详细信息
        /// </summary>
        public string updateinfo;
        /// <summary>
        /// 当前版本
        /// </summary>
        public string currentverson;
        public bool isfinish;
        #region 构造函数

        public SoftUpdate() { }

        /// <summary>   
        /// 程序更新   
        /// </summary>   
        /// <param name="file">要更新的文件</param>   

        public SoftUpdate(string file, string softName)
        {
            this.LoadFile = file;
            this.SoftName = softName;
        }

        #endregion
        #region 属性

        private string loadFile;
        private string newVerson;
        private string oldVerson;
        private string softName;
        private bool isUpdate = false;

        /// <summary>   
        /// 获取是否需要更新   
        /// </summary>   

        public bool IsUpdate
        {
            get
            {
                checkUpdate();
                return isUpdate;
            }

        }

        /// <summary>   
        /// 要检查更新的文件   
        /// </summary>   

        public string LoadFile
        {
            get { return loadFile; }
            set { loadFile = value; }
        }

        /// <summary>   
        /// 程序集新版本   
        /// </summary>   

        public string NewVerson
        {
            get { return newVerson; }
        }

        /// <summary>   
        /// 升级的名称   
        /// </summary>   

        public string SoftName
        {
            get { return softName; }
            set { softName = value; }
        }

        #endregion
        /// <summary>
        /// 更新信息
        /// </summary>
        public void Updatetxtinfo()
        {
            try
            {
                GetUrl();
                WebClient wc = new WebClient();

                Stream stream = wc.OpenRead(download.Substring(0, download.LastIndexOf(@"/") + 1) + "Update.xml");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                XmlNode list = xmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "Updateinfo")
                    {
                        updateinfo = node.InnerText;
                    }
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 得到URl,oldverson
        /// </summary>
        private void GetUrl()
        {
            try
            {
                WebClient cwc = new WebClient();
                Stream cstream = cwc.OpenRead(Path.GetDirectoryName(loadFile) + @"\ClientUpdate.xml");
                XmlDocument cxmlDoc = new XmlDocument();
                cxmlDoc.Load(cstream);
                XmlNode clist = cxmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in clist)
                {
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == SoftName.ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Verson")
                                oldVerson = xml.InnerText;
                            else
                                download = xml.InnerText;
                        }
                    }
                }
                currentverson = oldVerson;
                cstream.Close();
                System.Diagnostics.Trace.WriteLine(oldVerson + "," + download);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>   
        /// 检查是否需要更新   
        /// </summary>  
        private void checkUpdate()
        {
            try
            {
                GetUrl();
                WebClient wc = new WebClient();

                Stream stream = wc.OpenRead(download.Substring(0, download.LastIndexOf(@"/") + 1) + "Update.xml");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);

                XmlNode list = xmlDoc.SelectSingleNode("Update");

                foreach (XmlNode node in list)
                {
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == SoftName.ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Verson")
                            {
                                newVerson = xml.InnerText;

                            }
                            else
                            {
                                download = xml.InnerText;

                            }
                        }
                    }
                }
                stream.Close();

                Version ver = new Version(newVerson);
                currentverson = newVerson;
                Version verson = new Version(oldVerson);
                int tm = verson.CompareTo(ver);
                //Stream wstream = cwc.OpenWrite(Path.GetDirectoryName(loadFile) + @"\ClientUpdate.xml");

                if (tm >= 0)
                    isUpdate = false;
                else
                {
                    isUpdate = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw new Exception("更新出现错误，请确认网络连接无误后重试！");
            }
        }

        public event UpdateState UpdateFinish;

        private void isFinish()
        {
            if (UpdateFinish != null)
                UnZip();
            File.Delete(Path.GetDirectoryName(loadFile) + @"\" + softName);
            //overfile();

            setclientver();

            isfinish = true;
            UpdateFinish();
        }
        /// <summary>
        /// 开始下载
        /// </summary>
        public void StartDownload()
        {
            try
            {
                if (!isUpdate)
                    return;
                WebClient wc = new WebClient();
                //wc.DownloadFile(new Uri(download), Path.GetDirectoryName(LoadFile) + @"\" + softName);
                wc.DownloadFile(download, Path.GetDirectoryName(LoadFile) + @"\" + softName);
                wc.Dispose();
                isFinish();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 解压压缩包
        /// </summary>
        private void UnZip()
        {

            ZipInputStream s = new ZipInputStream(File.OpenRead(Path.GetDirectoryName(loadFile) + @"\" + softName));
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                try
                {
                    string directoryName = Path.GetDirectoryName(loadFile);
                    string fileName = Path.GetFileName(theEntry.Name);
                    //生成解压目录
                    //Directory.CreateDirectory(directoryName + @"\" + theEntry.Name.Replace("/", @"\").Substring(0, theEntry.Name.Replace("/", @"\").LastIndexOf(@"\")));

                    if (fileName != String.Empty)
                    {
                        if (fileName.ToLower() != "thumbs.db")
                        {
                            updatefilename.Add(fileName);
                            updatefilecount += 1;
                            //解压文件到指定的目录
                            //MessageBox.Show(theEntry.Name.Replace("/", @"\").Substring(0, theEntry.Name.Replace("/", @"\").LastIndexOf(@"\")));
                            FileStream streamWriter = File.Create(directoryName + @"\" + theEntry.Name.Replace("/", @"\"));
                            int size = 4096;
                            byte[] data = new byte[4096];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            streamWriter.Close();
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(directoryName + @"\" + theEntry.Name.Replace("/", @"\").Substring(0, theEntry.Name.Replace("/", @"\").LastIndexOf(@"\")));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                GetInfoDelegate("正在获取更新  ", "", 0);


            }
            s.Close();
        }
        private void setclientver()
        {
            WebClient cwc = new WebClient();
            Stream cstream = cwc.OpenRead(Path.GetDirectoryName(loadFile) + @"\ClientUpdate.xml");
            XmlDocument cxmlDoc = new XmlDocument();
            cxmlDoc.Load(Path.GetDirectoryName(loadFile) + @"\ClientUpdate.xml");
            XmlNode wlist = cxmlDoc.SelectSingleNode("Update");
            foreach (XmlNode node in wlist)
            {
                if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == SoftName.ToLower())
                {
                    foreach (XmlNode xml in node)
                    {
                        if (xml.Name == "Verson")
                            xml.InnerText = newVerson;
                        break;
                    }
                }
            }

            cstream.Close();
            cxmlDoc.Save(Path.GetDirectoryName(loadFile) + @"\ClientUpdate.xml");
            GetInfoDelegate("更新成功  ", "", 0);

        }


        public delegate void mydel(string sinfo, string scale, int step);
        public static mydel md;
        public static void GetInfoDelegate(string sinfo, string scale, int step)
        {
            if (md != null)
                md(sinfo, scale, step);
        }
    }
}
