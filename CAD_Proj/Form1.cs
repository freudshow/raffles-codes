using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace mFormTest
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            
            InitializeComponent();
            string ApproveMan = GetApproveMan("Y001728", "YTRS316-395-030", "0");
            if (ApproveMan == "NoDrawing")
                MessageBox.Show("在系统里没有找到该版图纸!");
            else if (ApproveMan == "NoApproveTemplate")
                MessageBox.Show("系统里存在该版图纸，但无审核模板，请联系您主管登陆到ECDMS中修改此纸的审批模版");
            else
            {
                string[] ApproveManArr = ApproveMan.Split(';');
                for (int i = 0; i < ApproveManArr.Length; i++)
                    this.listBox_approveMan.Items.Add(ApproveManArr[i]);

                string jpgPath = @"\\172.16.7.55\sign$\jpg\";
                List<string> jpgNameList = new List<string>();

                for (int i = 0; i < ApproveManArr.Length; i++)
                    jpgNameList.Add(ApproveManArr[i].Split(':')[0].ToString() + ".jpg");
                //for (int i = 0; i < jpgNameList.Count; i++)
                //    MessageBox.Show(jpgNameList[i].ToString());            

                //for (int i=0; i < jpgNameList.Count; i++)
                //{
                //    if (System.IO.Directory.GetFiles(jpgPath, jpgNameList[i].ToString()) == null)
                //    {
                //        MessageBox.Show("未找到 " + jpgNameList[i].ToString() + " 的预览签名图片，请联系软件开发组!");                    
                //    }
                //    else
                //        MessageBox.Show("找到 " + jpgNameList[i].ToString());
                //}

                this.pictureBox1.ImageLocation = jpgPath + jpgNameList[0].ToString();
            }
        }

        public string GetApproveMan(string IC_Card, string dwg_id, string dwg_edition)
        {
            return(new mWebRef.Drawing()).GetApproveTemplate(IC_Card, dwg_id, dwg_edition.ToString());
        }
    }
}