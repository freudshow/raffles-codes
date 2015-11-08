using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using jzpl.Lib;

namespace jzpl.Lib
{
    public class TimeRun
    {
        private static readonly TimeRun _ScheduledTask = null;
        private System.Threading.Timer UpdateTimer = null;
        private System.Threading.Timer UpdateLackTimer = null;  
        private long Interval = 60000*60;
        private long mInterval = 1000*60;
        private int _IsRunning;

        private string ServerLogPath = HttpRuntime.AppDomainAppPath + "log\\log.txt";
        private string FileDir = HttpRuntime.AppDomainAppPath + "files1\\";        
        private bool runningLogSwitch=true;

        static TimeRun()
        {
            _ScheduledTask = new TimeRun();
        }
        public static TimeRun Instance()
        {
            return _ScheduledTask;
        }

        public void Start()
        {           
            if (UpdateTimer == null)
            {
                //WriteTextFile(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":start.",ServerLogPath);
                UpdateTimer = new System.Threading.Timer(new System.Threading.TimerCallback(UpdateTimerCallback), null, Interval, Interval);
            }

            if (UpdateLackTimer == null)
            {
                UpdateLackTimer = new System.Threading.Timer(new System.Threading.TimerCallback(SendMailCallback), null, mInterval, mInterval);
            }
        }

        private void WriteTextFile(string lineString,string path)
        {
            if (!File.Exists(path))
            {
                //考虑路径的创建
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(lineString);
                sw.Close();
            }
        }

        private void UpdateTimerCallback(object sender)
        {
            if (Interlocked.Exchange(ref _IsRunning, 1) == 0)
            {
                try
                {
                    if (runningLogSwitch)
                    {
                        WriteTextFile(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":running...", ServerLogPath);
                    }
                    if (DateTime.Now.Hour == 11)
                    {
                        SendMailInTime("jp_issue_compare");
                    }
                }
                catch(Exception ex)
                {
                    WriteTextFile(ex.Message, ServerLogPath);
                }
                finally
                {
                    Interlocked.Exchange(ref _IsRunning, 0);
                }
            }
        }

        private void SendMailCallback(object sender)
        {
            if (Interlocked.Exchange(ref _IsRunning, 1) == 0)
            {
                try
                {
                    //if (DateTime.Now.Hour == 8)
                        SendLackMailInTime();
                }
                catch (Exception ex)
                {
                    WriteTextFile(ex.Message, ServerLogPath);
                }
                finally
                {
                    Interlocked.Exchange(ref _IsRunning, 0);
                }
            }
        }

        private void SendLackMailInTime()
        {
            jzpl.Lib.Mail myMail = new jzpl.Lib.Mail();
            string SQLReleaseUsers = "select t1.release_user,wm_concat(t1.demand_id) demand_id from (select * from JP_DEMAND_CHECK_QTY_V t where t.tod_qty>t.yes_qty) t1 group by t1.release_user";
            DataTable ReleaseUsers = DBHelper.GetDataset(SQLReleaseUsers).Tables[0];
            string Mail = string.Empty;
            for (int i = 0; i < ReleaseUsers.Rows.Count; i++)
            {
                string ReleaseUser = ReleaseUsers.Rows[i][0].ToString();
                string DemandID = ReleaseUsers.Rows[i][1].ToString();
                string MailBody = "物资需求单号为: " + DemandID + "的物资有到货";
                Mail += ReleaseUser + '\t' + MailBody + '\n';
                WriteTextFile(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "send lack mail--to:" + ReleaseUser + "@cimc-raffles.com" + "--:" + MailBody, ServerLogPath);
                myMail.Send("到货提醒", MailBody, "yias@cimc-raffles.com", ReleaseUser + "@cimc-raffles.com", "baoshan.song@cimc-raffles.com", true, false, null, null);
            }
        }

        private void SendMailInTime(string p_type)
        {
            Lib.Mail myMail = new Lib.Mail();
            switch (p_type)
            {
                case "jp_issue_compare":
                    string today_ = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                    string mailBody = "附件为DMS-ERP下发对照表，请及时在两个系统中下发，使数据同步。";
                    StringBuilder cmdText = new StringBuilder("select a.part_no,a.mtr_no,PART_DESCRIPTION,part_unit,a.dms_issue_qty,b.erp_issue_qty  from (");

                    cmdText.Append("select part_no,PART_DESCRIPTION,part_unit,matr_seq_no mtr_no,sum(nvl(issued_qty,0)) dms_issue_qty from jp_requisition t where rowstate in ('released','finished') ");
                    cmdText.Append(string.Format(" and (finish_time is null or (finish_time>to_date('{0}','yyyy-mm-dd') and finish_time<to_date('{1}','yyyy-mm-dd')+1)) ", "1981-2-5", today_));
                    cmdText.Append(" group by part_no,matr_seq_no,PART_DESCRIPTION,part_unit) a,(select t.sequence_no,sum(quantity) erp_issue_qty from ifsapp.inventory_transaction_hist2@erp_prod t ");
                    cmdText.Append(" where t.dated>=to_date('2009-9-1','yyyy-mm-dd')  and t.transaction_code='PROJISS' group by t.sequence_no ) b ");
                    cmdText.Append(" where a.mtr_no=b.sequence_no(+)");
                    cmdText.Append(" and a.dms_issue_qty <> nvl(b.erp_issue_qty,0)");

                    string attachmentFilePath = FileDir +  DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";//create file ret file path
                    if(!Misc.DBDataToXls(cmdText.ToString(), attachmentFilePath)) return;
                    string[] attachmentFiles = new string[] { attachmentFilePath };
                    string mailTo = "ming.li@yantai-raffles.net;juan.yan@yantai-raffles.net";
                    //string mailTo = "jikuan.lu@yantai-raffles.net;wancai.yu@yantai-raffles.net;yuanyuan.guo@yantai-raffles.net;xiaoying.sheng@yantai-raffles.net;chengfen.yang@yantai-raffles.net;" +
                    //    "lingyan.zhao@yantai-raffles.net;xiuling.xue@yantai-raffles.net;tianshuang.zhang@yantai-raffles.net;xiaobo.sun@yantai-raffles.net;jing01.li@yantai-raffles.net;" +
                    //    "ting.mu@yantai-raffles.net;zhilian.cui@yantai-raffles.net;hongsheng.yuan@yantai-raffles.net;qingyun.zhou@yantai-raffles.net;" +
                    //    "yingqiu.liu@yantai-raffles.net;xiaoyu.zhou@yantai-raffles.net;gaoqiang.wu@yantai-raffles.net;meiru.lv@yantai-raffles.net;zhenhua.li@yantai-raffles.net;" +
                    //    "chuanling.sun@yantai-raffles.net;ying03.li@yantai-raffles.net;yuping.yang@yantai-raffles.net;libo.fei@yantai-raffles.net;ying.sun@yantai-raffles.net;" +
                    //    "zhihua.feng@yantai-raffles.net;jing.sun@yantai-raffles.net;lijie.yu@yantai-raffles.net;tiantian.zhu@yantai-raffles.net;yuanjun.zhang@yantai-raffles.net";
                    
                    string mailCc = "ming.li@yantai-raffles.net";
                   // string mailCc = "chuanyong.huo@yantai-raffles.net;juan.xin@yantai-raffles.net;xin.chen@yantai-raffles.net;pengrong.wen@yantai-raffles.net;hansheng.liu@yantai-raffles.net";

                    myMail.Send("DMS-ERP下发对照-test", mailBody, "yias@yantai-raffles.net", mailTo, mailCc, true, false, attachmentFiles, null);
                    
                    break;
                default:
                    break;
            }   
        }
        
        public void Stop()
        {
            if (UpdateTimer != null)
            {
                UpdateTimer.Dispose();
                UpdateTimer = null;
            }
            WriteTextFile(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":stop.", ServerLogPath);
        }

    }
}
