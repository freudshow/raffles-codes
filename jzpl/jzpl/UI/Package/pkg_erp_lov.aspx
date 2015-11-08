<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_erp_lov.aspx.cs" Inherits="jzpl.UI.Package.pkg_erp_lov" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERP系统大包物资查询</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript" language="javascript">
        function ClosePage()
        {
            this.window.opener=null;
            this.window.close();
        }
        function SelectDB(dbid,dbmc,dbpd,dbpo,dbam,dbcu)
        {
            var return_array = new Array();
            return_array[0]=dbid;
            return_array[1]=dbmc;
            return_array[2]=dbpd;
            return_array[3]=dbpo;
            return_array[4]=dbam;
            return_array[5]=dbcu;
            window.returnValue=return_array;
            window.opner=null;
            window.close();
        }
        function InputCheck()
        {
            if(myf("TxtDBbh").value=="" && myf("TxtDBmc").value=="" && myf("TxtPO").value=="" && myf("DDLProject").value=="0")
            {
                alert("请输入必要条件后再进行查询。")
                return false;
            }
            return true;
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:10px">
        <div id="title">
            ERP系统大包物资查询<hr />
        </div>
        <div>
            <table>
                <tr>
                    <td style="width: 80px">
                        大包编号：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtDBbh" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td style="width: 20px">
                        <span style="color: #ff0000">%</span></td>
                    <td>
                        大包描述：
                    </td>
                    <td style="width: 240px">
                        <asp:TextBox ID="TxtDBmc" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                </tr>
                <tr>
                    <td>
                        PO：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPO" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        项目编号：
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLProject" runat="server" Width="206px">
                        </asp:DropDownList><span style="color: #ff0000">*</span>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnResult" runat="server" CssClass="button_1" Text="查看" OnClick="BtnResult_Click"
                            OnClientClick="return InputCheck();" /></td>
                </tr>
            </table>
        </div>
        <div style="width: 620px; height: 350px; overflow: scroll; border: 1px solid; ">
            <asp:GridView ID="DBGrid" runat="server" Width="600px" CssClass="gv" AutoGenerateColumns="False"
                AllowPaging="True" OnPageIndexChanging="DBGrid_PageIndexChanging" PageSize="12">
                <EmptyDataTemplate>
                    <table style="width: 600px;" class="gv" border="1" bordercolor="white" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <th style="width: 80px;">
                                <a>大包编号</a>
                            </th>
                            <th style="width: 220px;">
                                <a>大包名称</a>
                            </th>
                            <th style="width: 100px;">
                                <a>项目</a>
                            </th>
                            <th style="width: 60px;">
                                <a>PO</a>
                            </th>
                            <th style="width: 80px;">
                                <a>价值</a>
                            </th>
                            <th style="width: 60px">
                                <a>币种</a>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <table style="width: 600px;" class="gv" border="1" bordercolor="white" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <th style="width: 80px;">
                                        <a>大包编号</a>
                                    </th>
                                    <th style="width: 220px;">
                                        <a>大包名称</a>
                                    </th>
                                    <th style="width: 100px;">
                                        <a>项目</a>
                                    </th>
                                    <th style="width: 60px;">
                                        <a>PO</a>
                                    </th>
                                    <th style="width: 80px;">
                                        <a>价值</a>
                                    </th>
                                    <th style="width: 60px">
                                        <a>币种</a>
                                    </th>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 600px;">
                                <tr>
                                    <td style="width: 80px;">
                                        <u style="color: #2131A1; cursor: pointer;" onclick="SelectDB('<%# Eval("package_no")%>','<%# Eval("package_name").ToString().Replace("\"", "&quot;")%>','<%# Eval("project_id")%>','<%# Eval("po_no") %>','<%# Eval("package_value") %>','<%# Eval("currency") %>')">
                                            <%# Eval("package_no")%>
                                        </u>
                                    </td>
                                    <td style="width: 220px;">
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 180px;
                                            font-size: 12px; margin: 0" title="<%# Eval("package_name").ToString().Replace("\"", "&quot;") %>">
                                            <%# Eval("package_name") %>
                                        </p>
                                    </td>
                                    <td style="width: 100px;">
                                        <%# Eval("project_id") %>
                                    </td>
                                    <td style="width: 60px;">
                                        <%# Eval("po_no") %>
                                    </td>
                                    <td style="width: 80px;">
                                        <%# Eval("package_value") %>
                                    </td>
                                    <td style="width: 60px">
                                        <%# Eval("currency") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        </div>
    </form>
</body>
</html>
