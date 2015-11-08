using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace DetailInfo
{
    public partial class DrawingFlowLog
    {
        
       
        /// <summary>
        /// 添加流程日志
        /// </summary>
        /// <returns></returns>
        public int Add(int DrawingID,int FlowStatus,string UserName,string Remark,string Attach)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand("INSERT INTO PLM.DRAWING_FLOWLOG_TAB (DRAWING_ID,OCCURRENCE_TIME,FLOW_STATUS,USERNAME,REMARK,ATTACH) VALUES (:drawingid, :occurrencetime, :flowstat, :username,:remark,:attach)");
            db.AddInParameter(cmd, "drawingid", DbType.Int32, DrawingID);
            db.AddInParameter(cmd, "occurrencetime", DbType.String, DateTime.Now.ToString());
            db.AddInParameter(cmd, "flowstat", DbType.Int32, FlowStatus);
            db.AddInParameter(cmd, "username", DbType.String, UserName);
            db.AddInParameter(cmd, "remark", DbType.String, Remark);
            db.AddInParameter(cmd, "attach", DbType.String, Attach);
            return db.ExecuteNonQuery(cmd);
        }

       
        /// <summary>
        /// 删除流程日志
        /// </summary>
        /// <param name="id"></param>
        public static int Del(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "DELETE FROM PLM.DRAWING_FLOWLOG_TAB WHERE ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        

        
        /// <summary>
        /// 获得指定ID的获取附件的路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<string> GetAttachmentPath(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select t.attach from drawing_flowlog_tab t where id=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            List<string> att = new List<string>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                {
                    att.Add(dr[0].ToString());
                }
                dr.Close();
            }
            return att;
        }
    }
}
