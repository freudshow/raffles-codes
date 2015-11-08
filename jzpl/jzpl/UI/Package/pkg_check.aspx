<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_check.aspx.cs" Inherits="Package.pkg_check" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包检验</title>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #overLay{ width:100%; background:#E0E0E0;  position:absolute; top:0; left:0; display:none; opacity:0; filter:alpha(opacity=0);}
    #popuContent{ width:720px; line-height:20px;  display:none; position:absolute; z-index:100; background:#fff; border:2px solid #888888;}    
    </style>

    <script type="text/javascript">
    window.onload=function(){init();}
    function init(){
    SetGVBoxHeight("gvbox","CKGrid")
    }
    function setOpacity(elem,current){
	//如果是ie浏览器
	if(elem.filters){ 
	elem.style.filter = 'alpha(opacity=' + current + ')';
    }
    else{ //否则w3c浏览器
	elem.style.opacity = current/100;
    }
	}
	
	function display_popudiv()
	{
	    alertEle = myf('popuContent'); 
	    obj = myf('overLay');
	    obj.style.display = 'block';
		//判断页面的高度是否超过浏览器工作区的高度
		if(document.body.offsetHeight>=document.documentElement.clientHeight){
			obj.style.height = document.body.offsetHeight + 'px'; 
		}else{
			obj.style.height = document.documentElement.clientHeight + 'px';
		}
		var currentOpacity = 0;
		//设置定时器timer1
		var timer1 = setInterval(
			function(){
				if(currentOpacity<=50){
					setOpacity(obj,currentOpacity);
					currentOpacity+=5;	
				}
				else{
					clearInterval(timer1);
				}
			}
		,50);
		alertEle.style.display = 'block';
		alertEle.style.left = (document.body.offsetWidth - alertEle.offsetWidth)/2 + 'px';
		alertEle.style.top = '100px';
		
		if(isie6())
		{
		
		IfrRef = myf('DivShim');
		IfrRef.style.display='block';
		IfrRef.style.visibility='visible';
		IfrRef.style.top='100px';
		IfrRef.style.left=(document.body.offsetWidth - alertEle.offsetWidth)/2 + 'px';
		IfrRef.style.height=alertEle.offsetHeight;
		IfrRef.style.width=alertEle.offsetWidth;
		IfrRef.style.zIndex=alertEle.style.zIndex -1;
		
		}
	}
	
	function close_popudiv()
	{
	    alertEle = myf('popuContent'); 
	    obj = myf('overLay');
	    alertEle.style.display = 'none';
		var currentOpe = 50;
		//设置定时器timer2
		var timer2 = setInterval(
			function(){
				if(currentOpe>=0){
					setOpacity(obj,currentOpe);
					currentOpe-=5;
				}
				else{
					obj.style.display = 'none';
					clearInterval(timer2);
				}
			}
		,50);
		
		if(isie6())
		{
		IfrRef = myf('DivShim');
		IfrRef.style.display='none';		
		}
	}	
	
	function isie6()
    {
        var browser=navigator.appName 
        var b_version=navigator.appVersion 
        var version=b_version.split(";"); 
        var trim_Version=version[1].replace(/[ ]/g,"");  
        if(browser=="Microsoft Internet Explorer" && trim_Version=="MSIE6.0") 
        { 
            return true; 
        } 
        return false;
    }
    
    function NonCheck_GVTxtOnhandQty_onblur()
    {
        totalArrNum = TableColumnTotalSalc('GVNonCheck',4,1);
        myf("LblTotal").innerText=totalArrNum;
        myf("TxtArrQty").value = totalArrNum;
    }
    
    function FirstCheck_GVTxtOnhandQty_onblur(onhand,ok,bad)
    {
        GVFirstSalc(onhand,ok,bad)
    }
    
    function FirstCheck_GVTxtOkQty_onblur(onhand,ok,bad)
    {
        GVFirstSalc(onhand,ok,bad)
    }
    
    function SecondCheck_GVTxtOkQty_onkeyup(onhandQty,ok,bad)
    {
        badqty = parseFloat(onhandQty)-parseFloat(myf(ok).value);
        
        myf(bad).innerText = badqty;
        
        total = TableColumnTotalSalc('GVSecondCheck',5,0);
        oksum = TableColumnTotalSalc('GVSecondCheck',6,1);
        badsum = TableColumnTotalSalc('GVSecondCheck',7,2);
        
        myf("LblTotalOk").innerText = oksum;
        myf("LblTotalBad").innerText = badsum;
        myf("LblTotal").innerText = total;
    }
    function SecondCheck_GVTxtOkQty_onblur(onhandQty,ok,bad)
    {
        badqty = parseFloat(onhandQty)-parseFloat(myf(ok).value);
        
        myf(bad).innerText = badqty;
        
        total = TableColumnTotalSalc('GVSecondCheck',5,0);
        oksum = TableColumnTotalSalc('GVSecondCheck',6,1);
        badsum = TableColumnTotalSalc('GVSecondCheck',7,2);
        
        myf("LblTotalOk").innerText = oksum;
        myf("LblTotalBad").innerText = badsum;
        myf("LblTotal").innerText = total;
    }
    
    function GVFirstSalc(onhand,ok,bad)
    {
        badqty = parseFloat(myf(onhand).value)-parseFloat(myf(ok).value);
        
        myf(bad).innerText = badqty;
        
        total = TableColumnTotalSalc('GVFirstCheck',4,1);
        oksum = TableColumnTotalSalc('GVFirstCheck',5,1);
        badsum = TableColumnTotalSalc('GVFirstCheck',6,2);
        
        myf("LblTotalOk").innerText = oksum;
        myf("LblTotalBad").innerText = badsum;
        myf("LblTotal").innerText = total;
    }
    </script>

</head>
<body>
    <div id="overLay">
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="title">
            大包检验<hr />
        </div>
        <div id="QueryPl" runat="server">
            <table cellpadding="0" cellspacing="0" class="TableInput">
                <tr>
                    <td style="width: 80px">
                        到货ID：</td>
                    <td style="width: 220px">
                        <asp:TextBox ID="TxtArrivalId" runat="server" Width="200px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                    <td style="width: 80px">
                        到货日期：
                    </td>
                    <td style="width: 230px">
                        <asp:TextBox ID="TxtDHrq" runat="server" Width="200px" onfocus="WdatePicker();"></asp:TextBox></td>
                    <td style="width: 120px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        大包编码：</td>
                    <td style="width: 220px">
                        <asp:TextBox ID="TxtDBbh" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    <td style="width: 20px">
                    </td>
                    <td style="width: 80px">
                        大包名称：</td>
                    <td style="width: 230px">
                        <asp:TextBox ID="TxtDBmc" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    <td style="width: 120px">
                        <span style="color: #ff0000"></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        零件编码：</td>
                    <td style="width: 220px">
                        <asp:TextBox ID="TxtPartNo" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    <td style="width: 20px">
                    </td>
                    <td style="width: 80px">
                        零件名称：</td>
                    <td style="width: 230px">
                        <asp:TextBox ID="TxtLJmc" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    <td style="width: 120px">
                        <span style="color: #ff0000"></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        合同号：</td>
                    <td style="width: 220px">
                        <asp:TextBox ID="TxtHTH" runat="server" Width="200px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                    <td style="width: 80px">
                        关单号：</td>
                    <td style="width: 230px">
                        <asp:TextBox ID="TxtGDH" runat="server" Width="200px"></asp:TextBox><span style="color: #ff0000">%</span></td>
                    <td style="width: 120px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        PO号：</td>
                    <td style="width: 220px">
                        <asp:TextBox ID="TxtPONo" runat="server" Width="200px"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                    <td style="width: 80px">
                        项目：</td>
                    <td style="width: 230px">
                        <asp:DropDownList ID="DDLProject" runat="server" Width="206px">
                        </asp:DropDownList></td>
                    <td style="width: 120px">
                        <asp:CheckBox ID="ChkCheck" runat="server" Text="只显示待检" /></td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnResult" runat="server" CssClass="button_1" Text="查看" OnClick="BtnResult_Click" /></td>
                </tr>
            </table>
            <div style="overflow-x: auto; overflow-y: none; width: 100%;" id="gvbox">
                <asp:GridView ID="CKGrid" runat="server" Width="1580px" CssClass="gv" AutoGenerateColumns="False"
                    OnRowEditing="CKGrid_RowEditing" AllowPaging="True" OnPageIndexChanging="CKGrid_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="到货ID">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandName="Edit" runat="server"><%# Eval("arrived_id")%></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="package_no" HeaderText="大包编码" />
                        <asp:BoundField DataField="part_no" HeaderText="零件编码" />
                        <asp:TemplateField HeaderText="零件描述" HeaderStyle-Width="260px">
                            <ItemTemplate>
                                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 250px;"
                                    title="<%# Server.HtmlEncode(Eval("part_name_e").ToString())%>">
                                    <%# Server.HtmlEncode(Eval("part_name_e").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="part_spec" HeaderText="规格" />
                        <asp:BoundField DataField="part_unit" HeaderText="单位" />
                        <asp:BoundField DataField="arr_date_ch" HeaderText="到货日期" />
                        <asp:BoundField DataField="req_qty" HeaderText="登记数量" />
                        <asp:BoundField DataField="arrived_qty" HeaderText="到货数量" />
                        <asp:BoundField DataField="ok_qty" HeaderText="合格数量" />
                        <asp:BoundField DataField="bad_qty" HeaderText="不合格数量" />
                        <asp:BoundField DataField="check_mark" HeaderText="检验标志" />
                        <asp:BoundField DataField="dec_no" HeaderText="关单号" />
                        <asp:BoundField DataField="contract_no" HeaderText="合同号" />
                        <asp:BoundField DataField="rowversion" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="objid" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
            </div>
        </div>
        <div id="MxPl" runat="server">
            <table>
                <tr>
                    <td>
                        <strong>到货ID：</strong><asp:Literal ID="LtDHID" runat="server"></asp:Literal>&nbsp;
                        <strong>到货日期：</strong><asp:Literal ID="LtDHrq" runat="server"></asp:Literal>&nbsp;
                        <strong>检验状态：</strong><asp:Literal ID="LtDHChkMark" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>
                        <strong>大包编号：</strong><asp:Literal ID="LtDBbh" runat="server"></asp:Literal>&nbsp;
                        <strong>大包描述：</strong><asp:Literal ID="LtDbms" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>
                        <strong>小件编号：</strong><asp:Literal ID="LtXJbh" runat="server"></asp:Literal>&nbsp;
                        <strong>小件名称：</strong><asp:Literal ID="LtXJmcen" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>
                        <strong>单位：</strong><asp:Literal ID="LtUnit" runat="server"></asp:Literal>&nbsp;
                        <strong>&nbsp;&nbsp; 规格：</strong><asp:Literal ID="LtXJgg" runat="server"></asp:Literal>&nbsp;
                        <strong>项目：</strong><asp:Literal ID="LtProject" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>
                        <strong>合同号：</strong><asp:Literal ID="LtHTH" runat="server"></asp:Literal>&nbsp;
                        <strong>关单号：</strong><asp:Literal ID="LtGDH" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>
                        <strong>登记数量：</strong><asp:Literal ID="LtDJsl" runat="server"></asp:Literal>&nbsp;
                        &nbsp;<strong> 到货数量：</strong><asp:Literal ID="LtDhsl" runat="server"></asp:Literal>&nbsp;
                        &nbsp; <strong>合格数量：</strong><asp:Literal ID="LtHgsl" runat="server"></asp:Literal>&nbsp;
                        &nbsp; <strong>不合格数量：</strong><asp:Literal ID="LtBhgsl" runat="server"></asp:Literal>&nbsp;
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <strong>检验记录：</strong></td>
                </tr>
            </table>
            <asp:GridView ID="GVCheckRec" runat="server" CssClass="gv" AutoGenerateColumns="False"
                Width="840px">
                <Columns>
                    <asp:BoundField DataField="check_id" HeaderText="检验ID">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="chk_date_ch" HeaderText="检验日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check_person" HeaderText="检验人">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="chk_ok_qty" HeaderText="合格数">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="chk_bad_qty" HeaderText="不合格数">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="no_check_qty" HeaderText="免检数">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="state" HeaderText="状态">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;
                                font-size: 12px;" title="<%# Eval("part_name_e") %>">
                                <%# Eval("remark") %>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table class="gv">
                        <tr>
                            <th style="width: 80px">
                                检验日期
                            </th>
                            <th style="width: 80px">
                                检验人
                            </th>
                            <th style="width: 80px">
                                合格数
                            </th>
                            <th style="width: 80px">
                                不合格数
                            </th>
                            <th style="width: 80px">
                                免检数
                            </th>
                            <th style="width: 80px">
                                状态
                            </th>
                            <th style="width: 300px">
                                备注
                            </th>
                        </tr>
                        <tr>
                            <td colspan="7" style="padding-left: 20px">
                                无检验信息</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:GridView>
            <hr />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table style="width: 700px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 80px;">
                                到货数量：</td>
                            <td style="width: 130px;">
                                <asp:TextBox ID="TxtArrQty" runat="server" Width="115px"></asp:TextBox><span style="color: #ff0000">*</span></td>
                            <td style="width: 15px;">
                            </td>
                            <td style="width: 80px;">
                                检查日期：</td>
                            <td style="width: 130px">
                                <asp:TextBox ID="TxtJCrq" runat="server" Width="115px" onfocus="WdatePicker();"></asp:TextBox></td>
                            <td style="width: 15px">
                            </td>
                            <td style="width: 60px">
                                检查人：</td>
                            <td style="width: 130px">
                                &nbsp;<asp:DropDownList ID="DdlJcr" runat="server" Width="120px"></asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 80px">
                                备 &nbsp; &nbsp; &nbsp; &nbsp;注：</td>
                            <td colspan="4">
                                <asp:TextBox ID="TxtBZ" runat="server" Height="60px" TextMode="MultiLine"
                                    Width="374px"></asp:TextBox></td>
                            <td style="width: 15px">
                            </td>
                            <td style="width: 60px">
                            </td>
                            <td style="width: 130px">
                            </td>
                        </tr>
                    </table>
                   
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" style="width:680px">
                        <tr>
                            <td style="width: 500px">
                                <asp:LinkButton ID="LnkBtnRegCheckResult" runat="server" Text="[登记检验结果]" OnClick="LnkBtnRegCheckResult_Click"></asp:LinkButton>
                                <asp:LinkButton ID="LnkBtnCancelRegResult" runat="server" Text="[取消检验结果登记]" OnClick="LnkBtnCancelRegResult_Click"></asp:LinkButton>
                            </td>
                            <td style="width: 180px; text-align: right">
                                <asp:CheckBox ID="ChkBoxMj" runat="server" AutoPostBack="True" OnCheckedChanged="ChkBoxMj_CheckedChanged"
                                    Text="免检" /></td>
                        </tr>
                    </table>
                    <asp:Panel ID="PnlCheckResult" runat="server">
                        <%--信息来自库存信息 --%>
                        <asp:GridView ID="GVSecondCheck" runat="server" AutoGenerateColumns="False" Width="700px"
                            CssClass="gv" OnRowDataBound="GVSecondCheck_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="company" HeaderText="公司">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="area" HeaderText="区域">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="location" HeaderText="库位">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="onhand_qty" HeaderText="总数">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="checking_qty" HeaderText="待检数">
                                    <HeaderStyle Width="70px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="合格数">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GVTxtOkQty" runat="server" Width="60px" Text='<%# Eval("checking_qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="不合格数">
                                    <ItemTemplate>
                                        <asp:Label ID="GVLblBadQty" runat="server" Text='0'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--来自到货信息 --%>
                        <asp:GridView ID="GVFirstCheck" runat="server" AutoGenerateColumns="False" Width="700px"
                            CssClass="gv" OnRowDataBound="GVFirstCheck_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="company" HeaderText="公司">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="area" HeaderText="区域">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="location" HeaderText="库位">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="清点数">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GVTxtOnhandQty" runat="server" Width="90px" Text='<%# Eval("reg_qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="合格数">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GVTxtOkQty" runat="server" Width="90px" Text='<%# Eval("reg_qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="不合格数">
                                    <ItemTemplate>
                                        <asp:Label ID="GVLblBadQty" runat="server" Text='0'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%-- 免检--%>
                        <asp:GridView ID="GVNonCheck" runat="server" AutoGenerateColumns="False" Width="700px"
                            CssClass="gv" OnRowDataBound="GVNonCheck_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="company" HeaderText="公司">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="area" HeaderText="区域">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="location" HeaderText="库位">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="清点数">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GVTxtOnhandQty" runat="server" Width="90px" Text='<%# Eval("reg_qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="合格数">
                                    <ItemTemplate>
                                        -
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="不合格数">
                                    <ItemTemplate>
                                        -
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <table style="width: 690px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 330px; height: 29px;">
                                </td>
                                <td style="width: 120px; text-align: right; height: 29px;">
                                    <asp:Label ID="LblTotalTitle" runat="server"></asp:Label>：<asp:Label ID="LblTotal" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
                                <td style="width: 120px; text-align: right; height: 29px;">
                                    合格总数：<asp:Label ID="LblTotalOk" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
                                <td style="width: 120px; text-align: right; height: 29px;">
                                    不合格总数：<asp:Label ID="LblTotalBad" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ChkBoxMj" />
                </Triggers>
            </asp:UpdatePanel>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnSubmit" runat="server" CssClass="button_1" Text="提 交" OnClientClick="return checkSubmit();"
                            OnClick="BtnSubmit_Click" />&nbsp;<asp:Button ID="BtnBack" runat="server" CssClass="button_1"
                                OnClick="BtnBack_Click" Text="返 回" /></td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <asp:HiddenField ID="HiddenCheckId" runat="server" />
        <asp:HiddenField ID="HiddenGVType" runat="server" />
        <asp:HiddenField ID="HiddenArrivalRowid" runat="server" />
        <asp:HiddenField ID="HiddenArrivalRowversion" runat="server" />
        <asp:HiddenField ID="HiddenCheckMark" runat="server" />
        
        </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

<script type="text/javascript">
    function checkSubmit()
    {
        ArrivalQty = myf("TxtArrQty").value;
        
        if(!checkNum(ArrivalQty))
        {
            alert("错误（C1），到货数量输入非数值。");
            return false;
        }        
        if(checkTextIsNull("TxtJCrq"))
        {
            alert('错误（C1）,检查日期不能为空！');
            return false;
        }   
        return true;
    }
    
</script>

