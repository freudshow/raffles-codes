using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
namespace DetailInfo.Categery
{
    public class TreeNodes
    {
        private int _id;
        /// <summary>
        /// 自增编号
        /// </summary>
        [BindingField]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _text;
        /// <summary>
        ///节点名称 
        /// </summary>
        [BindingField]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private string _name;
        /// <summary>
        /// 节点标识
        /// </summary>
        [BindingField]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private int _imageindex;
        /// <summary>
        /// 节点图片id
        /// </summary>
        [BindingField]
        public int ImageIndex
        {
            get { return _imageindex; }
            set { _imageindex = value; }
        }
        private int _selectedimageindex;
        /// <summary>
        /// 节点选中后图片id
        /// </summary>
        [BindingField]
        public int SelectedImageIndex
        {
            get { return _selectedimageindex; }
            set { _selectedimageindex = value; }
        }
        private string _flag;
        /// <summary>
        /// 是否有权限设置
        /// </summary>
        [BindingField]
        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        private int _parentid;
        /// <summary>
        /// 节点图片id
        /// </summary>
        [BindingField(FieldName="PARENT_ID")]
        public int Parentid
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        private int _parentindex;
        /// <summary>
        ///子节点在父节点中的索引号
        /// </summary>
        [BindingField(FieldName="PARENT_INDEX")]
        public int ParentIndex
        {
            get { return _parentindex; }
            set { _parentindex = value; }
        }
        /// <summary>
        /// 获得该节点的权限设置标志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNodeFlag(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT FLAG FROM TREENODES_TAB WHERE ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            object ret = db.ExecuteScalar(cmd);
            return ret.ToString();
        }
        /// <summary>
        /// 获得该节点的权限设置标志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetParentid(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT PARENT_ID FROM TREENODES_TAB WHERE ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            object ret = db.ExecuteScalar(cmd);
            return Convert.ToInt32(ret);
        }
        public static TreeNodes Find(int id)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT * FROM TREENODES_TAB WHERE ID=:id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            return EntityBase<TreeNodes>.DReaderToEntity(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回所有节点列表
        /// </summary>
        /// <returns></returns>
        public static List<TreeNodes> FindAll()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT * FROM PLM.TREENODES_TAB ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return EntityBase<TreeNodes>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 获得该节点所有的子节点
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public static  DataSet GetSubNodes(int nodeid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT PARENT_INDEX,TEXT FROM PLM.TREENODES_TAB WHERE PARENT_ID=" + nodeid + " ORDER BY PARENT_INDEX";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// parentIndex依次加1
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="beforenode"></param>
        /// <returns></returns>
        public static int UpdateParentIndexAdd(int nodeid,int beforenode)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE (SELECT PARENT_INDEX FROM PLM.TREENODES_TAB S WHERE S.PARENT_ID=" + nodeid + ")T SET T.PARENT_INDEX=T.PARENT_INDEX+1 WHERE T.PARENT_INDEX>=" + beforenode;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// parentIndex依次减1
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="beforenode"></param>
        /// <returns></returns>
        public static int UpdateParentIndexDel(int nodeid, int beforenode)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE (SELECT PARENT_INDEX FROM PLM.TREENODES_TAB S WHERE S.PARENT_ID=" + nodeid + ")T SET T.PARENT_INDEX=T.PARENT_INDEX-1 WHERE T.PARENT_INDEX>" + beforenode;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// parentIndex依次加1
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="beforenode"></param>
        /// <returns></returns>
        public static int UpdateParentIndexAdd(int nodeid, int beforenode, int dropindex)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE (SELECT PARENT_INDEX FROM PLM.TREENODES_TAB S WHERE S.PARENT_ID=" + nodeid + ")T SET T.PARENT_INDEX=T.PARENT_INDEX+1 WHERE T.PARENT_INDEX<" + beforenode + " AND T.PARENT_INDEX>=" + dropindex;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// parentIndex依次减1
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="beforenode"></param>
        /// <returns></returns>
        public static int UpdateParentIndexDel(int nodeid, int beforenode, int dropindex)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE (SELECT PARENT_INDEX FROM PLM.TREENODES_TAB S WHERE S.PARENT_ID=" + nodeid + ")T SET T.PARENT_INDEX=T.PARENT_INDEX-1 WHERE T.PARENT_INDEX<" + dropindex + " AND T.PARENT_INDEX>=" + beforenode;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 更新parentIndex值
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int UpdateParentIndex(int nodeid,int num)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE PLM.TREENODES_TAB SET PARENT_INDEX=" + num + " WHERE ID=" + nodeid;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 更新父节点及parentIndex值
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int UpdateParentAndParentIndex(int nodeid, int parentid, int num)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "UPDATE PLM.TREENODES_TAB SET PARENT_ID=" + parentid + ",PARENT_INDEX=" + num + " WHERE ID=" + nodeid;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
    }
}
