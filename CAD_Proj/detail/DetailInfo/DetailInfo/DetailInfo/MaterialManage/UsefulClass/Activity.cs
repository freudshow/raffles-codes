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
    public class Activity
    {
        private string _Activityseq;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string activity_seq
        {
            get { return _Activityseq; }
            set { _Activityseq = value; }
        }
        private string _Activityno;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string activity_no
        {
            get { return _Activityno; }
            set { _Activityno = value; }
        }
        private string _id;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string project_id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _sub_id;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string sub_project_id
        {
            get { return _sub_id; }
            set { _sub_id = value; }
        }
        
        private string _disc;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string description
        {
            get { return _disc; }
            set { _disc = value; }
        }
        public static Activity Find(string id)
        {
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT * FROM IFSAPP.ACTIVITY WHERE activity_seq=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return Populate(db.ExecuteReader(cmd));
        }
        public static string FindName(string id)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT description FROM IFSAPP.ACTIVITY WHERE activity_seq=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Activity Populate(IDataReader dr)
        {
            return EntityBase<Activity>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 返回所有子项目列表
        /// </summary>
        /// <returns></returns>
        public static List<Activity> FindAll(string id,string SubPro)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT *	FROM IFSAPP.ACTIVITY T  WHERE T.PROJECT_ID =:id	 AND T.sub_project_id=:subPro ORDER BY t.activity_no";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            db.AddInParameter(cmd, "subPro", DbType.String, SubPro);
            return EntityBase<Activity>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回所有子项目列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindActivityDs(string id)
        {
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT activity_seq,description,sub_project_id,activity_no FROM IFSAPP.ACTIVITY T  WHERE T.PROJECT_ID =:id	ORDER BY t.activity_seq";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            
            return db.ExecuteDataSet(cmd);
        }

    }
}
