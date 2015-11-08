using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class FrmProject : Form
    {
        public decimal okstan = 0.8M;
        public decimal midstan = 0.5M;
        //public decimal weight_design = 0.33M;
        //public decimal weight_set = 0.33M;
        //public decimal weight_install = 0.33M;
        public decimal weight_design = 0.2M;
        public decimal weight_set = 0.2M;
        public decimal weight_qc = 0.2M;
        public decimal weight_install = 0.2M;
        public decimal weight_modulation = 0.2M;
        public FrmProject()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }

        private void FrmProject_Load(object sender, EventArgs e)
        {
            string Sql = "select distinct projectid from SP_SPOOL_TAB ";
            User.ComFill(cmb_project, Sql);
            cmb_project.SelectedValue = "SSCV H218";

            ((TreeView)MDIForm.ActiveForm.ActiveMdiChild.Controls["panel1"].Controls["treeView1"]).NodeMouseClick += new TreeNodeMouseClickEventHandler(FrmProject_NodeMouseClick);
        }

        void FrmProject_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
             TreeNode tn = e.Node;
             if (tn.Name == "System")
            {
                string sys = tn.Tag.ToString();
                StringBuilder sb = new StringBuilder();
                string SqlSystem = string.Empty;

                SqlSystem = "select projectid,SYSTEMID,spoolname,systemname,PIPEGRADE,surfacetreatment,WORKINGPRESSURE,PRESSURETESTFIELD,PIPECHECKFIELD,drawingno,blockno,flowstatus from SP_SPOOL_TAB  t WHERE flag='Y' and  t.projectid='" + cmb_project.SelectedValue.ToString() + "' and t.systemid='" + sys + "' ";
                listviewBind(SqlSystem);
            }
            else if (tn.Name == "Status")
            {
                ((TreeView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["panel1"].Controls["treeView1"])).SelectedNode = tn;
                int pidtag = Convert.ToInt32(tn.Tag.ToString());
                string sys = tn.Parent.Tag.ToString();
                StringBuilder sb = new StringBuilder();
                string SqlspoolCountok = string.Empty;

                if (pidtag == 1)
                    SqlspoolCountok = "select projectid,SYSTEMID,spoolname,systemname,PIPEGRADE,surfacetreatment,WORKINGPRESSURE,PRESSURETESTFIELD,PIPECHECKFIELD,drawingno,blockno,flowstatus from SP_SPOOL_TAB  t WHERE flag='Y' and  t.flowstatus>=2 and t.flowstatus not in (5,6,8,9) and t.projectid='" + cmb_project.SelectedValue.ToString() + "' and t.systemid='" + sys + "' ";
                else if (pidtag == 2)
                    SqlspoolCountok = "select projectid,SYSTEMID,spoolname,systemname,PIPEGRADE,surfacetreatment,WORKINGPRESSURE,PRESSURETESTFIELD,PIPECHECKFIELD,drawingno,blockno,flowstatus from SP_SPOOL_TAB t  WHERE flag='Y' and  t.flowstatus>=7 and t.flowstatus not in (8,9) and t.projectid='" + cmb_project.SelectedValue.ToString() + "' and t.systemid='" + sys + "' ";
                else if (pidtag == 3)
                    SqlspoolCountok = "select projectid,SYSTEMID,spoolname,systemname,PIPEGRADE,surfacetreatment,WORKINGPRESSURE,PRESSURETESTFIELD,PIPECHECKFIELD,drawingno,blockno,flowstatus from SP_SPOOL_TAB t  WHERE flag='Y' and  t.flowstatus>=11  and t.projectid='" + cmb_project.SelectedValue.ToString() + "' and t.systemid='" + sys + "'";
                else if (pidtag == 4)
                    SqlspoolCountok = "select projectid,SYSTEMID,spoolname,systemname,PIPEGRADE,surfacetreatment,WORKINGPRESSURE,PRESSURETESTFIELD,PIPECHECKFIELD,drawingno,blockno,flowstatus from SP_SPOOL_TAB t  WHERE flag='Y' and  t.flowstatus>=13 and t.projectid='" + cmb_project.SelectedValue.ToString() + "' and t.systemid='" + sys + "' ";
                else if (pidtag == 5)
                    SqlspoolCountok = "select projectid,SYSTEMID,spoolname,systemname,PIPEGRADE,surfacetreatment,WORKINGPRESSURE,PRESSURETESTFIELD,PIPECHECKFIELD,drawingno,blockno,flowstatus from SP_SPOOL_TAB t  WHERE flag='Y' and  t.flowstatus=14 and t.projectid='" + cmb_project.SelectedValue.ToString() + "' and t.systemid='" + sys + "'"; 

                listviewBind(SqlspoolCountok);
            }
            //throw new Exception("The method or operation is not implemented.");
        }
        public void listviewBind(string sql)
        {
            DataGridView d;
            try
            {
                d = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["panel2"].Controls["dataGridView1"]);
                d.AutoGenerateColumns = false;
                d.Rows.Clear();
            }
            catch(SystemException e)
            {
                MessageBox.Show(e.Message.ToString());
                return;
            }
            DataSet ds = new DataSet();
            User.DataBaseConnect(sql, ds);
            DataView dv = ds.Tables[0].DefaultView;

            foreach (DataRow dr in dv.Table.Rows)
            {
                string sqlState = "select name from spflowstatus_tab t where t.id=" + dr[11].ToString();
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(d);
                r.Cells[0].Value = dr[0].ToString();
                r.Cells[1].Value = dr[1].ToString();
                r.Cells[2].Value = dr[2].ToString();
                r.Cells[3].Value = dr[3].ToString();
                r.Cells[4].Value = dr[4].ToString();
                r.Cells[5].Value = dr[5].ToString();
                r.Cells[6].Value = dr[6].ToString();
                r.Cells[7].Value = dr[7].ToString();
                r.Cells[8].Value = dr[8].ToString();
                r.Cells[9].Value = dr[9].ToString();
                r.Cells[10].Value = dr[10].ToString();
                r.Cells[11].Value = (string)User.GetScalar1(sqlState, DataAccess.OIDSConnStr);
                d.Rows.Add(r);

            }
        }
        public void GridViewTitleBind()
        {
            DataGridView d=(DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["panel2"].Controls["dataGridView1"]);
            d.Columns.Add("projectid", "项目号");
            d.Columns.Add("SYSTEMID", "系统号");
            d.Columns.Add("spoolname", "小票号");
            d.Columns.Add("systemname", "系统名");
            d.Columns.Add("PIPEGRADE", "管路等级");

            d.Columns.Add("surfacetreatment", "表面处理");
            d.Columns.Add("WORKINGPRESSURE", "工作压力");
            d.Columns.Add("PRESSURETESTFIELD", "压力试验场所");
            d.Columns.Add("PIPECHECKFIELD", "校管场所");
            d.Columns.Add("drawingno", "图纸编号");
            d.Columns.Add("blockno", "分段号");
            d.Columns.Add("flowstatus", "小票状态");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            //((TreeView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["panel1"].Controls["treeView1"])).Nodes.Clear();
            ((TreeView)(MDIForm.pMainWin.ActiveMdiChild.Controls["panel1"].Controls["treeView1"])).Nodes.Clear();
            string prid = cmb_project.SelectedValue.ToString();
            TreeNode tn = new TreeNode();
            tn.Tag = prid;
            tn.Text = prid;
            tn.Name = "Project";
            tn.SelectedImageIndex = 3;
            //((TreeView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["panel1"].Controls["treeView1"])).Nodes.Add(tn);
            ((TreeView)(MDIForm.pMainWin.ActiveMdiChild.Controls["panel1"].Controls["treeView1"])).Nodes.Add(tn);

            DataSet ds = new DataSet();
            string Syssql = string.Empty;
            Syssql = "select distinct t.systemid from SP_SPOOL_TAB t where t.projectid='" + prid + "'";

            User.DataBaseConnect(Syssql, ds);
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string SqlspoolCountok_design = string.Empty;
                string SqlspoolCountok_set = string.Empty;
                string SqlspoolCountok_qc = string.Empty;
                string SqlspoolCountok_install = string.Empty;
                string SqlspoolCountok_modulation = string.Empty;
                //string SqlspoolCountok_design = string.Empty;
                //string SqlspoolCountok_set = string.Empty;
                //string SqlspoolCountok_install = string.Empty;

                SqlspoolCountok_design = "select count(t.rowid) from SP_SPOOL_TAB t where  t.flag='Y' and  t.flowstatus>=2 and t.flowstatus not in (5,6,8,9) and t.projectid='" + prid + "' and t.systemid='" + dt.Rows[i][0].ToString() + "' ";

                SqlspoolCountok_set = "select count(t.rowid) from SP_SPOOL_TAB t where t.flag='Y' and  t.flowstatus>=7 and t.flowstatus not in (8,9) and t.projectid='" + prid + "' and t.systemid='" + dt.Rows[i][0].ToString() + "' ";

                SqlspoolCountok_qc = "select count(t.rowid) from SP_SPOOL_TAB t where t.flag='Y' and  t.flowstatus>=11  and t.projectid='" + prid + "' and t.systemid='" + dt.Rows[i][0].ToString() + "' ";

                SqlspoolCountok_install = "select count(t.rowid) from SP_SPOOL_TAB t where t.flag='Y' and  t.flowstatus>=13 and t.projectid='" + prid + "' and t.systemid='" + dt.Rows[i][0].ToString() + "' ";

                SqlspoolCountok_modulation = "select count(t.rowid) from SP_SPOOL_TAB t where t.flag='Y' and  t.flowstatus =14  and t.projectid='" + prid + "' and t.systemid='" + dt.Rows[i][0].ToString() + "' ";

                string SqlSpoolCountSum = "select count(t.rowid) from SP_SPOOL_TAB t where  t.flag='Y' and t.projectid='" + prid + "' and t.systemid='" + dt.Rows[i][0].ToString() + "' ";

                //object count_design = User.GetScalar1(SqlspoolCountok_design, DataAccess.OIDSConnStr);
                //object count_set = User.GetScalar1(SqlspoolCountok_set, DataAccess.OIDSConnStr);
                //object count_install = User.GetScalar1(SqlspoolCountok_install, DataAccess.OIDSConnStr);
                object count_design = User.GetScalar1(SqlspoolCountok_design, DataAccess.OIDSConnStr);
                object count_set = User.GetScalar1(SqlspoolCountok_set, DataAccess.OIDSConnStr);
                object count_qc = User.GetScalar1(SqlspoolCountok_qc, DataAccess.OIDSConnStr);
                object count_install = User.GetScalar1(SqlspoolCountok_install, DataAccess.OIDSConnStr);
                object count_modulatioin = User.GetScalar1(SqlspoolCountok_modulation, DataAccess.OIDSConnStr);

                object Sum = User.GetScalar1(SqlSpoolCountSum, DataAccess.OIDSConnStr);

                try
                {
                    //decimal d_design = Math.Round((decimal)count_design / (decimal)Sum, 4);
                    //decimal d_set = Math.Round((decimal)count_set / (decimal)Sum, 4);
                    //decimal d_install = Math.Round((decimal)count_install / (decimal)Sum, 4);
                    decimal d_design = Math.Round((decimal)count_design / (decimal)Sum, 4);
                    decimal d_set = Math.Round((decimal)count_set / (decimal)Sum, 4);
                    decimal d_qc = Math.Round((decimal)count_qc / (decimal)Sum, 4);
                    decimal d_install = Math.Round((decimal)count_install / (decimal)Sum, 4);
                    decimal d_modulation = Math.Round((decimal)count_modulatioin / (decimal)Sum, 4);

                    decimal res = d_design * weight_design + d_set * weight_set + d_qc * weight_qc + d_install * weight_install + d_modulation * weight_modulation;
                    //decimal res = d_design * weight_design + d_set * weight_set + d_install * weight_install;


                    TreeNode systn = new TreeNode();
                    int staId = 0;
                    if (res >= okstan)
                        staId = 0;
                    else if (res >= midstan && res < okstan)
                        staId = 1;
                    else
                        staId = 2;
                    systn.ImageIndex = staId;
                    systn.SelectedImageIndex = staId;


                    string sysname = "select sysname from SP_SYSTEM t where  t.sysid='" + dt.Rows[i][0].ToString() + "'";
                    systn.Name = "System";
                    systn.Tag = dt.Rows[i][0].ToString();


                    systn.Text = (string)User.GetScalar1(sysname, DataAccess.OIDSConnStr) + " (" + res * 100 + "%)总数为:" + Sum.ToString();
                    tn.Nodes.Add(systn);
                    TreeNode tnNum = new TreeNode();
                    tnNum.Text = "设计下发数量:" + count_design.ToString();
                    tnNum.Tag = 1;
                    tnNum.Name = "Status";
                    tnNum.ImageIndex = 4;
                    tnNum.SelectedImageIndex = 4;
                    systn.Nodes.Add(tnNum);
                    TreeNode tnNum_set = new TreeNode();
                    tnNum_set.Text = "管加工完成数量:" + count_set.ToString();
                    tnNum_set.Tag = 2;
                    tnNum_set.Name = "Status";
                    tnNum_set.ImageIndex = 4;
                    tnNum_set.SelectedImageIndex = 4;
                    systn.Nodes.Add(tnNum_set);

                    TreeNode tnNum_qc = new TreeNode();
                    tnNum_qc.Text = "检验完成数量:" + count_qc.ToString() ;
                    tnNum_qc.Tag = 3;
                    tnNum_qc.Name = "Status";
                    tnNum_qc.ImageIndex = 4;
                    tnNum_qc.SelectedImageIndex = 4;
                    systn.Nodes.Add(tnNum_qc);

                    TreeNode tnNum_install = new TreeNode();
                    tnNum_install.Text = "安装完成数量:" + count_install.ToString();
                    tnNum_install.Tag = 4;
                    tnNum_install.Name = "Status";
                    tnNum_install.ImageIndex = 4;
                    tnNum_install.SelectedImageIndex = 4;
                    systn.Nodes.Add(tnNum_install);

                    TreeNode tnNum_modulation = new TreeNode();
                    tnNum_modulation.Text = "调试完成数量:" +count_modulatioin.ToString() ;
                    tnNum_modulation.Tag = 5;
                    tnNum_modulation.Name = "Status";
                    tnNum_modulation.ImageIndex = 4;
                    tnNum_modulation.SelectedImageIndex = 4;
                    systn.Nodes.Add(tnNum_modulation);
                    //TreeNode tnNum_qc = new TreeNode();
                    //tnNum_qc.Text = "安装完成为:" + count_qc.ToString();
                    //tnNum_qc.Tag = 3;
                    //tnNum_qc.Name = "Status";
                    //tnNum_qc.ImageIndex = 4;
                    //tnNum_qc.SelectedImageIndex = 4;
                    //systn.Nodes.Add(tnNum_qc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show( ex.Message.ToString() + "请与管理员联系以确认该项目！");
                    return;
                }
            }
        }
    }
}