using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JuniorTreeviewManager;
using System.Collections;
using System.Data.OracleClient;




namespace DetailInfo
{
    public partial class ToolsOptionForm : Form
    {

        private IList GanttObjectList = new ArrayList();

        public ToolsOptionForm()
        {
            InitializeComponent();
        }

        private void ToolsOptionForm_Load(object sender, EventArgs e)
        {
            DBConnection.GetProjectID(this.ProjectComboBox);
        }


        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTreeViewButton_Click(object sender, EventArgs e)
        {
            int count = this.ProjectTreeView.Nodes.Count;
            if (count == 0)
            {
                return;
            }
            this.ProjectTreeView.LabelEdit = true;
            if (this.ProjectTreeView.SelectedNode != null)
            {
                this.ProjectTreeView.SelectedNode.BeginEdit();
            }
            
        }

        private void ProjectTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            this.ProjectTreeView.LabelEdit = false;

            string projectname = this.ProjectTreeView.Nodes[0].Text.ToString();
            string edit_nodetxt = e.Node.Text.ToString();
            string edit_parenttxt = string.Empty;
            int parent_id = 0;
            if (e.Node.Parent == null)
            {
                edit_parenttxt = string.Empty;
                parent_id = 0;
            }
            else
            {
                edit_parenttxt = e.Node.Parent.Text.ToString();
                parent_id = DBConnection.GetParentID(projectname, edit_parenttxt);
            }
            int edit_nodeindex = e.Node.Index;

            if (string.IsNullOrEmpty(e.Label) == true)
            {
                return;
            }
            string after_edit_nodetxt = e.Label.ToString();

            DBConnection.EditTreeNode(projectname, edit_nodetxt, edit_nodeindex, after_edit_nodetxt,parent_id);



        }

        #region TreeView节点的拖拽功能
        private void ProjectTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop((TreeNode)e.Item, DragDropEffects.Move);
            }
            else
            {
                return;
            }
        }


        private TreeNode MyOldNode;

        private void ProjectTreeView_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                object MyData = e.Data.GetData(typeof(TreeNode));
                //如果节点有数据，拖放目标允许移动   
                if (MyData != null)
                {
                    e.Effect = DragDropEffects.Move;

                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
                TreeView MyTreeView = (TreeView)sender;
                TreeNode MyNode = MyTreeView.GetNodeAt(ProjectTreeView.PointToClient(new Point(e.X, e.Y)));
                //if (MyNode != null)
                //{   //改变进入节点的背景色   
                //    MyNode.BackColor = Color.Blue;
                //    //保存此节点，进入下一个时还原背景色   
                //    MyOldNode = MyNode;
                //}
            }
            catch (SystemException ex)
            {
                return;
            }


        }

        private void ProjectTreeView_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                TreeView MyTreeView = (TreeView)sender;
                TreeNode MyNode = MyTreeView.GetNodeAt(this.ProjectTreeView.PointToClient(new Point(e.X, e.Y)));
                if ((MyNode != null) && (MyNode != MyOldNode))
                {
                    //MyOldNode.BackColor = Color.White;
                    //MyNode.BackColor = Color.Blue;
                    MyOldNode = MyNode;
                }
                else
                {
                    return;
                }
            }
            catch (SystemException ex)
            {
                return;
            }

        }

        private void ProjectTreeView_DragDrop(object sender, DragEventArgs e)
        {

            TreeNode MyNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            TreeView MyTreeView = (TreeView)sender;
            //得到当前鼠标进入的节点   
            TreeNode MyTargetNode = MyTreeView.GetNodeAt(ProjectTreeView.PointToClient(new Point(e.X, e.Y)));
            if (MyTargetNode != null)
            {
                TreeNode MyTargetParent = MyTargetNode.Parent;
                //删除拖放的节点   
                if (MyNode == MyTargetNode)
                {
                    return;
                }
                MyNode.Remove();
                //添加到目标节点   

                MyTargetNode.Nodes.Add(MyNode);


                //MyTargetNode.BackColor = Color.White;
                MyTreeView.SelectedNode = MyTargetNode;
            }
 


        }
        #endregion

        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemstr = this.ProjectComboBox.SelectedItem.ToString();
            if (itemstr == "-新置项任务-")
            {
                this.ProjectTreeView.Nodes.Clear();

                string sqlString = "select count(*) from project_tree_tab where PROJECTID = '新置项目' and NODETEXT = '新置项目' and NODEPARENT is null and PARENTID = 0 and NODEINDEX = 0 ";
                object count = User.GetScalar(sqlString, DataAccess.OIDSConnStr);
                if (count == null || Convert.ToInt16(count.ToString()) == 0)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = "新置项目";
                    this.ProjectTreeView.Nodes.Add(tn);
                    //DBConnection.SaveTreeView("新置项目", "新置项目", "", 0, 0);
                    DBConnection.SaveTreeView("新置项目", "新置项目", 0, 0);
                }
                else
                {
                    return;
                }

            }
            else
            {
                this.ProjectTreeView.Nodes.Clear();
                string sql = "select * from project_tree_tab where PROJECTID = '"+itemstr+"' order by nodeindex";
                CreateTreeView(sql, this.ProjectTreeView.Nodes);
                this.ProjectTreeView.ExpandAll();
            }
        }

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
            filter = string.Format("PARENTID={0}", parentid);
            DataRow[] drarr = dataSource.Select(filter);//将过滤的ID放入数组中
            TreeNode node;
            foreach (DataRow dr in drarr)//递归循环查询出数据
            {
                node = new TreeNode();
                node.Text = dr["NODETEXT"].ToString();
                //node.Name = dr["name"].ToString();
                node.Tag = Convert.ToInt32(dr["ID"]);
                nodes.Add(node);//加入节点
                CreateTreeViewRecursive(node.Nodes, dataSource, Convert.ToInt32(node.Tag));
            }
        }

        /// <summary>
        /// 保存到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            int count = this.ProjectTreeView.Nodes.Count;
            if (count != 1)
            {
                DialogResult res;
                res = MessageBox.Show("请确定在建项目的唯一！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result;
            result = MessageBox.Show("确定要保存吗?", "保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                
                GetTreeNode(this.ProjectTreeView.Nodes[0].Text.ToString());
                this.ProjectComboBox.Items.Clear();
                DBConnection.GetProjectID(this.ProjectComboBox);
            }
            else
            {
                return;
            }
            
        }

        #region 获取TreeView所有节点信息并把TreeView写进数据库
        private void GetTreeNode(string pid)
        {
            foreach (TreeNode tn in this.ProjectTreeView.Nodes)
            {
                string projectid = tn.Text.ToString();
                string nodename = tn.Text.ToString();
                int nodeind = tn.Index;
                string sqlString = "select count(*)  from project_tree_tab where  PROJECTID = '"+projectid+"'";
                object obj = User.GetScalar(sqlString, DataAccess.OIDSConnStr);
                if (Convert.ToInt16(obj.ToString()) == 0 || obj == null)
                {
                    DBConnection.InsertRootNode(projectid, nodename, nodeind, nodeind);
                    //DBConnection.InsertNodesIntoPlanTab(projectid, nodename);
                    GetNodeDetail(tn, pid);
                }
                else
                {
                    MessageBox.Show("该项目已经存在于数据库，不可重复创建！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                //DBConnection.InsertRootNode(projectid, nodename, nodeind);
                //DBConnection.InsertNodesIntoPlanTab(projectid, nodename);
                //GetNodeDetail(tn,pid);
            }
        }


        private void GetNodeDetail(TreeNode node, string projectname)
        {
            
            foreach (TreeNode n in node.Nodes)
            {
                TreeNode rootnode = findparent(n);
                string projectstr = rootnode.Text.ToString();

                string node_txt = n.Text.ToString();
                string parent_txt = n.Parent.Text.ToString();

                int parent_id = DBConnection.GetParentID(projectname, parent_txt);
                int nodeind = n.Index;
                DBConnection.SaveTreeView(projectstr, node_txt, parent_id, nodeind);
                //DBConnection.SaveTreeView(projectstr, node_txt, parent_txt, parent_id, nodeind);
                //DBConnection.InsertNodesIntoPlanTab(projectstr, node_txt);
                GetNodeDetail(n, projectname);
                
            }
        }

        TreeNode   findparent   (TreeNode   node   ) 
        { 
            if   (node.Parent   ==   null) 
            {       
                    return   node; 
            } 
            else 
            {
                return findparent(node.Parent); 
            } 
        }


        #endregion


        private void ProjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearDateTimePicker();
            DataSet MyDs;
            string sqlString = string.Empty;
            
            if (this.ProjectTreeView.SelectedNode == null)
            {
                return;
            }
            else
            {
                if (this.ProjectTreeView.SelectedNode.Parent == null)
                {

                    MyDs = new DataSet();
                    sqlString = "SELECT  NODETEXT, PLANSTART, PLANEND, ACTUALSTART, ACTUALEND, BALANCE, RESPONSIBLE, CREATEDATE,VERSION,LATESTEDIT,BREAKPOINT  FROM project_tree_tab WHERE PROJECTID = '" + this.ProjectTreeView.SelectedNode.Text.ToString() + "' and version = 'Y'";
                    User.DataBaseConnect(sqlString, MyDs);
                    this.PlanDgv.DataSource = MyDs.Tables[0].DefaultView;
                    foreach (DataGridViewColumn dgvc in this.PlanDgv.Columns)
                    {
                        dgvc.ReadOnly = true;
                    }
                    MyDs.Dispose();
                }
                else
                {
                    //this.PlanDgv.Columns[1].ReadOnly = this.PlanDgv.Columns[2].ReadOnly = this.PlanDgv.Columns[3].ReadOnly = this.PlanDgv.Columns[4].ReadOnly = this.PlanDgv.Columns[5].ReadOnly = false;
                    MyDs = new DataSet();
                    sqlString = "SELECT  NODETEXT, PLANSTART, PLANEND, ACTUALSTART, ACTUALEND, BALANCE, RESPONSIBLE, CREATEDATE,VERSION,LATESTEDIT,BREAKPOINT  FROM project_tree_tab WHERE PROJECTID = '" + this.ProjectTreeView.Nodes[0].Text.ToString() + "' AND NODETEXT = '" + this.ProjectTreeView.SelectedNode.Text.ToString() + "' and version = 'Y'";
                    User.DataBaseConnect(sqlString, MyDs);
                    this.PlanDgv.DataSource = MyDs.Tables[0].DefaultView;
                    MyDs.Dispose();
                }

            }
        }

        private void TaskSaveBtn_Click(object sender, EventArgs e)
        {

        }

        DateTimePicker dtp = new DateTimePicker();
        private void PlanDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                int count = this.PlanDgv.Rows.Count;
                string sqlString = "select count(*)  from project_tree_tab where  PROJECTID = '" + this.ProjectTreeView.Nodes[0].Text.ToString() + "'";
                object obj = User.GetScalar(sqlString, DataAccess.OIDSConnStr);
                if (Convert.ToInt16(obj.ToString()) == count)
                {
                    return;
                }
                else
                {
                    if (this.PlanDgv.CurrentCell.ColumnIndex == 1 || this.PlanDgv.CurrentCell.ColumnIndex == 2 || this.PlanDgv.CurrentCell.ColumnIndex == 3 || this.PlanDgv.CurrentCell.ColumnIndex == 4)
                    {
                        this.PlanDgv.Controls.Add(dtp);
                        Rectangle rect = this.PlanDgv.GetCellDisplayRectangle(this.PlanDgv.CurrentCell.ColumnIndex, this.PlanDgv.CurrentCell.RowIndex, false);
                        dtp.Left = rect.Left;
                        dtp.Top = rect.Top;
                        dtp.Width = rect.Width;
                        dtp.Height = rect.Height;
                        dtp.BringToFront();
                        dtp.Visible = true;
                        dtp.ValueChanged += new EventHandler(dtp_ValueChanged);
                    }
                    else if (this.PlanDgv.CurrentCell.ColumnIndex != 1 && this.PlanDgv.CurrentCell.ColumnIndex != 2 && this.PlanDgv.CurrentCell.ColumnIndex != 3 && this.PlanDgv.CurrentCell.ColumnIndex != 4)
                    {
                        ClearDateTimePicker();
                    }
                }
            }
        }

        DateTime dt;
        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("-------------aaaa-------------");
            dt = dtp.Value;
            this.PlanDgv.CurrentCell.Value = dt;

        }

        /// <summary>
        /// 使datetimepicker不可视
        /// </summary>
        private void ClearDateTimePicker()
        {
            foreach (Control ctl in this.PlanDgv.Controls)
            {
                if (ctl is DateTimePicker)
                {
                    ctl.Visible = false;
                }
            }
        }

        /// <summary>
        /// 改变datagridview列宽释放datetimepicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlanDgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ClearDateTimePicker();
        }

        /// <summary>
        /// 垂直或水平拉动滚动条释放datetimepicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlanDgv_Scroll(object sender, ScrollEventArgs e)
        {
            ClearDateTimePicker();
        }

        /// <summary>
        /// 激活窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolsOptionForm_Activated(object sender, EventArgs e)
        {
            MDIForm.tool_strip.Items[0].Enabled = false;
        }

        /// <summary>
        /// 添加同级节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ProjectTreeView.SelectedNode == null)
            {
                return;
            }
            else
            {
                if (this.ProjectTreeView.SelectedNode.Parent == null)
                {
                    return;
                }
                int count = this.ProjectTreeView.SelectedNode.Parent.Nodes.Count;
                TreeNode tn = new TreeNode();
                tn.Text = this.ProjectTreeView.SelectedNode.Parent.Text.ToString() + count.ToString();
                this.ProjectTreeView.SelectedNode.Parent.Nodes.Add(tn);

                string projectname = this.ProjectTreeView.Nodes[0].Text.ToString();
                string parent_txt = tn.Parent.Text.ToString();
                int parent_id = DBConnection.GetParentID(projectname, parent_txt);
                DBConnection.SaveTreeView(projectname, tn.Text, parent_id, count);
                //DBConnection.SaveTreeView(projectname, tn.Text, parent_txt, parent_id,count);
                //DBConnection.InsertNodesIntoPlanTab(projectname, tn.Text);
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加子节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ProjectTreeView.SelectedNode == null)
            {
                return;
            }
            else
            {
                int count = this.ProjectTreeView.SelectedNode.Nodes.Count;
                TreeNode tn = new TreeNode();
                tn.Text = this.ProjectTreeView.SelectedNode.Text.ToString() + count.ToString();

                this.ProjectTreeView.SelectedNode.Nodes.Add(tn);
                this.ProjectTreeView.SelectedNode.Expand();

                string projectname = this.ProjectTreeView.Nodes[0].Text.ToString();
                string parent_txt = tn.Parent.Text.ToString();
                int parent_id = DBConnection.GetParentID(projectname, parent_txt);
                DBConnection.SaveTreeView(projectname, tn.Text, parent_id, count);
                //DBConnection.SaveTreeView(projectname, tn.Text, parent_txt, parent_id, count);
                //DBConnection.InsertNodesIntoPlanTab(projectname, tn.Text);

            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.ProjectTreeView.SelectedNode;
            if (tn != null)
            {
                DialogResult result;
                result = MessageBox.Show("确定要删除该任务？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    tn.Remove();
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 上移节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            JuniorTreeviewManager.TreeviewJManager.moveUp(ref ProjectTreeView);
        }

        /// <summary>
        /// 下移节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JuniorTreeviewManager.TreeviewJManager.moveDown(ref ProjectTreeView);
        }

        /// <summary>
        /// 升级节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 升级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JuniorTreeviewManager.TreeviewJManager.increaseLevel(ref ProjectTreeView);
        }

        /// <summary>
        /// 降级节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 降级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JuniorTreeviewManager.TreeviewJManager.decreaseLevel(ref ProjectTreeView);
        }

        /// <summary>
        /// 右键控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.ProjectTreeView.SelectedNode == null)
            {
                for (int i = 0; i < this.contextMenuStrip1.Items.Count; i++)
                {
                    this.contextMenuStrip1.Items[i].Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < this.contextMenuStrip1.Items.Count; i++)
                {
                    this.contextMenuStrip1.Items[i].Visible = true;
                }
            }
        }
    }
}
