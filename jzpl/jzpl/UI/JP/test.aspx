<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="JP.test" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>My table</title>
<script type="text/javascript" src="../../UI/script/common.js"></script>
<script type="text/javascript">
function setHeight()
{
//document.getElementById("div1").style.height = 
}

</script>

<style type="text/css">

</style>
</head>
<body  onload="setHeight()" >
<form runat="server" id="form_test">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:Button ID="sendMail" runat="server" Text="Send Mail" OnClick="sendMail_Click" />
    <asp:TextBox ID="MailFrom" runat="server"></asp:TextBox>
    <asp:Button ID="BtnStopTimer" runat="server" OnClick="BtnStopTimer_Click" Text="Stop" />&nbsp;
    <asp:Button ID="BtnStart" runat="server" OnClick="BtnStart_Click" Text="Start" />
    <asp:Button ID="temp" runat="server" OnClick="temp_Click" Text="temp" />
    <br />
    <asp:Button ID="test1" runat="server" Text="test oracle err" OnClick="test1_Click" />
<select name="test" id="test">

</select>
    
   

</form>
</body>
</html>


