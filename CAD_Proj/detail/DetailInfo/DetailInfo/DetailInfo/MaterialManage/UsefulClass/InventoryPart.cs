using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Common;
using DreamStu.Common.Log;

namespace Framework
{
    public class InventoryPart
    {
        private string _partno;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string PART_NO 
        {
            get { return _partno; }
            set { _partno = value; }
        }
        private string _desc;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string description
        {
            get { return _desc; }
            set { _desc = value; }
        }
        private string _site;
        /// <summary>
        /// 编号
        /// </summary>
        [BindingField]
        public string CONTRACT
        {
            get { return _site; }
            set { _site = value; }
        }
        private string _partFam;
        /// <summary>
        /// 产品家族
        /// </summary>
        [BindingField]
        public string part_product_family
        {
            get { return _partFam; }
            set { _partFam = value; }
        }
        private string _dim_qua;
        /// <summary>
        /// 尺寸质量
        /// </summary>
        [BindingField]
        public string  dim_quality
        {
            get { return _dim_qua; }
            set { _dim_qua = value; }
        }
        private string _unit_meas;
        /// <summary>
        /// 尺寸质量
        /// </summary>
        [BindingField]
        public string unit_meas
        {
            get { return _unit_meas; }
            set { _unit_meas = value; }
        }
        private string _qtyonhand;
        /// <summary>
        /// 项目可用库存总量
        /// </summary>
        [BindingField]
        public string qty_onhand
        {
            get { return _qtyonhand; }
            set { _qtyonhand = value; }
        }
        private string _qtyreserved;
        /// <summary>
        /// 项目预留库存总量
        /// </summary>
        [BindingField]
        public string qty_reserved
        {
            get { return _qtyreserved; }
            set { _qtyreserved = value; }
        }
        private string _qtyissued;
        /// <summary>
        /// 项目预留库存总量
        /// </summary>
        [BindingField]
        public string qty_issued
        {
            get { return _qtyissued; }
            set { _qtyissued = value; }
        }
        private string _partdesc;
        /// <summary>
        /// 材料描述
        /// </summary>
        [BindingField]
        public string Part_desc
        {
            get { return _partdesc; }
            set { _partdesc = value; }
        }
        private string _partunit;
        /// <summary>
        /// 材料描述
        /// </summary>
        [BindingField]
        public string Unit
        {
            get { return _partunit; }
            set { _partunit = value; }
        }
        /// <summary>
        /// 返回所有inventory part列表
        /// </summary>
        /// <returns></returns>
        public static DataSet FindInvPartDataset(string partno,string site)
        {
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT PART_NO,DESCRIPTION,Unit_meas unit FROM IFSAPP.inventory_part where PART_NO like '%" + partno + "%' and CONTRACT='" + site + "'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet FindInvPartInfByPartNameDataset(string partname,string site)
        {
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT PART_NO,DESCRIPTION FROM IFSAPP.inventory_part where DESCRIPTION like '%" + partname + "%' and CONTRACT='" + site + "'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet FindInvPartFamilyDataset()
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "  select PART_PRODUCT_FAMILY, IFSAPP.INVENTORY_PRODUCT_FAMILY_API.Get_Description(PART_PRODUCT_FAMILY) familyname  from (SELECT distinct PART_PRODUCT_FAMILY FROM IFSAPP.inventory_part)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public static DataSet FindInvPartDimDataset()
        {
           // Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT distinct dim_quality FROM IFSAPP.inventory_part";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(cmd);
        }
        public static string GetFamilyName(string ID)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("select IFSAPP.INVENTORY_PRODUCT_FAMILY_API.Get_Description(PART_PRODUCT_FAMILY) familyname from IFSAPP.inventory_part where PART_PRODUCT_FAMILY=:id");
            db.AddInParameter(cmd, "id", DbType.String, ID);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        public static string GetIsInventory(string site,string PartNo)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("select IFSAPP.Inventory_Part_API.Part_Exist(CONTRACT, PART_NO) isExist from IFSAPP.inventory_part where CONTRACT=:site and PART_NO=:partno");
            db.AddInParameter(cmd, "site", DbType.String, site);
            db.AddInParameter(cmd, "partno", DbType.String, PartNo);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 取得ERP中的库存
        /// </summary>
        /// <param name="site"></param>
        /// <param name="PartNo"></param>
        /// <returns></returns>
        public static string GetInventoryqty(string site, string PartNo)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("select IFSAPP.Inventory_Part_In_Stock_API.Get_Inventory_Qty_Onhand(CONTRACT,PART_NO,NULL) invQty  from IFSAPP.inventory_part where CONTRACT=:site and PART_NO=:partno");
            db.AddInParameter(cmd, "site", DbType.String, site);
            db.AddInParameter(cmd, "partno", DbType.String, PartNo);
            return string.IsNullOrEmpty(Convert.ToString(db.ExecuteScalar(cmd)))== false ? Convert.ToString(db.ExecuteScalar(cmd)) :"0";
        }
        /// <summary>
        /// 取得ERP中的积压库存
        /// </summary>
        /// <param name="site"></param>
        /// <param name="PartNo"></param>
        /// <returns></returns>
        public static string GetInventoryJYqty(string site, string PartNo)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("select IFSAPP.Inventory_Part_In_Stock_API.Get_Inventory_Qty_Reserved(CONTRACT,PART_NO,NULL) invQty  from IFSAPP.inventory_part where CONTRACT=:site and PART_NO=:partno");
            db.AddInParameter(cmd, "site", DbType.String, site);
            db.AddInParameter(cmd, "partno", DbType.String, PartNo);
            return string.IsNullOrEmpty(Convert.ToString(db.ExecuteScalar(cmd))) == false ? Convert.ToString(db.ExecuteScalar(cmd)) : "0";
        }
        /// <summary>
        /// 返回所有inventory part列表
        /// </summary>
        /// <returns></returns>
        public static InventoryPart FindInvInfor(string partno,string site)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT PART_NO,DESCRIPTION,PART_PRODUCT_FAMILY,dim_quality,unit_meas FROM IFSAPP.inventory_part where PART_NO = '" + partno + "' and CONTRACT='" + site + "'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Populate(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 返回所有inventory part列表
        /// </summary>
        /// <returns></s>
        public static string FindInvPartName(string partno, string site)
        {
            //Database db = DatabaseFactory.CreateDatabase("ifsConnection");
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sql = "SELECT DESCRIPTION FROM IFSAPP.inventory_part where PART_NO = '" + partno + "' and CONTRACT='" + site + "'";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 取得材料的库存信息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="PartNo"></param>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static InventoryPart GetOnhandqty(string site, string PartNo, string projectid)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            DbCommand cmd = db.GetSqlStringCommand("select sum(qty_onhand) qty_onhand,sum(qty_reserved) qty_reserved,part_no,part_desc,unit  from IFSAPP.yr_inv_on_hand_vw where CONTRACT=:site and PART_NO=:partno and req_dept like '%" + projectid + "%' group by part_no,part_desc,unit");
            db.AddInParameter(cmd, "site", DbType.String, site);
            db.AddInParameter(cmd, "partno", DbType.String, PartNo);
            return Populate(db.ExecuteReader(cmd)); 
        }
        /// <summary>
        /// 取得材料的申请详细信息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="PartNo"></param>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static InventoryPart GetRequiredqty(string site, string PartNo, string projectid)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            OracleDatabase db = new OracleDatabase(DataAccess.IFSConnStr);
            string sprojectname = projectid.Substring(projectid.Length - 3, 3);
            string querysql = "select p.contract ,p.part_no ,p.description ,p.unit_meas unit,nvl((select sum(tt.qty_onhand - tt.qty_reserved) from ifsapp.yr_inv_on_hand_vw tt WHERE tt.part_no = p.part_no and tt.contract = p.contract and tt.req_dept like 'YL" + sprojectname + "%'), 0) qty_reserved,nvl((select sum(REQUIRE_QTY ) from IFSAPP.PROJECT_MISC_PROCUREMENT where PROJECT_ID = '" + projectid + "'  and site = p.contract and  issue_from_inv = 0 and PART_NO = p.part_no and (select state from ifsapp.purchase_req_line_part q where q.requisition_no =p_requisition_no and q.part_no=p.part_no) <>'Cancelled'),0) qty_onhand,nvl((select sum(IFSAPP.PROJ_PROCU_RATION_API.Get_Accu_Ration_Qty(MATR_SEQ_NO)) from IFSAPP.PROJECT_MISC_PROCUREMENT where PROJECT_ID = '" + projectid + "'  and site = p.contract and  issue_from_inv = 0 and PART_NO = p.part_no and (select state from ifsapp.purchase_req_line_part q where q.requisition_no =p_requisition_no and q.part_no=p.part_no) <>'Cancelled'),0) qty_issued  from ifsapp.inventory_part p where  p.part_status='A' " + " AND CONTRACT = '" + site + "'" + " AND part_no like '" + PartNo + "%'";
            DbCommand cmd = db.GetSqlStringCommand(querysql);
            return Populate(db.ExecuteReader(cmd));
        }
        

        /// <summary>
        /// 从IDataReader中填充Comment实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static InventoryPart Populate(IDataReader dr)
        {
            return EntityBase<InventoryPart>.DReaderToEntity(dr);
        }

    }

}
