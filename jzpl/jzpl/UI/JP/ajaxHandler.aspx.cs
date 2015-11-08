using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace jzpl.UI.JP
{
    public partial class ajaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mode = Lib.Misc.GetHtmlRequestValue(Request, "mode");
            switch (mode)
            {
                case "getpname":
                    GetPartName();
                    break;
                case "getreceiverlist":
                    GetReceiverList();
                    break;
                case "getpordplace":
                    GetProdPlace();
                    break;
                case "getlocationlist":
                    GetLoactionList();
                    break;
                default:
                    break;
            }
        }
        private void GetPartName()
        {
            string partNo = Lib.Misc.GetHtmlRequestValue(Request, "pno");
            string project = Lib.Misc.GetHtmlRequestValue(Request,"proj");
            object objPart = Lib.DBHelper.getObject("select description from ifsapp.inventory_part@erp_prod where ifsapp.site_api.Get_Company@erp_prod(contract) = ifsapp.project_api.Get_Company@erp_prod('" + project + "') and part_no = '" + partNo + "'");
            string partName = objPart==null?"":objPart.ToString();
            Response.AddHeader("Content-Type", "text/plain");
            Response.Write(partName);
            Response.End();
        }
        private void GetReceiverList()
        {
            string receiveDate = Lib.Misc.GetHtmlRequestValue(Request, "date");
            string prodSite = Lib.Misc.GetHtmlRequestValue(Request, "prodsite");
            DataView receivers = Lib.DBHelper.createGridView(string.Format("select distinct receiver from jp_requisition where receive_date =  to_date('{0}','yyyy-mm-dd') and place = '{1}'", receiveDate,prodSite));
            string strReceivers = "";
            for (int i = 0; i < receivers.Count; i++)
            {
                strReceivers += receivers[i][0].ToString();
                if (i < receivers.Count - 1)
                {
                    strReceivers += ";";
                }
            }
            Response.AddHeader("Content-type", "text/plain");
            Response.Write(strReceivers);
            Response.End();
        }
        private void GetProdPlace()
        {
            StringBuilder xmlContent = new StringBuilder("<xml id=\"prodPlace\">");
            string receiveDate = Lib.Misc.GetHtmlRequestValue(Request, "date");
            DataSet ds = Lib.DBHelper.createDataset(string.Format("select distinct place place_id,place||'  '||place_description site_des from jp_requisition where receive_date=to_date('{0}','yyyy-mm-dd')", receiveDate));
            DataView dv = ds.Tables[0].DefaultView;
            for(int i=0;i<dv.Count;i++)
            {
                xmlContent.Append("<recordSet>");
                for (int j = 0; j < dv.Table.Columns.Count; j++)
                {
                    xmlContent.Append(string.Format("<{0}>{1}</{0}>",dv.Table.Columns[j].ColumnName,dv[i][j].ToString()));
                }
                xmlContent.Append("</recordSet>");
            }
            xmlContent.Append("</xml>");
            //xmlContent = ds.GetXml();
            Response.AddHeader("Content-type", "text/xml");
            Response.Write(xmlContent.ToString());
            Response.End();            
        }
        private void GetLoactionList()
        {
            string receiveDate = Lib.Misc.GetHtmlRequestValue(Request, "date");
            string prodSite = Lib.Misc.GetHtmlRequestValue(Request, "prodsite");
            string reciever = Lib.Misc.GetHtmlRequestValue(Request, "reciever");
            DataView DVLocations = Lib.DBHelper.createGridView(string.Format("select distinct IFS_DATA_API.GET_BAY_NO_FOR_PART(part_no,contract,project_id) from jp_requisition where receive_date =  to_date('{0}','yyyy-mm-dd') and place = '{1}' and receiver='{2}'", receiveDate, prodSite, reciever));
            string locations = "";
            for (int i = 0; i < DVLocations.Count; i++)
            {
                locations += DVLocations[i][0].ToString();
                if (i < DVLocations.Count - 1)
                {
                    locations += ";";
                }
            }
            Response.AddHeader("Content-type", "text/plain");
            Response.Write(locations);
            Response.End();
        }
    }
}
