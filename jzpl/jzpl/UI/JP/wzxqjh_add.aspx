<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wzxqjh_add.aspx.cs" Inherits="jzpl.wzxqjh_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>普通物资申请</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css"> 
    #overLay{ width:100%; background:#E0E0E0;  position:absolute; top:0; left:0; display:none; opacity:0; filter:alpha(opacity=0);}
    #popuContent{ width:370px; line-height:20px;  display:none; position:absolute; z-index:100; background:#fff; border:2px solid #888888;}   
    .T-hidden{ display:none;}
    </style>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript">        
    var xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
    
    window.onload=function(){init();}
    
    function init() {
    myf("TxtPartName").readOnly=true;
    SetGVBoxHeight("UserSaveData","GVData");    
    }
    
    
    function CallServerForGetPartName()
    {
        var partNo = myf("TxtPartNo").value;       
        
        var proj = myf("DdlProject").value;
        
        if((partNo==null) ||( partNo=="")){ 
        myf("TxtPartName").value="";
        return;}
        if(proj==null||proj=="0") { 
        myf("TxtPartName").value="";
        return;}
        var url = "ajaxHandler.aspx?mode=getpname&pno="+escape(partNo)+"&proj="+escape(proj);
        xmlHttp.open("GET",url,true);
        xmlHttp.onreadystatechange = setPartName;
        xmlHttp.send(null);
    }
    
    function setPartName()
    {
        if (xmlHttp.readyState == 4) {
            var partName = xmlHttp.responseText;
            if(partName=="")
            {
                alert("零件不存在！");
            }
            else
            {
                myf("TxtPartName").value = partName;
            } 
        }  
    }
    
    function checkInput1()
    {
        var partNo = myf("TxtPartNo").value;              
        var proj = myf("DdlProject").value;        
        var partName = myf("TxtPartName").value;
        var mtrNo = myf("TxtMtrNo").value;        
        //ming.li 20130318 取消必须选择项目
        //if( proj=="0" )
        //{
        //    alert("请选择项目！");
        //    return false;
        //}
        if(proj=="-1")
        {
            alert("请先设置可访问项目！");
        }
        if(!partNo=="")
        {        
            if(partName==""||partName==null)        
            {
                alert("无效零件！");
                return false;
            }
        }
        else
        {
            if(mtrNo=="")
            {
                alert("零件编号与物料流水号不能同时为空！");
                return false;
            }
        }
        return true;
    }
    
    function checkInput2()
    {
        objs_ = myf("GVRation").getElementsByTagName("input");
        if(objs_.length < 1)
        {
            alert("无可用定额！");
            return false;
        }
        else
        {
           for(i=0;i<objs_.length;i++)
           {
           
              if(objs_[i].value != "0" && objs_[i].value!=null && objs_[i].value!="")
              {
                 break;
              }
              if(i==objs_.length-1)
              {
                 alert("无可用定额！");
                 return false;
              }
           }
        }
        
        var prodSite= myf("DdlProdSite").value;
        var recDate = myf("TxtDate").value;
        var recPerson = myf("DdlReceiptPerson").value;
        var recPersonIC = myf("TxtIC").value;
        var recPersonContact = myf("TxtContact").value;
        var receiptDept = myf("DdlReceiptDept").value;
        
        if(prodSite=="0")
        {
            alert("请选择生产场地！");
            return false;
        }
        if(recDate==""||recDate==null)
        {
            alert("请输入接收时间！");
            return false;
        }
        if(recPerson=="0")
        {
            alert("请输入接收人姓名！");
            return false;
        }
        if(recPersonIC==""||recPersonIC==null)
        {
            alert("请输入接收人IC！");
            return false;
        }
        if(recPersonContact==""||recPersonContact==null)
        {
            alert("请输入接收人联系电话！");
            return false;
        }
        if(receiptDept=="0")
        {
            alert("请输入接收部门！");
            return false;
        }
        return true;
    }
   
    function checkRationNum(num1_,num2_,num3_,num4_)
    { 
        var reg = /^\d+(\.\d+)?$/;
        if(num1_.value=="")
        {
            num1_.value=0;
            return false;
        }
        if(!reg.test(num1_.value))
        {
            alert("请输入数值！");
            num1_.value=0;
            num1_.focus();
            return false;
        }
        num1 = NullCovertToZero(num1_.value);//用户输入的需求数量
        num2 = NullCovertToZero(num2_);      //ERP定额数量
        num3 = NullCovertToZero(num3_);      //ERP下发数量
        num4 = NullCovertToZero(num4_);      //有效的申请数量
        if(num1 > FloatSub(num2,num3)||num1 >FloatSub(num2,num4)){  
        alert("超出可申请数量！最多申请数量为"+Math.min(FloatSub(num2,num3),FloatSub(num2,num4)));
        num1_.focus();
        return false;
        }
        return true;            
    }
    
    function NullCovertToZero(value)
    {
        
        if(value==""||value==null)
        {
            return 0;
        }
        else
        {        
            return parseFloat(value);            
        }
    }
    function open_ration_lov()
    {
        ret_ = window.showModalDialog("../../UI/JP/wzxqjh_ration_lov.aspx",window,"dialogHeight:680px;dialogWidth:760px");
        
        if(typeof(ret_)=="undefined")
        {
            return;
        }
        
        myf("DdlProject").value=ret_[0];
        myf("TxtMtrNo").value=ret_[1];
        myf("TxtPartNo").value=ret_[2];
        myf("TxtPartName").value=ret_[3];
        
        __doPostBack("BtnShowRation","");
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
    
    function open_mod_page(reqid,objid,rowversion)
    {
        //window.open("wzxqjh_mod.aspx?id="+reqid+"&objid="+objid+"&ver="+rowversion+"",window,"status:no;scrollbars:no,dialogWidth:490px;dialogHeight:600px"); 
        window.open("wzxqjh_mod.aspx?id="+reqid+"&objid="+objid+"&ver="+rowversion+"",'mod1','height=600px, width=490px, toolbar =no, menubar=no, scrollbars=no, resizable=yes, location=no, status=no');    
        return false;
    }
    
    </script>

</head>
<body style="font-size: 10pt;">
    <div id="overLay">
    </div>
    
    <form id="form1" runat="server">
        <div id="title">普通物资申请<hr /></div>
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="vertical-align: middle; font-size: 10pt"
                    id="ration_condition">
                    <tr>
                        <td style="width: 68px">
                            <asp:Label ID="LProject" runat="server" Text="项目："></asp:Label></td>
                        <td style="width: 340px">
                            <asp:DropDownList ID="DdlProject" runat="server" Width="330px" onchange="javascript:CallServerForGetPartName();">
                            </asp:DropDownList></td>
                        <td style="width: 85px">
                            <asp:Label ID="LMtr" runat="server" Text="物料流水号："></asp:Label></td>
                        <td style="width: 183px">
                            <asp:TextBox ID="TxtMtrNo" runat="server" Width="123px" ToolTip="输入时以“;”、“..”分隔多个物料流水号，例如，输入“1;2;3”可以查找物流流水号为1、2、3的定额，输入“1..10”可查找物流水号大于包含1到小于包含10之间的定额"></asp:TextBox>-<asp:TextBox ID="TxtMtrSeqNo" runat="server" Width="40px" ToolTip="输入时以“;”、“..”分隔多个定额行号，例如，输入“1;2;3”可以查找定额行号为1、2、3的定额，输入“1..10”可查找定额行号大于包含1到小于包含10之间的定额"></asp:TextBox></td>
                        <td style="width: 87px">
                            <img src="../../images/Search.gif" onclick="open_ration_lov();" style="cursor: hand"
                                title="查询ERP定额" />
                            
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="vertical-align: middle; font-size: 10pt"
                    id="ration_condition1">
                    <tr>
                        <td style="width: 68px">
                            <asp:Label ID="Label2" runat="server" Text="物资编码："></asp:Label></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TxtPartNo" runat="server" onblur="javascript:CallServerForGetPartName();"
                                Width="80px"></asp:TextBox></td>
                        <td style="width: 68px">
                            <asp:Label ID="Label3" runat="server" Text="物资描述："></asp:Label></td>
                        <td style="width: 440px">
                            <asp:TextBox ID="TxtPartName" runat="server" Width="430px" BorderStyle="Groove"></asp:TextBox></td>
                        <td style="width: 87px">
                            <asp:Button ID="BtnShowRation" runat="server" Text="查询" OnClientClick="if(!checkInput1()){return;};"
                                OnClick="BtnShowRation_Click" Height="21px" Width="60px" Font-Size="10pt" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </div>
            <hr />
            <asp:GridView ID="GVRation" runat="server" AutoGenerateColumns="False" CellPadding="0"
                ForeColor="#333333" Font-Size="10pt" Width="100%" EmptyDataText="<b>无可用定额</b>"
                CssClass="gv" BorderWidth="1px" OnRowDataBound="GVRation_RowDataBound">
                <EmptyDataTemplate>
                    <table style="width: 100%;" class="gv">
                        <tr>
                            <th style="width: 300px">
                                活动</th>
                            <th>
                                材料顺序号</th>
                            <th>
                                定额行号</th> 
                            <th>
                                零件号</th>
                            <th>
                                零件描述</th>              
                            <th>                            
                                定额数量</th>
                            <th>
                                下发数量</th>
                            <th>
                                申请数量</th>
                            <th>
                                剩余数量</th>
                            <th>
                                需求数量</th>
                            <th>
                                Partial Information</th>
                            <th>
                                Design Code/Name</th>
                        </tr>
                        <tr>
                            <td colspan="10" style="color: Black; background: White; text-align: center; font-weight: bold;
                                border: solid 1 black;">
                                <b>无可用定额</b></td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="活动">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;"
                                title="<%# Eval("activity_desc")%>">
                                <%# Eval("activity_desc")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="300px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="misc_tab_ref_no" HeaderText="材料顺序号" /><%--Cells[1]--%>
                    <asp:BoundField DataField="material_req_seq_no" HeaderText="定额行号" /><%--Cells[2]--%>
                    <asp:BoundField DataField="part_no" HeaderText="零件号" />
                    <asp:TemplateField HeaderText="零件描述">
                        <ItemTemplate>
                            <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px;"
                                title="<%# Eval("part_description")%>">
                                <%# Eval("part_description")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="RATION_QTY" HeaderText="定额数量" /><%--Cells[5]--%>
                    <asp:BoundField DataField="issued_qty" HeaderText="下发数量" /><%--Cells[6]--%>
                    <asp:BoundField DataField="REQUESTED_QTY" HeaderText="申请数量" /><%--Cells[7]--%>
                    <asp:BoundField DataField="REMAIN_QTY" HeaderText="剩余数量" /><%--Cells[8]--%>
                    <asp:TemplateField HeaderText="需求数量"><%--Cells[9]--%>
                        <ItemTemplate>
                            <asp:TextBox ID="TxtReqQty" runat="server" Text='<%# Eval("REMAIN_QTY") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ControlStyle Width="80px" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="缺料配送方式"><%--Cells[10]--%>
                        <ItemTemplate>
                            <asp:DropDownList
                                ID="DDL_QH" runat="server">
                                 <asp:ListItem Value="1">继续配送</asp:ListItem>
                                 <asp:ListItem Value="2">取消配送</asp:ListItem>
                                 <asp:ListItem Value="3">需确认</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ControlStyle Width="105px" />
                        <HeaderStyle Width="105px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="block" HeaderStyle-CssClass="T-hidden" ItemStyle-CssClass="T-hidden" /><%--Cells[11]--%>
                    <asp:BoundField DataField="project_id" HeaderText="项目号" /><%--Cells[12]--%>
                    <asp:BoundField DataField="PARTIAL_INFO" HeaderText="Partial Information" /><%--分段 Cells[13]--%>
                    <asp:BoundField DataField="DESIGN_CODE" HeaderText="Design Code/Name" /><%--Cells[14]--%>
                </Columns>                
            </asp:GridView>
            <hr />
            <table id="req_content" style="font-size: 10pt">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="接收场地："></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="DdlProdSite" runat="server" Width="155px">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    <td style="width:40px">
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="接收时间："></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtDate" onfocus="WdatePicker();"
                            runat="server"></asp:TextBox><span style="color: red">*</span></td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        接收部门：</td>
                    <td>
                        <asp:DropDownList ID="DdlReceiptDept" runat="server" Width="155px">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    <td style="width: 20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="接收人："></asp:Label></td>
                    <td>
                        
                        <asp:DropDownList ID="DdlReceiptPerson" runat="server" Width="155px" AutoPostBack="True"
                            OnSelectedIndexChanged="DdlReceiptPerson_SelectedIndexChanged">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    <td>
                        <u onclick="display_popudiv()" style="color: #0000ff; cursor: hand;">[添加]</u>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="IC："></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtIC" runat="server"></asp:TextBox><span style="color: #ff0000">*</span></td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="联系方式："></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtContact" runat="server"></asp:TextBox><span style="color: #ff0000">*</span></td>
                    <td style="width: 20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="系统："></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtSystem" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="施工内容："></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtWorkContent" runat="server"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="分段：" Visible="False"></asp:Label>
                        <asp:CheckBox ID="ChkDz" runat="server" Text="吊装" /></td>
                    <td>
                        <asp:TextBox ID="TxtBlock" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        </td>
                </tr>
            </table>
        </div>
        <hr />
        <asp:Button ID="BtnSave" runat="server" Text="保存" OnClick="BtnSave_Click" Height="21px"
            Width="60px" Font-Size="10pt" OnClientClick="return checkInput2();" />
        <asp:Button ID="BtnQuery" runat="server" Text="Button" OnClick="BtnQuery_Click" CssClass="hidden" />
        <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
        <div style="overflow-x: auto; overflow-y: none; width: 100%" id="UserSaveData" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" 
                ForeColor="#333333" CssClass="gv"
                Width="1860px" OnRowDataBound="GVData_RowDataBound" OnRowCommand="GVData_RowCommand" >                
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="编辑">
                        <ItemTemplate>
                            <%--<asp:ImageButton ID="ImageButton2" runat="server" CommandName="Edit" ImageUrl="~/images/eidt.gif" />--%>
                            <img src="../../images/eidt.gif" onclick="open_mod_page('<%# Eval("demand_id") %>','<%# Server.UrlEncode(Eval("objid").ToString()) %>','<%# Eval("rowversion") %>')" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="删除">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/delete_.gif" CommandArgument='<%# Eval("objid")+"^"+Eval("rowversion") %>'
                                CommandName="ReqLineDelete" OnClientClick="return window.confirm('确定要删除吗?')" CssClass="deleteCell" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle CssClass="deleteCellStyle" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="rowstate" HeaderText="状态">
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
                    <asp:BoundField DataField="matr_seq_no" HeaderText="材料顺序号">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="matr_seq_line_no" HeaderText="行号">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="require_qty" HeaderText="需求数量">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="place" HeaderText="接收场地">
                        <HeaderStyle Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receive_date" HeaderText="接收日期">
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
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="lack_type" HeaderText="缺料配送方式">
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
                     <asp:BoundField DataField="work_content" HeaderText="施工内容">
                        <HeaderStyle Width="200px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <br />
        </div>
        <div id="popuContent" style="z-index:100">
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
        <iframe id="DivShim" src="javascript:false;" scrolling="no" frameborder="0" style="position:absolute; top:0px; left:0px; display:none;"></iframe>
    </form>
</body>
</html>
