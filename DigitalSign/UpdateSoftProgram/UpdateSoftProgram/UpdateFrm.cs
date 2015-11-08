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
using Microsoft.Win32;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using AdeskInter = Autodesk.AutoCAD.Interop;


namespace UpdatePlugin
{
    public partial class UpdateFrm : Form
    {
        RegistryKey YCRO = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Autodesk\AutoCAD\R16.2\ACAD-4001:409\Applications\YCRO_Digital");
        SoftUpdate app = null;

        public UpdateFrm()
        {
            InitializeComponent();
            System.Diagnostics.Process[] CADPro = System.Diagnostics.Process.GetProcessesByName("acad");
            foreach (System.Diagnostics.Process Pro in CADPro)
            {
                Pro.Kill();
            }
            System.Threading.Thread.Sleep(3000);
            object pathname = YCRO.GetValue("LOADER");
            app = new SoftUpdate(pathname.ToString(), "UpdateProgram.zip");

            app.Updatetxtinfo();
            textBox1.Text = app.updateinfo;
            app.UpdateFinish += new UpdateState(app_UpdateFinish);

            if (app.IsUpdate)
            {
                label1.Text = "当前版本(" + app.currentverson + ")";
                dels invoker = new dels(app.StartDownload);
                invoker.BeginInvoke(new AsyncCallback(CallBack), null);
            }
        }

        public delegate void DisplayInfoHanlder(string sinfo, string scale, int step);
        public delegate void dels();
        public void Disp(string sinfo, string scale, int step)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DisplayInfoHanlder(Disp), sinfo, scale, step);
                return;
            }
            label1.Text = sinfo;
            // progressBar1.Value = step;
        }
        private void CallBack(IAsyncResult tag)
        {
            AsyncResult result = (AsyncResult)tag;
            dels del = (dels)result.AsyncDelegate;
            del.EndInvoke(tag);

            try
            {
                MessageBox.Show("电子签名插件更新成功，重新启动Auto CAD","更新成功");
                string progID = "AutoCAD.Application";
                AdeskInter.AcadApplication CADAPP = null;
                try
                {
                    Type SType = Type.GetTypeFromProgID(progID);
                    CADAPP = (AdeskInter.AcadApplication)System.Activator.CreateInstance(SType, true);
                    CADAPP.Visible = true;
                }
                catch
                {
                    MessageBox.Show("fail");
                }

                System.Diagnostics.Process[] processName = System.Diagnostics.Process.GetProcessesByName("UpdateSoftProgram");
                foreach (System.Diagnostics.Process p in processName)
                {
                    if (!p.HasExited)
                        p.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void app_UpdateFinish()
        {
            //MessageBox.Show("更新完成，请重新启动程序！");
        }
    }
}