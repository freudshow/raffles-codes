using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;
using DreamStu.Common;
using System.IO;
using System.Windows.Forms;
namespace DetailInfo
{
    public class User
    {
        public static string rootpath = System.IO.Directory.GetCurrentDirectory();
        public static string cur_user = "";
        public static int projectid;

        private string _userName;
        /// <summary>
        /// 用户名称
        /// </summary>

        public string Name
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _userpass;
        /// <summary>
        /// 用户密码
        /// </summary>

        public string Pass
        {
            get { return _userpass; }
            set { _userpass = value; }
        }


        /// <summary>
        /// 根据用户名判断用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool Exist(string userName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT NAME FROM PLM.USER_TAB WHERE LOWER(NAME)=:name";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "name", DbType.String, userName.ToLower());
            object rname = db.ExecuteScalar(cmd);
            return (rname == null || rname == DBNull.Value) ? false : true;
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Verify(string userName, string password)
        {
            try
            {
                OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);

                string sql = "SELECT COUNT(*) FROM PLM.USER_TAB WHERE TRIM(LOWER(NAME))=:username AND PASS=:userpass";

                DbCommand cmd = db.GetSqlStringCommand(sql);

                db.AddInParameter(cmd, "username", DbType.String, userName.ToLower());

                db.AddInParameter(cmd, "userpass", DbType.String, Security.HashCryptString(password));

                return Convert.ToInt32(db.ExecuteScalar(cmd)) >= 1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                return false;
            }
           
        }

        /// <summary>
        /// 验证用户并获得用户的ID(若没此用户则返回0)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int VerifyID(string userName, string password)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT ID FROM PLM.USER_TAB WHERE LOWER(NAME)=:username AND PASS=:userpass";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "username", DbType.String, userName.ToLower());
            db.AddInParameter(cmd, "userpass", DbType.String, Security.HashCryptString(password));
            object ret = db.ExecuteScalar(cmd); if (ret == null) return 0;
            return Convert.ToInt32(ret);
        }

        /// <summary>
        /// 获得用户帐号状态
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static UserState GetState(string userName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT STATE FROM PLM.USER_TAB WHERE TRIM(LOWER(NAME))=:name";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "name", DbType.String, userName.ToLower());
            object s = db.ExecuteScalar(cmd);
            if (s == null || s == DBNull.Value) return UserState.LOCKED;
            if (string.IsNullOrEmpty(s.ToString())) return UserState.LOCKED;
            return (UserState)Enum.Parse(typeof(UserState), s.ToString());
        }

        /// <summary>
        /// 根据用户名获得用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User Find(string userName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            string sql = "SELECT * FROM PLM.USER_TAB WHERE LOWER(NAME)=:username";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, ":username", DbType.String, userName.ToLower());
            return Populate(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 从DataReader中填充用户实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static User Populate(IDataReader dr)
        {
            return EntityBase<User>.DReaderToEntity(dr);
        }

        /// <summary>
        /// 根据用户名称及权限标识符判断是否具有该权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="privilegeFlag"></param>
        /// <returns></returns>
        public static bool HavingPrivilege(string userName, string privilegeFlag)
        {
            int privlegeId = Privilege.FindIdByFlag(privilegeFlag);
            if (privlegeId == 0) return false;

            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PRIVILEGE_ID FROM PLM.USERINPRIVILEGE_TAB WHERE USERNAME=:username AND PRIVILEGE_ID=:privilegeid";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "username", DbType.String, userName);
            db.AddInParameter(cmd, "privilegeid", DbType.Int32, privlegeId);
            object isExist = db.ExecuteScalar(cmd);
            if (isExist != null && isExist != DBNull.Value) return true;

            List<string> roleNameList = FindRoleName(userName);
            if (roleNameList.Count == 0) return false;
            bool ret = false;
            foreach (string roleName in roleNameList)
            {
                if (Role.HavingPrivilege(roleName, privlegeId))
                {
                    ret = true; break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 根据用户名获得其所有角色名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<string> FindRoleName(string userName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT r.ROLENAME FROM PLM.ROLE_TAB r, PLM.USERINROLE_TAB u WHERE r.ROLENAME=u.ROLENAME AND LOWER(u.USERNAME)=:username";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, ":username", DbType.String, userName.ToLower());
            List<string> rnameList = new List<string>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                    rnameList.Add(dr[0].ToString());
                dr.Close();
            }
            return rnameList;
        }
        /// <summary>
        /// 根据用户名获得其所有权限id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string FindPrivilegeidList(string userName)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT PRIVILEGE_ID FROM PLM.USERINPRIVILEGE_TAB WHERE  LOWER(USERNAME)=:username";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, ":username", DbType.String, userName.ToLower());
            string pidList = string.Empty;

            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                    pidList += ("," + Convert.ToInt32(dr[0]));
                dr.Close();
            }

            if (pidList != string.Empty)
                pidList = pidList.Substring(1);
            return pidList;
        }
                /// <summary>
        /// 根据用户名称及权限标识符判断是否具有该权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="privilegeFlag"></param>
        /// <returns></returns>
        public static bool HavingPrivilege(string userName, string privilegeFlag, string andSql)
        {
            int privlegeId = Privilege.FindIdByFlag(privilegeFlag);
            if (privlegeId == 0) return false;

            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT ASQL FROM PLM.USERINPRIVILEGE_TAB WHERE USERNAME=:username AND PRIVILEGE_ID=:privilegeid";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "username", DbType.String, userName);
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


            List<string> roleNameList = FindRoleName(userName);
            if (roleNameList.Count == 0) return false;
            bool ret = false;
            foreach (string roleName in roleNameList)
            {
                if (Role.HavingPrivilege(roleName, privlegeId, andSql))
                {
                    ret = true; break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 标准导出Excel
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="pb"></param>
        /// <returns></returns>
        public static bool ExportToTxt(DataGridView dgv, ProgressBar pb)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel文件(*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel本文件";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                string columnTitle = "";
                try
                {
                    //写入列标题   
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (dgv.Columns[i].Visible == true)
                        {
                            if (i > 0)
                            {
                                columnTitle += "\t";
                            }
                            columnTitle += dgv.Columns[i].HeaderText;
                        }
                    }
                    columnTitle.Remove(columnTitle.Length - 1);
                    sw.WriteLine(columnTitle);

                    //写入列内容   
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        pb.Value = (j + 1) * 100 / dgv.Rows.Count;
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (dgv.Columns[k].Visible == true)
                            {

                                if (k > 0)
                                {
                                    columnValue += "\t";
                                }
                                if (dgv.Rows[j].Cells[k].Value == null)
                                {
                                    columnValue += "";
                                }
                                else
                                {
                                    string m = dgv.Rows[j].Cells[k].Value.ToString().Trim();
                                    columnValue += m.Replace("\t", "");
                                }
                            }
                        }
                        columnValue.Remove(columnValue.Length - 1);
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                    MessageBox.Show("导出完成！");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool ExportToExcel(DataGridView dgv, ToolStripProgressBar pb)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel文件(*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel本文件";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                string columnTitle = "";
                try
                {
                    //写入列标题   
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (dgv.Columns[i].Visible == true)
                        {
                            if (i > 0)
                            {
                                columnTitle += "\t";
                            }
                            columnTitle += dgv.Columns[i].HeaderText;
                        }
                    }
                    columnTitle.Remove(columnTitle.Length - 1);
                    sw.WriteLine(columnTitle);

                    //写入列内容   
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        pb.Value = (j + 1) * 100 / dgv.Rows.Count;
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (dgv.Columns[k].Visible == true)
                            {

                                if (k > 0)
                                {
                                    columnValue += "\t";
                                }
                                if (dgv.Rows[j].Cells[k].Value == null)
                                {
                                    columnValue += "";
                                }
                                else
                                {
                                    string m = dgv.Rows[j].Cells[k].Value.ToString().Trim();
                                    columnValue += m.Replace("\t", "");
                                }
                            }
                        }
                        columnValue.Remove(columnValue.Length - 1);
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                    MessageBox.Show("导出完成！");
                    pb.Value = 0;
                    pb.Visible = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>
        ///连接OIDSNEW数据库
        ///</summary>>
        public static void DataBaseConnect(string sr, DataSet dat)
        {
            OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
            try
            {
                //OracleConnection conn = new OracleConnection(DataAccess.OIDSConnStr);
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sr;
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;

                oda.Fill(dat);
                //conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="dat"></param>
        public static DataSet  getds(string sr)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = sr;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
           
        }
       

        ///<summary>
        ///连接数据库并执行SQL语句
        ///</summary>
        public static void UpdateCon(string sql_str, string con_str)
        {
            OracleConnection conn = new OracleConnection(con_str);
            try
            {
                //OracleConnection conn = new OracleConnection(con_str);
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql_str;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                //conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            finally
            {
                conn.Close();
            }
        }

        ///<summary>
        ///连接数据库并执行SQL语句获得最大行数
        ///</summary>
        public static object GetScalar(string sql_str, string con_str)
        {
            OracleConnection conn = new OracleConnection(con_str);
            object r = null;
            try
            {
                //OracleConnection conn = new OracleConnection(con_str);
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql_str;
                r = cmd.ExecuteOracleScalar();
                cmd.Dispose();
                //conn.Close();
                return r;
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                
            }
            finally 
            {
                conn.Close(); 
            }
            return r;

        }
        ///<summary>
        ///连接数据库并执行SQL语句获得最大行数
        ///</summary>
        public static object GetScalar1(string sql_str, string con_str)
        {
            OracleConnection conn = new OracleConnection(con_str);
            object r = null;
            try
            {
                //OracleConnection conn = new OracleConnection(con_str);
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql_str;
                r = cmd.ExecuteScalar();
                cmd.Dispose();
                conn.Close();
                return r;
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            finally
            {
                conn.Close();
            }
            return r;
        }
        /// <summary>
        /// 绑定数据源到combobox里
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="sql"></param>
        public static void ComFill(ComboBox combobox, string sql)
        {
            DataSet ds = new DataSet();
            User.DataBaseConnect(sql, ds);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            combobox.DataSource = ds.Tables[0].DefaultView;
            combobox.ValueMember = ds.Tables[0].Columns[0].ColumnName;
            combobox.SelectedIndex = -1;
            ds.Dispose();
        }


        /// <summary>
        /// 从主窗口获得当前用户名
        /// </summary>
        /// <param name="content"></param>
        public static void Get_CurrentUser(string content)
        {
            cur_user = content;
        }

        /// <summary>
        /// 获取分段施工计划查询条件
        /// </summary>
        public static string bcplanSql = string.Empty;
        public static void GetBlockContructionPlan(string Sql)
        {
            bcplanSql = Sql;
        }
        /// <summary>
        /// 获取项目图号查询条件
        /// </summary>
        public static string bcProDrawSql = string.Empty;
        public static void GetProjectDrawingPlan(string Sql)
        {
            bcProDrawSql = Sql;
        }
        /// <summary>
        /// 获取托盘材料纳期计划查询条件
        /// </summary>
        public static string traymatrialplan = string.Empty;
        public static void GetTrayMaterialPlan(string Sql)
        {
            traymatrialplan = Sql;
        }


        /// <summary>
        /// 获取托盘预制计划查询条件
        /// </summary>
        public static string traypreplan = string.Empty;
        public static void GetTrayPrePlan(string sql)
        {
            traypreplan = sql;
        }


        /// <summary>
        /// 获取托盘安装计划查询条件
        /// </summary>
        public static string trayinstallplan = string.Empty;
        public static void GetTrayInstallPlan(string sql)
        {
            trayinstallplan = sql;
        }

        /// <summary>
        /// 获取管路附件查询条件
        /// </summary>
        public static string pipepart = string.Empty;
        public static void GetPipePartString(string sql)
        {
            pipepart = sql;
        }

        public static string KickOffDate = string.Empty;
        public static void  getkickoffdate(string datestr)
        {
            KickOffDate= datestr;
        }

		public static string KickOffDate_start = string.Empty;
		public static void getkickoffdate_start(string datestr)
		{
			KickOffDate_start = datestr;
		}

		public static string KickOffDate_end = string.Empty;
		public static void getkickoffdate_end(string datestr)
		{
			KickOffDate_end = datestr;
		}

		public static string PipeBaseTotalWeight = string.Empty;
		public static void getPipeBaseTotalWeight(string datestr)
		{
			PipeBaseTotalWeight = datestr;
		}

        /// <summary>
        /// 获取管路材料查询条件
        /// </summary>
        public static string pipematerial = string.Empty;
        public static void GetPipeMaterialString(string sql)
        {
            pipematerial = sql;
        }
        /// <summary>
        /// 获取管路物料需求
        /// </summary>
        /// <returns></returns>
        public static DataTable PipeTab = new DataTable();
        public static void   GetBatchSheetPipeTable(DataTable  tab)
        {
            PipeTab = tab;
        }

		//母材余量
		public static string Margin = string.Empty;
		public static void GetMargin(string left)
		{
			Margin = left;
		}
		//母材总长度
		public static string TotalBaseLength = string.Empty;
		public static void GetTotalBaseLength(string total)
		{
			TotalBaseLength = total;
		}
		//母材利用率
		public static string PipeRatio = string.Empty;
		public static void GetPipeRatio(string Ratio)
		{
			PipeRatio = Ratio;
		}
       /// <summary>
       /// 获取零件材料需求
       /// </summary>
        /// <returns></returns>
        public static DataTable PartTab = new DataTable();
        public static void GetBatchSheetPartTable(DataTable tab)
        {
            PartTab = tab;
        }

		/// <summary>
		/// 获取套料材料
		/// </summary>
		/// <returns></returns>

		public static DataTable NestPipeTab = new DataTable();
		public static void GetBatchSheetNestPipeTab(DataTable tab)
		{
			NestPipeTab = tab;
		}

		

		public static DataTable FlageTab = new DataTable();
		public static void GetBatchSheetFlageTab(DataTable tab)
		{
			FlageTab = tab;
		}

		public static DataTable ElbowTab = new DataTable();
		public static void GetBatchSheetElbowTab(DataTable tab)
		{
			ElbowTab = tab;
		}

		public static DataTable SleeveTab = new DataTable();
		public static void GetBatchSheetSleeveTab(DataTable tab)
		{
			SleeveTab = tab;
		}

		public static DataTable OtherAttach = new DataTable();
		public static void GetOtherAttachTab(DataTable tab)
		{
			OtherAttach = tab;
		}
		

		/// <summary>
		/// 获取汇总
		/// </summary>
		/// <returns></returns>
		public static DataTable Gather = new DataTable();
		public static void GetBatchSheetGatherTab(DataTable tab)
		{
			Gather = tab;
		}

        public static string skinstr = "RealOne.ssk";
        public static void GetSkinStr(string str)
        {
            skinstr = str;
        }



    }



    /// <summary>
    /// 用户帐号的状态
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// 正常
        /// </summary>
        NORMAL,
        /// <summary>
        /// 锁定
        /// </summary>
        LOCKED
    }

    /// <summary>
    /// 角色的状态
    /// </summary>
    public enum RoleState
    {
        /// <summary>
        /// 正常
        /// </summary>
        NORMAL,
        /// <summary>
        /// 锁定
        /// </summary>
        LOCKED
    }

    /// <summary>
    /// 流程状态
    /// </summary>
    //public enum FlowState
    //{
    //    /// <summary>
    //    ///从三维模型提取原始数据
    //    /// </summary>
    //    初始 = 0,

    //    /// <summary>
    //    /// 设计人员提出审核
    //    /// </summary>
    //    待审 =1,

    //    /// <summary>
    //    /// 三级审核通过
    //    /// </summary>
    //    审核通过 =2,

    //    /// <summary>
    //    /// 由设计人员发往管加工
    //    /// </summary>
    //    待分配 =3,

    //    /// <summary>
    //    /// 任务分配完毕进行加工
    //    /// </summary>
    //    加工中 =4,

    //    /// <summary>
    //    /// 审核未通过并反馈给设计人员
    //    /// </summary>
    //    审核反馈 =5,

    //    /// <summary>
    //    /// 由管加工反馈小票问题给设计人员
    //    /// </summary>
    //    反馈设计 = 6,

    //    /// <summary>
    //    /// 由管加工完毕后发往质检并等待检验
    //    /// </summary>
    //    待验 =7,

    //    /// <summary>
    //    /// 设计人员处理管加工反馈问题小票
    //    /// </summary>
    //    处理反馈设计 = 8,

    //    /// <summary>
    //    /// 设计人员处理未通过审核的问题小票
    //    /// </summary>
    //    处理审核反馈 = 9,

    //    /// <summary>
    //    /// 质检反馈给管加工问题小票
    //    /// </summary>
    //    不合格 = 10,

    //    /// <summary>
    //    /// 质检部门检验通过发往生产并等待安装
    //    /// </summary>
    //    检验通过待安装 = 11,

    //    /// <summary>
    //    /// 正在安装
    //    /// </summary>
    //    安装中 = 12,

    //    /// <summary>
    //    /// 生产人员在船上安装完毕
    //    /// </summary>
    //    待调试 =13,

    //    /// <summary>
    //    /// 调试成功未发现问题
    //    /// </summary>
    //    调试完成 = 14,

    //    /// <summary>
    //    /// 调试失败发现问题
    //    /// </summary>
    //    调试失败= 15
    //}

    public enum FlowState
    {
        初始 = 0,
        审核中 = 1,
        审核通过 = 2,
        审核退回 = 3,
        下料完成 = 4,
        装配完成 = 5,
        焊接完成 = 6,
        待验 = 7,
        检验通过 = 8,
        检验不通过 = 9,
        处理完成 = 10,
        接收完成 = 11,
        发放完成 = 12,
        安装完成 = 13,
        待定 = 14
    }



    public enum QCStatus
    {
        初始数据 = 0,
        锁定数据 = 1,
        回传数据 = 2
    }
    
}
