using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CIMCPPSPL
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new LoginIn());
        }
    }
}