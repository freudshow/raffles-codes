//////////////////////////////////////////////////////////////////
//作者：牛A与牛C之间
//Q Q：1046559384   C#/Java技术交流群：96020642
//微博：http://weibo.com/flydoos
//博客：http://www.cnblogs.com/flydoos
//日期：2011-10-29
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FileRename
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
