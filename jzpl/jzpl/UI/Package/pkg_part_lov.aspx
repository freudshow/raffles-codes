<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_part_lov.aspx.cs" Inherits="jzpl.UI.Package.pkg_arrival_xj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择小件信息</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ClosePage()
        {
            //this.window.returnValue=0;
            this.window.opener=null;
            this.window.close();
        }
        function SelectXJ(xjid,xjmccn,xjmcen,xjgg,xjdw,gdh,hth,pono)
        {
            var return_array = new Array();
            return_array[0]=xjid;
            return_array[1]=xjmccn;
            return_array[2]=xjmcen;
            return_array[3]=xjgg;
            return_array[4]=xjdw;
            return_array[5]=gdh;
            return_array[6]=hth;
            return_array[7]=pono;
            window.returnValue=return_array;
            window.opner=null;
            window.close();
        }
    </script>

    <base target="_self" />
</head>
<body>
   <form id="form1" runat="server">
        <div style="margin: 10px">
            <div id="title">
                选择小件信息<hr />
            </div>
            <div>
                <table>
                    <tr>
                        <td style="width: 80px">
                            大包编号：</td>
                        <td style="width: 180px">
                            <asp:TextBox ID="TxtDBbh" runat="server" BorderStyle="Groove" Width="150px" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 80px">
                            零件编号：</td>
                        <td style="width: 190px">
                            <asp:TextBox ID="TxtXJbh" runat="server" Width="150px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    </tr>
                    <tr>
                        <td>
                            零件名称：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TxtXJmc" runat="server" Width="418px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            零件规格：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TxtXJgg" runat="server" Width="418px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    </tr>
                    <tr>
                        <td>
                            合同号：</td>
                        <td>
                            <asp:TextBox ID="TxtHTno" runat="server" Width="150px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                        <td>
                            关单号：</td>
                        <td>
                            <asp:TextBox ID="TxtGDno" runat="server" Width="150px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    </tr>
                    <tr>
                        <td>
                            PO号：</td>
                        <td>
                            <asp:TextBox ID="TxtPoNo" runat="server" Width="150px"></asp:TextBox></td>
                        <td>
                        </td>
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
                <div style="width: 620px; height: 350px; overflow: scroll; border: 1px solid;">
                    <asp:GridView ID="XJGrid" runat="server" AutoGenerateColumns="False" CssClass="gv"
                        Width="940px">
                        <Columns>
                            <asp:BoundField DataField="package_no" HeaderText="大包编号" HeaderStyle-Width="100px" />
                            
                            <asp:TemplateField HeaderText="零件编号" HeaderStyle-Width="160px">
                            <ItemTemplate>
                            
<u style="color: #2131A1; cursor: pointer;" onclick="SelectXJ('<%# Eval("part_no") %>','<%# Eval("part_name")%>','<%# Server.HtmlEncode(Eval("part_name_e").ToString().Replace("'","\\'"))%>','<%# Server.HtmlEncode(Eval("part_spec").ToString())%>','<%# Eval("unit") %>','<%# Eval("dec_no") %>','<%# Eval("contract_no") %>','<%# Eval("po_no") %>')">
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
        </div>
    </form>
</body>
</html>

