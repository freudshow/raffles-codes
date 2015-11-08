using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using UpdateSoft;

namespace UpdateApp
{
    public partial class Form1 : Form
    {

        private string MainProgram = "DetailInfo.exe";
        SoftUpdate app = new SoftUpdate(Application.ExecutablePath, "UpdateProgram.zip");
        public Form1()
        {
            InitializeComponent();
            app.Updatetxtinfo();
            textBox1.Text = app.updateinfo;
            label1.Text = "当前版本(" + app.currentverson + ")";
            button1.Enabled = false;
            app.UpdateFinish += new UpdateState(app_UpdateFinish);
            if (app.IsUpdate)
            {
                Thread update = new Thread(new ThreadStart(app.StartDownload));
                update.Start();
                Thread.Sleep(3000);

            }

        }

        void app_UpdateFinish()
        {
            //MessageBox.Show("更新完成，请重新启动程序！");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\" + MainProgram);
                this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Start();
            int m = 1;
            this.progressBar1.Maximum = app.updatefilecount;
            for (m = 1; m <= this.progressBar1.Maximum; m++)
            {
                this.progressBar1.Value = m;
                label1.Text = app.updatefilename[m-1].ToString();
                //Application.DoEvents();
            }
            //button1.Enabled = true;
            if (app.isfinish || !app.IsUpdate)
            {
                label1.Text = "当前已是最新版本(" + app.currentverson + ")";
                this.button1.Enabled = true;
                this.timer1.Stop();
            }
        }

    }
}