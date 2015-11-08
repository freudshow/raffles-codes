<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_mod.aspx.cs" Inherits="jzpl.UI.Package.pkg_mod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包物资数据维护</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript">
    function checkInput()
    {
        var partNo = document.getElementById("TxtPackageNo").value;              
        var proj = document.getElementById("DdlProject").value;
        var partname =document.getElementById("TxtPackageName").value;
        var partpo = document.getElementById("TxtPO").value;
        var partcurr = document.getElementById("DdlCurrency").value;
        var partvalue = document.getElementById("TxtAmount").value;
        
        if(partNo==""||partNo==null)        
            {
                alert("请输入大包编码！");
                return false;
            }
            if(partname=="" || partname ==null)
            {
            alert("请输入大包物资名称！");
                return false;
            }
            if( proj=="0" )
            {
            alert("请选择项目！");
            return false;
            }
            if(partpo=="" || partpo ==null)
            {
            alert("请输入大包物资的PO号！");
                return false;
            }
            if(partvalue=="" || partvalue ==null)
            {
            alert("请输入大包物资的金额！");
                return false;
            } 
            if( partcurr=="0" )
           {
            alert("请选择币种！");
            return false;
            }
        return true;
    }   
   

    
function check(obj)
{
   leastamount = obj.value;
   if(isInteger(leastamount))
    {
        alert("必须是大于或等于零的数字！");
        obj.focus();
        return false;
    }
  return true;
}
function isInteger(id)
   {
      /*var patrn=/^\d+$/;*/
      var patrn=/^([0-9][0-9]*(\.\d+)?)|(0\.\d+)/;
      num = id;
      if (!patrn.exec(num))
      {
        return true;
      }
      else
      {
        return false;
      }
   }    
  
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px">
        <div id="title">大包物资数据维护<hr /></div>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        大包编码：</td>
                    <td style="width: 200px">
                        <asp:TextBox ID="TxtPackageNo" runat="server" Width="200px" ReadOnly="True" BorderStyle="Groove"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        大包名称：</td>
                    <td style="width: 200px">
                        <asp:TextBox ID="TxtPackageName" runat="server" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        项目：</td>
                    <td style="width: 200px">
                        <asp:DropDownList ID="DdlProject" runat="server" Width="206px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        PO：</td>
                    <td>
                        <asp:TextBox ID="TxtPO" runat="server" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        大包价值：</td>
                    <td>
                        <asp:TextBox ID="TxtAmount" runat="server" Width="200px" onblur="return check(this);"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        币种：</td>
                    <td>
                        <asp:DropDownList ID="DdlCurrency" runat="server" Width="206px">
                        </asp:DropDownList></td>
                </tr>
            </table>
            <hr />
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                    </td>
                    <td style="text-align: right; width: 200px">
                        <asp:Button ID="ButtSave" runat="server" Text="保存" CssClass="button_1" OnClientClick="return checkInput();"
                            OnClick="ButtSave_Click" />&nbsp;&nbsp;<input type="button" value="关闭" class="button_1"
                                onclick="javascript:window.close();" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
