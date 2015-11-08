using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Collections;
using System.IO;

namespace DetailInfo
{
    public partial class QCAttachment : Form
    {
        public QCAttachment()
        {
            InitializeComponent();
        }

        public string namestr;

        public string Namestr
        {
            get { return namestr; }
            set { namestr = value; }
        }

        ArrayList pathlist = new ArrayList();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开文件";
            ofd.Filter = "文档文件(*.doc,*.xls,*.pdf)| *.doc;*.xls;*.pdf";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in ofd.FileNames)
                {
                    pathlist.Add(s);
                    string name=   s.Substring (s.LastIndexOf("\\")+1);
                    this.attachmentlist.Items.Add(name);
                }
            }
        }
        /// <summary>
        /// 插入文档到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] spoolstr = namestr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            System.DateTime currentTime = System.DateTime.Now;
            if (this.attachmentlist.Items.Count != 0)
            {
                for (int i = 0; i < attachmentlist.Items.Count; i++)
                {
                    BinaryReader reader = null;
                    FileStream myfilestream = new FileStream(pathlist[i].ToString(), FileMode.Open, FileAccess.Read);
                    try
                    {
                        reader = new BinaryReader(myfilestream);
                        byte[] file = reader.ReadBytes((int)myfilestream.Length);
                        using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                        {
                            conn.Open();
                            using (OracleCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "insert into SPLATTACHMENT_TAB (FILENAME, UPLOADER, UPLOADTIME,UPLOADFILE) VALUES ('" + attachmentlist.Items[i] + "', '" + User.cur_user + "', to_date('" + currentTime + "','yyyy-mm-dd hh24:mi:ss'), :dfd)";
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
                        object id = User.GetScalar("select max(id) from splattachment_tab", DataAccess.OIDSConnStr);
                        foreach (string sp in spoolstr)
                        {
                            string sql_spool = "insert into SPLINATT_TAB (ID, SPOOLNAME) VALUES('" + id + "', '" + sp + "')";
                            User.UpdateCon(sql_spool, DataAccess.OIDSConnStr);
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

            }
            else
            {
                MessageBox.Show("请确认已经添加附件！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            //return;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            for (int i = attachmentlist.SelectedItems.Count - 1; i > -1; i--)
            {
                attachmentlist.Items.Remove(attachmentlist.SelectedItems[i]);
            }
        }
    }
}