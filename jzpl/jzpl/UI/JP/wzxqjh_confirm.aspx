<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wzxqjh_confirm.aspx.cs"
    Inherits="jzpl.wzxqjh_confirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>普通物资配送下达</title>
    <%--<style type="text/css">
    div,form,img,ul,ol,li,dl,dt,dd {margin: 0; padding: 0; border: 0;}
    p{margin:0}
    .gv_{font-size:10pt}
    .hidden{display:none}
    .pop1 {
     padding: 10px;
     width: 370px;
     border: 1px solid #000000;
     position: relative;
     left: -3px;
     top: -3px;
     background-color: #FFFFFF;
     font-zise:10pt;
    }
    .shape {
     position:absolute;
     width: 370px;
     background-color: #CCCCCC;
     top:200px;
     left:400px;
     display:none;
    }

    </style>--%>
    <style type="text/css"> 
    #overLay{ width:100%; background:#E0E0E0;  position:absolute; top:0; left:0; display:none; opacity:0; filter:alpha(opacity=0);}
    #popuContent{ width:410px; line-height:20px;  display:none; position:absolute; z-index:100; background:#fff; border:2px solid #888888;}   
    </style>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript" src="../../UI/script/wzxqjh.js"></script>
    <script type="text/javascript">    
    window.onload=function(){init();}    
    function init() {    
    SetGVBoxHeight("gvbox","GVData");
    }   
    function nextdo(obj,objid,rowversion)
    {  
        objreq = obj.parentNode.parentNode.childNodes[1].getElementsByTagName("input")[0];
        var reg = /^\d+(\.\d+)?$/;
        if(objreq.value==null||objreq.value=="")
        {
            objreq.value=0;
            return false;
        }
        if(!reg.test(objreq.value))
        {
            alert("请输入数值！");
            objreq.value=0;
            objreq.focus();
            return false;
        }       
        formReqId = document.getElementById("reqid");    
        formReleaseNum = document.getElementById("releaseNum");
        formDo = document.getElementById("do");    
        //popudiv = document.getElementById("out_popu");
        
        releaseQty = parseFloat(objreq.value);
        reqQty = parseFloat(obj.parentNode.parentNode.childNodes[2].innerText);
        
        formReleaseNum.value=releaseQty;
        formReqId.value=obj.getAttribute("reqid");
        
        if(releaseQty==0)
        {
            formDo.value = "cancel";
            document.getElementById("HidObjId").value=objid;
            document.getElementById("HidRowversion").value=rowversion;
            display_popudiv();
            //show(popudiv,event.clientX+40,event.clientY+30);
            return false;
        }
        if(releaseQty == reqQty)
        {  
            //hidden(popudiv);
            return true;
        }
        if(releaseQty>reqQty)
        {
            alert("下达数量不能超出申请数量！");
            return false;
        }
        document.getElementById("HidObjId").value=objid;
        document.getElementById("HidRowversion").value=rowversion;
        //show(popudiv,event.clientX+40,event.clientY+30);
        display_popudiv();
        formDo.value="confirm";
        return false;
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
    </script>

</head>
<body >
<div id="overLay"></div>
    <form id="form1" runat="server">
    <div id="title">普通物资配送下达<hr /></div>
        <div >
        <table cellpadding="0" cellspacing="0" >
            <tr>
                <td>
                    项目：</td>
                <td colspan="4">
                    <asp:DropDownList ID="DdlProject" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <span style="color: #ff0000;"></span>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <span style="color: #ff0000"></span>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    材料顺序号：</td>
                <td>
                    <asp:TextBox ID="TxtMtrSeqNo" runat="server"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td>
                    行号：</td>
                <td>
                    <asp:TextBox ID="TxtMtrLineNo" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    物资编码：</td>
                <td>
                    <asp:TextBox ID="TxtPartNo" runat="server"></asp:TextBox></td>
                <td>
                    <span style="color: #ff0000">%</span></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    接收日期：</td>
                <td>
                    <asp:TextBox ID="TxtRecieveDate" runat="server" onfocus="WdatePicker();"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    接收人：</td>
                <td>
                    <asp:TextBox ID="TxtReciever" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    接收地：</td>
                <td>
                    <asp:DropDownList ID="DdlProdSite" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    接收部门：</td>
                <td>
                    <asp:DropDownList ID="DdlReceiptDept" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                    申请组：</td>
                <td>
                    <asp:DropDownList ID="DdlReqGroup" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    库位：</td>
                <td>
                    <asp:TextBox ID="TxtLocation" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    状态：</td>
                <td>
                    <asp:TextBox ID="TxtRowstate" runat="server"></asp:TextBox></td>
                <td>
                </td>
                <td>
                </td>
                <td colspan="2">
                    <span style="font-size: 8pt; color: #ff0000">（I/初始 R/下达 F/完成&nbsp; C/待确认 Ca/取消）</span></td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
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
        </div>
        <div style="overflow-x: auto; overflow-y: none; width: 100%; " runat="server" id="gvbox" >
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" 
                ForeColor="#333333" CssClass="gv" Width="1840px" OnPageIndexChanging="GVData_PageIndexChanging"
                PageSize="20" OnRowDataBound="GVData_RowDataBound"  OnRowCommand="GVData_RowCommand" AllowPaging="True">                
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="下达/取消">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgRelease" runat="server" CausesValidation="False" CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>'
                                CommandName="Release" ImageUrl="~/images/issue.png" ToolTip="下达" />
                             <asp:ImageButton
                                    ID="ImgCancelRelease" runat="server" CausesValidation="False" CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>'
                                    CommandName="CancelRelease" ImageUrl="~/images/GoPreviousState.png" OnClientClick='return confirm("确定要取消下达吗？");'
                                    ToolTip="取消下达" /><asp:Image ID="ImgNotAccess" runat="server" ImageUrl="~/images/notaccess1.gif" />
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下达数量">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <asp:TextBox ID="TxtReleaseQty" runat="server" Text='<%# Eval("release_qty_1") %>' Width="60px"></asp:TextBox>
                            <asp:Label ID="LblReleaseQty" runat="server" Text='<%# Eval("release_qty_1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="require_qty" HeaderText="需求数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rowstate" HeaderText="状态">
                        <HeaderStyle Width="60px" CssClass="hidden"/>
                        <ItemStyle CssClass="hidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rowstate_zh" HeaderText="状态">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_id" HeaderText="项目">
                        <HeaderStyle Width="80px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:BoundField>
                    <asp:BoundField DataField="part_no" HeaderText="物资编号">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="物资描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px"
                                title="<%# Server.HtmlEncode(Eval("part_description").ToString())%>">
                                <%# Eval("part_description")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="location" HeaderText="库位">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="matr_seq_no" HeaderText="材料顺序号">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="matr_seq_line_no" HeaderText="行号">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="接收场地">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 160px"
                                title="<%# String.Format("{0}-{1}",Eval("place"),Eval("place_description"))%>">
                                <%# String.Format("{0}-{1}", Eval("place"), Eval("place_description"))%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />
                        <ItemStyle CssClass="gvdataitem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receive_date_str" HeaderText="接收日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="crane" HeaderText="吊装">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver" HeaderText="接收人">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_ic" HeaderText="IC">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_contact" HeaderText="联系方式">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_block" HeaderText="分段">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_system" HeaderText="系统">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
            
        </div>
     
        <div id="popuContent" style="z-index:100">
            <table style="font-size: 10pt; background: #000080; color: #FFFFFF; width: 100%">
                <tr>
                    <td>
                        选择缺料原因</td>
                    <td style="text-align: right">
                        <input type="button" value="x" style="height: 18px; width: 18px; font-size: 7pt;
                            padding: 0; vertical-align: middle; text-align: center" id="BtnQuitPopuWin1"
                            onclick="close_popudiv();" /></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="padding: 5px">
                        缺料原因：
                    </td>
                    <td style="width:300px">
                        <asp:DropDownList
                                ID="DdlLackMsg" Width="290px" runat="server">
                            </asp:DropDownList>
                    </td>
                </tr>
               
            </table>
            <hr />
            <table style="width: 100%; margin-bottom: 20px">
                <tr>
                    <td style="text-align: right; padding-right: 20px">
                        <asp:Button ID="BtnLackOK" runat="server" Text="确认" CssClass="button_1" OnClientClick="close_popudiv()" CommandName="LackOK"
                             OnClick="BtnLackOK_Click" />&nbsp; &nbsp;<input id="BtnCloseLackReasenPopuDiv"
                                type="button" value="取消" class="button_1" onclick="close_popudiv();" />
                    </td>
                </tr>
            </table>
            <input type="hidden" name="reqid" id="reqid" />
            <input type="hidden" name="do" id="do"/>
            <input type="hidden" name="releaseNum" id="releaseNum" />
            <asp:HiddenField ID="HidObjId" runat="server" />
            <asp:HiddenField ID="HidRowversion" runat="server" />
        </div>
        <iframe id="DivShim" src="javascript:false;" scrolling="no" frameborder="0" style="position:absolute; top:0px; left:0px; display:none;"></iframe>
    </form>
</body>
</html>