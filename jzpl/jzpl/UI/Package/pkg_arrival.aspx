<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_arrival.aspx.cs" Inherits="Package.pkg_arrival" ValidateRequest="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包到货登记</title>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript" language="javascript">
    
    window.onload=function(){
    init();
    }
    function init(){
    myf('TxtDBbh').readOnly=true;
    myf('TxtXJbh').readOnly=true;
    SetGVBoxHeight("gvbox","GVSubmitData");
    }
    function ShowDBPage()
    {   
                
        var obj = ShowMD("pkgLov");
        if(typeof(obj)=="undefined")
        {            
            return;
        }
        
        myf("TxtDBbh").value=obj[0];
        //$("TxtDBbh").readOnly=true;
        //$("HdisInput").value="false";
        myf("TxtXJbh").value="";
        myf("TxtXJmccn").value="";
        myf("TxtXJmcen").value="";
        myf("TxtXJgg").value="";
        myf("TxtXJgdh").value="";
        myf("TxtXJhth").value="";
        myf("TxtPO").value="";
        myf("DdlPartUnit").value="0";
    }
    function ShowXJPage()
    {
        
        var dbbhValue=myf("TxtDBbh").value;
        if(dbbhValue=="" || dbbhValue==null)
        {
            alert('请首先选择大包信息！');
            return;
        }
        var obj = ShowMD("pkgPartLov",dbbhValue);
        
        if(typeof(obj)!="undefined")
        {            
            myf("TxtXJbh").value=obj[0];
            myf("TxtXJmccn").value=obj[1];
            myf("TxtXJmcen").value=obj[2];
            myf("TxtXJgg").value=obj[3];
            myf("DdlPartUnit").value=obj[4];
            myf("TxtXJgdh").value=obj[5];
            myf("TxtXJhth").value=obj[6];
            myf("TxtPO").value=obj[7];
            //-------
            //document.getElementById("HdisInput").value="true";
            myf("TxtXJmcen").disabled = true;
            myf("TxtXJmccn").disabled = true;
            myf("TxtXJgg").disabled = true;          
            myf("DdlPartUnit").disabled = true;
            myf("TxtXJgdh").disabled = true;
            myf("TxtXJhth").disabled = true; 
            myf("TxtPO").disabled = true;           
            myf("ChkPayFlag").disabled=true;
        }
    }
    function ShowLocPage()
    {
        //var company = $("DdlCompany").value;
        //var areaid = $("DdlArea").value;
        var paras = new Array(myf("DdlCompany").value,myf("DdlArea").value);
        var obj= ShowMD("pkgLocLov",paras);
        if(typeof(obj)=="undefined")
        {            
            return;
        }
        
        myf("DdlCompany").value=obj[0];
        myf("DdlArea").value=obj[1];
        myf("TxtLocationInput").value=obj[2];        
    }
    function ClickLocation(locationValue,lID)
    {
        document.getElementById("TxtArea").readOnly=true;
        document.getElementById("TxtLocation").readOnly=true;
        
        var ddl = document.getElementById("DDLArea")   
        var index = ddl.selectedIndex;
        var Value = ddl.options[index].value;   
        var Text = ddl.options[index].text;
        document.getElementById("TxtArea").value=Text;
        document.getElementById("TxtLID").value=lID;
        document.getElementById("TxtLocation").value=locationValue;
    }
    function ddlClick()
    {
        document.getElementById("TxtArea").value="";
        document.getElementById("TxtLocation").value="";
    }
    function checkIsNull(inputText)
    {        
        if(inputText.replace(/\s/g,"")=="")
        {             
             return true;	    
        } 
        return false;    
    }
    function checkInput()
    {
        //检查必添项小件名称、到货日期、小件位置列表
        if(checkIsNull(myf("TxtXJmcen").value)){alert("请填写小件描述（英）。");return false;}
        if(checkIsNull(myf("TxtDHrq").value)){alert("请填写到货日期。");return false;}
        if(myf("LblTotal").innerText=="0"){alert("请录入到货库位信息。");return false;}
        if(myf("DdlPartUnit").value=="0"){alert("请录入小件单位。");return false;}
    }
    
    function CheckLocInput()
    {
        if(!checkNum(myf("TxtAmount")))
        {
            alert("请填写到货数量。");
            return false;
        } 
        if(checkIsNull(myf("TxtLocationInput").value))
        {
            alert("请填写库位信息。");
            return false;
        }
        return true;
    }
    function EnterAgain()
    {
        myf("TxtXJbh").value="";
        myf("TxtXJmcen").disabled = false;
        myf("TxtXJmccn").disabled = false;
        myf("TxtXJgg").disabled = false;          
        myf("DdlPartUnit").disabled = false;
        myf("TxtXJgdh").disabled = false;
        myf("TxtXJhth").disabled = false;          
        myf("TxtDHrq").disabled = false;  
        myf("TxtPO").disabled = false;
        myf("ChkPayFlag").disabled=false;
              
    }
    
    function LockInput()
    {
        myf("TxtXJmcen").disabled = true;
        myf("TxtXJmccn").disabled = true;
        myf("TxtXJgg").disabled = true;          
        myf("DdlPartUnit").disabled = true;
        myf("TxtXJgdh").disabled = true;
        myf("TxtXJhth").disabled = true;          
        myf("TxtDHrq").disabled = true; 
        myf("TxtPO").disabled = true;
        myf("ChkPayFlag").disabled=true;
    }
    
    </script>

    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            大包到货登记<hr />
        </div>
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 130px">
                        大包编号：</td>
                    <td style="width: 160px">
                        <input id="TxtDBbh" type="text" style="width: 150px; border-style: groove" runat="server" /></td>
                    <td style="width: 40px">
                        <img src="../../images/Search.gif" onclick="ShowDBPage()" />
                    </td>
                    <td style="width: 100px">
                        小件编号：</td>
                    <td style="width: 160px">
                        <input id="TxtXJbh" type="text" style="width: 150px; border-style: groove" runat="server" />
                    </td>
                    <td style="width: 40px">
                        <img src="../../images/Search.gif" onclick="ShowXJPage()" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 130px">
                        小件名称（英文）：</td>
                    <td style="width: 460px">
                        <asp:TextBox ID="TxtXJmcen" runat="server" Width="450px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        小件名称（中文）：</td>
                    <td>
                        <asp:TextBox ID="TxtXJmccn" runat="server" Width="450px"></asp:TextBox></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 130px">
                        规格：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtXJgg" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 40px">
                    </td>
                    <td style="width: 100px">
                        单位：</td>
                    <td style="width: 160px">
                        <%--<asp:TextBox ID="TxtXJdw" runat="server" Width="150px"></asp:TextBox>--%>
                        <asp:DropDownList ID="DdlPartUnit" runat="server" Width="157px">
                        </asp:DropDownList></td>
                    <td style="width: 70px">
                    </td>
                </tr>
                <tr>
                    <td>
                        关单号：</td>
                    <td>
                        <asp:TextBox ID="TxtXJgdh" runat="server" Width="150px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        合同号：</td>
                    <td>
                        <asp:TextBox ID="TxtXJhth" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 70px">
                    </td>
                </tr>
                <tr>
                    <td>
                        PO号：</td>
                    <td>
                        <asp:TextBox ID="TxtPO" runat="server" Width="150px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        到货日期：</td>
                    <td>
                        <asp:TextBox ID="TxtDHrq" runat="server" Width="150px" onfocus="WdatePicker();"></asp:TextBox></td>
                    <td style="text-align: left; padding-left: 5px; width: 70px;">
                        </td>
                </tr>
               
                <tr>
                    <td>
                        备注：</td>
                    <td colspan="4">
                        <asp:TextBox ID="TxtExt1" runat="server" Height="50px" TextMode="MultiLine" Width="452px"></asp:TextBox></td>
                    <td style="width: 70px">
                    
                    </td>
                </tr>
                
                 <tr>
                    <td>
                        不付款：</td>
                    <td>
                        <asp:CheckBox ID="ChkPayFlag" runat="server" /></td>
                    <td>
                    </td>
                    <td>
                        </td>
                    <td style="padding-right: 5px; text-align: left">
                        <u onclick="EnterAgain();" style="color: #0000ff; cursor: hand;">[重新录入]</u></td>
                    <td style="width: 70px" >
                    
                    </td>
                </tr>
            </table>
            <hr />
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 130px">
                        公司：</td>
                    <td style="width: 460px">
                        <asp:DropDownList ID="DdlCompany" runat="server" Width="455px" CausesValidation="True"
                            AutoPostBack="True" OnSelectedIndexChanged="DdlCompany_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        仓库区域：</td>
                    <td style="width: 460px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="DDLArea" runat="server" Width="455px">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlCompany" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 130px">
                        库位：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtLocationInput" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 60px">
                        <u onclick="ShowLocPage()" style="color: #0000ff; cursor: hand;">[查找]</u>
                    </td>
                    <td style="width: 50px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        数量：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtAmount" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 60px">
                    </td>
                    <td style="width: 50px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="BtnAddRows" runat="server" CssClass="button_1" Text="确认" OnClick="BtnAddRows_Click" OnClientClick="return CheckLocInput()"/>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="OKGrid" runat="server" AutoGenerateColumns="False" Width="600px"
                        CssClass="gv" OnRowEditing="OKGrid_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IbtnDelRow" runat="server" CommandName="Edit" OnClientClick="return confirm('确认要删除吗？');"
                                        ImageUrl="~/images/delete_.gif" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="company_id" HeaderText="公司">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="area" HeaderText="区域">
                                <HeaderStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="location" HeaderText="位置">
                                <HeaderStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amount" HeaderText="数量">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table class="gv">
                                <tr>
                                    <th style="width: 60px">
                                        删除
                                    </th>
                                    <th style="width: 100px">
                                        公司
                                    </th>
                                    <th style="width: 250px">
                                        区域
                                    </th>
                                    <th style="width: 250px">
                                        位置
                                    </th>
                                    <th style="width: 140px">
                                        数量
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="5" style="padding-left: 10px">
                                        无到货信息。</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <table>
                        <tr>
                            <td style="width: 370px; text-align: left">
                                <asp:Button ID="BtnSubmit" runat="server" CssClass="button_1" Text="提交" OnClientClick="return checkInput();"
                                    OnClick="BtnSubmit_Click" />&nbsp;<asp:Button ID="BtnCancel" runat="server" CssClass="button_1"
                                        Text="取消" OnClick="BtnCancel_Click" />
                                <asp:Button ID="BtnQuery" runat="server" CssClass="button_1"
                                        Text="查询" OnClick="BtnQuery_Click" /></td>
                            <td style="width: 200px; text-align: right">
                                合计：<asp:Label ID="LblTotal" runat="server" Font-Bold="True" Text="0" Width="100px"></asp:Label></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnAddRows" />
                    <asp:AsyncPostBackTrigger ControlID="BtnCancel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <table>
            <tr>
                <td>
                    已提交到货信息：</td>
            </tr>
        </table>
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div id="gvbox" style="overflow-x: auto; overflow-y: none; width: 100%">
                        <asp:GridView ID="GVSubmitData" runat="server" AutoGenerateColumns="False" Width="1920px"
                            CssClass="gv" OnRowEditing="GVSubmitData_RowEditing">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="删除">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IbtnDelRow" runat="server" CommandName="Delete" OnClientClick="return confirm('确认要删除吗？');"
                                            ImageUrl="~/images/delete_.gif" CommandArgument='<%# Eval("arrived_id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IbtnDelRow" runat="server" CommandName="Edit" OnClientClick="return confirm('确认要删除吗？');"
                                        ImageUrl="~/images/delete_.gif" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:BoundField DataField="package_no" HeaderText="大包编码" HeaderStyle-Width="120px" />
                                <asp:TemplateField HeaderStyle-Width="180px" HeaderText="大包描述">
                                    <ItemTemplate>
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 180px"
                                            title="<%# Server.HtmlEncode(Eval("pkg_name").ToString())%>">
                                            <%# Eval("pkg_name")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="part_no" HeaderText="零件编码" HeaderStyle-Width="150px" />
                                <asp:TemplateField HeaderStyle-Width="180px" HeaderText="零件描述（英）">
                                    <ItemTemplate>
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 180px"
                                            title="<%# Server.HtmlEncode(Eval("part_name_e").ToString())%>">
                                            <%# Eval("part_name_e")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="180px" HeaderText="零件描述（中）">
                                    <ItemTemplate>
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 180px"
                                            title="<%# Server.HtmlEncode(Eval("part_name").ToString())%>">
                                            <%# Eval("part_name")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="part_unit" HeaderText="单位" HeaderStyle-Width="60px" />
                                <asp:BoundField DataField="req_qty" HeaderText="登记数量" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="right" />
                                <asp:BoundField DataField="arr_date_ch" HeaderText="到货日期" HeaderStyle-Width="80px" />
                                <asp:BoundField DataField="part_spec" HeaderText="零件规格" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="po_no" HeaderText="PO号" HeaderStyle-Width="80px" />
                                <asp:BoundField DataField="dec_no" HeaderText="关单号" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="contract_no" HeaderText="合同号" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="project_id" HeaderText="项目号" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderStyle-Width="200px" HeaderText="备注">
                                    <ItemTemplate>
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 200px"
                                            title="<%# Server.HtmlEncode(Eval("ext1").ToString())%>">
                                            <%# Eval("ext1")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <EmptyDataTemplate>
                                <table class="gv">
                                    <tr>
                                        <th style="width: 40px">
                                            删除</th>                                        
                                        <th style="width: 80px">
                                            大包编码</th>
                                        <th style="width: 180px">
                                            大包描述</th>
                                        <th style="width: 150px">
                                            零件编码</th>
                                        <th style="width: 180px">
                                            零件描述（英）</th>
                                        <th style="width: 180px">
                                            零件描述（中）</th>  
                                        <th style="width: 60px">
                                            单位</th>
                                        <th style="width: 150px">
                                            登记数量</th>
                                        <th style="width: 80px">
                                            到货日期</th>
                                        <th style="width: 150px">
                                            零件规格</th>
                                        <th style="width: 80px">
                                            PO号</th>
                                        <th style="width: 150px">
                                            关单号</th>
                                        <th style="width: 150px">
                                            合同号</th>
                                        <th style="width: 100px">
                                            项目号</th>
                                        <th style="width: 200px">
                                            备注</th>
                                    </tr>
                                    <tr>
                                        <td colspan="19" style="padding-left: 10px">
                                            无已提交到货信息。</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                   </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
