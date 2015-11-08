<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jp_pkg_jjd_finish.aspx.cs" Inherits="Package.jp_pkg_jjd_finish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>大包物资交接单完成</title>
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>    
    <script type="text/javascript">
    window.onload=function(){init();}
    function init()
    {
        SetGVBoxHeight("gvbox","GVDisplayJjdLine");
    }
    function getInputValue(obj)
    {
        i = obj.parentNode.parentNode.getElementsByTagName("input")[1].value ;
        return i;
    }
    </script>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="title">大包物资交接单完成<hr /></div>
    <div>
    <div id="divJjdQuery" runat="server" >
        
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        交接单号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtQJjdNo" name="TxtRecieveDate" runat="server" AutoPostBack="True"
                            Width="142px"></asp:TextBox></td>
                    <td style="width: 25px">
                        </td>
                    <td style="width: 80px">
                        需求日期：</td>
                    <td>
                        <asp:TextBox ID="TxtQReceiptDate" runat="server" AutoPostBack="True" name="TxtRecieveDate"
                            Width="142px" onfocus="WdatePicker();"></asp:TextBox></td>
                    <td>
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
                <tr>
                    <td>
                        接收地：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlQReceiptPlace" runat="server" Width="150px">
                        </asp:DropDownList></td>
                    <td>
                        <span style="color: #ff0000"></span>
                    </td>
                    <td>
                        接收部门：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlQReceiptDept" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 25px">
                        <span style="color: #ff0000"></span>
                    </td>
                    <td style="width: 80px">
                        接收人：</td>
                    <td>
                        <asp:TextBox ID="TxtQReceiptPerson" runat="server" AutoPostBack="True" name="TxtRecieveDate"
                            Width="142px"></asp:TextBox></td>
                    <td style="width: 25px">
                        <span style="color: #ff0000"></span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        状态：</td>
                    <td>
                        <asp:TextBox ID="TxtState" runat="server" AutoPostBack="True" name="TxtRecieveDate"
                            Width="142px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td colspan="5">
                        <span style="font-size: 8pt; color: #ff0000">（I/初始 P/部分完成 F/完成）</span></td>
                    <td >
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnQJjdQuery" runat="server" Text="查询" OnClick="BtnQJjdQuery_Click"
                            CssClass="button_1" /></td>
                </tr>
            </table>   
            <div style="overflow-x:auto; overflow-y: none; width: 100%" id="dataBox" runat="server">         
            <asp:GridView ID="GVJjdList" runat="server" AutoGenerateColumns="False" Width="720px"
                CssClass="gv" AllowPaging="True" OnRowCommand="GVJjdList_RowCommand" OnPageIndexChanging="GVJjdList_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="单号">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkBtnJjdNo" runat="server" Text='<%# Bind("jjd_no") %>' CommandName="displayJjd"
                                CommandArgument='<%# Bind("jjd_no") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receipt_date_str" HeaderText="接收时间">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField  HeaderText="接收地" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px;
                                    margin: 0" title="<%# Eval("receipt_place")%>"> <%# Eval("receipt_place")%></p>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="receipt_dept_name" HeaderText="接收部门">
                        <HeaderStyle Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receipt_person" HeaderText="接收人">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="state" HeaderText="状态">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView></div>         
        
        </div>
        <div id="divJjdDisplay" runat="server">
            <table>
                <tr>
                    <td width="80">
                        单号：</td>
                    <td width="160">
                        <asp:TextBox ID="TxtJjdNo1" runat="server" BorderStyle="Groove" ReadOnly="True" Width="160px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="80">
                        状态：</td>
                    <td width="160">
                        <asp:TextBox ID="TxtState1" runat="server" BorderStyle="Groove" ReadOnly="True" Width="160px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="LnkBtnBackCreateJjd" runat="server" OnClick="LnkBtnBackCreateJjd_Click">[返回]</asp:LinkButton></td>
                </tr>
                <tr>
                    <td width="80">
                        接收日期：</td>
                    <td width="160">
                        <asp:TextBox ID="TxtReceiptDate1" runat="server" BorderStyle="Groove" ReadOnly="True"
                            Width="160px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="80">
                        接收地：</td>
                    <td width="160">
                        <asp:TextBox ID="TxtReceiptPlace1" runat="server" BorderStyle="Groove" ReadOnly="True"
                            Width="160px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        接收部门：</td>
                    <td width="160">
                        <asp:TextBox ID="TxtReceiptDept1" runat="server" BorderStyle="Groove" ReadOnly="True"
                            Width="160px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="80">
                        接收人：</td>
                    <td width="160">
                        <asp:TextBox ID="TxtReceiptPerson1" runat="server" BorderStyle="Groove" ReadOnly="True"
                            Width="160px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="BtnAllFinsh" runat="server" Text="整单完成" OnClick="BtnAllFinsh_Click" CssClass="button_2" />
                </tr>
            </table>
            <hr />
            <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox">
                <asp:GridView ID="GVDisplayJjdLine" runat="server" AutoGenerateColumns="False" Width="1320px"
                    CssClass="gv" OnRowDataBound="GVDisplayJjdLine_RowDataBound" OnRowCommand="GVDisplayJjdLine_RowCommand">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="完成">
                            <ItemTemplate>
                                <asp:ImageButton ID="LnkIssue" runat="server" CausesValidation="False" CommandName="Finish"
                                    ImageUrl="~/images/issue.png" OnClientClick='return confirm("实配数量为："+getInputValue(this)+"，请确认！");'
                                    CommandArgument='<%# Eval("objid") %>' ToolTip="完成" /><asp:Image ID="ImgNotIssue"
                                        runat="server" ImageUrl="~/images/not_issue.png" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实配数量">
                            <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:TextBox ID="TxtIssueQty" runat="server" Text='<%# Eval("zh_qty") %>' Width="50px"></asp:TextBox>
                                <asp:Label ID="LblIssueQty" runat="server" Text='<%# Eval("xh_qty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="zh_qty" HeaderText="配送数量">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="part_no" HeaderText="零件编号">
                            <HeaderStyle Width="160px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="零件描述">
                            <ItemTemplate>
                                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;
                                    margin: 0" title="<%# Eval("part_name_e")%>">
                                    <%# Eval("part_name_e")%>
                                </p>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="package_no" HeaderText="大包编号">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="大包描述">
                            <ItemTemplate>
                                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px;
                                    margin: 0" title="<%# Eval("package_name")%>">
                                    <%# Eval("package_name")%>
                                </p>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="unit" HeaderText="单位">
                            <HeaderStyle Width="60px" />
                        </asp:BoundField>                         
                        <asp:BoundField DataField="rowstate" HeaderText="状态">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="project_id" HeaderText="项目">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="requisition_id" HeaderText="申请号">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
               
            </div>
        </div>
        </div>
    </form>
</body>
</html>
