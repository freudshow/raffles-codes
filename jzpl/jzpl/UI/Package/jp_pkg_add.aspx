<%@ Page Language="C#" AutoEventWireup="true" Codebehind="jp_pkg_add.aspx.cs" Inherits="Package.jp_pkg_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>大包物资申请</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #overLay{ width:100%; background:#E0E0E0;  position:absolute; top:0; left:0; display:none; opacity:0; filter:alpha(opacity=0);}
    #popuContent{ width:370px; line-height:20px;  display:none; position:absolute; z-index:100; background:#fff; border:2px solid #888888;}    
    </style>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript">
    window.onload=function(){init_form();}
    function init_form()
    {
        myf("TxtPartNo").readOnly=true;
        myf("TxtPkgNo").readOnly=true;      
        myf("TxtProject").readOnly=true;    
        myf("TxtPkgName").readOnly=true;    
        myf("TxtPO").readOnly=true;    
        myf("TxtPartName").readOnly=true;    
        myf("TxtPartNameE").readOnly=true;    
        myf("TxtPartSpec").readOnly=true;    
        myf("TxtPartUnit").readOnly=true;    
        myf("TxtOnHandQty").readOnly=true;    
        myf("TxtAvaiQty").readOnly=true;    
        
    }
    function show_part_lov()
    {
        var ret_= window.showModalDialog("pkg_part_lov1.aspx",window,"status:no;dialogWidth:650px;dialogHeight:700px");
        if(typeof(ret_)=="undefined")
        {
            myf("TxtPartNo").value="";            
            myf("TxtProject").value ="";
            myf("TxtPkgNo").value="";
            myf("TxtPkgName").value="";
            myf("TxtPO").value="";
            myf("TxtPartName").value="";
            myf("TxtPartNameE").value="";
            myf("TxtPartSpec").value="";
            myf("TxtPartUnit").value="";
            myf("TxtOnHandQty").value="";
            myf("TxtAvaiQty").value="";
        }
        else
        {
            myf("TxtPartNo").value=ret_[0];            
            myf("TxtProject").value =ret_[1];
            myf("TxtPkgNo").value=ret_[2];
            myf("TxtPkgName").value=ret_[3];
            myf("TxtPO").value=ret_[4];
            myf("TxtPartName").value=ret_[5];
            myf("TxtPartNameE").value=ret_[6];
            myf("TxtPartSpec").value=ret_[7];
            myf("TxtPartUnit").value=ret_[8];
            myf("TxtOnHandQty").value=ret_[9];
            myf("TxtAvaiQty").value=ret_[10];          
        }
    }
    function check_input()
    {
        //必添项校验     
      
        var partNo =  myf("TxtPartNo").value;        
        var prodSite= myf("DdlProdSite").value;
        var recDate = myf("TxtDate").value;
        var recPerson = myf("DdlReceiptPerson").value;
        var recPersonIC = myf("TxtIC").value;
        var recPersonContact = myf("TxtContact").value;
        var receiptDept = myf("DdlReceiptDept").value;        
        
        if(partNo==""||partNo==null)
        {
            alert("保存失败，零件编号不能为空！");
            return false;
        }
        if(prodSite=="0")
        {
            alert("保存失败，请选择生产场地！");
            return false;
        }
        if(recDate==""||recDate==null)
        {
            alert("保存失败，请输入接收时间！");
            return false;
        }
        if(recPerson=="0")
        {
            alert("保存失败，请输入接收人姓名！");
            return false;
        }
        if(recPersonIC==""||recPersonIC==null)
        {
            alert("保存失败，请输入接收人IC！");
            return false;
        }
        if(recPersonContact==""||recPersonContact==null)
        {
            alert("保存失败，请输入接收人联系电话！");
            return false;
        }
        if(receiptDept=="0")
        {
            alert("保存失败，请输入接收部门！");
            return false;
        }
        
        //校验需求数量是否为有效数值
        var objReqQty = myf("TxtReqNum");        
        var reg = /^\d+(\.\d+)?$/; 
        if(objReqQty.value==null||objReqQty.value==""||!reg.test(objReqQty.value))
        {
            alert("保存失败，请输入有效数值。");
            objReqQty.value=0;
            objReqQty.focus();
            return false;
        }         
        //校验输入需求数量是否超可用数量       
        var objAvaiQty =myf("TxtAvaiQty");
        
        if(Math.round(parseFloat(objReqQty.value)*1000000-parseFloat(objAvaiQty.value)*1000000)/1000000 > 0)
        {
            alert("保存失败，申请数量超出了可用数量。");
            objReqQty.focus();
            objReqQty.select();
            return false;
        }
        
    }
    function open_mod_page(reqid,objid,rowversion)
    {
        window.showModalDialog("jp_pkg_mod.aspx?id="+reqid+"&objid="+objid+"&ver="+rowversion+"",window,"status:no;dialogWidth:480px;dialogHeight:600px");
        return false;
    }
    
    function refresh()
    {
        __doPostBack('BtnRefresh','');
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
<body style="font-size: 10pt;">
    <div id="overLay">
    </div>
    <form id="form1" runat="server">
        <div id="title">
            大包物资申请
            <hr style="font-weight: bold" />
        </div>
        <div style="background-color: AliceBlue; padding: 4px; margin-bottom: 4px" id="prompt" runat="server" visible=false>
            <p>
                <span style="font-size: 9pt; color: red; font-family: 宋体; float: left">为了您的领料方便，以下信息请知悉：</span></p>
            <p style="float: right">
                <b style="cursor: hand; border-bottom: solid 1px #000; border-right: solid 1px #000;
                    border-left: solid 1px #fff; border-top: solid 1px #fff; padding:0 2px; display:block; color:Blue " onclick="myf('prompt').style.display='none'">
                    &nbsp;C&nbsp;</b></p>
            <p style="clear: both">
            </p>
            <p style="margin: 0cm 0cm 0pt; text-indent: 18pt">
                <span style="font-size: 9pt; color: red; font-family: 宋体">材料配送需要提前两天申请，直接领用需要提前一天申请。如有紧急用料需求，申请完成后请辅助以电话或者邮件的形式找仓库相关责任人催料。</span></p>
        </div>
        <div>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        零件编码：</td>
                    <td style="width: 150px">
                        <asp:TextBox ID="TxtPartNo" runat="server" Width="150px" BorderStyle="Groove"></asp:TextBox></td>
                    <td style="width: 60px">
                        <img src="../../images/Search.gif" style="cursor: hand;" onclick="show_part_lov()"
                            id="IMG1" />
                    </td>
                    <td style="width: 80px">
                        申请数量：</td>
                    <td style="width: 150px">
                        <asp:TextBox ID="TxtReqNum" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <asp:CheckBox ID="ChkPS" runat="server" Text="配送" TextAlign="Left" /></td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td width="120">
                        项目：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtProject" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td width="120">
                        PO：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPO" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        大包编码：</td>
                    <td>
                        <asp:TextBox ID="TxtPkgNo" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="120">
                        大包名称：</td>
                    <td>
                        <asp:TextBox ID="TxtPkgName" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        零件名称（英文）：</td>
                    <td>
                        <asp:TextBox ID="TxtPartNameE" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="120">
                        零件名称（中文）：</td>
                    <td>
                        <asp:TextBox ID="TxtPartName" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        零件规格：</td>
                    <td>
                        <asp:TextBox ID="TxtPartSpec" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="120">
                        单位：</td>
                    <td>
                        <asp:TextBox ID="TxtPartUnit" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        现有数量：</td>
                    <td>
                        <asp:TextBox ID="TxtOnHandQty" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td width="120">
                        可用数量：</td>
                    <td>
                        <asp:TextBox ID="TxtAvaiQty" runat="server" BorderStyle="Groove" Width="250px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <table id="req_content" style="font-size: 10pt">
                <tr>
                    <td width="80">
                        <asp:Label ID="Label1" runat="server" Text="接收场地："></asp:Label></td>
                    <td style="width: 174px">
                        <asp:DropDownList ID="DdlProdSite" runat="server" Width="160px">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    <td style="width: 40px">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label5" runat="server" Text="接收时间："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtDate" runat="server" Width="154px" onfocus="WdatePicker();"></asp:TextBox><span
                            style="color: red">*</span></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        接收部门：</td>
                    <td width="180">
                        <asp:DropDownList ID="DdlReceiptDept" runat="server" Width="160px">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    <td width="20">
                    </td>
                    <td width="60">
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        <asp:Label ID="Label6" runat="server" Text="接收人："></asp:Label></td>
                    <td style="width: 174px;">
                        <asp:DropDownList ID="DdlReceiptPerson" runat="server" Width="160px" AutoPostBack="True"
                            OnSelectedIndexChanged="DdlReceiptPerson_SelectedIndexChanged">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    <td style="width: 40px">
                        <u onclick="display_popudiv()" style="color: #0000ff; cursor: hand;">[添加]</u></td>
                    <td width="80">
                        <asp:Label ID="Label7" runat="server" Text="IC："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtIC" runat="server" Width="154px"></asp:TextBox><span style="color: #ff0000">*</span></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label8" runat="server" Text="联系方式："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtContact" runat="server" Width="154px"></asp:TextBox><span style="color: #ff0000">*</span></td>
                    <td width="20">
                    </td>
                    <td width="60">
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        <asp:Label ID="Label9" runat="server" Text="分段："></asp:Label></td>
                    <td style="width: 174px">
                        <asp:TextBox ID="TxtBlock" runat="server" Width="154px"></asp:TextBox></td>
                    <td style="width: 40px">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label10" runat="server" Text="系统："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtSystem" runat="server" Width="154px"></asp:TextBox></td>
                    <td width="20">
                    </td>
                    <td width="80">
                        <asp:Label ID="Label11" runat="server" Text="施工内容："></asp:Label></td>
                    <td width="180">
                        <asp:TextBox ID="TxtWorkContent" runat="server" Width="154px"></asp:TextBox></td>
                    <td width="20">
                    </td>
                    <td width="60">
                        <asp:CheckBox ID="ChkDz" runat="server" Text="吊装" /></td>
                </tr>
            </table>
            <hr />
            <asp:Button ID="BtnSave" runat="server" Text="保存" OnClick="BtnSave_Click" OnClientClick="return check_input()"
                CssClass="button_1" />
        </div>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="UserSaveData" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" CssClass="gv"
                Width="1840px" OnRowDataBound="GVData_RowDataBound" BorderWidth="1px" OnRowCommand="GVData_RowCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="编辑">
                        <ItemTemplate>
                            <img src="../../images/eidt.gif" onclick="open_mod_page('<%# Eval("requisition_id") %>','<%# Server.UrlEncode(Eval("objid").ToString()) %>','<%# Eval("rowversion") %>')" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="删除">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/delete_.gif" CommandName="ReqLineDelete"
                                CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>' OnClientClick="return window.confirm('确定要删除吗?')" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle CssClass="deleteCellStyle" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="requisition_id" HeaderText="申请ID">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="psflag_str" HeaderText="申请类别" HeaderStyle-Width="80px" />
                    <asp:BoundField DataField="part_no" HeaderText="零件编号">
                        <HeaderStyle Width="160px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="零件描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title="<%# Server.HtmlEncode(Eval("part_name_e").ToString())%>">
                                <%# Eval("part_name_e")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="package_no" HeaderText="大包编码">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="大包描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                title="<%# Server.HtmlEncode(Eval("package_name").ToString())%>">
                                <%# Eval("package_name")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="require_qty" HeaderText="申请数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_id" HeaderText="项目">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="接收场地">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 160px"
                                title="<%# Eval("place_name")%>">
                                <%# Eval("place_name")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receipt_date_str" HeaderText="接收日期">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="吊装">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkCrane" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receiver" HeaderText="接收人">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_ic" HeaderText="IC">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receiver_contract" HeaderText="联系方式">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_block" HeaderText="分段">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project_system" HeaderText="系统">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="objid" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="rowversion" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
        <asp:Button ID="BtnRefresh" runat="server" CssClass="hidden" OnClick="BtnRefresh_Click"
            Text="refresh" UseSubmitBehavior="False" />
        <div id="popuContent" style="z-index: 100">
            <table style="font-size: 10pt; background: #000080; color: #FFFFFF; width: 100%">
                <tr>
                    <td>
                        添加接收人</td>
                    <td style="text-align: right">
                        <input type="button" value="x" style="height: 18px; width: 18px; font-size: 7pt;
                            padding: 0; vertical-align: middle; text-align: center" id="BtnQuitPopuWin1"
                            onclick="close_popudiv();" /></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="padding: 5px">
                        姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtReceiptPersonName" runat="server" Width="260px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px">
                        IC：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtReceiptPersonIC" runat="server" Width="260px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px">
                        联系电话：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtReceiptPersonContact" runat="server" Width="260px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%; margin-bottom: 20px">
                <tr>
                    <td style="text-align: right; padding-right: 20px">
                        <asp:Button ID="BtnAddPerson" runat="server" Text="确认" CssClass="button_1" OnClientClick="close_popudiv()"
                            OnClick="BtnAddPerson_Click" />&nbsp; &nbsp;<input id="BtnCloseLackReasenPopuDiv"
                                type="button" value="取消" class="button_1" onclick="close_popudiv();" />
                    </td>
                </tr>
            </table>
        </div>
        <iframe id="DivShim" src="javascript:false;" scrolling="no" frameborder="0" style="position: absolute;
            top: 552px; left: 455px; display: none;"></iframe>
    </form>
</body>
</html>
