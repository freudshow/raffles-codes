<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pkg_value_query.aspx.cs" Inherits="jzpl.UI.Package.pkg_value_query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>大包价值</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="title">
            大包价值<hr />
        </div>
    <div>
    <table>
    <tr>
    <td style="width:80px">
    大包编码：
    </td>
    <td style="width:120px">
    <asp:TextBox ID="TxtPackageNo" runat="server" Width="110px"></asp:TextBox>
    </td>
        <td style="width: 20px">
        </td>
        <td style="width: 40px">
    
     PO：</td>
        <td style="width: 120px">
            <asp:TextBox ID="TxtPO" runat="server" Width="110px"></asp:TextBox></td>
        <td style="width: 20px">
        </td>
        <td style="width: 40px">
    项目：</td>
        <td style="width: 220px">
            <asp:DropDownList ID="DdlProject" runat="server" Width="220px"></asp:DropDownList></td>
    </tr>
    </table>
    <hr />
    <table>
    <tr>
    <td>
    <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="button_1" OnClick="BtnQuery_Click" />
    <asp:Button ID="BtnExport" runat="server" Text="导出" CssClass="button_1" OnClick="BtnExport_Click" />
    </td>
    </tr>
    </table>    
    
    <asp:GridView ID="GVPkgValue" runat="server" CssClass="gv" AutoGenerateColumns="False">
    <Columns>
    <asp:BoundField DataField="pkg_no" HeaderText="大包编码" >
        <HeaderStyle Width="120px" />
    </asp:BoundField>
    <asp:BoundField DataField="po_no" HeaderText="PO号" >
        <HeaderStyle Width="60px" />
    </asp:BoundField>
    <asp:BoundField DataField="po_line" HeaderText="PO行号" HeaderStyle-Width="60px"/>
    <asp:BoundField DataField="project_id" HeaderText="项目" >
        <HeaderStyle Width="120px" />
    </asp:BoundField>
    <asp:BoundField DataField="buy_qty_due" HeaderText="数量" >
        <HeaderStyle Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="fbuy_unit_price" HeaderText="价格" >
        <HeaderStyle Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="fbuy_tax_unit_price" HeaderText="含税价格" >
        <HeaderStyle Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="buy_unit_price" HeaderText="本位币价格">
        <HeaderStyle Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="total_base" HeaderText="总额" >
        <HeaderStyle Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="curr_code" HeaderText="币种" >
        <HeaderStyle Width="80px" />
    </asp:BoundField>
    
    </Columns>
    
    </asp:GridView>
    
    
    </div>
    </form>
</body>
</html>
