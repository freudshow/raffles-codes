
using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;
using DreamStu.Common;
using DreamStu.Common.Log;

namespace Framework
{
    public partial class MEOMSS_discipline_new
    {
        private int _oid;
        /// <summary>
        /// ID
        /// </summary>
        [BindingField]
        public int M_ID
        {
            get { return _oid; }
            set { _oid = value; }
        }
        private string _lname;
        /// <summary>
        /// 中文名称
        /// </summary>
        [BindingField]
        public string M_CNNAME
        {
            get { return _lname; }
            set { _lname = value; }
        }
        private string _enname;
        /// <summary>
        /// 英文简称
        /// </summary>
        [BindingField]
        public string M_ENNAME
        {
            get { return _enname; }
            set { _enname = value; }
        }
        private string _gttouser;
        /// <summary>
        /// 专业对应人员
        /// </summary>
        [BindingField]
        public string M_PERSON
        {
            get { return _gttouser; }
            set { _gttouser = value; }
        }

        public static List<MEOMSS_discipline_new> FindAll(string sql)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<MEOMSS_discipline_new>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取MEOMSS的专业英文简称
        /// </summary>
        /// <param name="dpid"></param>
        /// <returns></returns>
        public static string GetDPENname(int dpid)
        {
            string sql = "select m_enname from MEOMSS_discipline_tab t where  t.M_ID=:dpid";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "dpid", DbType.Int32, dpid);
            object pe = db.ExecuteScalar(cmd);
            if (pe == null || pe == DBNull.Value) return string.Empty;
            return Convert.ToString(pe);
        }
        /// <summary>
        /// 获取MEOMSS的专业中英文简称
        /// </summary>
        /// <param name="dpid"></param>
        /// <returns></returns>
        public static string GetDPname(int dpid)
        {
            string sql = "select m_cnname||'--'||m_enname from MEOMSS_discipline_tab t where  t.M_ID=:dpid";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "dpid", DbType.Int32, dpid);
            object pe = db.ExecuteScalar(cmd);
            if (pe == null || pe == DBNull.Value) return string.Empty;
            return Convert.ToString(pe);
        }
        /// <summary>
        /// 获取Discipline的NA状态
        /// </summary>
        /// <param name="dpid,did"></param>
        /// <returns></returns>
        public static string GetDPNAState(int dpid, int did)
        {
            string sql = "select NA_FLAG from PACKAGE_DOC_DISCIPLINE_TAB t where  t.doc_id=:did and t.DISCIPLINE_ID=:dpid";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "dpid", DbType.Int32, dpid);
            db.AddInParameter(cmd, "did", DbType.Int32, did);
            object pe = db.ExecuteScalar(cmd);
            if (pe == null || pe == DBNull.Value) return string.Empty;
            return Convert.ToString(pe);
        }
        /// <summary>
        /// 通过对应人获取专业ID
        /// </summary>
        /// <param name="dperson"></param>
        /// <returns></returns>
        public static int GetDPID(string dperson)
        {
            string sql = "select M_ID from MEOMSS_discipline_tab t where  t.M_PERSON=:dperson";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "dperson", DbType.String, dperson);
            object pe = db.ExecuteScalar(cmd);
            if (pe == null || pe == DBNull.Value) return 0;
            return Convert.ToInt16(pe);
        }
        /// <summary>
        /// 根据项目和专业获取MEOMSS的当前流水号
        /// </summary>
        /// <param name="dpid"></param>
        /// <returns></returns>
        public static string GetManualNo(int dpid, int pid)
        {
            string sql = "select o_id from project_discipline_oid t where t.P_ID=:pid and t.D_ID=:dpid";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "dpid", DbType.Int32, dpid);
            db.AddInParameter(cmd, "pid", DbType.Int32, pid);
            object pe = db.ExecuteScalar(cmd);
            if (pe == null || pe == DBNull.Value) return string.Empty;
            return Convert.ToString(pe);
        }
        /// <summary>
        /// 根据项目和专业更新MEOMSS的当前流水号
        /// </summary>
        /// <param name="dpid"></param>
        /// <returns></returns>
        public static int UPManualNo(int dpid, int pid)
        {
            string sql = "update project_discipline_oid t set t.o_id=t.o_id+1 where t.P_ID=:pid and t.D_ID=:dpid";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "dpid", DbType.Int32, dpid);
            db.AddInParameter(cmd, "pid", DbType.Int32, pid);
            return db.ExecuteNonQuery(cmd);

        }
        /// <summary>
        /// 更新MEOMSS的关联单据ERP ID
        /// </summary>
        /// <param name="MEOMSSid"></param><param name="ERPid"></param>
        /// <returns></returns>
        public static int UPERPID(int MEOMSSid, int ERPid)
        {
            string sql = "update project_drawing_tab t set t.related_drawing=:ERPid where t.drawing_id=:MEOMSSid";
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "MEOMSSid", DbType.Int32, MEOMSSid);
            db.AddInParameter(cmd, "ERPid", DbType.Int32, ERPid);
            return db.ExecuteNonQuery(cmd);

        }
       

    }
}

