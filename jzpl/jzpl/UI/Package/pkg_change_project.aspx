<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_change_project.aspx.cs"
    Inherits="jzpl.UI.Package.pkg_change_project" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目借大包零件</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../script/common.js"></script>
    <script type="text/javascript" language="javascript">
     
    window.onload=function(){init();}
    function init(){
    SetGVBoxHeight("divBox","PartGrid");
//    $("TxtXJbh").readOnly=true;   
//    $("TxtXJmcen").readOnly=true;
//    $("TxtXJgg").readOnly=true;    
//    $("TxtXJgdh").readOnly=true;
//    $("TxtXJhth").readOnly=true;
    }
    
    function ShowDBPage()
    {
        var obj= ShowMD("pkgLov");
        if(typeof(obj)=="undefined")
        {           
            return;
        }
        myf("TxtDBbh").value=obj[0];
        myf("TxtDBbh").readOnly=true;
        myf("TxtPartProject").value=obj[3];
        myf("TxtPartProject").readOnly=true;
        myf("TxtXJbh").value="";       
        myf("TxtXJmcen").value="";
        myf("TxtXJgg").value="";
        myf("TxtXJgdh").value="";
        myf("TxtXJhth").value="";
      
    }
        function ShowXJPage()
        {
            var dbbhValue=myf("TxtDBbh").value;
            if(dbbhValue=="" || dbbhValue==null)
            {
                alert('请首先选择大包信息！');
                return;
            }
            var obj= ShowMD("pkgPartLov",dbbhValue);
            if(typeof(obj)=="undefined")
            {               
                return;
            }
            myf("TxtXJbh").value=obj[0];
            myf("TxtXJmcen").value=obj[2];
            myf("TxtXJgg").value=obj[3];
            myf("TxtXJgdh").value=obj[5];
            myf("TxtXJhth").value=obj[6];  
            document.getElementById("btnViewdata").click();
        }
        function checkIsNull(inputText)
        {        
            if(inputText.replace(/\s/g,"")=="")
            {             
                 return true;	    
            } 
            return false;    
        }
        function checkViewData()
        {
            DBBHValue=document.getElementById("TxtDBbh").value;
            if(checkIsNull(DBBHValue))
            {
                alert('请输入大包编号！');
                return false;
            }
            XJBHValue=document.getElementById("TxtXJbh").value;
            if(checkIsNull(XJBHValue))
            {
                alert('请输入小件编号！');
                return false;
            }
            return true;
        }
        function CheckTextValue()
        {
            var OkQtyValue=0;
            var NoCheckQtyValue=0;
            var JYslvalue=0;
            for(i=0;i<grd_TxtJYsl.length;i++)
            {
                OkQtyValue=document.getElementById(grd_LbOkQty[i]).innerText;
                NoCheckQtyValue=document.getElementById(grd_LbNoCheckQty[i]).innerText;
                JYslvalue=document.getElementById(grd_TxtJYsl[i]).value;
                if(Number(JYslvalue)!=0)
                {
                    if(Number(NoCheckQtyValue)==0)
                    {
                        if(Number(JYslvalue)>Number(OkQtyValue))
                        {
                            alert("第"+ [parseInt(i)+1]+ "行的借用数量不能大于库存数量！");
                            JYslvalue.focus();
                            return false;
                        }
                    }
                    else
                    {
                        if(Number(JYslvalue)>Number(NoCheckQtyValue))
                        {
                            alert("第"+ [parseInt(i)+1]+ "行的借用数量不能大于免检库存数量！");
                            JYslvalue.focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            项目借大包零件<hr />
        </div>
        <table>
            <tr>
                <td style="width: 80px">
                    大包编号：</td>
                <td style="width: 130px">
                    <input id="TxtDBbh" runat="server" style="width: 120px; border-top-style: groove;
                        border-right-style: groove; border-left-style: groove; border-bottom-style: groove"
                        type="text" /></td>
                <td style="width: 40px">
                    <img src="../../images/Search.gif" onclick="ShowDBPage()" /></td>
                <td style="width: 80px">
                    小件名称：</td>
                <td style="width: 260px">
                    <asp:TextBox ID="TxtXJmcen" runat="server" BorderStyle="Groove" Width="220px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 80px">
                    小件编号：</td>
                <td style="width: 130px">
                    <input id="TxtXJbh" runat="server" style="width: 120px; border-top-style: groove;
                        border-right-style: groove; border-left-style: groove; border-bottom-style: groove"
                        type="text" /></td>
                <td style="width: 40px">
                    <img src="../../images/Search.gif" onclick="ShowXJPage()" /></td>
                <td style="width: 80px">
                    规格：</td>
                <td style="width: 260px">
                    <asp:TextBox ID="TxtXJgg" runat="server" BorderStyle="Groove" Width="220px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 80px">
                    合同号：</td>
                <td style="width: 130px">
                    <asp:TextBox ID="TxtXJhth" runat="server" BorderStyle="Groove" Width="120px"></asp:TextBox></td>
                <td style="width: 40px">
                </td>
                <td style="width: 80px">
                    关单号：</td>
                <td style="width: 260px">
                    <asp:TextBox ID="TxtXJgdh" runat="server" BorderStyle="Groove" Width="220px"></asp:TextBox></td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 80px">
                    项目：</td>
                <td style="width: 370px">
                    <asp:TextBox ID="TxtPartProject" runat="server" BorderStyle="Groove" Width="362px" />
                </td>
            </tr>
        </table>
        <hr />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnViewdata" runat="server" CssClass="button_1" Text="查看" OnClientClick="return checkViewData();"
                        OnClick="btnViewdata_Click" /></td>
                <td>
                </td>
            </tr>
        </table>
        
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="divBox" runat="server">
            <asp:GridView ID="PartGrid" runat="server" AutoGenerateColumns="False" Width="1180px"
                CssClass="gv" OnPreRender="PartGrid_PreRender">
                <Columns>
                    <asp:BoundField DataField="package_no" HeaderText="大包编号" HeaderStyle-Width="100px" />
                    <asp:BoundField DataField="part_no" HeaderText="小件编号" HeaderStyle-Width="160px" />
                    <asp:TemplateField HeaderText="小件名称">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px" title=' <%# Server.HtmlEncode(Eval("part_name_e").ToString())%>'>
                                <%# Server.HtmlEncode(Eval("part_name_e").ToString())%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="unit" HeaderText="小件单位">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="小件规格">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 160px" title=' <%# Server.HtmlEncode(Eval("spec").ToString())%>'>
                                <%# Server.HtmlEncode(Eval("spec").ToString())%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />                       
                    </asp:TemplateField>
                    <asp:BoundField DataField="area" HeaderText="存放区域" HeaderStyle-Width="100px" />
                    <asp:BoundField DataField="location" HeaderText="存放位置" HeaderStyle-Width="100px" />
                    <asp:TemplateField HeaderText="合格数量" HeaderStyle-Width="100px">
                    <ItemTemplate>
                    <asp:Label ID="LbOKQty" runat="server" Text='<%# Eval("part_ok_qty") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="免检数量" HeaderStyle-Width="100px">
                    <ItemTemplate>
                    <asp:Label ID="LbNoCheckQty" runat="server" Text='<%# Eval("no_check_qty") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>                 
                   
                    <asp:TemplateField HeaderText="借用数量" HeaderStyle-Width="100px" >
                    <ItemTemplate>
                     <asp:TextBox ID="TxtJYsl"  Width="80px" runat="server"></asp:TextBox>
                    </ItemTemplate>
                    </asp:TemplateField>
                        
                </Columns>
                <EmptyDataTemplate>
                <table class="gv">
                <tr>
                <th style="width:100px">大包编号</th>
                <th style="width:160px">小件编号</th>
                <th style="width:200px">小件名称</th>
                <th style="width:60px">小件单位</th>
                <th style="width:160px">小件规格</th>
                <th style="width:100px">存放区域</th>
                <th style="width:100px">存放位置</th>
                <th style="width:100px">合格数量</th>
                <th style="width:100px">免检数量</th>
                <th style="width:100px">借用数量</th>
                </tr>
                <tr><td colspan="10"></td></tr>
                </table>
                
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <table>
            <tr>
                <td>
                    选择借用项目：</td>
                <td>
                    <asp:DropDownList ID="DDLProject" runat="server" Width="369px">
                    </asp:DropDownList>
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button_1" Text="提交" OnClientClick="return CheckTextValue();"
                        OnClick="btnSubmit_Click"/></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
