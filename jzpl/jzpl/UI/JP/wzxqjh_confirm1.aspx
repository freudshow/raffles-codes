<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wzxqjh_confirm1.aspx.cs" Inherits="jzpl.wzxqjh_confirm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>普通物资配送确认（生产）</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript" src="../../UI/script/wzxqjh.js"></script>
    <script type="text/javascript">
    
    window.onload=function(){init();}    
    function init() {    
    SetGVBoxHeight("divBox","GVData");
    }

    </script>
</head>

<body >
    <form id="form1" runat="server">
    <div id="title">普通物资配送确认（生产）<hr /></div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    项目：</td>
                <td colspan="4">
                    <asp:DropDownList ID="DdlProject" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <span style="color: #ff0000;"></span>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="ChkOnlyShowConfirmingState" runat="server" Checked="True" Text="只显示待确认" /></td>
                <td>
                    <span style="color: #ff0000"></span>
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
                    材料顺序号：</td>
                <td>
                    <asp:TextBox ID="TxtMtrSeqNo" runat="server"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td>
                    行号：</td>
                <td>
                    <asp:TextBox ID="TxtMtrLineNo" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    物资编码：</td>
                <td>
                    <asp:TextBox ID="TxtPartNo" runat="server"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    接收日期：</td>
                <td>
                    <asp:TextBox ID="TxtRecieveDate" runat="server" onfocus="WdatePicker();"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    接收人：</td>
                <td>
                    <asp:TextBox ID="TxtReciever" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    接收地：</td>
                <td>
                    <asp:DropDownList ID="DdlProdSite" runat="server">
                    </asp:DropDownList></td>
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
                    接收部门：</td>
                <td>
                    <asp:DropDownList ID="DdlReceiptDept" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                    申请组：</td>
                <td>
                    <asp:DropDownList ID="DdlReqGroup" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    库位：</td>
                <td>
                    <asp:TextBox ID="TxtLocation" runat="server"></asp:TextBox></td>
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
        <table cellspacing="0" cellpadding="0" style="padding: 0; margin: 0 0 4px 0" >
            <tr>
                <td>
                    <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" CssClass="button_1"/>
                </td>                
            </tr>
        </table>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="divBox" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" 
                 CssClass="gv" Width="2160px" AllowPaging="True" OnPageIndexChanging="GVData_PageIndexChanging"
                PageSize="12" OnRowCommand="GVData_RowCommand" OnRowDataBound="GVData_RowDataBound">                
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="确定">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgOK" runat="server" CausesValidation="False" CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>'
                                CommandName="Release" ImageUrl="~/images/ok.gif" ToolTip="确认" OnClientClick="return confirm('要确认吗？');" />
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="取消">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgCancel" runat="server" CausesValidation="False" CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>'
                                CommandName="CancelX" ImageUrl="~/images/redx.gif" ToolTip="取消" OnClientClick="return confirm('要取消吗？');" />
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="release_qty" HeaderText="下达数量" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="require_qty" HeaderText="需求数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rowstate" HeaderText="状态">
                        <HeaderStyle Width="60px" CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rowstate_zh" HeaderText="状态">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="缺料原因">                        
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title="<%# Eval("lack_msg")%>">
                                <%# Eval("lack_msg")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="project_id" HeaderText="项目">
                        <HeaderStyle Width="80px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:BoundField>
                    <asp:BoundField DataField="part_no" HeaderText="物资编号">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="物资描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px"
                                title="<%# Server.HtmlEncode(Eval("part_description").ToString())%>">
                                <%# Eval("part_description")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="location" HeaderText="库位">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="matr_seq_no" HeaderText="材料顺序号">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="matr_seq_line_no" HeaderText="行号">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="接收场地">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 160px"
                                title="<%# String.Format("{0}-{1}",Eval("place"),Eval("place_description"))%>">
                                <%# String.Format("{0}-{1}", Eval("place"), Eval("place_description"))%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receive_date_str" HeaderText="接收日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="crane" HeaderText="吊装">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver" HeaderText="接收人">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_ic" HeaderText="IC">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_contact" HeaderText="联系方式">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_block" HeaderText="分段">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_system" HeaderText="系统">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="保管员" HeaderStyle-Width="100px">
                    <ItemTemplate>
                    <asp:Label ID="GVLblReleaseUser" runat="server" Text='<%# Eval("release_user") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
            
        </div>
        
    </form>
</body>
</html>
