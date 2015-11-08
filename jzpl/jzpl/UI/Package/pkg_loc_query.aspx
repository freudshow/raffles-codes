<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="pkg_loc_query.aspx.cs" Inherits="Package.pkg_loc_query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>大包库存查询</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript" src="../../UI/script/wzxqjh.js"></script>    
    <script type="text/javascript" language="javascript">
    window.onload=function(){init();}
    function init(){
    SetGVBoxHeight("divBox","GVData")
    }
        function ShowDBPage()
        {
            var obj= window.showModalDialog("pkg_lov.aspx",window,"status:no;dialogWidth:670px;dialogHeight:580px");
            if(typeof(obj)=="undefined")
            {
                document.getElementById("TxtPackageNo").value="";
                return;
            }
            document.getElementById("TxtPackageNo").value=obj[0];
            document.getElementById("TxtPackageNo").readOnly=true;
            document.getElementById("TxtPartNo").value="";
            document.getElementById("TxtPartNameE").value="";
        }
        function ShowXJPage()
        {
            var dbbhValue=document.getElementById("TxtPackageNo").value;
            if(dbbhValue=="" || dbbhValue==null)
            {
                alert('请首先选择大包信息！');
                return;
            }
            var obj= window.showModalDialog("pkg_part_lov.aspx?dbbh="+dbbhValue+"",window,"status:no;dialogWidth:610px;dialogHeight:556px");
            if(typeof(obj)=="undefined")
            {
                document.getElementById("TxtPartNo").value="";
                document.getElementById("TxtPartNameE").value="";
                document.getElementById("TxtPartNo").readOnly=false;
                return;
            }
            document.getElementById("TxtPartNo").value=obj[0];
            document.getElementById("TxtPartNo").readOnly=true;
            document.getElementById("TxtPartNameE").value = obj[2];
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="title">大包库存查询<hr /></div>
        <table cellpadding="0" cellspacing="0" >
            <tr>
                <td style="width: 80px">
                    项目：</td>
                <td colspan="4">
                    <asp:DropDownList ID="DdlProject" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 40px">
                </td>
            </tr>
            <tr>
                <td style="width: 80px">
                    大包编码：</td>
                <td style="width: 205px">
                    <asp:TextBox ID="TxtPackageNo" runat="server" Width="200px"></asp:TextBox></td>
                <td width="40px">
                    <span style="color: #ff0000">%</span></td>                
                <td style="width: 80px">
                    小件编码：</td>
                <td style="width: 205px">
                    <asp:TextBox ID="TxtPartNo" runat="server" Width="200px"></asp:TextBox></td>
                <td style="width: 40px">
                    <span style="color: #ff0000">%</span></td>
                 
            </tr>
            <tr>
                <td >
                    大包名称：</td>
                <td style="width: 205px">
                    <asp:TextBox ID="TxtPkgName" runat="server" Width="200px"></asp:TextBox></td>
                <td >
                    <span style="color: #ff0000">%</span></td>
                <td style="width: 80px" >
                    小件名称：</td>
                <td style="width: 205px">
                    <asp:TextBox ID="TxtPartNameE" runat="server" Width="200px"></asp:TextBox></td>
                <td style="width: 40px">
                    <span style="color: #ff0000">%</span></td>
                
            </tr>
            <tr>
                <td>
                    小件规格：</td>
                <td style="width: 205px">
                    <asp:TextBox ID="TxtSpec" runat="server" 
                        Width="200px"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td style="width: 80px">
                    到货日期：</td>
                <td style="width: 205px">
                    <asp:TextBox ID="TxtQueDate" runat="server" onfocus="WdatePicker();"
                        Width="200px"></asp:TextBox></td>
                <td style="width: 40px">
                </td>
            </tr>
            <tr>
                <td style="height: 24px">
                    关单号：</td>
                <td style="width: 205px; height: 24px;">
                    <asp:TextBox ID="TxtDec" runat="server" 
                        Width="200px"></asp:TextBox></td>
                <td style="height: 24px">
                    <span style="color: #ff0000">%</span></td>
                <td style="width: 80px; height: 24px">
                    存储区域：</td>
                <td style="width: 205px; height: 24px;">
                    <asp:TextBox ID="TxtArea" runat="server" 
                        Width="200px"></asp:TextBox></td>
                <td style="width: 40px; height: 24px;">
                    <span style="color: #ff0000">%</span></td>
            </tr>
            <tr>
                <td style="height: 24px">
                    存储位置：</td>
                <td style="width: 205px; height: 24px">
                    <asp:TextBox ID="TxtLoc" runat="server" 
                        Width="200px"></asp:TextBox></td>
                <td style="height: 24px">
                    <span style="color: #ff0000">%</span></td>
                <td style="width: 80px; height: 24px">
                </td>
                <td style="width: 205px; height: 24px">
                </td>
                <td style="width: 40px; height: 24px">
                </td>
            </tr>
        </table>
        <hr />
        <table>
            <tr>
                <td style="width: 200px">
                    <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" CssClass="button_1"/>
                    <asp:Button ID="BtnExportExcel" runat="server" Text="导出" OnClick="BtnExportExcel_Click" CssClass="button_1"/></td>
                
            </tr>
        </table>
        <div style="overflow-x: auto; overflow-y: none; width: 100%; " id="divBox" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" 
                CssClass="gv" Width="2000px" OnPageIndexChanging="GVData_PageIndexChanging"
                PageSize="12" EmptyDataText="无满足条件的数据" OnRowDataBound="GVData_RowDataBound" AllowPaging="True" OnDataBound="GVData_DataBound">              
                
                <Columns>
                <asp:BoundField DataField="project_id" HeaderText="项目" >
                    <HeaderStyle Width="120px" />
                </asp:BoundField>
                    <asp:BoundField DataField="package_no" HeaderText="大包编码">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField> 
                    <asp:TemplateField HeaderText="大包名称">
                    <ItemTemplate>
                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title='<%# Server.HtmlEncode(Eval("package_name").ToString())%>'>
                                <%# Eval("package_name")%>

                           </p>
                    </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="part_no" HeaderText="小件编号">
                        <HeaderStyle Width="180px" />
                    </asp:BoundField>
                    
                    <asp:TemplateField HeaderText="小件名称">
                    <ItemTemplate>
                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 250px"
                                title='<%# Server.HtmlEncode(Eval("part_name_e").ToString())%>'>
                                <%# Eval("part_name_e")%>

                           </p>
                    </ItemTemplate>
                        <HeaderStyle Width="260px" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="unit" HeaderText="小件单位">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
                    </asp:BoundField>
                    
                    <asp:TemplateField HeaderText="小件规格">
                    <ItemTemplate>
                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title='<%# Server.HtmlEncode(Eval("spec").ToString())%>'>
                                <%# Eval("spec")%>

                           </p>
                    </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="arrived_date" HeaderText="到货日期" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="area_id" HeaderText="区域ID" >
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="存储区域">
                    <ItemTemplate>
                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title='<%# Server.HtmlEncode(Eval("area").ToString())%>'>
                                <%# Eval("area")%></p>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="存储位置">
                    <ItemTemplate>
                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title='<%# Server.HtmlEncode(Eval("location").ToString())%>'>
                                <%# Eval("location")%></p>
                    </ItemTemplate>   
                    <HeaderStyle Width="200px" />                 
                    </asp:TemplateField>
                    <asp:BoundField DataField="part_ok_qty" HeaderText="合格数量">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="part_bad_qty" HeaderText="不合格数量">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="no_check_qty" HeaderText="免检数量">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <%--<PagerSettings Mode="NumericFirstLast" />--%>
                <PagerTemplate>
                
                    <table width="800px" style="border: 0px; " align="left">
                    <tr>
                    <td >
                    <asp:Label ID="lblCurrrentPage" runat="server" ForeColor="#CC3300">000</asp:Label>
                    <span>移至</span>
                    <asp:DropDownList ID="page_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="page_DropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span>页</span>
                    <asp:LinkButton ID="lnkBtnFirst" CommandArgument="First" CommandName="page" runat="server">第一页</asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnPrev" CommandArgument="prev" CommandName="page" runat="server">上一页</asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnNext" CommandArgument="Next" CommandName="page" runat="server">下一页</asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnLast" CommandArgument="Last" CommandName="page" runat="server">最后一页</asp:LinkButton>
                    </td>
                    </tr>
                    </table>

                </PagerTemplate>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
