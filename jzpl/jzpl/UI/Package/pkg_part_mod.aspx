<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_part_mod.aspx.cs" Inherits="jzpl.UI.Package.pkg_part_mod" %>
<%@ OutputCache Duration="1" VaryByParam="None"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>大包物资小件修改</title>

    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript" src="../../UI/script/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
    
    function checkInput()
    {        
        if(myf("TxtPartNameE").value==""){alert("保存失败，请录入零件描述（英）。");return false;}
        if(myf("DdlUnit").value=="0"){alert("保存失败，请选择零件单位。"); return false;} 
        if(!myf("ChkPayFlag").checked && myf("TxtPO").value==""){alert("保存失败，请录入PO号。");return false;}
        return true;
    } 
    
    //$(document).ready(function(){alert(1);});
     
    </script>

    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px">
        <div id="title">大包物资小件修改<hr /></div>
            <table cellpadding="0" cellspacing="5">
                <tr>
                    <td style="width: 99px">
                        <strong>
                        小件编码：</strong></td>
                    <td style="width: 353px">
                        <asp:Label ID="LblPartNo" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 99px;">
                        <strong>
                        大包编码：</strong></td>
                    <td style="width: 353px;">
                        <asp:Label ID="LblPackageNo" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 99px">
                        <strong>
                        大包描述：</strong></td>
                    <td style="width: 353px">
                        <asp:Label ID="LblPackageName" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
            </table>
            <hr />
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 130px;">
                        小件描述（英）：</td>
                    <td style="width: 307px">
                        <asp:TextBox ID="TxtPartNameE" runat="server" Width="300px"></asp:TextBox></td>
                    <td style="width: 20px">
                        <span style="color: #ff0000">*</span></td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        小件描述（中）：</td>
                    <td style="width: 307px">
                        <asp:TextBox ID="TxtPartName" runat="server" Width="300px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                </tr>
            <%--</table>
            <table cellpadding="0" cellspacing="0" style="vertical-align: middle; font-size: 10pt"
                id="Table1">--%>
                <tr>
                    <td style="width: 130px;">
                        单位：</td>
                    <td style="width: 160px;">
                        <asp:DropDownList ID="DdlUnit" runat="server" Width="166px">
                        </asp:DropDownList></td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        小件规格：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPartSpec" runat="server" Width="300px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        关单号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtDecNo" runat="server" Width="300px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        合同号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtContractNo" runat="server" Width="300px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        PO号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPO" runat="server" Width="300px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        不付款：</td>
                    <td style="width: 160px">
                        <asp:CheckBox ID="ChkPayFlag" runat="server" /></td>
                    <td style="width: 20px">
                    </td>
                </tr>                
            </table>
            <hr />
            <table>
                <%--<tr>
                    <td style="text-align: right; width: 425px;">
                        <div style="text-align:left"><asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                            &nbsp;
                            <asp:LinkButton ID="LBtnUpload" runat="server" Text="上传照片" OnClick="LBtnUpload_Click" /> </div>
                        <div style="margin-top:4px;"><asp:Image ID="Image1" runat="server" Height="200px" ImageAlign="Left" Width="200px" /></div>                        
                    </td>
                </tr>--%>
                <tr>
                    <td style="text-align: right; width: 425px;">
                    <asp:Button ID="ButtSave" runat="server" Text="保存" CssClass="button_1" OnClick="ButtSave_Click" OnClientClick="return checkInput();" />
                        <input type="button" onclick="window.close();" value="关闭" class="button_1" /></td></tr>
            </table>
            <br />
            <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>--%>
        <asp:HiddenField ID="HiddenObjId" runat="server" />
        &nbsp;
        <asp:HiddenField ID="HiddenRowversion" runat="server" />
    </form>
</body>
</html>
