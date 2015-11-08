<%@ Page Language="C#" AutoEventWireup="true" Codebehind="errPage.aspx.cs" Inherits="jzpl.errPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>error</title>

    <script type="text/javascript">
//    if(top.location!=self.location)
//    {
//        top.location=self.location;
//    }
//    function gohome()
//    {
//       top.location = "login.htm";
//    }
    function ShowInfoDiv()
    {  
        document.getElementById("div1").style.display = "block";
    }
    </script>

    <style type="text/css">
   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px 10px; padding: 10px 10px; background: #DDECEF; border: solid 1px #87AAAE">
            <h1 style="color: red">
                Error:</h1>
            <p>
                抱歉，系统出现故障。</p>
        </div>
        <div style="margin: 10px 10px; padding:0 0; border: 0">
            <div id="div_title" style="vertical-align:bottom; background: url('images/bg1.gif') repeat-x; height: 35px; margin:0 0;  border:0">
                
                <span style=" float:left; margin:10px 0 0 10px; font-weight:bold">详细信息</span><span style="float:left; margin:6px 0 0 0;"><a href="javascript:ShowInfoDiv();"><img src="images/ShowInfo.gif" style="border:0; margin:0 0;" id="img1"/></a></span>
            </div>
            <div id="div1" runat="server" style="background: #DDECEF; display:none; margin:0 0; padding:4px 2px;  border-bottom:solid 1px #87AAAE; border-left:solid 1px #87AAAE; border-right:solid 1px #87AAAE;">
            </div>
        </div>
    </form>
</body>
</html>
