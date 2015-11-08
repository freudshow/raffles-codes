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
    public class Unit
    {
        private string _id;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string UNIT_CODE
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
        public static Unit Find(string id)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT UNIT_CODE FROM IFSAPP.ISO_UNIT WHERE UNIT_CODE=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return Populate(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Unit Populate(IDataReader dr)
        {
            return EntityBase<Unit>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 返回所有Project列表
        /// </summary>
        /// <returns></returns>
        public static List<Unit> FindAll()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT UNIT_CODE  from IFSAPP.ISO_UNIT";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<Unit>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回所有Project列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindUnitDataset()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT UNIT_CODE  from IFSAPP.ISO_UNIT";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
    }
}
