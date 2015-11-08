using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace Framework
{
   
    public class ProjectCmbItem
    {
        private string _Value;
        public string Value
        {
            get
            {
                return _Value;
            }
        }
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
        }
        //
        public ProjectCmbItem(string name, string value)
        {
            _Name = name;
            _Value = value;
        }
        public override string ToString()
        {
            return _Name;
        }

        public static void ProjectCmbBind(ComboBox p_cmb_project)
        {
            p_cmb_project.AutoCompleteSource = AutoCompleteSource.ListItems;
            p_cmb_project.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            p_cmb_project.Items.Clear();
            DataSet PartDS = project.FindProDataset();
            DataTable dt = PartDS.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                ProjectCmbItem item = new ProjectCmbItem(row["description"].ToString(), row["project_id"].ToString());
                p_cmb_project.Items.Add(item);
            }
            //ProjectCmbItem itemn = new ProjectCmbItem("COSLProspector 半潜式钻井平台", "YCRO11-256");
            ////cmb_project.SelectedIndex = 7;
            //cmb_project.SelectedItem = itemn;

        }
        public static void ProjectCmbBind(ToolStripComboBox p_cmb_project)
        {
            p_cmb_project.AutoCompleteSource = AutoCompleteSource.ListItems;
            p_cmb_project.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            p_cmb_project.Items.Clear();
            DataSet PartDS = project.FindProDataset();
            DataTable dt = PartDS.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                ProjectCmbItem item = new ProjectCmbItem(row["description"].ToString(), row["project_id"].ToString());
                p_cmb_project.Items.Add(item);
            }
            //ProjectCmbItem itemn = new ProjectCmbItem("COSLProspector 半潜式钻井平台", "YCRO11-256");
            ////cmb_project.SelectedIndex = 7;
            //cmb_project.SelectedItem = itemn;

        }
        /// <summary>
        /// 获取ERP中的申请原因列表
        /// </summary>
        public static void ReasonCmbBind(ComboBox p_cmb_reason)
        {
            p_cmb_reason.AutoCompleteSource = AutoCompleteSource.ListItems;
            p_cmb_reason.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            p_cmb_reason.Items.Clear();
            DataSet PartDS = ReasonCode.FindReasonDataset();
            DataRow row = PartDS.Tables[0].NewRow();
            row[0] = "";
            PartDS.Tables[0].Rows.InsertAt(row, 0);
            p_cmb_reason.DataSource = PartDS.Tables[0].DefaultView;
            p_cmb_reason.DisplayMember = "DESCRIPTION";
            p_cmb_reason.ValueMember = "REASON_CODE";
            p_cmb_reason.SelectedValue = "1001";
        }
        /// <summary>
        /// 获取ERP中的域
        /// </summary>
        public static void SiteCmbBind(ComboBox p_cmb_site)
        {

            p_cmb_site.AutoCompleteSource = AutoCompleteSource.ListItems;
            p_cmb_site.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            p_cmb_site.Items.Clear();
            DataSet PartDS = project.FindSiteDataset();
            DataRow rowdim = PartDS.Tables[0].NewRow();
            rowdim[0] = "";
            PartDS.Tables[0].Rows.InsertAt(rowdim, 0);
            p_cmb_site.DataSource = PartDS.Tables[0].DefaultView;
            p_cmb_site.DisplayMember = "CONTRACT_REF";
            p_cmb_site.ValueMember = "CONTRACT";
            p_cmb_site.SelectedValue = "03";
            


        }
        /// <summary>
        /// 获取ECDMS中的专业列表
        /// </summary>
        public static void BindDiscipline(ComboBox p_cmb_discipline)
        {
            DataSet displist = PartParameter.QueryPartPara("select m_ID,M_cnname from meomss_discipline_tab");
            
            p_cmb_discipline.AutoCompleteSource = AutoCompleteSource.ListItems;
            p_cmb_discipline.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            p_cmb_discipline.Items.Clear();
            DataRow rowdim = displist.Tables[0].NewRow();
            rowdim[1] = "";
            //rowdim[1] = "";
            displist.Tables[0].Rows.InsertAt(rowdim, 0);
            p_cmb_discipline.DataSource = displist.Tables[0];
            p_cmb_discipline.ValueMember = "m_id";
            p_cmb_discipline.DisplayMember = "m_cnname";
            
            
        }
        /// <summary>
        /// 获取ECDMS中的专业列表
        /// </summary>
        public static void BindDisciplineName(ComboBox p_cmb_discipline)
        {
            DataSet displist = PartParameter.QueryPartPara("select m_ID,M_cnname from meomss_discipline_tab");

            p_cmb_discipline.AutoCompleteSource = AutoCompleteSource.ListItems;
            p_cmb_discipline.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            p_cmb_discipline.Items.Clear();
            DataRow rowdim = displist.Tables[0].NewRow();
            rowdim[1] = "";
            //rowdim[1] = "";
            displist.Tables[0].Rows.InsertAt(rowdim, 0);
            p_cmb_discipline.DataSource = displist.Tables[0];
            p_cmb_discipline.ValueMember = "m_cnname";
            p_cmb_discipline.DisplayMember = "m_cnname";


        }
        /// <summary>
        /// 获取分段信息
        /// </summary>
        public static void BlockBind(ComboBox p_cmb_block ,string ecprojectid)
        {

            DataSet blockds = PartParameter.QueryPartPara("select block_id,description from project_block_tab where project_id=" + ecprojectid+ " order by description");
            DataRow rowdim = blockds.Tables[0].NewRow();
            rowdim[0] = 1;
            blockds.Tables[0].Rows.InsertAt(rowdim, 0);
            p_cmb_block.DataSource = blockds.Tables[0].DefaultView;
            p_cmb_block.DisplayMember = "description";
            p_cmb_block.ValueMember = "description";
            p_cmb_block.SelectedValue = XmlOper.getXMLContent("Block");

        }
    } 
}
