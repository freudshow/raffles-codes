<%@ Page Language="C#" AutoEventWireup="true" Codebehind="package.aspx.cs" Inherits="Package.package" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包物资数据维护</title>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript">
    function show_po_lov(){
    var ret_= window.showModalDialog("pkg_po_lov.aspx",window,"status:no;dialogWidth:650px;dialogHeight:520px");
    if(typeof(ret_)!="undefined"){
    myf("DdlProject").value=ret_[0];
    myf("TxtPackageNo").value=ret_[1];
    myf("TxtPackageName").value=ret_[2];
    }
    }
    
    window.onload=function(){init();}    
    function init() { 
    SetGVBoxHeight("UserSaveData","GVBaleEdit");
    SetGVBoxHeight("GVERPPart","gvbox1");    
    }  
    
    function check_form() {
    if(myf("TxtPackageNo").value==""){
    alert("保存失败，大包编号不能为空。");
    return false;
    }
    if(myf("TxtPackageName").value==""){
    alert("保存失败，大包名称不能为空。");
    return false;
    }
    if(myf("DdlProject").value=="0"||myf("DdlProject").value==""){
    alert("保存失败，项目不能为空。");
    return false;
    }
    return true;
    }  
    </script>

    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="title">
            大包物资数据维护<hr />
        </div>
        <div runat="server" id="divEditPackage">
            <div runat="server" id="divNewPackage">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 80px">
                            大包编码：</td>
                        <td style="width: 240px">
                            <asp:TextBox ID="TxtPackageNo" runat="server" Width="230px" AutoPostBack="True" OnTextChanged="TxtPackageNo_TextChanged"></asp:TextBox></td>
                        <td style="width: 40px">
                            <img src="../../images/Search.gif" style="cursor: hand;" onclick="show_po_lov()"
                                id="IMG1" />
                        </td>
                        <td style="width: 80px">
                            大包描述：</td>
                        <td style="width: 250px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TxtPackageName" runat="server" Width="230px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TxtPackageNo"></asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                            <span style="color: #ff0000"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            项目：
                        </td>
                        <td style="width: 240px">
                            <asp:DropDownList ID="DdlProject" runat="server" Width="237px">
                            </asp:DropDownList></td>
                        <td style="width: 40px">
                        </td>
                        <td style="width: 80px">
                        </td>
                        <td style="width: 250px">
                        </td>
                    </tr>
                </table>
                <hr />
                <table class="button_box">
                    <tr>
                        <td>
                            <asp:Button ID="BtnSave" runat="server" Text="保存" UseSubmitBehavior="False" CssClass="button_1"
                                OnClick="BtnSave_Click"  />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                            <asp:LinkButton ID="LnkBtnGoToQuery" runat="server" OnClick="LnkBtnGoToQuery_Click">[查询大包物资]</asp:LinkButton></td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="divQeruyPackage">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 80px">
                            大包编码：</td>
                        <td style="width: 240px">
                            <asp:TextBox ID="TxtPackageNoQ" runat="server" Width="230px" AutoPostBack="True" OnTextChanged="TxtPackageNo_TextChanged"></asp:TextBox></td>
                        <td style="width: 40px">
                            &nbsp;<span style="color: #ff0000">%</span></td>
                        <td style="width: 80px">
                            大包描述：</td>
                        <td style="width: 250px">
                            &nbsp;<asp:TextBox ID="TxtPackageNameQ" runat="server" Width="230px"></asp:TextBox><span
                                style="color: #ff0000"></span><span style="color: #ff0000"></span></td>
                        <td style="width: 30px">
                            <span style="color: #ff0000">%</span></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            项目：
                        </td>
                        <td style="width: 240px">
                            <asp:DropDownList ID="DdlProjectQ" runat="server" Width="237px">
                            </asp:DropDownList></td>
                        <td style="width: 40px">
                        </td>
                        <td style="width: 80px">
                        </td>
                        <td style="width: 250px">
                        </td>
                        <td style="width: 30px">
                        </td>
                    </tr>
                </table>
                <hr />
                <table class="button_box">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="button_1" Text="查询" OnClick="BtnResult_Click"
                                 />
                            &nbsp;
                            <asp:Button ID="Button2" runat="server" CssClass="button_1" Text="返回" OnClick="BtnBack_Click" /></td>
                    </tr>
                </table>
            </div>
            <div style="overflow-x: auto; overflow-y: none; width: 100%" id="UserSaveData" runat="server">
                <asp:GridView ID="GVBaleEdit" runat="server" AutoGenerateColumns="False" CssClass="gv"
                    Width="770px" OnRowDeleting="GVBaleEdit_RowDeleting" AllowPaging="True" OnPageIndexChanging="GVBaleEdit_PageIndexChanging"
                    PageSize="15">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="编辑" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden">
                            <ItemTemplate>
                                <img src="../../images/eidt.gif" onclick="open_mod_page('<%# Eval("package_no") %>');" />
                            </ItemTemplate>
                            <HeaderStyle Width="50px" HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/delete_.gif"
                                    CommandName="Delete" OnClientClick="return window.confirm('确定要删除吗?')" CssClass="img_button" />
                            </ItemTemplate>
                            <HeaderStyle Width="50px" HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="package_no" HeaderText="大包编号">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="大包描述">
                            <ItemTemplate>
                                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;"
                                    title="<%# Eval("package_name").ToString().Replace("\"", "&quot;")%>">
                                    <%# Eval("package_name")%>
                                </p>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="project_id" HeaderText="项目编号">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="项目描述">
                            <ItemTemplate>
                                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                    title="<%# Eval("project_name") %>">
                                    <%# Eval("project_name")%>
                                </p>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" />
                        </asp:TemplateField>                        
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
                <br />
            </div>
        </div>
    </form>
</body>
</html>
