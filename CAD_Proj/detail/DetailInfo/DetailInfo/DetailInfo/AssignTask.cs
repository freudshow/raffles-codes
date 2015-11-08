using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.IO;
using System.Collections;

namespace DetailInfo
{
    public partial class AssignTask : Form
    {
        public AssignTask()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }
        public string personnames;

        public string Personnames
        {
            get { return personnames; }
            set { personnames = value; }
        }
        ArrayList alist = new ArrayList();
        private string sqlstr = string.Empty;
        private string selectedstr = string.Empty;

        private void AssignTask_Load(object sender, EventArgs e)
        {
            sqlstr = "SELECT DISTINCT TEAM FROM WORKSHOPINFO_TAB WHERE STATUS = 'Y'";
            DetailInfo.Application_Code.FillComboBox.GetFlowStatus(this.teamcb, sqlstr);

        }

        private void teamcb_SelectedIndexChanged(object sender, EventArgs e)
        {
            alist.Clear();
            DataSet ds = new DataSet();
            sqlstr = "SELECT NAME_CHN,NAME_ENG, (select count(allocation) from SP_Spool_Tab s where s.allocation like '%'||t.name_chn||'%' and s.flowstatus = 4) PROCESS, IDCARD, TEL_NO FROM WORKSHOPINFO_TAB T WHERE STATUS = 'Y' AND TEAM = '" + this.teamcb.SelectedItem.ToString() + "'";
            User.DataBaseConnect(sqlstr, ds);
            this.workerdgv.DataSource = ds.Tables[0].DefaultView;
            //ds.Dispose();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                using (OracleConnection connection = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandText = "select * from  WORKSHOPINFO_TAB WHERE 1=1 AND NAME_CHN = '" + ds.Tables[0].Rows[i][0] + "' and IDCARD = '" + ds.Tables[0].Rows[i][3] + "' and STATUS = 'Y'";
                    OracleDataReader dr = command.ExecuteReader();
                    string filepath = string.Empty;
                    while (dr.Read())
                    {
                        if (dr["PICTURE"] != null)//如果文章内容为空 不能转二进制
                        {
                            try
                            {
                                byte[] b1 = (byte[])dr["PICTURE"];
                                string pathstr = User.rootpath + "\\" + "shopworker";
                                if (!Directory.Exists(pathstr))//若文件夹不存在则新建文件夹   
                                {
                                    Directory.CreateDirectory(pathstr); //新建文件夹   
                                }

                                filepath = pathstr + "\\" + dr["NAME_CHN"] + ".jpg";
                                FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(b1, 0, b1.Length);
                                bw.Close();
                                fs.Close();
                            }
                            catch (SystemException ex)
                            {
                                filepath = string.Empty;
                            }
                        }
                        if (filepath == string.Empty)
                        {
                            this.workerdgv[1, i].Value = null;
                        }
                        else
                        {
                            Image tt = Image.FromFile(filepath);
                            this.workerdgv[1, i].Value = tt;
                            alist.Add(tt);
                        }

                    }
                    dr.Close();
                    connection.Close();
                }
            }

            ds.Dispose();

        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            int totalcount = this.workerdgv.Rows.Count;
            if (totalcount != 0)
            {
                for (int i = 0; i < this.workerdgv.Rows.Count; i++)
                {
                    if (this.workerdgv.Rows[i].Cells[0].FormattedValue.ToString() == "True")
                    {
                        selectedstr = this.workerdgv.Rows[i].Cells["NAME_CHN"].Value.ToString();

                        personnames = personnames + "," + selectedstr;
                    } 
                }
                personnames = personnames.Remove(0, 1).ToString();
            }
            else
            {
                return;
            }


        }

        private void AssignTask_FormClosed(object sender, FormClosedEventArgs e)
        {
            string root = User.rootpath + "\\" + "shopworker";
            DirectoryInfo dir = new DirectoryInfo(root);
            if (dir.Exists)
            {
                System.IO.Directory.Delete(root, true);
            }
        }

        private void AssignTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < alist.Count; i++)
            {
                ((Image)alist[i]).Dispose();
            }
        }

        private void AssignTask_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = false;
        }




    }
}
