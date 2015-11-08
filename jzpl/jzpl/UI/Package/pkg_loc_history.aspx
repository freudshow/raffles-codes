<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_loc_history.aspx.cs"
    Inherits="jzpl.UI.Package.pkg_loc_history" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包库存事务</title>
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            大包库存事务<hr />
        </div>
        <div>
       <table>
       <tr>
       <td style="width:80px">
       事务代码：
       </td>
           <td style="width: 120px">
               <asp:DropDownList ID="DdlHisType" runat="server" Width="116px">
               </asp:DropDownList></td>
           <td style="width: 20px">
           </td>
           <td style="width: 45px">
       
      时间：</td>
           <td style="width: 184px">
               <asp:TextBox ID="TxtTime" runat="server" Width="80px" onfocus="WdatePicker();"></asp:TextBox>
               -
               <asp:TextBox ID="TxtDateEnd" runat="server" Width="80px" onfocus="WdatePicker();"></asp:TextBox></td>
           <td style="width: 20px">
           </td>
           <td style="width: 80px">
       
       大包编码：</td>
           <td style="width: 120px">
               <asp:TextBox ID="TxtPackageNo" runat="server" Width="110px"></asp:TextBox></td>
           <td style="width: 20px">
           </td>
           <td style="width: 80px">
       零件编码：</td>
           <td style="width: 120px">
               <asp:TextBox ID="TxtPartNo" runat="server" Width="110px"></asp:TextBox></td>
       </tr>
           <tr>
               <td style="width: 80px">
       存储区域：</td>
               <td style="width: 120px">
                   <asp:TextBox ID="TxtArea" runat="server" Width="110px"></asp:TextBox></td>
               <td style="width: 20px">
               </td>
               <td style="width: 45px">
                   位置：</td>
               <td style="width: 184px">
                   <asp:TextBox ID="TxtLocation" runat="server" Width="176px"></asp:TextBox></td>
               <td style="width: 20px">
               </td>
               <td style="width: 80px">
               </td>
               <td style="width: 120px">
               </td>
               <td style="width: 20px">
               </td>
               <td style="width: 80px">
               </td>
               <td style="width: 120px">
               </td>
           </tr>
       </table>
       <hr />
       <table>
       <tr>
       <td>
       <asp:Button ID="BtnQuery" Text="查询" runat="server" CssClass="button_1" OnClick="BtnQuery_Click" />
        <asp:Button ID="BtnExport" Text="导出" runat="server" CssClass="button_1" OnClick="BtnExport_Click"  />
       </td>
       </tr>
       </table>    
        
        <asp:GridView ID="GVPkgHis" runat="server" CssClass="gv" AutoGenerateColumns="False" OnRowDataBound="GVPkgHis_RowDataBound">
        <Columns>
        <asp:BoundField DataField="his_id"  HeaderText="事务ID">
            <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="his_type_ch" HeaderText="事务代码" >
            <HeaderStyle Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="his_time_str" HeaderText="时间" >
            <HeaderStyle Width="160px" />
        </asp:BoundField>
        <asp:BoundField DataField="package_no" HeaderText="大包编码" >
            <HeaderStyle Width="120px" />
        </asp:BoundField>
        <asp:BoundField DataField="part_no" HeaderText="零件编码" >
            <HeaderStyle Width="160px" />
        </asp:BoundField>
        <asp:BoundField DataField="area" HeaderText="存储区域" >
            <HeaderStyle Width="160px" />
        </asp:BoundField>
        <asp:BoundField DataField="location" HeaderText="存储位置" >
            <HeaderStyle Width="160px" />
        </asp:BoundField>
        <asp:BoundField DataField="direction_ok" HeaderText="方向1" >
            <HeaderStyle Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="ok_qty" HeaderText="合格数量" >
            <HeaderStyle Width="80px" />
        </asp:BoundField>
         <asp:BoundField DataField="direction_bad" HeaderText="方向2" >
             <HeaderStyle Width="60px" />
         </asp:BoundField>
        <asp:BoundField DataField="bad_qty" HeaderText="不合格数量" >
            <HeaderStyle Width="80px" />
        </asp:BoundField>
         <asp:BoundField DataField="direction_nocheck" HeaderText="方向3" >
             <HeaderStyle Width="60px" />
         </asp:BoundField>
        <asp:BoundField DataField="nocheck_qty" HeaderText="免检数量" >
            <HeaderStyle Width="80px" />
        </asp:BoundField>
        </Columns>
        
        </asp:GridView>
        </div>
        
    </form>
</body>
</html>
