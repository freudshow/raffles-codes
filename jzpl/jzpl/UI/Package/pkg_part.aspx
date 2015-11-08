<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_part.aspx.cs" Inherits="Package.pkg_part" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包物资子物资添加</title>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript">
    window.onload=function(){init();}
    function init()
    {
        myf("TxtPackageName").readOnly=true;
        SetGVBoxHeight("gvbox","GVBaleItem");
    }
    function checkInput()
    {
    
        if(myf("TxtPackageNo").value==""){alert("保存失败，请录入大包编号。");return false;} 
        if(myf("TxtPartNameE").value==""){alert("保存失败，请录入零件描述（英）。");return false;}
        if(myf("DdlUnit").value=="0"){alert("保存失败，请选择零件单位。"); return false;}
        
        
        if(!myf("ChkNoPay").checked && myf("TxtPO").value==""){alert("保存失败，请录入PO号。");return false;}
        return true;
    }    

    function ShowDBPage()
    {
    var obj= ShowMD("pkgLov");
    if(typeof(obj)=="undefined")
    {
        document.getElementById("TxtPackageNo").value="";
        return;
    }
    document.getElementById("TxtPackageNo").value=obj[0];
    document.getElementById("TxtPackageName").value=obj[1];
    document.getElementById("TxtPartName").value="";
    document.getElementById("TxtPartNameE").value="";
    document.getElementById("TxtPartSpec").value="";
    document.getElementById("DdlUnit").value="0";
    document.getElementById("TxtDecNo").value="";     
    }
    function open_mod_page(pkgno,partno)
    {
        window.showModalDialog("pkg_part_mod.aspx?pkg="+pkgno+"&part="+partno+"",window,"status:no;dialogWidth:460px;dialogHeight:600px");        
    }
    function refresh()
    {
        __doPostBack('ButtQuerry','');
    } 
    function ClearForm()
    {
        myf("TxtPackageNo").value="";
        myf("TxtPackageName").value="";
        myf("TxtPartName").value="";
        myf("TxtPartNameE").value="";
        myf("TxtPartSpec").value="";
        myf("TxtDecNo").value="";
        myf("DdlUnit").value="0";   
        myf("TxtPO").value="";
        myf("TxtContractNo").value="";     
    }
    </script>

    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            大包物资子物资
            <hr />
        </div>
        <div>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 110px;">
                        大包编码：</td>
                    <td style="width: 160px;">
                        <asp:TextBox ID="TxtPackageNo" runat="server" Width="160px"></asp:TextBox></td>
                    <td style="width: 40px">
                        <img src="../../images/Search.gif" onclick="ShowDBPage();" /></td>
                    <td style="width: 80px;">
                        大包描述：</td>
                    <td style="width: 300px;">
                        <asp:TextBox ID="TxtPackageName" runat="server" Width="280px" BorderStyle="Groove"></asp:TextBox></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 110px;">
                        小件描述（英）：</td>
                    <td style="width: 590px">
                        <asp:TextBox ID="TxtPartNameE" runat="server" Width="567px"></asp:TextBox><span style="color: #ff0000">*</span></td>
                </tr>
                <tr>
                    <td style="width: 110px">
                        小件描述（中）：</td>
                    <td >
                        <asp:TextBox ID="TxtPartName" runat="server" Width="567px"></asp:TextBox></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="vertical-align: middle; font-size: 10pt"
                id="Table1">
                <tr>
                    <td style="width: 110px;">
                        单位：</td>
                    <td style="width: 160px;">
                        <asp:DropDownList ID="DdlUnit" runat="server" Width="166px">
                        </asp:DropDownList></td>
                    <td style="width: 40px">
                        <span style="color: #ff0000">*</span></td>
                    <td style="width: 80px">
                        小件规格：</td>
                    <td style="width: 300px">
                        <asp:TextBox ID="TxtPartSpec" runat="server" Width="280px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 110px">
                        关单号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtDecNo" runat="server" Width="160px"></asp:TextBox></td>
                    <td style="width: 40px">
                    </td>
                    <td style="width: 80px">
                        合同号：</td>
                    <td style="width: 300px">
                        <asp:TextBox ID="TxtContractNo" runat="server" Width="280px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 110px">
                        PO号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPO" runat="server" Width="160px"></asp:TextBox></td>
                    <td style="width: 40px">
                        <span style="color: #ff0000">*</span></td>
                    <td style="width: 80px">
                        <asp:CheckBox ID="ChkNoPay" runat="server" Text="不付款" TextAlign="Left" /></td>
                    <td style="width: 300px">
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="ButtSave" runat="server" Text="保存" CssClass="button_1" OnClick="ButtSave_Click"
                            OnClientClick="return checkInput();" />
                        <asp:Button ID="ButtQuerry" runat="server" Text="查询" CssClass="button_1" OnClick="ButtQuerry_Click"
                            UseSubmitBehavior="False" />
                        <input type="button" runat="server" value="清空" class="button_1" onclick="ClearForm();" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" runat="server" id="gvbox">
            <asp:GridView ID="GVBaleItem" runat="server" AutoGenerateColumns="False" CssClass="gv"
                Width="1270px" OnRowDeleting="GVBaleItem_RowDeleting" OnRowEditing="GVBaleItem_RowEditing"
                AllowPaging="True" OnPageIndexChanging="GVBaleItem_PageIndexChanging" PageSize="12">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="编辑">
                        <ItemTemplate>
                            <img src="../../images/eidt.gif" id="ImgEdit" onclick="open_mod_page('<%# Eval("package_no") %>','<%# Eval("part_no") %>');" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="删除">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/delete_.gif"
                                CommandName="Delete" OnClientClick="return window.confirm('确定要删除吗?')" CssClass="deleteCell" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle CssClass="deleteCellStyle" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PART_NO" HeaderText="零件号">
                        <HeaderStyle Width="160px" />
                        <ItemStyle Width="160px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderStyle-Width="180px" HeaderText="零件描述（中）">
                    <ItemTemplate>
                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 180px;
                     font-size: 12px; margin: 0" title="<%# Server.HtmlEncode(Eval("PART_NAME").ToString()) %>"><%# Server.HtmlEncode(Eval("PART_NAME").ToString())%></p>
                    </ItemTemplate>                    
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="180px"  HeaderText="零件描述（英）">
                    <ItemTemplate>
                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 180px;
                     font-size: 12px; margin: 0" title="<%# Server.HtmlEncode(Eval("PART_NAME_E").ToString()) %>"><%# Server.HtmlEncode(Eval("PART_NAME_E").ToString())%></p>
                    </ItemTemplate>                    
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="PART_SPEC" HeaderText="零件规格" HeaderStyle-Width="180px" />
                    <asp:BoundField DataField="unit" HeaderText="单位">
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="po_no" HeaderText="PO号" HeaderStyle-Width="100px" />
                    <asp:BoundField DataField="DEC_NO" HeaderText="关单号">
                        <HeaderStyle Width="160px" />
                        <ItemStyle Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CONTRACT_NO" HeaderText="合同号">
                        <HeaderStyle Width="160px" />
                        <ItemStyle Width="160px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            
        </div>
        <asp:HiddenField ID="HiddenNewId" runat="server" Visible="False" />
        <asp:HiddenField ID="HiddenResultSender" runat="server" Visible="False" />
    </form>
</body>
</html>
