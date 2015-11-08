<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_loc_move.aspx.cs" Inherits="Package.pkg_loc_move" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包移库</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #TxtLocationInput{}
    </style>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript" src="../../UI/script/wzxqjh.js"></script>

    <script language="javascript">
    window.onload=function(){init();}
    function init(){
    SetGVBoxHeight("divBox","GVData")
    }
         function showTB()
        {
             document.getElementById("tb").style.display="none";
        }
       var ClickNum=0;//判断同一checkbox连续点击次数
       var PreCheckboxID="";//记录点击checkbox的ID      
        function ChangeGet(SelectCheckBox)
            {   
           ///判断是否连击同一个checkbox
          if(PreCheckboxID==SelectCheckBox.id)
            {
                ClickNum = ClickNum + 1;
            }
            else
            {
                PreCheckboxID = SelectCheckBox.id;
                ClickNum = 0;
            }          
            //找到页面所有 input
              var objs = document.getElementsByTagName("input");
              for(var i=0; i<objs.length; i++) 
                {
            //找到input中的checkbox
                    if(objs[i].type.toLowerCase() == "checkbox" )
            //所有checkbox为false
                    objs[i].checked = false;
                }
            //找到选中checkbox
               var SelectCheckBoxID=SelectCheckBox.id;
               if(ClickNum%2!=1)
                    {
                              //选中checkbox为true
                               document.getElementById(SelectCheckBoxID).checked = true;
                                //document.getElementById("tb").style.display=""; 
                    }
                    else
                    {
                         document.getElementById(SelectCheckBoxID).checked = false;
                               // document.getElementById("tb").style.display="none"; 
                    }   
            }
    function checkBoxHaveSelected(){
    var objs=document.getElementById("GVData").getElementsByTagName("input");
    for(var i=0;i<objs.length;i++){
    if(objs[i].type.toLowerCase()=="checkbox"&&objs[i].checked==true) return true;}
    return false;    
    }
    
    function checkInput(){
    if(!checkBoxHaveSelected()){ alert("保存失败，未选择源库位！"); return false;}
    if(checkTextIsNull("TextBox1")&&checkTextIsNull("TextBox2")&&checkTextIsNull("TextBox3")){ alert("保存失败，未填入转移数量！"); return false;}
    if(!checkTextIsNull("TextBox1")&&!checkIsNumber("TextBox1")){ alert("保存失败，输入非数值！"); return false;}
    if(!checkTextIsNull("TextBox2")&&!checkIsNumber("TextBox2")){ alert("保存失败，输入非数值！");return false;}
    if(!checkTextIsNull("TextBox3")&&!checkIsNumber("TextBox3")){ alert("保存失败，输入非数值！");return false;}
    return true;
    }
    
    function ShowLocPage()
    {       
        var paras = new Array(myf("LblCompany").innerText,myf("ddl_area").value);
        var obj= ShowMD("pkgLocLov",paras);
        if(typeof(obj)=="undefined")
        {            
            return;
        }
        
        //$("DdlCompany").value=obj[0];
        myf("ddl_area").value=obj[1];
        //$("TxtLocationInput").innerText=obj[2];  
        myf("HiddenLocId").value=obj[3];  
        myf("TxtLocationInput").value=obj[2]; 
        //alert( $("TxtLocationInput").innerText +'  '+ $("HiddenLocId").value);
        __doPostBack('LinkButton1','');    
        myf("TxtLocationInput").value=obj[2]; 
    }
    
//    var xmlHttp = new ActiveXObject("Msxml2.XMLHTTP"); 
//    
//    function CallServerForGetPartNumInLocation(loc)
//    {   var part_no;
//        var arrival_id;
//        var loc_id;
//        
//        loc_id=loc;
//        part_no = 
//        var url = "jp_ajaxHandler.aspx?mode=getpnum&loc="+escape(loc_id)+"&pno="+escape(part_no)+"&arrid="+escape(arrival_id);
//        xmlHttp.open("GET",url,true);
//        xmlHttp.onreadystatechange = setPartNum;
//        xmlHttp.send(null);
//    }
//    
//    function setPartNum()
//    {
//        if (xmlHttp.readyState == 4) {
//            var partName = xmlHttp.responseText;
//            if(partName=="")
//            {
//                alert("零件不存在！");
//            }
//            else
//            {
//                $("TxtPartName").value = partName;
//            } 
//        }  
//    }
    
    </script>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="title">大包移库<hr /></div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Panel ID="PnlTop" runat="server" Width="100%">
                <table cellpadding="0" cellspacing="0" style="margin: 10px 0 0 4px; padding: 0; font-size: 10pt;
                    font-family: Arial 宋体 @宋体">
                    <tr>
                        <td style="width: 80px">
                            到货日期：</td>
                        <td style="width: 150px">
                            <asp:TextBox ID="TxtArrDate" runat="server" Width="140px" onfocus="WdatePicker();"></asp:TextBox></td>
                        <td style="width: 40px">
                        </td>
                        <td style="width: 80px">
                            PO：</td>
                        <td style="width: 150px">
                            <asp:TextBox ID="TxtPO" runat="server" Width="140px"></asp:TextBox></td>
                        <td style="width: 40px; text-align: left">
                        </td>
                        <td style="width: 80px">
                            关单号：</td>
                        <td style="width: 250px">
                            <asp:TextBox ID="TxtDec" runat="server" Width="140px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                        <td style="width: 40px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            大包编码：</td>
                        <td style="width: 150px">
                            <asp:TextBox ID="TxtPackageNo" runat="server" Width="140px"></asp:TextBox></td>
                        <td style="width: 40px">
                        </td>
                        <td style="width: 80px">
                            大包名称：</td>
                        <td style="width: 150px">
                            <asp:TextBox ID="TxtPkgName" runat="server" Width="140px"></asp:TextBox></td>
                        <td style="width: 40px; text-align: left">
                            <span style="color: #ff0000">%</span></td>
                        <td style="width: 80px">
                            项目：</td>
                        <td style="width: 250px">
                            <asp:DropDownList ID="DdlProject" Width="240px" runat="server">
                            </asp:DropDownList></td>
                        <td style="width: 40px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            小件名称：</td>
                        <td>
                            <asp:TextBox ID="TxtPart" runat="server" Width="140px"></asp:TextBox></td>
                        <td>
                            <span style="color: #ff0000">%</span></td>
                        <td>
                            小件规格：</td>
                        <td>
                            <asp:TextBox ID="TxtSpec" runat="server" Width="140px"></asp:TextBox></td>
                        <td style="width: 40px; text-align: left">
                            <span style="color: #ff0000">%</span></td>
                        <td style="width: 80px">
                        </td>
                        <td style="width: 150px">
                        </td>
                        <td style="width: 40px; text-align: left">
                            <span style="color: #ff0000"></span>
                        </td>
                    </tr>
                </table>
                <hr />
                <table cellspacing="0" cellpadding="0" style="padding: 0; margin: 0 0 4px 0" width="100%">
                    <tr>
                        <td style="width: 200px">
                            <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" Height="21px"
                                Width="60px" Font-Size="10pt" />&nbsp;
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div style="overflow-x: auto; overflow-y: none; width: 100%;" id="divBox" runat="server">
                    <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" Width="1660px"
                        AllowPaging="True" OnPageIndexChanging="GVData_PageIndexChanging" CssClass="gv"
                        OnRowDataBound="GVData_RowDataBound" OnRowCommand="GVData_RowCommand">
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField HeaderText="大包编号" DataField="package_no">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="大包描述">
                                <ItemTemplate>
                                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px;
                                        margin: 0px" title="<%# Server.HtmlEncode(Eval("package_name").ToString())%>">
                                        <%# Server.HtmlEncode(Eval("package_name").ToString())%>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="零件编号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkBtnTrans" runat="server" CommandName="trans" Text='<%# Eval("part_no") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="零件描述">
                                <ItemTemplate>
                                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px;
                                        margin: 0px" title="<%# Server.HtmlEncode(Eval("part_name_e").ToString()) %>">
                                        <%# Server.HtmlEncode(Eval("part_name_e").ToString())%>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="区域" DataField="area">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="位置" DataField="location">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="合格数量" DataField="part_ok_qty">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="不合格数量" DataField="part_bad_qty">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="免检数量" DataField="no_check_qty">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="公司" DataField="company_id">
                                <HeaderStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="项目" DataField="project_id">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="PO号" DataField="po_no">
                                <HeaderStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="单位" DataField="unit">
                                <HeaderStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="规格" DataField="spec">
                                <HeaderStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="到达日期" DataField="arrived_date" DataFormatString="{0:d}"
                                HtmlEncode="False">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="arrival_id">
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="location_id">
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="part_no">
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel1" runat="server" Width="100%">
                <div style="border: solid 1px; width: 700px; padding: 10px 10px 0 10px; margin: 10px 0 10px 0;
                    font-size: 10pt">
                    转移零件信息：
                    <hr />
                    <table>
                        <tr>
                            <td>
                                大包编码：<asp:Label ID="LblPackageNo" runat="server" Font-Bold="True"></asp:Label>&nbsp; &nbsp;大包描述：<asp:Label
                                    ID="LblPackageName" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                零件编码：<asp:Label ID="LblPartNo" runat="server" Font-Bold="True"></asp:Label>&nbsp; &nbsp;零件描述：<asp:Label
                                    ID="LblPartName" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                零件单位：<asp:Label ID="LblUnit" runat="server" Font-Bold="True"></asp:Label>&nbsp; &nbsp;零件规格：<asp:Label
                                    ID="LblPartSpec" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                公司：<asp:Label ID="LblCompany" runat="server" Font-Bold="True"></asp:Label>&nbsp; &nbsp;区域：<asp:Label
                                    ID="LblArea" runat="server" Font-Bold="True"></asp:Label>
                                &nbsp; &nbsp;位置编号：<asp:Label ID="LblLocationId" runat="server" Font-Bold="True"></asp:Label>
                                &nbsp; &nbsp;位置：<asp:Label ID="LblLocation" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                到货ID：<asp:Label ID="LblArrivedId" runat="server" Font-Bold="True"></asp:Label>&nbsp; &nbsp;到货日期：<asp:Label
                                    ID="LblArrivedDate" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                合格数量：<asp:Label ID="LblOkQty" runat="server" Font-Bold="True"></asp:Label>&nbsp; &nbsp;不合格数量：<asp:Label
                                    ID="LblBadQty" runat="server" Font-Bold="True"></asp:Label>
                                &nbsp; &nbsp;免检数量：<asp:Label ID="LblNocheckQty" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="border: solid 1px; width: 700px; padding: 10px 10px 0 10px; margin: 10px 0 10px 0;
                    font-size: 10pt">
                    选择目标位置：&nbsp;
                                
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 14px">
                            </td>
                            <td style="width: 80px">
                                区域:</td>
                            <td style="width: 300px">
                                
                                    <asp:DropDownList ID="ddl_area" runat="server" Width="291px" AutoPostBack="false">
                                    </asp:DropDownList></td>
                            <td style="width: 40px">
                            </td>
                            <td width="80">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 14px">
                            </td>
                            <td style="width: 80px">
                                位置:</td>
                            <td style="width: 300px">
                                    <asp:TextBox ID="TxtLocationInput" runat="server" Width="285px"></asp:TextBox></td>
                            <td style="width: 40px">
                            <u onclick="ShowLocPage()" style="color: #0000ff; cursor: hand;">[查找]</u>
                            </td>
                            <td width="80">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="False">LnkBtnGetPartNum</asp:LinkButton></td>
                        </tr>
                    </table><table>
                        <tr>
                            
                            
                            <td width="20">
                            </td>
                            <td width="80">
                                合格数量:</td>
                            <td width="140">
                                <asp:Label ID="LblOkQtyD" runat="server" Font-Bold="True" Width="120px"></asp:Label></td>
                            <td width="40">
                            </td>
                            <td width="100">
                                不合格数量:</td>
                            <td width="140">
                                <asp:Label ID="LblBadQtyD" runat="server" Font-Bold="True" Width="120px"></asp:Label></td>
                            <td width="40">
                            </td>
                            <td width="80">
                                免检数量:</td>
                            <td width="80">
                                <asp:Label ID="LblNochkQtyD" runat="server" Font-Bold="True" Width="120px"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div style="border: solid 1px; width: 700px; padding: 10px 10px 10px 10px; margin: 10px 0 10px 0;
                    font-size: 10pt">
                    录入转移数量：
                    <hr />
                    <table>
                        <tr>
                            <td width="20">
                            </td>
                            <td width="80">
                                合格数量:</td>
                            <td width="140">
                                <asp:TextBox ID="TxtOkQtyT" runat="server" Width="120px" Style="ime-mode: Disabled"></asp:TextBox></td>
                            <td width="40">
                            </td>
                            <td width="100">
                                不合格数量:</td>
                            <td width="140">
                                &nbsp;<asp:TextBox ID="TxtBadQtyT" runat="server" Width="120px" Style="ime-mode: Disabled"></asp:TextBox></td>
                            <td width="40">
                            </td>
                            <td width="80">
                                免检数量:</td>
                            <td width="80">
                                <asp:TextBox ID="TxtNocheckQtyT" runat="server" Width="120px" Style="ime-mode: Disabled"></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="button_1"
                                    OnClick="BtnSave_Click" OnClientClick="return checkInput();" />
                                <asp:Button ID="BtnBack" runat="server" Text="返回" CssClass="button_1" OnClick="BtnBack_Click" /></td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="HiddenLocId" runat="server" />
            </asp:Panel>
        
    </form>
</body>
</html>
