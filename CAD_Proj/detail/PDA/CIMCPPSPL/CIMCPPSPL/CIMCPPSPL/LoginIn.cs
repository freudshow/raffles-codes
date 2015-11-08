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
    public partial class LoginIn : Form
    {
        public LoginIn()
        {
            InitializeComponent();
        }
        private void TPW_TextChanged(object sender, EventArgs e)
        {

        }

        private void QX_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
            Application.Exit();
        }

        private void LG_Click(object sender, EventArgs e)
        {
            
        }

        private void LoginIn_Load(object sender, EventArgs e)
        {
            try
            {
                TUN.Text = XmlOper.getusername();
                TPW.Focus();
            }
            catch (Exception err)
            { MessageBox.Show(err.ToString()); }
        }

        private void LG_Click_1(object sender, EventArgs e)
        {
            string aa, bb;
            aa = TUN.Text.Trim();
            bb = TPW.Text.Trim();
            if (aa == "" || bb == "")
            {
                MessageBox.Show("用户名和密码都不能为空!");
                return;
            }
            try
            {
                WSR.PPSPLLogin rlong = new WSR.PPSPLLogin();
                string flag= rlong.ValidateLogin(aa, bb);
                //flag = rlong.ValidateLogin(aa, bb);
                if (flag == "1")
                {
                    MainForm f1 = new MainForm();
                    //setXML(aa);

                    XmlOper.setXML(aa);             
                    this.Hide();
                    f1.Show();

                }
                else
                {
                    MessageBox.Show("登录失败,输入密码或者用户名错误!");
                    return;
                }
            }
            catch (Exception err)
            {
                string flag = User.CheckLogin(aa, bb);
                //flag = rlong.ValidateLogin(aa, bb);
                if (flag == "1")
                {
                    MainForm f1 = new MainForm();
                    //setXML(aa);

                    XmlOper.setXML(aa);
                    this.Hide();
                    f1.Show();

                }
                else
                {
                    MessageBox.Show("登录失败,输入密码或者用户名错误!");
                    return;
                }
                //MessageBox.Show(err.ToString());
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            string message = "确认要退出此程序吗?";
            string caption = "完全退出程序!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                // Closes the parent form.
                this.Dispose();
                this.Close();
                Application.Exit();
                //login relogin = new login();
                //relogin.Show();

            }
        }

        private void QX_Click_1(object sender, EventArgs e)
        {
            string message = "确认要退出此程序吗?";
            string caption = "完全退出程序!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                // Closes the parent form.
                this.Dispose();
                this.Close();
                Application.Exit();
                //login relogin = new login();
                //relogin.Show();

            }
        }

        private void LoginIn_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        private void LoginIn_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}