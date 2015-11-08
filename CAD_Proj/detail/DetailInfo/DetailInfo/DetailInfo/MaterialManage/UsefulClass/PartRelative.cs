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
    public class PartRelative
    {
        private string _ifspartno;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string ERP_PART_NO
        {
            get { return _ifspartno; }
            set { _ifspartno = value; }
        }
        private string _staPartno;
        /// <summary>
        /// 标准件号
        /// </summary>
        [BindingField]
        public string STA_PART_NO
        {
            get { return _staPartno; }
            set { _staPartno = value; }
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
        private string _partname;
        /// <summary>
        /// 产品名称
        /// </summary>
        [BindingField]
        public string PART_NAME
        {
            get { return _partname; }
            set { _partname = value; }
        }
        private string _projectid;
        /// <summary>
        /// 项目
        /// </summary>
        [BindingField]
        public string PROJECTID
        {
            get { return _projectid; }
            set { _projectid = value; }
        }
        private int _actSeq;
        /// <summary>
        /// 活动
        /// </summary>
        [BindingField]
        public int ACTIVITYSEQ
        {
            get { return _actSeq; }
            set { _actSeq = value; }
        }
        private string _staIf;
        /// <summary>
        /// 是否标准件
        /// </summary>
        [BindingField]
        public string STA_IF
        {
            get { return _staIf; }
            set { _staIf = value; }
        }
        private string _creator;
        /// <summary>
        /// 创建者
        /// </summary>
        [BindingField]
        public string CREATOR
        {
            get { return _creator; }
            set { _creator = value; }
        }
        public int Add()
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("INSERT INTO plm.MM_IFS_STA_PART_TAB(ERP_PART_NO,STA_PART_NO,PART_NAME,PROJECTID,ACTIVITYSEQ,SITE,STA_IF,CREATOR) VALUES (:erppartno,:staPartno,:partname,:projectid,:actSeq,:site,:staIf,:creator)");
            db.AddInParameter(cmd, "erppartno", DbType.String,ERP_PART_NO);
            db.AddInParameter(cmd, "staPartno", DbType.String, STA_PART_NO);
            db.AddInParameter(cmd, "partname", DbType.String, PART_NAME);
            db.AddInParameter(cmd, "projectid", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "actSeq", DbType.Int32, ACTIVITYSEQ);
            db.AddInParameter(cmd, "site", DbType.String, SITE);
            db.AddInParameter(cmd, "staIf", DbType.String, STA_IF);
            db.AddInParameter(cmd, "creator", DbType.String, CREATOR);
            return db.ExecuteNonQuery(cmd);
        }
        public int Delete()
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("delete plm.MM_IFS_STA_PART_TAB where ERP_PART_NO=:erpPartno and PROJECTID=:proId and ACTIVITYSEQ=:actSeq and SITE=:site");
            db.AddInParameter(cmd, "erpPartno", DbType.String, ERP_PART_NO);
            db.AddInParameter(cmd, "proId", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "actSeq", DbType.Int32, ACTIVITYSEQ);
            db.AddInParameter(cmd, "site", DbType.String, SITE);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 判断是否已经存在对应关系
        /// </summary>
        /// <returns></returns>
        public  bool FindExistRelative()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT 1 FROM plm.MM_IFS_STA_PART_TAB where ERP_PART_NO=:erpPartno and STA_PART_NO=:sta_partno and PROJECTID=:proId and ACTIVITYSEQ=:actSeq and SITE=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "erpPartno", DbType.String, ERP_PART_NO);
            db.AddInParameter(cmd, "sta_partno", DbType.String, STA_PART_NO);
            db.AddInParameter(cmd, "proId", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "actSeq", DbType.Int32, ACTIVITYSEQ);
            db.AddInParameter(cmd, "site", DbType.String, SITE);
            object rname = db.ExecuteScalar(cmd);
            return (rname == null || rname == DBNull.Value) ? false : true;
        }

        /// <summary>
        /// 判断是否已经被合并
        /// </summary>
        /// <returns></returns>
        public bool IFmerged1()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT 1 FROM plm.MM_IFS_STA_PART_TAB where ERP_PART_NO=:erpPartno and PROJECTID=:proId and ACTIVITYSEQ=:actSeq and SITE=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "erpPartno", DbType.String, ERP_PART_NO);
            db.AddInParameter(cmd, "proId", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "actSeq", DbType.Int32, ACTIVITYSEQ);
            db.AddInParameter(cmd, "site", DbType.String, SITE);
            object rname = db.ExecuteScalar(cmd);
            return (rname == null || rname == DBNull.Value) ? false : true;
        }
        /// <summary>
        /// 判断是否标准件
        /// </summary>
        /// <returns></returns>
        public bool IFStandardPart()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT 1 FROM plm.MM_IFS_STA_PART_TAB where STA_PART_NO=:staPartno  and PROJECTID=:proId and ACTIVITYSEQ=:actSeq and SITE=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "staPartno", DbType.String, STA_PART_NO);
            db.AddInParameter(cmd, "proId", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "actSeq", DbType.Int32, ACTIVITYSEQ);
            db.AddInParameter(cmd, "site", DbType.String, SITE);
            object rname = db.ExecuteScalar(cmd);
            return (rname == null || rname == DBNull.Value) ? false : true;
        }
        public static DataSet FindERPPartDataset(string partno, string site,string ProId,string ActSeq)
        {
            // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT ERP_PART_NO,PART_NAME,site FROM plm.MM_IFS_STA_PART_TAB where STA_PART_NO=:staPartno  and PROJECTID=:proId and ACTIVITYSEQ=:actSeq and SITE=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "staPartno", DbType.String, partno);
            db.AddInParameter(cmd, "proId", DbType.String, ProId);
            db.AddInParameter(cmd, "actSeq", DbType.Int32, ActSeq);
            db.AddInParameter(cmd, "site", DbType.String, site);
            return db.ExecuteDataSet(cmd);
        }
        public static string FindRelativeStnPartno(string ErpPartno,string proId,int ActSeq,string site)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "  select STA_PART_NO  from plm.MM_IFS_STA_PART_TAB where ERP_PART_NO=:erpPartno  and PROJECTID=:proId and ACTIVITYSEQ=:actSeq and SITE=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "erpPartno", DbType.String, ErpPartno);
            db.AddInParameter(cmd, "proId", DbType.String, proId);
            db.AddInParameter(cmd, "actSeq", DbType.Int32, ActSeq);
            db.AddInParameter(cmd, "site", DbType.String, site);
            return Convert.ToString( db.ExecuteScalar(cmd));
        }

        public static List<PartRelative> FindRelativeERPPartList(string stnPartno,string projectId,string Site)
        {
            
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM plm.MM_IFS_STA_PART_TAB where STA_PART_NO=:stnPartno and PROJECTID=:proId and SITE=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "stnPartno", DbType.String, stnPartno);
            db.AddInParameter(cmd, "proId", DbType.String, projectId);
            db.AddInParameter(cmd, "site", DbType.String, Site);
            return EntityBase<PartRelative>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        //public static string GetFamilyName(string ID)
        //{
        //    //Database db = DatabaseFactory.CreateDatabase();
        //    OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
        //    DbCommand cmd = db.GetSqlStringCommand("select IFSAPP.INVENTORY_PRODUCT_FAMILY_API.Get_Description(PART_PRODUCT_FAMILY) familyname from IFSAPP.inventory_part where PART_PRODUCT_FAMILY=:id");
        //    db.AddInParameter(cmd, "id", DbType.String, ID);
        //    return Convert.ToString(db.ExecuteScalar(cmd));
        //}
        //public static string GetIsInventory(string site, string PartNo)
        //{
        //    //Database db = DatabaseFactory.CreateDatabase();
        //    OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
        //    DbCommand cmd = db.GetSqlStringCommand("select IFSAPP.Inventory_Part_API.Part_Exist(CONTRACT, PART_NO) isExist from IFSAPP.inventory_part where CONTRACT=:site and PART_NO=:partno");
        //    db.AddInParameter(cmd, "site", DbType.String, site);
        //    db.AddInParameter(cmd, "partno", DbType.String, PartNo);
        //    return Convert.ToString(db.ExecuteScalar(cmd));
        //}
        //public static string GetInventoryqty(string site, string PartNo)
        //{
        //    //Database db = DatabaseFactory.CreateDatabase();
        //    OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
        //    DbCommand cmd = db.GetSqlStringCommand("select IFSAPP.Inventory_Part_In_Stock_API.Get_Inventory_Qty_Onhand(CONTRACT,PART_NO,NULL) invQty  from IFSAPP.inventory_part where CONTRACT=:site and PART_NO=:partno");
        //    db.AddInParameter(cmd, "site", DbType.String, site);
        //    db.AddInParameter(cmd, "partno", DbType.String, PartNo);
        //    return Convert.ToString(db.ExecuteScalar(cmd));
        //}
        ///// <summary>
        ///// 返回所有inventory part列表
        ///// </summary>
        ///// <returns></returns>
        //public static InventoryPart FindInvInfor(string partno, string site)
        //{
        //    //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
        //    OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
        //    string sql = "SELECT PART_NO,DESCRIPTION,PART_PRODUCT_FAMILY,dim_quality,unit_meas FROM IFSAPP.inventory_part where PART_NO = '" + partno + "' and CONTRACT='" + site + "'";
        //    DbCommand cmd = db.GetSqlStringCommand(sql);
        //    return Populate(db.ExecuteReader(cmd));
        //}
        ///// <summary>
        ///// 返回所有inventory part列表
        ///// </summary>
        ///// <returns></s>
        //public static string FindInvPartName(string partno, string site)
        //{
        //    //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
        //    OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
        //    string sql = "SELECT DESCRIPTION FROM IFSAPP.inventory_part where PART_NO = '" + partno + "' and CONTRACT='" + site + "'";
        //    DbCommand cmd = db.GetSqlStringCommand(sql);
        //    return Convert.ToString(db.ExecuteScalar(cmd));
        //}
        ///// <summary>
        ///// 从IDataReader中填充Comment实体
        ///// </summary>
        ///// <param name="dr"></param>
        ///// <returns></returns>
        //public static InventoryPart Populate(IDataReader dr)
        //{
        //    return EntityBase<InventoryPart>.DReaderToEntity(dr);
        //}

    }
}
