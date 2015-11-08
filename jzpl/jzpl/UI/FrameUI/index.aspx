<%@ Page Language="C#" AutoEventWireup="true" Codebehind="index.aspx.cs" Inherits="jzpl.UI.FrameUI.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Material Distribution System</title>
    <meta http-equiv="Expires" content="0" />    
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../UI/CSS/navi.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../../UI/script/navi.js"></script>

    <script type="text/javascript">
    function set_currsite(obj)
    {
        currsite_ = document.getElementById("curr_site");
        currsite_.innerHTML = "当前位置 >> <b>" + obj.innerText +"</b>";
    }
    
    function reinitIframe(){
    var iframe = document.getElementById("jzpl_page");
    try{
            //        var bHeight = iframe.contentWindow.document.body.scrollHeight;
            //        var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
            //        var height = Math.max(bHeight, dHeight);
           // alert(document.body.clientHeight);
          
          iframe.height =  document.documentElement.clientHeight - 70;   
          iframe.width = document.documentElement.clientWidth;
    }catch (ex){}
    }
    window.setInterval("reinitIframe()", 200);
//    window.onload=function(){
//    var iframe = document.getElementById("jzpl_page");
//    iframe.height =  document.documentElement.clientHeight - 76;
//    iframe.width = document.documentElement.clientWidth;
//    }
    </script>

    <style type="text/css">
    html body{height:100%;margin:0;padding:0;border:0}
    
    #png{
    azimuth: expression(
    this.pngSet?this.pngSet=true:(this.nodeName == "IMG" && this.src.toLowerCase().indexOf('.png')>-1?(this.runtimeStyle.backgroundImage = "none",
    this.runtimeStyle.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + this.src + "', sizingMethod='image')",
    this.src = "transparent.gif"):(this.origBg = this.origBg? this.origBg :this.currentStyle.backgroundImage.toString().replace('url("','').replace('")',''),
    this.runtimeStyle.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + this.origBg + "', sizingMethod='crop')",
    this.runtimeStyle.backgroundImage = "none")),this.pngSet=true);
    }
    
    
    </style>
</head>
<body style=" margin: 0; padding: 0; overflow: hidden;">
    <form id="main_form" runat="server">
    
        <table style="font-size: 12pt; color: White; background: url('../../images/title_bg.jpg');
            width: 100%; font-family: Arial; padding: 0px; margin: 0px; border: 0px;" >
            <tr style="height: 40px">
                <td style="width: 169px;">
                    <img src="../../images/logo.png" id="png" /></td>
                <td style="text-align: left; padding: 3px 20px 6px 40px; height: 40px; vertical-align: bottom">
                    <span id="logo"><b><span style="color: activecaption; font-size: 1.3em;">M</span>aterial
                        <span style="color: activecaption; font-size: 1.2em;">D</span>istribution <span style="color: activecaption;
                            font-size: 1.2em;">S</span>ystem</b></span></td>
                <td style="text-align: right; font-size: 10pt; padding-bottom: 10px; vertical-align: bottom">
                    <asp:Label ID="LblServer" runat="server"></asp:Label><span style="padding: 0 8px">|</span><asp:Label
                        ID="UserID" runat="server"></asp:Label>
                    <span style="padding: 0 8px">|</span> <a href="login.htm" onclick="javascript:location.replace(this.href);event.returnValue=false;">
                        <span style="color: White; font-size: 10pt;"><u>注销</u></span></a></td>
            </tr>
        </table>
        <table style="width: 100%; background: url('../../images/title_2_bg_1.jpg') repeat-x;
            margin: 0;" cellpadding="0" cellspacing="0" >
            <tr>
                <td style="height:30px">
                    <asp:Panel ID="Panel1" runat="server">
                    </asp:Panel>
                </td>
                <td style="text-align: right; font-size: 10pt;  font-family: Arial 宋体"
                    id="curr_site">
                </td>
            </tr>
        </table>
        <iframe name="jzpl_page" id="jzpl_page"  frameborder="0" style="z-index:-1;">
        </iframe>
    </form>
</body>
</html>
