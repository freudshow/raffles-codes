using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.IO;
using System.Windows.Forms;

namespace DetailInfo.Categery
{
    public class CREATEPDFDRAWING
    {
        /// <summary>
        /// 查看合并生成图纸表中是否存在该项
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="drawingno"></param>
        /// <returns></returns>
        public static bool ExistInfo(string pid,string drawingno)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "select count(*) from plm.SP_CREATEPDFDRAWING t  where t.projectid='"+pid+"' and t.drawingno='"+drawingno+"' AND t.FLAG = 'Y'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object ret = db.ExecuteScalar(cmd);
            int num = Convert.ToInt32(ret);
            if (num==0)
                return false;
            return true;
        }
        /// <summary>
        /// 查看是否存在图纸
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="drawingno"></param>
        /// <returns></returns>
        public static bool ExistDrawing(string pid, string drawingno)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "select t.pdfdrawing from plm.SP_CREATEPDFDRAWING t  where t.projectid='" + pid + "' and t.drawingno='" + drawingno + "' and t.FRONTPAGE is not null AND t.FLAG = 'Y'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object ret = db.ExecuteScalar(cmd);
            if (ret==null||ret==DBNull.Value)
                return false;
            return true;
        }
        /// <summary>
        /// 查看是否存在修改通知单
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="drawingno"></param>
        /// <returns></returns>
        public static bool ExistModifyDrawing(string pid, string drawingno)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "select t.modifydrawings from plm.SP_CREATEPDFDRAWING t  where t.projectid='" + pid + "' and t.drawingno='" + drawingno + "' and t.FRONTPAGE is not null AND t.FLAG = 'Y'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object ret = db.ExecuteScalar(cmd);
            if (ret == null || ret == DBNull.Value)
                return false;
            return true;
        }
        /// <summary>
        /// 将生成的材料信息更新到数据库中
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="excelinfo"></param>
        public static void UpdateExcelInfo(string sql,byte[] excelinfo)
        {
            try
            {
                byte[] file = excelinfo;
                using (OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        OracleParameter op = new OracleParameter("dfd", OracleType.Blob);
                        op.Value = file;
                        if (file.Length == 0)
                        {
                            MessageBox.Show("插入信息表不能为空！", "WARNNING", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        else
                        {
                            cmd.Parameters.Add(op);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }

            }
            catch (IOException ee)
            {
                MessageBox.Show(ee.Message.ToString());
                return;
            }
        }
    }
}
