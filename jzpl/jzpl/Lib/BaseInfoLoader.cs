using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace jzpl.Lib
{
    public class BaseInfoLoader
    {

        public BaseInfoLoader() { }
        public void CompanyDropDrownListLoad(DropDownList ddl,Boolean onlyCode,Boolean limitState,Boolean noSelected,string default_ )
        {
            StringBuilder sql = new StringBuilder();
            if (onlyCode)
            {
                sql.Append("select company_id,cimpany_id company from jp_company");
            }
            else
            {
                sql.Append("select company_id,company_id||' '||company company from jp_company");
            }
            if (limitState)
            {
                sql.Append(" where state='1'");
            }
            if (noSelected)
            {
                ddl.DataSource = DBHelper.createDDLView(sql.ToString());
            }
            else
            {
                ddl.DataSource = DBHelper.createGridView(sql.ToString());
            }
            ddl.DataTextField = "company";
            ddl.DataValueField = "company_id";
            ddl.DataBind();

            if (default_ != string.Empty)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(default_));
            }
        }

        public void ProjectDropDownListLoad(DropDownList ddl, Boolean onlyCode, Boolean limitState, string userid)
        {
            StringBuilder sql = new StringBuilder();
            DataView dv = new DataView();
            //userid==string.Empty 不通过用户访问控制列表限制项目加载
            if (userid == string.Empty)
            {
                if (onlyCode)
                {
                    sql.Append("select project_id value_,project_id text_ from jp_project");
                }
                else
                {
                    sql.Append("select project_id value_,project_id||'  '||project_name text_ from jp_project");
                }
                if (limitState)
                {
                    sql.Append(" where state='1'");
                }

            }
            //userid!=string.Empty 通过用户访问控制列表限制项目加载
            else
            {
                //用户可访问项目为“%”，用户可访问所有项目
                int n = DBHelper.getCount(string.Format("select count(*) from jp_project_access_person where user_id='{0}' and project_id='%'", userid));
                if (n > 0)
                {
                    if (onlyCode)
                    {
                        sql.Append("select project_id value_,project_id text_ from jp_project");
                    }
                    else
                    {
                        sql.Append("select project_id value_,project_id||'  '||project_name text_ from jp_project");
                    }
                    if (limitState)
                    {
                        sql.Append(" where state='1'");
                    }

                }
                else
                {
                    if (onlyCode)
                    {
                        sql.Append(string.Format("select project_id value_,project_id text_ from jp_project_access_person  where user_id='{0}'", userid));
                    }
                    else
                    {
                        sql.Append(string.Format("select project_id value_,project_id||'  '||jp_project_api.get_name(project_id) text_ from jp_project_access_person where user_id='{0}'", userid));
                    }
                    if (limitState)
                    {
                        sql.Append(" and project_id in (select project_id from jp_project where state='1')");
                    }
                }
            }
            sql.Append(" order by project_id desc");

            dv = DBHelper.createDataset(sql.ToString()).Tables[0].DefaultView;

            ddl.Items.Clear();
            int m = dv.Count;
            switch (m)
            {
                case 0:
                    ddl.Items.Add(new ListItem("无可访问项目", "-1"));
                    ddl.DataBind();
                    ddl.Enabled = false;
                    return;                    
                case 1:
                    break;
                default:
                    DataRow dr = dv.Table.NewRow();
                    dr["value_"] = "0";
                    dr["text_"] = "请选择...";
                    dv.Table.Rows.InsertAt(dr, 0);
                    break;
            }
            ddl.DataSource = dv;
            ddl.DataTextField = "text_";
            ddl.DataValueField = "value_";
            ddl.DataBind();
        }

        public void PartUnitDropDownListLoad(DropDownList ddl, Boolean onlyCode, Boolean limitState)
        {
            StringBuilder sql = new StringBuilder();
            if (onlyCode)
            {
                sql.Append("select unit value_,unit text_ from jp_part_unit");
            }
            else
            {
                sql.Append("select unit value_,unit||'  '||unit_desc text_ from jp_part_unit");
            }
            if (limitState)
            {
                sql.Append(" where is_valid='1'");
            }

            ddl.DataSource = DBHelper.createDDLView(sql.ToString());
            ddl.DataTextField = "text_";
            ddl.DataValueField = "value_";
            ddl.DataBind();
        }

        public void CurrencyDropDownListLoad(DropDownList ddl, Boolean onlyCode, Boolean limitState)
        {
            StringBuilder sql = new StringBuilder();
            if (onlyCode)
            {
                sql.Append("select currency value_, currency text_ from jp_currency" );
            }
            else
            {
                sql.Append("select currency value_, currency||' '||currency_desc text_ from jp_currency ");
            }
            if (limitState)
            {
                sql.Append(" where is_valid='1'");
            }

            ddl.DataSource = DBHelper.createDDLView(sql.ToString());
            ddl.DataTextField = "text_";
            ddl.DataValueField = "value_";
            ddl.DataBind();
        }
       
        
    }
}
