<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_manage.aspx.cs" Inherits="jzpl.UI.ADMIN.user_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
    .permission{border-top:1px black solid;border-right:1px black solid;}    
    .permission td,th{text-size:10pt;border-bottom:solid 1px black;border-left:solid 1px black;}
    .botton{height:21px; width:100px}
    </style>
    <script type="text/javascript" src="../script/admin.js"></script>
</head>
<body >
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td><asp:DropDownList ID="DdlUser" runat="server" OnSelectedIndexChanged="DdlUser_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList></td><td><a href="user_manage_base.aspx" target="_self"><img src="../../images/rule.gif" style="border:0" alt="用户管理" title="用户管理" /></a></td>
           
        </tr>
    </table>
    <table><tr><td><asp:PlaceHolder ID="PH1" runat="server"></asp:PlaceHolder></td></tr>
        <tr><td><asp:Button ID="BtnSave" runat="server" Text="Update" CssClass="botton" OnClick="BtnSave_Click" OnClientClick="javascript:SetPermissionToHiddenForm()" /></td></tr></table>
    </div>  
    <input type="hidden" id="clientPermission" name="clientPermission" value="11111|000|000|1111"/>      
    </form>
</body>
</html>
