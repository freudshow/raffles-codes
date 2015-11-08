using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace DetailInfo.Application_Code
{
    class GetOracleTnsNames
    {
        static ArrayList keyarr = new ArrayList();
        public static string[] GetORCVersion()
        {
            int i = 0;
            int j = 0;
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("ORACLE");
            string[] subkey = key.GetSubKeyNames();
            if (key.GetValue("ORACLE_HOME") != null)
            {
                keyarr.Add((string)key.GetValue("ORACLE_HOME"));
                i++;
            }
            if (subkey.Length != 0)
            {
                for (j = i; j < subkey.Length; j++)
                {
                    if (key.OpenSubKey(subkey[j]).GetValue("ORACLE_HOME") != null)
                    {
                        //ORACLE_HOME获取
                        keyarr.Add(key.OpenSubKey(subkey[j]).GetValue("ORACLE_HOME"));
                    }
                }
            }
            return (string[])keyarr.ToArray(typeof(string));
        }
        public static string[] GetOTNames()
        {
            try
            {
                string pt = System.Environment.GetEnvironmentVariable("Path");
                //RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM").OpenSubKey("ControlSet001").OpenSubKey("Control").OpenSubKey("Session Manager").OpenSubKey("Environment");
                //string pt = (string)key.GetValue("path");
                string[] evpt = pt.Split(';');
                string home = null;
                int i = 0;
                int j = 0;
                GetORCVersion();
                for (i = 0; i < evpt.Length; i++)
                {
                    for (j = 0; j < keyarr.Count; j++)
                    {
                        if (evpt[i].ToLower().IndexOf(((string)keyarr[j]).ToLower()) != -1)
                        {
                            home = (string)keyarr[j];
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(home)) break;
                }
                #region
                //RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("ORACLE");
                //string home = (string)key.GetValue("ORACLE_HOME"); ;
                //string[] subkey = key.GetSubKeyNames();
                //if (subkey.Length != 0)
                //{
                //    int i = 0;
                //    for (i = 0; i < subkey.Length; i++)
                //    {
                //        if (subkey[i].Substring(0, 3) == "KEY")
                //        {
                //            //Oracle 10g 的获取
                //            key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("ORACLE").OpenSubKey(subkey[i]);
                //            home = (string)key.GetValue("ORACLE_HOME");

                //        }
                //    }
                //}
                #endregion
                string file = home + @"\network\ADMIN\tnsnames.ora";
                StreamReader reader = new StreamReader(file);
                ArrayList arr = new ArrayList();


                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    line = line.TrimStart();
                    if (line != "")
                    {
                        char c = line[0];
                        if (c > 'A' && c < 'z')
                        {
                            arr.Add(line.Substring(0, line.IndexOf('=')).Trim().ToUpper());
                        
                        }
                    }
                }

                reader.Close();
                return (string[])arr.ToArray(typeof(string));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
