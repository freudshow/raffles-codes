using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using jzpl.WebReference1;
using jzpl.UI.JP;

namespace jzpl.Lib
{
    public class Authentication
    { 
        public static readonly int  MaxPermisonLength = 100;
       
        public enum PERMDEFINE
        {
            PKG_JP_ADD = 1,             //大包创建配料申请/页面jp_pkg_add.aspx/子页面jp_pkg_mod.aspx/子页面pkg_part_lov1.aspx
            PKG_JP_QUERY = 2,           //大包配料申请变更/页面jp_pkg_query.aspx/子页面jp_pkg_mod.aspx
            PKG_JP_CONFIRM = 3,         //大包保管确认/页面jp_pkg_confirm.aspx/
            PKG_JP_CONFIRM1 = 4,        //大包生产确认/页面jp_pkg_confirm1.aspx/
            PKG_JP_QUERY1 = 5,          //大包配料申请查询/页面jp_pkg_query_ext.aspx/
            PKG_JP_ISS=6,               //大包物资下发/页面jp_pkg_issue.aspx/子页面jp_pkg_issue_a.aspx
            PKG_JP_JJD_NEW = 7,         //创建大包交接单/jp_pkg_jjd_new.aspx
            PKG_JP_JJD_FINISH = 8,      //大包交接单完成/jp_pkg_jjd_finish.aspx
            PKG_JP_JJD = 9,             //大包交接单查询
            PKG_JP_ISS_VCHR=10,         //大包物资领料单打印
                     

            PART_JP_ADD = 21,           //普通物资创建配料申请/关联页面wzxqjh_add.aspx/继承页面wzxqjh_ration_lov.aspx
            PART_JP_QUERY = 22,         //申请变更查询/wzxqjh_query.aspx/wzxqjh_mod.aspx
            PART_JP_LACK = 30,       //缺料/wzxqjh_lack.aspx
            PART_JP_CONFIRM = 23,       //保管确认/wzxqjh_confirm.aspx
            PART_JP_CONFIRM1 = 24,      //生产确认/wzxqjh_confirm1.aspx
            PART_JP_QUERY1 = 25,        //申请查询/wzxqjh_query_ext.aspx           
            PART_JP_JJD_NEW = 26,       //创建交接单/wzxqjh_jjd_new_.aspx
            PART_JP_JJD_FINISH = 27,    //交接单完成/wzxqjh_jjd_finish.aspx
            PART_JP_JJD = 28,           //交接单查询/
            PART_ISS_COMPARE = 29,      //ERP-DMS下发对照/


            PKG = 41,                  //大包信息维护/package.aspx/子页面pkg_erp_lov.aspx/子页面pkg_mod.aspx
            PKG_PART = 42,             //大包小件信息维护/pkg_part.aspx/pkg_lov.aspx/pkg_part_mod.aspx
            PKG_ARR = 43,              //大包到货登记/pkg_arrival.aspx
            PKG_CHK = 44,              //大包检验/pkg_check.aspx
            PKG_MOV = 45,              //大包位置转移/pkg_loc_move.aspx
            PKG_Q = 46,                //大包查询/pkg_query.aspx
            PKG_PART_Q = 47,           //大包小件查询/pkg_part_query.aspx  
            PKG_ARR_Q = 48,            //大包到货查询/pkg_arrival_query.aspx
            PKG_CHK_Q = 49,            //大包检验查询/pkg_check_query.aspx
            PKG_MOV_Q = 50,            //大包位置转移查询/pkg_loc_move_query.aspx
            PKG_LOC_Q = 51,             //大包库存查询/pkg_loc_query.aspx
            PKG_CHG_PRJ=52,             //大包项目转移/pkg_change_project.aspx   
            PKG_STORE_IN=53,
            PKG_STORE_ISS=54,
            PKG_VALUE_Q=55,
            PKG_LOCHIS_Q=56,


            BS_COMPANY = 61,            //公司
            BS_PROJECT = 62,            //项目
            BS_CURRENCY = 63,           //币种
            BS_UNIT = 64,               //单位
            BS_LACK = 65,               //缺料原因
            BS_RCPT_PLACE = 66,         //接收地
            BS_RCPT_DEPT = 67,          //接收部门
            BS_WH_AREA = 68,            //区域
            BS_WH_LOC = 69,             //位置
            BS_REQ_GROUP = 70,          //申请组
            BS_PRJ_ACC=71,              //项目访问人员维护
            BS_RCPT_PER=72,             //接收人维护
            BS_CHK_PER=73,              //检验人维护
            ADMIN=100
        }

        public struct LOGININFO
        {
            public string UserID;
            public string UserPwd;
            public string Permission;
            public string Admin;
            public string GroupID;
            public string ServerName;
        }
        
        private System.Web.HttpApplicationState Application;
        private System.Web.SessionState.HttpSessionState Session;        
        //public string[] PERMISSION_PAGE;
        public LOGININFO logininfo;        

        public Authentication(System.Web.UI.Page Parent)
        {
            this.Application = Parent.Application;
            this.Session = Parent.Session;
            //this.PERMISSION_PAGE = Enum.GetNames(typeof(FUN_INTERFACE));          
        }

        public Authentication(System.Web.UI.UserControl Parent)
        {
            this.Application = Parent.Application;
            this.Session = Parent.Session;
            //this.PERMISSION_PAGE = Enum.GetNames(typeof(FUN_INTERFACE));
        }

        public bool CheckUser(string id, string pwd, ref string ermsg)
        {
            authenticateService auth = new authenticateService();
            const string DOMAIN = "@raffles.local";
            DataSet ds = Lib.DBHelper.createDataset("select * from jp_user where user_id='" + id + "'" );
            if (ds.Tables[0].Rows.Count <= 0)
            {
                ermsg = id + "请重新登录！";
                return false;
            }
            if (false) //ming.li
            //if(auth.get_authenticate(string.Format("{0}{1}",id,DOMAIN),pwd)!="1")
            {
                ermsg = "验证错误！";
                return false;
            }
            logininfo.UserID = id;
            logininfo.UserPwd = pwd;
            logininfo.Permission = ds.Tables[0].Rows[0]["role"].ToString();
            logininfo.Admin = ds.Tables[0].Rows[0]["admin"].ToString();
            logininfo.GroupID = ds.Tables[0].Rows[0]["group_id"].ToString();
            logininfo.ServerName = DBHelper.dbStr; 
            Session["USERINFO"] = this.logininfo;
            return true;
        }

        //public bool CheckAccessAble(int index)
        //{
            
        //}
        public bool LoadSession()
        {
            try
            {
                LOGININFO logininfo = (LOGININFO)Session["USERINFO"];
                if (logininfo.UserID.Trim().Length <= 0 || logininfo.UserPwd.Trim().Length <= 0) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void RemoveSession()
        {
            LOGININFO loginInfo;
            loginInfo.UserID = string.Empty;
            loginInfo.UserPwd = string.Empty;
            loginInfo.Permission = string.Empty;
            logininfo.Admin = string.Empty;
            loginInfo.GroupID = string.Empty;
            logininfo.ServerName = string.Empty;

            Session["USERINFO"] = logininfo;
        }

        //public string[] InitPermissionArray()
        //{
        //    int range_;
        //    range_ = Enum.GetValues(typeof(FUN_INTERFACE)).Length;
        //    string[] m_permission_array = new string[range_];
        //    for (int i = 0; i < range_; i++)
        //    {
        //        m_permission_array[i] = "0";//access位初始值
        //        System.Type t = System.Type.GetType("jzpl." + PERMISSION_PAGE[i]);
        //        IBasePage p = (IBasePage)Activator.CreateInstance(t);
        //        if (p.GetPermissionType() == PERMISSION_TYPE.page_.ToString()) continue;
        //        for (int j = 0; j < p.GetFun().Length; j++)
        //        {
        //            m_permission_array[i] += "0";
        //        }
        //    }
            
        //    return m_permission_array;
        //}

        public static DataTable GetPermissionTable()
        {            
            DataTable per = new DataTable();
            per.Columns.Add("code", typeof(string));
            per.Columns.Add("page", typeof(string));
            per.Columns.Add("description", typeof(string));
            per.Columns.Add("permission", typeof(string));

            DataRow row  = per.NewRow();

            #region 大包配送 
            row = per.NewRow(); row["code"] = "PKG_JP_ADD"; row["page"] = "jp_pkg_add"; row["description"] = "大包配料申请创建"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_QUERY"; row["page"] = "jp_pkg_query"; row["description"] = "大包配料申请变更"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_CONFIRM"; row["page"] = "jp_pkg_confirm"; row["description"] = "大包配料申请确认（保管）"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_CONFIRM1"; row["page"] = "jp_pkg_confirm1"; row["description"] = "大包配料申请创建（生产）"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_QUERY1"; row["page"] = "jp_pkg_query_ext"; row["description"] = "大包配料申请查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_ISS"; row["page"] = "jp_pkg_issue"; row["description"] = "大包配料申请下发"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_JJD_NEW"; row["page"] = "jp_pkg_jjd_new"; row["description"] = "大包配送交接单创建"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_JJD_FINISH"; row["page"] = "jp_pkg_jjd_finish"; row["description"] = "大包配送交接单完成"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_JJD"; row["page"] = "jp_pkg_jjd"; row["description"] = "大包配送交接单查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_JP_ISS_VCHR"; row["page"] = "pkg_issue_voucher"; row["description"] = "大包物资领料打印"; row["permission"] = "0"; per.Rows.Add(row);
            #endregion
            #region 普通物资配送 
            row = per.NewRow(); row["code"] = "PART_JP_ADD"; row["page"] = "wzxqjh_add"; row["description"] = "普通物资配料申请创建"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_QUERY"; row["page"] = "wzxqjh_query"; row["description"] = "普通物资配料申请变更"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_LACK"; row["page"] = "wzxqjh_lack"; row["description"] = "普通物资缺料下达（缺品）"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_CONFIRM"; row["page"] = "wzxqjh_confirm"; row["description"] = "普通物资配料申请确认（保管）"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_CONFIRM1"; row["page"] = "wzxqjh_confirm1"; row["description"] = "普通物资配料申请（生产）"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_QUERY1"; row["page"] = "wzxqjh_query_ext"; row["description"] = "普通物资配料申请查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_JJD_NEW"; row["page"] = "wzxqjh_jjd_new_"; row["description"] = "普通物资配送交接单创建"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_JJD_FINISH"; row["page"] = "wzxqjh_jjd_finish"; row["description"] = "普通物资配送交接单完成"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_JP_JJD"; row["page"] = "wzxqjh_jjd"; row["description"] = "普通物资配送交接单查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PART_ISS_COMPARE"; row["page"] = "wzxqjh_iss_compare"; row["description"] = "ERP-DMS发料对照"; row["permission"] = "0"; per.Rows.Add(row);

            #endregion
            #region 大包信息维护
            row = per.NewRow(); row["code"] = "PKG"; row["page"] = "package"; row["description"] = "大包信息维护"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_PART"; row["page"] = "pkg_part"; row["description"] = "大包小件信息维护"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_ARR"; row["page"] = "pkg_arrival"; row["description"] = "大包到货登记"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_CHK"; row["page"] = "pkg_check"; row["description"] = "大包检验"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_MOV"; row["page"] = "pkg_loc_move"; row["description"] = "大包位置转移"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_Q"; row["page"] = "pkg_query"; row["description"] = "大包查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_PART_Q"; row["page"] = "pkg_part_query"; row["description"] = "大包小件查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_ARR_Q"; row["page"] = "pkg_arrival_query"; row["description"] = "大包到货查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_CHK_Q"; row["page"] = "pkg_check_query"; row["description"] = "大包检验查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_MOV_Q"; row["page"] = "pkg_loc_move_query"; row["description"] = "大包位置转移查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_LOC_Q"; row["page"] = "pkg_loc_query"; row["description"] = "大包库存查询"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_CHG_PRJ"; row["page"] = "pkg_change_project"; row["description"] = "大包项目转移"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_STORE_IN"; row["page"] = "pkg_part_receive"; row["description"] = "大包物资库存接收"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_STORE_ISS"; row["page"] = "pkg_part_issue"; row["description"] = "大包物资库存下发"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_VALUE_Q"; row["page"] = "pkg_value_query"; row["description"] = "大包物资价值"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "PKG_LOCHIS_Q"; row["page"] = "pkg_loc_history"; row["description"] = "大包物资库存事务档案"; row["permission"] = "0"; per.Rows.Add(row);
            #endregion
            #region 基础数据
            row = per.NewRow(); row["code"] = "BS_COMPANY"; row["page"] = "company"; row["description"] = "公司"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_PROJECT"; row["page"] = "project"; row["description"] = "项目"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_CURRENCY"; row["page"] = "currency"; row["description"] = "币种"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_UNIT"; row["page"] = "part_unit"; row["description"] = "单位"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_LACK"; row["page"] = "lack_reason"; row["description"] = "缺料原因"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_RCPT_PLACE"; row["page"] = "receipt_place"; row["description"] = "接收地"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_RCPT_DEPT"; row["page"] = "receipt_dept"; row["description"] = "接收部门"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_WH_AREA"; row["page"] = "wh_area"; row["description"] = "区域"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_WH_LOC"; row["page"] = "wh_location"; row["description"] = "位置"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_REQ_GROUP"; row["page"] = "req_group"; row["description"] = "申请组"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_PRJ_ACC"; row["page"] = "project_acc_per"; row["description"] = "项目访问人员维护"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_RCPT_PER"; row["page"] = "receipt_person"; row["description"] = "接收人"; row["permission"] = "0"; per.Rows.Add(row);
            row = per.NewRow(); row["code"] = "BS_CHK_PER"; row["page"] = "check_person"; row["description"] = "检验员"; row["permission"] = "0"; per.Rows.Add(row);
            
            #endregion

            return per;
        }
  
        public string UserID
        {
            get { return this.logininfo.UserID; }
        }
               
        public string UserPassword
        {
            get { return this.logininfo.UserPwd; }
        }
        
        public string Permission
        {
            get { return this.logininfo.Permission; }
        }

        public string Admin
        {
            get { return this.logininfo.Admin; }
        }
        public string GroupID
        {
            get { return this.logininfo.GroupID; }
        }
        public string ServerName
        {
            get { return this.logininfo.ServerName; }
        }
    }
}
