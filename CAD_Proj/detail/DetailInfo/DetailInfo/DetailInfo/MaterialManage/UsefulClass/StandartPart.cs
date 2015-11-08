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
    public class StandartPart
    {
      
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
        private int _typeId;
        /// <summary>
        /// 零件类型
        /// </summary>
        [BindingField]
        public int TYPEID
        {
            get { return _typeId; }
            set { _typeId = value; }
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
            DbCommand cmd = db.GetSqlStringCommand("INSERT INTO plm.MM_STA_PART_TAB(STA_PART_NO,PART_NAME,PROJECTID,TYPEID,SITE,CREATOR) VALUES (:staPartno,:partname,:projectid,:typeid,:site,:creator)");
            
            db.AddInParameter(cmd, "staPartno", DbType.String, STA_PART_NO);
            db.AddInParameter(cmd, "partname", DbType.String, PART_NAME);
            db.AddInParameter(cmd, "projectid", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "typeid", DbType.Int32, TYPEID);
            db.AddInParameter(cmd, "site", DbType.String, SITE);
            
            db.AddInParameter(cmd, "creator", DbType.String, CREATOR);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 判断是否已经存在对应关系
        /// </summary>
        /// <returns></returns>
        public bool FindExistStanPart()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT 1 FROM plm.MM_STA_PART_TAB where  STA_PART_NO=:sta_partno and PROJECTID=:proId  and SITE=:site and TYPEID=:typeId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
            db.AddInParameter(cmd, "sta_partno", DbType.String, STA_PART_NO);
            db.AddInParameter(cmd, "proId", DbType.String, PROJECTID);
            
            db.AddInParameter(cmd, "site", DbType.String, SITE);
            db.AddInParameter(cmd, "typeId", DbType.Int32, TYPEID);
            object rname = db.ExecuteScalar(cmd);
            return (rname == null || rname == DBNull.Value) ? false : true;
        }
        /// <summary>
        /// 返回所有Project列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindStnPartDataset(string ProjectId,string Site)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT typeid, STA_PART_NO, PART_NAME FROM plm.MM_STA_PART_TAB a  where   PROJECTID=:proId  and SITE=:site ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "proId", DbType.String, ProjectId);

            db.AddInParameter(cmd, "site", DbType.String, Site);
            return db.ExecuteDataSet(cmd);
        }
    }
}
