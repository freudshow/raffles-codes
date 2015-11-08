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
using jzpl.Lib;
using System.Text;
using System.Data.OleDb;

namespace jzpl.UI.ADMIN
{
    public partial class user_manage : System.Web.UI.Page
    {
        Authentication m_auth = null;
        string m_user = "";
        string m_permission ="";
        string[] m_permission_array;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_auth = new Authentication(this);            
            InitPermissionArray();
            //InitPermissionString();
            if (!IsPostBack)
            {
                BtnSave.Visible = false;
                DdlUserDataBind();
            }

            if (DdlUser.SelectedValue != "0")
            {
                m_user = DdlUser.SelectedValue;
                m_permission = DBHelper.getObject(string.Format("select role from jp_user where user_id='{0}'", DdlUser.SelectedValue)).ToString();
            }
        }

        protected void DdlUserDataBind()
        {
            DdlUser.DataSource = DBHelper.createDDLView("select user_id value,user_id text from jp_user where admin='0'");
            DdlUser.DataTextField = "text";
            DdlUser.DataValueField = "value";
            DdlUser.DataBind();
        }

        protected void SetClientPermission()
        {            
            //StringBuilder HTML_ = new StringBuilder();
                        
            //string[] permission_array_ = m_permission.Split('|');
            ////数据库存储permission位与m_permission_array位同步
            //for (int i = 0; i < m_permission_array.Length; i++)
            //{
            //    if (i == permission_array_.Length) break;
            //    permission_array_[i] += "000000000";
            //    m_permission_array[i] = permission_array_[i].Substring(0, m_permission_array[i].Length);                
            //}
            //HTML_.Append("<div>");
            //HTML_.Append("<table class=\"permission\" id=\"permissionTable\" cellspacing=\"0\"  cellpadding=\"0\" ><tr><th>Page</th><th>Access</th><th>Function</th></tr>");            
            //for (int i = 0,index_=0; i < m_auth.PERMISSION_PAGE.Length; i++)
            //{                
            //    System.Type t = System.Type.GetType("jzpl."+m_auth.PERMISSION_PAGE[i]);
            //    Lib.IBasePage p = (Lib.IBasePage)Activator.CreateInstance(t);
            //    HTML_.Append(string.Format("<tr id=\"{0}\"><td>{0}</td><td><input type=\"checkbox\" name=\"Access\" {1} >Access</input></td>", m_auth.PERMISSION_PAGE[i], BitToChecked(m_permission_array[i][index_++])));
            //    HTML_.Append("<td>");
            //    if (p.GetPermissionType() == Authentication.PERMISSION_TYPE.function_.ToString())
            //    {
            //        string[] funArray_ = p.GetFun();
            //        for (int j = 0; j < funArray_.Length; j++)
            //        {
            //            HTML_.Append(string.Format("<input type=\"checkbox\" name=\"{0}\" {1}>{0}</input>", funArray_[j],BitToChecked(m_permission_array[i][index_++]) ));
            //        }
            //    }
            //    else
            //    {
            //        HTML_.Append("&nbsp");
            //    }
            //    HTML_.Append("</td></tr>");
            //    index_ = 0; 
            //}
            //HTML_.Append("</table></div>");
            //LiteralControl Thead = new LiteralControl(HTML_.ToString());
            //PH1.Controls.Add(Thead);
            //BtnSave.Visible = true;
        }

        protected void DdlUser_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (DdlUser.SelectedValue != "0")
            {                
                SetClientPermission();
            }
            else
            {
                BtnSave.Visible = false;
            }
        }

        protected string BitToChecked(char bit)
        {            
            if (bit == '1') return "checked";
            return "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //save code
            string permission_ = Request.Form["clientPermission"];
            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand(string.Format("update jp_user set role='{0}' where user_id='{1}'", permission_, m_user), conn);
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.ExecuteNonQuery();
            }
            m_permission = DBHelper.getObject(string.Format("select role from jp_user where user_id='{0}'", DdlUser.SelectedValue)).ToString();
            SetClientPermission();
        }

        protected void InitPermissionArray()
        {
            //int range_;
            //range_ =Enum.GetValues(typeof(Authentication.FUN_INTERFACE)).Length;
            //m_permission_array = new string[range_];
            //for (int i = 0; i < range_; i++)
            //{
            //    m_permission_array[i] = "0";//access位初始值
            //    //System.Type t = System.Type.GetType("jzpl." + m_auth.PERMISSION_PAGE[i]);
            //    //Lib.IBasePage p = (Lib.IBasePage)Activator.CreateInstance(t);
            //    //if (p.GetPermissionType() == Authentication.PERMISSION_TYPE.page_.ToString()) continue;
            //    for (int j = 0; j < p.GetFun().Length; j++)
            //    {
            //        m_permission_array[i] += "0";
            //    }                
            //}            
        }        
    }
}
