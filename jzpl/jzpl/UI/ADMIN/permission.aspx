<%@ Page Language="C#" AutoEventWireup="true" Codebehind="permission.aspx.cs" Inherits="jzpl.UI.ADMIN.permission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="title">用户管理<hr /></div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="DdlUser" runat="server" OnSelectedIndexChanged="DdlUser_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList></td>
                    <td>
                        <a href="user_manage_base.aspx" target="_self">
                            <img src="../../images/rule.gif" style="border: 0" alt="用户管理" title="用户管理" /></a></td>
                    <td style="width: 200px; text-align: right">
                        <asp:Button ID="BtnSetPer" runat="server" Text="设置" CssClass="button_1" OnClick="BtnSetPer_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:GridView ID="GVCurrPer" runat="server" AutoGenerateColumns="False" OnRowDataBound="GVCurrPer_RowDataBound" CssClass="gv">
            <Columns>
                <asp:BoundField DataField="code" HeaderText="CODE" HeaderStyle-Width="120px" />
                <asp:BoundField DataField="page" HeaderText="PAGE" HeaderStyle-Width="200px"/>                
                <asp:TemplateField HeaderStyle-Width="280px" HeaderText="DESCRIPTION">
                <ItemTemplate>
                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 280px"><%# Eval("description") %></p>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PERMISSION" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkPer" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GVAllPer" runat="server" AutoGenerateColumns="False" OnRowDataBound="GVAllPer_RowDataBound" CssClass="gv">
            <Columns>
                <asp:BoundField DataField="code" HeaderText="CODE" HeaderStyle-Width="120px" />
                <asp:BoundField DataField="page" HeaderText="PAGE" HeaderStyle-Width="200px"/>                
                <asp:TemplateField HeaderStyle-Width="280px" HeaderText="DESCRIPTION">
                <ItemTemplate>
                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 280px"><%# Eval("description") %></p>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PERMISSION" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkPer" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Panel ID="Pnl" runat="server" Width="710px">
        <hr style="width:700px;text-align:left"/>
        <table>
        <tr>
        <td>
            <asp:Button ID="BtnUpdate" runat="server" Text="UPDATE" CssClass="button_1" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnBack" runat="server" Text="BACK" CssClass="button_1" OnClick="BtnBack_Click"/>
        </td>
        <td style="width:200px;text-align:right">
        Copy other user's permission:
        </td>
        <td><asp:DropDownList ID="DdlOtherUser" runat="server">
            </asp:DropDownList></td>
        <td><asp:Button ID="BtnCopyPer" runat="server" Text="COPY P" CssClass="button_1" OnClick="BtnCopyPer_Click"/></td>
        </tr></table></asp:Panel>
    </form>
</body>
</html>
