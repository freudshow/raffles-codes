<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receipt_person.aspx.cs" Inherits="ADMIN.receipt_person" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>配送接收人</title>
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
    <div id="title">配送接收人<hr /></div>
        <div style="text-align:left">
            <table cellpadding="0" cellspacing="0" width="600px">
                <tr>
                    <td style="width: 80px">
                        姓名:</td>
                    <td>
                        <span style="color: #ff0000">
                            <asp:TextBox ID="TxtName" runat="server" Width="480px"></asp:TextBox>*</span></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        IC号:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtIC" runat="server" Width="480px"></asp:TextBox><span style="color: #ff0000">*</span></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        联系电话:</td>
                    <td>
                        <asp:TextBox ID="TxtContact" runat="server" Width="480px"></asp:TextBox><span
                            style="color: #ff0000">*</span></td>
                </tr>
                
            </table>
            <hr width="600px" />
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td>
                        <asp:Button ID="BtnAddCompany" runat="server" Text="添 加" OnClientClick="return CheckInput();"
                            OnClick="BtnAddCompany_Click" CssClass="button_1" /></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Font-Size="10pt" CssClass="gv"
                            Width="600px" OnRowCancelingEdit="GV_RowCancelingEdit" OnRowEditing="GV_RowEditing"
                            OnRowUpdating="GV_RowUpdating" OnRowDeleting="GV_RowDeleting" OnRowDataBound="GV_RowDataBound">
                            
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="person" HeaderText="姓名" ReadOnly="True">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="ic" HeaderText="IC" ReadOnly="True">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="联系电话">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="GV_TxtContact" runat="server" Text='<%# Eval("contact") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("contact") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
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