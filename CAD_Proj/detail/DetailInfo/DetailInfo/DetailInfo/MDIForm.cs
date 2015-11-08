using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Timers;
using System.Data.OracleClient;
using DetailInfo.Categery;
using System.Threading;
using System.Diagnostics;


namespace DetailInfo
{
    public delegate void formrefresh();
    /// <summary>
    /// 
    /// </summary>
    public partial class MDIForm : Form
    {
        TaskbarNotifier taskbarNotifier;
        public static MDIForm pMainWin = null;
        public static ToolStrip  tool_strip= null;
        public static ToolStripButton stribtn = null;
        public static TreeView treeview = null;

        //bool rightClickNode = false;
        DateTime startTime;
        TreeNode dragDropTreeNode;
        public MDIForm()
        {
            InitializeComponent();
            pMainWin  =  this;//这里是初始化
            tool_strip = this.toolStrip1;
            treeview = this.treeView1;
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\office2007.ssk";
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//以下三行消除主界面闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
            {
                contextMenuStrip1.Items[i].Visible = false;
            }
        }
        
        /// <summary>
        /// 设置弹出消息窗口背景以及事件
        /// </summary>
        private void SetTaskBar()
        {
            taskbarNotifier = new TaskbarNotifier();
            taskbarNotifier.SetBackgroundBitmap(new Bitmap(User.rootpath + "\\PopupWindow\\skin.bmp"), Color.FromArgb(255, 0, 255));
            taskbarNotifier.SetCloseBitmap(new Bitmap(User.rootpath + "\\PopupWindow\\close.bmp"), Color.FromArgb(255, 0, 255), new Point(187, 8));
            taskbarNotifier.TitleRectangle = new Rectangle(40, 9, 80, 55);
            taskbarNotifier.ContentRectangle = new Rectangle(8, 41, 183, 68);
            taskbarNotifier.TitleClick += new EventHandler(TitleClick);
            taskbarNotifier.CloseClick += new EventHandler(CloseClick);
        }

        private void MDIForm_Load(object sender, EventArgs e)
        {
            System.DateTime dt = System.DateTime.Now;
            SearchtoolStripButton3.Enabled = false;
            ExcelStripButton.Enabled = false;
            SetStatus();

            #region 导航树显示
            string sql;
            if (User.cur_user == "plm")
                sql = "select * from treenodes_tab t order by t.parent_index";
            else
            {
                string privilegelist = User.FindPrivilegeidList(User.cur_user);
                if (privilegelist == string.Empty)
                {
                    sql = "select * from treenodes_tab t where t.flag='N' order by t.parent_index";
                    MessageBox.Show("您没有权限请与管理员联系");
                }
                else
                    sql = "select * from treenodes_tab t where t.flag='N' or (t.flag='Y' and t.id in (select p.node_id from privilege_node_tab p where p.privilege_id in (" + privilegelist + ")))  order by t.parent_index";
            }
            CreateTreeView(sql, treeView1.Nodes);
            if (treeView1.Nodes.Count != 0)
            {
                treeView1.Nodes[0].Expand();
            }
            //treeView1.ExpandAll();
            #endregion 

        }
        //WINFORM方法:
        /**/
        /// <summary>
        /// 动态创建TreeView
        /// </summary>
        /// <param name="sqlText">传入的SQL语句</param>
        /// <param name="nodes">TreeView节点集</param>
        public void CreateTreeView(string sqlText, System.Windows.Forms.TreeNodeCollection nodes)
        {
                DataTable dbTable = new DataTable();//实例化一个DataTable数据表对象
                try
                {
                    OracleConnection con = new OracleConnection(DataAccess.OIDSConnStr);
                    con.Open();
                    OracleDataAdapter oda = new OracleDataAdapter(sqlText, con);
                    OracleCommandBuilder builder = new OracleCommandBuilder(oda);
                    DataSet ds = new DataSet();
                    oda.Fill(ds);
                    dbTable = ds.Tables[0];
                    con.Close();
                    ds.Dispose();
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
                //将第一级菜单取出生成TreeView的节点,作为递归运算的入口递归查询出TreeView的所有节点数据
                CreateTreeViewRecursive(nodes, dbTable, 0);
        }
        /**/
        /// <summary>
        /// 递归查询
        /// </summary>
        /// <param name="nodes">TreeView节点集合</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="parentid">上一级菜单节点标识码</param>
        public void CreateTreeViewRecursive(System.Windows.Forms.TreeNodeCollection nodes, DataTable dataSource, int parentid)
        {
            string filter;//定义一个过滤器
            filter = string.Format("Parent_Id={0}", parentid);
            DataRow[] drarr = dataSource.Select(filter);//将过滤的ID放入数组中
            TreeNode node;
            foreach (DataRow dr in drarr)//递归循环查询出数据
            {
                node = new TreeNode();
                node.Text = dr["text"].ToString();
                node.Name = dr["name"].ToString();
                node.ImageIndex = Convert.ToInt32(dr["ImageIndex"]);
                node.SelectedImageIndex = Convert.ToInt32(dr["SelectedImageIndex"]);
                node.Tag = Convert.ToInt32(dr["Id"]);
                nodes.Add(node);//加入节点
                CreateTreeViewRecursive(node.Nodes, dataSource, Convert.ToInt32(node.Tag));
            }
        }

        /// <summary>
        /// 关闭主窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result_close;
            result_close = MessageBox.Show("确定要退出系统？", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result_close == DialogResult.OK)
            {
                if (this.MdiChildren.Length != 0)
                {
                    foreach (Form form in this.MdiChildren)
                    {
                        form.Close();
                        this.SearchtoolStripButton3.Enabled = false;
                    }
                }
                //string sqlStr = "update user_tab set loginstate = 'N' where name = '" + User.cur_user + "'";
                //User.UpdateCon(sqlStr, DataAccess.OIDSConnStr);
                //this.Dispose();
                
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MDIForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("确定要退出系统？", "退出系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (this.MdiChildren.Length != 0)
                {
                    foreach (Form form in this.MdiChildren)
                    {
                        form.Close();
                        this.SearchtoolStripButton3.Enabled = false;
                    }
                }

                //string sqlStr = "update user_tab set loginstate = 'N' where name = '"+User.cur_user+"'";
                //User.UpdateCon(sqlStr, DataAccess.OIDSConnStr);
                this.Dispose();
            }
            if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        /// <summary>
        /// 主窗口LISTVIEW双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DoubleClick_1(object sender, EventArgs e)
        {
            #region 管路数据查询操作
            //打开设计小票
            if (treeView1.SelectedNode.Name == "DesignSpoolOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "设计小票概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }

                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "设计小票概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }

            else if (treeView1.SelectedNode.Name == "ECDRAWINGREPORT")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "项目图纸报表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                ECDrawingReportFrm ecreport = new ECDrawingReportFrm();
                ecreport.Text = "项目图纸报表";
                ecreport.MdiParent = this;
                ecreport.Show();
            }

            else if (treeView1.SelectedNode.Name == "FrontPageUpdate")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "更新封面")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                InsertBlob insertFrom = new InsertBlob();
                insertFrom.MdiParent = this;
                insertFrom.Show();
            }

            if (treeView1.SelectedNode.Name == "ValveOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "阀门信息概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }

                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "阀门信息概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }


            //打开加工小票
            else if (treeView1.SelectedNode.Name == "MachineSpoolOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "加工小票概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }

                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "加工小票概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }




            //打开工时统计小票
            else if (treeView1.SelectedNode.Name == "NormHour")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "管加工工时管理")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "管加工工时管理";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }




            //打开质检小票
            else if (treeView1.SelectedNode.Name == "QCSpoolOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "质检小票概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "质检小票概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }


            //打开已经通过质检的小票
            else if (treeView1.SelectedNode.Name == "QCCheckedOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "检验通过小票概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "检验通过小票概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;

            }

            //材料信息概览
            else if (treeView1.SelectedNode.Name == "MaterialOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "材料信息概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "材料信息概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }
            
            //连接件信息概览
            else if (treeView1.SelectedNode.Name == "ConnectorOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "连接件信息概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "连接件信息概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }
            
            //加工信息概览
            else if (treeView1.SelectedNode.Name == "MachineOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "加工信息概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "加工信息概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }

           //焊接信息概览
            else if (treeView1.SelectedNode.Name == "WeldOverView")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "焊接信息概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "焊接信息概览";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }
            

            ///管路统计
            else if (treeView1.SelectedNode.Name == "PipeStatistics")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "管路统计信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "管路统计信息";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;

            }

            ///管路附件统计
            else if (treeView1.SelectedNode.Name == "PartStatistics")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "管路附件统计信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "管路附件统计信息";
                sgvf.MdiParent=this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }
            
            
             //项目进度统计
            else if (treeView1.SelectedNode.Name == "ProgressInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "项目管路进度分析")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                ProgressStatistics psform = new ProgressStatistics();
                psform.Text = "项目管路进度分析";
                psform.MdiParent = this;
                psform.Show();
                this.toolStrip1.Items[0].Enabled = false;
            }

            //车间物料需求表
            if (treeView1.SelectedNode.Name == "MaterialRequirement")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "物料需求概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                BatchSheet bsform = new BatchSheet();
                bsform.MdiParent = this;
                bsform.Show();
                SearchtoolStripButton3.Enabled = false;
            }

            if (treeView1.SelectedNode.Name == "AddNewWorker")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "添加车间人员基础信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WorkShopWorkerData wwdform = new WorkShopWorkerData();
                wwdform.MdiParent = this;
                wwdform.Show();
                SearchtoolStripButton3.Enabled = false;
            }

            //项目图纸概览
            else if (treeView1.SelectedNode.Name == "PROJECTDRAWINGOVERVIEW")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "项目图纸号概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                PROJECTDRAWINGINFO pdform = new PROJECTDRAWINGINFO();
                pdform.Text = "项目图纸号概览";
                pdform.MdiParent = this;
                pdform.Show();
                this.toolStrip1.Items[0].Enabled = false;
            }

            //已发图纸材料定额表
            else if (treeView1.SelectedNode.Name == "MATERIALRATION")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "已发图纸材料定额表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolGeneralViewForm sgvf = new SpoolGeneralViewForm();
                sgvf.Text = "已发图纸材料定额表";
                sgvf.MdiParent = this;
                sgvf.Show();
                SearchtoolStripButton3.Enabled = true;
            }

#endregion 

            #region 后台数据维护窗口
            else if (treeView1.SelectedNode.Name == "Bend")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "弯头列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "弯头列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "Cabin")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "舱室列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "舱室列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "Connector")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "连接件列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "连接件列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "ElbowMaterial")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "弯头材料对照列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "弯头材料对照列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "PSTAD")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "弯模列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "弯模列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "SocketElbow")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "承接弯头列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "承接弯头列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "Surface")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "表面处理列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "表面处理列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "System")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "系统列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "系统列表";
                dmf.MdiParent = this;
                dmf.Show();
            }

            else if (treeView1.SelectedNode.Name == "Approver")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "审核人列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "审核人列表";
                dmf.MdiParent = this;
                dmf.Show();
            }
           
            else if (treeView1.SelectedNode.Name == "BAITINGNORM_METALPIPE")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "金属管线下料时间定额列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "金属管线下料时间定额列表";
                dmf.MdiParent = this;
                dmf.Show();
            }
            else if (treeView1.SelectedNode.Name == "BAITINGNORM_PEPPH")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "PE&PPH管工时定额列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "PE&PPH管工时定额列表";
                dmf.MdiParent = this;
                dmf.Show();
            }
            else if (treeView1.SelectedNode.Name == "BEVEL_HOUR_NORM")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "坡口加工时间定额列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "坡口加工时间定额列表";
                dmf.MdiParent = this;
                dmf.Show();
            }
            else if (treeView1.SelectedNode.Name == "ELBOW_HOUR_NORM")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "弯管时间定额列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "弯管时间定额列表";
                dmf.MdiParent = this;
                dmf.Show();
            }
            else if (treeView1.SelectedNode.Name == "PIPECHECKING_HOUR_NORM")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "校管时间定额列表")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DataMaintenanceForm dmf = new DataMaintenanceForm();
                dmf.Text = "校管时间定额列表";
                dmf.MdiParent = this;
                dmf.Show();
            }



             #endregion

            #region 车间计划
            else if (treeView1.SelectedNode.Name == "WorkShopPlan")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "车间计划")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WorkShopPlan wspfrm = new WorkShopPlan();
                wspfrm.Text = "车间计划";
                wspfrm.MdiParent = this;
                wspfrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "WorkShopPlanDetails")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "车间计划明细")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WorkShopPlanDetail wspdfrm = new WorkShopPlanDetail();
                wspdfrm.Text = "车间计划明细";
                wspdfrm.MdiParent = this;
                wspdfrm.Show();
            }
            #endregion
            else if (treeView1.SelectedNode.Name == "MaterialManagement")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "物料信息管理")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                inventoryFrm inventoryform = new inventoryFrm();
                inventoryform.Text = "物料信息管理";
                inventoryform.MdiParent = this;
                inventoryform.Show();
            }
            #region 车间管路追踪
            else if (treeView1.SelectedNode.Name == "SpoolTrace")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "管路追踪")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WShopSPlTrace tracefrm = new WShopSPlTrace();
                tracefrm.Text = "管路追踪";
                tracefrm.MdiParent = this;
                tracefrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialTreaceability")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "报验单信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MaterialTraceabilityFrm traceabilityFrm = new MaterialTraceabilityFrm();
                traceabilityFrm.Text = "报验单信息";
                traceabilityFrm.MdiParent = this;
                traceabilityFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "ModifyStatistics")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "产前产后修改信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                ModifyStatisticsFrm msFrm = new ModifyStatisticsFrm();
                msFrm.Text = "产前产后修改信息";
                msFrm.MdiParent = this;
                msFrm.Show();
            }
                
            
            #endregion

            #region 车间工时定额窗体
            else if (treeView1.SelectedNode.Name == "MaterialPreparation")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "下料工时定额")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WShopManHour manhourfrm = new WShopManHour();
                manhourfrm.Text = "下料工时定额";
                manhourfrm.MdiParent = this;
                manhourfrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialAssembly")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "装配工时定额")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WShopManHour manhourfrm = new WShopManHour();
                manhourfrm.Text = "装配工时定额";
                manhourfrm.MdiParent = this;
                manhourfrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialWeld")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "焊接工时定额")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WShopManHour manhourfrm = new WShopManHour();
                manhourfrm.Text = "焊接工时定额";
                manhourfrm.MdiParent = this;
                manhourfrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialQC")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "报验工时定额")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WShopManHour manhourfrm = new WShopManHour();
                manhourfrm.Text = "报验工时定额";
                manhourfrm.MdiParent = this;
                manhourfrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialTransport")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "料场工时定额")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WShopManHour manhourfrm = new WShopManHour();
                manhourfrm.Text = "料场工时定额";
                manhourfrm.MdiParent = this;
                manhourfrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialPressTest")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "压力试验工时定额")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                WShopManHour manhourfrm = new WShopManHour();
                manhourfrm.Text = "压力试验工时定额";
                manhourfrm.MdiParent = this;
                manhourfrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "NormalPipeWT")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "普通碳钢管(铜管)及普通不锈钢管总工时概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                NormalPipeWorkingTime normalWTfrm = new NormalPipeWorkingTime();
                normalWTfrm.Text = "普通碳钢管(铜管)及普通不锈钢管总工时概览";
                normalWTfrm.MdiParent = this;
                normalWTfrm.Show();
            }
            #endregion
            #region ECDMS用户导入信息
            else if (treeView1.SelectedNode.Name == "ImportComments")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "报验意见信息导入程序")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DetailInfo.ECDMS.ImportComment commentform = new ECDMS.ImportComment();
                commentform.Text = "报验意见信息导入程序";
                commentform.MdiParent = this;
                commentform.Show();
            }
            else if (treeView1.SelectedNode.Name == "ImportDrawingPlan")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "图纸与图纸计划导入")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                ImportDrawingPlanFrm importDrawPlanfrom = new ImportDrawingPlanFrm();
                importDrawPlanfrom.Text = "图纸与图纸计划导入";
                importDrawPlanfrom.MdiParent = this;
                importDrawPlanfrom.Show();
            }

            else if (treeView1.SelectedNode.Name == "ImportEquipment")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "设备导入")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                ImportEquipmentFrm importEquipmentform = new ImportEquipmentFrm();
                importEquipmentform.Text = "设备导入";
                importEquipmentform.MdiParent = this;
                importEquipmentform.Show();
            }

            else if (treeView1.SelectedNode.Name == "CablesVolume")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "电缆册")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                CablesVolumeFrm cablesform = new CablesVolumeFrm();
                cablesform.Text = "电缆册";
                cablesform.MdiParent = this;
                cablesform.Show();
            }
            else if (treeView1.SelectedNode.Name == "ImportCable")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "电缆导入")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                DetailInfo.ECDMS.ProjectDrawingCableFrm pdcable = new ECDMS.ProjectDrawingCableFrm();
                pdcable.Text = "电缆导入";
                pdcable.MdiParent = this;
                pdcable.Show();
            }

            #endregion
            #region ECDMS用户材料管理信息

            else if (treeView1.SelectedNode.Name == "MatParaSet")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "材料预估")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                M_Estimate matreqadd = new M_Estimate();
                matreqadd.MdiParent = this;
                matreqadd.Text = "材料预估";
                matreqadd.Show();

            }
            else if (treeView1.SelectedNode.Name == "MatParaSummary")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "材料预估概览")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                frmPartParaSet matreqadd = new frmPartParaSet();
                matreqadd.MdiParent = this;
                matreqadd.Text = "材料预估概览";
                matreqadd.Show();

            }
            else if (treeView1.SelectedNode.Name == "MatParaConfirm")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "材料预估确认")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                frmPartParaConfirm matreqadd = new frmPartParaConfirm();
                matreqadd.MdiParent = this;
                matreqadd.Text = "材料预估确认";
                matreqadd.Show();

            }

            else if (treeView1.SelectedNode.Name == "MatRequire")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "材料申请页面")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MaterialManage.MaterialRequireAdd matreqadd = new MaterialManage.MaterialRequireAdd("0","1");
                matreqadd.MdiParent = this;
                matreqadd.Text = "材料申请页面";
                matreqadd.Show();

            }
            else if (treeView1.SelectedNode.Name == "ERPCode_Manage")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "编码管理")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                frmPartAdd matadd = new frmPartAdd();
                matadd.MdiParent = this;
                matadd.Text = "编码管理";
                matadd.Show();

            }
            else if (treeView1.SelectedNode.Name == "ConvertMSS")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "转换标准格式MSS")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                ConvertStandard matadd = new ConvertStandard();
                matadd.MdiParent = this;
                matadd.Text = "转换标准格式MSS";
                matadd.Show();

            }
            else if (treeView1.SelectedNode.Name == "ERP_Inventory")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "项目库存查询")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                frmPartInventory matadd = new frmPartInventory();
                matadd.MdiParent = this;
                matadd.Text = "项目库存查询";
                matadd.Show();

            }

            else if (treeView1.SelectedNode.Name == "MatReqSummary")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "MEO查询及导出")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MaterialManage.MaterialRequireQuery matadd = new MaterialManage.MaterialRequireQuery();
                matadd.MdiParent = this;
                matadd.Text = "MEO查询及导出";
                matadd.Show();

            }
            else if (treeView1.SelectedNode.Name == "MatRation")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "材料下发页面")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MaterialManage.MaterialRation matadd = new MaterialManage.MaterialRation("0","1");
                matadd.MdiParent = this;
                matadd.Text = "材料下发页面";
                matadd.Show();
            }

            #endregion
            #region 管加工流程细分
            else if (treeView1.SelectedNode.Name == "MaterialBlankingInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "下料信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "下料信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialAssembleInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "装配信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "装配信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialWeldInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "焊接信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "焊接信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialQCInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "报验信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "报验信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialTreatmentInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "处理信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "处理信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialRecieveInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "料场接收信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "料场接收信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialDeliveryInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "发放信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "发放信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialSetInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "安装信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "安装信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialTrayClass")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "托盘及分类信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MachinningInfoFrm machineInfoFrm = new MachinningInfoFrm();
                machineInfoFrm.Text = "托盘及分类信息";
                machineInfoFrm.MdiParent = this;
                machineInfoFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "SpoolReportCollection")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "管路报表汇总")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                SpoolReportFrm spoolreportFrm = new SpoolReportFrm();
                spoolreportFrm.Text = "管路报表汇总";
                spoolreportFrm.MdiParent = this;
                spoolreportFrm.Show();
            }

            else if (treeView1.SelectedNode.Name == "MaterialReports")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "材料报表汇总")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                MaterialReportsFrm materialreportFrm = new MaterialReportsFrm();
                materialreportFrm.Text = "材料报表汇总";
                materialreportFrm.MdiParent = this;
                materialreportFrm.Show();
            }
            else if (treeView1.SelectedNode.Name == "MDrawingInfo")
            {
                foreach (Form form in this.MdiChildren)
                {
                    if (form.Text == "修改删除信息")
                    {
                        if (form.WindowState == FormWindowState.Minimized)
                        {
                            form.WindowState = FormWindowState.Normal;
                        }
                        form.Activate();
                        return;
                    }
                }
                ModifyDelInfoFrm mdelfrm = new ModifyDelInfoFrm();
                mdelfrm.Text = "修改删除信息";
                mdelfrm.MdiParent = this;
                mdelfrm.Show();
            }
            #endregion



        }

        /// <summary>
        /// 最小化所有窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 所有最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList minarray = new ArrayList();
            foreach (Form form in this.MdiChildren)
            {
                minarray.Add(form);
            }
            if (minarray.Count == 0)
            {
                return;
            }
            if (minarray.Count != 0)
            {
                for (int i = 0; i < minarray.Count; i++)
                {
                    (minarray[i] as Form).WindowState = FormWindowState.Minimized;
                }
            }

        }

        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关闭所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length != 0)
            {
                foreach (Form form in this.MdiChildren)
                {
                    form.Close();
                    this.SearchtoolStripButton3.Enabled = false;
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 审核消息弹出窗口设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void ApproveTimer_Tick(object sender, EventArgs e)
        //{
            //ApproveTimer.Start();
            //string sqlstr = "SELECT COUNT(*) FROM SP_SPOOL_TAB T WHERE T.DRAWINGNO IN (SELECT S.DRAWING_NO FROM PLM.PROJECT_DRAWING_TAB S WHERE S.DRAWING_TYPE IS NULL AND DOCTYPE_ID IN (7) AND LASTFLAG = 'Y' AND NEW_FLAG = 'Y' AND DELETE_FLAG = 'N' AND S.RESPONSIBLE_USER = '"+User.cur_user+"') AND T.FLOWSTATUS = 6 AND T.FLAG = 'Y' AND T.LOGNAME='"+User.cur_user+"'";
            //object obj = User.GetScalar(sqlstr, DataAccess.OIDSConnStr);
            ////MessageBox.Show(obj.ToString());

            //if (obj == null || int.Parse(obj.ToString()) == 0)
            //{
            //    return;
            //}

            //if (obj != 0)
            //{
                //SetTaskBar();
                //taskbarNotifier.Show("任务提醒", "您有从加工反馈的问题小票需要处理", 500, 5000, 500);
            //}














            //object obj = null;
            //string sql = string.Empty;

            //if (UserSecurity.HavingPrivilege(User.cur_user, "SPL_APPROVE_DES"))
            //{
            //    //sql = "SELECT  * FROM SPOOL_APPROVE_TAB WHERE  ASSESOR = '" + User.cur_user + "' AND STATE = 0 AND APPROVENEEDFLAG = 'Y'";
            //    sql = "SELECT  * FROM PIPEAPPROVE_TAB WHERE  ASSESOR = '" + User.cur_user + "' AND STATE = 0 AND APPROVENEEDFLAG = 'Y'";
            //    obj = User.GetScalar(sql, DataAccess.OIDSConnStr);
            //    if (obj != null)
            //    {
            //        SetTaskBar();
            //        taskbarNotifier.ContentClick += new EventHandler(ContentClick);
            //        taskbarNotifier.Show("任务提醒", "您有待审小票", 500, 5000, 500);
            //    }
            //}

            //else if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLCONSTRUCTIONDESIGNUSERS"))
            //{
            //    //string sql1 = "SELECT * FROM SPFLOWLOG_TAB WHERE FLOWSTATUS =3 AND USERNAME = '" + User.cur_user + "' AND SPOOLNAME IN (SELECT SPOOLNAME FROM SPFLOWLOG_TAB WHERE FLOWSTATUS = 6) ";
            //    sql = "select * from SP_SPOOL_TAB where spoolname in (SELECT s.SPOOLNAME as 小票名 from spflowlog_tab S where s.spoolname in (select DISTINCT t.spoolname  from spflowlog_tab t where t.username = '" + User.cur_user + "' and t.flowstatus = " + (int)FlowState.待分配 + ") and s.flowstatus = " + (int)FlowState.反馈设计 + ")  and flag = 'Y' AND flowstatus = 6";
            //    obj = User.GetScalar(sql, DataAccess.OIDSConnStr);
            //    if (obj != null)
            //    {
            //        SetTaskBar();
            //        taskbarNotifier.ContentClick += new EventHandler(ContentClick);
            //        taskbarNotifier.Show("任务提醒", "您有从加工反馈的问题小票需要处理", 500, 5000, 500);
            //    }

            //}

            //if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLDESIGNUSERS"))
            //{
            //    //string sql2 = "SELECT * FROM SP_SPOOL_TAB t WHERE t.FLAG = 'Y' AND t.FLOWSTATUS = 5  AND t.SPOOLNAME  in   (SELECT s.SPOOLNAME FROM SPFLOWLOG_TAB s WHERE s.FLOWSTATUS =1 AND s.USERNAME = '" + User.cur_user + "' AND s.SPOOLNAME IN  (SELECT z.SPOOLNAME FROM SPFLOWLOG_TAB z WHERE z.FLOWSTATUS = 5 ) ) ";
            //    //sql = "select * from SP_SPOOL_TAB where SPOOLNAME in ( SELECT  s.SPOOLNAME  from spflowlog_tab S where s.spoolname in (select DISTINCT t.spoolname  from spflowlog_tab t where t.username = '" + User.cur_user + "' and t.flowstatus = " + (int)FlowState.待审 + ") and s.flowstatus = " + (int)FlowState.审核反馈 + ") and flag = 'Y' and flowstatus = 5";
            //    sql = "SELECT T.* FROM SP_SPOOL_TAB T WHERE T.FLOWSTATUS = 5 AND T.FLAG = 'Y' AND T.DRAWINGNO IN (SELECT S.DRAWINGNO from spflowlog_tab S where S.DRAWINGNO in (select DISTINCT A.DRAWINGNO from spflowlog_tab A where A.username = 'yang.yang' and A.flowstatus = 1) and S.flowstatus = 5) AND T.Projectid IN (SELECT S.Projectid from spflowlog_tab S where S.Projectid in (select DISTINCT A.Projectid from spflowlog_tab A where A.username = '"+User.cur_user+"' and A.flowstatus = 1) and S.flowstatus = 5)";
            //    obj = User.GetScalar(sql, DataAccess.OIDSConnStr);
            //    if (obj != null)
            //    {
            //        SetTaskBar();
            //        taskbarNotifier.ContentClick += new EventHandler(ContentClick);
            //        taskbarNotifier.Show("任务提醒", "您有审核未通过的小票需要处理", 500, 5000, 500);
            //    }

            //}




        //}


        void PopUpInformation()
        {
            SetTaskBar();
            taskbarNotifier.ContentClick += new EventHandler(ContentClick);
            taskbarNotifier.Show("任务提醒", "您有从加工反馈的问题小票需要处理", 500, 5000, 500);
        }

        void TitleClick(object obj, EventArgs ea)
        {
        //    MessageBox.Show("Title was Clicked");
        }
        void CloseClick(object obj, EventArgs ea)
        {
            //
        }

        void ContentClick(object obj, EventArgs ea)
        {
            //this.Activate(); this.WindowState = FormWindowState.Maximized;
            //if (UserSecurity.HavingPrivilege(User.cur_user, "SPL_APPROVE_DES"))
            //{
            //    foreach (Form form in this.MdiChildren)
            //    {
            //        if (form.Text == "审核窗口")
            //        {
            //            if (form.WindowState == FormWindowState.Minimized)
            //            {
            //                form.WindowState = FormWindowState.Normal;
            //            }
            //            form.Activate();
            //            return;
            //        }
            //    }
            //    ApproveForm af = new ApproveForm();
            //    af.Text = "审核窗口";
            //    af.MdiParent = this;
            //    af.Show();
            //}


            //else if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLCONSTRUCTIONDESIGNUSERS"))
            //{
            //    foreach (Form form in this.MdiChildren)
            //    {
            //        if (form.Name == "MachiningFeedback")
            //        {
            //            if (form.WindowState == FormWindowState.Minimized)
            //            {
            //                form.WindowState = FormWindowState.Normal;

            //            }
            //            form.Activate();
            //            return;
            //        }
            //    }
            //    MachiningFeedback mf = new MachiningFeedback();
            //    mf.Text = "管加工反馈问题小票";
            //    mf.MdiParent = this;
            //    mf.Show();
            //}

            //else if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLDESIGNUSERS"))
            //{
            //    foreach (Form form in this.MdiChildren)
            //    {
            //        if (form.Text == "审核反馈小票")
            //        {
            //            if (form.WindowState == FormWindowState.Minimized)
            //            {
            //                form.WindowState = FormWindowState.Normal;

            //            }
            //            form.Activate();
            //            return;
            //        }
            //    }
            //    ApproveForm af = new ApproveForm();
            //    af.Text = "审核反馈小票";
            //    af.MdiParent = this;
            //    af.Show();

            //}

        }
        #region 打开word\excel\notepad文档
        private void 启用记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }
        
        private void 启用wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("winword.exe");
        }

        private void 启用excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.exe");
        }
        #endregion
        private void 重新登录toolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("确定要重新登录吗?", "重新登录", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Process process = new Process();
                //process.StartInfo.FileName = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "DetailInfo.exe";
                //process.Start();
                if (this.MdiChildren.Length != 0)
                {
                    foreach (Form form in this.MdiChildren)
                    {
                        form.Close();
                        //this.SearchtoolStripButton3.Enabled = false;
                    }
                }
                this.Dispose();
                Process process = new Process();
                process.StartInfo.FileName = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "DetailInfo.exe";
                process.Start();
                //bool flag = false;

                /////实例化一个同步基元
                //Mutex mutex = new Mutex(true, "QueryMachine.BackManager.LoginForm", out flag);
                //if (flag)
                //{
                //    Application.Restart();

                //}
                //else
                //{
                //    Application.Exit();
                //}

            }
            else
            {
                return;
            }

        }

        /// <summary>
        /// 打开查询窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchtoolStripButton3_Click(object sender, EventArgs e)
        {
            string tableName = "";
            string name = string.Empty;
            try
            {
                name = this.ActiveMdiChild.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            switch (name)
            {
                case "设计小票概览": 
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "加工小票概览":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "质检小票概览":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "安装小票概览":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "调试小票概览":
                    tableName = "SP_SPOOL_TAB";
                    break;

                case "管路统计信息":
                    tableName = "SPL_VIEW";
                    break;
                case "管路附件统计信息":
                    tableName = "MATERIALATTACHMENT_VIEW";
                    break;

                case "材料信息概览":
                    tableName = "SP_SPOOLMATERIAL_VIEW";
                    break;
                case "连接件信息概览":
                    tableName = "SP_CONNECTOR_VIEW";
                    break;
                case "加工信息概览":
                    tableName = "SP_MACHININGINFO_TAB";
                    break;
                case "焊接信息概览":
                    tableName = "SP_SPOOLWELD_VIEW";
                    break;
                case "阀门信息概览":
                    tableName = "SP_VALVE_VIEW";
                    break;

                case "下料工时定额":
                    tableName = "SP_MATERIALPREPARETIME_VIEW";
                    break;
                case "装配工时定额":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "焊接工时定额":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "报验工时定额":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "料场工时定额":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "压力试验工时定额":
                    tableName = "SP_SPOOL_TAB";
                    break;
                case "普通碳钢管(铜管)及普通不锈钢管总工时概览":
                    tableName = "SP_NORMALPIPEWORKINGHOUR_VIEW";
                    break;
                case "下料信息":
                    tableName = "SP_WORKSHOPBLANKING_VIEW";
                    break;
                case "装配信息":
                    tableName = "SP_WORKSHOPASSEMBLY_VIEW";
                    break;
                case "焊接信息":
                    tableName = "SP_WORKSHOPWELD_VIEW";
                    break;
                case "报验信息":
                    tableName = "SP_WORKSHOTOQC_VIEW";
                    break;
                case "处理信息":
                    tableName = "SP_WORKSHOPTOTREATMENT_VIEW";
                    break;
                case "料场接收信息":
                    tableName = "SP_WORKSHOPRECIEVE_VIEW";
                    break;
                case "发放信息":
                    tableName = "SP_WORKSHOPDELIVERY_VIEW";
                    break;
                case "安装信息":
                    tableName = "SP_WORKSHOPINSTALL_VIEW";
                    break;
                case "托盘及分类信息":
                    tableName = "SP_WORKSHOPTRAYNOCLASS_VIEW";
                    break;
                case "已发图纸材料定额表":
                    tableName = "SP_MATERIALEQUIPRATION_TAB";
                    break;
                default:
                    this.statusStrip1.Items[0].Enabled = false;
                    break;
            }
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "SearchForm")
                {
                    form.Activate();
                    return;
                }
            }
            SearchForm sf = new SearchForm();
            sf.Table_name = tableName;
            sf.ShowDialog();
        }

        /// <summary>
        /// 隐藏form图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0)
            {
                e.Item.Visible = false;
            }
        }
        #region 多窗体分布控制
        private void 叠层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void 横向下铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 竖向平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }
        #endregion

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    if (User.cur_user == "plm")
                    {
                        //显示右键菜单
                        for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                        {
                            contextMenuStrip1.Items[i].Visible = true;
                        }
                    }
                    treeView1.SelectedNode = CurrentNode;//选中这个节点
                }
                else
                    for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                    {
                        contextMenuStrip1.Items[i].Visible = false;
                    }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Text;
            TreeNode selectednode = treeView1.SelectedNode;
            switch (name)
            {
                case "添加":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.Name == "AddNewNode")
                        {
                            form.Activate();
                            return;
                        }
                    }
                    AddNewNode ann = new AddNewNode(new formrefresh(SetReload));
                    ann.MdiParent = this;
                    ann.Text = "添加新节点";
                    ann.GetNode(selectednode);
                    ann.GetImagelist(imageList1);
                    ann.Show();
                    break;
                case "删除":
                    DialogResult result = MessageBox.Show("确定要删除【"+selectednode.Text+"】？", "删除节点", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        //判断选定的节点是否存在下一级节点
                        if (selectednode.Nodes.Count == 0)
                        {
                            int id = Convert.ToInt32(selectednode.Tag);
                            selectednode.Remove();//删除节点
                            TreeNodes td=TreeNodes.Find(id);
                            TreeNodes.UpdateParentIndexDel(td.Parentid, td.ParentIndex);
                            string sql = "delete from treenodes_tab t where t.id = " + id;
                            string sqlp = "delete from privilege_node_tab t where t.node_id = " + id;
                            User.UpdateCon(sql, DataAccess.OIDSConnStr);
                            User.UpdateCon(sqlp, DataAccess.OIDSConnStr);
                            MessageBox.Show("删除成功！");
                        }
                        else
                            MessageBox.Show("请先删除此节点下的子节点！", "提示信息", MessageBoxButtons.OK);
                    }
                        
                    break;
                case "修改":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.Name == "ModifyNode")
                        {
                            form.Activate();
                            return;
                        }
                    }
                    ModifyNode mn = new ModifyNode(new formrefresh(SetReload));
                    mn.MdiParent = this;
                    mn.Text = "修改节点";
                    mn.GetNode(selectednode);
                    mn.GetImagelist(imageList1);
                    mn.Show();
                    break;
                default:
                    break;
            }
        }

        private void SetReload()
        {
            treeView1.Nodes.Clear();
            string sql;
            if (User.cur_user == "plm")
                sql = "select * from treenodes_tab t order by t.parent_index";
            else
            {
                string privilegelist = User.FindPrivilegeidList(User.cur_user);
                sql = "select * from treenodes_tab t where t.flag='N' or (t.flag='Y' and t.id in (select p.node_id from privilege_node_tab p where p.privilege_id in (" + privilegelist + ")))  order by t.parent_index";
            }
            CreateTreeView(sql, treeView1.Nodes);
            treeView1.Nodes[0].Expand();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            SetStatus();
        }

        /// <summary>
        /// 设置状态栏
        /// </summary>
        private void SetStatus()
        {
            int count = this.MdiChildren.Length;
            this.toolStripStatusLabel1.Text = string.Format(" 状态:  当前打开窗口共:{0}个", count);
            this.toolStripStatusLabel2.Text = string.Format("系统维护:杨洋(2263)");
            this.toolStripStatusLabel3.Text = string.Format("当前登录用户:{0}", User.cur_user);
            this.toolStripStatusLabel4.Text = System.DateTime.Now.ToString();

        }

        #region 更换皮肤
        private void 钻石蓝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\DiamondBlue.ssk";
            User.GetSkinStr("DiamondBlue.ssk");
        }

        private void vistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\Vista.ssk";
            User.GetSkinStr("RealOne.ssk");
        }

        private void realOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\RealOne.ssk";
            User.GetSkinStr("Vista.ssk");
        }


        private void office2007ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\office2007.ssk";
            User.GetSkinStr("office2007.ssk");
        }

        private void mP10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\MP10.ssk";
            User.GetSkinStr("MP10.ssk");
        }
        #endregion

        #region 导航树管理
        private Point Position = new Point(0, 0);
        
        /// <summary>
        /// 在用户开始拖动项时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left&&User.cur_user=="plm")
                DoDragDrop(e.Item, DragDropEffects.Move);
        }
        /// <summary>
        /// 将对象拖过控件的边界时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            //当光标悬停在 TreeView 控件上时，展开该控件中的 TreeNode   

            Point p = this.treeView1.PointToClient(Control.MousePosition);
            TreeNode tn = this.treeView1.GetNodeAt(p);
            if (tn != null)
            {
                if (this.dragDropTreeNode != tn) //移动到新的节点   
                {
                    if (tn.Nodes.Count > 0 && tn.IsExpanded == false)
                    {
                        this.startTime = DateTime.Now;//设置新的起始时间   
                    }
                }
                else
                {
                    if (tn.Nodes.Count > 0 && tn.IsExpanded == false && this.startTime != DateTime.MinValue)
                    {
                        TimeSpan ts = DateTime.Now - this.startTime;
                        if (ts.TotalMilliseconds >= 1000) //一秒   
                        {
                            tn.Expand();
                            this.startTime = DateTime.MinValue;
                        }
                    }
                }
            }
            //设置拖放标签Effect状态   
            if (tn != null)//&& (tn != this.treeView.SelectedNode)) //当控件移动到空白处时，设置不可用。   
            {
                if ((e.AllowedEffect & DragDropEffects.Move) != 0)
                {
                    e.Effect = DragDropEffects.Move;
                }
                if (((e.AllowedEffect & DragDropEffects.Copy) != 0) && ((e.KeyState & 0x08) != 0))//Ctrl key   
                {
                    e.Effect = DragDropEffects.Copy;
                }
                if (((e.AllowedEffect & DragDropEffects.Link) != 0) && ((e.KeyState & 0x08) != 0) && ((e.KeyState & 0x04) != 0))//Ctrl hift key   
                {
                    e.Effect = DragDropEffects.Link;
                }
                if (e.Data.GetDataPresent(typeof(TreeNode)))//拖动的是TreeNode   
                {
                    TreeNode parND = tn;//判断是否拖到了子项   
                    bool isChildNode = false;
                    while (parND.Parent != null)
                    {
                        parND = parND.Parent;
                        if (parND == this.treeView1.SelectedNode)
                        {
                            isChildNode = true;
                            break;
                        }
                    }
                    if (isChildNode)
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else if (e.Data.GetDataPresent(typeof(ListViewItem)))//拖动的是ListViewItem   
                {
                    if (tn.Parent == null)
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            //设置拖放目标TreeNode的背景色   
            if (e.Effect == DragDropEffects.None)
            {
                if (this.dragDropTreeNode != null) //取消被放置的节点高亮显示   
                {
                    this.dragDropTreeNode.BackColor = SystemColors.Window;
                    this.dragDropTreeNode.ForeColor = SystemColors.WindowText;
                    this.dragDropTreeNode = null;
                }
            }
            else
            {
                if (tn != null)
                {
                    if (this.dragDropTreeNode != null)
                    {
                        if (this.dragDropTreeNode != tn)
                        {
                            this.dragDropTreeNode.BackColor = Color.Empty;//取消上一个被放置的节点高亮显示   
                            this.dragDropTreeNode.ForeColor = SystemColors.WindowText;
                            this.dragDropTreeNode = tn;//设置为新的节点   
                            this.dragDropTreeNode.BackColor = SystemColors.Highlight;
                            this.dragDropTreeNode.ForeColor = SystemColors.HighlightText;
                        }
                    }
                    else
                    {
                        this.dragDropTreeNode = tn;//设置为新的节点   
                        this.dragDropTreeNode.BackColor = SystemColors.Highlight;
                        this.dragDropTreeNode.ForeColor = SystemColors.HighlightText;
                    }
                }
            }
        }
        /// <summary>
        /// 拖放操作完成时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode myNode = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
            }
            else
            {
                MessageBox.Show("error");
            }
            Position.X = e.X;
            Position.Y = e.Y;
            Position = treeView1.PointToClient(Position);
            TreeNode DropNode = this.treeView1.GetNodeAt(Position);
            // 1.目标节点不是空。2.目标节点不是被拖拽接点的子节点。3.目标节点不是被拖拽节点本身
            if (DropNode != null && DropNode.Parent != myNode && DropNode != myNode)
            {
                TreeNode DragNode = (TreeNode)myNode.Clone();
                if (myNode.Parent == DropNode.Parent)//如果是同级节点
                {
                    TreeNodes mynode=TreeNodes.Find(Convert.ToInt32(myNode.Tag));
                    TreeNodes dropnode=TreeNodes.Find(Convert.ToInt32(DropNode.Tag));
                    if (myNode.NextNode == DropNode)
                    {
                        TreeNodes.UpdateParentIndex(Convert.ToInt32(DragNode.Tag), DropNode.Index);
                        TreeNodes.UpdateParentIndex(Convert.ToInt32(DropNode.Tag), DropNode.Index - 1);
                        DropNode.Parent.Nodes.Insert(DropNode.Index + 1, DragNode);
                    }
                    else
                    {
                        if (mynode.ParentIndex > dropnode.ParentIndex)//向上拖动
                        {
                            TreeNodes.UpdateParentIndexAdd(mynode.Parentid, mynode.ParentIndex, DropNode.Index);
                            TreeNodes.UpdateParentIndex(Convert.ToInt32(DragNode.Tag), DropNode.Index);
                        }
                        else//向下拖动
                        {
                            TreeNodes.UpdateParentIndexDel(mynode.Parentid, mynode.ParentIndex, DropNode.Index);
                            TreeNodes.UpdateParentIndex(Convert.ToInt32(DragNode.Tag), DropNode.Index - 1);
                        }
                        DropNode.Parent.Nodes.Insert(DropNode.Index , DragNode);
                    }
                    treeView1.SelectedNode = DragNode;
                    myNode.Remove();            
                }
                else
                {
                    // 将被拖拽节点从原来位置删除。
                    myNode.Remove();

                    // 在目标节点下增加被拖拽节点
                    DropNode.Nodes.Add(DragNode);
                    TreeNodes.UpdateParentAndParentIndex(Convert.ToInt32(DragNode.Tag), Convert.ToInt32(DropNode.Tag), DropNode.Nodes.Count - 1);
                }
                DropNode.BackColor = Color.Empty;//取消上一个被放置的节点高亮显示   
                DropNode.ForeColor = SystemColors.WindowText;
            }
            // 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
            if (DropNode == null)
            {
                MessageBox.Show("目标节点不存在!");
                //TreeNode DragNode = myNode;
                //myNode.Remove();
                //treeView1.Nodes.Add(DragNode);
            }
        }
        /// <summary>
        /// 用鼠标将某项拖动到该控件的工作区时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion
        #region 添加删除修改导航树节点
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        #endregion
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IntroForm introform = new IntroForm();
            introform.ShowDialog();

        }

        private void 首选项PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolsOptionForm toform = new ToolsOptionForm();
            toform.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Activate();
            this.Refresh();
        }

        /// <summary>
        /// 导出当前子窗体到excel表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelStripButton_Click(object sender, EventArgs e)
        {
            string frmtext = MDIForm.pMainWin.ActiveMdiChild.Name.ToString();
            DataGridView dgv = null;
            int count = 0;
            string tabpage = string.Empty;
            switch (frmtext)
            {
                case "SpoolGeneralViewForm":
                    dgv = (DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;

                case "ProgressStatistics":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["panel2"].Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                    
                case "BlockConstructionPlanForm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["dgvInfo"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;

                case "DataMaintenanceForm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["EditDgv"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;

                case "PROJECTDRAWINGINFO":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["groupBox2"].Controls["DrawingsDgv"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;

                case "DetailsForm":
                    tabpage = ((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).SelectedTab.Text.ToString();
                    if (tabpage == "材料信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage1"].Controls[0]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "连接件信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage2"].Controls[0]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "加工信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage3"].Controls[0]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "焊接信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage4"].Controls[0]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "相对坐标信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage5"].Controls[0]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }

                    else if (tabpage == "小票日志")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage10"].Controls[0]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    break;
                case "DrawingForm":
                    tabpage = ((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).SelectedTab.Text.ToString();
                    if (tabpage == "小票信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage1"].Controls["Appdgv"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "材料信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage2"].Controls["Materialdgv"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "连接件信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage12"].Controls["Connectordgv"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "附件信息")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage3"].Controls["Partdgv"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "附件材料表")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage6"].Controls["dataGridView1"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    else if (tabpage == "附件材料设备定额表")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage7"].Controls["dataGridView2"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }

                    else if (tabpage == "管系托盘表")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage8"].Controls["dataGridView3"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }

                    else if (tabpage == "管系材料表")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage9"].Controls["dataGridView4"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }

                    else if (tabpage == "管系材料设备定额表")
                    {
                        dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage10"].Controls["dataGridView5"]);
                        count = dgv.Rows.Count;
                        if (count == 0)
                        {
                            return;
                        }
                        User.ExportToExcel(dgv, toolStripProgressBar2);
                    }
                    break;
                case "HourNormForm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["dgvhournorm"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;

                case "WorkShopPlan":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["PlanDGV"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "WShopSPlTrace":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["SPTraceDgv"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "WShopManHour":
                    dgv = (DataGridView)((MDIForm.pMainWin.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls[0]));
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;

                case "NormalPipeWorkingTime":
                    dgv = (DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "SpoolReportFrm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["groupBox2"].Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "MaterialReportsFrm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["groupBox5"].Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "MaterialTraceabilityFrm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "ModifyStatisticsFrm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["groupBox2"].Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "ModifyDelInfoFrm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "ECDrawingReportFrm":
                    dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["dataGridView1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                case "MaterialRationForm":
                    dgv = (DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["groupBox2"].Controls["dgv1"]);
                    count = dgv.Rows.Count;
                    if (count == 0)
                    {
                        return;
                    }
                    User.ExportToExcel(dgv, toolStripProgressBar2);
                    break;
                default:
                    break;
            }
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchSheet bsform = new BatchSheet();
            bsform.MdiParent = this;
            bsform.Show();
        }

        private void 测试ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolsOptionForm tofprm = new ToolsOptionForm();
            tofprm.MdiParent = this;
            tofprm.Show();
        }

        #region 拷贝数据
        /// <summary>
        /// 拷贝当前选中数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyStripButton_Click(object sender, EventArgs e)
        {
            int count = this.MdiChildren.Length;
            if (count == 0)
            {
                return;
            }
            else
            {
                DataGridView dgv = null;
                string frmtext = MDIForm.pMainWin.ActiveMdiChild.Name.ToString();
                string tabpage = string.Empty;
                switch (frmtext)
                {
                    case "SpoolGeneralViewForm":
                        dgv = (DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"]);
                        if (dgv.Rows.Count == 0)
                        {
                            return;
                        }
                        DataGridViewEnableCopy(dgv);
                        break;
                    case "ProgressStatistics":
                        dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["panel2"].Controls["dataGridView1"]);
                        if (dgv.Rows.Count == 0)
                        {
                            return;
                        }
                        DataGridViewEnableCopy(dgv);
                        break;

                    case "BlockConstructionPlanForm":
                        dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["dgvInfo"]);
                        if (dgv.Rows.Count == 0)
                        {
                            return;
                        }
                        DataGridViewEnableCopy(dgv);
                        break;

                    case "DataMaintenanceForm":
                        dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["EditDgv"]);
                        if (dgv.Rows.Count == 0)
                        {
                            return;
                        }
                        DataGridViewEnableCopy(dgv);
                        break;

                    case "PROJECTDRAWINGINFO":
                        dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls["groupBox2"].Controls["DrawingsDgv"]);
                        if (dgv.Rows.Count == 0)
                        {
                            return;
                        }
                        DataGridViewEnableCopy(dgv);
                        break;

                    case "DetailsForm":
                        tabpage = ((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).SelectedTab.Text.ToString();
                        if (tabpage == "材料信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage1"].Controls[0]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "连接件信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage2"].Controls[0]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "加工信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage3"].Controls[0]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "焊接信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage4"].Controls[0]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "相对坐标信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage5"].Controls[0]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }

                        else if (tabpage == "小票日志")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage10"].Controls[0]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        break;

                    case "DrawingForm":
                        tabpage = ((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).SelectedTab.Text.ToString();
                        if (tabpage == "小票信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage1"].Controls["Appdgv"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "材料信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage2"].Controls["Materialdgv"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "附件信息")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage3"].Controls["Partdgv"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "附件材料表")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage6"].Controls["dataGridView1"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        else if (tabpage == "附件材料设备定额表")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage7"].Controls["dataGridView2"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }

                        else if (tabpage == "管系托盘表")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage8"].Controls["dataGridView3"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }

                        else if (tabpage == "管系材料表")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage9"].Controls["dataGridView4"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }

                        else if (tabpage == "管系材料设备定额表")
                        {
                            dgv = (DataGridView)(((TabControl)(MDIForm.ActiveForm.ActiveMdiChild.Controls["tabControl1"])).Controls["tabPage10"].Controls["dataGridView5"]);
                            if (dgv.Rows.Count == 0)
                            {
                                return;
                            }
                            DataGridViewEnableCopy(dgv);
                        }
                        break;
                    case "HourNormForm":
                        dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls["dgvhournorm"]);
                        if (dgv.Rows.Count == 0)
                        {
                            return;
                        }
                        DataGridViewEnableCopy(dgv);
                        break;
                    case "MachinningInfoFrm":
                        dgv = (DataGridView)(MDIForm.ActiveForm.ActiveMdiChild.Controls[0].Controls["dataGridView1"]);
                        if (dgv.Rows.Count == 0)
                        {
                            return;
                        }
                        DataGridViewEnableCopy(dgv);
                        break;

                    default:
                        break;
                }
            }
        }

        private void DataGridViewEnableCopy(DataGridView dgv)
        {
            Clipboard.SetData(DataFormats.Text,dgv.GetClipboardContent().GetData(DataFormats.Text.ToString()));
        }
        #endregion

        private void 放飞ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkShopWorkerData wwdform = new WorkShopWorkerData();
            wwdform.MdiParent = this;
            wwdform.Show();
        }


        private void 车间计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkShopPlan planfrm = new WorkShopPlan();
            planfrm.MdiParent = this;
            planfrm.Show();
        }

        private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolsOptionForm tpform = new ToolsOptionForm();
            tpform.MdiParent = this;
            tpform.Show();
        }

        private void nnnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaitingForm wform = new WaitingForm();
            wform.MdiParent = this;
            wform.Show();
        }
    }
}