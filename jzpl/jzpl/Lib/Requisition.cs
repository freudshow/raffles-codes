using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Collections.Generic;


namespace jzpl.Lib
{
    public class Requisition
    {
        public enum STATE
        {
            init,
            confirming,
            released,
            finished,
            cancelled
        }
        private string requisition_id;
        private string matr_seq_no;
        private string matr_seq_line_no;
        private string part_no;
        private string part_description;
        private string require_qty;
        private string issued_qty;
        private string project_id;
        private string project_description;
        private string project_block;
        private string project_system;
        private string place;
        private string place_description;
        private string receiver;
        private string receiver_ic;
        private string receive_date;
        private string receiver_contact;
        private string crane;
        private string recorder;
        private string record_time;
        private string finish_time;
        private string contract;
        private string rowstate;
        private string receipt_dept;
        private string req_group;
        private string work_content;
        private string lack_type;


        public Requisition(string id)
        {
            DataTable dt;
            string sql_ = "select requisition_id," +
                "matr_seq_no," +
                "matr_seq_line_no, " +
                "part_no, " +
                "part_description, " +
                "require_qty, " +
                "issued_qty," +
                "project_id," +
                "project_description, " +
                "project_block," +
                "project_system," +
                "place," +
                "place_description, " +
                "receiver, " +
                "receiver_ic," +
                "to_char(receive_date,'yyyy-mm-dd') receive_date," +
                "receiver_contact," +
                "crane," +
                "recorder," +
                "record_time, " +
                "finish_time," +
                "contract," +
                "rowstate, " +
                "receipt_dept , " +
                "req_group, " +
                "lack_type, " +//ming.li 20130326    
                "work_content " +
                "from jp_requisition where requisition_id='" + id + "'";
            dt = Lib.DBHelper.createDataset(sql_).Tables[0];
            if (dt.Rows.Count > 0)
            {
                requisition_id = dt.Rows[0]["requisition_id"].ToString(); 
                matr_seq_no = dt.Rows[0]["matr_seq_no"].ToString();
                matr_seq_line_no = dt.Rows[0]["matr_seq_line_no"].ToString();
                part_no = dt.Rows[0]["part_no"].ToString();
                part_description = dt.Rows[0]["part_description"].ToString();
                require_qty = dt.Rows[0]["require_qty"].ToString();
                issued_qty = dt.Rows[0]["issued_qty"].ToString();
                project_id = dt.Rows[0]["project_id"].ToString();
                project_description = dt.Rows[0]["project_description"].ToString();
                project_block = dt.Rows[0]["project_block"].ToString();
                project_system = dt.Rows[0]["project_system"].ToString();
                place = dt.Rows[0]["place"].ToString();
                place_description = dt.Rows[0]["place_description"].ToString();
                receiver = dt.Rows[0]["receiver"].ToString();
                receiver_ic = dt.Rows[0]["receiver_ic"].ToString();
                receive_date = dt.Rows[0]["receive_date"].ToString();
                receiver_contact = dt.Rows[0]["receiver_contact"].ToString();
                crane = dt.Rows[0]["crane"].ToString();
                recorder = dt.Rows[0]["recorder"].ToString();
                record_time = dt.Rows[0]["record_time"].ToString();
                finish_time = dt.Rows[0]["finish_time"].ToString();
                contract = dt.Rows[0]["contract"].ToString(); 
                rowstate=dt.Rows[0]["rowstate"].ToString();
                receipt_dept = dt.Rows[0]["receipt_dept"].ToString();
                req_group = dt.Rows[0]["req_group"].ToString();
                work_content = dt.Rows[0]["work_content"].ToString();
                lack_type = dt.Rows[0]["lack_type"].ToString();//ming.li 20130326 
            }
        }


        //public Requisition(string id)
        //{
        //    DataTable dt;
        //    string sql_ = "select demand_id," +
        //        "matr_seq_no," +
        //        "matr_seq_line_no, " +
        //        "part_no, " +
        //        "part_description, " +
        //        "require_qty, " +
        //        "issued_qty," +
        //        "project_id," +
        //        "project_description, " +
        //        "project_block," +
        //        "project_system," +
        //        "place," +
        //        "place_description, " +
        //        "receiver, " +
        //        "receiver_ic," +
        //        "to_char(receive_date,'yyyy-mm-dd') receive_date," +
        //        "receiver_contact," +
        //        "crane," +
        //        "recorder," +
        //        "record_time, " +
        //        "finish_time," +
        //        "contract," +
        //        "rowstate, " +
        //        "receipt_dept , " +
        //        "req_group, " +
        //        "lack_type, " +//ming.li 20130326    
        //        "work_content " +
        //        "from jp_demand where demand_id='" + id + "'";
        //    dt = Lib.DBHelper.createDataset(sql_).Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        requisition_id = dt.Rows[0]["demand_id"].ToString();
        //        matr_seq_no = dt.Rows[0]["matr_seq_no"].ToString();
        //        matr_seq_line_no = dt.Rows[0]["matr_seq_line_no"].ToString();
        //        part_no = dt.Rows[0]["part_no"].ToString();
        //        part_description = dt.Rows[0]["part_description"].ToString();
        //        require_qty = dt.Rows[0]["require_qty"].ToString();
        //        issued_qty = dt.Rows[0]["issued_qty"].ToString();
        //        project_id = dt.Rows[0]["project_id"].ToString();
        //        project_description = dt.Rows[0]["project_description"].ToString();
        //        project_block = dt.Rows[0]["project_block"].ToString();
        //        project_system = dt.Rows[0]["project_system"].ToString();
        //        place = dt.Rows[0]["place"].ToString();
        //        place_description = dt.Rows[0]["place_description"].ToString();
        //        receiver = dt.Rows[0]["receiver"].ToString();
        //        receiver_ic = dt.Rows[0]["receiver_ic"].ToString();
        //        receive_date = dt.Rows[0]["receive_date"].ToString();
        //        receiver_contact = dt.Rows[0]["receiver_contact"].ToString();
        //        crane = dt.Rows[0]["crane"].ToString();
        //        recorder = dt.Rows[0]["recorder"].ToString();
        //        record_time = dt.Rows[0]["record_time"].ToString();
        //        finish_time = dt.Rows[0]["finish_time"].ToString();
        //        contract = dt.Rows[0]["contract"].ToString();
        //        rowstate = dt.Rows[0]["rowstate"].ToString();
        //        receipt_dept = dt.Rows[0]["receipt_dept"].ToString();
        //        req_group = dt.Rows[0]["req_group"].ToString();
        //        work_content = dt.Rows[0]["work_content"].ToString();
        //        lack_type = dt.Rows[0]["lack_type"].ToString();//ming.li 20130326 
        //    }
        //}

        //public static int DeleteRequisitionOfDB(string id)
        //{
        //    //using (OleDbConnection conn = new OleDbConnection(Lib.DBHelper.OleConnectionString))
            //{
            //    if (conn.State != ConnectionState.Open) conn.Open();
            //    OleDbCommand cmd = new OleDbCommand("jp_requisition_api.delete_", conn);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    OleDbTransaction tr = conn.BeginTransaction();
            //    cmd.Transaction = tr;
            //    cmd.Parameters.Add("v_req_id", OleDbType.VarChar, 20).Value = id;
            //    try
            //    {
            //        cmd.ExecuteNonQuery();
            //        tr.Commit();
            //        ReqIDSessionHandler("REMOVE", id);
            //        GVDataBind();
            //    }
            //    catch (OleDbException oex)
            //    {
            //        tr.Rollback();
            //        throw oex;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
        //}

        #region 属性
        /// <summary>
        /// 申请ID
        /// </summary>
        public string REQUISITION_ID
        {
            get { return requisition_id; }
        }
        /// <summary>
        /// 物料顺序号
        /// </summary>
        public string MATR_SEQ_NO
        {
            get { return matr_seq_no; }
        }
        /// <summary>
        /// 定额行号
        /// </summary>
        public string MATR_SEQ_LINE_NO
        {
            get { return matr_seq_line_no; }
        }
        /// <summary>
        /// 零件号
        /// </summary>
        public string PART_NO
        {
            get { return part_no; }
        }
        /// <summary>
        /// 零件描述
        /// </summary>
        public string PART_DESCRIPTION
        {
            get { return part_description; }
        }
        /// <summary>
        /// 需求数量
        /// </summary>
        public string REQUIRE_QTY
        {
            get { return require_qty; }
        }
        /// <summary>
        /// 下发数量
        /// </summary>
        public string ISSUED_QTY
        {
            get { return issued_qty; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string PROJECT_ID
        {
            get { return project_id; }
        }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string PROJECT_DESCRIPTION
        {
            get { return project_description; }
        }
        /// <summary>
        /// 项目分段
        /// </summary>
        public string PROJECT_BLOCK
        {
            get { return project_block; }
        }
        /// <summary>
        /// 项目系统
        /// </summary>
        public string PROJECT_SYSTEM
        {
            get { return project_system; }
        }
        /// <summary>
        /// 接收位置
        /// </summary>
        public string PLACE
        {
            get { return place; }
        }
        /// <summary>
        /// 接收位置描述
        /// </summary>
        public string PLACE_DESCRIPTION
        {
            get { return place_description; }
        }
        /// <summary>
        /// 接收人
        /// </summary>
        public string RECEIVER
        {
            get { return receiver; }
        }
        /// <summary>
        /// 接收人IC
        /// </summary>
        public string RECEIVER_IC
        {
            get { return receiver_ic; }
        }
        /// <summary>
        /// 接收日期
        /// </summary>
        public string RECEIVE_DATE
        {
            get { return receive_date; }
        }
        /// <summary>
        /// 接收人联系电话
        /// </summary>
        public string RECEIVER_CONTACT
        {
            get { return receiver_contact; }
        }
        /// <summary>
        /// 吊装
        /// </summary>
        public string CRANE
        {
            get { return crane; }
        }
        /// <summary>
        /// 记录人员
        /// </summary>
        public string RECORDER
        {
            get { return recorder; }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RECORD_TIME
        {
            get { return record_time; }
        }
        /// <summary>
        /// 完成日期
        /// </summary>
        public string FINISH_TIME
        {
            get { return finish_time; }
        }
        /// <summary>
        /// 零件所在域
        /// </summary>
        public string CONTRACT
        {
            get { return contract; }
        }
        public string ROWSTATE
        {
            get { return rowstate; }
        }
        public string RECEIPT_DEPT
        {
            get { return receipt_dept; }
        }
        public string REQ_GROUP
        {
            get { return req_group; }
        }
        public string WORK_CONTENT
        {
            get { return work_content; }
        }
        public string LACK_TYPE
        {
            get { return lack_type; }
        }
        #endregion
    }

    public class DemandRequisition
    {
        public enum STATE
        {
            init,
            confirming,
            released,
            finished,
            cancelled
        }
        private string requisition_id;
        private string matr_seq_no;
        private string matr_seq_line_no;
        private string part_no;
        private string part_description;
        private string require_qty;
        private string issued_qty;
        private string project_id;
        private string project_description;
        private string project_block;
        private string project_system;
        private string place;
        private string place_description;
        private string receiver;
        private string receiver_ic;
        private string receive_date;
        private string receiver_contact;
        private string crane;
        private string recorder;
        private string record_time;
        private string finish_time;
        private string contract;
        private string rowstate;
        private string receipt_dept;
        private string req_group;
        private string work_content;
        private string lack_type;

        public DemandRequisition(string id)
        {
            DataTable dt;
            string sql_ = "select demand_id," +
                "matr_seq_no," +
                "matr_seq_line_no, " +
                "part_no, " +
                "part_description, " +
                "require_qty, " +
                "issued_qty," +
                "project_id," +
                "project_description, " +
                "project_block," +
                "project_system," +
                "place," +
                "place_description, " +
                "receiver, " +
                "receiver_ic," +
                "to_char(receive_date,'yyyy-mm-dd') receive_date," +
                "receiver_contact," +
                "crane," +
                "recorder," +
                "record_time, " +
                "finish_time," +
                "contract," +
                "rowstate, " +
                "receipt_dept , " +
                "req_group, " +
                "lack_type, " +//ming.li 20130326    
                "work_content " +
                "from jp_demand where demand_id='" + id + "'";
            dt = Lib.DBHelper.createDataset(sql_).Tables[0];
            if (dt.Rows.Count > 0)
            {
                requisition_id = dt.Rows[0]["demand_id"].ToString();
                matr_seq_no = dt.Rows[0]["matr_seq_no"].ToString();
                matr_seq_line_no = dt.Rows[0]["matr_seq_line_no"].ToString();
                part_no = dt.Rows[0]["part_no"].ToString();
                part_description = dt.Rows[0]["part_description"].ToString();
                require_qty = dt.Rows[0]["require_qty"].ToString();
                issued_qty = dt.Rows[0]["issued_qty"].ToString();
                project_id = dt.Rows[0]["project_id"].ToString();
                project_description = dt.Rows[0]["project_description"].ToString();
                project_block = dt.Rows[0]["project_block"].ToString();
                project_system = dt.Rows[0]["project_system"].ToString();
                place = dt.Rows[0]["place"].ToString();
                place_description = dt.Rows[0]["place_description"].ToString();
                receiver = dt.Rows[0]["receiver"].ToString();
                receiver_ic = dt.Rows[0]["receiver_ic"].ToString();
                receive_date = dt.Rows[0]["receive_date"].ToString();
                receiver_contact = dt.Rows[0]["receiver_contact"].ToString();
                crane = dt.Rows[0]["crane"].ToString();
                recorder = dt.Rows[0]["recorder"].ToString();
                record_time = dt.Rows[0]["record_time"].ToString();
                finish_time = dt.Rows[0]["finish_time"].ToString();
                contract = dt.Rows[0]["contract"].ToString();
                rowstate = dt.Rows[0]["rowstate"].ToString();
                receipt_dept = dt.Rows[0]["receipt_dept"].ToString();
                req_group = dt.Rows[0]["req_group"].ToString();
                work_content = dt.Rows[0]["work_content"].ToString();
                lack_type = dt.Rows[0]["lack_type"].ToString();//ming.li 20130326 
            }
        }

        #region 属性
        /// <summary>
        /// 申请ID
        /// </summary>
        public string REQUISITION_ID
        {
            get { return requisition_id; }
        }
        /// <summary>
        /// 物料顺序号
        /// </summary>
        public string MATR_SEQ_NO
        {
            get { return matr_seq_no; }
        }
        /// <summary>
        /// 定额行号
        /// </summary>
        public string MATR_SEQ_LINE_NO
        {
            get { return matr_seq_line_no; }
        }
        /// <summary>
        /// 零件号
        /// </summary>
        public string PART_NO
        {
            get { return part_no; }
        }
        /// <summary>
        /// 零件描述
        /// </summary>
        public string PART_DESCRIPTION
        {
            get { return part_description; }
        }
        /// <summary>
        /// 需求数量
        /// </summary>
        public string REQUIRE_QTY
        {
            get { return require_qty; }
        }
        /// <summary>
        /// 下发数量
        /// </summary>
        public string ISSUED_QTY
        {
            get { return issued_qty; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string PROJECT_ID
        {
            get { return project_id; }
        }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string PROJECT_DESCRIPTION
        {
            get { return project_description; }
        }
        /// <summary>
        /// 项目分段
        /// </summary>
        public string PROJECT_BLOCK
        {
            get { return project_block; }
        }
        /// <summary>
        /// 项目系统
        /// </summary>
        public string PROJECT_SYSTEM
        {
            get { return project_system; }
        }
        /// <summary>
        /// 接收位置
        /// </summary>
        public string PLACE
        {
            get { return place; }
        }
        /// <summary>
        /// 接收位置描述
        /// </summary>
        public string PLACE_DESCRIPTION
        {
            get { return place_description; }
        }
        /// <summary>
        /// 接收人
        /// </summary>
        public string RECEIVER
        {
            get { return receiver; }
        }
        /// <summary>
        /// 接收人IC
        /// </summary>
        public string RECEIVER_IC
        {
            get { return receiver_ic; }
        }
        /// <summary>
        /// 接收日期
        /// </summary>
        public string RECEIVE_DATE
        {
            get { return receive_date; }
        }
        /// <summary>
        /// 接收人联系电话
        /// </summary>
        public string RECEIVER_CONTACT
        {
            get { return receiver_contact; }
        }
        /// <summary>
        /// 吊装
        /// </summary>
        public string CRANE
        {
            get { return crane; }
        }
        /// <summary>
        /// 记录人员
        /// </summary>
        public string RECORDER
        {
            get { return recorder; }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RECORD_TIME
        {
            get { return record_time; }
        }
        /// <summary>
        /// 完成日期
        /// </summary>
        public string FINISH_TIME
        {
            get { return finish_time; }
        }
        /// <summary>
        /// 零件所在域
        /// </summary>
        public string CONTRACT
        {
            get { return contract; }
        }
        public string ROWSTATE
        {
            get { return rowstate; }
        }
        public string RECEIPT_DEPT
        {
            get { return receipt_dept; }
        }
        public string REQ_GROUP
        {
            get { return req_group; }
        }
        public string WORK_CONTENT
        {
            get { return work_content; }
        }
        public string LACK_TYPE
        {
            get { return lack_type; }
        }
        #endregion
    }

    public class Requisitions
    {
        private List<Requisition> requisition_list;
        public Requisitions(string mtr_seq_no, string mtr_line_no)
        {
            
        }
        public List<Requisition> REQUISITION_LIST
        {
            get { return requisition_list; }
        }
    }
    
}
