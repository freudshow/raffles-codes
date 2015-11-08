<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wzxqjh_iss_compare.aspx.cs" Inherits="jzpl.wzxqjh_iss_compare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ERP-DMS下发对照</title>
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 0 10px 0">
    <table cellpadding="0" cellspacing="0" style="margin: 10px 0 0 4px; padding: 0; font-size: 10pt; font-family:Arial 宋体 @宋体">
    <tr>
        <td>日期：
        </td>
        <td>
            <asp:TextBox ID="TxtDate1" runat="server" Width="120px" onfocus="WdatePicker();"></asp:TextBox><span style=" vertical-align:middle">&nbsp--
                &nbsp</span><asp:TextBox ID="TxtDate2"
                runat="server" Width="120px" onfocus="WdatePicker();"></asp:TextBox></td>
                <td>
                    <asp:CheckBox ID="ChkOnlyNotMatch" runat="server" Text="只显示不匹配" /></td>
    </tr>
    </table>
    <hr />
    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 4px; padding: 0; font-size: 10pt; font-family:Arial 宋体 @宋体">
    <tr>
        <td style="width: 61px"><asp:Button ID="BtnQuery" runat="server" Text="查询" Height="21px" Width="60px" Font-Size="10pt" OnClick="BtnQuery_Click" />&nbsp
        </td>
        <td>
            <asp:Button ID="BtnExcel" runat="server" Text="导出" Height="21px" Width="60px" Font-Size="10pt" OnClick="BtnExcel_Click" />
        </td>
    </tr>
    </table>
    </div>
    <table><tr><td>
    <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" Font-Size="10pt" CellPadding="4"
                ForeColor="#333333" OnRowDataBound="GV1_RowDataBound">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#9C9C9C" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    CssClass="gv_header" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="part_no" HeaderText="零件编号">
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="物资描述">   
            <ItemTemplate>
            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px; padding-left: 4px" title="<%# Eval("PART_DESCRIPTION")%>"><%# Eval("PART_DESCRIPTION")%></p>             
             </ItemTemplate>   
                <HeaderStyle Width="300px" />
            </asp:TemplateField>
            <asp:BoundField DataField="part_unit" HeaderText="单位" >
                <HeaderStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="mtr_no" HeaderText="物料流水号">
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="dms_issue_qty" HeaderText="DMS下发数量">
                <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="erp_issue_qty" HeaderText="ERP下发数量">
                <HeaderStyle Width="120px" />
            </asp:BoundField>
        </Columns>
        </asp:GridView></td></tr></table>
        
    </form>
</body>
</html>
