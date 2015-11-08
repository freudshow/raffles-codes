<%@ Page Language="C#" AutoEventWireup="true" Codebehind="currency.aspx.cs" Inherits="ADMIN.currency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>币种</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function CheckInput()
    {
        if(document.getElementById("TxtCurrencyID").value==""||document.getElementById("TxtCurrencyDesc").value=="")
        {
            alert("必填字段不能为空！");
            return false;
        }
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            币种
            <hr />
        </div>
        <div  style="text-align: left">
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td width="80">
                        币种名称:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtCurrencyID" runat="server" Width="480px"></asp:TextBox><span
                            style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        币种描述:</td>
                    <td>
                        <asp:TextBox ID="TxtCurrencyDesc" runat="server" Width="480px"></asp:TextBox><span
                            style="color: #ff0000">*</span></td>
                </tr>
            </table>
            <hr width="600px" />
            <table cellpadding="0" cellspacing="0"  width="600px">
                <tr>
                    <td>
                        <asp:Button ID="BtnAddCurrency" runat="server" Text="添加" OnClientClick="return CheckInput();"
                            OnClick="BtnAddCurrency_Click" CssClass="button_1" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
                            Width="600px" OnRowCancelingEdit="GV_RowCancelingEdit" OnRowDeleting="GV_RowDeleting"
                            OnRowEditing="GV_RowEditing" OnRowUpdating="GV_RowUpdating" CssClass="gv">
                            
                            <Columns>
                                <asp:BoundField DataField="currency" HeaderText="币种" ReadOnly="True">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="币种描述">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TxtDesc" runat="server" Text='<%# Eval("currency_desc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblDesc" runat="server" Text='<%# Eval("currency_desc") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="320px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="激活">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Checked='<%# Eval("is_valid").ToString().Equals("1")   %>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Checked='<%# Eval("is_valid").ToString().Equals("1")   %>' />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="修改" ShowEditButton="True">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                    <HeaderStyle Width="70px" />
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
