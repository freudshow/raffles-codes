using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace DetailInfo
{
    class UploadWorkSheet
    {
        public static bool UploadFile(string localFilePath, string serverFolder, bool reName)
        {
            string fileNameExt, newFileName, uriString;
            if (reName)
            {
                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf(".") + 1);
                newFileName = DateTime.Now.ToString("yyMMddhhmmss") + fileNameExt;
            }
            else
            {
                newFileName = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }

            if (!serverFolder.EndsWith("/") && !serverFolder.EndsWith("\\"))
            {
                serverFolder = serverFolder + "/";
            }
            uriString = serverFolder + newFileName;   //服务器保存路径
            /**/
            /// 创建WebClient实例
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;

            // 要上传的文件
            FileStream fs = new FileStream(newFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //FileStream fs = new FileStream(newFileName, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            try
            {
                //使用UploadFile方法可以用下面的格式
                //myWebClient.UploadFile(uriString,"PUT",localFilePath);
                byte[] postArray = r.ReadBytes((int)fs.Length);
                Stream postStream = myWebClient.OpenWrite(uriString, "PUT");
                if (postStream.CanWrite)
                {
                    postStream.Write(postArray, 0, postArray.Length);
                }
                else
                {
                    MessageBox.Show("文件目前不可写！");
                }
                postStream.Close();
            }
            catch
            {
                //MessageBox.Show("文件上传失败，请稍候重试~");
                return false;
            }

            return true;
        }
    }
}
