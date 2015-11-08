using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace jzpl.Lib
{
    public class PkgRequisition
    {
        private string requisition_id;
        private string package_no;
        private string package_name;
        private string part_no;
        private string part_name_e;
        private string part_spec;
        private decimal require_qty;
        private decimal released_qty;
        private decimal issued_qty;
        private string project_id;
        private string project_block;
        private string project_system;
        private string place_id;
        private string place_name;
        private string receiver;
        private string receiver_ic;
        private string receiver_contract;
        private string receipt_date;
        private string receipt_date_str;
        private string req_group;
        private string req_group_name;
        private string receipt_dept;
        private string receipt_dept_name;
        private string crance;
        private string recorder;
        private string record_time;
        private string record_time_str;
        private string released_time;
        private string released_time_str;
        private string finished_time;
        private string finished_time_str;
        private string rowstate;
        private string rowstate_zh;
        private string lack_msg;
        private string rowversion;
        private decimal finished_qty;
        private string issued_time;
        private string issued_time_str;
        private string objid;
        private string psflag;
        private string work_content;

        



        public PkgRequisition(string req_id)
        {
            DataTable dt;
            string sql_ = string.Format("select * from jp_pkg_requisition_v where requisition_id='{0}'",req_id);
            dt = Lib.DBHelper.createDataset(sql_).Tables[0];
            if (dt.Rows.Count > 0)
            {
                requisition_id = dt.Rows[0]["requisition_id"].ToString();
                package_no = dt.Rows[0]["package_no"].ToString();
                package_name = dt.Rows[0]["package_name"].ToString();
                part_no = dt.Rows[0]["part_no"].ToString();
                part_name_e = dt.Rows[0]["part_name_e"].ToString();
                require_qty = Misc.DBStrToNumber(dt.Rows[0]["require_qty"].ToString());
                released_qty = Misc.DBStrToNumber(dt.Rows[0]["released_qty"].ToString());
                issued_qty = Misc.DBStrToNumber(dt.Rows[0]["issued_qty"].ToString());
                project_id = dt.Rows[0]["project_id"].ToString();
                project_block = dt.Rows[0]["project_block"].ToString();
                project_system = dt.Rows[0]["project_system"].ToString();
                place_id = dt.Rows[0]["place_id"].ToString();
                place_name = dt.Rows[0]["place_name"].ToString();
                receiver = dt.Rows[0]["receiver"].ToString();
                receiver_ic = dt.Rows[0]["receiver_ic"].ToString();
                receiver_contract = dt.Rows[0]["receiver_contract"].ToString();
                receipt_date = dt.Rows[0]["receipt_date"].ToString();
                receipt_date_str = dt.Rows[0]["receipt_date_str"].ToString();
                req_group = dt.Rows[0]["req_group"].ToString();
                req_group_name = dt.Rows[0]["req_group_name"].ToString();
                receipt_dept = dt.Rows[0]["receipt_dept"].ToString();
                receipt_dept_name = dt.Rows[0]["receipt_dept_name"].ToString();
                crance = dt.Rows[0]["crance"].ToString();
                recorder = dt.Rows[0]["recorder"].ToString();
                record_time = dt.Rows[0]["record_time"].ToString();
                record_time_str = dt.Rows[0]["record_time_str"].ToString();
                released_time = dt.Rows[0]["released_time"].ToString();
                released_time_str = dt.Rows[0]["released_time_str"].ToString();
                finished_time = dt.Rows[0]["finished_time"].ToString();
                finished_time_str = dt.Rows[0]["finished_time_str"].ToString();
                rowstate = dt.Rows[0]["rowstate"].ToString();
                rowstate_zh = dt.Rows[0]["rowstate_zh"].ToString();
                lack_msg = dt.Rows[0]["lack_msg"].ToString();
                rowversion = dt.Rows[0]["rowversion"].ToString();
                finished_qty = Misc.DBStrToNumber(dt.Rows[0]["finished_qty"].ToString());
                issued_time = dt.Rows[0]["issued_time"].ToString();
                issued_time_str = dt.Rows[0]["issued_time_str"].ToString();
                objid = dt.Rows[0]["objid"].ToString();
                psflag = dt.Rows[0]["psflag"].ToString();
                work_content = dt.Rows[0]["work_content"].ToString();
            }
        }

        public string RequisitionId { get { return requisition_id; } }
        public string PackageNo { get { return package_no; } }
        public string PackageName { get { return package_name; } }
        public string PartNo { get { return part_no; } }
        public string PartNameE { get { return part_name_e; } }
        public decimal  RequireQty { get { return require_qty; } }
        public decimal  ReleasedQty { get { return released_qty; } }
        public decimal  IssuedQty { get { return issued_qty; } }
        public string ProjectId { get { return project_id; } }
        public string ProjectBlock { get { return project_block; } }
        public string ProjectSystem { get { return project_system; } }
        public string PlaceId { get { return place_id; } }
        public string Receiver { get { return receiver; } }
        public string ReceiverIc { get { return receiver_ic; } }
        public string ReceiverContract { get { return receiver_contract; } }
        public string ReceiptDate { get { return receipt_date; } }
        public string ReceiptDateStr { get { return receipt_date_str; } }
        public string ReqGroup { get { return req_group; } }
        public string ReqGroupName { get { return req_group_name; } }
        public string ReceiptDept { get { return receipt_dept; } }
        public string ReceiptDeptName { get { return receipt_dept_name; } }
        public string Crance { get { return crance; } }
        public string Recorder { get { return recorder; } }
        public string RecordTime { get { return record_time; } }
        public string RecordTimeStr { get { return record_time_str; } }
        public string ReleasedTime { get { return released_time; } }
        public string ReleasedTimeStr { get { return released_time_str; } }
        public string FinishedTime { get { return finished_time; } }
        public string FinishedTimeStr { get { return finished_time_str; } }
        public string RowState { get { return rowstate; } }
        public string RowStateZh { get { return rowstate_zh; } }
        public string LackMsg { get { return lack_msg; } }
        public string Rowversion { get { return rowversion; } }
        public decimal  FinishedQty { get { return finished_qty; } }
        public string IssuedTime { get { return issued_time; } }
        public string IssuedTimeStr { get { return issued_time_str; } }
        public string ObjId { get { return objid; } }
        public string Psflag { get { return psflag; } }
        public string WorkContent { get { return work_content; } }
    }
}
