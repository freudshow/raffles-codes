using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OutSourcing
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
            Application.Run(new Form_PhotoManager());
            //Application.Run(new Form_CreateDir());
        }
    }
}