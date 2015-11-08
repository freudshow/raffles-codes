using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace jzpl.Lib
{
    public class PkgJjd
    {
        private string jjd_no;
        private string place_id;
        private string place_name;
        private string receipt_person;
        private string receipt_contract;
        private string receipt_date;
        private string zh_place;
        private string zh_person;
        private string zh_contract;
        private string zh_star_date;
        private string zh_end_date;
        private string reg_group_id;
        private string cy_company;
        private string cy_person;
        private string cy_per_contract;
        private string cy_car_no;
        private string cy_doc;
        private string xh_star_date;
        private string xh_end_date;
        private string rowversion;
        private string safe;
        private string state;
        private string receipt_dept;
        private string zh_arr_time;
        private string xq_dept;
        private string xq_person;
        private string xq_contract;
        private string state_ch;

        public PkgJjd(string jjd_no_)
        {
            string sql;
            DataTable dt;
            sql = string.Format("select * from jp_pkg_jjd_v where jjd_no ='{0}'", jjd_no_);
            dt = DBHelper.createDataset(sql).Tables[0];

            jjd_no=jjd_no_;
            place_id = dt.Rows[0]["place_id"].ToString();
            place_name = dt.Rows[0]["place_name"].ToString();
            receipt_person = dt.Rows[0]["receipt_person"].ToString();
            receipt_contract = dt.Rows[0]["receipt_contract"].ToString();
            receipt_date = dt.Rows[0]["receipt_date_str"].ToString();
            zh_place = dt.Rows[0]["zh_place"].ToString();
            zh_person = dt.Rows[0]["zh_person"].ToString();
            zh_contract = dt.Rows[0]["zh_contract"].ToString();
            zh_arr_time = dt.Rows[0]["zh_arr_time"].ToString();
            zh_star_date = dt.Rows[0]["zh_star_date"].ToString();
            zh_end_date = dt.Rows[0]["zh_end_date"].ToString();
            reg_group_id = dt.Rows[0]["reg_group_id"].ToString();
            cy_company = dt.Rows[0]["cy_company"].ToString();
            cy_person = dt.Rows[0]["cy_person"].ToString();
            cy_per_contract = dt.Rows[0]["cy_per_contract"].ToString();
            cy_car_no = dt.Rows[0]["cy_car_no"].ToString();
            cy_doc = dt.Rows[0]["cy_doc"].ToString();
            xh_star_date = dt.Rows[0]["xh_star_date"].ToString();
            xh_end_date = dt.Rows[0]["xh_end_date"].ToString();
            rowversion = dt.Rows[0]["rowversion"].ToString();
            safe = dt.Rows[0]["safe"].ToString();
            state = dt.Rows[0]["state"].ToString();
            receipt_dept = dt.Rows[0]["receipt_dept"].ToString();
            xq_dept = dt.Rows[0]["xq_dept"].ToString();
            xq_person = dt.Rows[0]["xq_person"].ToString();
            xq_contract = dt.Rows[0]["xq_contract"].ToString();
            state_ch = dt.Rows[0]["state_ch"].ToString();
        }

        public string JjdNo
        {
            get { return jjd_no; }
        }
        public string PlaceId
        {
            get { return place_id; }
        }
        public string PlaceName
        {
            get { return place_name; }
        }
        public string ReceiptPerson
        {
            get { return receipt_person; }
        }
        public string ReceiptContract
        {
            get { return receipt_contract; }
        }
        public string ReceiptDate
        {
            get { return receipt_date; }
        }
        public string ZhPlace
        {
            get { return zh_place; }
        }
        public string ZhPerson
        {
            get { return zh_person; }
        }
        public string ZhContract
        {
            get { return zh_contract; }
        }
        
        public string ZhStarDate
        {
            get { return zh_star_date; }
        }
        public string ZhEndDate
        {
            get { return zh_end_date; }
        }
        public string RegGroupID
        {
            get { return reg_group_id; }
        }
        public string CyCompany
        {
            get { return cy_company; }
        }
        public string CyPerson
        {
            get { return cy_person; }
        }
        public string CyContract
        {
            get { return cy_per_contract; }
        }
        public string CycCarNo
        {
            get { return cy_car_no; }
        }
        public string CyDoc
        {
            get { return cy_doc; }
        }
        public string XhStarDate
        {
            get { return xh_star_date; }
        }
        public string XhEndDate
        {
            get { return xh_end_date; }
        }
        public string RowVersion
        {
            get { return rowversion; }
        }
        public string Safe
        {
            get { return safe; }
        }
        public string State
        {
            get { return state; }
        }
        public string ReceiptDept
        {
            get { return receipt_dept; }
        }
        public string ZhArrTime
        {
            get { return zh_arr_time; }
        }
        public string XQDept
        {
            get { return xq_dept; }
        }
        public string XQPerson
        {
            get { return xq_person; }
        }
        public string XQContract
        {
            get { return xq_contract; }
        }
        public string StateCh
        {
            get { return state_ch; }
        }
    }
}
