<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wh_area.aspx.cs" Inherits="ADMIN.wh_area" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>仓库区域</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function CheckInput()
    {
        if(document.getElementById("TxtAreaID").value==""||document.getElementById("TxtArea").value=="")
        {
            alert("必填字段不能为空！");
            return false;
        }
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="title">仓库区域<hr /></div>
        <div style="text-align:left">
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td width="100">
                        公司:
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlCompany" runat="server" Width="466px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="100">
                        区域编号:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAreaID" runat="server" Width="460px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="100">
                        区域描述:</td>
                    <td>
                        <asp:TextBox ID="TxtArea" runat="server" Width="460px"></asp:TextBox></td>
                </tr>
            </table>
            <hr width="600px" />
            <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                <tr>
                    <td>
                        <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="button_1" OnClick="BtnQuery_Click" />
                        <asp:Button ID="BtnAdd" runat="server" Text="添加" OnClientClick="return CheckInput();"
                            CssClass="button_1" OnClick="BtnAdd_Click" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" CssClass="gv"
                            OnRowCancelingEdit="GV_RowCancelingEdit" OnRowEditing="GV_RowEditing" OnRowDeleting="GV_RowDeleting"
                            OnRowUpdating="GV_RowUpdating" Width="600px">
                            
                            <Columns>
                                <asp:BoundField DataField="company_id" HeaderText="公司" ReadOnly="True">
                                    <HeaderStyle Width="110px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="area_id" HeaderText="区域编号" ReadOnly="True">
                                    <HeaderStyle Width="110px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="area" HeaderText="区域描述" ReadOnly="True">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="激活">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Enabled="false" Checked='<%# Eval("state").ToString().Equals("1")   %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Enabled="true" Checked='<%# Eval("state").ToString().Equals("1") %>' />
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
