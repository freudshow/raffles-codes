<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_part_receive.aspx.cs"
    Inherits="jzpl.UI.Package.pkg_part_receive" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包物资接收</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript">
    window.onload = function(){init();}
    function init()
    {
        SetGVBoxHeight("gvbox","GVData");
        
        myf("TxtPartNo").readOnly=true;           
        myf("TxtProject").readOnly=true;   
        myf("TxtPkgNo").readOnly=true;   
        myf("TxtPkgName").readOnly=true;   
        myf("TxtPO").readOnly=true;   
        myf("TxtPartName").readOnly=true;   
        myf("TxtPartNameE").readOnly=true;   
        myf("TxtPartSpec").readOnly=true;   
        myf("TxtPartUnit").readOnly=true;   
    }
    
    function show_part_lov()
    {
         var ret_= window.showModalDialog("pkg_part_lov2.aspx",window,"status:no;dialogWidth:650px;dialogHeight:700px");
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
            
            ShowStoreInfo();           
        }
    }
    
    function ShowStoreInfo()
    {
         __doPostBack("BtnQuery","");
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
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="title">
            大包物资接收<hr />
        </div>
        <div style="margin-bottom: 4px">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        零件编码：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPartNo" runat="server" Width="150px" BorderStyle="Groove"></asp:TextBox></td>
                    <td width="60">
                        <img src="../../images/Search.gif" style="cursor: hand;" onclick="show_part_lov()"
                            id="IMG1" />
                    </td>
                    <td style="width: 80px">
                    </td>
                    <td style="width: 120px">
                    </td>
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
            </table>
            <hr />
            <asp:Button ID="BtnQuery" runat="server" OnClick="BtnQuery_Click" Text="Button" UseSubmitBehavior="False"
                CssClass="hidden" />
            <table>
                <tr>
                    <td style="width: 120px">
                        公司：</td>
                    <td style="width: 460px">
                        <asp:DropDownList ID="DdlCompany" runat="server" Width="455px" CausesValidation="True"
                            AutoPostBack="True" OnSelectedIndexChanged="DdlCompany_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">
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
            <table>
                <tr>
                    <td style="width: 120px">
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
                    <td style="width: 120px">
                        合格数量：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPartOk" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 60px">
                    </td>
                    <td style="width: 50px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        不合格数量：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPartBad" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 60px">
                    </td>
                    <td style="width: 50px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        免检数量：</td>
                    <td style="width: 160px">
                        <asp:TextBox ID="TxtPartNocheck" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 60px">
                    </td>
                    <td style="width: 50px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <asp:Button ID="BtnSave" runat="server" CssClass="button_1" Text="保存" OnClick="BtnSave_Click" />
        </div>
        <div style="overflow-x: auto; overflow-y: none; width: 100%;" id="gvBox" runat="server">
            <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" Width="800px"
                CssClass="gv">
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" />
                <EmptyDataTemplate>
                    <table class="gv">
                        <tr>
                            <th style="width: 80px">
                                合格
                            </th>
                            <th style="width: 80px">
                                不合格
                            </th>
                            <th style="width: 80px">
                                免检
                            </th>
                            <th style="width: 100px">
                                公司
                            </th>
                            <th style="width: 150px">
                                区域
                            </th>
                            <th style="width: 150px">
                                位置
                            </th>
                            <th style="width: 80px">
                                到货ID
                            </th>
                            <th style="width: 80px">
                                到达日期
                            </th>
                        </tr>
                        <tr>
                            <td colspan="8">
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="合格" DataField="part_ok_qty">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="不合格" DataField="part_bad_qty">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="免检" DataField="no_check_qty">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="公司" DataField="company_id">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="区域" DataField="area">
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="位置" DataField="location">
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="到货ID" HeaderStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="GVLblArrivalId" runat="server" Text='<%# Eval("arrival_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="到达日期" DataField="arrived_date" DataFormatString="{0:d}"
                        HtmlEncode="False">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="location_id">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
