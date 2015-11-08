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
    public class Ration
    {
        #region 基本字段信息
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
        private string _dpname;
        /// <summary>
        /// 专业名称
        /// </summary>
        [BindingField]
        public string DISCIPLINE
        {
            get { return _dpname; }
            set { _dpname = value; }
        }
        private string _ecpid;
        /// <summary>
        /// EC项目编号
        /// </summary>
        [BindingField]
        public string ECPROJECTID
        {
            get { return _ecpid; }
            set { _ecpid = value; }
        }
        private string _unit;
        /// <summary>
        /// 零件计量单位
        /// </summary>
        [BindingField]
        public string UNIT
        {
            get { return _unit; }
            set { _unit = value; }
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
        private decimal _issQty;
        /// <summary>
        /// 申请数量
        /// </summary>
        [BindingField]
        public decimal ISSUED_QTY
        {
            get { return _issQty; }
            set { _issQty = value; }
        }
        private DateTime _issDate;
        /// <summary>
        /// 申请日期
        /// </summary>
        [BindingField]
        public DateTime ISSUED_DATE
        {
            get { return _issDate; }
            set { _issDate = value; }
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
        public string IF_INVENTORY
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
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public int RATION_ID
        {
            get { return _reqId; }
            set { _reqId = value; }
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
        /// 区域
        /// </summary>
        [BindingField]
        public string PART_ZONE
        {
            get { return _parentSubPro; }
            set { _parentSubPro = value; }
        }
        private string _subPro;
        /// <summary>
        /// 分项
        /// </summary>
        [BindingField]
        public string PART_FX
        {
            get { return _subPro; }
            set { _subPro = value; }
        }
        private string _activitySeq;
        /// <summary>
        /// 专业
        /// </summary>
        [BindingField]
        public string PART_DISCIPLINE
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
        private string _purpose;
        /// <summary>
        /// 备注
        /// </summary>
        [BindingField]
        public string PURPOSE
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
        private string _requisitonno;
        /// <summary>
        /// 备注
        /// </summary>
        [BindingField]
        public string P_REQUISITION_NO
        {
            get { return _requisitonno; }
            set { _requisitonno = value; }
        }
        private string _blockId;
        /// <summary>
        /// 备注
        /// </summary>
        [BindingField]
        public string BLOCK_ID
        {
            get { return _blockId; }
            set { _blockId = value; }
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
        private string _parttype;
        /// <summary>
        /// 种类
        /// </summary>
        [BindingField]
        public string PART_TYPE
        {
            get { return _parttype; }
            set { _parttype = value; }
        }
        private int _partid;
        /// <summary>
        /// 零件ID
        /// </summary>
        [BindingField]
        public int PARTID
        {
            get { return _partid; }
            set { _partid = value; }
        }
        private string _mssno;
        /// <summary>
        /// ERP定额流水号
        /// </summary>
        [BindingField]
        public string ERP_MEO
        {
            get { return _mssno; }
            set { _mssno = value; }
        }
        #endregion 
        /// <summary>
        /// 返回所有零件采购申请列表
        /// </summary>
        /// <returns></returns>
        public static DataSet QueryPartRationList(string sql)
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet QueryPartRationListERP(string sql)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public static Ration Find(int sequenceNo)
        {
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM plm.MM_PART_RATION_TAB WHERE ration_id=:seqNo";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "seqNo", DbType.Int32, sequenceNo);
            return Populate(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Ration Populate(IDataReader dr)
        {
            return EntityBase<Ration>.DReaderToEntity(dr);
        }
        public int Add()
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("INSERT INTO plm.MM_PART_RATION_TAB(PART_NO,CONTRACT,P_REQUISITION_NO,ISSUED_QTY,ISSUED_DATE,IF_INVENTORY,PURPOSE,BLOCK_ID,REASON_CODE,DESIGN_CODE,REMARK,CREATE_DATE,CREATER,PROJECT_ID,part_zone,part_discipline,part_fx,part_type,unit,PART_NAME) VALUES (:partno,:contract,:requisionNo,:requireqty,:requirDate,:isInventory,:purpose,:blockID,:reasoncode,:designcode,:information,sysdate,:creater,:projectId,:parentProId,:subProId,:actId,:parttype,:unit,:partname)");
            db.AddInParameter(cmd, "partno", DbType.String, PART_NO);
            db.AddInParameter(cmd, "contract", DbType.String, CONTRACT);
            db.AddInParameter(cmd, "requisionNo", DbType.String, P_REQUISITION_NO);
            db.AddInParameter(cmd, "requireqty", DbType.Single, ISSUED_QTY);
            db.AddInParameter(cmd, "requirDate", DbType.Date, ISSUED_DATE);
            db.AddInParameter(cmd, "isInventory", DbType.String, IF_INVENTORY);
            db.AddInParameter(cmd, "purpose", DbType.String, PURPOSE);
            db.AddInParameter(cmd, "blockID", DbType.String, BLOCK_ID);
            db.AddInParameter(cmd, "reasoncode", DbType.String, REASON_CODE);
            db.AddInParameter(cmd, "designcode", DbType.String, DESIGN_CODE);
            db.AddInParameter(cmd, "information", DbType.String, INFORMATION);
            db.AddInParameter(cmd, "creater", DbType.String, CREATER);
            db.AddInParameter(cmd, "projectId", DbType.String, PROJECT_ID);
            db.AddInParameter(cmd, "parentProId", DbType.String, PART_ZONE);
            db.AddInParameter(cmd, "subProId", DbType.String, PART_DISCIPLINE);
            db.AddInParameter(cmd, "actId", DbType.String, PART_FX);
            db.AddInParameter(cmd, "parttype", DbType.String, PART_TYPE);
            db.AddInParameter(cmd, "unit", DbType.String, UNIT);
            db.AddInParameter(cmd, "partname", DbType.String, PART_NAME);
            
            ;
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
            string sql = "DELETE FROM plm.MM_PART_RATION_TAB WHERE RATION_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 更新ERP的定额流水号
        /// </summary>
        /// <param name="id"></param>
        public static int UpdateERPMSSNO(int id,string mssno)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE plm.MM_PART_RATION_TAB set ERP_QUEUENO='"+mssno+"'WHERE RATION_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 更新系统内的定额的状态
        /// </summary>
        /// <param name="id"></param>
        public static int UpdateMSSState(string id,int strstate)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE plm.MM_PART_RATION_TAB set state='" + strstate + "' WHERE RATION_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return db.ExecuteNonQuery(cmd);
        }
        public int Update()
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("update plm.MM_PART_RATION_TAB set DISCIPLINE=:dpname,PART_NO=:partno,CONTRACT=:contract,P_REQUISITION_NO=:reqNo,ISSUED_QTY=:issqty,ISSUED_DATE=:issDate,IF_INVENTORY=:isInv,PURPOSE=:purpose,BLOCK_ID=:BLOCK_ID,REASON_CODE=:reasoncode,DESIGN_CODE=:designcode,REMARK=:information,UPDATE_DATE=sysdate,PROJECT_ID=:projectId,PART_ZONE=:partzone,part_discipline=:partdp,PART_FX= :actId,PART_TYPE=:parttype,unit=:unit,PART_NAME=:partname where RATION_ID=:rationId");
            db.AddInParameter(cmd, "partno", DbType.String, PART_NO);
            db.AddInParameter(cmd, "partname", DbType.String, PART_NAME);
            db.AddInParameter(cmd, "contract", DbType.String, CONTRACT);
            db.AddInParameter(cmd, "reqNo", DbType.String, P_REQUISITION_NO);
            db.AddInParameter(cmd, "issqty", DbType.Single, ISSUED_QTY);
            db.AddInParameter(cmd, "issDate", DbType.Date, ISSUED_DATE);
            db.AddInParameter(cmd, "isInv", DbType.String, IF_INVENTORY);
            db.AddInParameter(cmd, "purpose", DbType.String, PURPOSE);
            db.AddInParameter(cmd, "BLOCK_ID", DbType.String, BLOCK_ID);
            db.AddInParameter(cmd, "reasoncode", DbType.String, REASON_CODE);
            db.AddInParameter(cmd, "designcode", DbType.String, DESIGN_CODE);
            db.AddInParameter(cmd, "information", DbType.String, REMARK);
            db.AddInParameter(cmd, "projectId", DbType.String, PROJECT_ID);
            db.AddInParameter(cmd, "partzone", DbType.String, PART_ZONE);
            db.AddInParameter(cmd, "partdp", DbType.String, PART_DISCIPLINE);
            db.AddInParameter(cmd, "unit", DbType.String, UNIT);
            db.AddInParameter(cmd, "actId", DbType.String, PART_FX);
            db.AddInParameter(cmd, "parttype", DbType.String, PART_TYPE);
            db.AddInParameter(cmd, "rationId", DbType.Int32, RATION_ID);
            return db.ExecuteNonQuery(cmd);
        }
    }
}
