using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.IO;
namespace proj_del
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            FileInfo finfo = new FileInfo("\\log.txt");
            FileStream fs = finfo.OpenWrite();
            for (int i = 0; i < 130; i++)
            {
                if (IsRefD(Convert.ToInt32(this.textBox1.Text)))
                {
                    
                    
                        //根据上面创建的文件流创建写数据流  
                        StreamWriter w = new StreamWriter(fs);
                        //设置写数据流的起始位置为文件流的末尾  
                        w.BaseStream.Seek(0, SeekOrigin.End);
                        //写入“Log Entry : ”  
                        w.Write("\nLog Entry : ");
                        //写入当前系统时间并换行  
                        w.Write(
                            "{0} {1} \r\n",
                            DateTime.Now.ToLongTimeString(),
                            DateTime.Now.ToLongDateString());
                        //写入日志内容并换行  
                        w.Write("found ! " + i);
                        //写入----------------“并换行  
                        w.Write("------------------\n");
                        //清空缓冲区内容，并把缓冲区内容写入基础流  
                        w.Flush();
                        //关闭写数据流  
                        w.Close();                 
                    return;
                }
                else
                    MessageBox.Show("Not found !" + i);
            }

        }

        /// <summary>
        /// 检查表PROJECT_TAB中的项目号是否被引用
        /// </summary>
        /// <param name="id"></param>
        public static bool IsRefD(int prj_id)
        {
            try
            {
                /*
                 * 得到引用过PROJECT_ID字段的表(查找每个表的字段中有无类似PROJECT_ID字样)
                 */
                OracleConnection OraCon = new OracleConnection("Data Source=oidsnew;User ID=plm;Password=123!feed;Unicode=True");
                OraCon.Open();

                OracleDataAdapter OrclPrjAdapter = new OracleDataAdapter();
                OracleCommand OrclPrjCmd = OraCon.CreateCommand();
                OrclPrjAdapter.SelectCommand = OrclPrjCmd;
                OrclPrjCmd.CommandText = @"SELECT T.TABLE_NAME,T.COLUMN_NAME FROM USE_PROJECTID_TABLES_VIEW T";
                DataSet Mydata = new DataSet();
                OrclPrjAdapter.Fill(Mydata);
                /*
                 * 遍历每个表
                 * {
                 *      表中的PROJECT_ID字样的列中查找是否出现项目号
                 *      如果找到此项目号，则返回 true
                 *      如果没有，则继续查找下一个表
                 * }
                 * 默认返回 false
                 */
                DataSet tmpData = new DataSet();
                string QueryPrjIdCmdStr = string.Empty;
                for (int i = 0; i < Mydata.Tables[0].Rows.Count; i++)
                {
                    QueryPrjIdCmdStr = @"SELECT T." + Mydata.Tables[0].Rows[i][1] + " FROM " + Mydata.Tables[0].Rows[i][0] + " T WHERE TO_CHAR(T." + Mydata.Tables[0].Rows[i][1] + ")=TO_CHAR(" + prj_id+")";
                    OrclPrjCmd.CommandText = QueryPrjIdCmdStr;
                    OrclPrjAdapter.Fill(tmpData);
                    if (tmpData.Tables[0].Rows.Count > 0)
                        return true;
                }

                return false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return true;
            }
        }
    }
}