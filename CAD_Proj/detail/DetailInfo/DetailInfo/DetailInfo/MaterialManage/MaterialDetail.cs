using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework;
namespace DetailInfo.MaterialManage
{
    public partial class MaterialDetail : Form
    {
        private string project_no, part_no, part_name, site_no,design_code;
        public MaterialDetail(string projectid, string partno, string partdesc, string siteno, string designcode)
        {
            InitializeComponent();
            project_no = projectid;
            part_no=partno;
            part_name=partdesc;
            site_no=siteno;
            design_code = designcode;

        }

        private void MaterialDetail_Load(object sender, EventArgs e)
        {
            string sqlstr = "select t.matr_seq_no 申请流水号,t.site 域,(select description from IFSAPP.SUB_PROJECT qq where qq.project_id= t.project_id and  qq.sub_project_id =(select sub_project_id from IFSAPP.ACTIVITY_SUM_DETAIL p where p.activity_seq = 100086998)) 专业, (select description from IFSAPP.ACTIVITY_SUM_DETAIL pp where pp.activity_seq = t.activity_seq ) 材料种类,t.part_no 零件号,'" + part_name + "' as 零件描述 ,t.p_requisition_no MEO号,t.p_order_no 采购订单号,t.dt_issued 下发日期,t.request_date 需求日期,t.request_qty 需求数量,IFSAPP.PROJ_PROCU_RATION_API.Get_Accu_Ration_Qty(MATR_SEQ_NO) as 已下发数量,t.request_qty -IFSAPP.PROJ_PROCU_RATION_API.Get_Accu_Ration_Qty(MATR_SEQ_NO) as 可用数量,t.user_cre 操作人,t.reason_code 申请原因,t.design_code 范围,t.c_partial_info 范围 from IFSAPP.PROJECT_MISC_PROCUREMENT t where  design_code like '%" + design_code + "%' and  PROJECT_ID = '" + project_no + "' and site = '" + site_no + "' and issue_from_inv = 0 and PART_NO ='" + part_no + "' and (select state from ifsapp.purchase_req_line_part q where q.requisition_no =p_requisition_no and q.part_no=t.part_no) <>'Cancelled'";
            DataSet ds = PartParameter.QueryPartERPInventory(sqlstr);
            dgv_meo.DataSource = ds.Tables[0].DefaultView;
            string sprojectname = project_no.Substring(project_no.Length - 3, 3);
            string sqlstrnew = "select tt.part_no 零件号,tt.part_desc 零件描述,tt.qty_onhand 预留数量,tt.qty_reserved 已用数量,tt.req_dept 预留标识  from ifsapp.yr_inv_on_hand_vw tt WHERE tt.part_no ='"+part_no+"'   and tt.contract = '"+site_no+"'   and  tt.req_dept like 'YL" + sprojectname +"%'";
            DataSet dsnew = PartParameter.QueryPartERPInventory(sqlstrnew);
            dgv_reserved.DataSource = dsnew.Tables[0].DefaultView;

        }
    }
}
