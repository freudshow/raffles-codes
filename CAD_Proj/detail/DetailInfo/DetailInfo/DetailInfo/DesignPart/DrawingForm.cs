using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OracleClient;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DetailInfo
{
    public partial class DrawingForm : Form
    {
        public DrawingForm()
        {
            InitializeComponent();
            this.toolStripStatusLabel2.Text = null;
            for (int i = 0; i < DetailMStrip.Items.Count; i++)
            {
                DetailMStrip.Items[i].Visible = false;
            }
        }

        private string projectid;

        public string Projectid
        {
            get { return projectid; }
            set { projectid = value; }
        }

        private string drawing;

        public string Drawing
        {
            get { return drawing; }
            set { drawing = value; }
        }

        private int indicator;

        public int Indicator
        {
            get { return indicator; }
            set { indicator = value; }
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            this.pidtb.Text = projectid;
            if (drawing != string.Empty)
            {
                string[] drawingno = drawing.Split(new char[] {','});

                for (int i = 0; i < drawingno.Length - 1; i++)
                {
                    this.DRAWINGNOcomboBox.Items.Add(drawingno[i].ToString());
                }
            }
            foreach (TabPage tp in this.tabControl1.TabPages)
            {
                if (tp.Text == "附件材料表" || tp.Text == "附件材料设备定额表" || tp.Text == "管系托盘表" || tp.Text == "管系材料表" || tp.Text == "管系材料设备定额表")
                {
                    this.tabControl1.TabPages.Remove(tp);
                }
            }


            if (indicator == 1)
            {
                foreach ( TabPage tp in  this.tabControl1.TabPages )
                {
                    if (tp.Text == "图纸封面")
                    {
                        this.tabControl1.TabPages.Remove(tp);
                    }
                }
            }
            SetStatus();
            this.DRAWINGNOcomboBox.SelectedIndex = 0;
        }

        private void SetStatus()
        {
            int count = 0;

            if (this.tabControl1.SelectedTab.Text == "小票信息")
            {
                count = this.Appdgv.Rows.Count;
            }
            else if (this.tabControl1.SelectedTab.Text == "材料信息")
            {
                count = this.Materialdgv.Rows.Count;
            }
            else if (this.tabControl1.SelectedTab.Text == "附件信息")
            {
                count = this.Partdgv.Rows.Count;
            }

            else if (this.tabControl1.SelectedTab.Text == "附件材料表")
            {
                count = this.dataGridView1.Rows.Count;
            }

            else if (this.tabControl1.SelectedTab.Text == "附件材料设备定额表")
            {
                count = this.dataGridView2.Rows.Count;
            }

            else if (this.tabControl1.SelectedTab.Text == "管系托盘表")
            {
                count = this.dataGridView3.Rows.Count;
            }

            else if (this.tabControl1.SelectedTab.Text == "管系材料表")
            {
                count = this.dataGridView4.Rows.Count;
            }

            this.toolStripStatusLabel1.Text = string.Format(" 当前总记录数：{0}个", count);
            
        }

        private void GetSelectionRowCount(DataGridView dgv)
        {
            int count = dgv.SelectedRows.Count;
            this.toolStripStatusLabel2.Text = string.Format("当前选中{0}行", count);
        }

        #region 选择行数显示变化
        private void Appdgv_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectionRowCount(this.Appdgv);
            int j = this.Appdgv.SelectedRows.Count;
            if (j > 0)
            {
                for (int i = 0; i < this.DetailMStrip.Items.Count; i++)
                {
                    DetailMStrip.Items[i].Visible = true;
                }
            }
            else
            {
                for (int i = 0; i < DetailMStrip.Items.Count; i++)
                {
                    DetailMStrip.Items[i].Visible = false;
                }
            }
        }

        private void Materialdgv_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectionRowCount(this.Materialdgv);
        }

        private void Partdgv_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectionRowCount(this.Partdgv);
        }

        #endregion

        private void Appdgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            SpoolCellFormat.FormatCell(Appdgv);
        }

        /// <summary>
        /// 生成图纸封面并插入到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void requestbtn_Click(object sender, EventArgs e)
        {
            string comText = string.Empty;
            string btntext = this.requestbtn.Text.ToString();
            string pid = this.pidtb.Text.ToString();
            object drawno = this.DRAWINGNOcomboBox.SelectedItem;
            if (drawno == null)
            {
                return;
            }
            else
            {
                switch (btntext)
                {
                    case "生成封面":
                        string sqlstr = "SELECT count(*) FROM SP_CREATEPDFDRAWING T WHERE T.PROJECTID = '"+pid+"' AND T.DRAWINGNO = '"+drawno+"'";
                        object count = User.GetScalar1(sqlstr,DataAccess.OIDSConnStr);
                        if ( Convert.ToInt16(count) == 0)
                        {
                            comText = "INSERT INTO SP_CREATEPDFDRAWING (PROJECTID, DRAWINGNO, FRONTPAGE) VALUES ('" + pid + "', '" + drawno + "', :dfd)";
                            InsertFrontPage.GenerateFrontPage(comText);

                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("该图纸封面已存在，确定要重新生成？", "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                            if (result == DialogResult.OK)
                            {
                                comText = "UPDATE SP_CREATEPDFDRAWING SET FRONTPAGE = :dfd WHERE PROJECTID = '" + pid + "' AND DRAWINGNO = '" + drawno + "'";
                                InsertFrontPage.GenerateFrontPage(comText);
                            }
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        private void DRAWINGNOcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RightClick.ProjectDrawingDetails_TabHop(indicator, this.pidtb, this.DRAWINGNOcomboBox, this.tabControl1, Appdgv, Materialdgv, Partdgv, axAcroPDF1, axAcroPDF2, dataGridView1, dataGridView2, dataGridView3, dataGridView4, dataGridView5, axAcroPDF3, Connectordgv);
            SetStatus();
        }

        private void DrawingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIForm.tool_strip.Items[1].Enabled = false;
            MDIForm.treeview.Refresh();
            try
            {
                string root = User.rootpath + "\\" + "temp";
                DirectoryInfo dir = new DirectoryInfo(root);
                if (dir.Exists)
                {
                    System.IO.Directory.Delete(root, true);
                }
                else
                {
                    return;
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RightClick.ProjectDrawingDetails_TabHop(indicator, this.pidtb, this.DRAWINGNOcomboBox, this.tabControl1, Appdgv, Materialdgv, Partdgv, axAcroPDF1, axAcroPDF2, dataGridView1, dataGridView2, dataGridView3, dataGridView4, dataGridView5, axAcroPDF3, Connectordgv);
            SetStatus();
            if (this.tabControl1.SelectedIndex >= 3)
            {
                this.toolStripStatusLabel2.Text = string.Empty;
            }
        }

        private void 管路材料信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.Appdgv.SelectedRows.Count;
            if (rowcount > 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < Appdgv.Rows.Count; i++)
                {
                    if (Appdgv.Rows[i].Selected == true)
                    {
                        string spoolname = Appdgv.Rows[i].Cells["SPOOLNAME"].Value.ToString();
                        sb.Append("'" + spoolname + "'" + ",");
                    }
                }
                string str = sb.Remove(sb.Length - 1, 1).ToString();
                DataSet ds = new DataSet();
                string sql = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP零件代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from SP_SPOOLMATERIAL_TAB t where t.materialname like '%管%' and t.flag = 'Y' and t.spoolname in (" + str + ")";
                foreach (Form form in MDIForm.pMainWin.MdiChildren)
                {
                    if (form.Text == "管路材料信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = null;
                        User.DataBaseConnect(sql, ds);
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                        int i = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                        ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前材料数量共：{0}个", i);
                        ds.Dispose();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "管路材料信息";
                sgvf.MdiParent = MDIForm.pMainWin;
                sgvf.Show();
                User.DataBaseConnect(sql, ds);
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                int count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前材料数量共：{0}个", count);
                ds.Dispose();
            }
            else
            {
                return;
            }
        }

        private void 管路附件信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = this.Appdgv.SelectedRows.Count;

            if (rowcount > 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < Appdgv.Rows.Count; i++)
                {
                    if (Appdgv.Rows[i].Selected == true)
                    {
                        string spoolname = Appdgv.Rows[i].Cells["SPOOLNAME"].Value.ToString();
                        sb.Append("'" + spoolname + "'" + ",");
                    }
                }
                string str = sb.Remove(sb.Length - 1, 1).ToString();
                DataSet ds = new DataSet();
                string sql = "select t.projectid 项目号, t.spoolname 小票号, t.erpcode ERP零件代码, t.materialname 材料型号, t.logname 录入人, t.logdate 录入日期 from SP_SPOOLMATERIAL_TAB t where t.materialname not like '%管%' and t.flag = 'Y' and t.spoolname in (" + str + ")";
                foreach (Form form in MDIForm.pMainWin.MdiChildren)
                {
                    if (form.Text == "管路附件信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = null;
                        User.DataBaseConnect(sql, ds);
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                        int i = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                        ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前附件数量共：{0}个", i);
                        ds.Dispose();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "管路附件信息";
                sgvf.MdiParent = MDIForm.pMainWin;
                sgvf.Show();
                User.DataBaseConnect(sql, ds);
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = ds.Tables[0].DefaultView;
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                int count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前材料数量共：{0}个", count);
                ds.Dispose();
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 直接打开管路小票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 查看小票toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string spoolstr = this.Appdgv.CurrentRow.Cells["SPOOLNAME"].Value.ToString();
            DataSet ds = new DataSet();
            string sqlstr = @"select t.pdfpath from sp_pdf_tab t where t.spoolname = '"+spoolstr+"' and t.flag = 'Y' ";
            User.DataBaseConnect(sqlstr, ds);
            if (ds.Tables[0].Rows.Count<1)
            {
                MessageBox.Show("目标存储地址没有此小票文件信息,请与管理员联系！", "WARNNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                ds.Dispose();
                return;
            }
            string pathstr = ds.Tables[0].Rows[0][0].ToString();
            System.Diagnostics.Process.Start(pathstr);
            ds.Dispose();

        }

        private void 导出Excel表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.ExportToTxt(this.Appdgv, progressBar1);
        }

        private void DrawingForm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
            MDIForm.tool_strip.Items[1].Enabled = true;
            MDIForm.tool_strip.Items[2].Enabled = true;
        }

        [System.Runtime.InteropServices.DllImport("ole32.dll")]
        static extern void CoFreeUnusedLibraries();

        private void DrawingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.axAcroPDF1 != null)
            {
                axAcroPDF1.Dispose();
                System.Windows.Forms.Application.DoEvents();
                CoFreeUnusedLibraries();
            }
            if (this.axAcroPDF2 != null)
            {
                axAcroPDF2.Dispose();
                System.Windows.Forms.Application.DoEvents();
                CoFreeUnusedLibraries();
            }
            if (this.axAcroPDF3 != null)
            {
                axAcroPDF3.Dispose();
                System.Windows.Forms.Application.DoEvents();
                CoFreeUnusedLibraries();
            }
        }

        private void Appdgv_MouseUp(object sender, MouseEventArgs e)
        {
            int selectcount = this.Appdgv.SelectedRows.Count;
            if (selectcount > 1)
            {
                this.DetailMStrip.Items["查看小票toolStripMenuItem"].Enabled = false;
            }
            else
            {
                this.DetailMStrip.Items["查看小票toolStripMenuItem"].Enabled = true;
            }
        }





    }
}