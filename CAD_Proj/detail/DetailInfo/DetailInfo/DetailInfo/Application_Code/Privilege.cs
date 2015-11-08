using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace DetailInfo
{
    public partial class Privilege
    {




        /// <summary>
        /// 根据权限标识符判断是否存在此权限
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool Exist(string flag)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PRIVILEGE_FLAG FROM PLM.PRIVILEGE_TAB WHERE PRIVILEGE_FLAG=:flag";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "flag", DbType.String, flag);
            object ret = db.ExecuteScalar(cmd);
            return (ret == null || ret == DBNull.Value) ? false : true;
        }

        /// <summary>
        /// 根据权限标识符获得权限信息
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static Privilege Find(string flag)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.PRIVILEGE_TAB WHERE PRIVILEGE_FLAG=:flag";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "flag", DbType.String, flag);
            return Populate(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 根据权限ID获得权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Privilege Find(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.PRIVILEGE_TAB WHERE PRIVILEGE_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return Populate(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 根据权限标识符获得权限ID号
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static int FindIdByFlag(string flag)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PRIVILEGE_ID FROM PLM.PRIVILEGE_TAB WHERE PRIVILEGE_FLAG=:flag";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "flag", DbType.String, flag);
            object id = db.ExecuteScalar(cmd);
            return (id == null || id == DBNull.Value) ? 0 : Convert.ToInt32(id);
        }

        /// <summary>
        /// 从DataReader中填充权限信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Privilege Populate(IDataReader dr)
        {
            return EntityBase<Privilege>.DReaderToEntity(dr);
        }

        public static List<string> FindAllCata()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "SELECT PRIVILEGE_CATA FROM PLM.PRIVILEGE_TAB GROUP BY PRIVILEGE_CATA ORDER BY PRIVILEGE_CATA";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<string> cataList = new List<string>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                    cataList.Add(dr[0].ToString());
                dr.Close();
            }
            return cataList;
        }

        /// <summary>
        /// 查找所有权限信息
        /// </summary>
        /// <returns></returns>
        public static List<Privilege> FindAll()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "SELECT * FROM PLM.PRIVILEGE_TAB ORDER BY PRIVILEGE_ID";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<Privilege>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 查找所有权限信息
        /// </summary>
        /// <returns></returns>
        public static List<Privilege> FindAll(string cata)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "SELECT * FROM PLM.PRIVILEGE_TAB WHERE PRIVILEGE_CATA=:cata ORDER BY PRIVILEGE_ID";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "cata", DbType.String, cata);
            return EntityBase<Privilege>.DReaderToEntityList(db.ExecuteReader(cmd));
        }




    }
}

