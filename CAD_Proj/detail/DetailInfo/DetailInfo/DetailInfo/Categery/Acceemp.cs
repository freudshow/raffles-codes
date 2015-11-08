using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;

namespace DetailInfo.Categery
{
    public class Acceemp
    {
        /// <summary>
        ///项目名
        /// </summary>
        [BindingField]
        public string Project { get; set; }
        /// <summary>
        /// 小票名
        /// </summary>
        [BindingField]
        public string SpoolName { get; set; }
        /// <summary>
        /// 螺栓型号
        /// </summary>
        [BindingField]
        public string BoltStandard { get; set; }
        /// <summary>
        /// 螺母型号
        /// </summary>
        [BindingField]
        public string NutStandard { get; set; }
        /// <summary>
        /// 螺栓重量
        /// </summary>
        [BindingField]
        public int BoltWeight { get; set; }
        /// <summary>
        /// 螺母重量
        /// </summary>
        [BindingField]
        public int NutWeight { get; set; }
        /// <summary>
        /// 螺栓数量
        /// </summary>
        [BindingField]
        public int BoltNumber { get; set; }
        /// <summary>
        /// 螺母数量
        /// </summary>
        [BindingField]
        public int NutNumber { get; set; }
        /// <summary>
        /// 垫片型号
        /// </summary>
        [BindingField]
        public string GasketStandard { get; set; }
        /// <summary>
        /// 版本标识
        /// </summary>
        [BindingField]
        public string Flag { get; set; }
        /// <summary>
        ///数量
        /// </summary>
        [BindingField]
        public int TotalNum { get; set; }
        /// <summary>
        /// 分段号
        /// </summary>
        [BindingField]
        public string BlockName { get; set; }
        /// <summary>
        /// 图纸号
        /// </summary>
        [BindingField]
        public string DrawingNo { get; set; }
        /// <summary>
        /// 双头螺柱
        /// </summary>
        [BindingField]
        public string DoubleScrewBolt { get; set; }

        /// <summary>
        ///根据图号查询相关列表
        /// </summary>
        /// <param name="drawingno"></param>
        /// <returns></returns>
        public static List<Acceemp> Find(string drawingno, int flag)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = string.Empty;
            if (flag == 0)
                sql = "select * from plm.sp_acceemp t where t.drawingno='" + drawingno + "' and t.flag='Y'";
            else
                sql = "select * from plm.sp_acceemp t where t.flag='Y' and t.spoolname in (select s.spoolname from plm.sp_spool_tab s where s.modifydrawingno='" + drawingno + "' and s.flag='Y')";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<Acceemp>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        public static List<Acceemp> Finding(string block, int flag)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = string.Empty;
            if (flag == 0)
                sql = "select * from plm.sp_acceemp t where t.BLOCKNAME='" + block + "' and t.flag='Y'";
            else
                sql = "select * from plm.sp_acceemp t where t.flag='Y' and t.spoolname in (select s.spoolname from plm.sp_spool_tab s where s.blockno='" + block + "' and s.flag='Y' and MODIFYDRAWINGNO<>null)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<Acceemp>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 调用存储过程获得附件集合
        /// </summary>
        /// <param name="drawingno"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DataSet GetAcceempDS(string drawingno, int flag)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);//获得conn连接  
            conn.Open();
            OracleCommand cmd = new OracleCommand("sp_acceemp", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter pram = new OracleParameter("p_cursor", OracleType.Cursor);
            pram.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pram);

            cmd.Parameters.Add("drawing_no", OracleType.VarChar).Value = drawingno;
            cmd.Parameters["drawing_no"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add("flag", OracleType.Number).Value = flag;
            cmd.Parameters["flag"].Direction = System.Data.ParameterDirection.Input;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
    }
}
