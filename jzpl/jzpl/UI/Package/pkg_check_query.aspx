<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pkg_check_query.aspx.cs" Inherits="Package.pkg_check_query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>大包检验查询</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript" src="../../UI/script/wzxqjh.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="title">大包检验查询<hr /></div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 80px">
                    检验日期：</td>
                <td style="width: 150px">
                    <asp:TextBox ID="TxtCheckDate" runat="server" Width="140px" onfocus="WdatePicker();"></asp:TextBox></td>
                <td style="width: 40px">
                </td>
                <td style="width: 80px">
                    PO：</td>
                <td style="width: 150px">
                    <asp:TextBox ID="TxtPO" runat="server" Width="140px"></asp:TextBox></td>
                <td style="width: 40px; text-align: left">
                </td>
                <td style="width: 80px">
                    关单号：</td>
                <td style="width: 250px" >
                    <asp:TextBox ID="TxtDec" runat="server" Width="140px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                <td style="width: 40px">
                </td>
            </tr>
            <tr>
                <td style="width: 80px">
                    大包编码：</td>
                <td style="width: 150px">
                    <asp:TextBox ID="TxtPackageNo" runat="server" Width="140px"></asp:TextBox></td>
                <td style="width: 40px">
                </td>
                <td style="width: 80px">
                    大包名称：</td>
                <td style="width: 150px">
                    <asp:TextBox ID="TxtPkgName" runat="server" Width="140px"></asp:TextBox></td>
                <td style="width: 40px; text-align: left">
                    <span style="color: #ff0000">%</span></td>
                <td style="width: 80px">
                    项目：</td>
                <td style="width: 250px">
                    <asp:DropDownList ID="DdlProject" Width="240px" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 40px">
                </td>
            </tr>
            <tr>
                <td>
                    小件名称：</td>
                <td>
                    <asp:TextBox ID="TxtPart" runat="server" Width="140px"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td>
                    小件规格：</td>
                <td>
                    <asp:TextBox ID="TxtSpec" runat="server" Width="140px"></asp:TextBox></td>
                <td style="width: 40px; text-align: left">
                    <span style="color: #ff0000">%</span></td>
                <td style="width: 80px">
                    到货日期：</td>
                <td style="width: 150px">
                    <asp:TextBox ID="TxtArrDate" runat="server" Width="140px" onfocus="WdatePicker();"></asp:TextBox></td>
                <td style="width: 40px; text-align: left">
                    <span style="color: #ff0000"></span>
                </td>
            </tr>
        </table>
        <hr />
        <table >
            <tr>
                <td>
                    <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click"  CssClass="button_1"/>
                </td>
                
            </tr>
        </table>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="divBox" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False"
                CssClass="gv" Width="2040px" AllowPaging="True" OnPageIndexChanging="GVData_PageIndexChanging"
                PageSize="12" OnRowDataBound="GVData_RowDataBound" CellPadding="4">                
                <Columns>
                    <asp:BoundField DataField="chk_date_ch" HeaderText="检验日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="dec_no" HeaderText="关单号">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="po_no" HeaderText="PO编号">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="package_no" HeaderText="大包编码">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>                    
                    <asp:TemplateField HeaderText="大包名称">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px" title='<%# Server.HtmlEncode(Eval("pkg_name").ToString())%>'>
                                <%# Server.HtmlEncode(Eval("pkg_name").ToString())%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="小件名称">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px" title='<%# Server.HtmlEncode(Eval("part_name_e").ToString())%>'>
                                <%# Server.HtmlEncode(Eval("part_name_e").ToString())%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="part_unit" HeaderText="小件单位">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="小件规格">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px" title='<%# Server.HtmlEncode(Eval("part_spec").ToString())%>'>
                                <%# Server.HtmlEncode(Eval("part_spec").ToString())%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="project_id" HeaderText="项目编号">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    
                    <asp:TemplateField HeaderText="项目名称">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px">
                                <%# Eval("project_name")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField> 
                    <asp:BoundField DataField="arr_date_ch" HeaderText="到货日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="arrived_qty" HeaderText="清点数量">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="chk_ok_qty" HeaderText="合格数量">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="chk_bad_qty" HeaderText="不合格数量">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check_mark" HeaderText="检验标记">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
