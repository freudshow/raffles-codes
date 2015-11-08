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
    public class GenPkgPartBase
    {
        private string objversion;
        private string objid;
        private string project_id;
        private string project_name;
        private string package_no;
        private string package_name;
        private string po_no;
        private string part_no;
        private string part_name;
        private string part_name_e;
        private string part_spec;
        private string dec_no;
        private string contract_no;
        private string unit;
        private decimal reserved_qty;
        private string rowversion;
        private string currency;
        private decimal package_value;
        private string pay_flag;
        private string pic_path;

        public GenPkgPartBase(string PkgNo, string PartNo)
        {
            string sql;
            DataTable dt;
            sql = string.Format("select * from gen_part_package_item_v where package_no ='{0}' and part_no='{1}'", PkgNo, PartNo);
            dt=DBHelper.createDataset(sql).Tables[0];
            objversion = dt.Rows[0]["rowversion"].ToString();
            objid = dt.Rows[0]["objid"].ToString();
            project_id = dt.Rows[0]["project_id"].ToString();
            project_name = dt.Rows[0]["project_name"].ToString();
            package_no = dt.Rows[0]["package_no"].ToString();
            package_name = dt.Rows[0]["package_name"].ToString();
            po_no = dt.Rows[0]["po_no"].ToString();
            part_no = dt.Rows[0]["part_no"].ToString();
            part_name = dt.Rows[0]["part_name"].ToString();
            part_name_e = dt.Rows[0]["part_name_e"].ToString();
            part_spec = dt.Rows[0]["part_spec"].ToString();
            dec_no = dt.Rows[0]["dec_no"].ToString();
            contract_no = dt.Rows[0]["contract_no"].ToString();
            unit = dt.Rows[0]["unit"].ToString();
            reserved_qty = Misc.DBStrToNumber(dt.Rows[0]["reserved_qty"].ToString());
            rowversion = dt.Rows[0]["rowversion"].ToString();
            currency = dt.Rows[0]["currency"].ToString();
            package_value = Misc.DBStrToNumber(dt.Rows[0]["package_value"].ToString());
            pay_flag = dt.Rows[0]["pay_flag"].ToString();
            pic_path = dt.Rows[0]["pic_path"].ToString();

        }
        public string ObjVersion
        {
            get { return objversion; }
        }
        public string ObjId
        {
            get { return objid; }
        }
        public string ProjectId
        {
            get { return project_id; }
        }
        public string ProjectName
        {
            get { return project_name; }
        }
        public string PackageNo
        {
            get { return package_no; }
        }
        public string PackageName
        {
            get { return package_name; }
        }
        public string PO
        {
            get { return po_no; }
        }
        public string PartNo
        {
            get { return part_no; }
        }
        public string PartName
        {
            get { return part_name; }
        }
        public string PartNameE
        {
            get { return part_name_e; }
        }
        public string PartSpec
        {
            get { return part_spec; }
        }
        public string DecNo
        {
            get { return dec_no; }
        }
        public string ContractNo
        {
            get { return contract_no; }
        }
        public string Unit
        {
            get { return unit; }
        }
        public decimal ReservedQty
        {
            get { return reserved_qty; }
        }
        public string Rowversion
        {
            get { return rowversion; }
        }
        public string Currency
        {
            get { return currency; }
        }
        public decimal PackageValue
        {
            get { return package_value; }
        }
        public string PayFlag
        {
            get { return pay_flag; }
        }
        public string PicPath
        {
            get { return pic_path; }
        }

    }
}
