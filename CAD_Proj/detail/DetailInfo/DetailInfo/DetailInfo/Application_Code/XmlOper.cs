using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace DetailInfo.Application_Code
{
    public class XmlOper
    {
        public static string getXMLContent(string para)//Get current loged username from XML
        {
            //string currentpath = System.IO.Directory.GetCurrentDirectory();
            string currentpath = System.AppDomain.CurrentDomain.BaseDirectory;
            //string currentpathn = System.Environment.CurrentDirectory;
            string file = currentpath + "\\Input.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode person = doc.SelectSingleNode("/Project/Person/" + para);
            if (person.FirstChild!=null)
            {
                return person.FirstChild.Value.ToString().Trim();
            }
            else
            {
                if (para == "Type") return "64";
                return "yang.yang";
            }
        }
        public string insertvalue(string values)
        {
            return "'" + values.Trim() + "',";
        }

        public static void setXML(string para, string xmlname)
        {
            try
            {
                //string currentpath = System.IO.Directory.GetCurrentDirectory();
                //string currentpathn = System.Environment.CurrentDirectory;
                string currentpath = System.AppDomain.CurrentDomain.BaseDirectory;
                string file = currentpath + "\\Input.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlNode person = doc.SelectSingleNode("/Project/Person/" + para);
                if (person != null)
                {
                    person.FirstChild.Value = xmlname;

                }

                doc.Save(file);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
