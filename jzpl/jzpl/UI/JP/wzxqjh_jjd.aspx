<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wzxqjh_jjd.aspx.cs" Inherits="jzpl.wzxqjh_jjd"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>物资交接单</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style> 
    #divJjdDisplay{}
    #divJjdHeadInfo{width:600px;border:1px solid #DDDDDD;background:#EEEEE7;margin:5px 0 10px 0;}
    #divJjdHeadInfo div{margin:10px 10px 10px 10px;padding:10px 10px 10px 10px;border:1px solid #888}
    #divReqData{margin:5px 0 0 0}
    #BtnSaveJjdHeadInfo{}    
    .button_2{width:100px;height:21px;font-size:10pt;padding:0}     
    </style>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../../UI/script/wzxqjh_jjd.js"></script>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript"> 
    
    window.onload=function(){init();}
    
    function init() {    
    SetGVBoxHeight("gvbox1","GVReqData");
    SetGVBoxHeight("gvbox2","GVDisplayJjdLine");
    SetGVBoxHeight("gvbox3","GVJjdList");
    }

    </script>

</head>
<body>
    <div id="overLay">
    </div>
    <form id="form1" runat="server">
        <div id="title">
            普通物资交接单
            <hr />
        </div>
        <div>
            <div id="divJjdQuery" runat="server">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 80px">
                            交接单号：</td>
                        <td style="width: 160px">
                            <asp:TextBox ID="TxtQJjdNo" name="TxtRecieveDate" runat="server" AutoPostBack="True"
                                Width="142px"></asp:TextBox></td>
                        <td style="width: 25px">
                            &nbsp;</td>
                        <td>
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
                        <td style="width: 53px">
                            接收人：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="TxtQReceiptPerson" runat="server" AutoPostBack="True" name="TxtRecieveDate"
                                Width="142px"></asp:TextBox></td>
                        <td style="width: 25px">
                            <span style="color: #ff0000"></span>
                        </td>
                        <td>
                            &nbsp;</td>
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
                <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox3">
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
                            <asp:BoundField DataField="receipt_place" HeaderText="接收地">
                                <HeaderStyle Width="160px" />
                            </asp:BoundField>
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
                    </asp:GridView>
                </div>
            </div>
             <div id="divJjdDisplay" runat="server">
            <table style="width: 600px">
                <tr>
                    <td>
                        单号：</td>
                    <td>
                        <asp:TextBox ID="TxtJjdNo1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        状态：</td>
                    <td>
                        <asp:TextBox ID="TxtState1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="LnkBtnBackCreateJjd" runat="server" OnClick="LnkBtnBackCreateJjd_Click">[返回]</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        接收日期：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptDate1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        接收地：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptPlace1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        接收部门：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptDept1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        接收人：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptPerson1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="LnkBtnJjdExtHeadInfoDisplay" runat="server" OnClick="LnkBtnJjdExtHeadInfoDisplay_Click">[详细信息]</asp:LinkButton>
                </tr>
            </table>
            <hr />
            <table >
                <tr>
                    <td>
                        <asp:Button ID="BtnPrint" runat="server" Text="打印交接单" OnClick="BtnPrint_Click" /></td>
                </tr>
            </table>
            <div id="divJjdHeadInfo" runat="server">
                <div style="border-width: 0; margin: 0 10px -10px 10px">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right;">
                                <a
                                    href="#" onclick="myf('divJjdHeadInfo').style.display='none'"> [隐藏]</a></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                装货部门信息</td>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 200px">
                                装货地（仓库）：
                            </td>
                            <td>
                                <asp:Label ID="LblZHd" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                装货人：</td>
                            <td>
                                <asp:Label ID="LblZHr" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                装货人电话</td>
                            <td>
                                <asp:Label ID="LblZHDh" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                车辆到达装货地时间：</td>
                            <td>
                                <asp:Label ID="LblZHtime" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                装货开始：</td>
                            <td>
                                <asp:Label ID="LblZHSTime" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                装货结束：</td>
                            <td>
                                <asp:Label ID="LblZHETime" runat="server"></asp:Label>
                                </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                需求部门信息</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 200px">
                                需求部门/项目：
                            </td>
                            <td>
                                <asp:Label ID="LblXQbm" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                联系人姓名：</td>
                            <td>
                                <asp:Label ID="LblXQlxr" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                联系人电话：</td>
                            <td>
                                <asp:Label ID="LblXQdh" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                承运部门信息</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 200px">
                                承运公司名称：
                            </td>
                            <td>
                                <asp:Label ID="LblCYgs" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                承运人：</td>
                            <td>
                                <asp:Label ID="LblCYPer" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                承运人电话：</td>
                            <td>
                                <asp:Label ID="LblCYdh" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                车辆牌号：</td>
                            <td>
                                <asp:Label ID="LblCYph" runat="server"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                驾驶证号：</td>
                            <td>
                                <asp:Label ID="LblCYjz" runat="server"></asp:Label>
                                </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                收货部门信息</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 200px">
                                卸货开始：</td>
                            <td>
                                <asp:Label ID="LblXHSTime" runat="server"></asp:Label>
                                </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                卸货结束：</td>
                            <td>
                                <asp:Label ID="LblXHETime" runat="server"></asp:Label>
                                </td>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="border-width: 0; margin: 0 10px 0 10px">
                    <table style="width: 100%; margin: 0">
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkSafe" runat="server" Text="保险" /></td>
                            <td style="text-align: right">
                                </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox2">
                <asp:GridView ID="GVDisplayJjdLine" runat="server" AutoGenerateColumns="False" Width="1200px"
                    CssClass="gv">
                    <Columns>
                   
                        <asp:BoundField DataField="requisition_id" HeaderText="申请号">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="part_no" HeaderText="物资编号">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="物资描述">
                            <ItemTemplate>
                                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;
                                    margin: 0" title="<%# Eval("part_description")%>">
                                    <%# Eval("part_description")%>
                                </p>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="part_unit" HeaderText="单位">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="req_qty" HeaderText="下达数量">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="zh_qty" HeaderText="配送数量">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="xh_qty" HeaderText="接收数量">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rowstate" HeaderText="状态">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="project_id" HeaderText="项目">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="matr_seq_no" HeaderText="材料顺序号">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="matr_seq_line_no" HeaderText="行号">
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
