using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.IO;
using System.Net;
using System.Data.OracleClient;
using System.Collections;
using System.Threading;

namespace DetailInfo
{
    public partial class ModifyInfoForm : Form
    {

        public ModifyInfoForm()
        {
            InitializeComponent();
        }

        private string drawingno;

        public string Drawingno
        {
            get { return drawingno; }
            set { drawingno = value; }
        }

        private int drawingid;

        public int Drawingid
        {
            get { return drawingid; }
            set { drawingid = value; }
        }

        private string versionstr;

        public string Versionstr
        {
            get { return versionstr; }
            set { versionstr = value; }
        }

        private string originaldrawing;

        public string Originaldrawing
        {
            get { return originaldrawing; }
            set { originaldrawing = value; }
        }

        string filterstr = string.Empty;
        ModifyDrawingControl mdcontrol;
        private void ModifyInfoForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            this.modifynotb.Text = drawingno;
            this.versiontb.Text = versionstr;
            this.originaldrawingtb.Text = originaldrawing;
            BindWHStatus();

            mdcontrol = new ModifyDrawingControl();
            mdcontrol.Anchor = AnchorStyles.Top;
            mdcontrol.Anchor = AnchorStyles.Left;
            this.tableLayoutPanel1.Controls.Add(mdcontrol, 0, 2);
        }

        private void BindWHStatus()
        {
            DataSet ds = new DataSet();
            string matypesqlstr = "select status_desc, status_id from mf_productstatus_tab";
            User.DataBaseConnect(matypesqlstr, ds);
            this.whstatus.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            this.whstatus.DisplayMember = "status_desc";
            this.whstatus.ValueMember = "status_id";
            this.whstatus.SelectedIndex = -1;
        }

        /// <summary>
        /// 获取工作联系单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worksheettb_Click(object sender, EventArgs e)
        {
            filterstr = "Supported Image Types (*.doc,*.xls,*.pdf,*.txt)| *.doc;*.xls;*.pdf,*.txt";
            GetAttachment(this.worksheettb, filterstr);
        }

        /// <summary>
        /// 获取证明照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proofpictb_Click(object sender, EventArgs e)
        {
            filterstr = "Supported Image Types(*.jpg,*.png,*.jpeg,*.gif,*.bmp,*.tif)|*.jpg;*.png;*.jpeg;*.gif;*.bmp;*.tif";
            GetProofPic(this.proofpictb, filterstr);
        }
        /// <summary>
        /// 上传工作联系单
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="filter"></param>
        private void GetAttachment(TextBox tb, string filter)
        {
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.InitialDirectory = "D:";
            ofdialog.Filter = filter;
            if (ofdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb.Text = ofdialog.FileName.ToString();
            }
        }
        /// <summary>
        /// 获取证明照
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="filter"></param>
        private void GetProofPic(TextBox tb, string filter)
        {
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.InitialDirectory = "D:";
            ofdialog.Filter = filter;
            if (ofdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (tb.Text.ToString() == string.Empty)
                {
                    tb.Text = ofdialog.FileName.ToString();
                }
                else
                {
                    tb.Text = tb.Text + ";"+ofdialog.FileName.ToString();
                }
            }
        }

        /// <summary>
        /// 保存修改单或升版图纸的基本信息
        /// </summary>
        private void SaveModifyInfo()
        {
            string Cdata = "";
            if (DataAccess.severstr == "OIDS")
                Cdata = "http://172.16.7.55/";
            else
                Cdata = "http://172.20.64.3/";
            string sec_issue = string.Empty;
            string delay_issue = string.Empty;
            string file_name = string.Empty;
            string filepath = string.Empty;
            if (this.second_ck.Checked)
            {
                sec_issue = "Y";
            }
            else
            {
                sec_issue = "N";
            }
            if (delay_ck.Checked)
            {
                delay_issue = "Y";
            }
            else
            {
                delay_issue = "N";
            }
            int state = Convert.ToInt16(this.whstatus.SelectedValue.ToString());
            string reason = this.des_tb.Text.ToString();
            string solution = this.sol_tb.Text.ToString();
            string summary = this.con_tb.Text.ToString();
            string worksheetpath = this.worksheettb.Text.ToString();
            if (worksheetpath == string.Empty)
            {
                InsertModifyInfo(User.projectid, drawingid, sec_issue, delay_issue, state, reason, solution, summary, file_name, filepath, User.cur_user);
            }
            else
            {
                file_name = worksheetpath.Substring(worksheetpath.LastIndexOf("\\") + 1);
                filepath = this.UploadFile(Cdata + "UploadWorkSheet.aspx?filename=" + file_name, worksheetpath);
                InsertModifyInfo(User.projectid, drawingid, sec_issue, delay_issue, state, reason, solution, summary, file_name, filepath, User.cur_user);
            }
            DataSet ds = new DataSet();
            string sql = "select modi_id from mf_modifyinfo_tab where drawing_id  = '" + drawingid + "'";
            User.DataBaseConnect(sql, ds);
            int modifyid = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
            string proofpath = this.proofpictb.Text.ToString();
            if (proofpath != string.Empty)
            {
                string[] attachlist = proofpath.Split(new char[] { ',' });
                for (int i = 0; i < attachlist.Length; i++)
                {
                    string attachpath = attachlist[i].ToString();
                    file_name = attachpath.Substring(attachpath.LastIndexOf("\\") + 1);
                    filepath = this.UploadFile(Cdata + "UploadWorkSheet.aspx?filename=" + file_name, attachpath);
                    InsertModifyInfoAttach(modifyid, file_name, filepath, User.cur_user);
                }
            }
            SaveModifyInfoReason(modifyid);
            MessageBox.Show("-------------保存完成！------------");

        }

        /// <summary>
        /// 单个文件上传至服务器
        /// </summary>
        /// <param name="uriAddress">接收文件资源的URI, 例如: http://xxxx/Upload.aspx</param>
        /// <param name="filePath">要发送的资源文件, 例如: @"D:\workspace\WebService 相关.doc</param>
        /// <returns>返回文件保存的相对路径, 例如: "upload/xxxxx.jpg" 或者出错返回 ""</returns>
        private string UploadFile(string uriAddress, string filePath)
        {


            //利用 WebClient
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                byte[] responseArray = webClient.UploadFile(uriAddress, "POST", filePath);
                //webClient.DownloadData();
                string savePath = Encoding.UTF8.GetString(responseArray);
                return savePath;
            }
            catch (Exception err)
            {
                return err.ToString() + "上传失败";
            }
        }

        /// <summary>
        /// 保存修改单或升版图纸的修改原因
        /// </summary>
        private void SaveModifyInfoReason(int modifyid)
        {
            string sqlstr = "delete from MF_MODIFYINFOREASON_TAB where MODI_ID = '"+modifyid+"'";
            ExcuteSql(sqlstr, DataAccess.OIDSConnStr);
            int count = 0;
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                if (control.Name == "ModifyDrawingControl")
                {
                    count += 1;
                }
            }
            int rowcount = count + 2;
            for (int i = 2; i < rowcount; i++)
            {
                Control cntrl = tableLayoutPanel1.GetControlFromPosition(0, i);
                object reason = ((ComboBox)cntrl.Controls["groupBox1"].Controls["reason_cb"]).SelectedValue;
                string commentcode = ((TextBox)cntrl.Controls["groupBox1"].Controls["comcode_tb"]).Text.ToString();
                object responser = ((ComboBox)cntrl.Controls["groupBox1"].Controls["responsercomb"]).SelectedValue;
                object status = ((ComboBox)cntrl.Controls["groupBox1"].Controls["status_cb"]).SelectedValue;
                object type = ((ComboBox)cntrl.Controls["groupBox1"].Controls["typecob"]).SelectedValue;
                string materialcost = ((TextBox)cntrl.Controls["groupBox1"].Controls["materialcost_tb"]).Text.ToString();
                decimal techhrcost = Convert.ToDecimal(((TextBox)cntrl.Controls["groupBox1"].Controls["techcost_tb"]).Text.ToString());

                OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                OracleTransaction trans = conn.BeginTransaction();
                if (status.ToString() == "B")
                {
                    cmd.CommandText = @"insert into  MF_MODIFYINFOREASON_TAB (PROJECT_ID, DRAWING_ID, REASONTYPE_ID, DIS_SUPER, BA_FLAG, TECHHOUR_COST, MODI_ID, CREATER, COMMENT_NO) values (:projecid, :drawid, :reasontypeid, :supervisor, :BAflag, :techcost,  :moid,  :creater, :comcode)";

                    cmd.Parameters.Add("projecid", OracleType.Number).Value = User.projectid;
                    cmd.Parameters.Add("drawid", OracleType.Number).Value = drawingid;
                    cmd.Parameters.Add("reasontypeid", OracleType.Number).Value = Convert.ToInt16(reason);
                    cmd.Parameters.Add("comcode", OracleType.NVarChar).Value = commentcode;
                    cmd.Parameters.Add("supervisor", OracleType.NVarChar).Value = responser.ToString();
                    cmd.Parameters.Add("BAflag", OracleType.NVarChar).Value = status.ToString();
                    cmd.Parameters.Add("techcost", OracleType.Number).Value = techhrcost;
                    cmd.Parameters.Add("moid", OracleType.VarChar).Value = modifyid;
                    cmd.Parameters.Add("creater", OracleType.NVarChar).Value = User.cur_user;
                }
                else
                {
                    cmd.CommandText = @"insert into  MF_MODIFYINFOREASON_TAB (PROJECT_ID, DRAWING_ID, REASONTYPE_ID, DIS_SUPER, BA_FLAG, MATERAL_TYPE_ID, MATERAL_COST, TECHHOUR_COST, MODI_ID, CREATER,COMMENT_NO) values (:projecid, :drawid, :reasontypeid, :supervisor, :BAflag, :mattypeid, :matcost, :techcost,  :moid,  :creater, :comcode)";

                    cmd.Parameters.Add("projecid", OracleType.Number).Value = User.projectid;
                    cmd.Parameters.Add("drawid", OracleType.Number).Value = drawingid;
                    cmd.Parameters.Add("reasontypeid", OracleType.Number).Value = Convert.ToInt16(reason);
                    cmd.Parameters.Add("comcode", OracleType.NVarChar).Value = commentcode;
                    cmd.Parameters.Add("supervisor", OracleType.NVarChar).Value = responser.ToString();
                    cmd.Parameters.Add("BAflag", OracleType.NVarChar).Value = status.ToString();
                    cmd.Parameters.Add("mattypeid", OracleType.Number).Value = Convert.ToInt16(type);
                    cmd.Parameters.Add("matcost", OracleType.Number).Value = Convert.ToDecimal(materialcost);
                    cmd.Parameters.Add("techcost", OracleType.Number).Value = techhrcost;
                    cmd.Parameters.Add("moid", OracleType.VarChar).Value = modifyid;
                    cmd.Parameters.Add("creater", OracleType.NVarChar).Value = User.cur_user;
                }
                cmd.Transaction = trans;
                try
                {
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (OracleException ee)
                {
                    trans.Rollback();
                    MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 保存修改单或升版图纸信息以及工作联系单附件到数据库
        /// </summary>
        /// <param name="project_id"></param>
        /// <param name="drawingid"></param>
        /// <param name="sec_issue"></param>
        /// <param name="de_issue"></param>
        /// <param name="prostate"></param>
        /// <param name="mreason"></param>
        /// <param name="msolution"></param>
        /// <param name="msummary"></param>
        /// <param name="loacalfile"></param>
        /// <param name="despath"></param>
        /// <param name="username"></param>
        private void InsertModifyInfo(int project_id, int drawingid, string sec_issue, string de_issue,int prostate, string mreason, string msolution, string msummary, string loacalfile, string despath,string username)
        {
            string sqlstr = "select count(*) from mf_modifyinfo_tab where drawing_id = '"+drawingid+"'";
            object obj = User.GetScalar(sqlstr, DataAccess.OIDSConnStr);

            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            if (Convert.ToInt16(obj.ToString()) == 0 || obj == null)
            {
                cmd.CommandText = "insert into  mf_modifyinfo_tab (PROJECT_ID, DRAWING_ID, IS_SEC_ISSUE, IS_DELAY_ISSUE, ISSUE_PDUCT_STATE, MODIFY_REASON_DESC, MODIFY_SOLUTION, EXPERIENCE_SUMMARY, ATTACH_NAME, ATTACH_PATH,CREATER) values (:pid, : drawid, :secissue, :delayissue, :pdstate,:modreason, :modsolu, :modsum, :attname, :attpath, :creater)";
            }
            else
            {
                cmd.CommandText = @"update mf_modifyinfo_tab set PROJECT_ID = :pid, DRAWING_ID = :drawid, IS_SEC_ISSUE = :secissue, IS_DELAY_ISSUE = :delayissue, ISSUE_PDUCT_STATE = :pdstate, MODIFY_REASON_DESC = :modreason, MODIFY_SOLUTION = :modsolu, EXPERIENCE_SUMMARY = :modsum, ATTACH_NAME = :attname, ATTACH_PATH = :attpath,CREATER = :creater where drawing_id = '"+drawingid+"'";
            }

            OracleTransaction trans = conn.BeginTransaction();

            cmd.Parameters.Add("pid", OracleType.Number).Value = project_id;
            cmd.Parameters.Add("drawid", OracleType.Number).Value = drawingid;
            cmd.Parameters.Add("secissue", OracleType.NVarChar).Value = sec_issue;
            cmd.Parameters.Add("delayissue", OracleType.NVarChar).Value = de_issue;
            cmd.Parameters.Add("pdstate", OracleType.Number).Value = prostate;
            cmd.Parameters.Add("modreason", OracleType.NVarChar).Value = mreason;
            cmd.Parameters.Add("modsolu", OracleType.NVarChar).Value = msolution;
            cmd.Parameters.Add("modsum", OracleType.NVarChar).Value = msummary;
            cmd.Parameters.Add("attname", OracleType.NVarChar).Value = loacalfile;
            cmd.Parameters.Add("attpath", OracleType.NVarChar).Value = despath;
            cmd.Parameters.Add("creater", OracleType.VarChar).Value = username;
            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 保存证明图片
        /// </summary>
        /// <param name="modifyid"></param>
        /// <param name="loacalfile"></param>
        /// <param name="despath"></param>
        /// <param name="username"></param>
        private void InsertModifyInfoAttach(int modifyid,string loacalfile, string despath, string username)
        {
            string sql = "delete from mf_modifyinfoattach_tab where modi_id = '"+modifyid+"'";
            ExcuteSql(sql, DataAccess.OIDSConnStr);

            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into  mf_modifyinfoattach_tab (MODI_ID, ATTACH_NAME, ATTACH_PATH, CREATER) values (:moid, :attname, :attpath, :creater)";

            OracleTransaction trans = conn.BeginTransaction();

            cmd.Parameters.Add("moid", OracleType.Number).Value = modifyid;
            cmd.Parameters.Add("attname", OracleType.NVarChar).Value = loacalfile;
            cmd.Parameters.Add("attpath", OracleType.NVarChar).Value = despath;
            cmd.Parameters.Add("creater", OracleType.NVarChar).Value = username;

            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (OracleException ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sql_str"></param>
        /// <param name="con_str"></param>
         private  void ExcuteSql(string sql_str, string con_str)
        {
            OracleConnection conn = new OracleConnection(con_str);
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            OracleTransaction trans = conn.BeginTransaction();
            try
            {
                cmd.Transaction = trans;
                cmd.CommandText = sql_str;
                cmd.ExecuteNonQuery();
                trans.Commit();
                cmd.Dispose();

            }
            catch (OracleException ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            object workshopstatus = this.whstatus.SelectedValue;
            if (workshopstatus == null)
            {
                MessageBox.Show(this.whstatus_label.Text.ToString() + "不能为空！");
                return;
            }
            int count = 0;
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                if (control.Name == "ModifyDrawingControl")
                {
                    count += 1;
                }
            }
            int rowcount = count + 2;
            for (int i = 2; i < rowcount; i++)
            {
                Control cntrl = tableLayoutPanel1.GetControlFromPosition(0, i);
                object reason = ((ComboBox)cntrl.Controls["groupBox1"].Controls["reason_cb"]).SelectedValue;
                object responser = ((ComboBox)cntrl.Controls["groupBox1"].Controls["responsercomb"]).SelectedValue;
                object status = ((ComboBox)cntrl.Controls["groupBox1"].Controls["status_cb"]).SelectedValue;
                object type = ((ComboBox)cntrl.Controls["groupBox1"].Controls["typecob"]).SelectedValue;
                string materialcost = ((TextBox)cntrl.Controls["groupBox1"].Controls["materialcost_tb"]).Text.ToString();
                string techhrcost = ((TextBox)cntrl.Controls["groupBox1"].Controls["techcost_tb"]).Text.ToString();

                if (reason == null)
                {
                    MessageBox.Show("----------修改原因不能为空！--------");
                    return;
                }
                else if (responser == null)
                {
                    MessageBox.Show("----------责任主管不能为空！--------");
                    return;
                }
                else if (status == null)
                {
                        MessageBox.Show("----------产前/产后不能为空！--------");
                        return;
                }
                else if (status.ToString()  == "A")
                {
                    if (type == null)
                    {
                        MessageBox.Show("----------损耗材料类型不能为空！--------");
                        return;
                    }
                    else if (materialcost == string.Empty)
                    {
                        MessageBox.Show("----------生产损耗物量不能为空！--------");
                        return;
                    }
                }
                else if (status.ToString()  == "B")
                {
                    if (techhrcost == string.Empty)
                    {
                        MessageBox.Show("----------技术损耗工时不能为空！--------");
                        return;
                    }
                }
            }

            if (this.des_tb.Text.Length < 10)
            {
                MessageBox.Show(this.des_label.Text.ToString() + "不少于10个汉字！");
                return;
            }

            else if (this.sol_tb.Text.ToString() == string.Empty)
            {
                MessageBox.Show(this.sol_label.Text.ToString() +"不能为空！");
                return;
            }
            else if (this.con_tb.Text.ToString() == string.Empty)
            {
                MessageBox.Show(this.con_label.Text.ToString() + "不能为空！");
                return;
            }
            SaveModifyInfo();
            this.Close();
            //UploadToApprove();
            Control.CheckForIllegalCrossThreadCalls = false;
            ThreadStart threadstart = new ThreadStart(UploadToApprove);
            Thread thread = new Thread(threadstart);
            thread.IsBackground = true;
            thread.Start();

        }

        private void UploadToApprove()
        {
            if (!File.Exists(User.rootpath + "\\" + drawingno + ".zip"))
            {
                MessageBox.Show("请确定该图纸已经打包完成！");
                return;
            }
            else
            {
                string filepath = string.Empty;
                string filename = drawingno + ".zip";
                string Cdata = "";
                if (DataAccess.severstr == "OIDS")
                    Cdata = "http://172.16.7.55/";
                else
                    Cdata = "http://172.20.64.3/";

                filepath = this.UploadFile(Cdata + "ClientUpload.aspx?drawingno=" + drawingno + "&user=" + User.cur_user, User.rootpath + "\\" + drawingno + ".zip");
                if (filepath.Contains("失败"))
                {
                    MessageBox.Show(filepath, "上传失败");
                    return;
                }
                MessageBox.Show(filepath, "上传完毕");
                File.Delete(User.rootpath + "\\" + drawingno + ".zip");
                DBConnection.UpdateModifyDrawingStatus((int)FlowState.审核中, drawingno);
                foreach (Form form in MDIForm.pMainWin.MdiChildren)
                {
                    if (form.Name.ToString() == "PROJECTDRAWINGINFO")
                    {
                        form.Activate();
                        RadioButton radiobtn = (RadioButton)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["groupBox3"].Controls["modifyrbn"]);
                        if (radiobtn.Checked == true)
                        {
                            DataGridView dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["groupBox2"].Controls["DrawingsDgv"]);
                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                string cellvalue = dgv.Rows[i].Cells["DRAWINGNO"].Value.ToString();
                                if (cellvalue == drawingno)
                                {
                                    dgv.Rows[i].Cells["STATUS"].Value = "审核中";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ModifyInfoForm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }
    }
}
