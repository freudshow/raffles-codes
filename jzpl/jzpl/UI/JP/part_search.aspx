<%@ Page Language="C#" AutoEventWireup="true" Codebehind="part_search.aspx.cs" Inherits="jzpl.UI.JP.part_search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Part</title>
    <style type="text/css">
     .separator{background:url('../../images/icon1.gif') no-repeat ; text-align:center;}   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="font-size:10pt" width="100%">
                <tr>
                    <td width="20%">
                        域</td>
                        <td class="separator" width="20%" align="center">
                            </td>
                    <td >
                        <asp:TextBox runat="server" ID="TxtContract"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        物资编码</td><td class="separator" align="center"></td>
                    <td >
                        <asp:TextBox runat="server" ID="TxtPartNo"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        物资名称</td><td class="separator" align="center"></td>
                    <td>
                        <asp:TextBox runat="server" ID="TxtDescription"></asp:TextBox></td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="GVPart" AutoGenerateColumns="False">
            </asp:GridView>
            <asp:Button runat="server" Text="Button" ID="BtnOk" /><asp:Button runat="server"
                Text="Button" ID="BtnCancel" />
        </div>
    </form>
</body>
</html>
