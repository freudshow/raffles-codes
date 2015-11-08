<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_manage_base.aspx.cs" Inherits="jzpl.UI.ADMIN.user_manage_base" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">  
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="font-size:10pt" cellpadding="0" cellspacing="0"><tr><td style="width:70px">USER ID：</td><td style="width:160px"><asp:TextBox ID="TxtUserID" runat="server" Width="160px"></asp:TextBox></td></tr>
            <tr>
                <td style="width: 70px">
                    GROUP：</td>
                <td style="width: 160px">
                    <asp:DropDownList ID="DdlGroup" runat="server" Width="160px">
                    </asp:DropDownList></td>
            </tr></table>
            <hr style="width:480px;text-align:left"/>
            <table>
            <tr>
                <td style="width: 70px">
            
            <asp:Button ID="BtnAdd"
            runat="server" Text="ADD USER" CssClass="botton" OnClick="BtnAdd_Click" /></td>
                <td style="width: 160px">
                </td>
            </tr>
        </table>
        
        <div style="overflow-x:none; overflow-y: auto; width: 480px; height:500px">
        <asp:GridView ID="GV_User" runat="server" AutoGenerateColumns="False" Font-Size="10pt" Width="460px" OnRowDeleting="GV_User_RowDeleting">
            <Columns>                
                <asp:TemplateField HeaderText="USER ID" HeaderStyle-Width="100px">
                <ItemTemplate>
                <a href="permission.aspx?id=<%# Eval("user_id") %>" target="_self" title="设置权限"><%# Eval("user_id") %></a>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GROUP" HeaderStyle-Width="300px">
                <ItemTemplate>
                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px"
                                title="<%# Eval("group_name")%>">
                                <%# Eval("group_name")%>
                            </p>
                </ItemTemplate>
                </asp:TemplateField>                
                <asp:CommandField ButtonType="Image" CancelText="" DeleteImageUrl="~/images/delete_.gif"
                    DeleteText="" EditText="" HeaderText="DELETE" InsertText="" NewText="" SelectText=""
                    ShowCancelButton="False" ShowDeleteButton="True" UpdateText="" HeaderStyle-Width="60px">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </div>
        
        
    </div>
    </form>
</body>
</html>
