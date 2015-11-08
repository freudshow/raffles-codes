<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wzxqjh_query.aspx.cs" Inherits="jzpl.wzxqjh_query" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>普通物资申请变更</title>
   
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript" src="../../UI/script/wzxqjh.js"></script>
    <script type="text/javascript">
    window.onload=function(){init();}
    
    function init() {
    
    SetGVBoxHeight("gvbox","GVData");
    }    
    
    function open_mod_page(reqid,objid,rowversion)
    {
        //window.open("wzxqjh_mod.aspx?id="+reqid+"&objid="+objid+"&ver="+rowversion+"",window,"status:no;scrollbars:no,dialogWidth:490px;dialogHeight:600px"); 
        window.open("wzxqjh_mod.aspx?id="+reqid+"&objid="+objid+"&ver="+rowversion+"",'mod2','height=600px, width=490px, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');    
        //return false;
    }
    
    </script>
    <style type="text/css">
        .gv
        {
            margin-right: 21px;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server" >
    <div id="title">普通物资申请变更<hr /></div>
        <table cellpadding="0" cellspacing="0" >
            <tr>
                <td>
                    项目：</td>
                <td colspan="4">
                    <asp:DropDownList ID="DdlProject" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <span style="color: #ff0000"></span>
                </td>
                <td>
                </td>
                <td>
                    </td>
                <td>
                    </td>
                <td style="width: 12px">
                    <span style="color: #ff0000"></span>
                </td>
                <td style="width: 12px">
                </td>
                <td style="width: 12px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    材料顺序号：</td>
                <td>
                    <asp:TextBox ID="TxtMtrSeqNo" runat="server"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td>
                    行号：</td>
                <td>
                    <asp:TextBox ID="TxtMtrLineNo" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    &nbsp;&nbsp;</td>
                <td>
                    物资编码：</td>
                <td>
                    <asp:TextBox ID="TxtPartNo" runat="server"></asp:TextBox></td>
                <td style="width: 12px">
                    <span style="color: #ff0000">%</span></td>
                <td >
                    </td>
                <td >
                    </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    接收日期：</td>
                <td>
                    <asp:TextBox ID="TxtRecieveDate" runat="server" onfocus="WdatePicker();"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    接收人：</td>
                <td>
                    <asp:TextBox ID="TxtReciever" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    接收地：</td>
                <td>
                    <asp:DropDownList ID="DdlProdSite" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 12px">
                </td>
                <td style="width: 12px">
                </td>
                <td style="width: 12px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    接收部门：</td>
                <td>
                    <asp:DropDownList ID="DdlReceiptDept" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                    申请组：</td>
                <td>
                    <asp:DropDownList ID="DdlReqGroup" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    库位：</td>
                <td>
                    <asp:TextBox ID="TxtLocation" runat="server"></asp:TextBox></td>
                <td style="width: 12px">
                </td>
                <td style="width: 12px">
                </td>
                <td style="width: 12px">
                </td>
                <td>
                </td>
            </tr>
        </table>
        <hr />
        <table cellspacing="0" cellpadding="0" style="padding: 0; margin-bottom:4px " width="100%">
            <tr>
                <td style="width: 200px">
                    <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" CssClass="button_1" />&nbsp;
                    <asp:Button ID="BtnHtmlToExcel" runat="server" Text="EXCEL" CssClass="button_1" OnClick="BtnHtmlToExcel_Click"/></td>
                <td style="width: 100px">
                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton></td>
                <td>
                </td>
            </tr>
        </table>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" 
                ForeColor="#333333" CssClass="gv"
                Width="1760px" AllowPaging="True" OnPageIndexChanging="GVData_PageIndexChanging"
                PageSize="12" OnRowDataBound="GVData_RowDataBound" 
                OnRowCommand="GVData_RowCommand" EnableModelValidation="True">
                
                <Columns>
                    <asp:BoundField DataField="project_id" HeaderText="项目">
                        <HeaderStyle Width="80px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:BoundField>
                    <asp:BoundField DataField="part_no" HeaderText="物资编号">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="物资描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px"
                                title="<%# Server.HtmlEncode(Eval("part_description").ToString())%>">
                                <%# Eval("part_description")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="location" HeaderText="库位">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="matr_seq_no" HeaderText="材料顺序号">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="matr_seq_line_no" HeaderText="行号">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="require_qty" HeaderText="需求数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="place" HeaderText="接收场地">
                        <HeaderStyle Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receive_date" HeaderText="接收日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>


                    <asp:BoundField DataField="receive_date" HeaderText="接收日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="吊装">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkCrane" runat="server"   />
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receiver" HeaderText="接收人">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_ic" HeaderText="IC">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_contact" HeaderText="联系方式">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_block" HeaderText="分段">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_system" HeaderText="系统">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rowstate" HeaderText="状态">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="lack_type" HeaderText="缺料配送方式">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False" HeaderText="删除">
                        <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/delete_.gif" CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>'
                                CommandName="ReqLineDelete" OnClientClick="return window.confirm('确定要删除吗?')" CssClass="deleteCell" />
                            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/delete_.gif"
                                CommandName="Delete" OnClientClick="return window.confirm('确定要删除吗?')" CssClass="deleteCell" />--%>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle CssClass="deleteCellStyle" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="编辑">
                        <ItemTemplate>
                            <%--<asp:ImageButton ID="ImgBtnEdit" runat="server" ImageUrl="~/images/eidt.gif" 
		CommandName="ReqLineEdit" onclick="open_mod_page('<%# Eval("demand_id") %>','<%# Server.UrlEncode(Eval("objid").ToString()) %>','<%# Eval("rowversion") %>')" />--%>
                            <img id="ImgEdit" src="../../images/eidt.gif"  onclick="open_mod_page('<%# Eval("demand_id") %>','<%# Server.UrlEncode(Eval("objid").ToString()) %>','<%# Eval("rowversion") %>')"/>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="取消">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgBtnCancel" runat="server" ImageUrl="~/images/Cancel.gif" CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>'
                            CommandName="ReqLineCancel" OnClientClick="return window.confirm('确定要取消吗?')" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
           
        </div>
    </form>
</body>
</html>
