<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pkg_issue_voucher.aspx.cs" Inherits="jzpl.UI.Package.Pkg_issue_voucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>大包物资领料单</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript" src="../../UI/script/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
    
    window.onload=function(){FormInit();}
    function FormInit()
    {
        SetGVBoxHeight("UserSaveData","GVData");
    }
    
     var selectAll = function () {
            var chkState = $("#chkSelectAll").attr("checked");
            $(".firstCol","#GVData").children().attr("checked", chkState);
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">   
    <div id="title">
        大包物资领料打印
        <hr /></div>
        
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        申请ID：</td>
                    <td style="width: 120px">
                        <asp:TextBox ID="TxtReqID" runat="server" Width="120px"></asp:TextBox></td>
                    <td width="20">
                    </td>
                    <td style="width: 80px">
                        零件编码：</td>
                    <td style="width: 120px">
                        <asp:TextBox ID="TxtPartNo" runat="server" Width="120px"></asp:TextBox></td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td width="120">
                        项目：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlProject" runat="server" Width="245px">
                        </asp:DropDownList></td>
                    <td style="width: 20px">
                    </td>
                    <td width="120">
                        PO：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPO" runat="server" Width="240px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        大包编码：</td>
                    <td>
                        <asp:TextBox ID="TxtPkgNo" runat="server" Width="240px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="120">
                        大包名称：</td>
                    <td>
                        <asp:TextBox ID="TxtPkgName" runat="server" Width="240px"></asp:TextBox><span style="color: Red">%</span></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        零件名称（英文）：</td>
                    <td>
                        <asp:TextBox ID="TxtPartNameE" runat="server" Width="240px"></asp:TextBox><span style="color: Red">%</span></td>
                    <td>
                    </td>
                    <td width="120">
                        零件名称（中文）：</td>
                    <td>
                        <asp:TextBox ID="TxtPartName" runat="server" Width="240px"></asp:TextBox><span style="color: Red">%</span></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        零件规格：</td>
                    <td>
                        <asp:TextBox ID="TxtPartSpec" runat="server" Width="240px"></asp:TextBox><span style="color: Red">%</span></td>
                    <td>
                    </td>
                    <td width="120">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <table id="req_content" style="font-size: 10pt">
                <tr>
                    <td width="80">
                        <asp:Label ID="Label1" runat="server" Text="接收场地："></asp:Label></td>
                    <td width="180">
                        <asp:DropDownList ID="DdlProdSite" runat="server" Width="160px">
                        </asp:DropDownList><span style="color: #ff0000"></span></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label5" runat="server" Text="接收时间："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtDate" runat="server" Width="154px"></asp:TextBox><span style="color: red"></span></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        接收部门：</td>
                    <td width="180">
                        <asp:DropDownList ID="DdlReceiptDept" runat="server" Width="160px">
                        </asp:DropDownList><span style="color: #ff0000"></span></td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        <asp:Label ID="Label6" runat="server" Text="接收人："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtReceiver" runat="server" Width="154px"></asp:TextBox><span style="color: #ff0000"></span></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label7" runat="server" Text="IC："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtIC" runat="server" Width="154px"></asp:TextBox><span style="color: #ff0000"></span></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label8" runat="server" Text="联系方式："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtContact" runat="server" Width="154px"></asp:TextBox><span style="color: #ff0000"></span></td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        <asp:Label ID="Label9" runat="server" Text="分段："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtBlock" runat="server" Width="154px"></asp:TextBox></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label10" runat="server" Text="系统："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtSystem" runat="server" Width="154px"></asp:TextBox></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label11" runat="server" Text="施工内容："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtWorkContent" runat="server" Width="154px"></asp:TextBox></td>
                    <td width="20">
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td width="120">
                        申请部门：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlReqGroup" runat="server" Width="245px">
                        </asp:DropDownList></td>
                    <td style="width: 20px">
                    </td>
                    <td width="120">
                        申请用户：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtReqUser" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        申请日期：</td>
                    <td>
                        <asp:TextBox ID="TxtReqDate" runat="server" Width="240px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="120">
                        申请状态：</td>
                    <td>
                        <asp:TextBox ID="TxtReqState" runat="server" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
             <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" CssClass="button_1" UseSubmitBehavior="False" />
             <asp:Button ID="BtnPrint" runat="server" Text="打印" CssClass="button_1" UseSubmitBehavior="False" OnClick="BtnPrint_Click" />
    
        <div style="overflow-x:auto; overflow-y:none; width: 100%" id="UserSaveData" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" CssClass="gv"
                Width="2460px" BorderWidth="1px">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="选择">
                    <HeaderTemplate>
                     <input type="checkbox" onclick="selectAll()" id="chkSelectAll"  /> 
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkSelect" runat="server" CssClass="firstCol" />
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="requisition_id" HeaderText="申请编号">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="require_qty" HeaderText="申请数量">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="released_qty" HeaderText="下达数量" HeaderStyle-Width="100px" />
                    
                    <asp:BoundField DataField="part_no" HeaderText="零件编号">
                        <HeaderStyle Width="160px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="零件描述（英文）">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title="<%# Eval("part_name_e")%>">
                                <%# Eval("part_name_e")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="package_no" HeaderText="大包编码">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="大包描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title="<%# Eval("package_name")%>">
                                <%# Eval("package_name")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="project_id" HeaderText="项目">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="接收场地">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title="<%# Eval("place_name")%>">
                                <%# Eval("place_name")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="receipt_date_str" HeaderText="接收日期">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="吊装">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkCrane" runat="server" />
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
                    <asp:BoundField DataField="receiver_contract" HeaderText="联系方式">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_block" HeaderText="分段">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_system" HeaderText="系统">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>                   
                    <asp:BoundField DataField="record_time_str" HeaderText="创建日期">
                        <HeaderStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="recorder" HeaderText="创建人">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="req_group_name" HeaderText="申请部门">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rowstate_zh" HeaderText="状态">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>   
                    <asp:BoundField DataField="print_time_str" HeaderText="打印时间" HeaderStyle-Width="100px" />                 
                </Columns>
            </asp:GridView>
            </div>
       
 </form>
       </body>
       </html>