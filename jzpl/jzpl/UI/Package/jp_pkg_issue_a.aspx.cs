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

namespace jzpl.UI.Package
{
    public partial class jp_pkg_issue_a : System.Web.UI.Page
    {
        private string m_perimission;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
            Authentication auth = new Authentication(this);
            if (auth.LoadSession() == false)
            {
                auth.RemoveSession();
                Response.Redirect("../../UI/FrameUI/login.htm");
                Response.End();
            }
            else
            {
                m_perimission = ((Authentication.LOGININFO)Session["USERINFO"]).Permission;
                if (CheckAccessAble())
                {
                    if (!IsPostBack)
                    {
                        string id_ = Server.HtmlDecode(Misc.GetHtmlRequestValue(Request, "id"));
                        string objid_ = Misc.GetHtmlRequestValue(Request, "objid");
                        string rowversion_ = Server.HtmlDecode(Misc.GetHtmlRequestValue(Request, "ver"));
                        string packageNo_ = Server.HtmlDecode(Misc.GetHtmlRequestValue(Request, "pkg"));
                        string partNo_ = Server.HtmlDecode(Misc.GetHtmlRequestValue(Request, "part"));
                        decimal releaseQty_ = Convert.ToDecimal(Server.HtmlDecode(Misc.GetHtmlRequestValue(Request, "qty")));
                        decimal issueQty_ = Misc.DBStrToNumber(Server.HtmlDecode(Misc.GetHtmlRequestValue(Request, "iss")));

                        GenPkgPart pkg = new GenPkgPart(packageNo_, partNo_);

                        LblReqId.Text = id_;
                        LblPackageNo.Text = packageNo_;
                        LblPackageName.Text = pkg.PackageName;
                        LblPartNo.Text = partNo_;
                        LblPartName.Text = pkg.PartNameE;
                        LblPartSpec.Text = pkg.PartSpec;
                        LblReleasedQty.Text = releaseQty_.ToString();
                        LblIssuedQty.Text = issueQty_.ToString();

                        HiddenObjid.Value = objid_;
                        HiddenRowversion.Value = rowversion_;

                        GVDataDataBind(packageNo_, partNo_);
                    }
                }
                else
                {
                    auth.RemoveSession();
                    Response.Redirect("../../UI/FrameUI/login.htm");
                    Response.End();
                }
            }
        }

        protected Boolean CheckAccessAble()
        {
            if (m_perimission[(int)Authentication.PERMDEFINE.PKG_JP_ISS] == '1') return true;
            return false;
        }   

        private void GVDataDataBind(string packageNo_, string partNo_)
        {
            StringBuilder sql = new StringBuilder(string.Format("select * from gen_pkg_part_in_store_v where package_no='{0}' and part_no='{1}'", packageNo_, partNo_));
            if (TxtLocation.Text.Trim() != "")
            {
                sql.Append(string.Format(" and (area like '%{0}%' or location like '%{0}%')", TxtLocation.Text));
            }

            GVData.DataSource = DBHelper.createGridView(sql.ToString());
            GVData.DataBind();
        }

        protected void BtnResult_Click(object sender, EventArgs e)
        {
            GVDataDataBind(LblPackageNo.Text, LblPartNo.Text);
        }

        protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string GVLblOkQtyID = e.Row.FindControl("GVLblOkQty").ClientID;
                string GVLblNochkQtyID = e.Row.FindControl("GVLblNochkQty").ClientID;
                
                ((LinkButton)(e.Row.FindControl("GVLnkOkQty"))).Attributes.Add("onclick",
                    string.Format("return IssueQtyLnk_Click('{0}','{1}');",DataBinder.Eval(e.Row.DataItem, "part_ok_qty").ToString(), GVLblOkQtyID));
                ((LinkButton)(e.Row.FindControl("GVLnkNochkQty"))).Attributes.Add("onclick",
                    string.Format("return IssueQtyLnk_Click('{0}','{1}');", DataBinder.Eval(e.Row.DataItem, "no_check_qty").ToString(), GVLblNochkQtyID));
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string ReqId = LblReqId.Text;
            string ReqObjId = HiddenObjid.Value;
            string ReqRowversion = HiddenRowversion.Value;
            decimal ReqReleasedQty = Convert.ToDecimal(LblReleasedQty.Text);            
            decimal IssueOkQty;
            decimal IssueNochkQty;
            decimal TotalIssueQty=0;
            
            TextBox LblOkQty;
            TextBox LblNochkQty;

            //跟新申请行，调用jp_pkg_requisition_api.issue_
            //参数：objid rowversion issued_qty
            //遍历gridview累计下发数量
            foreach (GridViewRow gvr in GVData.Rows)
            {
                
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    
                    LblOkQty = (TextBox)(gvr.Cells[2].FindControl("GVLblOkQty"));
                    LblNochkQty = (TextBox)(gvr.Cells[2].FindControl("GVLblNochkQty"));
                    IssueOkQty = LblOkQty.Text == "" ? 0 : Convert.ToDecimal(LblOkQty.Text);
                    IssueNochkQty = LblNochkQty.Text == "" ? 0 : Convert.ToDecimal(LblNochkQty.Text);
                    TotalIssueQty = TotalIssueQty + IssueOkQty + IssueNochkQty;
                    
                }
            }

            if (TotalIssueQty > ReqReleasedQty)
            {
                Misc.Message(this.GetType(), ClientScript, "保存失败，下发数量超过了下达数量。");
                return;
            }

            //1、update jp_pkg_requisition (下发数量，下发日期，状态，时间戳)
            ////（old）暂时控制申请只能一次下发，即下发后状态更新为issued，不能再进行下发，无论是否已经全部下发
            //可以多次下发，下发后状态保持released，直到全部下发
            //2、update gen_package_part_in_store 减少零件现有量
            //3、记录下发细节gen_pkg_issue 
            //4、撤销预留

            using (OleDbConnection conn = new OleDbConnection(DBHelper.OleConnectionString))
            {
                conn.Open();
                OleDbTransaction tr = conn.BeginTransaction();
                try
                {   
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tr;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.CommandText = "jp_pkg_requisition_api.issue_";
                    cmd.Parameters.Add("v_req_objid", OleDbType.VarChar).Value = ReqObjId;
                    cmd.Parameters.Add("v_req_rowversion", OleDbType.VarChar).Value = ReqRowversion;
                    cmd.Parameters.Add("v_qty", OleDbType.Decimal).Value = TotalIssueQty;
                    //cmd.Parameters.Add("v_msg", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.CommandText = "gen_package_part_in_store_api.issue_";
                    cmd.Parameters.Add("v_requisition_id",OleDbType.VarChar).Value=ReqId;
                    cmd.Parameters.Add("v_objid",OleDbType.VarChar);
                    cmd.Parameters.Add("v_rowversion",OleDbType.VarChar);
                    cmd.Parameters.Add("v_type",OleDbType.VarChar);
                    cmd.Parameters.Add("v_qty", OleDbType.Decimal);           

                    foreach (GridViewRow gvr in GVData.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            LblOkQty = (TextBox)(gvr.FindControl("GVLblOkQty"));
                            LblNochkQty = (TextBox)(gvr.FindControl("GVLblNochkQty"));
                            IssueOkQty = LblOkQty.Text == "" ? 0 : Convert.ToDecimal(LblOkQty.Text);
                            IssueNochkQty = LblNochkQty.Text == "" ? 0 : Convert.ToDecimal(LblNochkQty.Text);
                            cmd.Parameters["v_objid"].Value = gvr.Cells[0].Text;
                            cmd.Parameters["v_rowversion"].Value = gvr.Cells[1].Text;
                            if (IssueOkQty == 0 && IssueNochkQty == 0)
                            {
                                continue;
                            }
                            if (IssueNochkQty > 0)
                            {
                                cmd.Parameters["v_type"].Value = "nocheck";
                                cmd.Parameters["v_qty"].Value = IssueNochkQty;
                                cmd.ExecuteNonQuery();
                            }
                            if (IssueOkQty > 0)
                            {
                                cmd.Parameters["v_type"].Value = "ok";
                                cmd.Parameters["v_qty"].Value = IssueOkQty;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tr.Commit();
                    Misc.RegisterClientScript(this.GetType(),"issue_refresh", ClientScript, "<script type='text/javascript'>window.dialogArguments.refresh();window.close();</script>");
                }
                catch (OleDbException ex)
                {
                    tr.Rollback();
                    if (Misc.CheckIsDBCustomException(ex))
                    {
                        Misc.RegisterClientScript(this.GetType(), "issue_refresh", ClientScript, string.Format("<script type='text/javascript'>alert('{0}')</script>", Misc.GetDBCustomException(ex)));
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    conn.Close();                    
                }
            }
        }
    }
}
