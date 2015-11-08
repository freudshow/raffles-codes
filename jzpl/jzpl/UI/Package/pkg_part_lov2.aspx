<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pkg_part_lov2.aspx.cs" Inherits="jzpl.UI.Package.pkg_part_lov2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>大包零件信息</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript">
    
    function select_part(part_no,project,pkg_no,pkg_name,po_no,part_name,part_name_e,part_spec,part_unit)
    {
        var ret_ = new Array();
        ret_[0]=part_no;
        ret_[1]=project;
        ret_[2]=pkg_no;
        ret_[3]=pkg_name;
        ret_[4]=po_no;
        ret_[5]=part_name;
        ret_[6]=part_name_e;
        ret_[7]=part_spec;
        ret_[8]=part_unit;       
        
        window.returnValue = ret_;
        window.close();
    }
    </script>
<base target="_self" />
</head>

<body>
    <form id="form1" runat="server">
        <div style="margin:10px">
        <div id="title">
                选择小件信息<hr />
            </div>
            <table >
                <tr>
                    <td style="width: 80px">
                        项目：</td>
                    <td style="width: 420px">
                        <asp:DropDownList ID="DdlProject" runat="server" Width="378px">
                        </asp:DropDownList></td>
                </tr>
            </table>
            <table >
                <tr>
                    <td style="width: 80px">
                        大包编号：</td>
                    <td style="width: 140px">
                        <asp:TextBox ID="TxtPkgNo" runat="server" Width="120px"></asp:TextBox><span
                            style="color: #ff0000">%</span></td>
                    <td style="width: 20px">
                    </td>
                    <td style="width: 80px">
                        零件编号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPartNo" runat="server" Width="120px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                </tr>
            </table>
            <table >
                <tr>
                    <td style="width: 80px">
                        大包名称：</td>
                    <td>
                        <asp:TextBox ID="TxtPkgName" runat="server" Width="372px"></asp:TextBox><span
                            style="color: #ff0000">%</span></td>
                </tr>
            </table>
            
            <table >
                <tr>
                    <td style="width: 80px">
                        零件名称：</td>
                    <td>
                        <asp:TextBox ID="TxtPartName" runat="server" Width="372px"></asp:TextBox><span
                            style="color: #ff0000">%</span></td>
                    <td>
                    </td>
                </tr>
            </table>
            <table >
                <tr>
                    <td style="width: 80px">
                        零件规格：</td>
                    <td>
                        <asp:TextBox ID="TxtPartSpec" runat="server" Width="372px"></asp:TextBox><span
                            style="color: #ff0000">%</span></td>
                    <td>
                    </td>
                </tr>
            </table>
            <table >
                <tr>
                    <td style="width: 80px">
                        合同号：</td>
                    <td style="width: 140px">
                        <span style="color: #ff0000">
                            <asp:TextBox ID="TxtContractNo" runat="server" Width="120px"></asp:TextBox>%</span></td>
                    <td style="width: 20px">
                    </td>
                    <td style="width: 80px">
                        PO号：</td>
                    <td>
                        <asp:TextBox ID="TxtPO" runat="server" Width="120px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
            </table>
            <table >
                <tr>
                    <td style="width: 80px">
                        关单号：</td>
                    <td>
                        <asp:TextBox ID="TxtDocNo" runat="server" Width="372px"></asp:TextBox><span
                            style="color: #ff0000">%</span></td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnResult" runat="server" CssClass="button_1" Text="查看" OnClick="BtnResult_Click" /></td>
                </tr>
            </table>
            <div style="width: 600px; height: 350px; overflow: scroll; border: 1px solid;">
                <asp:GridView ID="GVPart" runat="server" AutoGenerateColumns="False" CssClass="gv"
                        Width="940px">
                        <Columns>
                            <asp:BoundField DataField="package_no" HeaderText="大包编号" HeaderStyle-Width="100px" />
                            
                            <asp:TemplateField HeaderText="零件编号" HeaderStyle-Width="160px">
                            <ItemTemplate>
                            
<u style="color: #2131A1; cursor: pointer;" onclick="select_part('<%# Eval("part_no") %>','<%# Eval("project_id") %>','<%# Eval("package_no") %>','<%# Server.HtmlEncode(Eval("package_name").ToString()) %>','<%# Eval("po_no") %>','<%# Server.HtmlEncode(Eval("part_name").ToString()) %>','<%# Server.HtmlEncode(Eval("part_name_e").ToString()) %>','<%# Server.HtmlEncode(Eval("part_spec").ToString()) %>','<%# Eval("unit") %>')">
                                                    <%# Eval("part_no") %>
                                                </u>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="零件名称" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                        title="<%# Server.HtmlEncode(Eval("part_name_e").ToString())%>">
                                        <%# Server.HtmlEncode(Eval("part_name_e").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="零件规格" HeaderStyle-Width="160px">
                                <ItemTemplate>
                                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 160px;
                                        font-size: 12px;" title="<%# Server.HtmlEncode(Eval("part_spec").ToString())%>">
                                        <%# Server.HtmlEncode(Eval("part_spec").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="unit" HeaderText="单位" HeaderStyle-Width="60px" />
                            <asp:BoundField DataField="contract_no" HeaderText="合同号" HeaderStyle-Width="100px" />
                            <asp:BoundField DataField="dec_no" HeaderText="关单号" HeaderStyle-Width="160px" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table style="width: 940px;" class="gv">
                                <tr>
                                    <th style="width: 100px;">
                                        <a>大包编号</a>
                                    </th>
                                    <th style="width: 160px;">
                                        <a>零件编号</a>
                                    </th>
                                    <th style="width: 200px;">
                                        <a>零件名称</a>
                                    </th>                                   
                                    <th style="width: 160px;">
                                        <a>零件规格</a>
                                    </th>
                                    <th style="width: 60px">
                                        <a>单位</a>
                                    </th>
                                    <th style="width: 100px">
                                        <a>合同号</a>
                                    </th>
                                    <th style="width: 160px;">
                                        <a>关单号</a>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
