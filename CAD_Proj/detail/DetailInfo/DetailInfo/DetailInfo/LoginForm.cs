using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DetailInfo.Application_Code;
using System.Threading;
using System.Diagnostics;
using System.IO;
using UpdateSoft;
namespace DetailInfo
{
    public partial class LoginForm : Form
    {
        public string LoginUserName = string.Empty;

        public LoginForm()
        {
            InitializeComponent();

            this.tbUserName.Text = XmlOper.getXMLContent("Name");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string userName = tbUserName.Text.ToLower();
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("登录名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbUserName.Focus();
                return ;
            }

            string passWord = tbPassword.Text;
            if (string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("登录密码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPassword.Focus();
                return ;
            }

            string dbsever = string.Empty;
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请与数据库管理员联系配置数据库连接", "数据库连接问题", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (this.comboBox1.SelectedItem.ToString().Trim()=="正式库")
                {
                    dbsever = "OIDS";
                }
                else if (this.comboBox1.SelectedItem.ToString().Trim()=="测试库")
                {
                    dbsever = "OIDSNEW";
                }
                DataAccess.GetSeverName(dbsever);
                Framework.DataAccess.GetSeverName(dbsever);
            }

            bool loginState = User.Verify(userName, passWord);

            if (loginState)
            {
                LoginUserName = userName;
                XmlOper.setXML("Name", userName);
                this.DialogResult = DialogResult.OK;        
            }
            else
            {
                MessageBox.Show("登录名或密码错误！","小票查询", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string[] OTNS;
            OTNS = GetOracleTnsNames.GetOTNames();
            if (OTNS.Length == 0)
            {
                MessageBox.Show("请与数据库管理员联系配置数据库连接", "数据库连接问题", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = 0; i < OTNS.Length; i++)
                {
                    if (OTNS[i].Trim() == "OIDS")
                    {
                        comboBox1.Items.Add("正式库");
                    }
                    else if (OTNS[i].Trim() == "OIDSNEW")
                    {
                        comboBox1.Items.Add("测试库");
                    }
                }
                if (this.comboBox1.Items.Contains("正式库"))
                {
                    this.comboBox1.SelectedItem = "正式库";
                }
            }
        }


    }
}