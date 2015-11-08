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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
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

        private void MainForm_Closed(object sender, EventArgs e)
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
             

            }
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {

            string message = "确认要退出重新登录吗?";
            string caption = "重新登录!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.Dispose();
                this.Hide();
                LoginIn lg1 = new LoginIn();
                lg1.Show();

            }
            
        }

        private void menuItem3_Click(object sender, EventArgs e)
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
                this.Dispose();
                this.Close();
                Application.Exit();
                

            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            ModifyPwd mdp = new ModifyPwd();
            mdp.Show();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
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

        private void menuItem6_Click(object sender, EventArgs e)
        {
            InstallSpoolQuery spq = new InstallSpoolQuery();
            spq.Show();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            SpoolQuery spq = new SpoolQuery();
            spq.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            string sqlString = "SELECT ID,NAME,REMARK FROM spflowstatus_tab";

            //DataSet Ds = User.GetDataSetCommon(sqlString, "spool_tab");
            try
            {
                WSR.PPSPLLogin ppspl = new CIMCPPSPL.WSR.PPSPLLogin();

                DataSet Ds = ppspl.DatagridDs(sqlString, "spflowstatus_tab");
                string delPDAstr = "delete from spflowstatus_tab ";
                string insertPDAstr = "insert into spflowstatus_tab values (";
                int flag = User.RUNSQLCommon(delPDAstr);
                if (Ds.Tables[0].Rows.Count != 0)
                    {
                        int rownum = Ds.Tables[0].Rows.Count;
                        for (int i = 0; i < rownum; i++)
                        { 
                            string fid = Ds.Tables[0].Rows[i][0].ToString();
                            string fname = Ds.Tables[0].Rows[i][1].ToString();
                            string fremark = Ds.Tables[0].Rows[i][2].ToString();
                            string insPDAstr = insertPDAstr + fid + ",'" + fname + "','" + fremark + "')";
                            User.RUNSQLCommon(insPDAstr);
                        }
                    }
                
                
                MessageBox.Show("更新数据成功:已为最新流程状态", "提示消息");
             }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString() + ":请检查手机是否已经正确连接至电脑！！");
            }
        }
    }
}