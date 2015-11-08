<%@ Page Language="C#" AutoEventWireup="true" Codebehind="project.aspx.cs" Inherits="ADMIN.project" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function CheckInput()
    {
        if(document.getElementById("TxtCompanyID").value==""||document.getElementById("TxtCompanyName").value==""||document.getElementById("txt_short_name").value=="")
        {
            alert("必填字段不能为空！");
            return false;
        }
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="title">项目<hr /></div>
        <div style="text-align:left">
            <asp:Panel ID="PnlTop" runat="server">
                <table cellpadding="0" cellspacing="0" width="600px">
                    <tr>
                        <td style="width: 100px">
                            项目编号：</td>
                        <td style="width: 450px">
                            <asp:TextBox ID="TxtProjectID" runat="server" Width="420px"></asp:TextBox><span style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            项目名称：</td>
                        <td>
                            <asp:TextBox ID="TxtProjectName" runat="server" Width="420px"></asp:TextBox><span
                                style="color: Red">*</span></td>
                    </tr>
                    <tr>
                        <td>
                            项目简称：</td>
                        <td>
                            <asp:TextBox ID="txt_short_name" runat="server" Width="420px"></asp:TextBox><span
                                style="color: #ff0000">*</span></td>
                    </tr>
                </table>
                <hr width="600px" />
                <table cellpadding="0" cellspacing="0" style="font-size: 10pt" width="600px">
                    <tr>
                        <td style="width: 600px">
                            <asp:Button ID="BtnAddCompany" runat="server" Text="添加项目" OnClientClick="return CheckInput();"
                                OnClick="BtnAddCompany_Click" CssClass="button_1" />
                            &nbsp;
                            <asp:Button ID="Button1" runat="server" Text="导入项目" OnClick="Button1_Click" CssClass="button_1" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
                    Width="600px" OnRowCancelingEdit="GV_RowCancelingEdit" OnRowEditing="GV_RowEditing"
                    OnRowUpdating="GV_RowUpdating" OnRowDeleting="GV_RowDeleting" OnRowDataBound="GV_RowDataBound" CssClass="gv">                    
                    <Columns>
                        <asp:BoundField DataField="project_id" HeaderText="项目编号" ReadOnly="True">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="项目名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtDesc" runat="server" Text='<%# Eval("project_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblDesc" runat="server" Text='<%# Eval("project_name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="240px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目简称">
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtShort" runat="server" Text='<%# Eval("short_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblShort" runat="server" Text='<%# Eval("short_name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="激活">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkActive" runat="server" Enabled="false" Checked='<%# Eval("state").ToString().Equals("1")   %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="ChkActive" runat="server" Checked='<%# Eval("state").ToString().Equals("1")   %>' />
                            </EditItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="修改" ShowEditButton="True">
                            <HeaderStyle Width="70px" />
                            <ItemStyle HorizontalAlign="center" />
                        </asp:CommandField>
                        <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                            <HeaderStyle Width="70px" />
                            <ItemStyle HorizontalAlign="center" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="Panel1" runat="server" Width="600px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
                    Width="600px" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="gv">
                    
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkActive" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="project_id" HeaderText="项目编号" ReadOnly="True">
                            <HeaderStyle Width="180px" />
                            <ItemStyle Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="project_name" HeaderText="项目名称">
                            <ItemStyle Width="380px" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Font-Size="12px" />
                    <FooterStyle Font-Size="12px" />
                </asp:GridView>
                <table width="600">
                    <tr>
                        <td style="text-align:left" >
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">导  入</asp:LinkButton>
                            &nbsp; &nbsp;<asp:LinkButton ID="LinkButton2" runat="server"  OnClick="LinkButton2_Click">返  回</asp:LinkButton></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
