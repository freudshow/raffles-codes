using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;
using System.Text;
namespace jzpl.Lib
{
    public class Mail
    {       
        public Mail()
        {

        }
        public static void SendTest()
        {           
            MailAddress from_ = new MailAddress("yias@yantai-raffles.net");
            MailAddress to_ = new MailAddress("mng.li@yantai-raffles.net");
            MailMessage m = new MailMessage(from_, to_);
            
            m.Subject = "JP test";
            m.Body = "test";
            m.Attachments.Add(new Attachment(HttpRuntime.AppDomainAppPath + "\\log\\log.txt"));
            SmtpClient s = new SmtpClient();
            try
            {
                s.Send(m);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool Send(string mTitle, string mBody, string mFrom, string mTo, string mCC, bool mHtmlFormat, bool isServer, string[] fileList, string serverPath)
        {
            MailMessage mm;
            SmtpClient ss;
            string split = ";";
            try
            {
                mm = new MailMessage("yias@cimc-raffles.com", "yias@cimc-raffles.com");

                if (mTo.Length > 0)
                {
                    string[] toList = mTo.Split(split.ToCharArray());
                    mm.To.Clear();
                    for (int i = 0; i < toList.Length; i++)
                    {
                        if (toList[i].Length == 0) continue;
                        mm.To.Add(toList[i].ToString().Trim());
                    }
                }

                if (mCC.Length > 0)
                {
                    string[] ccList = mCC.Split(split.ToCharArray());
                    for (int i = 0; i < ccList.Length; i++)
                    {
                        if (ccList[i].Length == 0) continue;
                        mm.CC.Add(ccList[i].ToString().Trim());
                    }
                }
                if (fileList != null)
                {
                    for (int i = 0; i < fileList.Length; i++)
                    {
                        mm.Attachments.Add(new Attachment(fileList[i]));
                    }
                }

                mm.Subject = mTitle.Trim();
                mm.SubjectEncoding = Encoding.UTF8;

                mm.Body = mBody.Trim();
                mm.BodyEncoding = Encoding.UTF8;
                mm.IsBodyHtml = mHtmlFormat;

                ss = new SmtpClient();

                ss.Send(mm);
                mm.Dispose();

                return true;
            }
            catch (SmtpException ex)
            {
                throw ex;
                //return false;
            }
        }
    }
}
