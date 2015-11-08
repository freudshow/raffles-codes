using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
//using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;

namespace DetailInfo.Categery
{
    public class PrivilegeNode
    {
        private int _privilegeid;
        /// <summary>
        /// 权限id
        /// </summary>
        [BindingField(FieldName="PRIVILEGE_ID")]
        public int PrivilegeId
        {
            get { return _privilegeid; }
            set { _privilegeid = value; }
        }
        private int _nodeid;
        /// <summary>
        /// 节点id
        /// </summary>
        [BindingField(FieldName = "NODE_ID")]
        public int NodeId
        {
            get { return _nodeid; }
            set { _nodeid = value; }
        }

        public int Add()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "INSERT INTO PLM.PRIVILEGE_NODE_TAB (PRIVILEGE_ID,NODE_ID) VALUES (:privilegeid,:nodeid)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "privilegeid", DbType.Int32, PrivilegeId);
            db.AddInParameter(cmd, "nodeid", DbType.Int32, NodeId);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 获得改节点所有的权限id
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public static List<int> GetPrivilegeIds(int nodeid)
        {
            List<int> privilegeids=new List<int>();
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT PRIVILEGE_ID FROM PRIVILEGE_NODE_TAB WHERE NODE_ID=:nodeid ORDER BY PRIVILEGE_ID";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "nodeid", DbType.Int32, nodeid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                   privilegeids.Add(Convert.ToInt32(dr[0]));
                }
                dr.Close();
            }
            return privilegeids;
        }
        /// <summary>
        /// 节点是否有该权限设置
        /// </summary>
        /// <returns></returns>
        public static bool ExistPrivilege(int privilegeid,int nodeid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT * FROM PLM.PRIVILEGE_NODE_TAB WHERE PRIVILEGE_ID=:privilegeid AND NODE_ID=:nodeid";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "privilegeid", DbType.Int32, privilegeid);
            db.AddInParameter(cmd, "nodeid", DbType.Int32, nodeid);
            object ret = db.ExecuteScalar(cmd);
            if (ret == null || ret == DBNull.Value) return false;
            return true;
        }
    }
}
