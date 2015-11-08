<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Pkg_po_lov.aspx.cs" Inherits="jzpl.UI.Package.Pkg_po_lov" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包零件信息</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript">
    
    function select_part(project,pkg_no,pkg_name)
    {
        var ret_ = new Array();
        ret_[0]=project;        
        ret_[1]=pkg_no;
        ret_[2]=pkg_name;        
        window.returnValue = ret_;
        window.close();
    }
    </script>

    <base target="_self" />
</head>
<body>
    <div style="margin: 10px">        
        <form id="form1" runat="server">
        <div id="title">
            ERP PO零件<hr />
        </div>
            <div runat="server" id="divImportPackage">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 70px; height: 24px;">
                            大包编号：
                        </td>
                        <td style="width: 180px; height: 24px;">
                            <asp:TextBox ID="TxtDBbh" runat="server" Width="150px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                        <td style="width: 30px; height: 24px;">
                            <span style="color: #ff0000"></span>
                        </td>
                        <td style="width: 70px; height: 24px;">
                            大包描述：
                        </td>
                        <td style="width: 250px; height: 24px;">
                            <asp:TextBox ID="TxtDBmc" runat="server" Width="230px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    </tr>
                    <tr>
                        <td style="width: 70px">
                            PO：
                        </td>
                        <td style="width: 180px">
                            <asp:TextBox ID="TxtPO" runat="server" Width="150px"></asp:TextBox></td>
                        <td style="width: 30px">
                        </td>
                        <td style="width: 70px">
                            项目编号：
                        </td>
                        <td style="width: 250px">
                            <asp:DropDownList ID="DdlProject_" runat="server" Width="237px">
                            </asp:DropDownList><span style="color: #ff0000"></span></td>
                    </tr>
                </table>
                <hr />
                <table class="button_box">
                    <tr>
                        <td>
                            <asp:Button ID="BtnResult" runat="server" CssClass="button_1" Text="查询" OnClick="BtnResult_Click" />
                            &nbsp;&nbsp;<input type="button" class="button_1" onclick="window.close()" value="返回"/>
                        </td>
                    </tr>
                </table>
                <div style="overflow-x: auto; overflow-y: auto; width: 605px; height:300px" id="gvbox1">
                    <asp:GridView ID="GVERPPart" runat="server" AutoGenerateColumns="False" CssClass="gv"
                        Width="600px">
                        <Columns>
                        <asp:BoundField DataField="rownum" />
                            <asp:TemplateField HeaderText="零件编码">
                                <ItemTemplate>
                                     <u style="color: #2131A1; cursor: pointer;" onclick="select_part('<%# Eval("project_id") %>','<%# Eval("part_no") %>','<%# Server.HtmlEncode(Eval("description").ToString()) %>')"><%# Eval("part_no") %></u>
                                </ItemTemplate>
                                <HeaderStyle Width="160px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="250px" HeaderText="零件描述">
                                <ItemTemplate>
                                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 250px"
                                        title='<%# Server.HtmlEncode(Eval("description").ToString()) %>'>
                                        <%# Eval("description") %>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="project_id" HeaderText="项目" HeaderStyle-Width="150px" />
                            <asp:BoundField DataField="order_no" HeaderText="PO号" HeaderStyle-Width="100px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
