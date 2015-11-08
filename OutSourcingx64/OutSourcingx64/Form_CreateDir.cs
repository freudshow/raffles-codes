using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OutSourcingx64
{
    public partial class Form_CreateDir : Form
    {
        public Form_CreateDir()
        {
            InitializeComponent();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f1 = new FolderBrowserDialog();
            if (f1.ShowDialog() == DialogResult.OK)
            {
                this.text_browse.Text = f1.SelectedPath;
            }
        }

        private void btn_CreateImage(object sender, EventArgs e)
        {
            DataTable ManListInfo;
            OracleHelper newhelp = new OracleHelper();
            string sql = @"SELECT T.RECORD_ID, T.ID_NO,T.ID_PREFIX, T.NAME, T.USED_NAME, T.CREATE_DATE 
                            FROM cimc_lbr_idcard_read_trans_vie T 
                            WHERE TO_DATE(T.CREATE_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('2013-03-22','YYYY-MM-DD') AND TO_DATE('2013-03-22','YYYY-MM-DD')";

            try
            {
                ManListInfo = newhelp.GetDataTable(sql);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            string mPath = "D:\\ICCARD_PHOTO\\2013\\03\\22\\";
            for (int i = 0; i < ManListInfo.Rows.Count; i++)
            {
                string ICCard = ManListInfo.Rows[i]["USED_NAME"].ToString();
                if (ICCard==string.Empty)
                {
                    continue;
                }
                string filename = mPath + ICCard + ".jpg";
                File.Create(filename);
            }
            MessageBox.Show("done");
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            DateTime mdt = this.dateTimePicker1.Value.Date.AddDays(1 - this.dateTimePicker1.Value.Date.Day);//当月月初
            DateTime endMonth = mdt.AddMonths(1).AddDays(-1);  //当月月末

            string mDir;
            string mYear, mMonth, mDay;
            if (this.text_browse.Text == string.Empty)
            {
                mDir = "D:\\ICCARD_PHOTO\\";
            }
            else
            {
                mDir = this.text_browse.Text;
            }

            while (mdt <= endMonth)
            {
                mYear = mdt.Year.ToString();
                if (mdt.Month < 10)
                {
                    mMonth = "0" + mdt.Month.ToString();
                }
                else
                {
                    mMonth = mdt.Month.ToString();
                }
                if (mdt.Day < 10)
                {
                    mDay = "0" + mdt.Day.ToString();
                }
                else
                {
                    mDay = mdt.Day.ToString();
                }
                
                string mFilePath =  mDir + mYear + "\\" + mMonth + "\\" + mDay;
                if (!Directory.Exists(mFilePath))
                {
                    Directory.CreateDirectory(mFilePath);
                }
                mdt = mdt.AddDays(1);
            }
            MessageBox.Show("创建完毕!");
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}