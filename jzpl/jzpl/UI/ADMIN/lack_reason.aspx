<%@ Page Language="C#" AutoEventWireup="true" Codebehind="lack_reason.aspx.cs" Inherits="ADMIN.lack_reason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>缺料原因</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function CheckInput()
    {
        if(document.getElementById("TxtReasonID").value==""||document.getElementById("TxtReasonDesc").value=="")
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
            缺料原因<hr />
        </div>
        <div style="text-align: left">
            <table cellpadding="0" cellspacing="0"  width="600px">
                <tr>
                    <td width="120">
                        缺料原因编号:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtReasonID" runat="server" Width="440px"></asp:TextBox><span style="color: Red">*</span>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        缺料原因描述:</td>
                    <td>
                        <asp:TextBox ID="TxtReasonDesc" runat="server" Width="440px"></asp:TextBox><span
                            style="color: #ff0000">*</span></td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr width="600px" />
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td>
                        <asp:Button ID="BtnAddReason" runat="server" Text="添加" OnClick="BtnAddReason_Click"
                            OnClientClick="return CheckInput();" CssClass="button_1" /></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Font-Size="10pt" CssClass="gv"
                            Width="600px" OnRowCancelingEdit="GV_RowCancelingEdit" OnRowDeleting="GV_RowDeleting"
                            OnRowEditing="GV_RowEditing" OnRowUpdating="GV_RowUpdating">                            
                            <Columns>
                                <asp:BoundField DataField="reason_id" HeaderText="原因编号" ReadOnly="True">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="原因描述">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TxtDesc" runat="server" Text='<%# Eval("reason_desc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblDesc" runat="server" Text='<%# Eval("reason_desc") %>'></asp:Label>
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
