<%@ Page Language="C#" AutoEventWireup="true" Codebehind="company.aspx.cs" Inherits="ADMIN.company" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公司</title>
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
        <div id="title">
            
            公司
            <hr />
        </div>
        <div style="text-align: left">
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px" id="input1">
                <tr>
                    <td width="80">
                        公司编号:：</td>
                    <td>
                        <asp:TextBox ID="TxtCompanyID" runat="server" Width="480px"></asp:TextBox><span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        公司名称：</td>
                    <td>
                        <asp:TextBox ID="TxtCompanyName" runat="server" Width="480px"></asp:TextBox><span
                            style="color: #ff0000">*</span></td>
                </tr>
                <tr>
                    <td width="80">
                        公司地址：</td>
                    <td>
                        <asp:TextBox ID="TxtAddress" runat="server" Width="480px"></asp:TextBox></td>
                </tr>
            </table>
            <hr style="width: 800px;" />
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px" >
                <tr>
                    <td>
                        <asp:Button ID="BtnAddCompany" runat="server" Text="添加" OnClientClick="return CheckInput();"
                            OnClick="BtnAddCompany_Click" CssClass="button_1" /></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
                            Width="800px" OnRowCancelingEdit="GV_RowCancelingEdit" OnRowEditing="GV_RowEditing"
                            OnRowUpdating="GV_RowUpdating" OnRowDeleting="GV_RowDeleting" OnRowDataBound="GV_RowDataBound"
                            CssClass="gv">                            
                            <Columns>
                                <asp:BoundField DataField="company_id" HeaderText="公司编号" ReadOnly="True">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="公司名称">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TxtDesc" runat="server" Text='<%# Eval("company") %>' Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px"
                                            title="<%# Eval("company") %>">
                                            <%# Eval("company") %>
                                         </p>
                                    </ItemTemplate>
                                    <HeaderStyle Width="260px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="公司地址">
                                <ItemTemplate>
                                     <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 250px"
                                        title="<%# Eval("address")%>">
                                        <%# Eval("address")%>
                                    </p>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="TxtAddress" runat="server" Text='<%# Eval("address") %>' Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="260px" />
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
