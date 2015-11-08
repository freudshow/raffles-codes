<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pkg_loc_lov.aspx.cs" Inherits="jzpl.UI.Package.pkg_loc_lov" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>库位查询</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function SelectedLoc(company,areaid,loc,locid)
    {        
        var ret = new Array(company,areaid,loc,locid);
        window.returnValue =ret;
        window.close();
    }
    
    </script>
    <style type="text/css">body{overflow:hidden}</style>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:10px;height:560px">
    <div id="title">库位查询<hr /></div>
    <table style=" margin-top: 10px">
        <tr>
            <td style="width: 80px">
                公司：</td>
            <td style="width: 400px">
                <asp:TextBox ID="TxtCompany" runat="server" Enabled="False" Width="367px"></asp:TextBox>
                <%--<asp:DropDownList ID="DdlCompany" runat="server" Width="376px">
                </asp:DropDownList>--%></td>
        </tr>
                <tr>
                    <td style="width: 80px;">
                        区域：</td>
                    <td style="width: 400px">
                        <asp:DropDownList ID="DdlArea" runat="server" Width="376px">
                        </asp:DropDownList></td>
                    
                </tr>
            </table>
            <table>
                <tr>
                    <td style="width: 80px;">
                        库位：
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="TxtLocation" runat="server" Width="370px"></asp:TextBox>
                        <span style="color: red">%</span>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnLocationQuery" runat="server" CssClass="button_1" Text="查询" OnClick="BtnLocationQuery_Click" /></td>
                </tr>
            </table>
           
            
            <div style="width: 500px; height: 300px; overflow: scroll; border: 1px solid;" >
                <asp:GridView ID="GVLocation" runat="server" AutoGenerateColumns="False" Width="480px"
                    CssClass="gv">
                    <Columns>
                        <asp:BoundField DataField="company_id" HeaderText="公司" >
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="area" HeaderText="区域" >
                            <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="库位">
                            <ItemTemplate>
                                <u onclick="SelectedLoc('<%# Eval("company_id") %>','<%# Eval("area_id") %>','<%# Eval("location") %>','<%# Eval("location_id") %>')"
                                    style="color: #0000ff; cursor: hand;">
                                    <%# Eval("location") %>
                                </u>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
           
    </div>
    </form>
</body>
</html>
