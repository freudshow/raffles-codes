using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class ModifyReasonForm : Form
    {
        public ModifyReasonForm()
        {
            InitializeComponent();
        }

        private int drawingid;

        public int Drawingid
        {
            get { return drawingid; }
            set { drawingid = value; }
        }

        private void ModifyReasonForm_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://172.16.5.161/Manage/Drawing/DrawingDisModifyInfo/DrawingModifyInfo.aspx?id="+drawingid);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document; //获取document对象
            HtmlElement btn = null;
            foreach (HtmlElement em in doc.All)
            {
                string str = em.Name;
                //MessageBox.Show(str.ToString());
                if ((str == "loginName") || (str == "password") || (str == "img_but"))  //减少处理
                {
                    switch (str)
                    {
                        case "loginName":
                            em.SetAttribute("value", "xiaoyan.cai");  //赋用户名
                            break;
                        case "password":
                            em.SetAttribute("value", "123");  //赋密码
                            break;
                        case "img_but":
                            btn = em;
                            break; //获取submit按钮
                        default:
                            break;
                    }
                }
            }
            btn.InvokeMember("onclick");
        }

    }
}
