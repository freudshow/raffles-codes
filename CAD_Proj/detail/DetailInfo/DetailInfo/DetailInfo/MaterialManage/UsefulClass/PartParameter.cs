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
namespace Framework
{
    public class PartParameter
    {
        #region 预估基础数据
        private int _partId;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public int ID
        {
            get { return _partId; }
            set { _partId = value; }
        }
        private int _partid;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public int PARTID
        {
            get { return _partid; }
            set { _partid = value; }
        }
        private int _sequenceId;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public int SEQUENCE_ID
        {
            get { return _sequenceId; }
            set { _sequenceId = value; }
        }
        private int _parentId;
        /// <summary>
        /// 父ID
        /// </summary>
        [BindingField]
        public int PARENTID
        {
            get { return _parentId; }
            set { _parentId = value; }
        }
        private string _projectid;
        /// <summary>
        /// 项目编号
        /// </summary>
        [BindingField]
        public string PROJECTID
        {
            get { return _projectid; }
            set { _projectid = value; }
        }
        private string _partno;
        /// <summary>
        /// 零件编号
        /// </summary>
        [BindingField]
        public string PART_NO
        {
            get { return _partno; }
            set { _partno = value; }
        }
        private string _partname;
        /// <summary>
        /// 零件编号
        /// </summary>
        [BindingField]
        public string DESCRIPTION
        {
            get { return _partname; }
            set { _partname = value; }
        }
        private string _site;
        /// <summary>
        /// 零件域
        /// </summary>
        [BindingField]
        public string CONTRACT
        {
            get { return _site; }
            set { _site = value; }
        }
        private decimal _weightSingle;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public decimal WEIGHT_SINGLE
        {
            get { return _weightSingle; }
            set { _weightSingle = value; }
        }
        private decimal _preqty;
        /// <summary>
        /// 预估量
        /// </summary>
        [BindingField]
        public decimal PREDICTION_QTY
        {
            get { return _preqty; }
            set { _preqty = value; }
        }
        private decimal _preAlert;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public decimal PREDICTION_ALERT
        {
            get { return _preAlert; }
            set { _preAlert = value; }
        }
        private decimal _coeficient;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public decimal COEFFICIENT_ERP
        {
            get { return _coeficient; }
            set { _coeficient = value; }
        }
        private string _creator;
        /// <summary>
        /// 预估人
        /// </summary>
        [BindingField]
        public string PREDICT_CREATOR
        {
            get { return _creator; }
            set { _creator = value; }
        }
        private string _partspec;
        /// <summary>
        /// 规格
        /// </summary>
        [BindingField]
        public string PART_SPEC
        {
            get { return _partspec; }
            set { _partspec = value; }
        }
        private string _partstand;
        /// <summary>
        /// 尺寸标准
        /// </summary>
        [BindingField]
        public string PART_SIZE_STAND
        {
            get { return _partstand; }
            set { _partstand = value; }
        }
        
        private string _punit;
        /// <summary>
        /// 材料单位
        /// </summary>
        [BindingField]
        public string UNIT
        {
            get { return _punit; }
            set { _punit = value; }
        }
        private int _lastflag;
        /// <summary>
        /// 最后表示列
        /// </summary>
        [BindingField]
        public int LAST_FLAG
        {
            get { return _lastflag; }
            set { _lastflag = value; }
        }
        private int _sysdid;
        /// <summary>
        /// SYSTEMID
        /// </summary>
        [BindingField]
        public int SYSTEMID
        {
            get { return _sysdid; }
            set { _sysdid = value; }
        }
        private string _blockid;
        /// <summary>
        /// BLOCKID
        /// </summary>
        [BindingField]
        public string BLOCKID
        {
            get { return _blockid; }
            set { _blockid = value; }
        }
        private int _ecpid;
        /// <summary>
        /// ECPROJECTID
        /// </summary>
        [BindingField]
        public int ECPROJECTID
        {
            get { return _ecpid; }
            set { _ecpid = value; }
        }
        private DateTime _ctime;
        /// <summary>
        ///创建时间
        /// </summary>
        [BindingField]
        public DateTime CREATEDATE
        {
            get { return _ctime; }
            set { _ctime = value; }
        }
        private DateTime _uptime;
        /// <summary>
        ///更新时间
        /// </summary>
        [BindingField]
        public DateTime UPDATEDATE
        {
            get { return _uptime; }
            set { _uptime = value; }
        }
        private DateTime _pdtime;
        /// <summary>
        ///预估日期
        /// </summary>
        [BindingField]
        public DateTime PREDICT_DATE
        {
            get { return _pdtime; }
            set { _pdtime = value; }
        }
        private string _pzone;
        /// <summary>
        /// 预估区域
        /// </summary>
        [BindingField]
        public string PROJECT_ZONE
        {
            get { return _pzone; }
            set { _pzone = value; }
        }
        private string _pdpline;
        /// <summary>
        /// 预估区域
        /// </summary>
        [BindingField]
        public string DISCIPLINE
        {
            get { return _pdpline; }
            set { _pdpline = value; }
        }
        private decimal _finalpreqty;
        /// <summary>
        /// 确认预估量
        /// </summary>
        [BindingField]
        public decimal FINAL_PREDICTION_QTY
        {
            get { return _finalpreqty; }
            set { _finalpreqty = value; }
        }
        private decimal _firstbatchqty;
        /// <summary>
        /// 第一批生产需求量
        /// </summary>
        [BindingField]
        public decimal FIRSTBATCH_QTY
        {
            get { return _firstbatchqty; }
            set { _firstbatchqty = value; }
        }
        private decimal _secondbatchqty;
        /// <summary>
        /// 第二批生产需求量
        /// </summary>
        [BindingField]
        public decimal SECONDBATCH_QTY
        {
            get { return _secondbatchqty; }
            set { _secondbatchqty = value; }
        }
        private DateTime _firstbatchtime;
        /// <summary>
        ///第一批生产需求时间
        /// </summary>
        [BindingField]
        public DateTime FIRSTBATCH_DATE
        {
            get { return _firstbatchtime; }
            set { _firstbatchtime = value; }
        }
        private DateTime _secondbatchtime;
        /// <summary>
        ///第二批生产需求时间
        /// </summary>
        [BindingField]
        public DateTime SECONDBATCH_DATE
        {
            get { return _secondbatchtime; }
            set { _secondbatchtime = value; }
        }
        private string _ptype;
        /// <summary>
        /// 材料种类
        /// </summary>
        [BindingField]
        public string PART_TYPE
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        private string _operator;
        /// <summary>
        /// 材料种类
        /// </summary>
        [BindingField]
        public string OPERATOR
        {
            get { return _operator; }
            set { _operator = value; }
        }
        private string _modreason;
        /// <summary>
        /// 材料种类
        /// </summary>
        [BindingField]
        public string MODIFY_REASON
        {
            get { return _modreason; }
            set { _modreason = value; }
        }

        #endregion
        /// <summary>
        /// 返回当前项目当前材料类别的预估列表
        /// </summary>
        /// <returns></returns>
        public static List<PartParameter> FindPartList(string sysid,string pid, string projectstr, string creator,int lastflag)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM mm_part_para_view t WHERE systemid=" + sysid+ " and t.last_flag=" + lastflag + " and t.creator='" + creator + "'  and t.projectid ='" + projectstr + "'";
            //string sql = "SELECT * FROM mm_part_para_view t WHERE systemid=" + sysid + "  and t.last_flag=" + lastflag + " and t.creator='" + creator + "'  and t.projectid ='" + projectstr + "'";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            return EntityBase<PartParameter>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回当前项目当前材料类别的预估历史
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="projectstr"></param>
        /// <param name="creator"></param>
        /// <param name="lastflag"></param>
        /// <returns></returns>
        public static List<PartParameter> FindPartList(string partid, string projectstr, string creator)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            string sql = "SELECT * FROM mm_part_para_view t WHERE t.last_flag=0 and t.creator='" + creator + "' and t.PARTID=" + partid + " and t.projectid ='" + projectstr + "'";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            return EntityBase<PartParameter>.DReaderToEntityList(db.ExecuteReader(cmd));
        }
        public static PartParameter Find(int systemid,string project, string partno, string site,string creator)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT * FROM plm.MM_PART_PARAMETER_TAB WHERE systemid=:systemid and last_flag=1 and creator='" + creator + "' and PROJECTID=:projectid and PART_NO=:partno and CONTRACT=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "projectid", DbType.String, project);
            db.AddInParameter(cmd, "partno", DbType.String, partno);
            db.AddInParameter(cmd, "site", DbType.String, site);
            db.AddInParameter(cmd, "systemid", DbType.Int32, systemid);
            return Populate(db.ExecuteReader(cmd));
        }
        public static string FindPreQty(string project, string partno, string site)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
 //           Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT PREDICTION_QTY FROM plm.MM_PART_PARAMETER_TAB WHERE PROJECTID=:projectid and PART_NO=:partno and CONTRACT=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "projectid", DbType.String, project);
            db.AddInParameter(cmd, "partno", DbType.String, partno);
            db.AddInParameter(cmd, "site", DbType.String, site);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        public static string FindPreAlert(string project, string partno, string site)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
          //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT PREDICTION_ALERT FROM plm.MM_PART_PARAMETER_TAB WHERE PROJECTID=:projectid and PART_NO=:partno and CONTRACT=:site";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "projectid", DbType.String, project);
            db.AddInParameter(cmd, "partno", DbType.String, partno);
            db.AddInParameter(cmd, "site", DbType.String, site);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 取得spec字段对应的名称
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static string GetSpecName(string typeid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT spec_namelist("+typeid+") FROM dual";
            DbCommand cmd = db.GetSqlStringCommand(sql);
              return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 取得spec字段对应的名称
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetSpecName(string typeid,string num)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT spec_namespec(" + typeid+","+num+") FROM dual";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 取得spec字段个数
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetSpecCou(string typeid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT spec_namecount(" + typeid + ") FROM dual";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        public static int GetPartParaCou(string partid, string creator, string projectstr)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT count(*) from MM_PART_PARAMETER_TAB where projectid='"+projectstr+"' and part_no=" + partid + " and last_flag=0 and discipline ="+creator;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 根据ID查找预估信息
        /// </summary>
        /// <param name="sequenceNo"></param>
        /// <returns></returns>
        public static PartParameter Find(int sequenceNo)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
          //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT * FROM plm.MM_PART_PARAMETER_TAB WHERE SEQUENCE_ID=:seqNo";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "seqNo", DbType.Int32, sequenceNo);

            return Populate(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 根据零件ID和项目ID找出预估总量
        /// </summary>
        /// <param name="partid"></param>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static decimal FindPartParaSum(int partid, string projectid)
        {
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            //  Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            //OracleDatabase db = new OracleDatabase(UserSecurity.ConnectionString);
            string sql = "SELECT pred_sum_qty FROM plm.mm_part_parasum_view WHERE PARTID="+partid+" and ecprojectid="+projectid ;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object pdsumqty = db.ExecuteScalar(cmd);
            return (pdsumqty == null || pdsumqty == DBNull.Value) ? Convert.ToDecimal(0) : Convert.ToDecimal(pdsumqty);
            
        }
        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static PartParameter Populate(IDataReader dr)
        {
            return EntityBase<PartParameter>.DReaderToEntity(dr);
        }
        /// <summary>
        /// 返回所有inventory part列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindInvPartDataset()
        {
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            string sql = "SELECT * FROM IFSAPP.inventory_part";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 返回所有零件参数列表
        /// </summary>
        /// <returns></returns>
        public static DataSet  QueryPartPara(string sql)
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");          
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 返回所有ERP库存零件参数列表
        /// </summary>
        /// <returns></returns>
        public static DataSet QueryPartERPInventory(string sql)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");          
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public int Add()
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("INSERT INTO PLM.MM_PART_PARAMETER_TAB(description,blockid,systemid,ecprojectid,last_flag,PART_NO,CONTRACT,WEIGHT_SINGLE,PREDICTION_QTY,PREDICTION_ALERT,COEFFICIENT_ERP,PROJECTID,PREDICT_CREATOR,PROJECT_ZONE,DISCIPLINE,FIRSTBATCH_QTY,FIRSTBATCH_DATE,SECONDBATCH_QTY,SECONDBATCH_DATE,PART_TYPE,FINAL_PREDICTION_QTY,OPERATOR,MODIFY_REASON,PREDICT_DATE,UNIT) VALUES (:description,:blockid,:systemid,:ecprojectid,:lastflag,:partno,:site,:weightSingle,:preQty,:pre_alert,:coeficient,:PROJECTID,:creator,:predictzone,:discipline,:firstpreQty,:firstpreDate,:secondpreQty,:secondpreDate,:part_type,:finalpreQty,:operator,:modreason,:predictdate,:unitmeas)");
            db.AddInParameter(cmd, "description", DbType.String, DESCRIPTION);
            db.AddInParameter(cmd, "blockid", DbType.String, BLOCKID);
            db.AddInParameter(cmd, "systemid", DbType.Int32, SYSTEMID);
            db.AddInParameter(cmd, "ecprojectid", DbType.Int32, ECPROJECTID);
            db.AddInParameter(cmd, "lastflag", DbType.Int32, LAST_FLAG );
            db.AddInParameter(cmd, "partno", DbType.String, PART_NO);
            db.AddInParameter(cmd, "site", DbType.String, CONTRACT);
            db.AddInParameter(cmd, "weightSingle", DbType.Decimal, WEIGHT_SINGLE);
            db.AddInParameter(cmd, "preQty", DbType.Decimal, PREDICTION_QTY);
            db.AddInParameter(cmd, "pre_alert", DbType.Decimal, PREDICTION_ALERT);
            db.AddInParameter(cmd, "coeficient", DbType.Decimal, COEFFICIENT_ERP);
            db.AddInParameter(cmd, "PROJECTID", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "creator", DbType.String, PREDICT_CREATOR);
            db.AddInParameter(cmd, "predictzone", DbType.String, PROJECT_ZONE);
            db.AddInParameter(cmd, "discipline", DbType.String, DISCIPLINE);
            db.AddInParameter(cmd, "firstpreQty", DbType.Decimal, FIRSTBATCH_QTY);
            db.AddInParameter(cmd, "firstpreDate", DbType.DateTime, FIRSTBATCH_DATE);
            db.AddInParameter(cmd, "secondpreQty", DbType.Decimal, SECONDBATCH_QTY);
            db.AddInParameter(cmd, "secondpreDate", DbType.DateTime, SECONDBATCH_DATE);
            db.AddInParameter(cmd, "part_type", DbType.String, PART_TYPE);
            db.AddInParameter(cmd, "finalpreQty", DbType.Decimal, FINAL_PREDICTION_QTY);
            db.AddInParameter(cmd, "predictdate", DbType.DateTime, PREDICT_DATE);
            db.AddInParameter(cmd, "operator", DbType.String, OPERATOR);
            db.AddInParameter(cmd, "modreason", DbType.String, MODIFY_REASON);
            db.AddInParameter(cmd, "unitmeas", DbType.String, UNIT);
            return db.ExecuteNonQuery(cmd);
        }
        public int Update()
        {
            //Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("update PLM.MM_PART_PARAMETER_TAB t set t.createdate=sysdate,t.updatedate=sysdate, t.blockid=:blockid,t.systemid=:systemid,ecprojectid=:ecprojectid, t.unit=:unit,t.WEIGHT_SINGLE=:weightSingle,t.PREDICTION_QTY=:preQty,t.PREDICTION_ALERT=:pre_alert,t.COEFFICIENT_ERP=:coeficient, CREATOR=:creator where systemid=:systemid and PROJECTID=:projectid and last_flag=1 and t.PART_NO=:partno and CONTRACT=:site and t.creator=:creator");
            db.AddInParameter(cmd, "projectid", DbType.String, PROJECTID);
            db.AddInParameter(cmd, "partno", DbType.String, PART_NO);
            db.AddInParameter(cmd, "site", DbType.String, CONTRACT);
            db.AddInParameter(cmd, "weightSingle", DbType.Single, WEIGHT_SINGLE);
            db.AddInParameter(cmd, "preQty", DbType.Decimal, PREDICTION_QTY);
            db.AddInParameter(cmd, "pre_alert", DbType.Decimal, PREDICTION_ALERT);
            db.AddInParameter(cmd, "coeficient", DbType.Decimal, COEFFICIENT_ERP);
            db.AddInParameter(cmd, "creator", DbType.String, PREDICT_CREATOR);
            db.AddInParameter(cmd, "unit", DbType.String, UNIT);
            db.AddInParameter(cmd, "systemid", DbType.Int32, SYSTEMID);
            db.AddInParameter(cmd, "ecprojectid", DbType.Int32, ECPROJECTID);
            db.AddInParameter(cmd, "blockid", DbType.String, BLOCKID);
            db.AddInParameter(cmd, "systemid", DbType.Int32, SYSTEMID);
            
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除预估
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public static int Delete(int seqNo)
        {
           // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("delete PLM.MM_PART_PARAMETER_TAB t where SEQUENCE_ID=:seqNo");
            db.AddInParameter(cmd, "seqNo", DbType.Int32, seqNo);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除预估
        /// </summary>
        /// <param name="contractno">域</param>
        /// <param name="pid">项目ID</param>
        /// <param name="dpline">项目专业</param>
        /// <returns></returns>
        public static void DeleteEstimate(string contractno,string pid,string dpline)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("update PLM.MM_PART_PARAMETER_TAB t set t.last_flag=0 where CONTRACT =:mSite and projectid=:pid and  DISCIPLINE =:dpid");
            db.AddInParameter(cmd, "mSite", DbType.String, contractno);
            db.AddInParameter(cmd, "pid", DbType.String, pid);
            db.AddInParameter(cmd, "dpid", DbType.String, dpline);
            db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 确认预估
        /// </summary>
        /// <param name="mainid"></param>
        public static void DeleteEstimate(string seqidList)
        {
            // Database db = DatabaseFactory.CreateDatabase("oidsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.OIDSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("update PLM.MM_PART_PARAMETER_TAB t set t.confirmed='Y' where sequence_id in (" + seqidList + ")");
            string stringlist = "update PLM.MM_PART_PARAMETER_TAB t set t.confirmed='Y' where sequence_id in (" + seqidList + ")";
            //db.AddInParameter(cmd, "dpid", DbType.String, seqidList);
            db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 标准导出Excel
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="pb"></param>
        /// <returns></returns>
        public static bool ExportToTxt(DataGridView dgv,ProgressBar pb)
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
                        pb.Value = (j+1) * 100 / dgv.Rows.Count;
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

    }
}
