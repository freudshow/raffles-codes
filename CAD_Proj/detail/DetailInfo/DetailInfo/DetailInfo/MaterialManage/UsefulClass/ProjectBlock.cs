using System;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Data.OracleClient;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;
using DreamStu.Common.Log;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework;
namespace DetailInfo
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProjectBlock
    {
        private string _project;
        /// <summary>
        /// 项目编号
        /// </summary>
        [BindingField]
        public string Project_Id
        {
            get { return _project; }
            set { _project = value; }
        }
        private int _block;
        /// <summary>
        /// 分段编号
        /// </summary>
        [BindingField]
        public int Block_Id
        {
            get { return _block; }
            set { _block = value; }
        }

        private string _description;
        /// <summary>
        /// 分段描述
        /// </summary>
        [BindingField]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _location;
        /// <summary>
        /// 分段位置
        /// </summary>
        [BindingField]
        public string LOCATION
        {
            get { return _location; }
            set { _location = value; }
        }

        private decimal? _weight;
        /// <summary>
        ///分段重量
        /// </summary>
        [BindingField]
        public decimal? WEIGHT
        {
            get { return _weight; }
            set { _weight = value; }
        }
        private decimal? _l;
        /// <summary>
        ///分段长
        /// </summary>
        [BindingField]
        public decimal? DIMENSION_L
        {
            get { return _l; }
            set { _l = value; }
        }
        private decimal? _w;
        /// <summary>
        ///分段宽
        /// </summary>
        [BindingField]
        public decimal? DIMENSION_W
        {
            get { return _w; }
            set { _w = value; }
        }
        private decimal? _h;
        /// <summary>
        ///分段高
        /// </summary>
        [BindingField]
        public decimal? DIMENSION_H
        {
            get { return _h; }
            set { _h = value; }
        }

        private decimal? _lcg;
        /// <summary>
        ///X轴向绝对坐标（LCG MM）
        /// </summary>
        [BindingField]
        public decimal? LCG
        {
            get { return _lcg; }
            set { _lcg = value; }
        }
        private decimal? _tcg;
        /// <summary>
        ///Y轴向绝对坐标（TCG MM）
        /// </summary>
        [BindingField]
        public decimal? TCG
        {
            get { return _tcg; }
            set { _tcg = value; }
        }
        private decimal? _vcg;
        /// <summary>
        ///Z轴向绝对坐标（VCG MM）
        /// </summary>
        [BindingField]
        public decimal? VCG
        {
            get { return _vcg; }
            set { _vcg = value; }
        }
        


        /// <summary>
        /// 根据Block编号查找区域信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ProjectBlock Find(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.PROJECT_BLOCK_TAB WHERE Block_Id=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return Populate(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获得Block的名称
        /// </summary>
        /// <param name="id">区域 ID</param>
        /// <returns></returns>
        public static string GetName(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT DESCRIPTION FROM PLM.PROJECT_BLOCK_TAB WHERE BLOCK_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.String, id);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        public static string GetAreaByBlock(string blockname,string projectid,string siteno)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT area_cn FROM PLM.MM_RELATION_TAB WHERE BLOCK_NO=:blockname and project_id =:pid  and site =:csite";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "blockname", DbType.String, blockname);
            db.AddInParameter(cmd, "pid", DbType.String, projectid);
            db.AddInParameter(cmd, "csite", DbType.String, siteno);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 根据blockId列表查找BlockName
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string GetNames(string ids)
        {
            if (ids.Trim() == string.Empty) return string.Empty;
            string blockNames = string.Empty;
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string Sql = "SELECT DESCRIPTION FROM PLM.PROJECT_BLOCK_TAB WHERE BLOCK_ID IN (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(Sql);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                    blockNames += "," + dr[0].ToString();
                dr.Close();
            }
            if (blockNames != string.Empty) blockNames = blockNames.Substring(1);
            return blockNames;
        }

        /// <summary>
        /// 从IDataReader中填充名称实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static ProjectBlock Populate(IDataReader dr)
        {
            return EntityBase<ProjectBlock>.DReaderToEntity(dr);
        }

        /// <summary>
        /// 返回所有名称列表
        /// </summary>
        /// <returns></returns>
        public static List<ProjectBlock> FindAll()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM Project_Block_tab";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<ProjectBlock>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 根据项目ID返回其Block列表
        /// </summary>
        /// <returns></returns>
        public static List<ProjectBlock> FindAll(string  projectid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM PLM.Project_Block_tab WHERE PROJECT_ID=:projectid ORDER BY DESCRIPTION";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "projectid", DbType.String, projectid);
            return EntityBase<ProjectBlock>.DReaderToEntityList(db.ExecuteReader(cmd));
        }

        
        public static int FindDelDrawing(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT DRAWING_ID FROM PLM.project_drawing_tab WHERE BLOCK_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        public static int FindDelDevice(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT DEVICE_ID FROM PLM.device_tab WHERE BLOCK_ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 根据项目ID和分段描述查找BLOCKID
        /// </summary>
        /// <param name="block"></param>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static int FindBlockId(string block, string projectid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT BLOCK_ID FROM PLM.Project_Block_tab WHERE PROJECT_ID=:projectid and Description like '%" + block + "%'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "projectid", DbType.String, projectid);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 判断是否存在block
        /// </summary>
        /// <param name="drawingNo"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static bool Exist(string block, string projectId)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT BLOCK_ID FROM PLM.Project_Block_tab WHERE PROJECT_ID=:projectid and trim(lower(Description)) like '%" + block.ToLower().Trim() + "%'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "projectid", DbType.String, projectId);
            object ret = db.ExecuteScalar(cmd);
            return (ret != null && ret != DBNull.Value);
        }
    }
}
