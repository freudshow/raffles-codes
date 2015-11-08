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
    public class SubProject
    {
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
        private string _parent_sub_id;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string parent_sub_project_id
        {
            get { return _parent_sub_id; }
            set { _parent_sub_id = value; }
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
        public static SubProject Find(string id)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT * FROM IFSAPP.SUB_PROJECT WHERE project_id=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return Populate(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static SubProject Populate(IDataReader dr)
        {
            return EntityBase<SubProject>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 返回所有子项目列表
        /// </summary>
        /// <returns></returns>
        public static List<SubProject> FindAll(string id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT *	FROM IFSAPP.SUB_PROJECT T  WHERE T.PROJECT_ID =:id	 AND T.PARENT_SUB_PROJECT_ID IS  NULL ORDER BY t.sub_project_id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return EntityBase<SubProject>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回所有子项目列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindAllSubPro(string id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT t.project_id, t.sub_project_id,nvl(parent_sub_project_id,'0') parent_sub_project_id,t.description	FROM IFSAPP.SUB_PROJECT T  WHERE T.PROJECT_ID =:id	 ORDER BY t.sub_project_id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 返回所有子项目下的子项目列表
        /// </summary>
        /// <returns></returns>
        public static List<SubProject> FindAllSubProjects(string id,string subId)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "	 SELECT * 	FROM IFSAPP.SUB_PROJECT T  WHERE T.PROJECT_ID = :id 	 AND T.PARENT_SUB_PROJECT_ID =:subId ORDER BY t.sub_project_id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            db.AddInParameter(cmd, "subId", DbType.String, subId);
            return EntityBase<SubProject>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回所有子项目下的子项目列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindSub2ProjectsDs(string id, string subId)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "	 SELECT * FROM IFSAPP.SUB_PROJECT T  WHERE T.PROJECT_ID = :id 	 AND T.PARENT_SUB_PROJECT_ID =:subId ORDER BY t.sub_project_id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            db.AddInParameter(cmd, "subId", DbType.String, subId);
            return db.ExecuteDataSet(cmd);
        }


    }

}
