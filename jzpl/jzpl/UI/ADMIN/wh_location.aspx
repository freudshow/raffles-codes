<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wh_location.aspx.cs" Inherits="ADMIN.wh_location" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>库位</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div id="title">库位<hr /></div>
        <div style="text-align:left">
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td width="80">
                        公司:
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlCompany" runat="server" OnSelectedIndexChanged="DdlCompany_SelectedIndexChanged" AutoPostBack="True" Width="480px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td width="80">
                        区域:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <asp:DropDownList ID="DdlArea" runat="server" Width="480px">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlCompany" />
                            </Triggers>
                        
                        
                        </asp:UpdatePanel>
                        </td>
                </tr>
                <tr>
                    <td width="80">
                        库位:</td>
                    <td>
                        <asp:TextBox ID="TxtLoc" runat="server" Width="473px"></asp:TextBox></td>
                </tr>
            </table>
            <hr width="600px" />
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px"><tr><td>
            <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="button_1" OnClick="BtnQuery_Click" />
            <asp:Button ID="BtnAdd" runat="server" Text="添加"  CssClass="button_1"  OnClick="BtnAdd_Click" />
            </td></tr></table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" CssClass="gv"
                            OnRowCancelingEdit="GV_RowCancelingEdit" OnRowEditing="GV_RowEditing" OnRowDeleting="GV_RowDeleting"
                            OnRowUpdating="GV_RowUpdating" Width="600px">
                           
                            <Columns>
                                <asp:BoundField DataField="company_id" HeaderText="公司" ReadOnly="True">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="area" HeaderText="区域" ReadOnly="True">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="location" HeaderText="库位" ReadOnly="True">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="激活">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Enabled="false" Checked='<%# Eval("state").ToString().Equals("1")   %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Enabled="true" Checked='<%# Eval("state").ToString().Equals("1") %>' />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="修改" ShowEditButton="True">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
