using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DetailInfo
{
    class GetIPAddress
    {
        public static string GetIP()
        {
            IPHostEntry tempHost = new IPHostEntry();
            tempHost = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            if (tempHost.AddressList.Length == 1)
            {
                return tempHost.AddressList[0].ToString();
            }
            else
            {
                return tempHost.AddressList[1].ToString();
            }
        }
    }
}
