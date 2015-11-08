using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using DetailInfo.Categery;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;

namespace DetailInfo
{
    public partial class AddNewNode : Form
    {
        private TreeNode parentnode=new TreeNode();
        string flag = "N";
        private formrefresh add_setreload;
        private TreeNodes tnTmp = null;
        private List<TreeNodes> nodelist = TreeNodes.FindAll();
        public AddNewNode(formrefresh setreload)
        {
            InitializeComponent();
            this.add_setreload = setreload;
        }
        /// <summary>
        /// 获得父节点
        /// </summary>
        /// <param name="node"></param>
        public void GetNode(TreeNode node)
        {
            parentnode = node;
        }
        /// <summary>
        /// 获得父窗口树的imagelist
        /// </summary>
        /// <param name="imglist"></param>
        public  void GetImagelist(System.Windows.Forms.ImageList imglist)
        {
            imageList1 = imglist;
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sqlstr"></param>
        private void DataBind(string sqlstr)
        {
            try
            {
                OracleConnection con = new OracleConnection(DataAccess.OIDSConnStr);
                con.Open();
                OracleDataAdapter oda = new OracleDataAdapter(sqlstr, con);
                OracleCommandBuilder builder = new OracleCommandBuilder(oda);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                checkedListBox1.DataSource = ds.Tables[0];
                checkedListBox1.DisplayMember = ds.Tables[0].Columns[1].ToString(); //要显示的属性名
                checkedListBox1.ValueMember = ds.Tables[0].Columns[0].ToString(); //存储的属性名
                con.Close();
                ds.Dispose();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
               return;
            }
        } 
        private void AddNewNode_Load(object sender, EventArgs e)
        {
            label7.Text = parentnode.Text;
            checkedListBox1.Visible = false;
            DataSet ds = TreeNodes.GetSubNodes(Convert.ToInt32(parentnode.Tag));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox3.Items.Add(ds.Tables[0].Rows[i][1]);
            }
            comboBox3.Items.Add("【无】");
            #region 初始化可扩展的下拉框控件
            comboBox1.ImageList = imageList1;
            comboBox2.ImageList = imageList1;
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                comboBox1.Items.Add(new ComboBoxExItem(i.ToString(),i));
                comboBox2.Items.Add(new ComboBoxExItem(i.ToString(),i));
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text.Trim();
            if (text == string.Empty)
            {
                MessageBox.Show("节点标识不能为空！");
                return;
            }
            string name = textBox3.Text.Trim();
            if (name == string.Empty)
            {
                MessageBox.Show("节点名称不能为空！");
                return;
            }
            int imageindex = Convert.ToInt32(comboBox1.SelectedIndex);
            int selectedimageindex = Convert.ToInt32(comboBox2.SelectedIndex);
            int parentid =  Convert.ToInt32(parentnode.Tag);
            int afternode = Convert.ToInt32(comboBox3.SelectedIndex);
            if (afternode == -1)
            {
                MessageBox.Show("请选择新增节点的后节点！");
                return;
            }
            if (Convert.ToInt32(comboBox3.SelectedIndex) != (comboBox3.Items.Count - 1))//如果该节点不是添加到最后
                TreeNodes.UpdateParentIndexAdd(parentid, afternode);
            string sql = "insert into treenodes_tab t (t.name,t.text,t.imageindex,t.selectedimageindex,t.parent_id,t.flag,t.parent_index) values ('" + name + "','" + text + "'," + imageindex + "," + selectedimageindex + "," + parentid + ",'" + flag + "'," + afternode + ")";
            User.UpdateCon(sql, DataAccess.OIDSConnStr);
            if (flag == "Y")
            {
                int n = 0;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                
                        n++;
                        checkedListBox1.SetSelected(i,true);
                        int privilegeid = Convert.ToInt32(checkedListBox1.SelectedValue);
                        string sqlstr = "insert into privilege_node_tab t ( t.privilege_id,t.node_id) values ("+privilegeid+",(select max(d.id) from treenodes_tab d))";//添加该节点的权限设置关系
                        User.UpdateCon(sqlstr, DataAccess.OIDSConnStr);
                        SetPrivilegetoParentnode(privilegeid,parentid);
                    }
                }
                if (n == 0)
                {
                    MessageBox.Show("请选择权限！","提示信息",MessageBoxButtons.OK);
                    return;
                }

                MessageBox.Show("添加节点成功！");
            }
            else
                MessageBox.Show("添加节点成功！");
            this.add_setreload();
        }

        private void radioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked)
            {
                return;
            }
            switch (((RadioButton)sender).Text.ToString())
            {
                case "N":
                    checkedListBox1.Visible = false;
                    flag = "N";
                    break;
                case "Y":
                    checkedListBox1.Visible = true;
                    flag = "Y";
                    checkedListBox1.Focus();
                    string sql = "select t.privilege_id,t.privilege_flag from privilege_tab t where t.privilege_cata='Pipe'";
                    DataBind(sql);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 当子节点权限改变时，父节点设置相同权限
        /// </summary>
        /// <param name="nodeid"></param>
        private void SetPrivilegetoParentnode(int privilegeid,int nodeid)
        {
            int parentid = TreeNodes.GetParentid(nodeid);
            tnTmp = nodelist.Find(delegate(TreeNodes tn) { return tn.Id == parentid; });
            if (tnTmp == null)//没有父节点
                return;
            else
            {
                if (TreeNodes.GetNodeFlag(parentid) == "N")//父节点没有权限设置
                    return;
                else
                {
                    if (PrivilegeNode.ExistPrivilege(privilegeid, parentid))//如果父节点有该权限设置
                        return;
                    else
                    {
                        PrivilegeNode pnTmp = new PrivilegeNode();//父节点权限设置
                        pnTmp.PrivilegeId = privilegeid;
                        pnTmp.NodeId = parentid;
                        int n = pnTmp.Add();
                        if (n == 0)
                            MessageBox.Show("Error!");
                        SetPrivilegetoParentnode(privilegeid,parentid);
                    }
                }
            }
        }
    }
}