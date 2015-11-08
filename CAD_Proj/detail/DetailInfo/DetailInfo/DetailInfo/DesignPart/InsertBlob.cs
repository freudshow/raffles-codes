using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OracleClient;

namespace DetailInfo
{
    public partial class InsertBlob : Form
    {
        public InsertBlob()
        {
            InitializeComponent();
        }

        private void FilePathLb_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.InitialDirectory = "D:";
            ofdialog.Filter = "Supported Image Types (*.pdf)|*.pdf";
            if (ofdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FilePathLb.Text = ofdialog.FileName.ToString();
            }
        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            string project = this.textBox1.Text.ToString();
            string drawing = this.textBox2.Text.ToString();
            string pathstr = this.FilePathLb.Text.ToString();
            string sqlStr = string.Empty;
            if (project == string.Empty || drawing == string.Empty || pathstr == string.Empty)
            {
                MessageBox.Show("项目,图纸号和目标文件均不能为空！");
                return;
            }

            sqlStr = "SELECT COUNT(*) FROM SP_CREATEPDFDRAWING T WHERE T.PROJECTID = '" + project + "' AND T.DRAWINGNO = '" + drawing + "' AND T.FLAG = 'Y' ";
            object rowcount = User.GetScalar(sqlStr, DataAccess.OIDSConnStr);
            if (int.Parse(rowcount.ToString()) == 0)
            {
                MessageBox.Show("数据库没有查询到该图纸记录，可重新在CATIA端打印封页！","信息提示！",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if (this.radioButton1.Checked == true)
            {
                sqlStr = "UPDATE SP_CREATEPDFDRAWING SET UPDATEDFRONTPAGE = :dfd WHERE PROJECTID = '" + project + "' AND DRAWINGNO = '" + drawing + "' AND FLAG = 'Y'";
            }
            else if (this.radioButton2.Checked == true)
            {
                sqlStr = "UPDATE SP_CREATEPDFDRAWING SET MODIFYDRAWINGFRONTPAGE = :dfd WHERE PROJECTID = '" + project + "' AND DRAWINGNO = '" + drawing + "' AND FLAG = 'Y'";
            }
            else 
            {
                MessageBox.Show("请选择封皮类别！");
                return; 
            }

            UpdateFrontPage(pathstr, sqlStr);
            MessageBox.Show("--------OK-------");


        }

        private void CALbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void UpdateFrontPage(string filepath, string sql)
        {
            BinaryReader reader = null;
            FileStream myfilestream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            try
            {
                reader = new BinaryReader(myfilestream);
                byte[] file = reader.ReadBytes((int)myfilestream.Length);
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
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
                }

            }
            catch (OracleException oex)
            {
                MessageBox.Show(oex.Message.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private void InsertBlob_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }


    }
}
