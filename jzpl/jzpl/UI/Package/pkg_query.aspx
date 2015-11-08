<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_query.aspx.cs" Inherits="Package.pkg_query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包查询</title>  
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    window.onload = function(){init();}
    function init(){
    SetGVBoxHeight("divBox","GVData");
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="title">大包查询<hr /></div>
        <table cellpadding="0" cellspacing="0" >
            <tr>
                <td style="width: 80px">
                    大包编码：</td>
                <td style="width: 220px">
                    <asp:TextBox ID="TxtPackageNo" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                <td style="width: 60px">
                    </td>
                <td style="width: 80px">
                    大包名称：</td>
                <td style="width: 240px">
                    <asp:TextBox ID="TxtPkgName" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
            </tr>
            <tr>
                <td>
                    项目：
                </td>
                <td>
                    <asp:DropDownList ID="DdlProject" runat="server" Width="206px">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
      
        </table>
        <hr />
        <table cellspacing="0" cellpadding="0" style="padding: 0; margin: 0 0 4px 0">
            <tr>
                <td >
                    <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" CssClass="button_1" />
                    <asp:Button ID="BtnExport" runat="server" Text="导出" CssClass="button_1" OnClick="BtnExport_Click" />
                </td>                
            </tr>
        </table>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="divBox" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" CssClass="gv" Width="820px" AllowPaging="True" OnPageIndexChanging="GVData_PageIndexChanging"
                PageSize="12" OnRowDataBound="GVData_RowDataBound" >                
                <Columns>
                    <asp:BoundField DataField="package_no" HeaderText="大包编号">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="大包描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;" title="<%# Server.HtmlEncode(Eval("package_name").ToString())%>">
                                <%# Server.HtmlEncode(Eval("package_name").ToString())%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="project_id" HeaderText="项目编号">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="项目描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px" title="<%# Eval("project_name") %>">
                                <%# Eval("project_name")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="po_no" HeaderText="PO编号">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="currency" HeaderText="币种" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="package_value" HeaderText="大包价值"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
