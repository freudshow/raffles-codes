<%@ Page Language="C#" AutoEventWireup="true" Codebehind="project_acc_per.aspx.cs"
    Inherits="ADMIN.project_acc_per" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户操作项目授权</title>    
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="title">用户操作项目授权<hr /></div>
        <div style="text-align:left">
            <table cellpadding="0" cellspacing="0"  style="margin-bottom:4px">
                <tr>
                    <td style="width: 42px">
                        用户:
                    </td>
                    <td style="width:210px">
                        <asp:DropDownList ID="DdlUser" runat="server" OnSelectedIndexChanged="DdlUser_SelectedIndexChanged"
                            Width="200px" AutoPostBack="True">
                        </asp:DropDownList></td><td>
                        <asp:Button ID="BtnAddCompany" runat="server" CssClass="button_1" OnClick="BtnAddCompany_Click"
                            Text="保存" style="margin-bottom:0px" /></td>
                </tr>
            </table>
            <asp:Panel ID="Panel2" runat="server" Width="600px">
                <asp:GridView ID="gv_project" runat="server" AutoGenerateColumns="False" Width="600px" CssClass="gv">
                    
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("judge").ToString().Equals("1")||Eval("judge").ToString().Equals("%") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="project_id" HeaderText="项目号">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="project_name" HeaderText="项目描述">
                            <ItemStyle Width="400px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
