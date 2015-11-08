using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CIMCPPSPL
{
    public partial class ModifyPwd : Form
    {
        public ModifyPwd()
        {
            InitializeComponent();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            string aa, bb, cc;
            aa = TCP.Text.Trim();
            bb = TPW.Text.Trim();
            cc = TCNP.Text.Trim();
            if (aa == "")
            {
                MessageBox.Show("请输入原密码!!");
                return;
            }
            if (bb == "")
            {
                MessageBox.Show("请输入新密码!!");
                return;

            }
            if (cc == "")
            {
                MessageBox.Show("请输入确认新密码!!");
                return;
            }
            if (cc != bb)
            {
                MessageBox.Show("两次输入新密码不一致!!");
                return;
            }
            //string[]sqlarray = new string[1];
            string username;
            WSR.PPSPLLogin rlogin = new WSR.PPSPLLogin();
            //string file = "/Storage Card/Input.xml";//从ppc共享文档中读取xml文档
            //string file = "c:\\Input.xml";//从openFileDialog中读取xml文档
            //string file = "\\My Documents\\Input.xml";
            //XmlDocument doc = new XmlDocument();
            //doc.Load(file);
            //XmlNode person = doc.SelectSingleNode("/Project/Person/Name");
            //if (person != null)
            //{
            ////    username = person.FirstChild.Value.ToString();
            //}
            //else
            //{ 
            //    return; 
            //}
            username = XmlOper.getusername();
            string flag = User.GetDataSetCommon("select * from online_user_tab where username ='"+username+"' and password ='"+aa+"'","online_user_tab").Tables[0].Rows.Count.ToString();
            //MessageBox.Show(flag);
            if (flag == "1")
            {
                //sqlarray[0] = "update user_tab set pass ='" + bb + "' where name='" + username + "'";
                //MessageBox.Show(sqlarray[0]);
                string updatestr = "update online_user_tab set password ='" + bb + "' where username='" + username + "'";
                int flagnew = User.RUNSQLCommon(updatestr);
                if (flagnew > 0)
                    MessageBox.Show("更新成功！");
                else
                {
                    MessageBox.Show("更新失败！");
                    return;
                }


            }
            else
            {
                MessageBox.Show("更新失败，当前密码不对!");
                return;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}