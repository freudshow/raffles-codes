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
    public class ReasonCode
    {
        private string _id;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string REASON_CODE
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _disc;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string DESCRIPTION
        {
            get { return _disc; }
            set { _disc = value; }
        }
        public static ReasonCode Find(string id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase();
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT * FROM IFSAPP.YRS_REQUISITION_REASON_TAB WHERE REASON_CODE=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return Populate(db.ExecuteReader(cmd));
        }
        public static string FindDesc(string id)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT DESCRIPTION FROM IFSAPP.YRS_REQUISITION_REASON_TAB WHERE REASON_CODE=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static ReasonCode Populate(IDataReader dr)
        {
            return EntityBase<ReasonCode>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 返回所有Project列表
        /// </summary>
        /// <returns></returns>
        public static List<ReasonCode> FindAll()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT * FROM IFSAPP.YRS_REQUISITION_REASON_TAB";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<ReasonCode>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回所有Project列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindReasonDataset()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT * FROM IFSAPP.YRS_REQUISITION_REASON_TAB";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
    }
}
