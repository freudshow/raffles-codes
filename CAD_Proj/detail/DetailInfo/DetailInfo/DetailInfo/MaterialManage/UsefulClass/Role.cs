using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace Framework
{
    public partial class Role
    {
        

        /// <summary>
        /// 根据角色名判断是否存在此角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool Exist(string roleName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "SELECT ROLENAME FROM PLM.ROLE_TAB WHERE ROLENAME=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            object rname = db.ExecuteScalar(cmd);
            return (rname == null || rname == DBNull.Value) ? false : true;
        }

        /// <summary>
        /// 判断角色是否可用
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool Available(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT STATE FROM PLM.ROLE_TAB WHERE ROLENAME=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            object state = db.ExecuteScalar(cmd);
            if (state == null || state == DBNull.Value) return false;
            return state.ToString().ToUpper() == RoleState.NORMAL.ToString();
        }

        /// <summary>
        /// 获得角色状态
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static RoleState GetState(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT STATE FROM PLM.ROLE_TAB WHERE LOWER(ROLENAME)=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName.ToLower());
            object s = db.ExecuteScalar(cmd);
            if (s == null || s == DBNull.Value) return RoleState.LOCKED;
            if (string.IsNullOrEmpty(s.ToString())) return RoleState.LOCKED;
            return (RoleState)Enum.Parse(typeof(RoleState), s.ToString());
        }

        /// <summary>
        /// 更改角色的状态
        /// </summary>
        /// <returns></returns>
        public static int UpdateState(string roleName, RoleState us)
        {
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE PLM.ROLE_TAB SET STATE=:state WHERE LOWER(ROLENAME)=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "state", DbType.String, us.ToString());
            db.AddInParameter(cmd, "rolename", DbType.String, roleName.ToLower());
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据角色名获得角色
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Role Find(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.ROLE_TAB WHERE ROLENAME=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            return Populate(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 根据角色名获得人员名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<Role> FindName(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.userinrole_tab WHERE ROLENAME=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            return EntityBase<Role>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 从DataReader中填充角色实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Role Populate(IDataReader dr)
        {
            return EntityBase<Role>.DReaderToEntity(dr);
        }

        /// <summary>
        /// 查找所有角色
        /// </summary>
        /// <returns></returns>
        public static List<Role> FindAll()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.ROLE_TAB ORDER BY ROLENAME";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<Role>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 根据用户名查找此用户所属的角色
        /// </summary>
        /// <returns></returns>
        public static List<Role> FindAll(string userName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT r.* FROM PLM.ROLE_TAB r, PLM.USERINROLE_TAB u WHERE r.ROLENAME=u.ROLENAME AND u.USERNAME=:username";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "username", DbType.String, userName);
            return EntityBase<Role>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        

        /// <summary>
        /// 添加角色继承
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="inheritRoleName"></param>
        /// <returns></returns>
        public static void AddInherit(string roleName, string[] inheritRoleName)
        {
            string sql = "INSERT INTO PLM.ROLEINHERIT_TAB (ROLENAME, INHERITNAME) VALUES (:rolename, :inheritname)";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            foreach (string inheritName in inheritRoleName)
            {
                if (inheritName == string.Empty) continue;
                if (cmd.Parameters.Contains("inheritname")) cmd.Parameters.RemoveAt("inheritname");
                db.AddInParameter(cmd, "inheritname", DbType.String, inheritName);
                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 移除角色继承
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static int RemoveInherit(string roleName)
        {
            string sql = "DELETE FROM PLM.ROLEINHERIT_TAB WHERE ROLENAME=:rolename";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据角色名获得其继承角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<string> FindInheritRole(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT INHERITNAME FROM PLM.ROLEINHERIT_TAB WHERE ROLENAME=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            List<string> inheritRoleList = new List<string>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                    inheritRoleList.Add(dr[0].ToString());
                dr.Close();
            }
            return inheritRoleList;
        }

        /// <summary>
        /// 根据角色名获得其可以继承的角色集合（已排序其本身及继承自它的角色）
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<Role> FindAvailableInheritRole(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.ROLE_TAB WHERE ROLENAME<>:rolename AND ROLENAME NOT IN (SELECT DISTINCT ROLENAME FROM PLM.ROLEINHERIT_TAB WHERE INHERITNAME=:rolename)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            return EntityBase<Role>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 添加角色的权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="privilegeId">权限</param>
        /// <returns></returns>
        public static int AddPrivilege(string roleName, int privilegeId)
        {
            return AddPrivilege(roleName, privilegeId, "ALL");
        }

        /// <summary>
        /// 添加角色的权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="privilegeId">权限ID</param>
        /// <param name="projectIDs">项目ID集合</param>
        /// <returns></returns>
        public static int AddPrivilege(string roleName, int privilegeId, string projectIDs)
        {
            string sql = "INSERT INTO PLM.ROLEINPRIVILEGE_TAB (ROLENAME, PRIVILEGE_ID, PROJECT_ID) VALUES (:rolename, :privilegeid, :projectids)";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            db.AddInParameter(cmd, "privilegeid", DbType.Int32, privilegeId);
            db.AddInParameter(cmd, "projectids", DbType.String, projectIDs);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 对角色移除权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static int RemovePrivilege(string roleName)
        {
            string sql = "DELETE FROM PLM.ROLEINPRIVILEGE_TAB WHERE ROLENAME=:rolename";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据角色名称及权限标识ID判断是否具有该权限
        /// </summary>
        /// <param name="privlegeId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool HavingPrivilege(string roleName, int privlegeId)
        {
            if (!Available(roleName)) return false;
            //Self Privlege Check
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PRIVILEGE_ID FROM PLM.ROLEINPRIVILEGE_TAB WHERE ROLENAME=:rolename AND PRIVILEGE_ID=:privilegeid";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            db.AddInParameter(cmd, "privilegeid", DbType.Int32, privlegeId);
            object ret = db.ExecuteScalar(cmd);
            if (ret != null && ret != DBNull.Value) return true;

            //Inherit Role Privilege Check
            //bool hasPrivilege = false;
            foreach (string inheritRole in FindInheritRole(roleName))
            {
                if (HavingPrivilege(inheritRole, privlegeId)) return true;
            }

            return false;
        }

        /// <summary>
        /// 根据角色名称、权限标识符及项目ID判断是否具有该权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="privlegeId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static bool HavingPrivilege(string roleName, int privlegeId, int projectId)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PROJECT_ID FROM PLM.ROLEINPRIVILEGE_TAB WHERE ROLENAME=:rolename AND PRIVILEGE_ID=:privilegeid";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            db.AddInParameter(cmd, "privilegeid", DbType.Int32, privlegeId);
            object projectIDS = db.ExecuteScalar(cmd);

            if (projectIDS != null)
            {
                if (projectIDS.ToString().ToLower().Contains("all")) return true;

                string[] pidArray = projectIDS.ToString().Split(',');
                if (DreamStu.Common.Util.IsArrayContainStr(pidArray, projectId.ToString())) return true;
            }

            //Inherit Role Privilege Check
            //bool hasPrivilege = false;
            foreach (string inheritRole in FindInheritRole(roleName))
            {
                if (HavingPrivilege(inheritRole, privlegeId, projectId)) return true;
            }

            return false;
        }

        /// <summary>
        /// 根据角色名称、权限标识符及项目ID判断是否具有该权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="privlegeId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static bool HavingPrivilege(string roleName, int privlegeId, string andSql)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT ASQL FROM PLM.ROLEINPRIVILEGE_TAB WHERE ROLENAME=:rolename AND PRIVILEGE_ID=:privilegeid";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            db.AddInParameter(cmd, "privilegeid", DbType.Int32, privlegeId);
            object asql = db.ExecuteScalar(cmd);

            if (asql != null)
            {
                if (asql.ToString() == "ALL") return true;

                string finalSql = string.Format("{0}{1}", asql, string.IsNullOrEmpty(andSql) ? string.Empty : (" AND " + andSql));
                DbCommand finalCmd = db.GetSqlStringCommand(finalSql);
                object c = db.ExecuteScalar(finalCmd);
                if (Convert.ToInt32(c) > 0) return true;
            }

            //Inherit Role Privilege Check
            //bool hasPrivilege = false;
            foreach (string inheritRole in FindInheritRole(roleName))
            {
                if (HavingPrivilege(inheritRole, privlegeId, andSql)) return true;
            }

            return false;
        }

        /// <summary>
        /// 根据角色和图纸编号获取是否允许访问标识
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="drawingId"></param>
        /// <returns></returns>
        public static string DrawingAllowedFlag(string roleName, int drawingId)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT ALLOW_FLAG FROM PLM.ROLEINDRAWING_TAB WHERE ROLENAME=:rolename AND DRAWING_ID=:drawingid";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            db.AddInParameter(cmd, "drawingid", DbType.Int32, drawingId);
            object allowFlag = db.ExecuteScalar(cmd);

            if (allowFlag == DBNull.Value || allowFlag == null) return string.Empty;
            return allowFlag.ToString();
        }

        /// <summary>
        /// 根据角色名获得其权限ID:Project Id集合
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<string> FindPrivilege(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PRIVILEGE_ID,PROJECT_ID FROM PLM.ROLEINPRIVILEGE_TAB WHERE ROLENAME=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            List<string> pidList = new List<string>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                    pidList.Add(string.Format("{0}:{1}", dr[0], dr[1]));
                dr.Close();
            }
            return pidList;
        }

        /// <summary>
        /// 根据角色名称获得此角色权限标识
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static string FindFlagsByRoleName(string roleName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PRIVILEGE_FLAGS FROM PLM.ROLE_TAB WHERE ROLENAME=:rolename";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "rolename", DbType.String, roleName);
            object flags = db.ExecuteScalar(cmd);
            return (flags == null || flags == DBNull.Value) ? string.Empty : flags.ToString();
        }

        
    }
}
