using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework;
namespace DetailInfo.MaterialManage
{
    public partial class ERP_PROJECTTREE : UserControl
    {
        private TreeNode tn = new TreeNode();
        private string ProjectId;
        public ERP_PROJECTTREE(string ParaProjectid)
        {
            ProjectId = ParaProjectid;
            InitializeComponent();
        }

        private void tvMType_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void ERP_PROJECTTREE_Load(object sender, EventArgs e)
        {
            tn.Tag = ProjectId;
            project p = project.Find(ProjectId);
            if (p != null)
            {
                tn.Text = project.Find(ProjectId).description;
                tn.ImageIndex = 3;
                tn.SelectedImageIndex = 3;
            }
            else
            {
                tn.Text = "项目";
            }
            this.tvMType.Nodes.Add(tn);
            //AddFirstSubProject(tn);
            //AddSystem(tn);
            //this.tvMType.ExpandAll();
           
            DataTable dt = new DataTable();
            DataTable dtact = new DataTable();
            //int projectid = ProjectSystem.FindProjectid(ProjectId);
            dt = SubProject.FindAllSubPro(ProjectId).Tables[0];
            dtact = Activity.FindActivityDs(ProjectId).Tables[0];
            //string virroot = ProjectSystem.GetProName(projectID);
            CreateTreeViewRecursiveNew(tn, dt, dtact, "0"); 
        }
        public void CreateTreeViewRecursiveNew(TreeNode nodes, DataTable dataSource, DataTable dataAct, string parentId)
        {

            DataView dv = new DataView(dataSource);
            if (parentId == "0")
                dv.RowFilter = "parent_sub_project_id='0'";
            else
                dv.RowFilter = "parent_sub_project_id='" + parentId + "'";
            foreach (DataRowView dr in dv)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["sub_project_id"].ToString() + " " + dr["description"].ToString();
                node.Tag = dr["sub_project_id"].ToString();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                nodes.Nodes.Add(node);
                DataView dv1 = new DataView(dataAct);
                dv1.RowFilter = "sub_project_id='" + dr["sub_project_id"].ToString() + "'";
                foreach (DataRowView act in dv1)
                {
                    TreeNode tn_act = new TreeNode();
                    tn_act.Name = "activity";
                    tn_act.Tag = act["activity_seq"].ToString();
                    tn_act.Text = act["activity_no"].ToString() + " " + act["description"].ToString();
                    tn_act.SelectedImageIndex = 4;
                    tn_act.ImageIndex = 4;
                    node.Nodes.Add(tn_act);
                }
                CreateTreeViewRecursiveNew(node, dataSource, dataAct, dr["sub_project_id"].ToString());

            }
            if (parentId == "0")
                nodes.Expand();
        }
    }
}
