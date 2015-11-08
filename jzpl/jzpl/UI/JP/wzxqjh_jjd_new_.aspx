<%@ Page Language="C#" AutoEventWireup="true" Codebehind="wzxqjh_jjd_new_.aspx.cs"
    Inherits="jzpl.UI.JP.wzxqjh_jjd_new_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>普通物资交接单</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style>     
    #overLay{ width:100%; background:#E0E0E0;  position:absolute; top:0; left:0; display:none; opacity:0; filter:alpha(opacity=0);}
    #popuContent{ width:320px; line-height:20px;  display:none; position:absolute; z-index:100; background:#fff; border:2px solid #888888;}
    #divGreateJjd{display:block}
    #divJjdDisplay{}
    #divJjdHeadInfo{width:600px;border:1px solid #DDDDDD;background:#EEEEE7;margin:5px 0 10px 0;}
    #divJjdHeadInfo div{margin:10px 10px 10px 10px;padding:10px 10px 10px 10px;border:1px solid #888}
    #divReqData{margin:5px 0 0 0}
    #BtnSaveJjdHeadInfo{}    
    .button_2{width:100px;height:21px;font-size:10pt;padding:0}     
    </style>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../UI/script/wzxqjh_jjd.js"></script>
    <script type="text/javascript" src="../../UI/script/common.js"></script>
    <script type="text/javascript"> 
    
    window.onload=function(){init();}
    
    function init() {    
    SetGVBoxHeight("gvbox1","GVReqData");
    SetGVBoxHeight("gvbox2","GVDisplayJjdLine");
    SetGVBoxHeight("gvbox3","GVJjdList");
    }
    
//    function SetGVBoxHeight(boxid,gvid)
//    {
//        if(!$(boxid)||!$(gvid)) return;
//        $(boxid).style.height=$(gvid).offsetHeight + 20 + 'px';
//    }
    
    function HeadInfoEdit()
    {
    obj = myf("divJjdHeadInfo").getElementsByTagName("input");
    myf("BtnSaveJjdHeadInfo").style.display="block";
    for(i=0;i<obj.length;i++)
    {
    if(obj[i].type=="text"){
    obj[i].style.display="block";}
    }
    objSpan = myf("divJjdHeadInfo").getElementsByTagName("span");
    for(j=0;j<obj.length;j++)
    {
    objSpan[j].style.display="none";
    }
//    $("TxtZHd").style.display="block";
//    $("LblZHd").style.display="none";
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
<body>
    <div id="overLay">
    </div>
    <form id="form1" runat="server">
    <div id="title">普通物资交接单<hr /></div>
        <div id="divCreateJjd" runat="server">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        需求日期：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtRecieveDate" name="TxtRecieveDate" runat="server" AutoPostBack="True"
                            OnTextChanged="TxtRecieveDate_TextChanged" Width="142px" onfocus="WdatePicker();"></asp:TextBox><span style="color: #ff0000">*</span></td>
                    <td style="width: 25px">
                        <asp:ImageButton ID="ImgBtnQuery" runat="server" ImageUrl="~/images/Search.gif" OnClick="ImgBtnQuery_Click" />
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
                        接收地：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlReceiptPlace" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlReceiptPlace_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    <td>
                        <span style="color: #ff0000"></span></td>
                    <td>
                        接收部门：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlReceiptDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlReceiptDept_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 25px">
                        <span style="color: #ff0000">*</span></td>
                    <td style="width: 53px">
                        接收人：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlReceiptPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlReceiptPerson_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 25px">
                        <span style="color: #ff0000">*</span>
                    </td>
                    <%--增加库管选项，非必选 ming.li 20130321--%> 
                    <td style="width: 53px">
                        下达人：
                    </td>

                    <td>
                        <asp:DropDownList ID="DdlReleasePerson" runat="server" AutoPostBack="True" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <%--/增加库管选项 ming.li 20130321--%> 
                    <td>
                        <asp:LinkButton ID="LnkBtnInitQuery" runat="server" OnClick="LnkBtnInitQuery_Click">[重新筛选]</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        项目：</td>
                    <td>
                        <asp:DropDownList ID="DdlProject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlReceiptPlace_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td colspan="7">
                        <span style="color: #ff0000">（红色星号“*”标识的项必须填写或选择）</span></td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnQuery" runat="server" Text="查询" OnClick="BtnQuery_Click" CssClass="button_1" style="margin-bottom:0px" /></td>
                    <td>
                        <asp:Button ID="BtnNewJjd" runat="server" Text="创建交接单" OnClick="BtnNewJjd_Click"
                            CssClass="button_2" /></td>
                    <td>
                        <input type="button" value="加入交接单" id="BtnJoinJjd" class="button_2" runat="server" onclick="display_popudiv()"  /></td>
                    <td>
                        <asp:Button ID="BtnJjdQuery" runat="server" Text="查询交接单" CssClass="button_2" OnClick="BtnJjdQuery_Click" /></td>
                </tr>
            </table>
            <div id="divReqData" runat="server">
                <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox1">
                    <asp:GridView ID="GVReqData" runat="server" AutoGenerateColumns="False" OnRowDataBound="GVReqData_RowDataBound"
                        CssClass="gv" Width="990px">
                        <Columns>
                            <asp:TemplateField HeaderText="配送数量">
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtJjdLineQty" runat="server" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RATION_QTY" HeaderText="定额数量">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="require_qty" HeaderText="申请数量">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kp_qty" HeaderText="可配数量">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="yp_qty" HeaderText="已配数量">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="release_qty" HeaderText="下达数量">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="part_no" HeaderText="物资编号">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="物资描述">
                                <ItemTemplate>
                                    <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;
                                        margin: 0" title="<%# Eval("part_description")%>">
                                        <%# Eval("part_description")%>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="project_id" HeaderText="项目">
                                <HeaderStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="project_block" HeaderText="分段">
                                <HeaderStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="project_system" HeaderText="系统">
                                <HeaderStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="matr_seq_no" HeaderText="材料顺序号">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="matr_seq_line_no" HeaderText="行号">
                                <HeaderStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="objid">
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rowversion">
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="release_user" HeaderText="下达库管">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div id="popuContent" runat="server" style="left: 618px; top: 319px">
            <table style="font-size: 10pt; background: #000080; color: #FFFFFF; width: 100%">
                <tr>
                    <td>
                        加到已有交接单</td>
                    <td style="text-align: right">
                        <input type="button" value="x" style="height: 18px; width: 18px; font-size: 7pt;
                            padding: 0; vertical-align: middle; text-align: center" id="BtnQuitPopuWin1" onclick="close_popudiv()" /></td>
                </tr>
            </table>
            <div style="overflow-y: scroll; overflow-x: none; width: 300px; height: 200px; border: solid 1px Black;
                margin: 10px">
                <asp:GridView ID="GVJjdNo" runat="server" AutoGenerateColumns="False" EmptyDataText="无符合条件交接单"
                    OnRowDataBound="GVJjdNo_RowDataBound" CssClass="gv" BorderStyle="Inset">
                    <Columns>
                        <asp:BoundField DataField="jjd_no" HeaderText="选择一个交接单号">
                            <HeaderStyle Width="300px" HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <table style="margin: 0 0 0 10px">
                <tr>
                    <td>
                        交接单号：</td>
                    <td>
                        <asp:TextBox ID="TxtJjdNo" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            <hr />
            <table style="margin: 0 0 20px 10px">
                <tr>
                    <td>
                        <asp:Button ID="BtnInsertJjd" runat="server" Text="加入交接单" OnClick="BtnInsertJjd_Click"
                            CssClass="button_2" />
                        <input id="BtnQuitPopuWin" size="" type="button" value="取消" class="button_1" onclick="close_popudiv()" style="margin-bottom:0px" /></td>
                </tr>
            </table>
        </div>
        <div id="divJjdDisplay" runat="server">
            <table style="width: 600px">
                <tr>
                    <td>
                        单号：</td>
                    <td>
                        <asp:TextBox ID="TxtJjdNo1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        状态：</td>
                    <td>
                        <asp:TextBox ID="TxtState1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="LnkBtnBackCreateJjd" runat="server" OnClick="LnkBtnBackCreateJjd_Click">[返回]</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        接收日期：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptDate1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        接收地：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptPlace1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        接收部门：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptDept1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        接收人：</td>
                    <td>
                        <asp:TextBox ID="TxtReceiptPerson1" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="LnkBtnJjdExtHeadInfoDisplay" runat="server" OnClick="LnkBtnJjdExtHeadInfoDisplay_Click">[详细信息]</asp:LinkButton>
                </tr>
            </table>
            <hr />
            <table >
                <tr>
                    <td>
                        <asp:Button ID="BtnPrint" runat="server" Text="打印交接单" OnClick="BtnPrint_Click" /></td>
                </tr>
            </table>
            <div id="divJjdHeadInfo" runat="server">
                <div style="border-width: 0; margin: 0 10px -10px 10px">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right;">
                                <asp:LinkButton ID="LnkBtnJjdExtHeadInfoEdit" runat="server" OnClick="LnkBtnJjdExtHeadInfoEdit_Click">[编辑]</asp:LinkButton><a
                                    href="#" onclick="myf('divJjdHeadInfo').style.display='none'"> [隐藏]</a></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                装货部门信息</td>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 200px">
                                装货地（仓库）：
                            </td>
                            <td>
                                <asp:Label ID="LblZHd" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtZHd" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                装货人：</td>
                            <td>
                                <asp:Label ID="LblZHr" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtZHr" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                装货人电话</td>
                            <td>
                                <asp:Label ID="LblZHDh" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtZHDh" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                车辆到达装货地时间：</td>
                            <td>
                                <asp:Label ID="LblZHtime" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtZHTime" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                装货开始：</td>
                            <td>
                                <asp:Label ID="LblZHSTime" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtZHSTime" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                装货结束：</td>
                            <td>
                                <asp:Label ID="LblZHETime" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtZHETime" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                需求部门信息</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 200px">
                                需求部门/项目：
                            </td>
                            <td>
                                <asp:Label ID="LblXQbm" runat="server"></asp:Label><asp:TextBox ID="TxtXQbm" runat="server"
                                    Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                联系人姓名：</td>
                            <td>
                                <asp:Label ID="LblXQlxr" runat="server"></asp:Label><asp:TextBox ID="TxtXQlxr" runat="server"
                                    Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                联系人电话：</td>
                            <td>
                                <asp:Label ID="LblXQdh" runat="server"></asp:Label><asp:TextBox ID="TxtXQdh" runat="server"
                                    Width="200px"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                承运部门信息</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 200px">
                                承运公司名称：
                            </td>
                            <td>
                                <asp:Label ID="LblCYgs" runat="server"></asp:Label><asp:TextBox ID="TxtCYgs" runat="server"
                                    Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                承运人：</td>
                            <td>
                                <asp:Label ID="LblCYPer" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtCYPer" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                承运人电话：</td>
                            <td>
                                <asp:Label ID="LblCYdh" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtCYdh" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                车辆牌号：</td>
                            <td>
                                <asp:Label ID="LblCYph" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtCYph" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                驾驶证号：</td>
                            <td>
                                <asp:Label ID="LblCYjz" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtCYjz" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                收货部门信息</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 200px">
                                卸货开始：</td>
                            <td>
                                <asp:Label ID="LblXHSTime" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtXHSTime" runat="server" Width="200px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                卸货结束：</td>
                            <td>
                                <asp:Label ID="LblXHETime" runat="server"></asp:Label>
                                <asp:TextBox ID="TxtXHETime" runat="server" Width="200px"></asp:TextBox></td>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="border-width: 0; margin: 0 10px 0 10px">
                    <table style="width: 100%; margin: 0">
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkSafe" runat="server" Text="保险" /></td>
                            <td style="text-align: right">
                                <asp:Button ID="BtnSaveJjdHeadInfo" runat="server" CssClass="button_1" Text="保存"
                                    OnClick="BtnSaveJjdHeadInfo_Click" /><asp:Button ID="BtnJjdHeadExtInfoEditQuit" runat="server"
                                        CssClass="button_1" Text="取消" OnClick="BtnJjdHeadExtInfoEditQuit_Click" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox2">
                <asp:GridView ID="GVDisplayJjdLine" runat="server" AutoGenerateColumns="False" Width="1200px"
                    CssClass="gv" OnRowDataBound="GVDisplayJjdLine_RowDataBound" OnRowCommand="GVDisplayJjdLine_RowCommand">
                    <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="删除">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/delete_.gif"
                                CommandName="DeleteJjdLine" OnClientClick="return window.confirm('确定要删除吗?')" CommandArgument= '<%# Eval("objid")+"^"+Eval("rowversion") %>' ToolTip="删除"  />
                            <asp:Image ID="ImgNotAccess" runat="server" ImageUrl="~/images/notaccess1.gif" />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle  HorizontalAlign="Center" />
                    </asp:TemplateField>
                        <asp:BoundField DataField="requisition_id" HeaderText="申请号">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="part_no" HeaderText="物资编号">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="物资描述">
                            <ItemTemplate>
                                <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 300px;
                                    margin: 0" title="<%# Eval("part_description")%>">
                                    <%# Eval("part_description")%>
                                </p>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="part_unit" HeaderText="单位">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="req_qty" HeaderText="下达数量">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="zh_qty" HeaderText="配送数量">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="xh_qty" HeaderText="接收数量">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rowstate" HeaderText="状态">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="project_id" HeaderText="项目">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="matr_seq_no" HeaderText="材料顺序号">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="matr_seq_line_no" HeaderText="行号">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>                
            </div>
        </div>
        <div id="divJjdQuery" runat="server">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        交接单号：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtQJjdNo" name="TxtRecieveDate" runat="server" AutoPostBack="True" Width="142px"></asp:TextBox></td>
                    <td style="width: 25px">
                        &nbsp;</td>
                    <td>
                        需求日期：</td>
                    <td>
                        <asp:TextBox ID="TxtQReceiptDate" runat="server" AutoPostBack="True" name="TxtRecieveDate"
                            Width="142px" onfocus="WdatePicker();"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:LinkButton ID="LnkBtnQBackCreate" runat="server" OnClick="LnkBtnQBackCreate_Click">[返回]</asp:LinkButton></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        接收地：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlQReceiptPlace" runat="server"
                            Width="150px">
                        </asp:DropDownList></td>
                    <td>
                        <span style="color: #ff0000"></span>
                    </td>
                    <td>
                        接收部门：
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlQReceiptDept" runat="server"
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 25px">
                        <span style="color: #ff0000"></span>
                    </td>
                    <td style="width: 53px">
                        接收人：
                    </td>
                    <td>
                        &nbsp;<asp:TextBox ID="TxtQReceiptPerson" runat="server" AutoPostBack="True" name="TxtRecieveDate" Width="142px"></asp:TextBox></td>
                    <td style="width: 25px">
                        <span style="color: #ff0000"></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnQJjdQuery" runat="server" Text="查询" OnClick="BtnQJjdQuery_Click"
                            CssClass="button_1" /></td>
                </tr>
            </table>
            <div style="overflow-x: auto; overflow-y: none; width: 100%" id="gvbox3">
            <asp:GridView ID="GVJjdList" runat="server" AutoGenerateColumns="False" Width="720px"
                CssClass="gv" AllowPaging="True" OnRowCommand="GVJjdList_RowCommand" OnPageIndexChanging="GVJjdList_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="单号">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkBtnJjdNo" runat="server" Text='<%# Bind("jjd_no") %>' CommandName="displayJjd"
                                CommandArgument='<%# Bind("jjd_no") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="receipt_date_str" HeaderText="接收时间">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receipt_place" HeaderText="接收地">
                        <HeaderStyle Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receipt_dept_name" HeaderText="接收部门">
                        <HeaderStyle Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="receipt_person" HeaderText="接收人">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="state" HeaderText="状态">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        </div>
        </div>
        <iframe id="DivShim" src="javascript:false;" scrolling="no" frameborder="0" style="position:absolute; top:0px; left:0px; display:none;"></iframe>
    </form>
</body>
</html>
