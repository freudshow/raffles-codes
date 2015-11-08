<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_arrival_mod.aspx.cs"
    Inherits="jzpl.UI.Package.pkg_arrival_mod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包到货登记修改</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px">
            <div id="title">
                大包到货登记修改<hr />
            </div>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="font-weight:bold">
                       大包编码：<asp:Label ID="LblPkgNo" runat="server"></asp:Label>&nbsp;&nbsp;
                    零件编码：<asp:Label ID="LblPartNo" runat="server"></asp:Label></td>
                    
                </tr>
                <tr>
                    <td style="font-weight: bold">
                        大包描述：<asp:Label
                           ID="LblPkgName" runat="server" ></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">
                        零件描述（英）：<asp:Label ID="LblPartNameE" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">
                        零件描述（中）：<asp:Label ID="LblPartNameCh" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td  style="font-weight:bold">
                    项目：<asp:Label ID="LblProject" runat="server"></asp:Label>&nbsp; 规格：<asp:Label ID="LblSpec"
                        runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">
                        单位：<asp:Label ID="LblUnit" runat="server"></asp:Label>&nbsp; 合同号：<asp:Label ID="LblHt"
                            runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">
                        关单号：<asp:Label ID="LblGd" runat="server"></asp:Label>&nbsp; PO号：<asp:Label ID="LblPo"
                            runat="server"></asp:Label></td>
                </tr>
            </table>
            <hr />
            
        </div>
    </form>
</body>
</html>
