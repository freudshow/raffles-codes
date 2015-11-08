using System;
//using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace CIMCPPSPL
{
    class XmlOper
    {
        public static string  fpath =  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
        public static string getusername()//Get current loged username from XML
        {
            string file = fpath.Substring(0, fpath.Length - 13)+"Input.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode person = doc.SelectSingleNode("/Project/Person/Name");
            if (person != null)
            {
                return person.FirstChild.Value.ToString().Trim();
            }
            else
            {
                return "请输入";
            }
        }
        public  string insertvalue(string values)
        {
            return "'" + values.Trim() + "',";
        }

        public static void setXML(string CName)
        {
            try
            {

                //MessageBox.Show(spath);
                //string file = "/Storage Card/Input.xml";//从ppc共享文档中读取xml文档
                string file = fpath.Substring(0, fpath.Length - 13) + "Input.xml";
                //string file = "\\My Documents\\aaa\\Input.xml";
                //string file = "C:\\Input.xml";//从本地系统中读取xml文档
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlNode person = doc.SelectSingleNode("/Project/Person/Name");
                if (person != null)
                {
                    person.FirstChild.Value = CName;

                }
                
                doc.Save(fpath.Substring(0, fpath.Length - 13)+"Input.xml");
                //doc.Save("/Storage Card/Input.xml");
                //doc.Save("c:\\Input.xm");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
    }
}
