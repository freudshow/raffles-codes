<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wzxqjh_ration_lov.aspx.cs"
    Inherits="jzpl.UI.JP.wzxqjh_ration_lov" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERP定额查询</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript">
        function selectRation(project_id,mtrNo, partNo, partName)
    {   //myf("DdlProject").value
        var ret_ = [project_id, mtrNo, partNo, partName];
        window.returnValue = ret_;
        window.close();
    }
    function checkInput()
    {
        if (myf("DdlProject").value == "0" && myf("TxtMtrNo").value == "" && myf("TxtPartNo").value == "" && myf("TxtPartName").value == "")
        {
            alert("不能所有项全为空！");
            return false;
        }
        return true;
    }
    </script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    
        <div style="margin: 8px 2px 8px 8px;"><div id="title">ERP定额查询<hr /></div>
            <table cellpadding="0" cellspacing="0" style="vertical-align: middle; font-size: 10pt">
                <tr>
                    <td style="width: 68px">
                        <asp:Label ID="LProject" runat="server" Text="项目："></asp:Label></td>
                    <td style="width: 318px">
                        <asp:DropDownList ID="DdlProject" runat="server" Width="316px">
                        </asp:DropDownList></td>
                    <td style="width: 85px">
                        <asp:Label ID="LMtr" runat="server" Text="物料流水号："></asp:Label></td>
                    <td style="width: 183px">
                        <asp:TextBox ID="TxtMtrNo" runat="server" Width="153px" ></asp:TextBox></td>
                    <td style="width: 63px">
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="vertical-align: middle; font-size: 10pt">
                <tr>
                    <td style="width: 68px">
                        <asp:Label ID="Label2" runat="server" Text="物资编码："></asp:Label></td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TxtPartNo" runat="server" Width="80px"></asp:TextBox></td>
                    <td style="width: 68px">
                        <asp:Label ID="Label3" runat="server" Text="物资描述："></asp:Label></td>
                    <td style="width: 419px">
                        <asp:TextBox ID="TxtPartName" runat="server" Width="388px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    <td style="width: 62px">
                        <asp:Button ID="BtnShowRation" runat="server" Text="查询" OnClientClick="return checkInput();"
                            OnClick="BtnShowRation_Click" CssClass="button_1" /></td>
                </tr>
            </table>
       
        <hr />
        <div style="overflow-x:scroll;overflow-y:scroll;width:720px;height:500px" runat="server" id="DataBox">
        <asp:GridView ID="GVRation" runat="server" AutoGenerateColumns="False" CellPadding="0"
            ForeColor="#333333" Font-Size="10pt" Width="1030px" EmptyDataText="<b>无可用定额</b>"
            CssClass="gv" BorderWidth="1px" PageSize="30" EnableModelValidation="True" >
            <Columns>
                <asp:BoundField DataField="project_id" HeaderText="项目号" >
                <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="活动">
                    <ItemTemplate>
                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;" title="<%# Eval("activity_desc")%>">
                            <%# Eval("activity_desc")%>
                        </p>
                    </ItemTemplate>
                    <HeaderStyle Width="300px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="材料顺序号">
                <ItemTemplate>                
                <u onclick="selectRation('<%# Eval("project_id") %>','<%# Eval("misc_tab_ref_no") %>','<%# Eval("part_no") %>','<%# Server.HtmlEncode(Eval("part_name").ToString()) %>')" style="color:Blue"><%# Eval("misc_tab_ref_no")%></u>
                </ItemTemplate>  
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:BoundField DataField="site" HeaderText="域" >
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="part_no" HeaderText="零件编号" >
                    <HeaderStyle Width="70px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="零件描述">
                <ItemTemplate>
                <p style="text-overflow:ellipsis;white-space:nowrap;overflow:hidden;width:200px;" title="<%# Server.HtmlEncode(Eval("part_name").ToString()) %>"><%# Server.HtmlEncode(Eval("part_name").ToString()) %></p>
                </ItemTemplate>
                    <HeaderStyle Width="200px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PARTIAL_INFO" HeaderText="Partial Information" >
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="DESIGN_CODE" HeaderText="Design Code/Name" >
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="material_req_seq_no" HeaderText="行号">
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="RATION_QTY" HeaderText="定额数量" >
                    <HeaderStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="issued_qty" HeaderText="下发数量">
                    <HeaderStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="REQUESTED_QTY" HeaderText="申请数量">
                    <HeaderStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="REMAIN_QTY" HeaderText="剩余数量">
                    <HeaderStyle Width="70px" />
                </asp:BoundField>
            </Columns>            
        </asp:GridView>
        </div>
        
        <asp:Label runat="server" ID="LblMsg" ForeColor="red"></asp:Label> </div>
    </form>
</body>
</html>