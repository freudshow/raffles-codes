using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace jzpl.Lib
{
    public class DeliveryVoucher
    {
        private string m_receiver;
        private string m_receive_date;
        private string m_receive_place;
        private string m_location;
        private ArrayList m_delivery_items;
        private DeliveryVoucherLine[] m_delievery_voucher_lines;
        private string m_delivery_voucher_no;

        public DeliveryVoucher()
        {

        }
        public DeliveryVoucher(string voucher_no)
        {
            DataView dv;
            dv = DBHelper.createDataset(string.Format("select * from jp_jjd where jjd_no = '{0}'", voucher_no)).Tables[0].DefaultView;
            if (dv.Count > 0)
            {
                m_delivery_voucher_no = dv[0]["jjd_no"].ToString();
                m_receiver = dv[0]["receiver"].ToString();
                m_receive_date = dv[0]["receiver_date"].ToString();
                m_receive_place = dv[0]["receiver_place"].ToString();
                m_location = dv[0]["location"].ToString();

                dv = DBHelper.createDataset(string.Format("select * from jp_jjd_line where jjd_no ='{0}'", voucher_no)).Tables[0].DefaultView;
                for (int i = 0; i < dv.Count; i++)
                {
                    m_delievery_voucher_lines[i] = new DeliveryVoucherLine();
                    m_delievery_voucher_lines[i].JjdNo = voucher_no;
                    m_delievery_voucher_lines[i].LocationGrid = dv[i]["location_grid"].ToString();
                    m_delievery_voucher_lines[i].MatrSeqNo = dv[i]["matr_seq_no"].ToString();
                    m_delievery_voucher_lines[i].PartDescription = dv[i]["part_description"].ToString();
                    m_delievery_voucher_lines[i].PartNo = dv[i]["part_no"].ToString();
                    m_delievery_voucher_lines[i].PartUnit = dv[i]["part_unit"].ToString();
                    m_delievery_voucher_lines[i].ProjectId = dv[i]["project_id"].ToString();
                    m_delievery_voucher_lines[i].Qty = dv[i]["qty"].ToString();
                    m_delievery_voucher_lines[i].Remark = dv[i]["remark"].ToString();
                    m_delievery_voucher_lines[i].RequisitionId = dv[i]["requisition"].ToString();
                    m_delievery_voucher_lines[i].Rowstate = dv[i]["rowstate"].ToString();
                    m_delievery_voucher_lines[i].Rowversion = dv[i]["rowversion"].ToString();
                }
            }
        }
        public void SetDeliveryVoucherNo()
        {
            m_delivery_voucher_no = Lib.DBHelper.getObject("select jp_public.get_delivery_voucher_no from dual").ToString();
        }
        public string Receiver
        {
            get { return m_receiver; }
            set { m_receiver = value; }
        }
        public string ReceiveDate
        {
            get { return m_receive_date; }
            set { m_receive_date = value; }
        }
        public string ReceivePlace
        {
            get { return m_receive_place; }
            set { m_receive_place = value; }
        }
        public ArrayList DeliveryItems
        {
            get { return m_delivery_items; }
            set { m_delivery_items = value; }
        }
        public string DeliveryVoucherNo
        {
            get { return m_delivery_voucher_no; }
        }

        public DeliveryVoucherLine[] DeliveryVoucherLines
        {
            get { return m_delievery_voucher_lines; }
            set { m_delievery_voucher_lines = value; }
        }

        public class DeliveryVoucherLine
        {
            private string m_jjd_no;
            private string m_requisition_id;
            private string m_rowstate;
            private string m_remark;
            private string m_location_grid;
            private string m_project_id;
            private string m_matr_seq_no;
            private string m_part_no;
            private string m_part_description;
            private string m_part_unit;
            private string m_qty;
            private string m_rowversion;

            public DeliveryVoucherLine()
            {

            }

            public DeliveryVoucherLine(string voucher_no, string requisition_id)
            {

                DataView dv;
                dv = DBHelper.createDataset(string.Format("select * from jp_jjd_line where jjd_no = '{0}' and requisition_id ='{1}'", voucher_no, requisition_id)).Tables[0].DefaultView;
                if (dv.Count > 0)
                {
                    m_jjd_no = voucher_no;
                    m_requisition_id = requisition_id;
                    m_rowstate = dv[0]["rowstate"].ToString();
                    m_remark = dv[0]["remark"].ToString();
                    m_location_grid = dv[0]["location_grid"].ToString();
                    m_project_id = dv[0]["project_id"].ToString();
                    m_matr_seq_no = dv[0]["matr_seq_no"].ToString();
                    m_part_no = dv[0]["part_no"].ToString();
                    m_part_description = dv[0]["part_description"].ToString();
                    m_part_unit = dv[0]["part_unit"].ToString();
                    m_qty = dv[0]["qty"].ToString();
                    m_rowversion = dv[0]["rowversion"].ToString();
                }
            }
            #region  Ù–‘
            public string JjdNo
            {
                get { return m_jjd_no; }
                set { m_jjd_no = value; }
            }
            public string RequisitionId
            {
                get { return m_jjd_no; }
                set { m_jjd_no = value; }
            }
            public string Rowstate
            {
                get { return m_rowstate; }
                set { m_rowstate = value; }
            }
            public string Remark
            {
                get { return m_remark; }
                set { m_remark = value; }
            }
            public string LocationGrid
            {
                get { return m_location_grid; }
                set { m_location_grid = value; }
            }
            public string ProjectId
            {
                get { return m_project_id; }
                set { m_project_id = value; }
            }
            public string MatrSeqNo
            {
                get { return m_matr_seq_no; }
                set { m_matr_seq_no = value; }
            }
            public string PartNo
            {
                get { return m_part_no; }
                set { m_part_no = value; }
            }
            public string PartDescription
            {
                get { return m_part_description; }
                set { m_part_description = value; }
            }
            public string PartUnit
            {
                get { return m_part_unit; }
                set { m_part_unit = value; }
            }
            public string Qty
            {
                get { return m_qty; }
                set { m_qty = value; }
            }
            public string Rowversion
            {
                get { return m_rowversion; }
                set { m_rowversion = value; }
            }
            #endregion

        }


    }
    
}
