<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wzxqjh_query_ext.aspx.cs"
    Inherits="jzpl.wzxqjh_query_ext" EnableEventValidation="false" %>

<%@ Register Src="../FrameUI/ProjectLoader.ascx" TagName="ProjectLoader" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>普通物资申请查询</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript" src="../../UI/script/wzxqjh.js"></script>

    <script type="text/javascript">
    window.onload=function(){init();}
    
    function init() {
    
    SetGVBoxHeight("gvbox","GVData");
    }
    
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            普通物资申请查询<hr />
        </div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    项目：</td>
                <td colspan="4">
                    <asp:DropDownList ID="DdlProject" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <span style="color: #ff0000"></span>
                </td>
                <td>
                </td>
                <td>
                    吊装：</td>
                <td>
                    <asp:DropDownList ID="DdlDz" runat="server">
                        <asp:ListItem Value="all">全部</asp:ListItem>
                        <asp:ListItem Value="Y">吊装</asp:ListItem>
                        <asp:ListItem Value="N">不需要吊装</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    <span style="color: #ff0000"></span>
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
                    &nbsp;&nbsp;</td>
                <td>
                    物资编码：</td>
                <td>
                    <asp:TextBox ID="TxtPartNo" runat="server"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    需求日期：</td>
                <td >
                    <asp:TextBox ID="TxtRecieveDate" runat="server" onfocus="WdatePicker();"></asp:TextBox></td><td>
                    </td><td>
                        至</td>
                <td>
                    <asp:TextBox ID="TxtRecDataEnd" runat="server" onfocus="WdatePicker();"></asp:TextBox></td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    下达日期：</td>
                <td>
                    <asp:TextBox ID="TxtReleaseDateS" runat="server" onfocus="WdatePicker();"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    <span style="color: black">至</span></td>
                <td>
                    <asp:TextBox ID="TxtReleaseDateE" runat="server" onfocus="WdatePicker();"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    接收人：</td>
                <td>
                    <asp:TextBox ID="TxtReciever" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    接收地：</td>
                <td>
                    <asp:DropDownList ID="DdlProdSite" runat="server" Width="154px">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    接收部门：</td>
                <td>
                    <asp:DropDownList ID="DdlReceiptDept" runat="server" Width="155px">
                    </asp:DropDownList></td>
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
                    申请组：</td>
                <td>
                    <asp:DropDownList ID="DdlReqGroup" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                    申请人：</td>
                <td>
                    <asp:TextBox ID="TxtRecorder" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    保管员：</td>
                <td>
                    <asp:TextBox ID="TxtReleaseUser" runat="server"></asp:TextBox></td>
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
                    分段：</td>
                <td>
                    <asp:TextBox ID="Txtfd" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    系统：</td>
                <td>
                    <asp:TextBox ID="Txtxt" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    施工内容：</td>
                <td>
                    <asp:TextBox ID="Txtsg" runat="server"></asp:TextBox></td>
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
                    库位：</td>
                <td>
                    <asp:TextBox ID="TxtLocation" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    状态：</td>
                <td>
                    <asp:TextBox ID="TxtRowstate" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                </td>
                <td colspan="2">
                    <span style="font-size: 8pt; color: #ff0000">（I/初始 R/下达 F/完成 C/待确认 Ca/取消）</span></td>
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
                    申请日期：</td>
                <td>
                    <asp:TextBox ID="TxtReqDate" runat="server" onfocus="WdatePicker();"></asp:TextBox></td>
                <td>
                 </td><td>
                 </td>
                <td>
                    <asp:DropDownList ID="DDL_Ration" runat="server">
                        <asp:ListItem Value="no">不查ERP定额数量</asp:ListItem>
                        <asp:ListItem Value="yes">查询ERP定额数量</asp:ListItem>
                    </asp:DropDownList></td> 
                <td colspan=3><span style="font-size: 8pt; color: #ff0000">批量查询时，ERP定额数量会影响速度</span></td>
                <td>
                </td>
                <td>
                </td>
                <td colspan="2">
                </td>
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
        </table>
        <hr />
        <table cellspacing="0" cellpadding="0" style="padding: 0; margin-bottom: 4px" width="100%">
            <tr>
                <td style="width: 200px">
                    <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" CssClass="button_1" />&nbsp;
                    <asp:Button ID="BtnHtmlToExcel" runat="server" Text="EXCEL" CssClass="button_1" OnClick="BtnHtmlToExcel_Click" /></td>
                <td style="width: 100px">
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                CssClass="gv" Width="3020px" AllowPaging="True" OnPageIndexChanging="GVData_PageIndexChanging"
                PageSize="12" OnRowDataBound="GVData_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="project_id" HeaderText="项目">
                        <HeaderStyle Width="80px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:BoundField>
                    <asp:BoundField DataField="part_no" HeaderText="物资编号">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="物资描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px" title="<%# Server.HtmlEncode(Eval("part_description").ToString())%>">
                                <%# Server.HtmlEncode(Eval("part_description").ToString())%>
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
                    <asp:BoundField DataField="ration_qty" HeaderText="定额数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="require_qty" HeaderText="需求数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="release_qty" HeaderText="下达数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="issued_qty" HeaderText="下发数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="接收地">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 160px"
                                title="<%# String.Format("{0}-{1}",Eval("place"),Eval("place_description"))%>">
                                <%# String.Format("{0}-{1}", Eval("place"), Eval("place_description"))%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receive_date_str" HeaderText="需求日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="record_time_str" HeaderText="创建日期">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="release_time_str" HeaderText="下达日期">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="finish_time_str" HeaderText="完成日期">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="吊装">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkCrane" runat="server" Enabled="false" />
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
                    <asp:BoundField DataField="receiver_contact" HeaderText="联系方式">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_block" HeaderText="分段">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="系统" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 120px"
                                title='<%# Eval("project_system")%>'>
                                <%# Eval("project_system")%>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="施工内容" HeaderStyle-Width="180px">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 120px"
                                title='<%# Eval("work_content")%>'>
                                <%# Eval("work_content")%>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="接收部门" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 120px"
                                title='<%# String.Format("{0}-{1}",Eval("receipt_company"),Eval("receipt_dept_name"))%>'>
                                <%# String.Format("{0}-{1}", Eval("receipt_company"), Eval("receipt_dept_name"))%>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="申请组" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 120px"
                                title='<%# Eval("req_group_name")%>'>
                                <%# Eval("req_group_name")%>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="release_user" HeaderText="保管员" HeaderStyle-Width="100px"/>
                    <asp:BoundField DataField="rowstate_zh" HeaderText="状态">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="jjd_no" HeaderText="交接单号">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="缺料原因">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px" title="<%# Server.HtmlEncode(Eval("LACK_MSG").ToString())%>">
                                <%# Server.HtmlEncode(Eval("LACK_MSG").ToString())%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
