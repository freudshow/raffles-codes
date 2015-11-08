using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.IO;
using DetailInfo.Categery;
using System.Threading;
using System.Net;
using DetailInfo.Application_Code;
namespace DetailInfo.ECDMS
{
    public partial class ProjectDrawingCableFrm : Form
    {
        public ProjectDrawingCableFrm()
        {
            InitializeComponent();

            this.drawingrbn.Checked = true;
            this.modifyrbn.Enabled = false;
            this.querybtn.Enabled = false;
            this.toolStripStatusLabel2.Text = null;
            this.toolStripProgressBar1.Visible = false;
        }
        public static string drawingno="";
        public static string projectid = "";
        public static string drawingtitle = "";
        public static string drawingtitlecn="";
        public static string duty = "";
        private int flag = 0;

        private void GetFlag()
        {
            if (this.drawingrbn.Checked == true)
            {
                flag = 0;
            }
            else if (this.modifyrbn.Checked == true)
            {
                flag = 1;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DRAWINGNOcomboBox.Text = "";
            RESPONSIBLEcb.Text = "";
            RESPONSIBLEcb.Items.Clear();
            if (e.Node.Name == "PROJECTS")
            {
                return;
            }
            else
            {
                this.textBox1.Text = e.Node.Text.ToString();
                projectid = e.Node.Text.ToString();
            }

            DataBindFuntion();
        }

        private void DrawingsDgv_SelectionChanged(object sender, EventArgs e)
        {
            GetFlag();
            if (flag == 0)
            {

            }
            else if (flag == 1)
            {

            }
            int count = this.DrawingsDgv.SelectedRows.Count;
            this.toolStripStatusLabel2.Text = string.Format("当前选中:{0}行", count);
        }

        /// <summary>
        /// 精确查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void querybtn_Click(object sender, EventArgs e)
        {
            string sqlstr = string.Empty;
            string wheresqlstr = string.Empty;
            string pidstr_input = this.textBox1.Text.ToString();

            wheresqlstr += " and PLM.PROJECT_API.Get_PROJECT_NAME(project_id) = '" + pidstr_input + "'";


            string drawingno_input = DRAWINGNOcomboBox.Text.ToString();
            if (drawingno_input == string.Empty)
            {
                wheresqlstr += string.Empty;
            }
            else
            {
                wheresqlstr += " and DRAWING_NO  LIKE  '" + drawingno_input + "'";
            }
            string status_input = this.RESPONSIBLEcb.Text.ToString();
            if (status_input == string.Empty)
            {
                wheresqlstr += string.Empty;
            }
            else
            {

                wheresqlstr += " and PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) = '" + this.RESPONSIBLEcb.Text + "'";

            }
            wheresqlstr += "  ORDER BY DRAWING_ID DESC";
            DataSet ds = new DataSet();
            if (flag == 0)
            {
                sqlstr = "SELECT PLM.PROJECT_API.Get_PROJECT_NAME(project_id) projectname, DRAWING_NO, DRAWING_TITLE,DRAWING_TITLE_CN,Revision,  PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) ChineseName  FROM PLM.PROJECT_DRAWING_TAB t where drawing_type is null  AND DOCTYPE_ID IN (7) AND DOCTYPE_ID != 71 AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' and Discipline_Id=9 ";
            }
            else if (flag == 1)
            {
                sqlstr = "SELECT PLM.PROJECT_API.Get_PROJECT_NAME(project_id) projectname, DRAWING_NO, DRAWING_TITLE,DRAWING_TITLE_CN, Revision, PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) ChineseName  FROM PLM.PROJECT_DRAWING_TAB t where drawing_type is null  AND DRAWING_NO IN (SELECT DISTINCT S.MODIFYDRAWINGNO FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y' AND S.MODIFYDRAWINGNO IS NOT NULL) AND DOCTYPE_ID = 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' and Discipline_Id=9 ";
            }
            User.DataBaseConnect(sqlstr + wheresqlstr, ds);
            this.DrawingsDgv.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            SetStatus();
        }

        /// <summary>
        ///设置状态栏
        /// </summary>
        private void SetStatus()
        {
            int count = this.DrawingsDgv.Rows.Count;
            this.toolStripStatusLabel1.Text = string.Format(" 当前总记录数：{0}个", count);
        }

        private void PROJECTDRAWINGINFO_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }

        /// <summary>
        /// 窗体控件的数据绑定
        /// </summary>
        private void DataBindFuntion()
        {
            string drawingstr = string.Empty;
            string sqlstr = string.Empty;
            string responuser=string.Empty;
            this.DRAWINGNOcomboBox.Items.Clear();
            if (this.DRAWINGNOcomboBox.Text.Length != 0)
            {
                this.DRAWINGNOcomboBox.Text.Remove(0);
            }
            this.querybtn.Enabled = true;
            this.DRAWINGNOcomboBox.Items.Clear();

            if (this.drawingrbn.Checked == true)
            {
                sqlstr = "SELECT DRAWING_NO FROM PLM.PROJECT_DRAWING_TAB where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC";
            }
            else if (this.modifyrbn.Checked == true)
            {
                sqlstr = "SELECT DRAWING_NO FROM PLM.PROJECT_DRAWING_TAB where drawing_type is null AND Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DRAWING_NO IN (SELECT DISTINCT S.MODIFYDRAWINGNO FROM SP_SPOOL_TAB S WHERE S.FLAG = 'Y' AND S.MODIFYDRAWINGNO IS NOT NULL) AND DOCTYPE_ID = 71 AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC";
            }
            FillComboBox.GetFlowStatus(this.DRAWINGNOcomboBox, sqlstr);

            DataSet ds = new DataSet();
            drawingstr = "SELECT distinct PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) FROM PLM.PROJECT_DRAWING_TAB where Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N'";
            FillComboBox.GetFlowStatus(this.RESPONSIBLEcb, drawingstr);
            responuser = "select PLM.PROJECT_API.Get_PROJECT_NAME(project_id) projectname, DRAWING_NO, DRAWING_TITLE, DRAWING_TITLE_CN,Revision,  PLM.USER_API.CHINESENAME(RESPONSIBLE_USER) ChineseName  FROM PLM.PROJECT_DRAWING_TAB t where Project_Id = (select T.ID from PROJECT_TAB T where T.NAME='" + this.textBox1.Text.ToString() + "') AND DOCTYPE_ID IN (7)  AND DOCTYPE_ID != 71  AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' ORDER BY DRAWING_ID DESC";
            User.DataBaseConnect(responuser, ds);
            this.DrawingsDgv.DataSource = ds.Tables[0];
            ds.Dispose();
            SetStatus();

        }

 
        private void drawingrbn_CheckedChanged(object sender, EventArgs e)
        {
            this.DrawingsDgv.DataSource = null;
            this.DrawingsDgv.Columns.Clear();
            DrawingsTileBind();
            DataBindFuntion();
        }

        private void modifyrbn_CheckedChanged(object sender, EventArgs e)
        {
            this.DrawingsDgv.DataSource = null;
            this.DrawingsDgv.Columns.Clear();
            DrawingsTileBind();

            DataBindFuntion();
        }

        private void DrawingsTileBind()
        {
            this.DrawingsDgv.Columns.Add("PROJECTID", "项目号"); this.DrawingsDgv.Columns["PROJECTID"].DataPropertyName = "projectname";
            this.DrawingsDgv.Columns.Add("DRAWINGNO", "图纸号"); this.DrawingsDgv.Columns["DRAWINGNO"].DataPropertyName = "DRAWING_NO";
            this.DrawingsDgv.Columns.Add("DRAWING_TITLE", "图纸标题"); this.DrawingsDgv.Columns["DRAWING_TITLE"].DataPropertyName = "DRAWING_TITLE";
            this.DrawingsDgv.Columns.Add("REVISION", "版本号"); this.DrawingsDgv.Columns["REVISION"].DataPropertyName = "Revision";
            this.DrawingsDgv.Columns.Add("STATUS", "当前状态"); this.DrawingsDgv.Columns["STATUS"].DataPropertyName = "status";
            this.DrawingsDgv.Columns.Add("RESPONSIBLE_USER", "责任人"); this.DrawingsDgv.Columns["RESPONSIBLE_USER"].DataPropertyName = "ChineseName";
            this.DrawingsDgv.Columns.Add("RESPONSIBLEUSER", "责任人英文名"); this.DrawingsDgv.Columns["RESPONSIBLEUSER"].DataPropertyName = "RESPONSIBLE_USER";
            this.DrawingsDgv.Columns["RESPONSIBLEUSER"].Visible = false;
        }

        private void PROJECTDRAWINGINFO_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[1].Enabled = false;
        }

        private void ProjectDrawingCableFrm_Load_1(object sender, EventArgs e)
        {
            this.modifyrbn.Enabled = true;
            SetStatus();
            DataSet ds = new DataSet();
            //string sqlstr = "SELECT T.NAME FROM PROJECT_TAB T WHERE T.STATUS = 'N'/* AND T.PROJECT_TYPE LIKE '%项目%'*/ ORDER BY T.NAME";
            string sqlstr = " SELECT NAME FROM PLM.PROJECT_TAB WHERE STATUS='N' and ID NOT IN (76,81,82)   ORDER BY NAME";
            User.DataBaseConnect(sqlstr, ds);
            TreeNode tn;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i][0].ToString();
                treeView1.Nodes["PROJECTS"].Nodes.Add(tn);
            }
            this.treeView1.Nodes[0].Expand();
        }

        private void DrawingsDgv_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            drawingno = DrawingsDgv.SelectedRows[0].Cells[1].Value.ToString();
            drawingtitle = DrawingsDgv.SelectedRows[0].Cells[2].Value.ToString();
            drawingtitlecn = DrawingsDgv.SelectedRows[0].Cells[3].Value.ToString();
            duty = DrawingsDgv.SelectedRows[0].Cells[5].Value.ToString();
            CableListFrm clf = new CableListFrm();
            clf.MdiParent = MDIForm.ActiveForm;
            clf.Show();
        }

    }
}
