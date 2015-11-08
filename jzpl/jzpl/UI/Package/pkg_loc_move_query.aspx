<%@ Page Language="C#" AutoEventWireup="true" Codebehind="pkg_loc_move_query.aspx.cs" Inherits="jzpl.UI.Package.pkg_loc_move_query" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包移库历史纪录查询</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .tblhead td{border:1px solid #FFF;border-collapse:collapse;padding-left:6px;padding-top:2px}
    </style>
    <script type="text/javascript" src="../script/common.js"></script>
    <script language="javascript">
    window.onload=function(){init();}
    function init(){
    SetGVBoxHeight("PnlData","GridView1");
    }
        function winOpen()
        {
            var obj= window.showModalDialog("pkg_lov.aspx",window,"status:no;dialogWidth:610px;dialogHeight:510px");
            if(typeof(obj)=="undefined")
            {
                document.getElementById("txt_part_no").value="";
                return;
            }
            document.getElementById("txt_part_no").value=obj[0];  
        }
       function ShowXJPage()
        {
            var dbbhValue=document.getElementById("txt_part_no").value;
            if(dbbhValue=="" || dbbhValue==null)
            {
                alert('请首先选择大包信息！');
                return;
            }
            var obj= window.showModalDialog("pkg_part_lov.aspx?dbbh="+dbbhValue+"",window,"status:no;dialogWidth:610px;dialogHeight:556px");
           if(typeof(obj)=="undefined")
            {
                document.getElementById("txt_small_part").value="";
                document.getElementById("txt_small_name").value="";
                return;
            }
            document.getElementById("txt_small_part").value=obj[0];
            document.getElementById("txt_small_name").value=obj[2];   
        }   
    </script>

    <script type="text/javascript" src="../../UI/script/DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="title">大包移库历史纪录查询<hr /></div>
            <table >
                <tr>
                    <td style="width: 80px;">
                        大包编号:</td>
                    <td style="width: 120px;">
                        <asp:TextBox ID="txt_part_no" runat="server" Width="120px"></asp:TextBox></td>
                    <td style="width: 40px">
                        <img src="../../images/Search.gif" onclick="winOpen()" /></td>
                    <td style="width: 80px;">
                        小件编号:</td>
                    <td style="width: 120px;">
                        <asp:TextBox ID="txt_small_part" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td style="width: 40px">
                        <img src="../../images/Search.gif" onclick="ShowXJPage()" />
                    </td>
                    <td style="width: 80px">
                        小件名称:</td>
                    <td style="width: 200px">
                        <asp:TextBox ID="txt_small_name" ReadOnly="false" runat="server" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        保管员:</td>
                    <td>
                        <asp:TextBox ID="txt_person" runat="server" Width="120px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        移库日期:</td>
                    <td>
                        <asp:TextBox ID="txt_arrival_date" runat="server" onfocus="WdatePicker();"
                            Width="120px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" CssClass="button_1" Text="查询" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        <asp:Label ID="LblMsg" runat="server"></asp:Label>
        <div id="PnlData" runat="server" style="overflow-x: auto; overflow-y: none; width: 100%;border-collapse:collapse;border:solid 1px #000; "  >
            <asp:GridView ID="GridView1" ForeColor="#333333" runat="server" Width="1400px" AutoGenerateColumns="False" PageSize="16" OnPageIndexChanging="GridView1_PageIndexChanging"
                CssClass="gv">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <table style="width: 1400px" cellpadding="0" cellspacing="0" class="tblhead">
                                <tr>
                                    <td rowspan="2" style="width: 100px">
                                        大包编号</td>
                                    <td rowspan="2" style="width: 200px">
                                        大包描述</td>
                                    <td rowspan="2" style="width: 200px">
                                        零件编号</td>
                                    <td rowspan="2" style="width: 200px">
                                        零件描述</td>
                                    <td colspan="2" style="text-align: center">
                                        移库前</td>
                                    <td colspan="2" style="text-align: center">
                                        移库后</td>
                                    <td colspan="3" style="text-align: center">
                                        移库数量</td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        区域</td>
                                    <td style="width: 100px">
                                        位置</td>
                                    <td style="width: 100px">
                                        区域</td>
                                    <td style="width: 100px">
                                        位置</td>
                                    <td style="width: 100px">
                                        合格</td>
                                    <td style="width: 100px">
                                        不合格</td>
                                    <td style="width: 100px">
                                        免检</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <table cellpadding="0" cellspacing="0"  style="width: 1400px" class="tblhead" >
                                <tr>
                                    <td rowspan="2" style="width: 100px">
                                        <%# Eval("package_no")%></td>
                                    <td rowspan="2" style="width: 200px">
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 80px;"
                                            title="<%# Server.HtmlEncode(Eval("package_name").ToString()) %>">
                                            <%# Server.HtmlEncode(Eval("package_name").ToString())%>
                                        </p></td>
                                    <td rowspan="2" style="width: 200px">
                                        <%# Eval("part_no") %></td>
                                    <td rowspan="2" style="width: 200px">
                                        <p style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; width: 80px;"
                                            title="<%# Server.HtmlEncode(Eval("part_name_e").ToString())%>">
                                            <%# Server.HtmlEncode(Eval("part_name_e").ToString())%>
                                        </p></td>                                   
                                
                                    <td style="width: 100px">
                                       <%# Eval("area_name_s")%></td>
                                    <td style="width: 100px">
                                        <%# Eval("location_name_s")%></td>
                                    <td style="width: 100px">
                                        <%# Eval("area_name_d")%></td>
                                    <td style="width: 100px">
                                        <%# Eval("location_name_d")%></td>
                                    <td style="width: 100px">
                                        <%# Eval("ok_qty")%></td>
                                    <td style="width: 100px">
                                        <%# Eval("bad_qty")%></td>
                                    <td style="width: 100px">
                                        <%# Eval("nocheck_qty")%></td>
                                </tr>
                            </table>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        
    </form>
</body>
</html>
