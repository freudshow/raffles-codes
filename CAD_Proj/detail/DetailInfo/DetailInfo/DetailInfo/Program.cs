using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using UpdateSoft;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace DetailInfo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool bCreatedNew;
            Mutex m = new Mutex(false, "myUniqueName", out bCreatedNew);
            SoftUpdate app = new SoftUpdate(Application.ExecutablePath, "UpdateProgram.zip");

            app.UpdateFinish += new UpdateState(app_UpdateFinish);

            if (app.IsUpdate)
            {
                Application.Exit();
                System.Diagnostics.Process.Start(Application.StartupPath + "\\" + "UpdateSoftProgram.exe");
            }

            else
            {
                if (bCreatedNew)
                {
                    try
                    {
                        //处理未捕获的异常   
                        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                        //处理UI线程异常   
                        Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                        //处理非UI线程异常   
                        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        LoginForm lform = new LoginForm();
                        if (lform.ShowDialog() == DialogResult.OK)
                        {
                            string sqlStr = string.Empty;
                            string user_name = lform.LoginUserName;
                            User.Get_CurrentUser(user_name);
                            Application.Run(new MDIForm());
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = "";
                        string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";

                        if (ex != null)
                        {
                            str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                                 ex.GetType().Name, ex.Message, ex.StackTrace);
                        }
                        else
                        {
                            str = string.Format("应用程序线程错误:{0}", ex);
                        }


                        writeLog(str);
                        MessageBox.Show(str);
                        //MessageBox.Show("发生致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();

                    }
                }
            }



        }
        static void app_UpdateFinish()
        {
            //MessageBox.Show("更新完成，请重新启动程序！");
        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            string str = "";
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            writeLog(str);
            MessageBox.Show(str);
            //MessageBox.Show("发生UI致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }

            writeLog(str);
            MessageBox.Show("发生非UI致命错误，请停止当前操作并及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="str"></param>
        static void writeLog(string str)
        {
            //string pathstr = Path.Combine(User.rootpath, "\\ErrLog");
            string pathstr = User.rootpath+ "\\"+"ErrLog";
            if (!Directory.Exists(pathstr))
            {
                Directory.CreateDirectory(pathstr);
            }

            using (StreamWriter sw = new StreamWriter(pathstr + "\\" + "ErrLog.txt", true))

            {
                sw.WriteLine(str);
                sw.WriteLine("---------------------------------------------------------");
                sw.Close();
            }
        }


    }
}