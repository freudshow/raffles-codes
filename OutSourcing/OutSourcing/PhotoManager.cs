using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Text.RegularExpressions;

namespace OutSourcing
{
    public partial class Form_PhotoManager : Form
    {
        public Form_PhotoManager()
        {
            InitializeComponent();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            if (this.dTime_start.Value.Date > this.dTime_end.Value.Date)
            {
                MessageBox.Show("截止日期应不小于起始日期!");
                return;
            }
            
            string StartTime = this.dTime_start.Value.ToString().Split(' ')[0];
            string EndTime = this.dTime_end.Value.ToString().Split(' ')[0];
            
            string mDate, Year, Month, Day;
            string RecordID, IDSuffixNum, ICCard, Name;

            DataTable ManListInfo;
            OracleHelper newhelp=new OracleHelper();
            string sql=@"SELECT T.RECORD_ID, T.ID_NO,T.ID_PREFIX, T.NAME, T.USED_NAME, T.CREATE_DATE 
                            FROM cimc_lbr_idcard_read_trans_vie T 
                            WHERE TO_DATE(T.CREATE_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartTime + "','YYYY-MM-DD') AND TO_DATE('" + EndTime + "','YYYY-MM-DD')";

            try
            {
                ManListInfo = newhelp.GetDataTable(sql);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (ManListInfo.Rows.Count == 0)
            {
                MessageBox.Show("数据库中未查到此时间段人员信息");
                return;
            }
            this.dGV_infolist.DataSource = ManListInfo;

            for (int i = 0; i < ManListInfo.Rows.Count; i++)
            {
                RecordID = ManListInfo.Rows[i]["RECORD_ID"].ToString();
                ICCard = ManListInfo.Rows[i]["USED_NAME"].ToString();
                Name = ManListInfo.Rows[i]["Name"].ToString();
                IDSuffixNum = ManListInfo.Rows[i]["ID_PREFIX"].ToString();
                string mPath = "D:\\ICCARD_PHOTO\\";
                mDate = ManListInfo.Rows[i]["CREATE_DATE"].ToString();
                Year = mDate.Split('-')[0];
                Month = mDate.Split('-')[1];
                Day = mDate.Split('-')[2];

                if (this.textPath.Text == string.Empty)
                {
                    mPath += Year + "\\" + Month + "\\" + Day + "\\";
                    if (!Directory.Exists(mPath))
                    {
                        MessageBox.Show("入职日期的路径不存在, 请手动选择或建立相应文件夹");
                        return;
                    }
                }
                else
                {
                    mPath = this.textPath.Text;
                }

                if (!Directory.Exists(mPath + "ICCARD"))
                {
                    Directory.CreateDirectory(mPath + "ICCARD");
                }

                if (!Directory.Exists(mPath + "NAME"))
                {
                    Directory.CreateDirectory(mPath + "NAME");
                }

                string JpgName = mPath + IDSuffixNum + ".jpg";
                if (File.Exists(JpgName))
                {
                    //ICCard
                    if (ICCard == string.Empty)
                    {
                        CallProce(RecordID, "0", "0", "IC Card Not Exists");
                        continue;
                    }

                    try
                    {
                        File.Copy(JpgName, mPath + "ICCARD\\" + ICCard + ".jpg", true);
                    }
                    catch (System.Exception ex)
                    {
                        //Name
                        if (Name == string.Empty)
                        {
                            CallProce(RecordID, "0", "0", "Name Not Exists");
                            continue;
                        }

                        if (File.Exists(mPath + "NAME\\" + Name + ".jpg"))
                        {
                            try
                            {
                                File.Copy(JpgName, mPath + "NAME\\" + Name + "_" + IDSuffixNum + ".jpg", true);
                            }
                            catch (System.Exception err)
                            {
                                CallProce(RecordID, "1", "0", err.Message);
                                continue;
                            }
                        }

                        try
                        {
                            File.Copy(JpgName, mPath + "NAME\\" + Name + ".jpg", true);
                        }
                        catch (System.Exception err)
                        {
                            CallProce(RecordID, "0", "0", err.Message);
                            continue;
                        }
                        
                        CallProce(RecordID, "0", "1", ex.Message);
                        continue;
                    }
                    
                    //Name
                    if (Name == string.Empty)
                    {
                        CallProce(RecordID, "0", "0", "Name Not Exists");
                        continue;
                    }
                    if (!Directory.Exists(mPath + "NAME"))
                    {
                        Directory.CreateDirectory(mPath + "NAME");
                    }
                    else
                    {
                        if (File.Exists(mPath + "NAME\\" + Name + ".jpg"))
                        {
                            try
                            {
                                File.Copy(JpgName, mPath + "NAME\\" + Name + "_" + IDSuffixNum + ".jpg", true);
                            }
                            catch (System.Exception err)
                            {
                                CallProce(RecordID, "1", "0", err.Message);
                                continue;
                            }
                        }

                        try
                        {
                            File.Copy(JpgName, mPath + "NAME\\" + Name + ".jpg", true);
                        }
                        catch (System.Exception ex)
                        {
                            CallProce(RecordID, "1", "0", ex.Message);
                            continue;
                        }
                    }
                    CallProce(RecordID, "1", "1", "Succeed");
                }
            }
            MessageBox.Show("复制完毕");
        }

        private void CallProce(string RECORD_ID, string ICCARD_TRANS_FLAG, string NAME_TRANS_FLAG, string ERR_MSG)
        {
            OracleConnection conn = new OracleConnection(OracleHelper.OraConnectString);
            OracleCommand comm = new OracleCommand();
            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed) conn.Open();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "UPDATE_CIMC_LBR_IDCARD_TRANS";
            OracleTransaction oTran = conn.BeginTransaction();
            comm.Transaction = oTran;
            try
            {
                comm.Parameters.Add("RECORD_ID_", OracleType.Int32).Value = Convert.ToInt32(RECORD_ID);
                comm.Parameters.Add("ICCARD_TRANS_FLAG_", OracleType.VarChar, 1).Value = ICCARD_TRANS_FLAG;
                comm.Parameters.Add("NAME_TRANS_FLAG_", OracleType.VarChar, 1).Value = NAME_TRANS_FLAG;
                comm.Parameters.Add("ERR_MSG_", OracleType.VarChar, 3000).Value = ERR_MSG;
                comm.ExecuteNonQuery();
                oTran.Commit();
            }
            catch (Exception ex)
            {
                oTran.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                comm.Dispose();
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f1 = new FolderBrowserDialog();
            if (f1.ShowDialog() == DialogResult.OK)
            {
                this.textPath.Text = f1.SelectedPath;
            }
        }

        private void btn_CreateDir_Click(object sender, EventArgs e)
        {
            Form_CreateDir CreateDir = new Form_CreateDir();
            CreateDir.Show();
        }

        private void btn_CopyByIC_Click(object sender, EventArgs e)
        {
            if (this.dTime_start.Value.Date > this.dTime_end.Value.Date)
            {
                MessageBox.Show("截止日期应不小于起始日期!");
                return;
            }

            string StartTime = this.dTime_start.Value.ToString().Split(' ')[0];
            string EndTime = this.dTime_end.Value.ToString().Split(' ')[0];

            string mDate, Year, Month, Day;
            string RecordID, IDSuffixNum, ICCard, Name;

            DataTable ManListInfo;
            OracleHelper newhelp = new OracleHelper();
            string sql = @"SELECT T.RECORD_ID, T.ID_NO,T.ID_PREFIX, T.NAME, T.USED_NAME, T.CREATE_DATE 
                            FROM cimc_lbr_idcard_read_trans_vie T 
                            WHERE TO_DATE(T.CREATE_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartTime + "','YYYY-MM-DD') AND TO_DATE('" + EndTime + "','YYYY-MM-DD')";

            try
            {
                ManListInfo = newhelp.GetDataTable(sql);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (ManListInfo.Rows.Count == 0)
            {
                MessageBox.Show("数据库中未查到此时间段人员信息");
                return;
            }
            this.dGV_infolist.DataSource = ManListInfo;

            for (int i = 0; i < ManListInfo.Rows.Count; i++)
            {
                RecordID = ManListInfo.Rows[i]["RECORD_ID"].ToString();
                ICCard = ManListInfo.Rows[i]["USED_NAME"].ToString();
                Name = ManListInfo.Rows[i]["Name"].ToString();
                IDSuffixNum = ManListInfo.Rows[i]["ID_PREFIX"].ToString();
                string mPath = "D:\\ICCARD_PHOTO\\";
                mDate = ManListInfo.Rows[i]["CREATE_DATE"].ToString();
                Year = mDate.Split('-')[0];
                Month = mDate.Split('-')[1];
                Day = mDate.Split('-')[2];

                if (this.textPath.Text == string.Empty)
                {
                    mPath += Year + "\\" + Month + "\\" + Day + "\\";
                    if (!Directory.Exists(mPath))
                    {
                        MessageBox.Show("入职日期的路径不存在, 请手动选择或建立相应文件夹");
                        return;
                    }
                }
                else
                {
                    mPath = this.textPath.Text+"\\";
                }

                if (!Directory.Exists(mPath + "NAME"))
                {
                    Directory.CreateDirectory(mPath + "NAME");
                }

                string JpgName = mPath + ICCard + ".jpg";
                if (File.Exists(JpgName))
                {
                    //Name
                    if (Name == string.Empty)
                    {
                        CallProce(RecordID, "0", "0", "Name Not Exists in database");
                        continue;
                    }
                    if (!Directory.Exists(mPath + "NAME"))
                    {
                        try
                        {
                            Directory.CreateDirectory(mPath + "NAME");
                        }
                        catch (System.Exception ex)
                        {
                            CallProce(RecordID, "0", "0", "Can not create Name dir: " + mPath + "NAME" + ex.Message);
                            continue;
                        }
                        
                    }
                    else
                    {
                        if (File.Exists(mPath + "NAME\\" + Name + ".jpg"))
                        {
                            try
                            {
                                File.Copy(JpgName, mPath + "NAME\\" + Name + "_" + IDSuffixNum + ".jpg", true);
                                CallProce(RecordID, "0", "1", ICCard + ".jpg Copy Succeed");
                                continue;
                            }
                            catch (System.Exception err)
                            {
                                CallProce(RecordID, "1", "0", err.Message);
                                continue;
                            }
                        }

                        try
                        {
                            File.Copy(JpgName, mPath + "NAME\\" + Name + ".jpg", true);
                            CallProce(RecordID, "0", "1", ICCard + ".jpg Copy Succeed");
                        }
                        catch (System.Exception ex)
                        {
                            CallProce(RecordID, "0", "0", ex.Message);
                            continue;
                        }
                    }
                }
            }

            MessageBox.Show("复制完毕");
        }
    }
}