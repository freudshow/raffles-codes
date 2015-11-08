<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_lov.aspx.cs" Inherits="jzpl.UI.Package.pkg_arrival_db" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择大包信息</title>
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
        function SelectDB(dbid,dbmc)
        {
            var ddl = document.getElementById("DDLProject")  
            var index = ddl.selectedIndex;  
            var Value = ddl.options[index].value;  
            var Text = ddl.options[index].text;
            
            var return_array = new Array();
            return_array[0]=dbid;
            return_array[1]=dbmc;
            return_array[2]=Value;
            return_array[3]=Text;
            window.returnValue=return_array;
            window.opner=null;
            window.close();
        }
        function CheckForm()
        {
            if(myf("DDLProject").value=="0"){ alert("请选择项目后再进行查询。"); return false;}
            return true;
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">    
        <div style="margin: 10px">
        <div id="title">选择大包信息<hr /></div>
            <table>
                <tr>
                    <td style="width: 120px">
                        大包编号：
                    </td>
                    <td style="width: 430px">
                        <asp:TextBox ID="TxtDBbh" runat="server" Width="420px"></asp:TextBox>
                    </td>
                    <td style="width: 20px">
                        <span style="color: #ff0000">%</span></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        大包描述：</td>
                    <td style="width: 430px">
                        <asp:TextBox ID="TxtDBmc" runat="server" Width="420px"></asp:TextBox></td>
                    <td>
                        <span style="color: #ff0000">%</span></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        项目：</td>
                    <td style="width: 430px">
                        <asp:DropDownList ID="DDLProject" runat="server" Width="426px">
                        </asp:DropDownList></td>
                    <td>
                        <span style="color: #ff0000"></span></td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnResult" runat="server" CssClass="button_1" Text="查看" OnClick="BtnResult_Click" OnClientClick="return CheckForm();"/></td>
                </tr>
            </table>
            <div style="width: 620px; height: 350px; overflow: scroll; border: 1px solid;" >
        
        <asp:GridView ID="DBGrid" runat="server" Width="600px" CssClass="gv" AutoGenerateColumns="False">    
        <EmptyDataTemplate>
        <table style="width: 600px;" class="gv" border="1" bordercolor="white" cellpadding="0"
                            cellspacing="0">
            <tr>
                <th style="width: 150px;">
                    <a>项目</a>
                </th>
                
                <th style="width: 150px;">
                    <a>大包编号</a>
                </th>
                <th style="width: 300px;">
                    <a>大包名称</a>
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
                                <th style="width: 150px;">
                                    <a>项目</a>
                                </th>
                                
                                <th style="width: 150px;">
                                    <a>大包编号</a>
                                </th>
                                <th style="width: 300px;">
                                    <a>大包名称</a>
                                </th>                               
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 600px;" class="gv">
                            <tr>
                                <td style="width: 150px;">
                                    <%# Eval("project_id") %>
                                </td>
                                
                                <td style="width: 150px;">
                                    <u style="color: #2131A1; cursor: pointer;" onclick="SelectDB('<%# Eval("package_no")%>','<%# Eval("package_name").ToString().Replace("\"", "&quot;") %>')">
                                        <%# Eval("package_no")%>
                                    </u>
                                </td>
                                <td style="width: 300px;">
                                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;
                                        font-size: 12px;" title="<%# Server.HtmlEncode(Eval("package_name").ToString()) %>">
                                        <%# Server.HtmlEncode(Eval("package_name").ToString()) %>
                                    </p>
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
