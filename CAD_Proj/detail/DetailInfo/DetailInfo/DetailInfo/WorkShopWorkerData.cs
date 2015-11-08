using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.IO;

namespace DetailInfo
{
    public partial class WorkShopWorkerData : Form
    {
        public WorkShopWorkerData()
        {
            InitializeComponent();
        }

        private void WorkShopWorkerData_Load(object sender, EventArgs e)
        {
            //string[] listStr = new string[] { "Y","N"};
            //this.STCb.Items.AddRange(listStr);
        }

        /// <summary>
        /// 保存员工信息到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (this.CHtb.Text == string.Empty || this.ENTb.Text == string.Empty || this.ICTb.Text == string.Empty || this.TEAMTb.Text == string.Empty || this.TLTb.Text == string.Empty )
            {
                MessageBox.Show("带*号的不能为空！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string CH_name = this.CHtb.Text.ToString();
            string EN_name = this.ENTb.Text.ToString();
            string IC_NO = this.ICTb.Text.ToString();
            string TEAM_NO = this.TEAMTb.Text.ToString();
            string TL_NO = this.TLTb.Text.ToString();
            string Eaddr = this.EMTb.Text.ToString();
            DBConnection.WorkShopNewWorker(CH_name, EN_name, IC_NO, TEAM_NO, TL_NO, Eaddr);

            string rootStr = this.PICTb.Text.ToString();
            if (rootStr != string.Empty)
            {
                SaveWorkerPhoto(rootStr, CH_name, EN_name, IC_NO);
            }

            MessageBox.Show("数据已经保存完成！");
            

        }

        /// <summary>
        /// 清空所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            this.CHtb.Clear(); this.ENTb.Clear(); this.ICTb.Clear(); this.TEAMTb.Clear(); this.TLTb.Clear(); 
            this.EMTb.Clear();  this.PICTb.Clear();
            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image = null;
            }
        }

        /// <summary>
        /// 退出该form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RootBtn_Click(object sender, EventArgs e)
        {
            string fileStr = string.Empty;
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.InitialDirectory = "C:";
            ofdialog.Filter = "Supported Image Types (*.jpg, *.gif, *.bmp)|*.jpg;*.gif;*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|BITMAP Image|*.bmp"; 
            if (ofdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileStr = ofdialog.FileName.ToString();
            }
            this.PICTb.Text = fileStr;
            if (fileStr != string.Empty)
            {
                this.pictureBox1.Image = Image.FromFile(fileStr);
            }
            
        }

        private void SaveWorkerPhoto(string picPath, string CHname, string EGname,string IDcard)
        {
            BinaryReader reader = null;
            FileStream myfilestream = new FileStream(picPath, FileMode.Open, FileAccess.Read);
            try
            {
                reader = new BinaryReader(myfilestream);
                byte[] file = reader.ReadBytes((int)myfilestream.Length);
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = "update workshopinfo_tab set PICTURE = :dfd where NAME_CHN = '" + CHname + "' and NAME_ENG = '" + EGname + "' and IDCARD = '"+IDcard+"' and STATUS = 'Y'";
                        OracleParameter op = new OracleParameter("dfd", OracleType.Blob);
                        op.Value = file;
                        if (file.Length == 0)
                        {
                            MessageBox.Show("插入文档不能为空！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        else
                        {
                            cmd.Parameters.Add(op);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                    myfilestream.Close();
                    conn.Close();
                    //System.IO.File.Delete(drawingpath);

                }
            }
            catch (IOException ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private void WorkShopWorkerData_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
            MDIForm.tool_strip.Items[2].Enabled = false;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string fileStr = string.Empty;
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.InitialDirectory = "C:";
            ofdialog.Filter = "Supported Image Types (*.jpg, *.gif, *.bmp)|*.jpg;*.gif;*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|BITMAP Image|*.bmp";
            if (ofdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileStr = ofdialog.FileName.ToString();
            }
            this.PICTb.Text = fileStr;
            if (fileStr != string.Empty)
            {
                this.pictureBox1.Image = Image.FromFile(fileStr);
            }
        }


    }
}
