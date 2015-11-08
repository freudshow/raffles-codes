<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="req_group.aspx.cs" Inherits="ADMIN.req_group" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>申请组</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function CheckInput()
    {
        if(document.getElementById("TxtGroup").value=="")
        {
            alert("必填字段不能为空！");
            return false;
        }
    }
    
    </script>   
</head>
<body>
    <form id="form1" runat="server">
    <div id="title">申请组<hr /></div>
    <div style="text-align:left">
        <table cellpadding="0" cellspacing="0" width="600px">
                <tr>
                    <td width="80">
                        公 司:
                    </td>
                    <td >
                        <asp:DropDownList ID="DDL_company" runat="server" Width="486px">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                </tr>
            <tr>
                <td width="80">
                        申请组:</td>
                <td >
                    <asp:TextBox ID="TxtGroup" runat="server" Width="480px"></asp:TextBox><span style="color: #ff0000">*</span></td>
            </tr>
            </table>
            <hr width="600px" />
            <table  cellpadding="0" cellspacing="0" width="600px">
            <tr>
            <td>
            <asp:Button ID="BtnAdd" runat="server" Text="添加" OnClientClick="return CheckInput();" CssClass="button_1" OnClick="BtnAdd_Click" />
            <asp:Button ID="BtnQuery" runat="server" Text="查询"  CssClass="button_1" OnClick="BtnQuery_Click" />
            </td>
            </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" CssClass="gv" Width="600px" OnRowCancelingEdit="GV_RowCancelingEdit" OnRowEditing="GV_RowEditing" OnRowUpdating="GV_RowUpdating" OnRowDeleting="GV_RowDeleting" OnRowDataBound="GV_RowDataBound">
                        
                            <Columns>
                                <asp:TemplateField HeaderText="申请组">
                                    <ItemTemplate>
                                        <asp:Label ID="LblDesc" runat="server" Text='<%# Eval("group_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="320px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="COMPANY_ID" HeaderText="公司" ReadOnly="True" >
                                     <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="激活">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Enabled="false" Checked='<%# Eval("state").ToString().Equals("1")   %>'  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="ChkActive" runat="server" Checked='<%# Eval("state").ToString().Equals("1")   %>'  />
                                    </EditItemTemplate> 
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="修改" ShowEditButton="True" >
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:CommandField HeaderText="删除" ShowDeleteButton="True" >
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr></table>
    </div>
    </form>
</body>
</html>
