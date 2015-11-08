using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Framework
{
    public class XmlOper
    {
        
        
        public static  string getXMLContent(string para)//Get current loged username from XML
        {
            //string currentpath = System.IO.Directory.GetCurrentDirectory();
            //string currentpathn = System.Environment.CurrentDirectory;
            string currentpath = System.AppDomain.CurrentDomain.BaseDirectory;
            string file = currentpath + "\\Input.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode person = doc.SelectSingleNode("/Project/Person/"+para);
            if (person != null)
            {
                return person.FirstChild.Value.ToString().Trim();
            }
            else
            {
                if (para == "Type") return "64";
                return "weijun.qu";
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
                XmlNode person = doc.SelectSingleNode("/Project/Person/"+para);
                if (person != null)
                {
                    person.FirstChild.Value = xmlname;

                }

                doc.Save(file);
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
