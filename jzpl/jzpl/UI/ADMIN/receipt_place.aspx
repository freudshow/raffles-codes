<%@ Page Language="C#" AutoEventWireup="true" Codebehind="receipt_place.aspx.cs"
    Inherits="ADMIN.receipt_place" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>配送接收场地</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function CheckInput()
    {
        if(document.getElementById("TxtCompanyID").value==""||document.getElementById("TxtCompanyName").value=="")
        {
            alert("必填字段不能为空！");
            return false;
        }
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="title">配送接收场地<hr /></div>
        <div style="text-align:left">
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td style="width: 120px">
                        公 司:</td>
                    <td>
                        <asp:DropDownList ID="DDL_company" runat="server" Width="426px">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        接收场地编号:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtCompanyID" runat="server" Width="420px"></asp:TextBox><span style="color: #ff0000">*</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        接收场地名称:</td>
                    <td>
                        <asp:TextBox ID="TxtCompanyName" runat="server" Width="420px"></asp:TextBox><span
                            style="color: #ff0000">*</span></td>
                </tr>
                
            </table>
            <hr width="600px" />
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td>
                        <asp:Button ID="BtnAddCompany" runat="server" Text="添加" OnClientClick="return CheckInput();"
                            OnClick="BtnAddCompany_Click" CssClass="button_1" />
                        <asp:Button ID="BtnQuery" runat="server" Text="查询" 
                            OnClick="BtnQuery_Click" CssClass="button_1" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
                            Width="600px" OnRowCancelingEdit="GV_RowCancelingEdit" OnRowEditing="GV_RowEditing" CssClass="gv"
                            OnRowUpdating="GV_RowUpdating" OnRowDeleting="GV_RowDeleting" OnRowDataBound="GV_RowDataBound">
                            
                            <Columns>
                                <asp:BoundField DataField="PLACE_ID" HeaderText="接收场地编号" ReadOnly="True">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="接收场地名称">
                                    <ItemTemplate>
                                        <asp:Label ID="LblDesc" runat="server" Text='<%# Eval("PLACE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="220px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="COMPANY_ID" HeaderText="公司" ReadOnly="True">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="激活">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Enabled="false" Checked='<%# Eval("state").ToString().Equals("1")   %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Checked='<%# Eval("state").ToString().Equals("1")   %>' />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="修改" ShowEditButton="True">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
