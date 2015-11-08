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
  
    public class PartBasicInfo
    {
        private string _actSeq;
        /// <summary>
        /// 域
        /// </summary>
        [BindingField]
        public string ACTIVITY_SEQ
        {
            get { return _actSeq; }
            set { _actSeq = value; }
        }
        private string _site;
        /// <summary>
        /// 域
        /// </summary>
        [BindingField]
        public string SITE
        {
            get { return _site; }
            set { _site = value; }
        }
        private string _partno;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string PART_NO
        {
            get { return _partno; }
            set { _partno = value; }
        }
        private string _partname;
        /// <summary>
        /// 零件名称
        /// </summary>
        [BindingField]
        public string PART_NAME
        {
            get { return _partname; }
            set { _partname = value; }
        }
        private string _unit;
        /// <summary>
        /// 计量单位
        /// </summary>
        [BindingField]
        public string UNIT
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private string _isexist;
        /// <summary>
        /// 是否有库存
        /// </summary>
        [BindingField]
        public string IS_EXIST
        {
            get { return _isexist; }
            set { _isexist = value; }
        }
        private string _issueInv;
        /// <summary>
        ///是否从仓库中下发
        /// </summary>
        [BindingField]
        public string ISSUE_FROM_INV
        {
            get { return _issueInv; }
            set { _issueInv = value; }
        }
        private int _requestQty;
        /// <summary>
        /// 申请数量
        /// </summary>
        [BindingField]
        public int REQUEST_QTY
        {
            get { return _requestQty; }
            set { _requestQty = value; }
        }
        private int _sum;
        /// <summary>
        /// 累计定额数量
        /// </summary>
        [BindingField]
        public int SUM
        {
            get { return _sum; }
            set { _sum = value; }
        }
           private int  _rest;
        /// <summary>
        /// 余下申请数量
        /// 
        /// </summary>
        [BindingField]
        public int REST
        {
            get { return _rest; }
            set { _rest = value; }
        }
           private DateTime _requestdate;
        /// <summary>
        /// 申请日期
        /// </summary>
        [BindingField]
        public DateTime REQUEST_DATE
        {
            get { return _requestdate; }
            set { _requestdate = value; }
        }
           private string _requistionNo;
        /// <summary>
        /// 申请号
        /// </summary>
        [BindingField]
        public string P_REQUISITION_NO
        {
            get { return _requistionNo; }
            set { _requistionNo = value; }
        }
        private string _req_lineNo;
        /// <summary>
        /// 栏号
        /// </summary>
        [BindingField]
        public string P_REQ_LINE_NO
        {
            get { return _req_lineNo; }
            set { _req_lineNo = value; }
        }
        private string _releaseNo;
        /// <summary>
        /// 下发号      
        /// </summary>
        [BindingField]
        public string P_REQ_RELEASE_NO
        {
            get { return _releaseNo; }
            set { _releaseNo = value; }
        }
        private string _information;
        /// <summary>
        /// 信息
        /// </summary>
        [BindingField]
        public string INFORMATION
        {
            get { return _information; }
            set { _information = value; }
        }
        private string _reasonCode;
        /// <summary>
        /// reasonCode
        /// </summary>
        [BindingField]
        public string REASON_CODE
        {
            get { return _reasonCode; }
            set { _reasonCode = value; }
        }
        private string _reasonDesc;
        /// <summary>
        /// reasonDesc
        /// </summary>
        [BindingField]
        public string REASON_DESC
        {
            get { return _reasonDesc; }
            set { _reasonDesc = value; }
        }
        private string _designCode;
        /// <summary>
        /// DesignCode
        /// </summary>
        [BindingField]
        public string DESIGN_CODE
        {
            get { return _designCode; }
            set { _designCode = value; }
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static PartBasicInfo Populate(IDataReader dr)
        {
            return EntityBase<PartBasicInfo>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 返回所有子项目列表
        /// </summary>
        /// <returns></returns>
        public static List<PartBasicInfo> FindAll(string Pid, string ACT_SEQ)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT PROJECT_ID, ACTIVITY_SEQ, SITE, PART_NO, IFSAPP.PURCHASE_PART_API.GET_DESCRIPTION(SITE, PART_NO) PART_NAME,IFSAPP.INVENTORY_PART_API.GET_UNIT_MEAS(SITE, PART_NO) UNIT, IFSAPP.INVENTORY_PART_API.PART_EXIST(SITE, PART_NO) IS_EXIST,	 ISSUE_FROM_INV, REQUEST_QTY, IFSAPP.PROJ_PROCU_RATION_API.GET_ACCU_RATION_QTY(MATR_SEQ_NO) SUM, REQUIRE_QTY -IFSAPP.PROJ_PROCU_RATION_API.GET_ACCU_RATION_QTY(MATR_SEQ_NO) REST, REQUEST_DATE,	 P_REQUISITION_NO, P_REQ_LINE_NO, P_REQ_RELEASE_NO, INFORMATION, REASON_CODE, IFSAPP.YRS_REQUISITION_REASON_API.GET_DESCRIPTION(REASON_CODE) REASON_DESC, DESIGN_CODE FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID	 AND (ACTIVITY_SEQ =:ACT_SEQ)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "ACT_SEQ", DbType.String, ACT_SEQ);
            return EntityBase<PartBasicInfo>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        public static List<PartBasicInfo> FindSelectResult(string sql)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");           
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<PartBasicInfo>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        public static DataSet FindSelectResultDS(string sql)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }

        public static DataSet PartBasicDs(string Pid, string ACT_SEQ)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT distinct PART_NO FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID	 AND (ACTIVITY_SEQ =:ACT_SEQ)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "ACT_SEQ", DbType.String, ACT_SEQ);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet PartNameDs(string Pid, string ACT_SEQ)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT distinct PART_NO, IFSAPP.PURCHASE_PART_API.GET_DESCRIPTION(SITE, PART_NO) PART_NAME FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID	 AND (ACTIVITY_SEQ =:ACT_SEQ)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "ACT_SEQ", DbType.String, ACT_SEQ);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet UnitDs(string Pid, string ACT_SEQ)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT distinct IFSAPP.INVENTORY_PART_API.GET_UNIT_MEAS(SITE, PART_NO) UNIT FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID	 AND (ACTIVITY_SEQ =:ACT_SEQ)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "ACT_SEQ", DbType.String, ACT_SEQ);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet ApplyDs(string Pid, string ACT_SEQ)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT distinct P_REQUISITION_NO FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID	 AND (ACTIVITY_SEQ =:ACT_SEQ)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "ACT_SEQ", DbType.String, ACT_SEQ);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet ReasonCodeDs(string Pid, string ACT_SEQ)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT distinct REASON_CODE, IFSAPP.YRS_REQUISITION_REASON_API.GET_DESCRIPTION(REASON_CODE) REASON_DESC FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID	 AND (ACTIVITY_SEQ =:ACT_SEQ)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "ACT_SEQ", DbType.String, ACT_SEQ);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 获取ERP中与零件相关的MEO号的集合
        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="PartNo"></param>
        /// <returns></returns>
        public static DataSet RequisionNoDs(string Pid, string PartNo)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT distinct P_REQUISITION_NO FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID	 AND (PART_NO =:partno)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "partno", DbType.String, PartNo);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 检查MEONO在ERP中是否存在
        /// </summary>
        /// <param name="site"></param>
        /// <param name="Pid"></param>
        /// <param name="reqno"></param>
        /// <returns></returns>
        public static string RequisionNoExist(string site,string Pid, string reqno)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT P_REQUISITION_NO FROM IFSAPP.PROJECT_MISC_PROCUREMENT WHERE PROJECT_ID =:PROID and site=:site AND P_REQUISITION_NO =:reqno)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PROID", DbType.String, Pid);
            db.AddInParameter(cmd, "reqno", DbType.String, reqno);
            db.AddInParameter(cmd, "site", DbType.String, site);
            object meono = db.ExecuteScalar(cmd); if (meono == null) return string.Empty;
            return Convert.ToString(meono);
            
        }
    }
}
