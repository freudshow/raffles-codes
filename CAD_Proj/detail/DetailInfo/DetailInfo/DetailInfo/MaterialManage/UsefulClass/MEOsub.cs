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
    public class MEOsub
    {
        #region 基础数据
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
        private string _partno;
        /// <summary>
        /// 零件编号
        /// </summary>
        [BindingField]
        public string PART_NO
        {
            get { return _partno; }
            set { _partno = value; }
        }
        private int _partid;
        /// <summary>
        /// 零件ID
        /// </summary>
        [BindingField]
        public int PART_ID
        {
            get { return _partid; }
            set { _partid = value; }
        }
        private int _meoid;
        /// <summary>
        /// MEO ID
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
        private decimal _reqQty;
        /// <summary>
        /// 申请数量
        /// </summary>
        [BindingField]
        public decimal REQUIRE_QTY
        {
            get { return _reqQty; }
            set { _reqQty = value; }
        }
        private DateTime _reqDate;
        /// <summary>
        /// 申请日期
        /// </summary>
        [BindingField]
        public DateTime REQUIRE_DATE
        {
            get { return _reqDate; }
            set { _reqDate = value; }
        }
        private string _reasoncode;
        /// <summary>
        /// 原因代码
        /// </summary>
        [BindingField]
        public string REASON_CODE
        {
            get { return _reasoncode; }
            set { _reasoncode = value; }
        }
        private string _designCode;
        /// <summary>
        /// 设计图纸
        /// </summary>
        [BindingField]
        public string DESIGN_CODE
        {
            get { return _designCode; }
            set { _designCode = value; }
        }
        private string _unitmeas;
        [BindingField]
        public string UNIT_MEAS
        {
            get { return _unitmeas; }
            set { _unitmeas = value; }
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
        private string _parentSubPro;
        /// <summary>
        /// 分系统
        /// </summary>
        [BindingField]
        public string PARENT_SUB_PROJECT_ID
        {
            get { return _parentSubPro; }
            set { _parentSubPro = value; }
        }
        private string _subPro;
        /// <summary>
        /// 子系统
        /// </summary>
        [BindingField]
        public string SUB_PROJECT_ID
        {
            get { return _subPro; }
            set { _subPro = value; }
        }
        private string _activitySeq;
        /// <summary>
        /// 专业编号
        /// </summary>
        [BindingField]
        public string ACTIVITY_SEQ
        {
            get { return _activitySeq; }
            set { _activitySeq = value; }
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
        private string _partname;
        /// <summary>
        /// 备注
        /// </summary>
        [BindingField]
        public string PART_NAME
        {
            get { return _partname; }
            set { _partname = value; }
        }
        private string _meonoerp;
        /// <summary>
        /// mroerpno
        /// </summary>
        [BindingField]
        public string MEO_ERP
        {
            get { return _meonoerp; }
            set { _meonoerp = value; }
        }
       
        #endregion
        /// <summary>
        /// 根据ID查找MEO从表信息
        /// </summary>
        /// <param name="sequenceNo"></param>
        /// <returns></returns>
        
        public static List<MEOsub> FindMEOList(string sequenceNo)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM mm_part_REQUIRELINE_tab t WHERE t.deleteflag='N' and t.require_id= " + sequenceNo;
            DbCommand cmd = db.GetSqlStringCommand(sql);

            return EntityBase<MEOsub>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static MEOsub Populate(IDataReader dr)
        {
            return EntityBase<MEOsub>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 返回所有零件采购申请列表
        /// </summary>
        /// <returns></returns>
        public static DataSet QueryPartMiscProcList(string sql)
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet QueryPartMiscProcListEPR(string sql)
        {
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 新增MEO从表
        /// </summary>
        /// <returns></returns>
        public int REQUIRELINE_Add()
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("INSERT INTO plm.MM_PART_REQUIRELINE_TAB(MEO_ERP,PART_ID,PART_NO,CONTRACT,REQUIRE_QTY,REQUIRE_DATE,REASON_CODE,DESIGN_CODE,UNIT_MEAS,IS_INVENTORY,CREATE_DATE,CREATER,REQUIRE_ID,PROJECT_ID,REMARK,PART_NAME) VALUES (:meoerp,:part_id,:partno,:contract,:requireqty,:requirDate,:reasoncode,:designcode,:information,:isinventory,sysdate,:creater,:parentProId,:projectId,:remark,:partname)");
            db.AddInParameter(cmd, "partno", DbType.String, PART_NO);
            db.AddInParameter(cmd, "contract", DbType.String, CONTRACT);
            db.AddInParameter(cmd, "requireqty", DbType.Decimal, REQUIRE_QTY);
            db.AddInParameter(cmd, "requirDate", DbType.Date, REQUIRE_DATE);
            db.AddInParameter(cmd, "reasoncode", DbType.String, REASON_CODE);
            db.AddInParameter(cmd, "designcode", DbType.String, DESIGN_CODE);
            db.AddInParameter(cmd, "information", DbType.String, UNIT_MEAS);
            db.AddInParameter(cmd, "isinventory", DbType.String, IS_INVENTORY);
            db.AddInParameter(cmd, "creater", DbType.String, CREATER);
            db.AddInParameter(cmd, "projectId", DbType.String, PROJECT_ID);
            db.AddInParameter(cmd, "parentProId", DbType.Int32, REQUIRE_ID);
            db.AddInParameter(cmd, "remark", DbType.String, REMARK);
            db.AddInParameter(cmd, "partname", DbType.String, PART_NAME);
            db.AddInParameter(cmd, "meoerp", DbType.String, MEO_ERP);
            db.AddInParameter(cmd, "part_id", DbType.Int32, PART_ID);
            //db.AddInParameter(cmd, "createrole", DbType.String, "MaterialApp");
            return db.ExecuteNonQuery(cmd);
            
        }
       
        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="id"></param>
        public static int Del(int id)
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "DELETE FROM plm.MM_PART_REQUIRELINE_TAB WHERE REQUIRE_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        public int Update()
        {
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("update plm.MM_PART_REQUIRE_TAB set PART_NO=:partno,CONTRACT=:contract,REQUIRE_QTY=:requireqty,REQUIRE_DATE=:requirDate,REASON_CODE=:reasoncode,DESIGN_CODE=:designcode,INFORMATION=:information,IS_INVENTORY=:isinventory,UPDATE_DATE=sysdate,PROJECT_ID=:projectId,PARENT_SUB_PROJECT_ID=:parentProId,SUB_PROJECT_ID=:subProId,ACTIVITY_SEQ= :actId,REMARK=:remark,PART_NAME=:partname where REQUIRE_ID=:requireId");
            db.AddInParameter(cmd, "partno", DbType.String, PART_NO);
            db.AddInParameter(cmd, "requireId", DbType.Int32, REQUIRE_ID);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 从采购申请表中查询某零件是否已经存在多个MEO NO
        /// </summary>
        /// <returns></returns>
        public static int meomssCount(string sql,string DBname)
        {
            OracleDatabase db = new OracleDatabase(DBname);
          //  Database db = DatabaseFactory.CreateDatabase(DBname);            
           // string sql = "SELECT COUNT(P_REQUISITION_NO) FROM IFSAPP.PROJECT_MISC_PROCUREMENT misc where P_REQUISITION_NO is not null and SITE=:site and misc.PART_NO=:partno";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            //db.AddInParameter(cmd, "site", DbType.String, site);
           // db.AddInParameter(cmd, "partno", DbType.String, Partno);
            object rname = db.ExecuteScalar(cmd);

            return (rname==null||rname==DBNull.Value)? 0: Convert.ToInt32(db.ExecuteScalar(cmd));

        }
        /// <summary>
        /// 从采购申请表中查询某零件是否存在MEO NO
        /// </summary>
        /// <returns></returns>
        public static int meomssExistERP(string meono,string partno)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT 1 FROM IFSAPP.PROJECT_MISC_PROCUREMENT misc where P_REQUISITION_NO =:meono and part_no ='"+partno+"'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            //db.AddInParameter(cmd, "site", DbType.String, site);
            db.AddInParameter(cmd, "meono", DbType.String, meono);
            object rname = db.ExecuteScalar(cmd);

            return (rname == null || rname == DBNull.Value) ? 0 : Convert.ToInt32(db.ExecuteScalar(cmd));

        }
        /// <summary>
        /// 从采购申请表中查询某零件唯一的MEO NO
        /// </summary>
        /// <returns></returns>
        public static string meoNo(string sql,string DBname)
        {
            string meoNo = string.Empty;
            OracleDatabase db = new OracleDatabase(DBname);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            IDataReader rname = db.ExecuteReader(cmd);
            while (rname.Read())
            {
                meoNo =meoNo+","+ rname[0].ToString();
            }
            return meoNo;

        }
        public static string meoDate(string sql,string DBname)
        {
            OracleDatabase db = new OracleDatabase(DBname);
            //Database db = DatabaseFactory.CreateDatabase(DBname);
            //string sql = "SELECT P_REQUISITION_NO FROM IFSAPP.PROJECT_MISC_PROCUREMENT misc where SITE=:site and misc.PART_NO=:partno";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            // db.AddInParameter(cmd, "site", DbType.String, site);
            //db.AddInParameter(cmd, "partno", DbType.String, Partno);
            object rname = db.ExecuteScalar(cmd);
            return (rname==null||rname==DBNull.Value)? string.Empty: Convert.ToDateTime(db.ExecuteScalar(cmd)).ToString("yyyy-MM-dd");

        }
        /// <summary>
        /// 从采购申请表中查询某零件的MEO的总数量
        /// </summary>
        /// <returns></returns>
        public static decimal meomssQty(string sql, string DBname)
        {
            OracleDatabase db = new OracleDatabase(DBname);
            //Database db = DatabaseFactory.CreateDatabase(DBname);            
            //string sql = "SELECT sum(REQUEST_QTY) FROM IFSAPP.PROJECT_MISC_PROCUREMENT misc where P_REQUISITION_NO is not null and SITE=:site and misc.PART_NO=:partno";
            DbCommand cmd = db.GetSqlStringCommand(sql);
           // db.AddInParameter(cmd, "site", DbType.String, site);
           // db.AddInParameter(cmd, "partno", DbType.String, Partno);
            object rname = db.ExecuteScalar(cmd);
            return (rname == null || rname == DBNull.Value) ? 0 : Convert.ToDecimal(rname);
        }
        // <summary>
        /// 根据ID查询MEO信息
        /// </summary>
        /// <param name="sequenceNo"></param>
        /// <returns></returns>
        public static MEOsub Find(int sequenceNo,string partid)
        {
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM plm.MM_PART_REQUIRELINE_TAB WHERE REQUIRE_ID=:seqNo and deleteflag='N' and part_id="+partid;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "seqNo", DbType.Int32, sequenceNo);

            return Populate(db.ExecuteReader(cmd));
        }
        ///// <summary>
        ///// 从定额表中查询某零件的MSS的总数量
        ///// </summary>
        ///// <returns></returns>
        //public static float MssQty(string sql)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("ifsConnection");
        //    // OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
        //    //string sql = "select  RATION_NO,  RATION_QTY,  NOTE,  DATE_CREATED,  CREATED_BY,   DATE_MODIFIED,  MODIFIED_BY, IFSAPP.PROJECT_SITE_API.Get_Default_Site(PROJECT_ID),  PURCH_PART_NO,  IFSAPP.PURCHASE_PART_API.Get_Description(IFSAPP.PROJECT_SITE_API.Get_Default_Site(PROJECT_ID),  PURCH_PART_NO), IFSAPP.PURCHASE_PART_API.Get_Default_Buy_Unit_Meas(IFSAPP.PROJECT_SITE_API.Get_Default_Site(PROJECT_ID),PURCH_PART_NO), PURCH_REQ_NO, PROJECT_ID, ACTIVITY_SEQ,REASON_CODE,IFSAPP.YRS_REQUISITION_REASON_API.Get_Description(REASON_CODE),       DESIGN_CODE  from IFSAPP.PROJ_PROCU_RATION where  IFSAPP.PROJECT_SITE_API.Get_Default_Site(PROJECT_ID)=:site and PROJECT_ID = :projectId and PURCH_PART_NO =:partno";
        //    DbCommand cmd = db.GetSqlStringCommand(sql);
        //   // db.AddInParameter(cmd, "site", DbType.String, site);
        //   // db.AddInParameter(cmd, "projectId", DbType.String, Project);
        //   // db.AddInParameter(cmd, "partno", DbType.String, Partno);
        //    return Convert.ToSingle(db.ExecuteScalar(cmd));
        //}
    }
}
