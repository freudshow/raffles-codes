using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;

namespace jzpl.Lib
{
    public class DBHelper
    {
        /// <summary>
        /// 获取Web.config中的数据库连接字符串
        /// </summary>
        /// 
        public static readonly string dbStr = "didev";
        public static readonly string oledbStr = "oledidev";
        //public static readonly string dbStr = "prod";
        //public static readonly string oledbStr = "oleprod";
        public static string ConnectionString
        {
            get
            {
                string _connectionString = ConfigurationManager.ConnectionStrings[dbStr].ConnectionString;               
                return _connectionString;
            }
        }
        /// <summary>
        /// 获取Web.config中的数据库连接字符串
        /// </summary>
        public static string OleConnectionString
        {
            get
            {    
                string _connectionString = ConfigurationManager.ConnectionStrings[oledbStr].ConnectionString;  
                return _connectionString;
            }
        }
        #region 对象数组转换成dataset
        /// <summary>
        /// 创建分等级显示的数据表
        /// </summary>
        /// <param name="sqlStr">sqlstr必须为：节点、节点描述、父节点</param>
        /// <returns></returns>
        public static DataTable createDataTable(string sqlStr)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SysOrganizationID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Name", System.Type.GetType("System.String"));
            dt.Columns.Add("FatherSysOrganizationID", System.Type.GetType("System.Int32"));
            //yroframe.ManPower.MpClass mp = new yroframe.ManPower.MpClass();
            //DBHelper db = new DBHelper();
            DataView view = createGridView(sqlStr);
            for (int i = 0; i < view.Count; i++)
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = Convert.ToInt32(view[i][0].ToString());
                NewRow[1] = view[i][1].ToString();
                NewRow[2] = Convert.ToInt32(view[i][2].ToString());
                dt.Rows.Add(NewRow);
            }
            return dt;
        }
        #endregion
        //绑定父节点
        /// <summary>
        /// 分级绑定DropDownList
        /// </summary>
        /// <param name="drplist">DropDownList</param>
        /// <param name="sqlstr">SQL语句</param>
        public void BindFather(DropDownList drplist, string sqlstr)
        {
            //drplist.Items.Add(new ListItem("请选择", "0"));
            DataTable dt = createDataTable(sqlstr);

            DataRow[] drs = dt.Select("FatherSysOrganizationID= " + 0);//父节点第一个对应的父节点为0
            foreach (DataRow dr in drs)
            {
                string classid = dr["SysOrganizationID"].ToString();
                string classname = dr["Name"].ToString();
                //顶级分类显示形式
                classname = "" + classname;

                drplist.Items.Add(new ListItem(classname, classid));
                int sonparentid = int.Parse(classid);
                string blank = "　---";
                //递归子分类方法
                BindNode(sonparentid, dt, blank, drplist);
            }
            drplist.DataTextField = "Name";
            drplist.DataValueField = "SysOrganizationID";
            drplist.DataBind();

        }
        //绑定子分类
        /// <summary>
        /// 分级绑定子集数据
        /// </summary>
        /// <param name="parentid">父ID</param>
        /// <param name="dt">数据表</param>
        /// <param name="blank">分隔符</param>
        /// <param name="drplist">DropDownList</param>
        public void BindNode(int parentid, DataTable dt, string blank, DropDownList drplist)
        {
            DataRow[] drs = dt.Select("FatherSysOrganizationID= " + parentid);

            foreach (DataRow dr in drs)
            {
                string classid = dr["SysOrganizationID"].ToString();
                string classname = dr["Name"].ToString();

                classname = blank + classname;
                drplist.Items.Add(new ListItem(classname, classid));

                int sonparentid = int.Parse(classid);
                string blank2 = blank + "　";

                BindNode(sonparentid, dt, blank2, drplist);//递归操作
            }
        }
        /// <summary>
        /// 依据Sql语句返回数据表2字段 带“请选择...”
        /// </summary>
        /// <param name="sqlStr">Sql</param>
        /// <returns>DataTable</returns>
        public static DataView createDDLView(string sqlStr)
        {
            OracleConnection conn = new OracleConnection(DBHelper.ConnectionString);
            conn.Open();
            OracleDataAdapter od = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataView seleView = new DataView();
            od.SelectCommand = new OracleCommand(sqlStr, conn);
            od.Fill(ds, "Table1");
            DataRow NewRow = ds.Tables["Table1"].NewRow();
            String Title = ds.Tables["Table1"].Columns[1].Caption;
            String TitleName = ds.Tables["Table1"].Columns[1].Caption;
            NewRow[0] = "0";
            NewRow[1] = " 请选择....";
            //ds.Tables["Table1"].Rows.Add(NewRow);
            ds.Tables["Table1"].Rows.InsertAt(NewRow, 0);
            seleView = ds.Tables["Table1"].DefaultView;
            //seleView.Sort = "" + Title + "," + Title + " ASC";
            conn.Close();
            conn.Dispose();
            return seleView;
        }
        /// <summary>
        /// 依据Sql语句返回数据表1字段 带“请选择...”
        /// </summary>
        /// <param name="sqlstr">SQL</param>
        /// <returns></returns>
        public DataView createDDlDataView(string sqlstr)
        {
            OracleConnection conn = new OracleConnection(DBHelper.ConnectionString);
            conn.Open();
            OracleDataAdapter od = new OracleDataAdapter();
            DataSet ds = new DataSet();
            DataView seleView = new DataView();
            od.SelectCommand = new OracleCommand(sqlstr, conn);
            od.Fill(ds, "Table1");
            DataRow NewRow = ds.Tables["Table1"].NewRow();
            String Title = ds.Tables["Table1"].Columns[0].Caption;
            NewRow[0] = " 请选择....";
            ds.Tables["Table1"].Rows.Add(NewRow);
            seleView = ds.Tables["Table1"].DefaultView;
            seleView.Sort = "" + Title + "," + Title + " ASC";
            conn.Close();
            conn.Dispose();
            return seleView;
        }
        /// <summary>
        /// 创建GridView的数据源
        /// </summary>
        /// <param name="sqlStr">SQL 语句</param>
        /// <returns></returns>
        public static DataView createGridView(string sqlStr)
        {
            try
            {
                OracleConnection conn = new OracleConnection(ConnectionString);
                conn.Open();
                OracleDataAdapter od = new OracleDataAdapter();
                DataSet ds = new DataSet();
                DataView seleView = new DataView();
                od.SelectCommand = new OracleCommand(sqlStr, conn);
                od.Fill(ds, "Table1");
                seleView = ds.Tables["Table1"].DefaultView;
                conn.Close();
                conn.Dispose();
                return seleView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 返回数据库查询的首行首列结果，以Object的形式返回
        /// </summary>
        /// <param name="sqlStr">SQL 语句</param>
        /// <returns></returns>
        public static Object getObject(string sqlStr)
        {
            Object obj = null;
            OracleConnection conn = new OracleConnection(ConnectionString);
            conn.Open();
            OracleCommand comm = new OracleCommand(sqlStr, conn);
            obj = comm.ExecuteScalar();
            conn.Close();
            conn.Dispose();
            return obj;
        }
        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="sqlStr">sql 语句</param>
        /// <returns>返回数据集</returns>
        public static DataSet createDataset(string sqlStr)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConnectionString))
                {                    
                    OracleDataAdapter od = new OracleDataAdapter();
                    DataSet ds = new DataSet();
                    od.SelectCommand = new OracleCommand(sqlStr, conn);
                    od.Fill(ds, "Table1");                    
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="sqlStr">sql 语句</param>
        /// <returns>返回数据集</returns>
        public static DataSet GetDataset(string sqlStr)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConnectionString))
                {
                    OracleDataAdapter od = new OracleDataAdapter();
                    DataSet ds = new DataSet();
                    od.SelectCommand = new OracleCommand(sqlStr, conn);
                    od.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 计算数据库表的行数
        /// </summary>
        /// <param name="sqlStr">条件</param>
        /// <returns></returns>
        public static int getCount(string sqlStr)
        {
            OracleConnection conn = new OracleConnection(ConnectionString);
            conn.Open();
            OracleCommand comm = new OracleCommand(sqlStr, conn);
            int cnt = Convert.ToInt32(comm.ExecuteScalar());
            conn.Close();
            conn.Dispose();
            return cnt;
        }
    }

}

