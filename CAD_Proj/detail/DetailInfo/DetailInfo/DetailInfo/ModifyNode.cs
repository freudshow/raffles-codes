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
    public partial class ModifyNode : Form
    {
        private TreeNode currentnode = new TreeNode();
        string flag = "N";
        private formrefresh modify_setreload;
        private TreeNodes tnTmp = null;
        private List<TreeNodes> nodelist = TreeNodes.FindAll();
        public ModifyNode(formrefresh setreload)
        {
            InitializeComponent();
            this.modify_setreload = setreload;
        }
        /// <summary>
        /// 获得当前节点
        /// </summary>
        /// <param name="node"></param>
        public void GetNode(TreeNode node)
        {
            currentnode = node;
        }
        /// <summary>
        /// 获得父窗口树的imagelist
        /// </summary>
        /// <param name="imglist"></param>
        public void GetImagelist(System.Windows.Forms.ImageList imglist)
        {
            imageList1 = imglist;
        }
        private void ModifyNode_Load(object sender, EventArgs e)
        {
            #region 初始化扩展的下拉框控件
            //checkedListBox1.SelectionMode= SelectionMode.MultiSimple;
            comboBox1.ImageList = imageList1;
            comboBox2.ImageList = imageList1;
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                comboBox1.Items.Add(new ComboBoxExItem(i.ToString(), i));
                comboBox2.Items.Add(new ComboBoxExItem(i.ToString(), i));
            }
            #endregion
            textBox2.Text = currentnode.Text;
            textBox3.Text = currentnode.Name;
            comboBox1.Text = currentnode.ImageIndex.ToString();
            comboBox2.Text = currentnode.SelectedImageIndex.ToString();
            flag = DetailInfo.Categery.TreeNodes.GetNodeFlag(Convert.ToInt32(currentnode.Tag));
            if (flag == "N")
            {
                radioBtn.Checked = true;
                radioButton1.Checked = false;
                checkedListBox1.Visible = false;
            }
            else
            {
                radioButton1.Checked = true;
                radioBtn.Checked = false;
                checkedListBox1.Visible = true;
            }
            string sql = "select t.privilege_id,t.privilege_flag from privilege_tab t where t.privilege_cata='Pipe' order by privilege_id";
            DataBind(sql);
            List<int> privilegeids = PrivilegeNode.GetPrivilegeIds(Convert.ToInt32(currentnode.Tag));
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DataSet ds=db.ExecuteDataSet (CommandType.Text, sql );
            //string aa = checkedListBox1.GetItemText(checkedListBox1.Items[1]);
            for (int j = 0; j < checkedListBox1.Items.Count; j++)
            {
                for (int i = 0; i < privilegeids.Count; i++)
                {
                    if (ds.Tables[0].Rows[j]["privilege_id"].ToString ()== privilegeids[i].ToString())
                        checkedListBox1.SetItemChecked(j, true);
                }
            }                                                                                                                                                                  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text.Trim();
            string name = textBox3.Text.Trim();
            int imageindex = Convert.ToInt32(comboBox1.SelectedIndex);
            int selectedimageindex = Convert.ToInt32(comboBox2.SelectedIndex);
            int parentid = Convert.ToInt32(currentnode.Tag);
            if (radioBtn.Checked == true)
                flag = "N";
            else
                flag = "Y";
            string sql = "update treenodes_tab t set t.text='" + text + "',t.name='" + name + "',t.imageindex=" + imageindex + ",t.selectedimageindex=" + selectedimageindex + ",t.flag='"+flag+"'     where t.id=" + Convert.ToInt32(currentnode.Tag);
            User.UpdateCon(sql, DataAccess.OIDSConnStr);
            if (flag == "Y")
            {
                string sqlp = "delete from privilege_node_tab t where t.node_id = " + Convert.ToInt32(currentnode.Tag);
                User.UpdateCon(sqlp, DataAccess.OIDSConnStr);
                int n = 0;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        n++;
                        checkedListBox1.SetSelected(i, true);
                        int privilegeid = Convert.ToInt32(checkedListBox1.SelectedValue);
                        string sqlstr = "insert into privilege_node_tab t ( t.privilege_id,t.node_id) values (" + privilegeid + ","+Convert.ToInt32(currentnode.Tag)+")";
                        User.UpdateCon(sqlstr, DataAccess.OIDSConnStr);
                        SetPrivilegetoParentnode(privilegeid, Convert.ToInt32(currentnode.Tag));
                    }
                }
                if (n == 0)
                {
                    MessageBox.Show("请选择权限！", "提示信息", MessageBoxButtons.OK);
                    return;
                }
                MessageBox.Show("修改节点成功！");
            }
            else
                MessageBox.Show("修改节点成功！");
            this.modify_setreload();
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
                    string sql = "select t.privilege_id,t.privilege_flag from privilege_tab t where t.privilege_cata='Pipe'";
                    DataBind(sql);
                    checkedListBox1.Focus();
                    break;
                default:
                    break;
            }
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
        /// <summary>
        /// 当子节点权限改变时，父节点设置相同权限
        /// </summary>
        /// <param name="nodeid"></param>
        private void SetPrivilegetoParentnode(int privilegeid, int nodeid)
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
                        SetPrivilegetoParentnode(privilegeid, parentid);
                    }
                }
            }
        }
    }
}