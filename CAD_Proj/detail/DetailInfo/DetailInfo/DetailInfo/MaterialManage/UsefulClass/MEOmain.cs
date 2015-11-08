using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;
using DreamStu.Common.Log;

namespace Framework
{
    public partial class MEOmain
    {
        #region
        private string _dpid;
        /// <summary>
        /// 专业ID
        /// </summary>
        [BindingField]
        public string DISCIPLINEID
        {
            get { return _dpid; }
            set { _dpid = value; }
        
        }
        private int _zid;
        /// <summary>
        /// MEO_自增ID
        /// </summary>
        [BindingField]
        public int ID
        {
            get { return _zid; }
            set { _zid = value; }
        }
        private string _reqno;
        /// <summary>
        /// 零件编号
        /// </summary>
        [BindingField]
        public string REQUIRE_NO
        {
            get { return _reqno; }
            set { _reqno = value; }
        }
        private string _ecpid;
        /// <summary>
        /// ECPROJECTID
        /// </summary>
        [BindingField]
        public string ECPROJECTID
        {
            get { return _ecpid; }
            set { _ecpid = value; }
        }
        private string _partypeid;
        /// <summary>
        /// 零件大类ID
        /// </summary>
        [BindingField]
        public string PARTTYPE_ID
        {
            get { return _partypeid; }
            set { _partypeid = value; }
        }
        private int _meoid;
        /// <summary>
        /// MEO自增 ID
        /// </summary>
        [BindingField]
        public int REQUIRE_ID
        {
            get { return _meoid; }
            set { _meoid = value; }
        }
        private string _site;
        /// <summary>
        /// 域
        /// </summary>
        [BindingField]
        public string CONTRACT
        {
            get { return _site; }
            set { _site = value; }
        }
        
        
        private string _information;
        [BindingField]
        public string INFORMATION
        {
            get { return _information; }
            set { _information = value; }
        }
        private string _isInventory;
        /// <summary>
        /// 是否有库存
        /// </summary>
        [BindingField]
        public string IS_INVENTORY
        {
            get { return _isInventory; }
            set { _isInventory = value; }
        }
        private DateTime _createDate;
        [BindingField]
        public DateTime CREATE_DATE
        {
            get { return _createDate; }
            set { _createDate = value; }
        }
        private DateTime _updateDate;
        /// <summary>
        /// 更新者
        /// </summary>
        [BindingField]
        public DateTime UPDATE_DATE
        {
            get { return _updateDate; }
            set { _updateDate = value; }
        }
        private string _creater;
        /// <summary>
        /// 创建者
        /// </summary>
        [BindingField]
        public string CREATER
        {
            get { return _creater; }
            set { _creater = value; }
        }
        private int _reqId;
        
        private string _projectId;
        /// <summary>
        /// 项目
        /// </summary>
        [BindingField]
        public string PROJECT_ID
        {
            get { return _projectId; }
            set { _projectId = value; }
        }
        private string _systemid;
        /// <summary>
        /// 分系统
        /// </summary>
        [BindingField]
        public string SYSTEM_ID
        {
            get { return _systemid; }
            set { _systemid = value; }
        }
        
        private string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [BindingField]
        public string REMARK
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private string _updatername;
        /// <summary>
        /// 更新人
        /// </summary>
        [BindingField]
        public string UPDATER
        {
            get { return _updatername; }
            set { _updatername = value; }
        }
        private string _appflag;
        /// <summary>
        /// 审核人
        /// </summary>
        [BindingField]
        public string APPROVER
        {
            get { return _appflag; }
            set { _appflag = value; }
        }
        private string _erpdiscip;
        /// <summary>
        /// ERP中的专业
        /// </summary>
        [BindingField]
        public string ERP_DISCIPLINE
        {
            get { return _erpdiscip; }
            set { _erpdiscip = value; }
        }
        private string _erptype;
        /// <summary>
        /// ERP中的专业
        /// </summary>
        [BindingField]
        public string ERP_PARTTYPE
        {
            get { return _erptype; }
            set { _erptype = value; }
        }
        private string _mstate;
        /// <summary>
        /// MEO状态
        /// </summary>
        [BindingField]
        public string STATE
        {
            get { return _mstate; }
            set { _mstate = value; }
        }
        /// <summary>
        /// 图纸流程的状态
        /// </summary>
        public enum MEOFlowStatus
        {
            /// <summary>
            /// 初始状态
            /// </summary>
            INITIAL = 0,
            /// <summary>
            /// 已计划
            /// </summary>
            PLANNED = 1,
            /// <summary>
            /// 已完成但还未提交审核
            /// </summary>
            FINISHED = 2,
            /// <summary>
            /// 审核未通过，退回。
            /// </summary>
            REJECTED = 3,
            /// <summary>
            /// 审核通过
            /// </summary>
            APPROVED = 4,
            /// <summary>
            /// 已下发
            /// </summary>
            ISSUED = 5,
            /// <summary>
            /// 审核中
            /// </summary>
            APPROVING = 6,
            /// <summary>
            /// 送审船东中
            /// </summary>
            OWNERAPPROVING = 7,
            /// <summary>
            /// 船东审核通过
            /// </summary>
            OWNERAPPROVED = 8,
            /// <summary>
            /// 船东审核退回
            /// </summary>
            OWNERREJECTED = 9,
            /// <summary>
            /// 送审船级社中
            /// </summary>
            CLASSAPPROVING = 10,
            /// <summary>
            /// 船级社审核通过
            /// </summary>
            CLASSAPPROVED = 11,
            /// <summary>
            /// 船级社退回
            /// </summary>
            CLASSREJECTED = 12,
            /// <summary>
            /// 采购确认待分配
            /// </summary>
            PURCHASECONFIRM = 111,
            /// <summary>
            /// 采购退回
            /// </summary>
            PURCHASEREJECT = 112,
            /// <summary>
            /// 下发采购
            /// </summary>
            ISSUEDTOPURCHASE = 211,
            /// <summary>
            /// 技术撤回
            /// </summary>
            TAKEBACKPURCHASE = 212,
            /// <summary>
            /// 采购已分配
            /// </summary>
            PURCHASEASSIGNED = 311,
        }
        #endregion
        /// <summary>
        /// 新增MEO从表
        /// </summary>
        /// <returns></returns>
        public int REQUIRE_Add()
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("INSERT INTO plm.MM_PART_REQUIRE_TAB(DISCIPLINEID,ECPROJECTID,REQUIRE_NO,CONTRACT,INFORMATION,IS_INVENTORY,CREATE_DATE,CREATER,PROJECT_ID,SYSTEM_ID,PARTTYPE_ID,REMARK,ERP_PARTTYPE,ERP_DISCIPLINE,state) VALUES (:discpid,:ECprojectid,:requireno,:contract,:information,:isinventory,sysdate,:creater,:projectId,:sysId,:parttypeid,:remark,:ERPtypeid,:ERPDiscip,1)");
            db.AddInParameter(cmd, "requireno", DbType.String, REQUIRE_NO);
            db.AddInParameter(cmd, "contract", DbType.String, CONTRACT);
            db.AddInParameter(cmd, "information", DbType.String, INFORMATION);
            db.AddInParameter(cmd, "isinventory", DbType.String, IS_INVENTORY);
            db.AddInParameter(cmd, "creater", DbType.String, CREATER);
            //db.AddInParameter(cmd, "createrole", DbType.String, "MaterialApp");
            db.AddInParameter(cmd, "projectId", DbType.String, PROJECT_ID);
            db.AddInParameter(cmd, "sysId", DbType.String, SYSTEM_ID);
            db.AddInParameter(cmd, "remark", DbType.String, REMARK);
            db.AddInParameter(cmd, "parttypeid", DbType.String, PARTTYPE_ID);
            db.AddInParameter(cmd, "ECprojectid", DbType.String, ECPROJECTID);
            db.AddInParameter(cmd, "discpid", DbType.String, DISCIPLINEID);
            db.AddInParameter(cmd, "ERPtypeid", DbType.String, ERP_PARTTYPE);
            db.AddInParameter(cmd, "ERPDiscip", DbType.String, ERP_DISCIPLINE);
            int requireId = 0;
            int rowsAffected = db.ExecuteNonQuery(cmd);
            if (rowsAffected > 0)
            {
                DbCommand cmdSeq = db.GetSqlStringCommand("SELECT PLM.MM_REQUIRE_SEQ.CURRVAL FROM DUAL");
                requireId = Convert.ToInt32(db.ExecuteScalar(cmdSeq));
            }
            return requireId;
        }
        /// <summary>
        /// 更新MEO主表
        /// </summary>
        /// <returns></returns>
        public int REQUIRE_Update()
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("UPDATE plm.MM_PART_REQUIRE_TAB set DISCIPLINEID=:discpid,CONTRACT=:contract,INFORMATION=:information,UPDATE_DATE=:updatedate,UPDATER=:updater,REMARK=:remark where require_id=:requireid");
            db.AddInParameter(cmd, "requireid", DbType.Int32, REQUIRE_ID);
            db.AddInParameter(cmd, "contract", DbType.String, CONTRACT);
            db.AddInParameter(cmd, "information", DbType.String, INFORMATION);
            db.AddInParameter(cmd, "updater", DbType.String, CREATER);
            db.AddInParameter(cmd, "updatedate", DbType.Date, UPDATE_DATE);
            db.AddInParameter(cmd, "remark", DbType.String, REMARK);
            db.AddInParameter(cmd, "discpid", DbType.String, DISCIPLINEID);
            int requireId = db.ExecuteNonQuery(cmd);
            
            return requireId;
        }
        /// <summary>
        /// 删除需求单据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Del(int id)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "DELETE FROM plm.MM_PART_REQUIRELINE_TAB WHERE REQUIRE_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 审核反审核MEO单据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static int ApproveMEO(int id,string flag)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE plm.MM_PART_REQUIRE_TAB SET STATE='"+flag+"' WHERE require_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除MEO从表中的记录
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public  int MEODelete(int seqNo)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("update PLM.MM_PART_REQUIRELINE_TAB t set t.deleteflag='Y' where REQUIRE_ID=:seqNo");
            db.AddInParameter(cmd, "seqNo", DbType.Int32, seqNo);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据ID查询MEO信息
        /// </summary>
        /// <param name="sequenceNo"></param>
        /// <returns></returns>
        public static MEOmain Find(int sequenceNo)
        {
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM plm.MM_PART_REQUIRE_TAB WHERE REQUIRE_ID=:seqNo";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "seqNo", DbType.Int32, sequenceNo);

            return Populate(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static MEOmain Populate(IDataReader dr)
        {
            return EntityBase<MEOmain>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 根据零件ID和项目ID查找申请总量
        /// </summary>
        /// <param name="partid"></param>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static decimal GetMEOqty(int partid, string projectid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT meo_sum_qty FROM plm.mm_part_requiresum_view WHERE ID=" + partid + " and ecprojectid=" + projectid;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object pdsumqty = db.ExecuteScalar(cmd);
            return (pdsumqty == null || pdsumqty == DBNull.Value) ? Convert.ToDecimal(0) : Convert.ToDecimal(pdsumqty);

        }
        /// <summary>
        /// 根据申请单号和项目ID查找申请号
        /// </summary>
        /// <param name="meoid"></param>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static string GetMEONO(string meoid,string projectid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT require_no FROM plm.mm_part_require_tab WHERE REQUIRE_ID=" + meoid + " and ecprojectid=" + projectid;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object pdsumqty = db.ExecuteScalar(cmd);
            return (pdsumqty == null || pdsumqty == DBNull.Value) ? "" : Convert.ToString(pdsumqty);

        }
    }
}
